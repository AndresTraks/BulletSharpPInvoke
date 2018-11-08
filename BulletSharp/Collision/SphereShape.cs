using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class SphereShape : ConvexInternalShape
	{
		public SphereShape(float radius)
		{
			IntPtr native = btSphereShape_new(radius);
			InitializeCollisionShape(native);
		}

		public void SetUnscaledRadius(float radius)
		{
			btSphereShape_setUnscaledRadius(Native, radius);
		}

		public float Radius => btSphereShape_getRadius(Native);
	}
}
