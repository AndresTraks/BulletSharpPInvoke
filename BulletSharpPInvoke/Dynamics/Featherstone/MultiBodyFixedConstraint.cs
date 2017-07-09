using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBodyFixedConstraint : MultiBodyConstraint
	{
		internal MultiBodyFixedConstraint(IntPtr native)
			: base(native)
		{
		}

		public MultiBodyFixedConstraint(MultiBody body, int link, RigidBody bodyB,
			Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB)
			: base(btMultiBodyFixedConstraint_new(body._native, link, bodyB.Native,
				ref pivotInA, ref pivotInB, ref frameInA, ref frameInB))
		{
		}

		public MultiBodyFixedConstraint(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB)
			: base(btMultiBodyFixedConstraint_new2(bodyA._native, linkA, bodyB._native,
				linkB, ref pivotInA, ref pivotInB, ref frameInA, ref frameInB))
		{
		}

		public Matrix FrameInA
		{
			get
			{
				Matrix value;
				btMultiBodyFixedConstraint_getFrameInA(_native, out value);
				return value;
			}
			set { btMultiBodyFixedConstraint_setFrameInA(_native, ref value); }
		}

		public Matrix FrameInB
		{
			get
			{
				Matrix value;
				btMultiBodyFixedConstraint_getFrameInB(_native, out value);
				return value;
			}
			set { btMultiBodyFixedConstraint_setFrameInB(_native, ref value); }
		}

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				btMultiBodyFixedConstraint_getPivotInA(_native, out value);
				return value;
			}
			set { btMultiBodyFixedConstraint_setPivotInA(_native, ref value); }
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btMultiBodyFixedConstraint_getPivotInB(_native, out value);
				return value;
			}
			set { btMultiBodyFixedConstraint_setPivotInB(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyFixedConstraint_new(IntPtr body, int link, IntPtr bodyB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodyFixedConstraint_new2(IntPtr bodyA, int linkA, IntPtr bodyB, int linkB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_getFrameInA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_getFrameInB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_getPivotInA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_getPivotInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_setFrameInA(IntPtr obj, [In] ref Matrix frameInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_setFrameInB(IntPtr obj, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_setPivotInA(IntPtr obj, [In] ref Vector3 pivotInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodyFixedConstraint_setPivotInB(IntPtr obj, [In] ref Vector3 pivotInB);
	}
}
