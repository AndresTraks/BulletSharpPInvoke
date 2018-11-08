using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SphereShape : ConvexInternalShape
	{
		public SphereShape(double radius)
		{
			IntPtr native = btSphereShape_new(radius);
			InitializeCollisionShape(native);
		}

		public void SetUnscaledRadius(double radius)
		{
			btSphereShape_setUnscaledRadius(Native, radius);
		}

		public double Radius => btSphereShape_getRadius(Native);
	}
}
