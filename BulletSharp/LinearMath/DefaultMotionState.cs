using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class DefaultMotionState : MotionState
	{
		public DefaultMotionState()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btDefaultMotionState_new();
			InitializeUserOwned(native);
		}

		public DefaultMotionState(Matrix4x4 startTrans)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btDefaultMotionState_new2(ref startTrans);
			InitializeUserOwned(native);
		}

		public DefaultMotionState(Matrix4x4 startTrans, Matrix4x4 centerOfMassOffset)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btDefaultMotionState_new3(ref startTrans, ref centerOfMassOffset);
			InitializeUserOwned(native);
		}

		public override void GetWorldTransform(out Matrix4x4 worldTrans)
		{
			btMotionState_getWorldTransform(Native, out worldTrans);
		}

		public override void SetWorldTransform(ref Matrix4x4 worldTrans)
		{
			btMotionState_setWorldTransform(Native, ref worldTrans);
		}

		public Matrix4x4 CenterOfMassOffset
		{
			get
			{
				Matrix4x4 value;
				btDefaultMotionState_getCenterOfMassOffset(Native, out value);
				return value;
			}
			set => btDefaultMotionState_setCenterOfMassOffset(Native, ref value);
		}

		public Matrix4x4 GraphicsWorldTrans
		{
			get
			{
				Matrix4x4 value;
				btDefaultMotionState_getGraphicsWorldTrans(Native, out value);
				return value;
			}
			set => btDefaultMotionState_setGraphicsWorldTrans(Native, ref value);
		}

		public Matrix4x4 StartWorldTrans
		{
			get
			{
				Matrix4x4 value;
				btDefaultMotionState_getStartWorldTrans(Native, out value);
				return value;
			}
			set => btDefaultMotionState_setStartWorldTrans(Native, ref value);
		}

		public IntPtr UserPointer
		{
			get => btDefaultMotionState_getUserPointer(Native);
			set => btDefaultMotionState_setUserPointer(Native, value);
		}
	}
}
