using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class DiscreteDynamicsWorldMultiThreaded : DiscreteDynamicsWorld
	{
		public DiscreteDynamicsWorldMultiThreaded(Dispatcher dispatcher, BroadphaseInterface pairCache,
			ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
			: base(btDiscreteDynamicsWorldMt_new(dispatcher != null ? dispatcher._native : IntPtr.Zero, pairCache != null ? pairCache._native : IntPtr.Zero,
				constraintSolver != null ? constraintSolver._native : IntPtr.Zero, collisionConfiguration != null ? collisionConfiguration._native : IntPtr.Zero),
				dispatcher, pairCache)
		{
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btDiscreteDynamicsWorldMt_new(IntPtr dispatcher, IntPtr pairCache, IntPtr constraintSolver, IntPtr collisionConfiguration);
	}
}
