using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class FixedConstraint : Generic6DofSpring2Constraint
	{
		public FixedConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix4x4 frameInA,
			Matrix4x4 frameInB)
		{
			IntPtr native = btFixedConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}
	}
}
