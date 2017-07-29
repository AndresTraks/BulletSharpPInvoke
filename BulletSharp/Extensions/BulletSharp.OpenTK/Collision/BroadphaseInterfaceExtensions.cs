using System;
using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BroadphaseRayCallbackExtensions
	{
		public unsafe static void GetRayDirectionInverse(this BroadphaseRayCallback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.RayDirectionInverse;
			}
		}

		public static OpenTK.Vector3 GetRayDirectionInverse(this BroadphaseRayCallback obj)
		{
			OpenTK.Vector3 value;
			GetRayDirectionInverse(obj, out value);
			return value;
		}

		public unsafe static void SetRayDirectionInverse(this BroadphaseRayCallback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.RayDirectionInverse = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetRayDirectionInverse(this BroadphaseRayCallback obj, OpenTK.Vector3 value)
		{
			SetRayDirectionInverse(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BroadphaseInterfaceExtensions
	{
		public unsafe static void AabbTest(this BroadphaseInterface obj, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax, BroadphaseAabbCallback callback)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.AabbTest(ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr, callback);
				}
			}
		}

		public unsafe static BroadphaseProxy CreateProxy(this BroadphaseInterface obj, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax, int shapeType, IntPtr userPtr, short collisionFilterGroup, short collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					return obj.CreateProxy(ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher, multiSapProxy);
				}
			}
		}

        public unsafe static void GetAabb(this BroadphaseInterface obj, BroadphaseProxy proxy, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
                    obj.GetAabb(proxy, out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}

        public unsafe static void GetBroadphaseAabb(this BroadphaseInterface obj, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
                    obj.GetBroadphaseAabb(out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}

		public unsafe static void RayTest(this BroadphaseInterface obj, ref OpenTK.Vector3 rayFrom, ref OpenTK.Vector3 rayTo, BroadphaseRayCallback rayCallback, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* rayFromPtr = &rayFrom)
			{
				fixed (OpenTK.Vector3* rayToPtr = &rayTo)
				{
					fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
					{
						fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
						{
							obj.RayTest(ref *(BulletSharp.Math.Vector3*)rayFromPtr, ref *(BulletSharp.Math.Vector3*)rayToPtr, rayCallback, ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr);
						}
					}
				}
			}
		}

		public unsafe static void RayTest(this BroadphaseInterface obj, ref OpenTK.Vector3 rayFrom, ref OpenTK.Vector3 rayTo, BroadphaseRayCallback rayCallback)
		{
			fixed (OpenTK.Vector3* rayFromPtr = &rayFrom)
			{
				fixed (OpenTK.Vector3* rayToPtr = &rayTo)
				{
					obj.RayTest(ref *(BulletSharp.Math.Vector3*)rayFromPtr, ref *(BulletSharp.Math.Vector3*)rayToPtr, rayCallback);
				}
			}
		}

		public unsafe static void SetAabb(this BroadphaseInterface obj, BroadphaseProxy proxy, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax, Dispatcher dispatcher)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.SetAabb(proxy, ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr, dispatcher);
				}
			}
		}
	}
}
