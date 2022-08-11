using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum RotateOrder
	{
		XYZ,
		XZY,
		YXZ,
		YZX,
		ZXY,
		ZYX
	}

	public class RotationalLimitMotor2 : BulletDisposableObject
	{
		internal RotationalLimitMotor2(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public RotationalLimitMotor2()
		{
			IntPtr native = btRotationalLimitMotor2_new();
			InitializeUserOwned(native);
		}

		public RotationalLimitMotor2(RotationalLimitMotor2 limitMotor)
		{
			IntPtr native = btRotationalLimitMotor2_new2(limitMotor.Native);
			InitializeUserOwned(native);
		}

		public void TestLimitValue(float testValue)
		{
			btRotationalLimitMotor2_testLimitValue(Native, testValue);
		}

		public float Bounce
		{
			get => btRotationalLimitMotor2_getBounce(Native);
			set => btRotationalLimitMotor2_setBounce(Native, value);
		}

		public int CurrentLimit
		{
			get => btRotationalLimitMotor2_getCurrentLimit(Native);
			set => btRotationalLimitMotor2_setCurrentLimit(Native, value);
		}

		public float CurrentLimitError
		{
			get => btRotationalLimitMotor2_getCurrentLimitError(Native);
			set => btRotationalLimitMotor2_setCurrentLimitError(Native, value);
		}

		public float CurrentLimitErrorHi
		{
			get => btRotationalLimitMotor2_getCurrentLimitErrorHi(Native);
			set => btRotationalLimitMotor2_setCurrentLimitErrorHi(Native, value);
		}

		public float CurrentPosition
		{
			get => btRotationalLimitMotor2_getCurrentPosition(Native);
			set => btRotationalLimitMotor2_setCurrentPosition(Native, value);
		}

		public bool EnableMotor
		{
			get => btRotationalLimitMotor2_getEnableMotor(Native);
			set => btRotationalLimitMotor2_setEnableMotor(Native, value);
		}

		public bool EnableSpring
		{
			get => btRotationalLimitMotor2_getEnableSpring(Native);
			set => btRotationalLimitMotor2_setEnableSpring(Native, value);
		}

		public float EquilibriumPoint
		{
			get => btRotationalLimitMotor2_getEquilibriumPoint(Native);
			set => btRotationalLimitMotor2_setEquilibriumPoint(Native, value);
		}

		public float HiLimit
		{
			get => btRotationalLimitMotor2_getHiLimit(Native);
			set => btRotationalLimitMotor2_setHiLimit(Native, value);
		}

		public bool IsLimited => btRotationalLimitMotor2_isLimited(Native);

		public float LoLimit
		{
			get => btRotationalLimitMotor2_getLoLimit(Native);
			set => btRotationalLimitMotor2_setLoLimit(Native, value);
		}

		public float MaxMotorForce
		{
			get => btRotationalLimitMotor2_getMaxMotorForce(Native);
			set => btRotationalLimitMotor2_setMaxMotorForce(Native, value);
		}

		public float MotorCfm
		{
			get => btRotationalLimitMotor2_getMotorCFM(Native);
			set => btRotationalLimitMotor2_setMotorCFM(Native, value);
		}

		public float MotorErp
		{
			get => btRotationalLimitMotor2_getMotorERP(Native);
			set => btRotationalLimitMotor2_setMotorERP(Native, value);
		}

		public bool ServoMotor
		{
			get => btRotationalLimitMotor2_getServoMotor(Native);
			set => btRotationalLimitMotor2_setServoMotor(Native, value);
		}

		public float ServoTarget
		{
			get => btRotationalLimitMotor2_getServoTarget(Native);
			set => btRotationalLimitMotor2_setServoTarget(Native, value);
		}

		public float SpringDamping
		{
			get => btRotationalLimitMotor2_getSpringDamping(Native);
			set => btRotationalLimitMotor2_setSpringDamping(Native, value);
		}

		public bool SpringDampingLimited
		{
			get => btRotationalLimitMotor2_getSpringDampingLimited(Native);
			set => btRotationalLimitMotor2_setSpringDampingLimited(Native, value);
		}

		public float SpringStiffness
		{
			get => btRotationalLimitMotor2_getSpringStiffness(Native);
			set => btRotationalLimitMotor2_setSpringStiffness(Native, value);
		}

		public bool SpringStiffnessLimited
		{
			get => btRotationalLimitMotor2_getSpringStiffnessLimited(Native);
			set => btRotationalLimitMotor2_setSpringStiffnessLimited(Native, value);
		}

		public float StopCfm
		{
			get => btRotationalLimitMotor2_getStopCFM(Native);
			set => btRotationalLimitMotor2_setStopCFM(Native, value);
		}

		public float StopErp
		{
			get => btRotationalLimitMotor2_getStopERP(Native);
			set => btRotationalLimitMotor2_setStopERP(Native, value);
		}

		public float TargetVelocity
		{
			get => btRotationalLimitMotor2_getTargetVelocity(Native);
			set => btRotationalLimitMotor2_setTargetVelocity(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btRotationalLimitMotor2_delete(Native);
			}
		}
	}

	public class TranslationalLimitMotor2 : BulletDisposableObject
	{
		internal TranslationalLimitMotor2(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public TranslationalLimitMotor2()
		{
			IntPtr native = btTranslationalLimitMotor2_new();
			InitializeUserOwned(native);
		}

		public TranslationalLimitMotor2(TranslationalLimitMotor2 other)
		{
			IntPtr native = btTranslationalLimitMotor2_new2(other.Native);
			InitializeUserOwned(native);
		}

		public bool IsLimited(int limitIndex)
		{
			return btTranslationalLimitMotor2_isLimited(Native, limitIndex);
		}

		public void TestLimitValue(int limitIndex, float testValue)
		{
			btTranslationalLimitMotor2_testLimitValue(Native, limitIndex, testValue);
		}

		public Vector3 Bounce
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getBounce(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setBounce(Native, ref value);
		}
		/*
		public IntArray CurrentLimit
		{
			get { return new IntArray(btTranslationalLimitMotor2_getCurrentLimit(Native), 3); }
		}
		*/
		public Vector3 CurrentLimitError
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getCurrentLimitError(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setCurrentLimitError(Native, ref value);
		}

		public Vector3 CurrentLimitErrorHi
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getCurrentLimitErrorHi(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setCurrentLimitErrorHi(Native, ref value);
		}

		public Vector3 CurrentLinearDiff
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getCurrentLinearDiff(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setCurrentLinearDiff(Native, ref value);
		}
		/*
		public BoolArray EnableMotor
		{
			get { return new BoolArray(btTranslationalLimitMotor2_getEnableMotor(Native), 3); }
		}

		public BoolArray EnableSpring
		{
			get { return new BoolArray(btTranslationalLimitMotor2_getEnableSpring(Native), 3); }
		}
		*/
		public Vector3 EquilibriumPoint
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getEquilibriumPoint(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setEquilibriumPoint(Native, ref value);
		}

		public Vector3 LowerLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getLowerLimit(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setLowerLimit(Native, ref value);
		}

		public Vector3 MaxMotorForce
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getMaxMotorForce(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setMaxMotorForce(Native, ref value);
		}

		public Vector3 MotorCFM
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getMotorCFM(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setMotorCFM(Native, ref value);
		}

		public Vector3 MotorERP
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getMotorERP(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setMotorERP(Native, ref value);
		}
		/*
		public BoolArray ServoMotor
		{
			get { return new BoolArray(btTranslationalLimitMotor2_getServoMotor(Native)); }
		}
		*/
		public Vector3 ServoTarget
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getServoTarget(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setServoTarget(Native, ref value);
		}

		public Vector3 SpringDamping
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getSpringDamping(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setSpringDamping(Native, ref value);
		}
		/*
		public BoolArray SpringDampingLimited
		{
			get { return btTranslationalLimitMotor2_getSpringDampingLimited(Native); }
		}
		*/
		public Vector3 SpringStiffness
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getSpringStiffness(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setSpringStiffness(Native, ref value);
		}
		/*
		public BoolArray SpringStiffnessLimited
		{
			get { return btTranslationalLimitMotor2_getSpringStiffnessLimited(Native); }
		}
		*/
		public Vector3 StopCfm
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getStopCFM(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setStopCFM(Native, ref value);
		}

		public Vector3 StopEep
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getStopERP(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setStopERP(Native, ref value);
		}

		public Vector3 TargetVelocity
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getTargetVelocity(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setTargetVelocity(Native, ref value);
		}

		public Vector3 UpperLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor2_getUpperLimit(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor2_setUpperLimit(Native, ref value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btTranslationalLimitMotor2_delete(Native);
			}
		}
	}

	public class Generic6DofSpring2Constraint : TypedConstraint
	{
		private RotationalLimitMotor2[] _angularLimits = new RotationalLimitMotor2[3];
		private TranslationalLimitMotor2 _linearLimits;

		protected internal Generic6DofSpring2Constraint()
		{
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix4x4 frameInA, Matrix4x4 frameInB, RotateOrder rotOrder = RotateOrder.XYZ)
		{
			IntPtr native = btGeneric6DofSpring2Constraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB, rotOrder);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}

		public Generic6DofSpring2Constraint(RigidBody rigidBodyB, Matrix4x4 frameInB,
			RotateOrder rotOrder = RotateOrder.XYZ)
		{
			IntPtr native = btGeneric6DofSpring2Constraint_new2(rigidBodyB.Native, ref frameInB,
				rotOrder);
			InitializeUserOwned(native);
			InitializeMembers(GetFixedBody(), rigidBodyB);
		}

		public static float BtGetMatrixElem(Matrix4x4 mat, int index)
		{
			return btGeneric6DofSpring2Constraint_btGetMatrixElem(ref mat, index);
		}

		public void CalculateTransforms(Matrix4x4 transA, Matrix4x4 transB)
		{
			btGeneric6DofSpring2Constraint_calculateTransforms(Native, ref transA,
				ref transB);
		}

		public void CalculateTransforms()
		{
			btGeneric6DofSpring2Constraint_calculateTransforms2(Native);
		}

		public void EnableMotor(int index, bool onOff)
		{
			btGeneric6DofSpring2Constraint_enableMotor(Native, index, onOff);
		}

		public void EnableSpring(int index, bool onOff)
		{
			btGeneric6DofSpring2Constraint_enableSpring(Native, index, onOff);
		}

		public float GetAngle(int axisIndex)
		{
			return btGeneric6DofSpring2Constraint_getAngle(Native, axisIndex);
		}

		public Vector3 GetAxis(int axisIndex)
		{
			Vector3 value;
			btGeneric6DofSpring2Constraint_getAxis(Native, axisIndex, out value);
			return value;
		}

		public float GetRelativePivotPosition(int axisIndex)
		{
			return btGeneric6DofSpring2Constraint_getRelativePivotPosition(Native,
				axisIndex);
		}

		public RotationalLimitMotor2 GetRotationalLimitMotor(int index)
		{
			if (_angularLimits[index] == null)
			{
				_angularLimits[index] = new RotationalLimitMotor2(btGeneric6DofSpring2Constraint_getRotationalLimitMotor(Native, index), this);
			}
			return _angularLimits[index];
		}

		public bool IsLimited(int limitIndex)
		{
			return btGeneric6DofSpring2Constraint_isLimited(Native, limitIndex);
		}

		public static bool MatrixToEulerZXY(Matrix4x4 mat, ref Vector3 xyz)
		{
			return btGeneric6DofSpring2Constraint_matrixToEulerZXY(ref mat, ref xyz);
		}

		public static bool MatrixToEulerZYX(Matrix4x4 mat, ref Vector3 xyz)
		{
			return btGeneric6DofSpring2Constraint_matrixToEulerZYX(ref mat, ref xyz);
		}

		public static bool MatrixToEulerXZY(Matrix4x4 mat, ref Vector3 xyz)
		{
			return btGeneric6DofSpring2Constraint_matrixToEulerXZY(ref mat, ref xyz);
		}

		public static bool MatrixToEulerXYZ(Matrix4x4 mat, ref Vector3 xyz)
		{
			return btGeneric6DofSpring2Constraint_matrixToEulerXYZ(ref mat, ref xyz);
		}

		public static bool MatrixToEulerYZX(Matrix4x4 mat, ref Vector3 xyz)
		{
			return btGeneric6DofSpring2Constraint_matrixToEulerYZX(ref mat, ref xyz);
		}

		public static bool MatrixToEulerYXZ(Matrix4x4 mat, ref Vector3 xyz)
		{
			return btGeneric6DofSpring2Constraint_matrixToEulerYXZ(ref mat, ref xyz);
		}

		public void SetAxis(Vector3 axis1, Vector3 axis2)
		{
			btGeneric6DofSpring2Constraint_setAxis(Native, ref axis1, ref axis2);
		}

		public void SetBounce(int index, float bounce)
		{
			btGeneric6DofSpring2Constraint_setBounce(Native, index, bounce);
		}

		public void SetDamping(int index, float damping, bool limitIfNeeded = true)
		{
			btGeneric6DofSpring2Constraint_setDamping(Native, index, damping, limitIfNeeded);
		}

		public void SetEquilibriumPoint()
		{
			btGeneric6DofSpring2Constraint_setEquilibriumPoint(Native);
		}

		public void SetEquilibriumPoint(int index, float val)
		{
			btGeneric6DofSpring2Constraint_setEquilibriumPoint2(Native, index, val);
		}

		public void SetEquilibriumPoint(int index)
		{
			btGeneric6DofSpring2Constraint_setEquilibriumPoint3(Native, index);
		}

		public void SetFrames(Matrix4x4 frameA, Matrix4x4 frameB)
		{
			btGeneric6DofSpring2Constraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(int axis, float lo, float hi)
		{
			btGeneric6DofSpring2Constraint_setLimit(Native, axis, lo, hi);
		}

		public void SetLimitReversed(int axis, float lo, float hi)
		{
			btGeneric6DofSpring2Constraint_setLimitReversed(Native, axis, lo, hi);
		}

		public void SetMaxMotorForce(int index, float force)
		{
			btGeneric6DofSpring2Constraint_setMaxMotorForce(Native, index, force);
		}

		public void SetServo(int index, bool onOff)
		{
			btGeneric6DofSpring2Constraint_setServo(Native, index, onOff);
		}

		public void SetServoTarget(int index, float target)
		{
			btGeneric6DofSpring2Constraint_setServoTarget(Native, index, target);
		}

		public void SetStiffness(int index, float stiffness, bool limitIfNeeded = true)
		{
			btGeneric6DofSpring2Constraint_setStiffness(Native, index, stiffness,
				limitIfNeeded);
		}

		public void SetTargetVelocity(int index, float velocity)
		{
			btGeneric6DofSpring2Constraint_setTargetVelocity(Native, index, velocity);
		}

		public Vector3 AngularLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularLowerLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofSpring2Constraint_setAngularLowerLimit(Native, ref value);
		}

		public Vector3 AngularLowerLimitReversed
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularLowerLimitReversed(Native, out value);
				return value;
			}
			set => btGeneric6DofSpring2Constraint_setAngularLowerLimitReversed(Native, ref value);
		}

		public Vector3 AngularUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularUpperLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofSpring2Constraint_setAngularUpperLimit(Native, ref value);
		}

		public Vector3 AngularUpperLimitReversed
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getAngularUpperLimitReversed(Native, out value);
				return value;
			}
			set => btGeneric6DofSpring2Constraint_setAngularUpperLimitReversed(Native, ref value);
		}

		public Matrix4x4 CalculatedTransformA
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofSpring2Constraint_getCalculatedTransformA(Native, out value);
				return value;
			}
		}

		public Matrix4x4 CalculatedTransformB
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofSpring2Constraint_getCalculatedTransformB(Native, out value);
				return value;
			}
		}

		public Matrix4x4 FrameOffsetA
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofSpring2Constraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix4x4 FrameOffsetB
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofSpring2Constraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public Vector3 LinearLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getLinearLowerLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofSpring2Constraint_setLinearLowerLimit(Native, ref value);
		}

		public Vector3 LinearUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofSpring2Constraint_getLinearUpperLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofSpring2Constraint_setLinearUpperLimit(Native, ref value);
		}

		public RotateOrder RotationOrder
		{
			get => btGeneric6DofSpring2Constraint_getRotationOrder(Native);
			set => btGeneric6DofSpring2Constraint_setRotationOrder(Native, value);
		}

		public TranslationalLimitMotor2 TranslationalLimitMotor
		{
			get
			{
				if (_linearLimits == null)
				{
					_linearLimits = new TranslationalLimitMotor2(btGeneric6DofSpring2Constraint_getTranslationalLimitMotor(Native), this);
				}
				return _linearLimits;
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct Generic6DofSpring2ConstraintFloatData
	{
		public TypedConstraintFloatData TypeConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public Vector3FloatData LinearUpperLimit;
		public Vector3FloatData LinearLowerLimit;
		public Vector3FloatData LinearBounce;
		public Vector3FloatData LinearStopErp;
		public Vector3FloatData LinearStopCfm;
		public Vector3FloatData LinearMotorErp;
		public Vector3FloatData LinearMotorCfm;
		public Vector3FloatData LinearTargetVelocity;
		public Vector3FloatData LinearMaxMotorForce;
		public Vector3FloatData LinearServoTarget;
		public Vector3FloatData LinearSpringStiffness;
		public Vector3FloatData LinearSpringDamping;
		public Vector3FloatData LinearEquilibriumPoint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearEnableMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearServoMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearEnableSpring;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearSpringStiffnessLimited;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearSpringDampingLimited;
		public int Padding;
		public Vector3FloatData AngularUpperLimit;
		public Vector3FloatData AngularLowerLimit;
		public Vector3FloatData AngularBounce;
		public Vector3FloatData AngularStopErp;
		public Vector3FloatData AngularStopCfm;
		public Vector3FloatData AngularMotorErp;
		public Vector3FloatData AngularMotorCfm;
		public Vector3FloatData AngularTargetVelocity;
		public Vector3FloatData AngularMaxMotorForce;
		public Vector3FloatData AngularServoTarget;
		public Vector3FloatData AngularSpringStiffness;
		public Vector3FloatData AngularSpringDamping;
		public Vector3FloatData AngularEquilibriumPoint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularEnableMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularServoMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularEnableSpring;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularSpringStiffnessLimited;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularSpringDampingLimited;
		public int RotateOrder;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofSpring2ConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct Generic6DofSpring2ConstraintDoubleData
	{
		public TypedConstraintDoubleData TypeConstraintData;
		public TransformDoubleData RigidBodyAFrame;
		public TransformDoubleData RigidBodyBFrame;
		public Vector3DoubleData LinearUpperLimit;
		public Vector3DoubleData LinearLowerLimit;
		public Vector3DoubleData LinearBounce;
		public Vector3DoubleData LinearStopErp;
		public Vector3DoubleData LinearStopCfm;
		public Vector3DoubleData LinearMotorErp;
		public Vector3DoubleData LinearMotorCfm;
		public Vector3DoubleData LinearTargetVelocity;
		public Vector3DoubleData LinearMaxMotorForce;
		public Vector3DoubleData LinearServoTarget;
		public Vector3DoubleData LinearSpringStiffness;
		public Vector3DoubleData LinearSpringDamping;
		public Vector3DoubleData LinearEquilibriumPoint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearEnableMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearServoMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearEnableSpring;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearSpringStiffnessLimited;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] LinearSpringDampingLimited;
		public int Padding;
		public Vector3DoubleData AngularUpperLimit;
		public Vector3DoubleData AngularLowerLimit;
		public Vector3DoubleData AngularBounce;
		public Vector3DoubleData AngularStopErp;
		public Vector3DoubleData AngularStopCfm;
		public Vector3DoubleData AngularMotorErp;
		public Vector3DoubleData AngularMotorCfm;
		public Vector3DoubleData AngularTargetVelocity;
		public Vector3DoubleData AngularMaxMotorForce;
		public Vector3DoubleData AngularServoTarget;
		public Vector3DoubleData AngularSpringStiffness;
		public Vector3DoubleData AngularSpringDamping;
		public Vector3DoubleData AngularEquilibriumPoint;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularEnableMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularServoMotor;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularEnableSpring;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularSpringStiffnessLimited;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] AngularSpringDampingLimited;
		public int RotateOrder;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofSpring2ConstraintDoubleData), fieldName).ToInt32(); }
	}
}
