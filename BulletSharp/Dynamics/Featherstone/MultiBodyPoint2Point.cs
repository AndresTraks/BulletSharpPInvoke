using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyPoint2Point : MultiBodyConstraint
	{
		public MultiBodyPoint2Point(MultiBody body, int link, RigidBody bodyB, Vector3 pivotInA,
			Vector3 pivotInB)
		{
			IntPtr native = btMultiBodyPoint2Point_new(body.Native, link, bodyB != null ? bodyB.Native : IntPtr.Zero,
				ref pivotInA, ref pivotInB);
			InitializeUserOwned(native);
			InitializeMembers(body, null);
		}

		public MultiBodyPoint2Point(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB)
		{
			IntPtr native = btMultiBodyPoint2Point_new2(bodyA.Native, linkA, bodyB.Native,
				linkB, ref pivotInA, ref pivotInB);
			InitializeUserOwned(native);
			InitializeMembers(bodyA, bodyB);
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btMultiBodyPoint2Point_getPivotInB(Native, out value);
				return value;
			}
			set => btMultiBodyPoint2Point_setPivotInB(Native, ref value);
		}
	}
}
