using System;
using BulletSharp.Math;
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

		public DefaultMotionState(Matrix startTrans)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btDefaultMotionState_new2(ref startTrans);
			InitializeUserOwned(native);
		}

		public DefaultMotionState(Matrix startTrans, Matrix centerOfMassOffset)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btDefaultMotionState_new3(ref startTrans, ref centerOfMassOffset);
			InitializeUserOwned(native);
		}

		public override void GetWorldTransform(out Matrix worldTrans)
		{
			btMotionState_getWorldTransform(Native, out worldTrans);
		}

		public override void SetWorldTransform(ref Matrix worldTrans)
		{
			btMotionState_setWorldTransform(Native, ref worldTrans);
		}

		public Matrix CenterOfMassOffset
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getCenterOfMassOffset(Native, out value);
				return value;
			}
			set => btDefaultMotionState_setCenterOfMassOffset(Native, ref value);
		}

		public Matrix GraphicsWorldTrans
		{
			get
			{
				Matrix value;
				btDefaultMotionState_getGraphicsWorldTrans(Native, out value);
				return value;
			}
			set => btDefaultMotionState_setGraphicsWorldTrans(Native, ref value);
		}

		public Matrix StartWorldTrans
		{
			get
			{
				Matrix value;
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
