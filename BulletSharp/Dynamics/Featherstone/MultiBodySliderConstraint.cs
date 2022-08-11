using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodySliderConstraint : MultiBodyConstraint
	{
		public MultiBodySliderConstraint(MultiBody body, int link, RigidBody bodyB,
			Vector3 pivotInA, Vector3 pivotInB, Matrix4x4 frameInA, Matrix4x4 frameInB, Vector3 jointAxis)
		{
			IntPtr native = btMultiBodySliderConstraint_new(body.Native, link, bodyB.Native,
				ref pivotInA, ref pivotInB, ref frameInA, ref frameInB, ref jointAxis);
			InitializeUserOwned(native);
			InitializeMembers(body, null);
		}

		public MultiBodySliderConstraint(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB, Matrix4x4 frameInA, Matrix4x4 frameInB,
			Vector3 jointAxis)
		{
			IntPtr native = btMultiBodySliderConstraint_new2(bodyA.Native, linkA, bodyB.Native,
				linkB, ref pivotInA, ref pivotInB, ref frameInA, ref frameInB, ref jointAxis);
			InitializeUserOwned(native);
			InitializeMembers(bodyA, bodyB);
		}

		public Matrix4x4 FrameInA
		{
			get
			{
				Matrix4x4 value;
				btMultiBodySliderConstraint_getFrameInA(Native, out value);
				return value;
			}
			set => btMultiBodySliderConstraint_setFrameInA(Native, ref value);
		}

		public Matrix4x4 FrameInB
		{
			get
			{
				Matrix4x4 value;
				btMultiBodySliderConstraint_getFrameInB(Native, out value);
				return value;
			}
			set => btMultiBodySliderConstraint_setFrameInB(Native, ref value);
		}

		public Vector3 JointAxis
		{
			get
			{
				Vector3 value;
				btMultiBodySliderConstraint_getJointAxis(Native, out value);
				return value;
			}
			set => btMultiBodySliderConstraint_setJointAxis(Native, ref value);
		}

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				btMultiBodySliderConstraint_getPivotInA(Native, out value);
				return value;
			}
			set => btMultiBodySliderConstraint_setPivotInA(Native, ref value);
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btMultiBodySliderConstraint_getPivotInB(Native, out value);
				return value;
			}
			set => btMultiBodySliderConstraint_setPivotInB(Native, ref value);
		}
	}
}
