using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ResultExtensions
	{
        public unsafe static void AddContactPoint(this DiscreteCollisionDetectorInterface.Result obj, ref OpenTK.Vector3 normalOnBInWorld, ref OpenTK.Vector3 pointInWorld, float depth)
		{
			fixed (OpenTK.Vector3* normalOnBInWorldPtr = &normalOnBInWorld)
			{
				fixed (OpenTK.Vector3* pointInWorldPtr = &pointInWorld)
				{
					obj.AddContactPoint(*(BulletSharp.Math.Vector3*)normalOnBInWorldPtr, *(BulletSharp.Math.Vector3*)pointInWorldPtr, depth);
				}
			}
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ClosestPointInputExtensions
	{
		public unsafe static void GetTransformA(this DiscreteCollisionDetectorInterface.ClosestPointInput obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.TransformA;
			}
		}

        public static OpenTK.Matrix4 GetTransformA(this DiscreteCollisionDetectorInterface.ClosestPointInput obj)
		{
			OpenTK.Matrix4 value;
			GetTransformA(obj, out value);
			return value;
		}

        public unsafe static void GetTransformB(this DiscreteCollisionDetectorInterface.ClosestPointInput obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.TransformB;
			}
		}

        public static OpenTK.Matrix4 GetTransformB(this DiscreteCollisionDetectorInterface.ClosestPointInput obj)
		{
			OpenTK.Matrix4 value;
			GetTransformB(obj, out value);
			return value;
		}

        public unsafe static void SetTransformA(this DiscreteCollisionDetectorInterface.ClosestPointInput obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.TransformA = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

        public static void SetTransformA(this DiscreteCollisionDetectorInterface.ClosestPointInput obj, OpenTK.Matrix4 value)
		{
			SetTransformA(obj, ref value);
		}

        public unsafe static void SetTransformB(this DiscreteCollisionDetectorInterface.ClosestPointInput obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.TransformB = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

        public static void SetTransformB(this DiscreteCollisionDetectorInterface.ClosestPointInput obj, OpenTK.Matrix4 value)
		{
			SetTransformB(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StorageResultExtensions
	{
		public unsafe static void GetClosestPointInB(this StorageResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ClosestPointInB;
			}
		}

		public static OpenTK.Vector3 GetClosestPointInB(this StorageResult obj)
		{
			OpenTK.Vector3 value;
			GetClosestPointInB(obj, out value);
			return value;
		}

		public unsafe static void GetNormalOnSurfaceB(this StorageResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.NormalOnSurfaceB;
			}
		}

		public static OpenTK.Vector3 GetNormalOnSurfaceB(this StorageResult obj)
		{
			OpenTK.Vector3 value;
			GetNormalOnSurfaceB(obj, out value);
			return value;
		}

		public unsafe static void SetClosestPointInB(this StorageResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ClosestPointInB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetClosestPointInB(this StorageResult obj, OpenTK.Vector3 value)
		{
			SetClosestPointInB(obj, ref value);
		}

		public unsafe static void SetNormalOnSurfaceB(this StorageResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.NormalOnSurfaceB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetNormalOnSurfaceB(this StorageResult obj, OpenTK.Vector3 value)
		{
			SetNormalOnSurfaceB(obj, ref value);
		}
	}
}
