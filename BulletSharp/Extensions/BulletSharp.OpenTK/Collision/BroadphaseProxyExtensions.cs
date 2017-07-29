using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BroadphaseProxyExtensions
	{
		public unsafe static void GetAabbMax(this BroadphaseProxy obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AabbMax;
			}
		}

		public static OpenTK.Vector3 GetAabbMax(this BroadphaseProxy obj)
		{
			OpenTK.Vector3 value;
			GetAabbMax(obj, out value);
			return value;
		}

		public unsafe static void GetAabbMin(this BroadphaseProxy obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AabbMin;
			}
		}

		public static OpenTK.Vector3 GetAabbMin(this BroadphaseProxy obj)
		{
			OpenTK.Vector3 value;
			GetAabbMin(obj, out value);
			return value;
		}

		public unsafe static void SetAabbMax(this BroadphaseProxy obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AabbMax = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAabbMax(this BroadphaseProxy obj, OpenTK.Vector3 value)
		{
			SetAabbMax(obj, ref value);
		}

		public unsafe static void SetAabbMin(this BroadphaseProxy obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AabbMin = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAabbMin(this BroadphaseProxy obj, OpenTK.Vector3 value)
		{
			SetAabbMin(obj, ref value);
		}
	}
}
