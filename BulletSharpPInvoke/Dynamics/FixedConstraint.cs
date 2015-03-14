using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class FixedConstraint : TypedConstraint
	{
        public FixedConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, ref Matrix frameInA, ref Matrix frameInB)
            : base(btFixedConstraint_new(rigidBodyA._native, rigidBodyB._native, ref frameInA, ref frameInB))
        {
            _rigidBodyA = rigidBodyA;
            _rigidBodyB = rigidBodyB;
        }

		public FixedConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix frameInA, Matrix frameInB)
			: this(rigidBodyA, rigidBodyB, ref frameInA, ref frameInB)
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btFixedConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
	}
}
