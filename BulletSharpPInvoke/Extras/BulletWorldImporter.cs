using System;
using System.Collections.Generic;
using System.IO;
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

            foreach (byte[] bvhData in file._bvhs)
            {
                OptimizedBvh bvh = CreateOptimizedBvh();

                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    GCHandle bvhDataHandle = GCHandle.Alloc(bvhData, GCHandleType.Pinned);
                    bvh.DeSerializeFloat(bvhDataHandle.AddrOfPinnedObject());
                    bvhDataHandle.Free();
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

            foreach (byte[] shapeData in file._collisionShapes)
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
                }
                /*
                if (shape && shapeData->m_name)
                {
                    char* newname = duplicateName(shapeData->m_name);
                    m_objectNameMap.insert(shape, newname);
                    m_nameShapeMap.insert(newname, shape);
                }*/
                //throw new NotImplementedException();
            }

            foreach (byte[] bodyData in file._rigidBodies)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    ConvertRigidBodyFloat(bodyData);
                }
            }

            foreach (byte[] colObjData in file._collisionObjects)
            {
                if ((file.Flags & FileFlags.DoublePrecision) != 0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    using (MemoryStream colObjStream = new MemoryStream(colObjData, false))
                    {
                        using (BulletReader colObjReader = new BulletReader(colObjStream))
                        {
                            long shapePtr = colObjReader.ReadPtr(CollisionObjectFloatData.Offset("CollisionShape"));
                            CollisionShape shape = _shapeMap[shapePtr];
                            Math.Matrix startTransform = colObjReader.ReadMatrix(CollisionObjectFloatData.Offset("WorldTransform"));
                            long namePtr = colObjReader.ReadPtr(CollisionObjectFloatData.Offset("Name"));
                            if (namePtr != 0)
                            {
                                throw new NotImplementedException();
                            }
                            CollisionObject colObj = CreateCollisionObject(ref startTransform, shape, "n");
                            _bodyMap.Add(colObjData, colObj);
                        }
                    }
                }
            }

            foreach (byte[] constraintData in file._constraints)
            {
                MemoryStream stream = new MemoryStream(constraintData, false);
                using (BulletReader reader = new BulletReader(stream))
                {
                    long collisionObjectAPtr = reader.ReadPtr(TypedConstraintFloatData.Offset("RigidBodyA"));
                    long collisionObjectBPtr = reader.ReadPtr(TypedConstraintFloatData.Offset("RigidBodyB"));

                    RigidBody a = null, b = null;

                    if (collisionObjectAPtr != 0)
                    {
                        if (!file.LibPointers.ContainsKey(collisionObjectAPtr))
                        {
                            a = TypedConstraint.FixedBody;
                        }
                        else
                        {
                            byte[] coData = file.LibPointers[collisionObjectAPtr];
                            a = RigidBody.Upcast(_bodyMap[coData]);
                            if (a == null)
                            {
                                a = TypedConstraint.FixedBody;
                            }
                        }
                    }

                    if (collisionObjectBPtr != 0)
                    {
                        if (!file.LibPointers.ContainsKey(collisionObjectBPtr))
                        {
                            b = TypedConstraint.FixedBody;
                        }
                        else
                        {
                            byte[] coData = file.LibPointers[collisionObjectBPtr];
                            b = RigidBody.Upcast(_bodyMap[coData]);
                            if (b == null)
                            {
                                b = TypedConstraint.FixedBody;
                            }
                        }
                    }

                    if (a == null && b == null)
                    {
                        stream.Dispose();
                        continue;
                    }

                    if ((file.Flags & FileFlags.DoublePrecision) != 0)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        ConvertConstraintFloat(a, b, constraintData, file.Version);
                    }
                }
                stream.Dispose();
            }

            return true;
		}

        public bool LoadFile(string fileName, string preSwapFilenameOut)
		{
			BulletFile bulletFile = new BulletFile(fileName);
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
            BulletFile bulletFile = new BulletFile(memoryBuffer, len);
            return LoadFileFromMemory(bulletFile);
		}
        
        public bool LoadFileFromMemory(BulletFile bulletFile)
		{
            if ((bulletFile.Flags & FileFlags.OK) != FileFlags.OK)
            {
                return false;
            }

            bulletFile.Parse(_verboseMode);

            if ((_verboseMode & FileVerboseMode.DumpChunks) == FileVerboseMode.DumpChunks)
            {
                //bulletFile.DumpChunks(bulletFile->FileDna);
            }

            return ConvertAllObjects(bulletFile);
		}
	}
}
