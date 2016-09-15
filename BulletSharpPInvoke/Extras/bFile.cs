using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace BulletSharp
{
    [Flags]
    public enum FileFlags
    {
        None = 0,
        EndianSwap = 1,
        File64 = 2,
        BitsVaries = 4,
        DoublePrecision = 8,
        BrokenDna = 0x10
    }

    [Flags]
    public enum FileVerboseMode
    {
        None = 0,
        ExportXml = 1,
        DumpDnaTypeDefinitions = 2,
        DumpChunks = 4,
        DumpFileInfo = 8
    }

    class PointerFixup
    {
        public byte[] StructAlloc { get; set; }
        public long[] Offsets { get; set; }

        public PointerFixup(byte[] structAlloc, long[] offsets)
        {
            StructAlloc = structAlloc;
            Offsets = offsets;
        }
    }

    public abstract class bFile
    {
        protected const int SizeOfBlenderHeader = 12;
        const int MaxArrayLength = 512;
        const int MaxStringLength = 1024;

        protected List<ChunkInd> _chunks = new List<ChunkInd>();
        protected long _dataStart;
        protected byte[] _fileBuffer;
        protected Dna _fileDna, _memoryDna;
        private bool[] _structChanged;
        protected FileFlags _flags;
        protected Dictionary<long, byte[]> _libPointers = new Dictionary<long, byte[]>();
        protected int _version;

        public bFile(string filename)
        {
            try
            {
                _fileBuffer = File.ReadAllBytes(filename);
                ParseHeader();
            }
            catch
            {

            }
        }

        public bFile(byte[] memoryBuffer, int len)
        {
            _fileBuffer = memoryBuffer;

            ParseHeader();
        }

        protected abstract string HeaderTag { get; }
        public bool OK { get; protected set; }

        public abstract void AddDataBlock(byte[] dataBlock);

        /*
		public void DumpChunks(bDNA dna)
		{
			bFile_dumpChunks(_native, dna._native);
		}
        */
        public byte[] FindLibPointer(long ptr)
        {
            byte[] data;
            if (LibPointers.TryGetValue(ptr, out data))
            {
                return data;
            }
            return null;
        }

        private void GetElement(BinaryReader reader, int ArrayLen, Dna.TypeDecl type, double[] data)
        {
            if (type.Name.Equals("float"))
            {
                for (int i = 0; i < ArrayLen; i++)
                {
                    data[i] = reader.ReadSingle();
                }
            }
            else
            {
                for (int i = 0; i < ArrayLen; i++)
                {
                    data[i] = reader.ReadDouble();
                }
            }
        }

        protected Dna.ElementDecl GetFileElement(Dna.StructDecl fileStruct, Dna.ElementDecl lookupElement, out long elementOffset)
        {
            elementOffset = 0;
            foreach (Dna.ElementDecl element in fileStruct.Elements)
            {
                if (element.NameInfo.Equals(lookupElement.NameInfo))
                {
                    if (element.Type.Name.Equals(lookupElement.Type.Name))
                    {
                        return element;
                    }
                    break;
                }
                elementOffset += _fileDna.GetElementSize(element);
            }
            return null;
        }

        protected void FindFileElement(Dna.StructDecl fileStruct, Dna.ElementDecl memoryElement, BinaryWriter dataOutput, long data, bool fixupPointers)
        {
            bool brokenDna = (_flags & FileFlags.BrokenDna) != 0;

            int elementOffset;
            Dna.ElementDecl element = fileStruct.FindElement(_fileDna, brokenDna, memoryElement.NameInfo, out elementOffset);
            if (element != null)
            {
                int elementLength = _fileDna.GetElementSize(element);
                WriteFileElement(element, elementLength, memoryElement, dataOutput, data + elementOffset, fixupPointers);
            }
        }

        private void WriteFileElement(Dna.ElementDecl element, int eleLen, Dna.ElementDecl lookupElement, BinaryWriter dataWriter, long data, bool fixupPointers)
        {
            MemoryStream dataStream = new MemoryStream(_fileBuffer, false);
            BinaryReader dataReader = new BinaryReader(dataStream);

            int arrayLen = element.NameInfo.ArrayLength;

            dataStream.Position = data;

            if (element.NameInfo.Name[0] == '*')
            {
                SafeSwapPtr(dataWriter, dataReader);

                if (fixupPointers)
                {
                    if (arrayLen > 1)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        if (element.NameInfo.Name[1] == '*')
                        {
                            throw new NotImplementedException();
                        }
                        else
                        {
                            //_chunkPointerFixupArray.Add(strcData.BaseStream.Position);
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("skipped {0} {1} : {2:X}", element.Type.Name, element.Name.Name, strcData.BaseStream.Position);
                }
            }
            else if (element.Type.Name.Equals(lookupElement.Type.Name))
            {
                byte[] mem = new byte[eleLen];
                dataReader.Read(mem, 0, eleLen);
                dataWriter.Write(mem);
            }
            else
            {
                throw new NotImplementedException();
                //GetElement(arrayLen, lookupType, type, data, strcData);
            }

            dataReader.Dispose();
            dataStream.Dispose();
        }

        // buffer offset util
        protected int GetNextBlock(out ChunkInd dataChunk, BinaryReader reader)
        {
            bool swap = (_flags & FileFlags.EndianSwap) != 0;
            bool varies = (_flags & FileFlags.BitsVaries) != 0;

            if (swap)
            {
                throw new NotImplementedException();
            }

            if (IntPtr.Size == 8)
            {
                if (varies)
                {
                    Chunk4 c = new Chunk4(reader);
                    dataChunk = new ChunkInd(c);
                }
                else
                {
                    Chunk8 c = new Chunk8(reader);
                    dataChunk = new ChunkInd(c);
                }
            }
            else
            {
                if (varies)
                {
                    Chunk8 c = new Chunk8(reader);
                    if (c.UniqueInt1 == c.UniqueInt2)
                    {
                        c.UniqueInt2 = 0;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                    dataChunk = new ChunkInd(c);
                }
                else
                {
                    Chunk4 c = new Chunk4(reader);
                    dataChunk = new ChunkInd(c);
                }
            }

            if (dataChunk.Length < 0)
                return -1;

            return dataChunk.Length + ChunkUtils.GetChunkSize(_flags);
        }

        public abstract void Parse(FileVerboseMode verboseMode);
        public abstract void ParseData();

        protected void ParseHeader()
        {
            string header = Encoding.UTF8.GetString(_fileBuffer, 0, SizeOfBlenderHeader);
            if (header.Substring(0, 6) != HeaderTag)
            {
                return;
            }

            if (header[6] == 'd')
            {
                _flags |= FileFlags.DoublePrecision;
            }

            int.TryParse(header.Substring(9), out _version);

            // swap ptr sizes...
            if (header[7] == '-')
            {
                _flags |= FileFlags.File64;
                if (IntPtr.Size != 8)
                    _flags |= FileFlags.BitsVaries;
            }
            else if (IntPtr.Size == 8)
            {
                _flags |= FileFlags.BitsVaries;
            }

            // swap endian...
            if (header[8] == 'V')
            {
                if (BitConverter.IsLittleEndian)
                    _flags |= FileFlags.EndianSwap;
            }
            else
            {
                if (!BitConverter.IsLittleEndian)
                    _flags |= FileFlags.EndianSwap;
            }

            OK = true;
        }

        protected void ParseInternal(FileVerboseMode verboseMode)
        {
            if (!OK)
            {
                return;
            }

            using (var fileStream = new MemoryStream(_fileBuffer, false))
            {
                using (var reader = new BulletReader(fileStream))
                {
                    long dnaStart = FindDnaChunk(reader);
                    OK = dnaStart != -1;

                    if (OK)
                    {
                        fileStream.Position = dnaStart;
                        LoadDna(reader);
                    }
                    else
                    {
                        //Console.WriteLine("Failed to find DNA1+SDNA pair");
                    }
                }
            }

            if (OK)
            {
                ParseData();
                //ResolvePointers(verboseMode);
                //UpdateOldPointers();
            }
        }

        private long FindDnaChunk(BinaryReader reader)
        {
            var stream = reader.BaseStream;

            int i = 0;
            while (i < stream.Length)
            {
                stream.Position = i;

                // looking for the data's starting position
                // and the start of SDNA decls
                byte[] codeData = reader.ReadBytes(4);
                string code = Encoding.ASCII.GetString(codeData);

                if (_dataStart == 0 && code.Equals("REND"))
                {
                    _dataStart = stream.Position;
                }

                if (code == "DNA1")
                {
                    // read the DNA1 block and extract SDNA
                    stream.Position = i;
                    ChunkInd chunk;
                    if (GetNextBlock(out chunk, reader) > 0)
                    {
                        string sdnaname = Encoding.ASCII.GetString(reader.ReadBytes(8));
                        if (sdnaname == "SDNANAME")
                        {
                            return i + ChunkUtils.GetChunkSize(_flags);
                        }
                    }
                }
                else if (code == "SDNA")
                {
                    // Some Bullet files are missing the DNA1 block
                    // In Blender it's DNA1 + ChunkUtils.GetOffset() + SDNA + NAME
                    // In Bullet tests its SDNA + NAME
                    return i;
                }

                i++;
            }
            return 0;
        }

        private void LoadDna(BulletReader reader)
        {
            bool swap = (_flags & FileFlags.EndianSwap) != 0;
            _fileDna = Dna.Load(reader, swap);

            if (_fileDna.IsBroken(_version))
            {
                //Console.WriteLine("warning: fixing some broken DNA version");
                _flags |= FileFlags.BrokenDna;
            }

            //if ((verboseMode & FileVerboseMode.DumpDnaTypeDefinitions) != 0)
            //    _fileDna.DumpTypeDefinitions();

            byte[] memoryDnaData = IntPtr.Size == 8
                ? Serializer.GetBulletDna64()
                : Serializer.GetBulletDna();
            using (var dnaStream = new MemoryStream(memoryDnaData, false))
            {
                using (var dnaReader = new BulletReader(dnaStream))
                {
                    _memoryDna = Dna.Load(dnaReader, !BitConverter.IsLittleEndian);
                }
            }

            _structChanged = _fileDna.Compare(_memoryDna);
        }

        protected void ParseStruct(BinaryWriter writer, BinaryReader reader, Dna.StructDecl fileStruct, Dna.StructDecl memoryStruct, bool fixupPointers)
        {
            if (fileStruct == null) return;
            if (memoryStruct == null) return;

            long readPtr = reader.BaseStream.Position;
            long writePtr = writer.BaseStream.Position;

            foreach (Dna.ElementDecl element in memoryStruct.Elements)
            {
                int memorySize = _memoryDna.GetElementSize(element);
                if (element.Type.Struct != null && element.NameInfo.Name[0] != '*')
                {
                    long elementOffset;
                    Dna.ElementDecl elementOld = GetFileElement(fileStruct, element, out elementOffset);
                    if (elementOld != null)
                    {
                        Dna.StructDecl structOld = _fileDna.GetStruct(element.Type.Name);
                        reader.BaseStream.Position = elementOffset;
                        int arrayLen = elementOld.NameInfo.ArrayLength;
                        if (arrayLen == 1)
                        {
                            writer.BaseStream.Position = writePtr;
                            ParseStruct(writer, reader, structOld, element.Type.Struct, fixupPointers);
                            writePtr += memorySize;
                        }
                        else
                        {
                            int fileSize = _fileDna.GetElementSize(elementOld) / arrayLen;
                            memorySize /= arrayLen;
                            for (int i = 0; i < arrayLen; i++)
                            {
                                writer.BaseStream.Position = writePtr;
                                ParseStruct(writer, reader, structOld, element.Type.Struct, fixupPointers);
                                reader.BaseStream.Position += fileSize;
                                writePtr += memorySize;
                            }
                        }
                    }
                }
                else
                {
                    writer.BaseStream.Position = writePtr;
                    FindFileElement(fileStruct, element, writer, readPtr, fixupPointers);
                    writePtr += memorySize;
                }
            }
        }

        public void PreSwap()
        {
            throw new NotImplementedException();
        }

        protected byte[] ReadStruct(BinaryReader reader, ChunkInd dataChunk)
        {
            //bool ignoreEndianFlag = false;

            if ((_flags & FileFlags.EndianSwap) != 0)
            {
                //swap(head, dataChunk, ignoreEndianFlag);
            }

            if (StructChanged(dataChunk.StructIndex))
            {
                // Ouch! need to rebuild the struct
                Dna.StructDecl oldStruct = _fileDna.GetStruct(dataChunk.StructIndex);

                if ((_flags & FileFlags.BrokenDna) != 0)
                {
                    if (oldStruct.Type.Name.Equals("btQuantizedBvhNodeData") && oldStruct.Type.Length == 28)
                    {
                        throw new NotImplementedException();
                    }

                    if (oldStruct.Type.Name.Equals("btShortIntIndexData"))
                    {
                        throw new NotImplementedException();
                    }
                }

                // Don't try to convert Link block data, just memcpy it. Other data can be converted.
                if (oldStruct.Type.Name.Equals("Link"))
                {
                    //Console.WriteLine("Link found");
                }
                else
                {
                    Dna.StructDecl curStruct = _memoryDna.GetStruct(oldStruct.Type.Name);
                    if (curStruct != null)
                    {
                        byte[] structAlloc = new byte[dataChunk.NumBlocks * curStruct.Type.Length];
                        AddDataBlock(structAlloc);
                        using (MemoryStream stream = new MemoryStream(structAlloc))
                        {
                            using (BinaryWriter writer = new BinaryWriter(stream))
                            {
                                long readPtr = reader.BaseStream.Position;
                                for (int block = 0; block < dataChunk.NumBlocks; block++)
                                {
                                    reader.BaseStream.Position = readPtr;
                                    ParseStruct(writer, reader, oldStruct, curStruct, true);
                                    readPtr += oldStruct.Type.Length;
                                    //_libPointers.Add(old, cur);
                                }
                            }
                        }
                        return structAlloc;
                    }
                }
            }
            else
            {
#if DEBUG_EQUAL_STRUCTS
#endif
            }

            byte[] dataAlloc = new byte[dataChunk.Length];
            reader.Read(dataAlloc, 0, dataAlloc.Length);
            return dataAlloc;
        }

        public void ResolvePointers(FileVerboseMode verboseMode)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            if (true) // && ((_flags & FileFlags.BitsVaries | FileFlags.VersionVaries) != 0))
            {
                //ResolvePointersMismatch();
            }

            if ((verboseMode & FileVerboseMode.ExportXml) != 0)
            {
                Console.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                Console.WriteLine("<bullet_physics version=\"{0}\" itemcount=\"{1}\">", Version, _chunks.Count);
            }

            foreach (ChunkInd dataChunk in _chunks)
            {
                if (_fileDna == null || !StructChanged(dataChunk.StructIndex))
                {
                    Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.StructIndex);

                    if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                    {
                        Console.WriteLine(" <{0} pointer=\"{1}\">", oldStruct.Type.Name, dataChunk.OldPtr);
                    }

                    ResolvePointersChunk(dataChunk, verboseMode);

                    if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                    {
                        Console.WriteLine(" </{0}>", oldStruct.Type.Name);
                    }
                }
                else
                {
                    //Console.WriteLine("skipping struct");
                }
            }

            if ((verboseMode & FileVerboseMode.ExportXml) != 0)
            {
                Console.WriteLine("</bullet_physics>");
            }
        }

        protected void ResolvePointersChunk(ChunkInd dataChunk, FileVerboseMode verboseMode)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            Dna.StructDecl oldStruct = fileDna.GetStruct(dataChunk.StructIndex);
            int oldLen = oldStruct.Type.Length;

            byte[] cur = FindLibPointer(dataChunk.OldPtr);
            using (MemoryStream stream = new MemoryStream(cur, false))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    for (int block = 0; block < dataChunk.NumBlocks; block++)
                    {
                        long streamPosition = stream.Position;
                        ResolvePointersStructRecursive(reader, fileDna.GetStruct(dataChunk.StructIndex), verboseMode, 1);
                        Debug.Assert(stream.Position == streamPosition + oldLen);
                    }
                }
            }
        }

        protected int ResolvePointersStructRecursive(BinaryReader reader, Dna.StructDecl oldStruct, FileVerboseMode verboseMode, int recursion)
        {
            Dna fileDna = (_fileDna != null) ? _fileDna : _memoryDna;

            int totalSize = 0;

            foreach (Dna.ElementDecl element in oldStruct.Elements)
            {
                int size = fileDna.GetElementSize(element);
                int arrayLen = element.NameInfo.ArrayLength;

                if (element.NameInfo.Name[0] == '*')
                {
                    if (arrayLen > 1)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        long ptr = (IntPtr.Size == 8) ? reader.ReadInt64() : reader.ReadInt32();
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            for (int i = 0; i < recursion; i++)
                            {
                                Console.Write("  ");
                            }
                            Console.WriteLine("<{0} type=\"pointer\"> {1} </{0}>", element.NameInfo.Name.Substring(1), ptr);
                        }
                        byte[] ptrChunk = FindLibPointer(ptr);
                        if (ptrChunk != null)
                        {
                            //throw new NotImplementedException();
                        }
                        else
                        {
                            //Console.WriteLine("Cannot fixup pointer at {0} from {1} to {2}!", ptrptr, *ptrptr, ptr);
                        }
                    }
                }
                else
                {
                    if (element.Type.Struct != null)
                    {
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            for (int i = 0; i < recursion; i++)
                            {
                                Console.Write("  ");
                            }

                            if (arrayLen > 1)
                            {
                                Console.WriteLine("<{0} type=\"{1}\" count={2}>", element.NameInfo.CleanName, element.Type.Name, arrayLen);
                            }
                            else
                            {
                                Console.WriteLine("<{0} type=\"{1}\">", element.NameInfo.CleanName, element.Type.Name);
                            }
                        }

                        for (int i = 0; i < arrayLen; i++)
                        {
                            Dna.StructDecl revType = _fileDna.GetStruct(element.Type.Name);
                            ResolvePointersStructRecursive(reader, revType, verboseMode, recursion + 1);
                            //throw new NotImplementedException();
                        }

                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            for (int i = 0; i < recursion; i++)
                            {
                                Console.Write("  ");
                            }
                            Console.WriteLine("</{0}>", element.NameInfo.CleanName);
                        }
                    }
                    else
                    {
                        // export a simple type
                        if ((verboseMode & FileVerboseMode.ExportXml) != 0)
                        {
                            if (arrayLen > MaxArrayLength)
                            {
                                Console.WriteLine("too long");
                            }
                            else
                            {
                                bool isIntegerType;
                                switch (element.Type.Name)
                                {
                                    case "char":
                                    case "short":
                                    case "int":
                                        isIntegerType = true;
                                        break;
                                    default:
                                        isIntegerType = false;
                                        break;
                                }

                                if (isIntegerType)
                                {
                                    throw new NotImplementedException();
                                    /*
                                    const char* newtype="int";
							        int dbarray[MAX_ARRAY_LENGTH];
							        int* dbPtr = 0;
							        char* tmp = elemPtr;
							        dbPtr = &dbarray[0];
							        if (dbPtr)
							        {
								        char cleanName[MAX_STRLEN];
								        getCleanName(memName,cleanName);

								        int i;
								        getElement(arrayLen, newtype,memType, tmp, (char*)dbPtr);
								        for (i=0;i<recursion;i++)
									        printf("  ");
								        if (arrayLen==1)
									        printf("<%s type=\"%s\">",cleanName,memType);
								        else
									        printf("<%s type=\"%s\" count=%d>",cleanName,memType,arrayLen);
								        for (i=0;i<arrayLen;i++)
									        printf(" %d ",dbPtr[i]);
								        printf("</%s>\n",cleanName);
							        }*/
                                }
                                else
                                {
                                    double[] dbArray = new double[arrayLen];
                                    GetElement(reader, arrayLen, element.Type, dbArray);
                                    for (int i = 0; i < recursion; i++)
                                    {
                                        Console.Write("  ");
                                    }
                                    if (arrayLen == 1)
                                    {
                                        Console.Write("<{0} type=\"{1}\">", element.NameInfo.Name, element.Type.Name);
                                    }
                                    else
                                    {
                                        Console.Write("<{0} type=\"{1}\" count=\"{2}\">", element.NameInfo.CleanName, element.Type.Name, arrayLen);
                                    }
                                    for (int i = 0; i < arrayLen; i++)
                                    {
                                        Console.Write(" {0} ", dbArray[i].ToString(CultureInfo.InvariantCulture));
                                    }
                                    Console.WriteLine("</{0}>", element.NameInfo.CleanName);
                                }
                            }
                        }
                        reader.BaseStream.Position += size;
                    }
                }
                totalSize += size;
            }

            return totalSize;
        }

        protected void SafeSwapPtr(BinaryWriter strcData, BinaryReader data)
        {
            int filePtrSize = _fileDna.PointerSize;
            int memPtrSize = _memoryDna.PointerSize;

            if (filePtrSize == memPtrSize)
            {
                byte[] mem = new byte[memPtrSize];
                data.Read(mem, 0, memPtrSize);
                strcData.Write(mem);
            }
            else if (memPtrSize == 4 && filePtrSize == 8)
            {
                int uniqueId1 = data.ReadInt32();
                int uniqueId2 = data.ReadInt32();
                if (uniqueId1 == uniqueId2)
                {
                    strcData.Write(uniqueId1);
                    data.BaseStream.Position -= 4;
                }
                else
                {
                    data.BaseStream.Position -= 8;
                    long longValue = data.ReadInt64();
                    data.BaseStream.Position -= 4;
                    if ((Flags & FileFlags.EndianSwap) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    longValue = longValue >> 3;
                    int intValue = (int)longValue;
                    strcData.Write(intValue);
                }
            }
            else if (memPtrSize == 8 && filePtrSize == 4)
            {
                int uniqueId1 = data.ReadInt32();
                int uniqueId2 = data.ReadInt32();
                data.BaseStream.Position -= 4;
                if (uniqueId1 == uniqueId2)
                {
                    strcData.Write(uniqueId1);
                    strcData.Write(0);
                }
                else
                {
                    strcData.Write((long)uniqueId1);
                }
            }
            else
            {
                Console.WriteLine("Invalid pointer len {0} {1}", filePtrSize, memPtrSize);
            }
        }

        protected bool StructChanged(int structIndex)
        {
            Debug.Assert(structIndex < _structChanged.Length);
            return _structChanged[structIndex];
        }

        public void UpdateOldPointers()
        {
            for (int i = 0; i < _chunks.Count; i++)
            {
                //_chunks[i].OldPtr
                byte[] data = FindLibPointer(_chunks[i].OldPtr);
                data.ToString();
            }
        }
        /*
		public int Write(char fileName, bool fixupPointers)
		{
			return bFile_write(_native, fileName._native, fixupPointers);
		}

		public int Write(char fileName)
		{
			return bFile_write2(_native, fileName._native);
		}

		public void WriteChunks(FILE fp, bool fixupPointers)
		{
			bFile_writeChunks(_native, fp._native, fixupPointers);
		}

		public void WriteDNA(FILE fp)
		{
			bFile_writeDNA(_native, fp._native);
		}

		public void WriteFile(char fileName)
		{
			bFile_writeFile(_native, fileName._native);
		}

		public bDNA FileDNA
		{
			get { return bFile_getFileDNA(_native); }
		}
        */
        public FileFlags Flags
        {
            get { return _flags; }
        }

        public Dictionary<long, byte[]> LibPointers
        {
            get { return _libPointers; }
        }

        public int Version
        {
            get { return _version; }
        }
    }
}
