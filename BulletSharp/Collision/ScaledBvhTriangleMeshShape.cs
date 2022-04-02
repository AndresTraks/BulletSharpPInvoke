using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ScaledBvhTriangleMeshShape : ConcaveShape
	{
		public ScaledBvhTriangleMeshShape(BvhTriangleMeshShape childShape, Vector3 localScaling)
		{
			IntPtr native = btScaledBvhTriangleMeshShape_new(childShape.Native, ref localScaling);
			InitializeCollisionShape(native);

			ChildShape = childShape;
		}

		public BvhTriangleMeshShape ChildShape { get; }
	}
}
