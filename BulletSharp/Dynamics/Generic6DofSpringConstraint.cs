using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Generic6DofSpringConstraint : Generic6DofConstraint
	{
		public Generic6DofSpringConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix4x4 frameInA, Matrix4x4 frameInB, bool useLinearReferenceFrameA)
		{
			IntPtr native = btGeneric6DofSpringConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB, useLinearReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}

		public Generic6DofSpringConstraint(RigidBody rigidBodyB, Matrix4x4 frameInB,
			bool useLinearReferenceFrameB)
		{
			IntPtr native = btGeneric6DofSpringConstraint_new2(rigidBodyB.Native, ref frameInB,
				useLinearReferenceFrameB);
			InitializeUserOwned(native);
			InitializeMembers(GetFixedBody(), rigidBodyB);
		}

		public void EnableSpring(int index, bool onOff)
		{
			btGeneric6DofSpringConstraint_enableSpring(Native, index, onOff);
		}

		public float GetDamping(int index)
		{
			return btGeneric6DofSpringConstraint_getDamping(Native, index);
		}

		public float GetEquilibriumPoint(int index)
		{
			return btGeneric6DofSpringConstraint_getEquilibriumPoint(Native, index);
		}

		public float GetStiffness(int index)
		{
			return btGeneric6DofSpringConstraint_getStiffness(Native, index);
		}

		public bool IsSpringEnabled(int index)
		{
			return btGeneric6DofSpringConstraint_isSpringEnabled(Native, index);
		}

		public void SetDamping(int index, float damping)
		{
			btGeneric6DofSpringConstraint_setDamping(Native, index, damping);
		}

		public void SetEquilibriumPoint()
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint(Native);
		}

		public void SetEquilibriumPoint(int index)
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint2(Native, index);
		}

		public void SetEquilibriumPoint(int index, float val)
		{
			btGeneric6DofSpringConstraint_setEquilibriumPoint3(Native, index, val);
		}

		public void SetStiffness(int index, float stiffness)
		{
			btGeneric6DofSpringConstraint_setStiffness(Native, index, stiffness);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct Generic6DofSpringConstraintFloatData
	{
		public Generic6DofConstraintFloatData SixDofData;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public int[] SpringEnabled;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public float[] EquilibriumPoint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public float[] SpringStiffness;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public float[] SpringDamping;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofSpringConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct Generic6DofSpringConstraintDoubleData
	{
		public Generic6DofConstraintDoubleData SixDofData;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public int[] SpringEnabled;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public double[] EquilibriumPoint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public double[] SpringStiffness;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public double[] SpringDamping;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofSpringConstraintDoubleData), fieldName).ToInt32(); }
	}
}
