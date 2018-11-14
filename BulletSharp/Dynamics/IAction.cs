using System;
using System.Runtime.InteropServices;
using System.Security;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public interface IAction
	{
		void DebugDraw(DebugDraw debugDrawer);
		void UpdateAction(CollisionWorld collisionWorld, double deltaTimeStep);
	}

	internal class ActionInterfaceWrapper : BulletDisposableObject
	{
		private IAction _actionInterface;
		private readonly DynamicsWorld _world;

		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void DebugDrawUnmanagedDelegate(IntPtr debugDrawer);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void UpdateActionUnmanagedDelegate(IntPtr collisionWorld, double deltaTimeStep);

		private readonly DebugDrawUnmanagedDelegate _debugDraw;
		private readonly UpdateActionUnmanagedDelegate _updateAction;

		public ActionInterfaceWrapper(IAction actionInterface, DynamicsWorld world)
		{
			_debugDraw = new DebugDrawUnmanagedDelegate(DebugDrawUnmanaged);
			_updateAction = new UpdateActionUnmanagedDelegate(UpdateActionUnmanaged);

			IntPtr native = btActionInterfaceWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_debugDraw),
				Marshal.GetFunctionPointerForDelegate(_updateAction));
			InitializeUserOwned(native);

			_actionInterface = actionInterface;
			_world = world;
		}

		private void DebugDrawUnmanaged(IntPtr debugDrawer)
		{
			_actionInterface.DebugDraw(DebugDraw.GetManaged(debugDrawer));
		}

		private void UpdateActionUnmanaged(IntPtr collisionWorld, double deltaTimeStep)
		{
			_actionInterface.UpdateAction(_world, deltaTimeStep);
		}

		protected override void Dispose(bool disposing)
		{
			btActionInterface_delete(Native);
		}
	}
}
