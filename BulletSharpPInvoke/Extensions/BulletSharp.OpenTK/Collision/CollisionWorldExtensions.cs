using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class LocalRayResultExtensions
	{
		public unsafe static void GetHitNormalLocal(this LocalRayResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitNormalLocal;
			}
		}

		public static OpenTK.Vector3 GetHitNormalLocal(this LocalRayResult obj)
		{
			OpenTK.Vector3 value;
			GetHitNormalLocal(obj, out value);
			return value;
		}

		public unsafe static void SetHitNormalLocal(this LocalRayResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitNormalLocal = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitNormalLocal(this LocalRayResult obj, OpenTK.Vector3 value)
		{
			SetHitNormalLocal(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ClosestRayResultCallbackExtensions
	{
		public unsafe static void GetHitNormalWorld(this ClosestRayResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitNormalWorld;
			}
		}

		public static OpenTK.Vector3 GetHitNormalWorld(this ClosestRayResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetHitNormalWorld(obj, out value);
			return value;
		}

		public unsafe static void GetHitPointWorld(this ClosestRayResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitPointWorld;
			}
		}

		public static OpenTK.Vector3 GetHitPointWorld(this ClosestRayResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetHitPointWorld(obj, out value);
			return value;
		}

		public unsafe static void GetRayFromWorld(this ClosestRayResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.RayFromWorld;
			}
		}

		public static OpenTK.Vector3 GetRayFromWorld(this ClosestRayResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetRayFromWorld(obj, out value);
			return value;
		}

		public unsafe static void GetRayToWorld(this ClosestRayResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.RayToWorld;
			}
		}

		public static OpenTK.Vector3 GetRayToWorld(this ClosestRayResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetRayToWorld(obj, out value);
			return value;
		}

		public unsafe static void SetHitNormalWorld(this ClosestRayResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitNormalWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitNormalWorld(this ClosestRayResultCallback obj, OpenTK.Vector3 value)
		{
			SetHitNormalWorld(obj, ref value);
		}

		public unsafe static void SetHitPointWorld(this ClosestRayResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitPointWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitPointWorld(this ClosestRayResultCallback obj, OpenTK.Vector3 value)
		{
			SetHitPointWorld(obj, ref value);
		}

		public unsafe static void SetRayFromWorld(this ClosestRayResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.RayFromWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetRayFromWorld(this ClosestRayResultCallback obj, OpenTK.Vector3 value)
		{
			SetRayFromWorld(obj, ref value);
		}

		public unsafe static void SetRayToWorld(this ClosestRayResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.RayToWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetRayToWorld(this ClosestRayResultCallback obj, OpenTK.Vector3 value)
		{
			SetRayToWorld(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AllHitsRayResultCallbackExtensions
	{
		public unsafe static void GetRayFromWorld(this AllHitsRayResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.RayFromWorld;
			}
		}

		public static OpenTK.Vector3 GetRayFromWorld(this AllHitsRayResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetRayFromWorld(obj, out value);
			return value;
		}

		public unsafe static void GetRayToWorld(this AllHitsRayResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.RayToWorld;
			}
		}

		public static OpenTK.Vector3 GetRayToWorld(this AllHitsRayResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetRayToWorld(obj, out value);
			return value;
		}

		public unsafe static void SetRayFromWorld(this AllHitsRayResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.RayFromWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetRayFromWorld(this AllHitsRayResultCallback obj, OpenTK.Vector3 value)
		{
			SetRayFromWorld(obj, ref value);
		}

		public unsafe static void SetRayToWorld(this AllHitsRayResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.RayToWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetRayToWorld(this AllHitsRayResultCallback obj, OpenTK.Vector3 value)
		{
			SetRayToWorld(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class LocalConvexResultExtensions
	{
		public unsafe static void GetHitNormalLocal(this LocalConvexResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitNormalLocal;
			}
		}

		public static OpenTK.Vector3 GetHitNormalLocal(this LocalConvexResult obj)
		{
			OpenTK.Vector3 value;
			GetHitNormalLocal(obj, out value);
			return value;
		}

		public unsafe static void GetHitPointLocal(this LocalConvexResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitPointLocal;
			}
		}

		public static OpenTK.Vector3 GetHitPointLocal(this LocalConvexResult obj)
		{
			OpenTK.Vector3 value;
			GetHitPointLocal(obj, out value);
			return value;
		}

		public unsafe static void SetHitNormalLocal(this LocalConvexResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitNormalLocal = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitNormalLocal(this LocalConvexResult obj, OpenTK.Vector3 value)
		{
			SetHitNormalLocal(obj, ref value);
		}

		public unsafe static void SetHitPointLocal(this LocalConvexResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitPointLocal = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitPointLocal(this LocalConvexResult obj, OpenTK.Vector3 value)
		{
			SetHitPointLocal(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ClosestConvexResultCallbackExtensions
	{
		public unsafe static void GetConvexFromWorld(this ClosestConvexResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ConvexFromWorld;
			}
		}

		public static OpenTK.Vector3 GetConvexFromWorld(this ClosestConvexResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetConvexFromWorld(obj, out value);
			return value;
		}

		public unsafe static void GetConvexToWorld(this ClosestConvexResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ConvexToWorld;
			}
		}

		public static OpenTK.Vector3 GetConvexToWorld(this ClosestConvexResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetConvexToWorld(obj, out value);
			return value;
		}

		public unsafe static void GetHitNormalWorld(this ClosestConvexResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitNormalWorld;
			}
		}

		public static OpenTK.Vector3 GetHitNormalWorld(this ClosestConvexResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetHitNormalWorld(obj, out value);
			return value;
		}

		public unsafe static void GetHitPointWorld(this ClosestConvexResultCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitPointWorld;
			}
		}

		public static OpenTK.Vector3 GetHitPointWorld(this ClosestConvexResultCallback obj)
		{
			OpenTK.Vector3 value;
			GetHitPointWorld(obj, out value);
			return value;
		}

		public unsafe static void SetConvexFromWorld(this ClosestConvexResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ConvexFromWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetConvexFromWorld(this ClosestConvexResultCallback obj, OpenTK.Vector3 value)
		{
			SetConvexFromWorld(obj, ref value);
		}

		public unsafe static void SetConvexToWorld(this ClosestConvexResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ConvexToWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetConvexToWorld(this ClosestConvexResultCallback obj, OpenTK.Vector3 value)
		{
			SetConvexToWorld(obj, ref value);
		}

		public unsafe static void SetHitNormalWorld(this ClosestConvexResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitNormalWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitNormalWorld(this ClosestConvexResultCallback obj, OpenTK.Vector3 value)
		{
			SetHitNormalWorld(obj, ref value);
		}

		public unsafe static void SetHitPointWorld(this ClosestConvexResultCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitPointWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitPointWorld(this ClosestConvexResultCallback obj, OpenTK.Vector3 value)
		{
			SetHitPointWorld(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CollisionWorldExtensions
	{
		public unsafe static void ConvexSweepTest(this CollisionWorld obj, ConvexShape castShape, ref OpenTK.Matrix4 from, ref OpenTK.Matrix4 to, ConvexResultCallback resultCallback, float allowedCcdPenetration)
		{
			fixed (OpenTK.Matrix4* fromPtr = &from)
			{
				fixed (OpenTK.Matrix4* toPtr = &to)
				{
					obj.ConvexSweepTest(castShape, ref *(BulletSharp.Math.Matrix*)fromPtr, ref *(BulletSharp.Math.Matrix*)toPtr, resultCallback, allowedCcdPenetration);
				}
			}
		}

		public unsafe static void ConvexSweepTest(this CollisionWorld obj, ConvexShape castShape, ref OpenTK.Matrix4 from, ref OpenTK.Matrix4 to, ConvexResultCallback resultCallback)
		{
			fixed (OpenTK.Matrix4* fromPtr = &from)
			{
				fixed (OpenTK.Matrix4* toPtr = &to)
				{
					obj.ConvexSweepTest(castShape, ref *(BulletSharp.Math.Matrix*)fromPtr, ref *(BulletSharp.Math.Matrix*)toPtr, resultCallback);
				}
			}
		}

		public unsafe static void DebugDrawObject(this CollisionWorld obj, ref OpenTK.Matrix4 worldTransform, CollisionShape shape, ref OpenTK.Vector3 color)
		{
			fixed (OpenTK.Matrix4* worldTransformPtr = &worldTransform)
			{
				fixed (OpenTK.Vector3* colorPtr = &color)
				{
					obj.DebugDrawObject(ref *(BulletSharp.Math.Matrix*)worldTransformPtr, shape, ref *(BulletSharp.Math.Vector3*)colorPtr);
				}
			}
		}

		public unsafe static void RayTest(this CollisionWorld obj, ref OpenTK.Vector3 rayFromWorld, ref OpenTK.Vector3 rayToWorld, RayResultCallback resultCallback)
		{
			fixed (OpenTK.Vector3* rayFromWorldPtr = &rayFromWorld)
			{
				fixed (OpenTK.Vector3* rayToWorldPtr = &rayToWorld)
				{
					obj.RayTest(ref *(BulletSharp.Math.Vector3*)rayFromWorldPtr, ref *(BulletSharp.Math.Vector3*)rayToWorldPtr, resultCallback);
				}
			}
		}
	}
}
