using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	[Flags]
	public enum HingeFlags
	{
		None = 0,
		CfmStop = 1,
		ErpStop = 2,
		CfmNormal = 4,
		ErpNormal = 8
	}

	public class HingeConstraint : TypedConstraint
	{
		protected internal HingeConstraint()
		{
		}

		public HingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Vector3 pivotInA,
			Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}

		public HingeConstraint(RigidBody rigidBodyA, Vector3 pivotInA, Vector3 axisInA,
			bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeConstraint_new2(rigidBodyA.Native, ref pivotInA, ref axisInA,
				useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, GetFixedBody());
		}

		public HingeConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix4x4 rigidBodyAFrame,
			Matrix4x4 rigidBodyBFrame, bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeConstraint_new3(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame, useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}

		public HingeConstraint(RigidBody rigidBodyA, Matrix4x4 rigidBodyAFrame, bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeConstraint_new4(rigidBodyA.Native, ref rigidBodyAFrame,
				useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, GetFixedBody());
		}

		public void EnableAngularMotor(bool enableMotor, float targetVelocity, float maxMotorImpulse)
		{
			btHingeConstraint_enableAngularMotor(Native, enableMotor, targetVelocity,
				maxMotorImpulse);
		}

		public float GetHingeAngleRef(ref Matrix4x4 transA, ref Matrix4x4 transB)
		{
			return btHingeConstraint_getHingeAngle(Native, ref transA, ref transB);
		}

		public float GetHingeAngle(Matrix4x4 transA, Matrix4x4 transB)
		{
			return btHingeConstraint_getHingeAngle(Native, ref transA, ref transB);
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btHingeConstraint_getInfo1NonVirtual(Native, info.Native);
		}

		public void GetInfo2Internal(ConstraintInfo2 info, Matrix4x4 transA, Matrix4x4 transB,
			Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2Internal(Native, info.Native, ref transA,
				ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2InternalUsingFrameOffset(ConstraintInfo2 info, Matrix4x4 transA,
			Matrix4x4 transB, Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2InternalUsingFrameOffset(Native, info.Native,
				ref transA, ref transB, ref angVelA, ref angVelB);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix4x4 transA, Matrix4x4 transB,
			Vector3 angVelA, Vector3 angVelB)
		{
			btHingeConstraint_getInfo2NonVirtual(Native, info.Native, ref transA,
				ref transB, ref angVelA, ref angVelB);
		}

		public void SetAxisRef(ref Vector3 axisInA)
		{
			btHingeConstraint_setAxis(Native, ref axisInA);
		}

		public void SetAxis(Vector3 axisInA)
		{
			btHingeConstraint_setAxis(Native, ref axisInA);
		}

		public void SetFramesRef(ref Matrix4x4 frameA, ref Matrix4x4 frameB)
		{
			btHingeConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetFrames(Matrix4x4 frameA, Matrix4x4 frameB)
		{
			btHingeConstraint_setFrames(Native, ref frameA, ref frameB);
		}

		public void SetLimit(float low, float high)
		{
			btHingeConstraint_setLimit(Native, low, high);
		}

		public void SetLimit(float low, float high, float softness)
		{
			btHingeConstraint_setLimit2(Native, low, high, softness);
		}

		public void SetLimit(float low, float high, float softness, float biasFactor)
		{
			btHingeConstraint_setLimit3(Native, low, high, softness, biasFactor);
		}

		public void SetLimit(float low, float high, float softness, float biasFactor,
			float relaxationFactor)
		{
			btHingeConstraint_setLimit4(Native, low, high, softness, biasFactor,
				relaxationFactor);
		}

		public void SetMotorTarget(float targetAngle, float deltaTime)
		{
			btHingeConstraint_setMotorTarget(Native, targetAngle, deltaTime);
		}

		public void SetMotorTargetRef(ref Quaternion qAinB, float deltaTime)
		{
			btHingeConstraint_setMotorTarget2(Native, ref qAinB, deltaTime);
		}

		public void SetMotorTarget(Quaternion qAinB, float deltaTime)
		{
			btHingeConstraint_setMotorTarget2(Native, ref qAinB, deltaTime);
		}

		public void TestLimitRef(ref Matrix4x4 transA, ref Matrix4x4 transB)
		{
			btHingeConstraint_testLimit(Native, ref transA, ref transB);
		}

		public void TestLimit(Matrix4x4 transA, Matrix4x4 transB)
		{
			btHingeConstraint_testLimit(Native, ref transA, ref transB);
		}

		public void UpdateRhs(float timeStep)
		{
			btHingeConstraint_updateRHS(Native, timeStep);
		}

		public Matrix4x4 AFrame
		{
			get
			{
				Matrix4x4 value;
				btHingeConstraint_getAFrame(Native, out value);
				return value;
			}
		}

		public bool AngularOnly
		{
			get => btHingeConstraint_getAngularOnly(Native);
			set => btHingeConstraint_setAngularOnly(Native, value);
		}

		public Matrix4x4 BFrame
		{
			get
			{
				Matrix4x4 value;
				btHingeConstraint_getBFrame(Native, out value);
				return value;
			}
		}

		public bool EnableMotor
		{
			get => btHingeConstraint_getEnableAngularMotor(Native);
			set => btHingeConstraint_enableMotor(Native, value);
		}

		public HingeFlags Flags => btHingeConstraint_getFlags(Native);

		public Matrix4x4 FrameOffsetA
		{
			get
			{
				Matrix4x4 value;
				btHingeConstraint_getFrameOffsetA(Native, out value);
				return value;
			}
		}

		public Matrix4x4 FrameOffsetB
		{
			get
			{
				Matrix4x4 value;
				btHingeConstraint_getFrameOffsetB(Native, out value);
				return value;
			}
		}

		public bool HasLimit => btHingeConstraint_hasLimit(Native);

		public float HingeAngle => btHingeConstraint_getHingeAngle2(Native);

		public float LimitBiasFactor => btHingeConstraint_getLimitBiasFactor(Native);

		public float LimitRelaxationFactor => btHingeConstraint_getLimitRelaxationFactor(Native);

		public float LimitSign => btHingeConstraint_getLimitSign(Native);

		public float LimitSoftness => btHingeConstraint_getLimitSoftness(Native);

		public float LowerLimit => btHingeConstraint_getLowerLimit(Native);

		public float MaxMotorImpulse
		{
			get => btHingeConstraint_getMaxMotorImpulse(Native);
			set => btHingeConstraint_setMaxMotorImpulse(Native, value);
		}

		public float MotorTargetVelocity => btHingeConstraint_getMotorTargetVelocity(Native);

		public int SolveLimit => btHingeConstraint_getSolveLimit(Native);

		public float UpperLimit => btHingeConstraint_getUpperLimit(Native);

		public bool UseFrameOffset
		{
			get => btHingeConstraint_getUseFrameOffset(Native);
			set => btHingeConstraint_setUseFrameOffset(Native, value);
		}

		public bool UseReferenceFrameA
		{
			get => btHingeConstraint_getUseReferenceFrameA(Native);
			set => btHingeConstraint_setUseReferenceFrameA(Native, value);
		}
	}

	public class HingeAccumulatedAngleConstraint : HingeConstraint
	{
		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Vector3 pivotInA, Vector3 pivotInB, Vector3 axisInA, Vector3 axisInB, bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeAccumulatedAngleConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB, ref axisInA, ref axisInB, useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, rigidBodyB);
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, Vector3 pivotInA,
			Vector3 axisInA, bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeAccumulatedAngleConstraint_new2(rigidBodyA.Native, ref pivotInA,
				ref axisInA, useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, GetFixedBody());
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			Matrix4x4 rigidBodyAFrame, Matrix4x4 rigidBodyBFrame, bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeAccumulatedAngleConstraint_new3(rigidBodyA.Native, rigidBodyB.Native,
				ref rigidBodyAFrame, ref rigidBodyBFrame, useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, GetFixedBody());
		}

		public HingeAccumulatedAngleConstraint(RigidBody rigidBodyA, Matrix4x4 rigidBodyAFrame,
			bool useReferenceFrameA = false)
		{
			IntPtr native = btHingeAccumulatedAngleConstraint_new4(rigidBodyA.Native, ref rigidBodyAFrame,
				useReferenceFrameA);
			InitializeUserOwned(native);
			InitializeMembers(rigidBodyA, GetFixedBody());
		}

		public float AccumulatedHingeAngle
		{
			get => btHingeAccumulatedAngleConstraint_getAccumulatedHingeAngle(Native);
			set => btHingeAccumulatedAngleConstraint_setAccumulatedHingeAngle(Native, value);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct HingeConstraintFloatData
	{
		public TypedConstraintFloatData TypedConstraintData;
		public TransformFloatData RigidBodyAFrame;
		public TransformFloatData RigidBodyBFrame;
		public int UseReferenceFrameA;
		public int AngularOnly;
		public int EnableAngularMotor;
		public float MotorTargetVelocity;
		public float MaxMotorImpulse;
		public float LowerLimit;
		public float UpperLimit;
		public float LimitSoftness;
		public float BiasFactor;
		public float RelaxationFactor;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(HingeConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct HingeConstraintDoubleData
	{
		public TypedConstraintDoubleData TypedConstraintData;
		public TransformDoubleData RigidBodyAFrame;
		public TransformDoubleData RigidBodyBFrame;
		public int UseReferenceFrameA;
		public int AngularOnly;
		public int EnableAngularMotor;
		public double MotorTargetVelocity;
		public double MaxMotorImpulse;
		public double LowerLimit;
		public double UpperLimit;
		public double LimitSoftness;
		public double BiasFactor;
		public double RelaxationFactor;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(HingeConstraintDoubleData), fieldName).ToInt32(); }
	}
}
