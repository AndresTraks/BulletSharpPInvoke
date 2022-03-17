using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum ConstraintParam
	{
		Erp = 1,
		StopErp,
		Cfm,
		StopCfm
	}

	public enum TypedConstraintType
	{
		Point2Point = 3,
		Hinge,
		ConeTwist,
		D6,
		Slider,
		Contact,
		D6Spring,
		Gear,
		Fixed,
		D6Spring2,
		Max
	}

	public class JointFeedback : BulletDisposableObject
	{
		public JointFeedback()
		{
			IntPtr native = btJointFeedback_new();
			InitializeUserOwned(native);
		}

		public Vector3 AppliedForceBodyA
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedForceBodyA(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedForceBodyA(Native, ref value);
		}

		public Vector3 AppliedForceBodyB
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedForceBodyB(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedForceBodyB(Native, ref value);
		}

		public Vector3 AppliedTorqueBodyA
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedTorqueBodyA(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedTorqueBodyA(Native, ref value);
		}

		public Vector3 AppliedTorqueBodyB
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedTorqueBodyB(Native, out value);
				return value;
			}
			set => btJointFeedback_setAppliedTorqueBodyB(Native, ref value);
		}

		protected override void Dispose(bool disposing)
		{
			btJointFeedback_delete(Native);
		}
	}

	public abstract class TypedConstraint : BulletDisposableObject
	{
		public class ConstraintInfo1 : BulletDisposableObject
		{
			public ConstraintInfo1()
			{
				IntPtr native = btTypedConstraint_btConstraintInfo1_new();
				InitializeUserOwned(native);
			}

			public int Nub
			{
				get => btTypedConstraint_btConstraintInfo1_getNub(Native);
				set => btTypedConstraint_btConstraintInfo1_setNub(Native, value);
			}

			public int NumConstraintRows
			{
				get => btTypedConstraint_btConstraintInfo1_getNumConstraintRows(Native);
				set => btTypedConstraint_btConstraintInfo1_setNumConstraintRows(Native, value);
			}

			protected override void Dispose(bool disposing)
			{
				btTypedConstraint_btConstraintInfo1_delete(Native);
			}
		}

		public class ConstraintInfo2 : BulletDisposableObject
		{
			public ConstraintInfo2()
			{
				IntPtr native = btTypedConstraint_btConstraintInfo2_new();
				InitializeUserOwned(native);
			}
			/*
			public float Cfm
			{
				get { return btTypedConstraint_btConstraintInfo2_getCfm(Native); }
				set { btTypedConstraint_btConstraintInfo2_setCfm(Native, value.Native); }
			}

			public float ConstraintError
			{
				get { return btTypedConstraint_btConstraintInfo2_getConstraintError(Native); }
				set { btTypedConstraint_btConstraintInfo2_setConstraintError(Native, value.Native); }
			}
			*/
			public float Damping
			{
				get => btTypedConstraint_btConstraintInfo2_getDamping(Native);
				set => btTypedConstraint_btConstraintInfo2_setDamping(Native, value);
			}

			public float Erp
			{
				get => btTypedConstraint_btConstraintInfo2_getErp(Native);
				set => btTypedConstraint_btConstraintInfo2_setErp(Native, value);
			}
			/*
			public int Findex
			{
				get { return btTypedConstraint_btConstraintInfo2_getFindex(Native); }
				set { btTypedConstraint_btConstraintInfo2_setFindex(Native, value.Native); }
			}
			*/
			public float Fps
			{
				get => btTypedConstraint_btConstraintInfo2_getFps(Native);
				set => btTypedConstraint_btConstraintInfo2_setFps(Native, value);
			}
			/*
			public float J1angularAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ1angularAxis(Native); }
				set { btTypedConstraint_btConstraintInfo2_setJ1angularAxis(Native, value.Native); }
			}

			public float J1linearAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ1linearAxis(Native); }
				set { btTypedConstraint_btConstraintInfo2_setJ1linearAxis(Native, value.Native); }
			}

			public float J2angularAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ2angularAxis(Native); }
				set { btTypedConstraint_btConstraintInfo2_setJ2angularAxis(Native, value.Native); }
			}

			public float J2linearAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ2linearAxis(Native); }
				set { btTypedConstraint_btConstraintInfo2_setJ2linearAxis(Native, value.Native); }
			}

			public float LowerLimit
			{
				get { return btTypedConstraint_btConstraintInfo2_getLowerLimit(Native); }
				set { btTypedConstraint_btConstraintInfo2_setLowerLimit(Native, value.Native); }
			}
			*/
			public int NumIterations
			{
				get => btTypedConstraint_btConstraintInfo2_getNumIterations(Native);
				set => btTypedConstraint_btConstraintInfo2_setNumIterations(Native, value);
			}

			public int Rowskip
			{
				get => btTypedConstraint_btConstraintInfo2_getRowskip(Native);
				set => btTypedConstraint_btConstraintInfo2_setRowskip(Native, value);
			}
			/*
			public float UpperLimit
			{
				get { return btTypedConstraint_btConstraintInfo2_getUpperLimit(Native); }
				set { btTypedConstraint_btConstraintInfo2_setUpperLimit(Native, value.Native); }
			}
			*/
			protected override void Dispose(bool disposing)
			{
				btTypedConstraint_btConstraintInfo2_delete(Native);
			}
		}

		private JointFeedback _jointFeedback;

		private static RigidBody _fixedBody;

		protected internal TypedConstraint()
		{
		}

		protected internal void InitializeMembers(RigidBody rigidBodyA, RigidBody rigidBodyB)
		{
			RigidBodyA = rigidBodyA;
			RigidBodyB = rigidBodyB;
		}

		public void BuildJacobian()
		{
			btTypedConstraint_buildJacobian(Native);
		}

		public int CalculateSerializeBufferSize()
		{
			return btTypedConstraint_calculateSerializeBufferSize(Native);
		}

		public void EnableFeedback(bool needsFeedback)
		{
			btTypedConstraint_enableFeedback(Native, needsFeedback);
		}

		public static RigidBody GetFixedBody()
		{
			if (_fixedBody == null)
			{
				_fixedBody = new RigidBody(btTypedConstraint_getFixedBody());
			}
			return _fixedBody;
		}

		public void GetInfo1(ConstraintInfo1 info)
		{
			btTypedConstraint_getInfo1(Native, info.Native);
		}

		public void GetInfo2(ConstraintInfo2 info)
		{
			btTypedConstraint_getInfo2(Native, info.Native);
		}

		public float GetParam(ConstraintParam num)
		{
			return btTypedConstraint_getParam(Native, num);
		}

		public float GetParam(ConstraintParam num, int axis)
		{
			return btTypedConstraint_getParam2(Native, num, axis);
		}

		public float InternalGetAppliedImpulse()
		{
			return btTypedConstraint_internalGetAppliedImpulse(Native);
		}

		public void InternalSetAppliedImpulse(float appliedImpulse)
		{
			btTypedConstraint_internalSetAppliedImpulse(Native, appliedImpulse);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btTypedConstraint_serialize(Native, dataBuffer, serializer.Native));
		}

		public void SetParam(ConstraintParam num, float value)
		{
			btTypedConstraint_setParam(Native, num, value);
		}

		public void SetParam(ConstraintParam num, float value, int axis)
		{
			btTypedConstraint_setParam2(Native, num, value, axis);
		}
		/*
		public void SetupSolverConstraint(btAlignedObjectArray<btSolverConstraint> ca,
			int solverBodyA, int solverBodyB, float timeStep)
		{
			btTypedConstraint_setupSolverConstraint(_native, ca._native, solverBodyA,
				solverBodyB, timeStep);
		}

		public void SolveConstraintObsolete(SolverBody __unnamed0, SolverBody __unnamed1,
			float __unnamed2)
		{
			btTypedConstraint_solveConstraintObsolete(_native, __unnamed0._native,
				__unnamed1._native, __unnamed2);
		}
		*/
		public float AppliedImpulse => btTypedConstraint_getAppliedImpulse(Native);

		public float BreakingImpulseThreshold
		{
			get => btTypedConstraint_getBreakingImpulseThreshold(Native);
			set => btTypedConstraint_setBreakingImpulseThreshold(Native, value);
		}

		public TypedConstraintType ConstraintType => btTypedConstraint_getConstraintType(Native);

		public float DebugDrawSize
		{
			get => btTypedConstraint_getDbgDrawSize(Native);
			set => btTypedConstraint_setDbgDrawSize(Native, value);
		}

		public bool IsEnabled
		{
			get => btTypedConstraint_isEnabled(Native);
			set => btTypedConstraint_setEnabled(Native, value);
		}

		public JointFeedback JointFeedback
		{
			get => _jointFeedback;
			set
			{
				btTypedConstraint_setJointFeedback(Native, value != null ? value.Native : IntPtr.Zero);
				_jointFeedback = value;
			}
		}

		public bool NeedsFeedback => btTypedConstraint_needsFeedback(Native);

		public int OverrideNumSolverIterations
		{
			get => btTypedConstraint_getOverrideNumSolverIterations(Native);
			set => btTypedConstraint_setOverrideNumSolverIterations(Native, value);
		}

		public RigidBody RigidBodyA { get; private set; }

		public RigidBody RigidBodyB { get; private set; }

		public int Uid => btTypedConstraint_getUid(Native);

		public int UserConstraintId
		{
			get => btTypedConstraint_getUserConstraintId(Native);
			set => btTypedConstraint_setUserConstraintId(Native, value);
		}

		public object Userobject { get; set; }

		public int UserConstraintType
		{
			get => btTypedConstraint_getUserConstraintType(Native);
			set => btTypedConstraint_setUserConstraintType(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			btTypedConstraint_delete(Native);
		}
	}

	public class AngularLimit : BulletDisposableObject
	{
		public AngularLimit()
		{
			IntPtr native = btAngularLimit_new();
			InitializeUserOwned(native);
		}

		public void Fit(ref float angle)
		{
			btAngularLimit_fit(Native, ref angle);
		}

		public void Set(float low, float high, float softness = 0.9f, float biasFactor = 0.3f,
			float relaxationFactor = 1.0f)
		{
			btAngularLimit_set(Native, low, high, softness, biasFactor, relaxationFactor);
		}

		public void Test(float angle)
		{
			btAngularLimit_test(Native, angle);
		}

		public float BiasFactor => btAngularLimit_getBiasFactor(Native);

		public float Correction => btAngularLimit_getCorrection(Native);

		public float Error => btAngularLimit_getError(Native);

		public float HalfRange => btAngularLimit_getHalfRange(Native);

		public float High => btAngularLimit_getHigh(Native);

		public bool IsLimit => btAngularLimit_isLimit(Native);

		public float Low => btAngularLimit_getLow(Native);

		public float RelaxationFactor => btAngularLimit_getRelaxationFactor(Native);

		public float Sign => btAngularLimit_getSign(Native);

		public float Softness => btAngularLimit_getSoftness(Native);

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btAngularLimit_delete(Native);
			}
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct TypedConstraintFloatData
	{
		public IntPtr RigidBodyA;
		public IntPtr RigidBodyB;
		public IntPtr Name;
		public int ObjectType;
		public int UserConstraintType;
		public int UserConstraintId;
		public int NeedsFeedback;
		public float AppliedImpulse;
		public float DebugDrawSize;
		public int DisableCollisionsBetweenLinkedBodies;
		public int OverrideNumSolverIterations;
		public float BreakingImpulseThreshold;
		public int IsEnabled;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(TypedConstraintFloatData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct TypedConstraintDoubleData
	{
		public IntPtr RigidBodyA;
		public IntPtr RigidBodyB;
		public IntPtr Name;
		public int ObjectType;
		public int UserConstraintType;
		public int UserConstraintId;
		public int NeedsFeedback;
		public double AppliedImpulse;
		public double DebugDrawSize;
		public int DisableCollisionsBetweenLinkedBodies;
		public int OverrideNumSolverIterations;
		public double BreakingImpulseThreshold;
		public int IsEnabled;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(TypedConstraintDoubleData), fieldName).ToInt32(); }
	}
}
