using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class SoftBodySolver : BulletDisposableObject
	{
		protected internal SoftBodySolver()
		{
		}

		public bool CheckInitialized()
		{
			return btSoftBodySolver_checkInitialized(Native);
		}

		public void CopyBackToSoftBodies(bool bMove = true)
		{
			btSoftBodySolver_copyBackToSoftBodies(Native, bMove);
		}
		/*
		public void Optimize(AlignedObjectArray softBodies, bool forceUpdate = false)
		{
			btSoftBodySolver_optimize(Native, softBodies._native, forceUpdate);
		}
		*/
		public void PredictMotion(float solverdt)
		{
			btSoftBodySolver_predictMotion(Native, solverdt);
		}
		/*
		public void ProcessCollision(SoftBody __unnamed0, CollisionObjectWrapper __unnamed1)
		{
			btSoftBodySolver_processCollision(Native, __unnamed0._native, __unnamed1._native);
		}

		public void ProcessCollision(SoftBody __unnamed0, SoftBody __unnamed1)
		{
			btSoftBodySolver_processCollision2(Native, __unnamed0._native, __unnamed1._native);
		}
		*/
		public void SolveConstraints(float solverdt)
		{
			btSoftBodySolver_solveConstraints(Native, solverdt);
		}

		public void UpdateSoftBodies()
		{
			btSoftBodySolver_updateSoftBodies(Native);
		}

		public int NumberOfPositionIterations
		{
			get => btSoftBodySolver_getNumberOfPositionIterations(Native);
			set => btSoftBodySolver_setNumberOfPositionIterations(Native, value);
		}

		public int NumberOfVelocityIterations
		{
			get => btSoftBodySolver_getNumberOfVelocityIterations(Native);
			set => btSoftBodySolver_setNumberOfVelocityIterations(Native, value);
		}
		/*
		public SolverTypes SolverType
		{
			get { return btSoftBodySolver_getSolverType(Native); }
		}
		*/
		public float TimeScale => btSoftBodySolver_getTimeScale(Native);

		protected override void Dispose(bool disposing)
		{
			btSoftBodySolver_delete(Native);
		}
	}
	/*
	public class SoftBodySolverOutput : IDisposable
	{
		internal IntPtr _native;

		internal SoftBodySolverOutput(IntPtr native)
		{
			_native = native;
		}

		public void CopySoftBodyToVertexBuffer(SoftBody softBody, VertexBufferDescriptor vertexBuffer)
		{
			btSoftBodySolverOutput_copySoftBodyToVertexBuffer(_native, softBody._native, vertexBuffer._native);
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
				btSoftBodySolverOutput_delete(_native);
				_native = IntPtr.Zero;
			}
		}

		~SoftBodySolverOutput()
		{
			Dispose(false);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolverOutput_copySoftBodyToVertexBuffer(IntPtr obj, IntPtr softBody, IntPtr vertexBuffer);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSoftBodySolverOutput_delete(IntPtr obj);
	}*/
}
