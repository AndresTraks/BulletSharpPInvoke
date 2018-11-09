using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultimaterialTriangleMeshShape : BvhTriangleMeshShape
	{
		public MultimaterialTriangleMeshShape(StridingMeshInterface meshInterface,
			bool useQuantizedAabbCompression, bool buildBvh = true)
		{
			IntPtr native = btMultimaterialTriangleMeshShape_new(meshInterface.Native, useQuantizedAabbCompression,
				buildBvh);
			InitializeCollisionShape(native);
			InitializeMembers(meshInterface);
		}

		public MultimaterialTriangleMeshShape(StridingMeshInterface meshInterface,
			bool useQuantizedAabbCompression, Vector3 bvhAabbMin, Vector3 bvhAabbMax,
			bool buildBvh = true)
		{
			IntPtr native = btMultimaterialTriangleMeshShape_new2(meshInterface.Native, useQuantizedAabbCompression,
				ref bvhAabbMin, ref bvhAabbMax, buildBvh);
			InitializeCollisionShape(native);
			InitializeMembers(meshInterface);
		}
		/*
		public BulletMaterial GetMaterialProperties(int partID, int triIndex)
		{
			return btMultimaterialTriangleMeshShape_getMaterialProperties(Native,
				partID, triIndex);
		}
		*/
	}
}
