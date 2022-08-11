using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class DiscreteCollisionDetectorInterface : BulletDisposableObject
	{
		public class ClosestPointInput : BulletDisposableObject
		{
			public ClosestPointInput()
			{
				IntPtr native = btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
				InitializeUserOwned(native);
			}

			public float MaximumDistanceSquared
			{
				get => btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(Native);
				set => btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(Native, value);
			}

			public Matrix4x4 TransformA
			{
				get
				{
					Matrix4x4 value;
					btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(Native, out value);
					return value;
				}
				set => btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(Native, ref value);
			}

			public Matrix4x4 TransformB
			{
				get
				{
					Matrix4x4 value;
					btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(Native, out value);
					return value;
				}
				set => btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(Native, ref value);
			}

			protected override void Dispose(bool disposing)
			{
				btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(Native);
			}
		}

		public abstract class Result : BulletDisposableObject
		{
			protected internal Result()
			{
			}

			public void AddContactPoint(Vector3 normalOnBInWorld, Vector3 pointInWorld,
				float depth)
			{
				btDiscreteCollisionDetectorInterface_Result_addContactPoint(Native,
					ref normalOnBInWorld, ref pointInWorld, depth);
			}

			public void SetShapeIdentifiersA(int partId0, int index0)
			{
				btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(
					Native, partId0, index0);
			}

			public void SetShapeIdentifiersB(int partId1, int index1)
			{
				btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(
					Native, partId1, index1);
			}

			protected override void Dispose(bool disposing)
			{
				btDiscreteCollisionDetectorInterface_Result_delete(Native);
			}
		}

		protected internal DiscreteCollisionDetectorInterface()
		{
		}

		public void GetClosestPoints(ClosestPointInput input, Result output, DebugDraw debugDraw,
			bool swapResults = false)
		{
			btDiscreteCollisionDetectorInterface_getClosestPoints(Native, input.Native,
				output.Native, debugDraw != null ? debugDraw.Native : IntPtr.Zero, swapResults);
		}

		protected override void Dispose(bool disposing)
		{
			btDiscreteCollisionDetectorInterface_delete(Native);
		}
	}

	public abstract class StorageResult : DiscreteCollisionDetectorInterface.Result
	{
		internal StorageResult() // public
		{
			//IntPtr native = btStorageResultWrapper_new();
			//InitializeUserOwned(native);
		}

		public Vector3 ClosestPointInB
		{
			get
			{
				Vector3 value;
				btStorageResult_getClosestPointInB(Native, out value);
				return value;
			}
			set => btStorageResult_setClosestPointInB(Native, ref value);
		}

		public float Distance
		{
			get => btStorageResult_getDistance(Native);
			set => btStorageResult_setDistance(Native, value);
		}

		public Vector3 NormalOnSurfaceB
		{
			get
			{
				Vector3 value;
				btStorageResult_getNormalOnSurfaceB(Native, out value);
				return value;
			}
			set => btStorageResult_setNormalOnSurfaceB(Native, ref value);
		}
	}
}
