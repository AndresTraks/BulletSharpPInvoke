using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class Generic6DofSpringConstraint : Generic6DofConstraint
	{
		internal Generic6DofSpringConstraint(IntPtr native)
			: base(native)
		{
		}

        public Generic6DofSpringConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, ref Matrix frameInA, ref Matrix frameInB, bool useLinearReferenceFrameA)
            : base(btGeneric6DofSpringConstraint_new(rigidBodyA._native, rigidBodyB._native, ref frameInA, ref frameInB, useLinearReferenceFrameA))
        {
            _rigidBodyA = rigidBodyA;
            _rigidBodyB = rigidBodyB;
        }

		public Generic6DofSpringConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix frameInA, Matrix frameInB, bool useLinearReferenceFrameA)
			: this(rigidBodyA, rigidBodyB, ref frameInA, ref frameInB, useLinearReferenceFrameA)
		{
		}

        public Generic6DofSpringConstraint(RigidBody rigidBodyB, ref Matrix frameInB, bool useLinearReferenceFrameB)
            : base(btGeneric6DofSpringConstraint_new2(rigidBodyB._native, ref frameInB, useLinearReferenceFrameB))
        {
            _rigidBodyA = FixedBody;
            _rigidBodyB = rigidBodyB;
        }

		public Generic6DofSpringConstraint(RigidBody rigidBodyB, Matrix frameInB, bool useLinearReferenceFrameB)
			: this(rigidBodyB, ref frameInB, useLinearReferenceFrameB)
		{
		}

		public void EnableSpring(int index, bool onOff)
		{
			btGeneric6DofSpringConstraint_enableSpring(_native, index, onOff);
		}

		public void SetDamping(int index, float damping)
		{
			btGeneric6DofSpringConstraint_setDamping(_native, index, damping);
		}

		public void SetEquilibriumPoint()
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint(_native);
		}

		public void SetEquilibriumPoint(int index)
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint2(_native, index);
		}

		public void SetEquilibriumPoint(int index, float val)
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint3(_native, index, val);
		}

		public void SetStiffness(int index, float stiffness)
		{
			btGeneric6DofSpringConstraint_setStiffness(_native, index, stiffness);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpringConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, bool useLinearReferenceFrameA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btGeneric6DofSpringConstraint_new2(IntPtr rbB, [In] ref Matrix frameInB, bool useLinearReferenceFrameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpringConstraint_enableSpring(IntPtr obj, int index, bool onOff);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpringConstraint_setDamping(IntPtr obj, int index, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpringConstraint_setEquilibriumPoint(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpringConstraint_setEquilibriumPoint2(IntPtr obj, int index);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpringConstraint_setEquilibriumPoint3(IntPtr obj, int index, float val);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btGeneric6DofSpringConstraint_setStiffness(IntPtr obj, int index, float stiffness);
	}
}
