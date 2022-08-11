using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class DeformableGravityForce : DeformableLagrangianForce
	{
		public DeformableGravityForce(Vector3 gravity)
		{
			IntPtr native = btDeformableGravityForce_new(ref gravity);
			InitializeUserOwned(native);
		}
	}
}
