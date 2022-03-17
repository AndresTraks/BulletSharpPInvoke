using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class GearConstraint : TypedConstraint
	{
		public GearConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 axisInA,
			Vector3 axisInB, float ratio = 1.0f)
		{
			IntPtr native = btGearConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref axisInA, ref axisInB, ratio);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}

		public Vector3 AxisA
		{
			get
			{
				Vector3 value;
				btGearConstraint_getAxisA(Native, out value);
				return value;
			}
			set => btGearConstraint_setAxisA(Native, ref value);
		}

		public Vector3 AxisB
		{
			get
			{
				Vector3 value;
				btGearConstraint_getAxisB(Native, out value);
				return value;
			}
			set => btGearConstraint_setAxisB(Native, ref value);
		}

		public float Ratio
		{
			get => btGearConstraint_getRatio(Native);
			set => btGearConstraint_setRatio(Native, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct GearConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public Vector3FloatData AxisInA;
		public Vector3FloatData AxisInB;
		public float Ratio;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(GearConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct GearConstraintDoubleData
	{
		public TypedConstraintDoubleData TypedConstraintData;
		public Vector3DoubleData AxisInA;
		public Vector3DoubleData AxisInB;
		public double Ratio;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(GearConstraintDoubleData), fieldName).ToInt32(); }
	}
}
