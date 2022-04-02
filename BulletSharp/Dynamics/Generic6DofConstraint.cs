using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	[Flags]
	public enum SixDofFlags
	{
		None = 0,
		CfmNormal = 1,
		CfmStop = 2,
		ErpStop = 4
	}

	public class RotationalLimitMotor : BulletDisposableObject
	{
		internal RotationalLimitMotor(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public RotationalLimitMotor()
		{
			IntPtr native = btRotationalLimitMotor_new();
			InitializeUserOwned(native);
		}

		public RotationalLimitMotor(RotationalLimitMotor limitMotor)
		{
			IntPtr native = btRotationalLimitMotor_new2(limitMotor.Native);
			InitializeUserOwned(native);
		}

		public bool NeedApplyTorques()
		{
			return btRotationalLimitMotor_needApplyTorques(Native);
		}

		public float SolveAngularLimitsRef(float timeStep, ref Vector3 axis, float jacDiagABInv,
			RigidBody body0, RigidBody body1)
		{
			return btRotationalLimitMotor_solveAngularLimits(Native, timeStep, ref axis,
				jacDiagABInv, body0.Native, body1.Native);
		}

		public float SolveAngularLimits(float timeStep, Vector3 axis, float jacDiagABInv,
			RigidBody body0, RigidBody body1)
		{
			return btRotationalLimitMotor_solveAngularLimits(Native, timeStep, ref axis,
				jacDiagABInv, body0.Native, body1.Native);
		}

		public int TestLimitValue(float testValue)
		{
			return btRotationalLimitMotor_testLimitValue(Native, testValue);
		}

		public float AccumulatedImpulse
		{
			get => btRotationalLimitMotor_getAccumulatedImpulse(Native);
			set => btRotationalLimitMotor_setAccumulatedImpulse(Native, value);
		}

		public float Bounce
		{
			get => btRotationalLimitMotor_getBounce(Native);
			set => btRotationalLimitMotor_setBounce(Native, value);
		}

		public int CurrentLimit
		{
			get => btRotationalLimitMotor_getCurrentLimit(Native);
			set => btRotationalLimitMotor_setCurrentLimit(Native, value);
		}

		public float CurrentLimitError
		{
			get => btRotationalLimitMotor_getCurrentLimitError(Native);
			set => btRotationalLimitMotor_setCurrentLimitError(Native, value);
		}

		public float CurrentPosition
		{
			get => btRotationalLimitMotor_getCurrentPosition(Native);
			set => btRotationalLimitMotor_setCurrentPosition(Native, value);
		}

		public float Damping
		{
			get => btRotationalLimitMotor_getDamping(Native);
			set => btRotationalLimitMotor_setDamping(Native, value);
		}

		public bool EnableMotor
		{
			get => btRotationalLimitMotor_getEnableMotor(Native);
			set => btRotationalLimitMotor_setEnableMotor(Native, value);
		}

		public float HiLimit
		{
			get => btRotationalLimitMotor_getHiLimit(Native);
			set => btRotationalLimitMotor_setHiLimit(Native, value);
		}

		public bool IsLimited => btRotationalLimitMotor_isLimited(Native);
		public float LimitSoftness
		{
			get => btRotationalLimitMotor_getLimitSoftness(Native);
			set => btRotationalLimitMotor_setLimitSoftness(Native, value);
		}

		public float LoLimit
		{
			get => btRotationalLimitMotor_getLoLimit(Native);
			set => btRotationalLimitMotor_setLoLimit(Native, value);
		}

		public float MaxLimitForce
		{
			get => btRotationalLimitMotor_getMaxLimitForce(Native);
			set => btRotationalLimitMotor_setMaxLimitForce(Native, value);
		}

		public float MaxMotorForce
		{
			get => btRotationalLimitMotor_getMaxMotorForce(Native);
			set => btRotationalLimitMotor_setMaxMotorForce(Native, value);
		}

		public float NormalCfm
		{
			get => btRotationalLimitMotor_getNormalCFM(Native);
			set => btRotationalLimitMotor_setNormalCFM(Native, value);
		}

		public float StopCfm
		{
			get => btRotationalLimitMotor_getStopCFM(Native);
			set => btRotationalLimitMotor_setStopCFM(Native, value);
		}

		public float StopErp
		{
			get => btRotationalLimitMotor_getStopERP(Native);
			set => btRotationalLimitMotor_setStopERP(Native, value);
		}

		public float TargetVelocity
		{
			get => btRotationalLimitMotor_getTargetVelocity(Native);
			set => btRotationalLimitMotor_setTargetVelocity(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btRotationalLimitMotor_delete(Native);
			}
		}
	}

	public class TranslationalLimitMotor : BulletDisposableObject
	{
		internal TranslationalLimitMotor(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public TranslationalLimitMotor()
		{
			IntPtr native = btTranslationalLimitMotor_new();
			InitializeUserOwned(native);
		}

		public TranslationalLimitMotor(TranslationalLimitMotor other)
		{
			IntPtr native = btTranslationalLimitMotor_new2(other.Native);
			InitializeUserOwned(native);
		}

		public bool IsLimited(int limitIndex)
		{
			return btTranslationalLimitMotor_isLimited(Native, limitIndex);
		}

		public bool NeedApplyForce(int limitIndex)
		{
			return btTranslationalLimitMotor_needApplyForce(Native, limitIndex);
		}

		public float SolveLinearAxisRef(float timeStep, float jacDiagABInv, RigidBody body1,
			ref Vector3 pointInA, RigidBody body2, ref Vector3 pointInB, int limitIndex, ref Vector3 axisNormalOnA,
			ref Vector3 anchorPos)
		{
			return btTranslationalLimitMotor_solveLinearAxis(Native, timeStep, jacDiagABInv,
				body1.Native, ref pointInA, body2.Native, ref pointInB, limitIndex,
				ref axisNormalOnA, ref anchorPos);
		}

		public float SolveLinearAxis(float timeStep, float jacDiagABInv, RigidBody body1,
			Vector3 pointInA, RigidBody body2, Vector3 pointInB, int limitIndex, Vector3 axisNormalOnA,
			Vector3 anchorPos)
		{
			return btTranslationalLimitMotor_solveLinearAxis(Native, timeStep, jacDiagABInv,
				body1.Native, ref pointInA, body2.Native, ref pointInB, limitIndex,
				ref axisNormalOnA, ref anchorPos);
		}

		public int TestLimitValue(int limitIndex, float testValue)
		{
			return btTranslationalLimitMotor_testLimitValue(Native, limitIndex,
				testValue);
		}

		public Vector3 AccumulatedImpulse
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getAccumulatedImpulse(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setAccumulatedImpulse(Native, ref value);
		}
		/*
		public IntArray CurrentLimit
		{
			get { return new IntArray(btTranslationalLimitMotor_getCurrentLimit(Native), 3); }
		}
		*/
		public Vector3 CurrentLimitError
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getCurrentLimitError(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setCurrentLimitError(Native, ref value);
		}

		public Vector3 CurrentLinearDiff
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getCurrentLinearDiff(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setCurrentLinearDiff(Native, ref value);
		}

		public float Damping
		{
			get => btTranslationalLimitMotor_getDamping(Native);
			set => btTranslationalLimitMotor_setDamping(Native, value);
		}
		/*
		public bool EnableMotor
		{
			get { return btTranslationalLimitMotor_getEnableMotor(_native); }
		}
		*/
		public float LimitSoftness
		{
			get => btTranslationalLimitMotor_getLimitSoftness(Native);
			set => btTranslationalLimitMotor_setLimitSoftness(Native, value);
		}

		public Vector3 LowerLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getLowerLimit(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setLowerLimit(Native, ref value);
		}

		public Vector3 MaxMotorForce
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getMaxMotorForce(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setMaxMotorForce(Native, ref value);
		}

		public Vector3 NormalCfm
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getNormalCFM(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setNormalCFM(Native, ref value);
		}

		public float Restitution
		{
			get => btTranslationalLimitMotor_getRestitution(Native);
			set => btTranslationalLimitMotor_setRestitution(Native, value);
		}

		public Vector3 StopCfm
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getStopCFM(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setStopCFM(Native, ref value);
		}

		public Vector3 StopErp
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getStopERP(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setStopERP(Native, ref value);
		}

		public Vector3 TargetVelocity
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getTargetVelocity(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setTargetVelocity(Native, ref value);
		}

		public Vector3 UpperLimit
		{
			get
			{
				Vector3 value;
				btTranslationalLimitMotor_getUpperLimit(Native, out value);
				return value;
			}
			set => btTranslationalLimitMotor_setUpperLimit(Native, ref value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btTranslationalLimitMotor_delete(Native);
			}
		}
	}

	public class Generic6DofConstraint : TypedConstraint
	{
		private RotationalLimitMotor[] _angularLimits = new RotationalLimitMotor[3];
		private TranslationalLimitMotor _linearLimits;

		protected internal Generic6DofConstraint()
		{
		}

		public Generic6DofConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix4x4 frameInA, Matrix4x4 frameInB, bool useLinearReferenceFrameA)
		{
			IntPtr native = btGeneric6DofConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref frameInA, ref frameInB, useLinearReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}

		public Generic6DofConstraint(RigidBody rigidBodyB, Matrix4x4 frameInB, bool useLinearReferenceFrameB)
		{
			IntPtr native = btGeneric6DofConstraint_new2(rigidBodyB.Native, ref frameInB,
				useLinearReferenceFrameB);
			InitializeUserOwned(native);
			InitializeMembers(GetFixedBody(), rigidBodyB);
		}

		public void CalcAnchorPos()
		{
			btGeneric6DofConstraint_calcAnchorPos(Native);
		}

		public void CalculateTransformsRef(ref Matrix4x4 transA, ref Matrix4x4 transB)
		{
			btGeneric6DofConstraint_calculateTransforms(Native, ref transA, ref transB);
		}

		public void CalculateTransforms(Matrix4x4 transA, Matrix4x4 transB)
		{
			btGeneric6DofConstraint_calculateTransforms(Native, ref transA, ref transB);
		}

		public void CalculateTransforms()
		{
			btGeneric6DofConstraint_calculateTransforms2(Native);
		}

		public int GetLimitMotorInfo2(RotationalLimitMotor limitMotor, Matrix4x4 transA,
			Matrix4x4 transB, Vector3 linVelA, Vector3 linVelB, Vector3 angVelA, Vector3 angVelB,
			ConstraintInfo2 info, int row, ref Vector3 ax1, int rotational, int rotAllowed = 0)
		{
			return btGeneric6DofConstraint_get_limit_motor_info2(Native, limitMotor.Native,
				ref transA, ref transB, ref linVelA, ref linVelB, ref angVelA, ref angVelB,
				info.Native, row, ref ax1, rotational, rotAllowed);
		}

		public float GetAngle(int axisIndex)
		{
			return btGeneric6DofConstraint_getAngle(Native, axisIndex);
		}

		public Vector3 GetAxis(int axisIndex)
		{
			Vector3 value;
			btGeneric6DofConstraint_getAxis(Native, axisIndex, out value);
			return value;
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btGeneric6DofConstraint_getInfo1NonVirtual(Native, info.Native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix4x4 transA, Matrix4x4 transB,
			Vector3 linVelA, Vector3 linVelB, Vector3 angVelA, Vector3 angVelB)
		{
			btGeneric6DofConstraint_getInfo2NonVirtual(Native, info.Native, ref transA,
				ref transB, ref linVelA, ref linVelB, ref angVelA, ref angVelB);
		}

		public float GetRelativePivotPosition(int axisIndex)
		{
			return btGeneric6DofConstraint_getRelativePivotPosition(Native, axisIndex);
		}

		public RotationalLimitMotor GetRotationalLimitMotor(int index)
		{
			if (_angularLimits[index] == null)
			{
				_angularLimits[index] = new RotationalLimitMotor(btGeneric6DofConstraint_getRotationalLimitMotor(Native, index), this);
			}
			return _angularLimits[index];
		}

		public bool IsLimited(int limitIndex)
		{
			return btGeneric6DofConstraint_isLimited(Native, limitIndex);
		}

		public void SetAxisRef(ref Vector3 axis1, ref Vector3 axis2)
		{
			btGeneric6DofConstraint_setAxis(Native, ref axis1, ref axis2);
		}

		public void SetAxis(Vector3 axis1, Vector3 axis2)
		{
			btGeneric6DofConstraint_setAxis(Native, ref axis1, ref axis2);
		}

		public void SetFramesRef(ref Matrix4x4 frameA, ref Matrix4x4 frameB)
		{
			btGeneric6DofConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix4x4 frameA, Matrix4x4 frameB)
		{
			btGeneric6DofConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(int axis, float lo, float hi)
		{
			btGeneric6DofConstraint_setLimit(Native, axis, lo, hi);
		}

		public bool TestAngularLimitMotor(int axisIndex)
		{
			return btGeneric6DofConstraint_testAngularLimitMotor(Native, axisIndex);
		}

		public void UpdateRhs(float timeStep)
		{
			btGeneric6DofConstraint_updateRHS(Native, timeStep);
		}

		public Vector3 AngularLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getAngularLowerLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofConstraint_setAngularLowerLimit(Native, ref value);
		}

		public Vector3 AngularUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getAngularUpperLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofConstraint_setAngularUpperLimit(Native, ref value);
		}

		public Matrix4x4 CalculatedTransformA
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofConstraint_getCalculatedTransformA(Native, out value);
				return value;
			}
		}

		public Matrix4x4 CalculatedTransformB
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofConstraint_getCalculatedTransformB(Native, out value);
				return value;
			}
		}

		public SixDofFlags Flags => btGeneric6DofConstraint_getFlags(Native);

		public Matrix4x4 FrameOffsetA
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix4x4 FrameOffsetB
		{
			get
			{
				Matrix4x4 value;
				btGeneric6DofConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public Vector3 LinearLowerLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getLinearLowerLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofConstraint_setLinearLowerLimit(Native, ref value);
		}

		public Vector3 LinearUpperLimit
		{
			get
			{
				Vector3 value;
				btGeneric6DofConstraint_getLinearUpperLimit(Native, out value);
				return value;
			}
			set => btGeneric6DofConstraint_setLinearUpperLimit(Native, ref value);
		}

		public TranslationalLimitMotor TranslationalLimitMotor
		{
			get
			{
				if (_linearLimits == null)
				{
					_linearLimits = new TranslationalLimitMotor(btGeneric6DofConstraint_getTranslationalLimitMotor(Native), this);
				}
				return _linearLimits;
			}
		}

		public bool UseFrameOffset
		{
			get => btGeneric6DofConstraint_getUseFrameOffset(Native);
			set => btGeneric6DofConstraint_setUseFrameOffset(Native, value);
		}

		public bool UseLinearReferenceFrameA
		{
			get => btGeneric6DofConstraint_getUseLinearReferenceFrameA(Native);
			set => btGeneric6DofConstraint_setUseLinearReferenceFrameA(Native, value);
		}

		public bool UseSolveConstraintObsolete
		{
			get => btGeneric6DofConstraint_getUseSolveConstraintObsolete(Native);
			set => btGeneric6DofConstraint_setUseSolveConstraintObsolete(Native, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct Generic6DofConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public Vector3FloatData LinearUpperLimit;
		public Vector3FloatData LinearLowerLimit;
		public Vector3FloatData AngularUpperLimit;
		public Vector3FloatData AngularLowerLimit;
		public int UseLinearReferenceFrameA;
		public int UseOffsetForConstraintFrame;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct Generic6DofConstraintDoubleData
	{
		public TypedConstraintDoubleData TypedConstraintData;
		public TransformDoubleData RigidBodyAFrame;
		public TransformDoubleData RigidBodyBFrame;
		public Vector3DoubleData LinearUpperLimit;
		public Vector3DoubleData LinearLowerLimit;
		public Vector3DoubleData AngularUpperLimit;
		public Vector3DoubleData AngularLowerLimit;
		public int UseLinearReferenceFrameA;
		public int UseOffsetForConstraintFrame;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Generic6DofConstraintDoubleData), fieldName).ToInt32(); }
	}
}
