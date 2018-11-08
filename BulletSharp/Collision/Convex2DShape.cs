using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Convex2DShape : ConvexShape
	{
		public Convex2DShape(ConvexShape convexChildShape)
		{
			IntPtr native = btConvex2dShape_new(convexChildShape.Native);
			InitializeCollisionShape(native);

			ChildShape = convexChildShape;
		}

		public ConvexShape ChildShape { get; }
	}
}
