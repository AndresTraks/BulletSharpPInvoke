using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class MotionState : BulletDisposableObject
	{
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void GetWorldTransformUnmanagedDelegate(out Matrix4x4 worldTrans);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate void SetWorldTransformUnmanagedDelegate(ref Matrix4x4 worldTrans);

		private readonly GetWorldTransformUnmanagedDelegate _getWorldTransform;
		private readonly SetWorldTransformUnmanagedDelegate _setWorldTransform;

		internal MotionState(ConstructionInfo info)
		{
		}

		protected MotionState()
		{
			_getWorldTransform = new GetWorldTransformUnmanagedDelegate(GetWorldTransformUnmanaged);
			_setWorldTransform = new SetWorldTransformUnmanagedDelegate(SetWorldTransformUnmanaged);

			IntPtr native = btMotionStateWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_getWorldTransform),
				Marshal.GetFunctionPointerForDelegate(_setWorldTransform));
			InitializeUserOwned(native);
		}

		void GetWorldTransformUnmanaged(out Matrix4x4 worldTrans)
		{
			GetWorldTransform(out worldTrans);
		}

		void SetWorldTransformUnmanaged(ref Matrix4x4 worldTrans)
		{
			SetWorldTransform(ref worldTrans);
		}

		public abstract void GetWorldTransform(out Matrix4x4 worldTrans);
		public abstract void SetWorldTransform(ref Matrix4x4 worldTrans);

		public Matrix4x4 WorldTransform
		{
			get
			{
				Matrix4x4 transform;
				GetWorldTransform(out transform);
				return transform;
			}
			set => SetWorldTransform(ref value);
		}

		protected override void Dispose(bool disposing)
		{
			btMotionState_delete(Native);
		}
	}
}
