using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class ConeTwistConstraint : TypedConstraint
	{
        public ConeTwistConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, ref Matrix rigidBodyAFrame, ref Matrix rigidBodyBFrame)
            : base(btConeTwistConstraint_new(rigidBodyA._native, rigidBodyB._native, ref rigidBodyAFrame, ref rigidBodyBFrame))
        {
            _rigidBodyA = rigidBodyA;
            _rigidBodyB = rigidBodyB;
        }

		public ConeTwistConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix rigidBodyAFrame, Matrix rigidBodyBFrame)
			: this(rigidBodyA, rigidBodyB, ref rigidBodyAFrame, ref rigidBodyBFrame)
		{
		}

        public ConeTwistConstraint(RigidBody rigidBodyA, ref Matrix rigidBodyAFrame)
            : base(btConeTwistConstraint_new2(rigidBodyA._native, ref rigidBodyAFrame))
        {
            _rigidBodyA = rigidBodyA;
            _rigidBodyB = FixedBody;
        }

		public ConeTwistConstraint(RigidBody rigidBodyA, Matrix rigidBodyAFrame)
            : this(rigidBodyA, ref rigidBodyAFrame)
		{
		}

		public void CalcAngleInfo()
		{
			btConeTwistConstraint_calcAngleInfo(_native);
		}

        public void CalcAngleInfo2(ref Matrix transA, ref Matrix transB, ref Matrix invInertiaWorldA, ref Matrix invInertiaWorldB)
        {
            btConeTwistConstraint_calcAngleInfo2(_native, ref transA, ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
        }

		public void CalcAngleInfo2(Matrix transA, Matrix transB, Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_calcAngleInfo2(_native, ref transA, ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public void EnableMotor(bool b)
		{
			btConeTwistConstraint_enableMotor(_native, b);
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btConeTwistConstraint_getInfo1NonVirtual(_native, info._native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, Matrix transA, Matrix transB, Matrix invInertiaWorldA, Matrix invInertiaWorldB)
		{
			btConeTwistConstraint_getInfo2NonVirtual(_native, info._native, ref transA, ref transB, ref invInertiaWorldA, ref invInertiaWorldB);
		}

		public Vector3 GetPointForAngle(float fAngleInRadians, float fLength)
		{
			Vector3 value;
			btConeTwistConstraint_GetPointForAngle(_native, fAngleInRadians, fLength, out value);
			return value;
		}

		public void SetAngularOnly(bool angularOnly)
		{
			btConeTwistConstraint_setAngularOnly(_native, angularOnly);
		}

		public void SetDamping(float damping)
		{
			btConeTwistConstraint_setDamping(_native, damping);
		}

        public void SetFrames(ref Matrix frameA, ref Matrix frameB)
        {
            btConeTwistConstraint_setFrames(_native, ref frameA, ref frameB);
        }

		public void SetFrames(Matrix frameA, Matrix frameB)
		{
			btConeTwistConstraint_setFrames(_native, ref frameA, ref frameB);
		}

		public void SetLimit(float swingSpan1, float swingSpan2, float twistSpan)
		{
			btConeTwistConstraint_setLimit(_native, swingSpan1, swingSpan2, twistSpan);
		}

		public void SetLimit(float swingSpan1, float swingSpan2, float twistSpan, float softness)
		{
			btConeTwistConstraint_setLimit2(_native, swingSpan1, swingSpan2, twistSpan, softness);
		}

		public void SetLimit(float swingSpan1, float swingSpan2, float twistSpan, float softness, float biasFactor)
		{
			btConeTwistConstraint_setLimit3(_native, swingSpan1, swingSpan2, twistSpan, softness, biasFactor);
		}

		public void SetLimit(float swingSpan1, float swingSpan2, float twistSpan, float softness, float biasFactor, float relaxationFactor)
		{
			btConeTwistConstraint_setLimit4(_native, swingSpan1, swingSpan2, twistSpan, softness, biasFactor, relaxationFactor);
		}

		public void SetLimit(int limitIndex, float limitValue)
		{
			btConeTwistConstraint_setLimit5(_native, limitIndex, limitValue);
		}

		public void SetMaxMotorImpulse(float maxMotorImpulse)
		{
			btConeTwistConstraint_setMaxMotorImpulse(_native, maxMotorImpulse);
		}

		public void SetMaxMotorImpulseNormalized(float maxMotorImpulse)
		{
			btConeTwistConstraint_setMaxMotorImpulseNormalized(_native, maxMotorImpulse);
		}

        public void SetMotorTarget(ref Quaternion q)
        {
            btConeTwistConstraint_setMotorTarget(_native, ref q);
        }

		public void SetMotorTarget(Quaternion q)
		{
			btConeTwistConstraint_setMotorTarget(_native, ref q);
		}

        public void SetMotorTargetInConstraintSpace(ref Quaternion q)
        {
            btConeTwistConstraint_setMotorTargetInConstraintSpace(_native, ref q);
        }

		public void SetMotorTargetInConstraintSpace(Quaternion q)
		{
			btConeTwistConstraint_setMotorTargetInConstraintSpace(_native, ref q);
		}

		public void UpdateRHS(float timeStep)
		{
			btConeTwistConstraint_updateRHS(_native, timeStep);
		}

		public Matrix AFrame
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getAFrame(_native, out value);
				return value;
			}
		}

		public Matrix BFrame
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getBFrame(_native, out value);
				return value;
			}
		}

		public float FixThresh
		{
			get { return btConeTwistConstraint_getFixThresh(_native); }
			set { btConeTwistConstraint_setFixThresh(_native, value); }
		}

		public Matrix FrameOffsetA
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getFrameOffsetA(_native, out value);
				return value;
			}
		}

		public Matrix FrameOffsetB
		{
			get
			{
				Matrix value;
				btConeTwistConstraint_getFrameOffsetB(_native, out value);
				return value;
			}
		}

		public bool IsPastSwingLimit
		{
			get { return btConeTwistConstraint_isPastSwingLimit(_native); }
		}

		public int SolveSwingLimit
		{
			get { return btConeTwistConstraint_getSolveSwingLimit(_native); }
		}

		public int SolveTwistLimit
		{
			get { return btConeTwistConstraint_getSolveTwistLimit(_native); }
		}

		public float SwingSpan1
		{
			get { return btConeTwistConstraint_getSwingSpan1(_native); }
		}

		public float SwingSpan2
		{
			get { return btConeTwistConstraint_getSwingSpan2(_native); }
		}

		public float TwistAngle
		{
			get { return btConeTwistConstraint_getTwistAngle(_native); }
		}

		public float TwistLimitSign
		{
			get { return btConeTwistConstraint_getTwistLimitSign(_native); }
		}

		public float TwistSpan
		{
			get { return btConeTwistConstraint_getTwistSpan(_native); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConeTwistConstraint_new(IntPtr rbA, IntPtr rbB, [In] ref Matrix rbAFrame, [In] ref Matrix rbBFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btConeTwistConstraint_new2(IntPtr rbA, [In] ref Matrix rbAFrame);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_calcAngleInfo(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_calcAngleInfo2(IntPtr obj, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Matrix invInertiaWorldA, [In] ref Matrix invInertiaWorldB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_enableMotor(IntPtr obj, bool b);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getAFrame(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getBFrame(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getFixThresh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getFrameOffsetA(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getFrameOffsetB(IntPtr obj, [Out] out Matrix value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getInfo1NonVirtual(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_getInfo2NonVirtual(IntPtr obj, IntPtr info, [In] ref Matrix transA, [In] ref Matrix transB, [In] ref Matrix invInertiaWorldA, [In] ref Matrix invInertiaWorldB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_GetPointForAngle(IntPtr obj, float fAngleInRadians, float fLength, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConeTwistConstraint_getSolveSwingLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btConeTwistConstraint_getSolveTwistLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getSwingSpan1(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getSwingSpan2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getTwistAngle(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getTwistLimitSign(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btConeTwistConstraint_getTwistSpan(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btConeTwistConstraint_isPastSwingLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setAngularOnly(IntPtr obj, bool angularOnly);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setDamping(IntPtr obj, float damping);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setFixThresh(IntPtr obj, float fixThresh);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setFrames(IntPtr obj, [In] ref Matrix frameA, [In] ref Matrix frameB);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit2(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit3(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness, float _biasFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit4(IntPtr obj, float _swingSpan1, float _swingSpan2, float _twistSpan, float _softness, float _biasFactor, float _relaxationFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setLimit5(IntPtr obj, int limitIndex, float limitValue);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setMaxMotorImpulse(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setMaxMotorImpulseNormalized(IntPtr obj, float maxMotorImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setMotorTarget(IntPtr obj, [In] ref Quaternion q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_setMotorTargetInConstraintSpace(IntPtr obj, [In] ref Quaternion q);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btConeTwistConstraint_updateRHS(IntPtr obj, float timeStep);
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct ConeTwistConstraintFloatData
    {
        public TypedConstraintFloatData TypedConstraintData;
        public TransformFloatData RigidBodyAFrame;
        public TransformFloatData RigidBodyBFrame;
        public float SwingSpan1;
        public float SwingSpan2;
        public float TwistSpan;
        public float LimitSoftness;
        public float BiasFactor;
        public float RelaxationFactor;
        public float Damping;
        public int Pad;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConeTwistConstraintFloatData), fieldName).ToInt32(); }
    }
}
