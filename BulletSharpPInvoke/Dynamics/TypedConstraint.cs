using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
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
        Max
    }

    public enum ConstraintParam
    {
        Erp = 1,
        StopErp,
        Cfm,
        StopCfm
    }

	public class JointFeedback : IDisposable
	{
		internal IntPtr _native;

		internal JointFeedback(IntPtr native)
		{
			_native = native;
		}

		public JointFeedback()
		{
			_native = btJointFeedback_new();
		}

		public Vector3 AppliedForceBodyA
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedForceBodyA(_native, out value);
				return value;
			}
			set { btJointFeedback_setAppliedForceBodyA(_native, ref value); }
		}

		public Vector3 AppliedForceBodyB
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedForceBodyB(_native, out value);
				return value;
			}
			set { btJointFeedback_setAppliedForceBodyB(_native, ref value); }
		}

		public Vector3 AppliedTorqueBodyA
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedTorqueBodyA(_native, out value);
				return value;
			}
			set { btJointFeedback_setAppliedTorqueBodyA(_native, ref value); }
		}

		public Vector3 AppliedTorqueBodyB
		{
			get
			{
				Vector3 value;
				btJointFeedback_getAppliedTorqueBodyB(_native, out value);
				return value;
			}
			set { btJointFeedback_setAppliedTorqueBodyB(_native, ref value); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btJointFeedback_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~JointFeedback()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btJointFeedback_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_getAppliedForceBodyA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_getAppliedForceBodyB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_getAppliedTorqueBodyA(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_getAppliedTorqueBodyB(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_setAppliedForceBodyA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_setAppliedForceBodyB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_setAppliedTorqueBodyA(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_setAppliedTorqueBodyB(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btJointFeedback_delete(IntPtr obj);
	}

	public class TypedConstraint : IDisposable
	{
		public class ConstraintInfo1 : IDisposable
		{
			internal IntPtr _native;

			internal ConstraintInfo1(IntPtr native)
			{
				_native = native;
			}

			public ConstraintInfo1()
			{
				_native = btTypedConstraint_btConstraintInfo1_new();
			}

			public int Nub
			{
				get { return btTypedConstraint_btConstraintInfo1_getNub(_native); }
				set { btTypedConstraint_btConstraintInfo1_setNub(_native, value); }
			}

			public int NumConstraintRows
			{
				get { return btTypedConstraint_btConstraintInfo1_getNumConstraintRows(_native); }
				set { btTypedConstraint_btConstraintInfo1_setNumConstraintRows(_native, value); }
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_native != IntPtr.Zero)
				{
					btTypedConstraint_btConstraintInfo1_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~ConstraintInfo1()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo1_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btTypedConstraint_btConstraintInfo1_getNub(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btTypedConstraint_btConstraintInfo1_getNumConstraintRows(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo1_setNub(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo1_setNumConstraintRows(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo1_delete(IntPtr obj);
		}

		public class ConstraintInfo2 : IDisposable
		{
			internal IntPtr _native;

			internal ConstraintInfo2(IntPtr native)
			{
				_native = native;
			}

			public ConstraintInfo2()
			{
				_native = btTypedConstraint_btConstraintInfo2_new();
			}
            /*
			public float Cfm
			{
				get { return btTypedConstraint_btConstraintInfo2_getCfm(_native); }
				set { btTypedConstraint_btConstraintInfo2_setCfm(_native, value._native); }
			}

			public float ConstraintError
			{
				get { return btTypedConstraint_btConstraintInfo2_getConstraintError(_native); }
				set { btTypedConstraint_btConstraintInfo2_setConstraintError(_native, value._native); }
			}
            */
			public float Damping
			{
				get { return btTypedConstraint_btConstraintInfo2_getDamping(_native); }
				set { btTypedConstraint_btConstraintInfo2_setDamping(_native, value); }
			}

			public float Erp
			{
				get { return btTypedConstraint_btConstraintInfo2_getErp(_native); }
				set { btTypedConstraint_btConstraintInfo2_setErp(_native, value); }
			}
            /*
			public int Findex
			{
				get { return btTypedConstraint_btConstraintInfo2_getFindex(_native); }
				set { btTypedConstraint_btConstraintInfo2_setFindex(_native, value._native); }
			}
            */
			public float Fps
			{
				get { return btTypedConstraint_btConstraintInfo2_getFps(_native); }
				set { btTypedConstraint_btConstraintInfo2_setFps(_native, value); }
			}
            /*
			public float J1angularAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ1angularAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ1angularAxis(_native, value._native); }
			}

			public float J1linearAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ1linearAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ1linearAxis(_native, value._native); }
			}

			public float J2angularAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ2angularAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ2angularAxis(_native, value._native); }
			}

			public float J2linearAxis
			{
				get { return btTypedConstraint_btConstraintInfo2_getJ2linearAxis(_native); }
				set { btTypedConstraint_btConstraintInfo2_setJ2linearAxis(_native, value._native); }
			}

			public float LowerLimit
			{
				get { return btTypedConstraint_btConstraintInfo2_getLowerLimit(_native); }
				set { btTypedConstraint_btConstraintInfo2_setLowerLimit(_native, value._native); }
			}
            */
			public int NumIterations
			{
				get { return btTypedConstraint_btConstraintInfo2_getNumIterations(_native); }
				set { btTypedConstraint_btConstraintInfo2_setNumIterations(_native, value); }
			}

			public int Rowskip
			{
				get { return btTypedConstraint_btConstraintInfo2_getRowskip(_native); }
				set { btTypedConstraint_btConstraintInfo2_setRowskip(_native, value); }
			}
            /*
			public float UpperLimit
			{
				get { return btTypedConstraint_btConstraintInfo2_getUpperLimit(_native); }
				set { btTypedConstraint_btConstraintInfo2_setUpperLimit(_native, value._native); }
			}
            */
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_native != IntPtr.Zero)
				{
					btTypedConstraint_btConstraintInfo2_delete(_native);
					_native = IntPtr.Zero;
				}
			}

			~ConstraintInfo2()
			{
				Dispose(false);
			}

			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_new();
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getCfm(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getConstraintError(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btTypedConstraint_btConstraintInfo2_getDamping(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btTypedConstraint_btConstraintInfo2_getErp(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getFindex(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern float btTypedConstraint_btConstraintInfo2_getFps(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ1angularAxis(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ1linearAxis(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ2angularAxis(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getJ2linearAxis(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getLowerLimit(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btTypedConstraint_btConstraintInfo2_getNumIterations(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern int btTypedConstraint_btConstraintInfo2_getRowskip(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern IntPtr btTypedConstraint_btConstraintInfo2_getUpperLimit(IntPtr obj);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setCfm(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setConstraintError(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setDamping(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setErp(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setFindex(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setFps(IntPtr obj, float value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setJ1angularAxis(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setJ1linearAxis(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setJ2angularAxis(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setJ2linearAxis(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setLowerLimit(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setNumIterations(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setRowskip(IntPtr obj, int value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_setUpperLimit(IntPtr obj, IntPtr value);
			[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
			static extern void btTypedConstraint_btConstraintInfo2_delete(IntPtr obj);
		}

        internal IntPtr _native;

        private static RigidBody _fixedBody;

        protected RigidBody _rigidBodyA;
        protected RigidBody _rigidBodyB;

        internal static TypedConstraint GetManaged(IntPtr native)
        {
            if (native == IntPtr.Zero)
            {
                return null;
            }

            IntPtr handlePtr = btTypedConstraint_getUserConstraintPtr(native);
            return GCHandle.FromIntPtr(handlePtr).Target as TypedConstraint;
        }

		internal TypedConstraint(IntPtr native)
		{
            _native = native;

            if (btTypedConstraint_getUserConstraintId(_native) != -1)
            {
                throw new InvalidOperationException();
            }

            GCHandle handle = GCHandle.Alloc(this, GCHandleType.Weak);
            btTypedConstraint_setUserConstraintPtr(_native, GCHandle.ToIntPtr(handle));
		}

		public void BuildJacobian()
		{
			btTypedConstraint_buildJacobian(_native);
		}

		public int CalculateSerializeBufferSize()
		{
			return btTypedConstraint_calculateSerializeBufferSize(_native);
		}

		public void EnableFeedback(bool needsFeedback)
		{
			btTypedConstraint_enableFeedback(_native, needsFeedback);
		}

		public void GetInfo1(ConstraintInfo1 info)
		{
			btTypedConstraint_getInfo1(_native, info._native);
		}

		public void GetInfo2(ConstraintInfo2 info)
		{
			btTypedConstraint_getInfo2(_native, info._native);
		}

        public float GetParam(ConstraintParam num)
		{
			return btTypedConstraint_getParam(_native, num);
		}

        public float GetParam(ConstraintParam num, int axis)
		{
			return btTypedConstraint_getParam2(_native, num, axis);
		}

		public float InternalGetAppliedImpulse()
		{
			return btTypedConstraint_internalGetAppliedImpulse(_native);
		}

		public void InternalSetAppliedImpulse(float appliedImpulse)
		{
			btTypedConstraint_internalSetAppliedImpulse(_native, appliedImpulse);
		}

		public bool NeedsFeedback()
		{
			return btTypedConstraint_needsFeedback(_native);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btTypedConstraint_serialize(_native, dataBuffer, serializer._native));
		}

        public void SetParam(ConstraintParam num, float value)
		{
			btTypedConstraint_setParam(_native, num, value);
		}

        public void SetParam(ConstraintParam num, float value, int axis)
		{
			btTypedConstraint_setParam2(_native, num, value, axis);
		}
        /*
		public void SetupSolverConstraint(ConstraintArray ca, int solverBodyA, int solverBodyB, float timeStep)
		{
			btTypedConstraint_setupSolverConstraint(_native, ca._native, solverBodyA, solverBodyB, timeStep);
		}

		public void SolveConstraintObsolete(SolverBody __unnamed0, SolverBody __unnamed1, float __unnamed2)
		{
			btTypedConstraint_solveConstraintObsolete(_native, __unnamed0._native, __unnamed1._native, __unnamed2);
		}
        */
		public float AppliedImpulse
		{
			get { return btTypedConstraint_getAppliedImpulse(_native); }
		}

		public float BreakingImpulseThreshold
		{
			get { return btTypedConstraint_getBreakingImpulseThreshold(_native); }
			set { btTypedConstraint_setBreakingImpulseThreshold(_native, value); }
		}

		public TypedConstraintType ConstraintType
		{
			get { return btTypedConstraint_getConstraintType(_native); }
		}

		public float DebugDrawSize
		{
			get { return btTypedConstraint_getDbgDrawSize(_native); }
			set { btTypedConstraint_setDbgDrawSize(_native, value); }
		}

		public static RigidBody FixedBody
		{
            get
            {
                if (_fixedBody == null)
                {
                    _fixedBody = new RigidBody(btTypedConstraint_getFixedBody());
                    _fixedBody._preventDelete = true;
                }
                return _fixedBody;
            }
		}

		public bool IsEnabled
		{
			get { return btTypedConstraint_isEnabled(_native); }
			set { btTypedConstraint_setEnabled(_native, value); }
		}

		public JointFeedback JointFeedback
		{
            get
            {
                IntPtr ptr = btTypedConstraint_getJointFeedback(_native);
                return (ptr != IntPtr.Zero) ? new JointFeedback(ptr) : null;
            }
			set { btTypedConstraint_setJointFeedback(_native, value._native); }
		}

		public int OverrideNumSolverIterations
		{
			get { return btTypedConstraint_getOverrideNumSolverIterations(_native); }
			set { btTypedConstraint_setOverrideNumSolverIterations(_native, value); }
		}

		public RigidBody RigidBodyA
		{
            get { return CollisionObject.GetManaged(btTypedConstraint_getRigidBodyA(_native)) as RigidBody; }
		}

		public RigidBody RigidBodyB
		{
            get { return CollisionObject.GetManaged(btTypedConstraint_getRigidBodyB(_native)) as RigidBody; }
		}

		public int Uid
		{
			get { return btTypedConstraint_getUid(_native); }
		}

		public int UserConstraintId
		{
			get { return btTypedConstraint_getUserConstraintId(_native); }
			set { btTypedConstraint_setUserConstraintId(_native, value); }
		}

        public Object Userobject { get; set; }

		public int UserConstraintType
		{
			get { return btTypedConstraint_getUserConstraintType(_native); }
			set { btTypedConstraint_setUserConstraintType(_native, value); }
		}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                if (btTypedConstraint_getUserConstraintId(_native) != -1)
                {
                    IntPtr handlePtr = btTypedConstraint_getUserConstraintPtr(_native);
                    GCHandle.FromIntPtr(handlePtr).Free();
                    btTypedConstraint_delete(_native);
                }
                _native = IntPtr.Zero;
            }
        }

        ~TypedConstraint()
        {
            Dispose(false);
        }

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_buildJacobian(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTypedConstraint_calculateSerializeBufferSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_enableFeedback(IntPtr obj, bool needsFeedback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTypedConstraint_getAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTypedConstraint_getBreakingImpulseThreshold(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern TypedConstraintType btTypedConstraint_getConstraintType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTypedConstraint_getDbgDrawSize(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTypedConstraint_getFixedBody();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_getInfo1(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_getInfo2(IntPtr obj, IntPtr info);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTypedConstraint_getJointFeedback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTypedConstraint_getOverrideNumSolverIterations(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btTypedConstraint_getParam(IntPtr obj, ConstraintParam num);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern float btTypedConstraint_getParam2(IntPtr obj, ConstraintParam num, int axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTypedConstraint_getRigidBodyA(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTypedConstraint_getRigidBodyB(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTypedConstraint_getUid(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTypedConstraint_getUserConstraintId(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTypedConstraint_getUserConstraintPtr(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btTypedConstraint_getUserConstraintType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btTypedConstraint_internalGetAppliedImpulse(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_internalSetAppliedImpulse(IntPtr obj, float appliedImpulse);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btTypedConstraint_isEnabled(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btTypedConstraint_needsFeedback(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btTypedConstraint_serialize(IntPtr obj, IntPtr dataBuffer, IntPtr serializer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setBreakingImpulseThreshold(IntPtr obj, float threshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setDbgDrawSize(IntPtr obj, float dbgDrawSize);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setEnabled(IntPtr obj, bool enabled);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setJointFeedback(IntPtr obj, IntPtr jointFeedback);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setOverrideNumSolverIterations(IntPtr obj, int overideNumIterations);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setParam(IntPtr obj, ConstraintParam num, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_setParam2(IntPtr obj, ConstraintParam num, float value, int axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setupSolverConstraint(IntPtr obj, IntPtr ca, int solverBodyA, int solverBodyB, float timeStep);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setUserConstraintId(IntPtr obj, int uid);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setUserConstraintPtr(IntPtr obj, IntPtr ptr);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_setUserConstraintType(IntPtr obj, int userConstraintType);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btTypedConstraint_solveConstraintObsolete(IntPtr obj, IntPtr __unnamed0, IntPtr __unnamed1, float __unnamed2);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        static extern void btTypedConstraint_delete(IntPtr obj);
	}

	public class AngularLimit : IDisposable
	{
		internal IntPtr _native;

		internal AngularLimit(IntPtr native)
		{
			_native = native;
		}

		public AngularLimit()
		{
			_native = btAngularLimit_new();
		}

		public void Fit(ref float angle)
		{
			btAngularLimit_fit(_native, ref angle);
		}

		public void Set(float low, float high)
		{
			btAngularLimit_set(_native, low, high);
		}

		public void Set(float low, float high, float softness)
		{
			btAngularLimit_set2(_native, low, high, softness);
		}

		public void Set(float low, float high, float softness, float biasFactor)
		{
			btAngularLimit_set3(_native, low, high, softness, biasFactor);
		}

		public void Set(float low, float high, float softness, float biasFactor, float relaxationFactor)
		{
			btAngularLimit_set4(_native, low, high, softness, biasFactor, relaxationFactor);
		}

		public void Test(float angle)
		{
			btAngularLimit_test(_native, angle);
		}

		public float BiasFactor
		{
			get { return btAngularLimit_getBiasFactor(_native); }
		}

		public float Correction
		{
			get { return btAngularLimit_getCorrection(_native); }
		}

		public float Error
		{
			get { return btAngularLimit_getError(_native); }
		}

		public float HalfRange
		{
			get { return btAngularLimit_getHalfRange(_native); }
		}

		public float High
		{
			get { return btAngularLimit_getHigh(_native); }
		}

		public bool IsLimit
		{
			get { return btAngularLimit_isLimit(_native); }
		}

		public float Low
		{
			get { return btAngularLimit_getLow(_native); }
		}

		public float RelaxationFactor
		{
			get { return btAngularLimit_getRelaxationFactor(_native); }
		}

		public float Sign
		{
			get { return btAngularLimit_getSign(_native); }
		}

		public float Softness
		{
			get { return btAngularLimit_getSoftness(_native); }
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_native != IntPtr.Zero)
			{
				btAngularLimit_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~AngularLimit()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btAngularLimit_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAngularLimit_fit(IntPtr obj, ref float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getBiasFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getCorrection(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getError(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getHalfRange(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getHigh(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getLow(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getRelaxationFactor(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getSign(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btAngularLimit_getSoftness(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool btAngularLimit_isLimit(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAngularLimit_set(IntPtr obj, float low, float high);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAngularLimit_set2(IntPtr obj, float low, float high, float _softness);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAngularLimit_set3(IntPtr obj, float low, float high, float _softness, float _biasFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAngularLimit_set4(IntPtr obj, float low, float high, float _softness, float _biasFactor, float _relaxationFactor);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAngularLimit_test(IntPtr obj, float angle);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btAngularLimit_delete(IntPtr obj);
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
}
