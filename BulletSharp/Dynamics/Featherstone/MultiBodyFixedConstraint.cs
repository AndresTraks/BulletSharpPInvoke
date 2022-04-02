using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyFixedConstraint : MultiBodyConstraint
	{
		public MultiBodyFixedConstraint(MultiBody body, int link, RigidBody bodyB,
			Vector3 pivotInA, Vector3 pivotInB, Matrix4x4 frameInA, Matrix4x4 frameInB)
		{
			IntPtr native = btMultiBodyFixedConstraint_new(body.Native, link, bodyB.Native,
				ref pivotInA, ref pivotInB, ref frameInA, ref frameInB);
			InitializeUserOwned(native);
			InitializeMembers(body, null);
		}

		public MultiBodyFixedConstraint(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB, Matrix4x4 frameInA, Matrix4x4 frameInB)
		{
			IntPtr native = btMultiBodyFixedConstraint_new2(bodyA.Native, linkA, bodyB.Native,
				linkB, ref pivotInA, ref pivotInB, ref frameInA, ref frameInB);
			InitializeUserOwned(native);
			InitializeMembers(bodyA, bodyB);
		}

		public Matrix4x4 FrameInA
		{
			get
			{
				Matrix4x4 value;
				btMultiBodyFixedConstraint_getFrameInA(Native, out value);
				return value;
			}
			set => btMultiBodyFixedConstraint_setFrameInA(Native, ref value);
		}

		public Matrix4x4 FrameInB
		{
			get
			{
				Matrix4x4 value;
				btMultiBodyFixedConstraint_getFrameInB(Native, out value);
				return value;
			}
			set => btMultiBodyFixedConstraint_setFrameInB(Native, ref value);
		}

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				btMultiBodyFixedConstraint_getPivotInA(Native, out value);
				return value;
			}
			set => btMultiBodyFixedConstraint_setPivotInA(Native, ref value);
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btMultiBodyFixedConstraint_getPivotInB(Native, out value);
				return value;
			}
			set => btMultiBodyFixedConstraint_setPivotInB(Native, ref value);
		}
	}
}
