using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public abstract class DeformableLagrangianForce : BulletDisposableObject
	{
		protected internal DeformableLagrangianForce()
		{
		}

		protected override void Dispose(bool disposing)
		{
			btDeformableLagrangianForce_delete(Native);
		}
	}
}
