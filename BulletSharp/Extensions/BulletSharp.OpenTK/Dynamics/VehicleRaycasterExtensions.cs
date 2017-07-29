using System;
using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class VehicleRaycasterResultExtensions
	{
		public unsafe static void GetHitNormalInWorld(this VehicleRaycasterResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitNormalInWorld;
			}
		}

		public static OpenTK.Vector3 GetHitNormalInWorld(this VehicleRaycasterResult obj)
		{
			OpenTK.Vector3 value;
			GetHitNormalInWorld(obj, out value);
			return value;
		}

		public unsafe static void GetHitPointInWorld(this VehicleRaycasterResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HitPointInWorld;
			}
		}

		public static OpenTK.Vector3 GetHitPointInWorld(this VehicleRaycasterResult obj)
		{
			OpenTK.Vector3 value;
			GetHitPointInWorld(obj, out value);
			return value;
		}

		public unsafe static void SetHitNormalInWorld(this VehicleRaycasterResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitNormalInWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitNormalInWorld(this VehicleRaycasterResult obj, OpenTK.Vector3 value)
		{
			SetHitNormalInWorld(obj, ref value);
		}

		public unsafe static void SetHitPointInWorld(this VehicleRaycasterResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.HitPointInWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetHitPointInWorld(this VehicleRaycasterResult obj, OpenTK.Vector3 value)
		{
			SetHitPointInWorld(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class VehicleRaycasterExtensions
	{
		public unsafe static object CastRay(this IVehicleRaycaster obj, ref OpenTK.Vector3 from, ref OpenTK.Vector3 to, VehicleRaycasterResult result)
		{
			fixed (OpenTK.Vector3* fromPtr = &from)
			{
				fixed (OpenTK.Vector3* toPtr = &to)
				{
					return obj.CastRay(ref *(BulletSharp.Math.Vector3*)fromPtr, ref *(BulletSharp.Math.Vector3*)toPtr, result);
				}
			}
		}
	}
}
