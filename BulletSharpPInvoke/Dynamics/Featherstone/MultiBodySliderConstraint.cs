using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBodySliderConstraint : MultiBodyConstraint
	{
		internal MultiBodySliderConstraint(IntPtr native)
			: base(native)
		{
		}

		public MultiBodySliderConstraint(MultiBody body, int link, RigidBody bodyB,
			Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB, Vector3 jointAxis)
			: base(btMultiBodySliderConstraint_new(body._native, link, bodyB.Native,
				ref pivotInA, ref pivotInB, ref frameInA, ref frameInB, ref jointAxis))
		{
		}

		public MultiBodySliderConstraint(MultiBody bodyA, int linkA, MultiBody bodyB,
			int linkB, Vector3 pivotInA, Vector3 pivotInB, Matrix frameInA, Matrix frameInB,
			Vector3 jointAxis)
			: base(btMultiBodySliderConstraint_new2(bodyA._native, linkA, bodyB._native,
				linkB, ref pivotInA, ref pivotInB, ref frameInA, ref frameInB, ref jointAxis))
		{
		}

		public Matrix FrameInA
		{
			get
			{
				Matrix value;
				btMultiBodySliderConstraint_getFrameInA(_native, out value);
				return value;
			}
			set { btMultiBodySliderConstraint_setFrameInA(_native, ref value); }
		}

		public Matrix FrameInB
		{
			get
			{
				Matrix value;
				btMultiBodySliderConstraint_getFrameInB(_native, out value);
				return value;
			}
			set { btMultiBodySliderConstraint_setFrameInB(_native, ref value); }
		}

		public Vector3 JointAxis
		{
			get
			{
				Vector3 value;
				btMultiBodySliderConstraint_getJointAxis(_native, out value);
				return value;
			}
			set { btMultiBodySliderConstraint_setJointAxis(_native, ref value); }
		}

		public Vector3 PivotInA
		{
			get
			{
				Vector3 value;
				btMultiBodySliderConstraint_getPivotInA(_native, out value);
				return value;
			}
			set { btMultiBodySliderConstraint_setPivotInA(_native, ref value); }
		}

		public Vector3 PivotInB
		{
			get
			{
				Vector3 value;
				btMultiBodySliderConstraint_getPivotInB(_native, out value);
				return value;
			}
			set { btMultiBodySliderConstraint_setPivotInB(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodySliderConstraint_new(IntPtr body, int link, IntPtr bodyB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, [In] ref Vector3 jointAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultiBodySliderConstraint_new2(IntPtr bodyA, int linkA, IntPtr bodyB, int linkB, [In] ref Vector3 pivotInA, [In] ref Vector3 pivotInB, [In] ref Matrix frameInA, [In] ref Matrix frameInB, [In] ref Vector3 jointAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_getFrameInA(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_getFrameInB(IntPtr obj, out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_getJointAxis(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_getPivotInA(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_getPivotInB(IntPtr obj, out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_setFrameInA(IntPtr obj, [In] ref Matrix frameInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_setFrameInB(IntPtr obj, [In] ref Matrix frameInB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_setJointAxis(IntPtr obj, [In] ref Vector3 jointAxis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_setPivotInA(IntPtr obj, [In] ref Vector3 pivotInA);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultiBodySliderConstraint_setPivotInB(IntPtr obj, [In] ref Vector3 pivotInB);
	}
}
