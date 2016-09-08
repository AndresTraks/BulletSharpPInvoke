using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BulletSharp
{
    public class Dna
    {
        public class ElementDecl
        {
            public TypeDecl Type { get; private set; }
            public NameInfo Name { get; private set; }

            public ElementDecl(TypeDecl type, NameInfo name)
            {
                Type = type;
                Name = name;
            }

            public override bool Equals(object obj)
            {
                ElementDecl other = obj as ElementDecl;
                if (other == null)
                {
                    return false;
                }
                return Type.Equals(other.Type) && Name.Equals(other.Name);
            }

            public override int GetHashCode()
            {
                return Type.GetHashCode() + Name.GetHashCode();
            }

            public override string ToString()
            {
                return Type + ": " + Name.ToString();
            }
        }

        public class StructDecl
        {
            public TypeDecl Type { get; set; }
            public ElementDecl[] Elements { get; set; }

            public ElementDecl FindElement(Dna dna, bool brokenDna, NameInfo name, out int offset)
            {
                offset = 0;
                foreach (ElementDecl element in Elements)
                {
                    if (element.Name.Equals(name))
                    {
                        return element;
                    }
                    int eleLen = dna.GetElementSize(element);
                    if (brokenDna)
                    {
                        if (element.Type.Name.Equals("short") && element.Name.Name.Equals("int"))
                        {
                            eleLen = 0;
                        }
                    }
                    offset += eleLen;
                }
                return null;
            }

            public override bool Equals(object obj)
            {
                StructDecl other = obj as StructDecl;
                if (other == null)
                {
                    return false;
                }

                int elementCount = Elements.Length;
                if (elementCount != other.Elements.Length)
                {
                    return false;
                }

                for (int i = 0; i < elementCount; i++)
                {
                    if (!Elements[i].Equals(other.Elements[i]))
                    {
                        return false;
                    }
                }

                return Type.Equals(other.Type);
            }

            public override int GetHashCode()
            {
                return Type.GetHashCode() + Elements.Length;
            }

            public override string ToString()
            {
                return Type.ToString();
            }
        }

        public class TypeDecl
        {
            public StructDecl Struct { get; set; }
            public short Length { get; set; }
            public string Name { get; private set; }

            public TypeDecl(string name)
            {
                Name = name;
            }

            public override bool Equals(object obj)
            {
                TypeDecl other = obj as TypeDecl;
                if (other == null)
                {
                    return false;
                }
                return Name.Equals(other.Name) && Length == other.Length;
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode() + Length;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public class NameInfo
        {
            public string Name { get; private set; }
            public bool IsPointer { get; private set; }
            public int Dim0 { get; set; }
            public int Dim1 { get; set; }

            public int ArraySizeNew { get { return Dim0 * Dim1; } }

            public string CleanName
            {
                get
                {
                    int p = Name.IndexOf('[');
                    if (p != -1)
                    {
                        return Name.Substring(0, Name.IndexOf('['));
                    }
                    return Name;
                }
            }

            public NameInfo(string name)
            {
                Name = name;
                IsPointer = name[0] == '*' || name[1] == '*';
                
                int bp = name.IndexOf('[') + 1;
                if (bp == 0)
                {
                    Dim0 = 1;
                    Dim1 = 1;
                    return;
                }
                int bp2 = name.IndexOf(']');
                Dim1 = int.Parse(name.Substring(bp, bp2 - bp));

                // find second dim, if any
                bp = name.IndexOf('[', bp2) + 1;
                if (bp == 0)
                {
                    Dim0 = 1;
                    return;
                }
                bp2 = name.IndexOf(']');
                Dim0 = Dim1;
                Dim1 = int.Parse(name.Substring(bp, bp2 - bp));
            }

            public override bool Equals(object obj)
            {
                NameInfo other = obj as NameInfo;
                if (other == null)
                {
                    return false;
                }
                return Name.Equals(other.Name);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }

            public override string ToString()
            {
                return Name;
            }
        }

        private NameInfo[] _names;
        private StructDecl[] _structs;
        private TypeDecl[] _types;
        private Dictionary<string, StructDecl> _structReverse;

        private int _ptrLen;

        public static Dna Load(BulletReader reader, bool swap)
        {
            var dna = new Dna();
            dna.Init(reader, swap);
            return dna;
        }

        public int GetElementSize(ElementDecl element)
		{
            return element.Name.ArraySizeNew * (element.Name.IsPointer ? _ptrLen : element.Type.Length);
		}

        public string GetName(int i)
        {
            return _names[i].Name;
        }

        public StructDecl GetStruct(int i)
        {
            return _structs[i];
        }

        public StructDecl GetStruct(string typeName)
        {
            StructDecl s;
            if (_structReverse.TryGetValue(typeName, out s))
            {
                return s;
            }
            return null;
        }

        public void Init(BulletReader reader, bool swap)
        {
            Stream stream = reader.BaseStream;

            // SDNA
            byte[] code = reader.ReadBytes(8);
            string codes = Encoding.ASCII.GetString(code);

            // NAME
            if (!codes.Equals("SDNANAME"))
            {
                throw new InvalidDataException();
            }
            int numNames = reader.ReadInt32();
            _names = new NameInfo[numNames];
            for (int i = 0; i < numNames; i++)
            {
                string name = reader.ReadNullTerminatedString();
                _names[i] = new NameInfo(name);
            }
            stream.Position = (stream.Position + 3) & ~3;

            // Type names
            code = reader.ReadBytes(4);
            codes = Encoding.ASCII.GetString(code);
            if (!codes.Equals("TYPE"))
            {
                throw new InvalidDataException();
            }
            int numTypes = reader.ReadInt32();
            _types = new TypeDecl[numTypes];
            for (int i = 0; i < numTypes; i++)
            {
                string type = reader.ReadNullTerminatedString();
                _types[i] = new TypeDecl(type);
            }
            stream.Position = (stream.Position + 3) & ~3;

            // Type lengths
            code = reader.ReadBytes(4);
            codes = Encoding.ASCII.GetString(code);
            if (!codes.Equals("TLEN"))
            {
                throw new InvalidDataException();
            }
            for (int i = 0; i < numTypes; i++)
            {
                _types[i].Length = reader.ReadInt16();
            }
            stream.Position = (stream.Position + 3) & ~3;

            // Structures
            code = reader.ReadBytes(4);
            codes = Encoding.ASCII.GetString(code);
            if (!codes.Equals("STRC"))
            {
                throw new InvalidDataException();
            }
            int numStructs = reader.ReadInt32();
            _structs = new StructDecl[numStructs];
            long shtPtr = stream.Position;
            for (int i = 0; i < numStructs; i++)
            {
                StructDecl structDecl = new StructDecl();
                _structs[i] = structDecl;
                if (swap)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    short typeNr = reader.ReadInt16();
                    TypeDecl type = _types[typeNr];
                    structDecl.Type = type;
                    type.Struct = structDecl;

                    int numElements = reader.ReadInt16();
                    structDecl.Elements = new ElementDecl[numElements];
                    for (int j = 0; j < numElements; j++)
                    {
                        typeNr = reader.ReadInt16();
                        short nameNr = reader.ReadInt16();
                        structDecl.Elements[j] = new ElementDecl(_types[typeNr], _names[nameNr]);
                    }
                }
            }

            // Build reverse lookup
            _structReverse = new Dictionary<string, StructDecl>(_structs.Length);
            foreach (var s in _structs)
            {
                _structReverse.Add(s.Type.Name, s);
                if (_ptrLen == 0 && s.Type.Name.Equals("ListBase"))
                {
                    _ptrLen = s.Type.Length / 2;
                }
            }
        }

        public bool[] CompareDna(Dna memoryDna)
        {
            Debug.Assert(_names.Length != 0); // SDNA empty!
            bool[] _structChanged = new bool[_structs.Length];

            for (int i = 0; i < _structs.Length; i++)
            {
                StructDecl oldStruct = _structs[i];
                StructDecl curStruct = memoryDna.GetStruct(oldStruct.Type.Name);

                _structChanged[i] = !_structs[i].Equals(curStruct);
            }

            // Recurse in
            for (int i = 0; i < _structs.Length; i++)
            {
                if (_structChanged[i])
                {
                    CompareStruct(_structs[i], _structChanged);
                }
            }

            return _structChanged;
        }

        // Structs containing non-equal structs are also non-equal
        private void CompareStruct(StructDecl iter, bool[] _structChanged)
        {
            for (int i = 0; i < _structs.Length; i++)
            {
                StructDecl curStruct = _structs[i];
                if (curStruct != iter && !_structChanged[i])
                {
                    foreach (ElementDecl element in curStruct.Elements)
                    {
                        if (curStruct.Type == iter.Type && element.Name.IsPointer)
                        {
                            _structChanged[i] = true;
                            CompareStruct(curStruct, _structChanged);
                        }
                    }
                }
            }
        }

        public int NumNames
        {
            get
            {
                if (_names == null)
                {
                    return 0;
                }
                return _names.Length;
            }
        }

        public int NumStructs
        {
            get
            {
                if (_structs == null)
                {
                    return 0;
                }
                return _structs.Length;
            }
        }

        public int PointerSize
        {
            get { return _ptrLen; }
        }
    }
}
