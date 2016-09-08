using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            ReadTag(reader, "SDNA");

            ReadTag(reader, "NAME");
            _names = reader.ReadStringList()
                .Select(s => new NameInfo(s))
                .ToArray();

            ReadTag(reader, "TYPE");
            _types = reader.ReadStringList()
                .Select(s => new TypeDecl(s))
                .ToArray();

            Stream stream = reader.BaseStream;

            // Type lengths
            ReadTag(reader, "TLEN");
            for (int i = 0; i < _types.Length; i++)
            {
                _types[i].Length = reader.ReadInt16();
            }
            stream.Position = (stream.Position + 3) & ~3;

            // Structs
            ReadTag(reader, "STRC");
            int numStructs = reader.ReadInt32();
            _structs = new StructDecl[numStructs];
            for (int i = 0; i < numStructs; i++)
            {
                StructDecl structDecl = new StructDecl();
                if (swap)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    short typeIndex = reader.ReadInt16();
                    TypeDecl type = _types[typeIndex];
                    structDecl.Type = type;
                    type.Struct = structDecl;

                    int numElements = reader.ReadInt16();
                    structDecl.Elements = new ElementDecl[numElements];
                    for (int j = 0; j < numElements; j++)
                    {
                        typeIndex = reader.ReadInt16();
                        short nameIndex = reader.ReadInt16();
                        structDecl.Elements[j] = new ElementDecl(_types[typeIndex], _names[nameIndex]);
                    }
                }
                _structs[i] = structDecl;
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

        private static void ReadTag(BulletReader reader, string tag)
        {
            byte[] codeData = reader.ReadBytes(tag.Length);
            string code = Encoding.ASCII.GetString(codeData);
            if (code != tag)
            {
                throw new InvalidDataException($"Expected tag: {tag}");
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

        public bool IsBroken(int fileVersion)
        {
            return fileVersion == 276 && _names.Any(n => n.Name == "int");
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
