using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class FixedConstraint : Generic6DofSpring2Constraint
	{
		public FixedConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix frameInA,
			Matrix frameInB)
			: base(btFixedConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btFixedConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
	}
}
