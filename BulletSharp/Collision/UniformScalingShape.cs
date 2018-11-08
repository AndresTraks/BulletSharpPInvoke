using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class UniformScalingShape : ConvexShape
	{
		public UniformScalingShape(ConvexShape convexChildShape, double uniformScalingFactor)
		{
			IntPtr native = btUniformScalingShape_new(convexChildShape.Native, uniformScalingFactor);
			InitializeCollisionShape(native);

			ChildShape = convexChildShape;
		}

		public ConvexShape ChildShape { get; }

		public double UniformScalingFactor
		{
			get { return btUniformScalingShape_getUniformScalingFactor(Native); }
		}
	}
}
