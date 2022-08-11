using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace BulletSharp
{
	public class BulletWorldImporter : WorldImporter
	{
		public BulletWorldImporter(DynamicsWorld world)
			: base(world)
		{
		}

		public BulletWorldImporter()
			: base(null)
		{
		}

		public bool ConvertAllObjects(BulletFile file)
		{
            _shapeMap.Clear();
            _bodyMap.Clear();

            foreach (byte[] bvhData in file.Bvhs)
            {
                OptimizedBvh bvh = CreateOptimizedBvh();

                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    // QuantizedBvhData is parsed in C++, so we need to set pointers to actual values
                    GCHandle? contiguousNodes = PinDataAtPointer(bvhData, QuantizedBvhFloatData.Offset("ContiguousNodesPtr"), file);
                    GCHandle? quantizedContiguousNodes = PinDataAtPointer(bvhData, QuantizedBvhFloatData.Offset("QuantizedContiguousNodesPtr"), file);
                    GCHandle? subTreeInfo = PinDataAtPointer(bvhData, QuantizedBvhFloatData.Offset("SubTreeInfoPtr"), file);

                    GCHandle bvhDataHandle = GCHandle.Alloc(bvhData, GCHandleType.Pinned);
                    IntPtr bvhDataPinnedPtr = bvhDataHandle.AddrOfPinnedObject();
                    bvh.DeSerializeFloat(bvhDataPinnedPtr);
                    bvhDataHandle.Free();

                    contiguousNodes?.Free();
                    quantizedContiguousNodes?.Free();
                    subTreeInfo?.Free();
                }

                foreach (KeyValuePair<long, byte[]> lib in file.LibPointers)
                {
                    if (lib.Value == bvhData)
                    {
                        _bvhMap.Add(lib.Key, bvh);
                        break;
                    }
                }
            }

            foreach (byte[] shapeData in file.CollisionShapes)
            {
                CollisionShape shape = ConvertCollisionShape(shapeData, file.LibPointers);
                if (shape != null)
                {
                    foreach (KeyValuePair<long, byte[]> lib in file.LibPointers)
                    {
                        if (lib.Value == shapeData)
                        {
                            _shapeMap.Add(lib.Key, shape);
                            break;
                        }
                    }

                    long namePtr = BulletReader.ToPtr(shapeData, CollisionShapeData.Offset("Name"));
                    if (namePtr != 0)
                    {
                        byte[] nameData = file.LibPointers[namePtr];
                        int length = Array.IndexOf(nameData, (byte)0);
                        string name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
                        _objectNameMap.Add(shape, name);
                        _nameShapeMap.Add(name, shape);
                    }
                }
            }

            foreach (byte[] solverInfoData in file.DynamicsWorldInfo)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    //throw new NotImplementedException();
                }
                else
                {
                    //throw new NotImplementedException();
                }
            }

            foreach (byte[] bodyData in file.RigidBodies)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    ConvertRigidBodyDouble(bodyData, file.LibPointers);
                }
                else
                {
                    ConvertRigidBodyFloat(bodyData, file.LibPointers);
                }
            }

            foreach (byte[] colObjData in file.CollisionObjects)
            {
                using (var colObjStream = new MemoryStream(colObjData, false))
                {
                    using (var colObjReader = new BulletReader(colObjStream))
                    {
                        if ((file.Flags & FileFlags.DoublePrecision) != 0)
                        {
                            long shapePtr = colObjReader.ReadPtr(CollisionObjectDoubleData.Offset("CollisionShape"));
                            CollisionShape shape = _shapeMap[shapePtr];
                            Matrix4x4 startTransform = colObjReader.ReadMatrixDouble(CollisionObjectDoubleData.Offset("WorldTransform"));
                            long namePtr = colObjReader.ReadPtr(CollisionObjectDoubleData.Offset("Name"));
                            string name = null;
                            if (namePtr != 0)
                            {
                                byte[] nameData = file.FindLibPointer(namePtr);
                                int length = Array.IndexOf(nameData, (byte)0);
                                name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
                            }
                            CollisionObject colObj = CreateCollisionObject(ref startTransform, shape, name);
                            _bodyMap.Add(colObjData, colObj);
                        }
                        else
                        {
                            long shapePtr = colObjReader.ReadPtr(CollisionObjectFloatData.Offset("CollisionShape"));
                            CollisionShape shape = _shapeMap[shapePtr];
                            Matrix4x4 startTransform = colObjReader.ReadMatrix(CollisionObjectFloatData.Offset("WorldTransform"));
                            long namePtr = colObjReader.ReadPtr(CollisionObjectFloatData.Offset("Name"));
                            string name = null;
                            if (namePtr != 0)
                            {
                                byte[] nameData = file.FindLibPointer(namePtr);
                                int length = Array.IndexOf(nameData, (byte)0);
                                name = System.Text.Encoding.ASCII.GetString(nameData, 0, length);
                            }
                            CollisionObject colObj = CreateCollisionObject(ref startTransform, shape, name);
                            _bodyMap.Add(colObjData, colObj);
                        }
                    }
                }
            }

            foreach (byte[] constraintData in file.Constraints)
            {
                RigidBody a = null, b = null;

                long collisionObjectAPtr = BulletReader.ToPtr(constraintData, TypedConstraintFloatData.Offset("RigidBodyA"));
                if (collisionObjectAPtr != 0)
                {
                    if (!file.LibPointers.ContainsKey(collisionObjectAPtr))
                    {
                        a = TypedConstraint.GetFixedBody();
                    }
                    else
                    {
                        byte[] coData = file.LibPointers[collisionObjectAPtr];
                        a = RigidBody.Upcast(_bodyMap[coData]);
                        if (a == null)
                        {
                            a = TypedConstraint.GetFixedBody();
                        }
                    }
                }

                long collisionObjectBPtr = BulletReader.ToPtr(constraintData, TypedConstraintFloatData.Offset("RigidBodyB"));
                if (collisionObjectBPtr != 0)
                {
                    if (!file.LibPointers.ContainsKey(collisionObjectBPtr))
                    {
                        b = TypedConstraint.GetFixedBody();
                    }
                    else
                    {
                        byte[] coData = file.LibPointers[collisionObjectBPtr];
                        b = RigidBody.Upcast(_bodyMap[coData]);
                        if (b == null)
                        {
                            b = TypedConstraint.GetFixedBody();
                        }
                    }
                }

                if (a == null && b == null)
                {
                    continue;
                }

                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ConvertConstraintFloat(a, b, constraintData, file.Version, file.LibPointers);
                }
            }

            return true;
		}

        // Replaces an identifier in serialized data with an actual pointer to something.
        // The handle should be used to free the pointer once it is no longer used.
        private static GCHandle? PinDataAtPointer(byte[] data, int pointerPosition, BulletFile file)
        {
            long pointer = BulletReader.ToPtr(data, pointerPosition);
            if (pointer != 0)
            {
                byte[] referencedData = file.LibPointers[pointer];
                GCHandle dataHandle = GCHandle.Alloc(referencedData, GCHandleType.Pinned);
                WritePointer(data, pointerPosition, dataHandle.AddrOfPinnedObject());
                return dataHandle;
            }
            return null;
        }

        private static void WritePointer(byte[] destinationArray, int destinationIndex, IntPtr pointer)
        {
            byte[] sourceArray = IntPtr.Size == 8
                ? BitConverter.GetBytes(pointer.ToInt64())
                : BitConverter.GetBytes(pointer.ToInt32());
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, IntPtr.Size);
        }

        public bool LoadFile(string fileName, string preSwapFilenameOut)
		{
            var bulletFile = new BulletFile(fileName);
            bool result = LoadFileFromMemory(bulletFile);

            //now you could save the file in 'native' format using
            //bulletFile.WriteFile("native.bullet");
            if (result)
            {
                    if (preSwapFilenameOut != null)
                    {
                        bulletFile.PreSwap();
                        //bulletFile.WriteFile(preSwapFilenameOut);
                    }
                
            }

            return result;
		}

        public bool LoadFile(string fileName)
        {
            return LoadFile(fileName, null);
        }
        
		public bool LoadFileFromMemory(byte[] memoryBuffer, int len)
		{
            var bulletFile = new BulletFile(memoryBuffer, len);
            return LoadFileFromMemory(bulletFile);
		}
        
        public bool LoadFileFromMemory(BulletFile bulletFile)
		{
            if (!bulletFile.OK)
            {
                return false;
            }

            bulletFile.Parse(_verboseMode);

            if ((_verboseMode & FileVerboseMode.DumpChunks) != 0)
            {
                //bulletFile.DumpChunks(bulletFile->FileDna);
            }

            return ConvertAllObjects(bulletFile);
		}
	}
}
