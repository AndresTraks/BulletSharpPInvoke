using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PointCollectorExtensions
	{
		public unsafe static void GetNormalOnBInWorld(this PointCollector obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.NormalOnBInWorld;
			}
		}

		public static OpenTK.Vector3 GetNormalOnBInWorld(this PointCollector obj)
		{
			OpenTK.Vector3 value;
			GetNormalOnBInWorld(obj, out value);
			return value;
		}

		public unsafe static void GetPointInWorld(this PointCollector obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.PointInWorld;
			}
		}

		public static OpenTK.Vector3 GetPointInWorld(this PointCollector obj)
		{
			OpenTK.Vector3 value;
			GetPointInWorld(obj, out value);
			return value;
		}

		public unsafe static void SetNormalOnBInWorld(this PointCollector obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.NormalOnBInWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetNormalOnBInWorld(this PointCollector obj, OpenTK.Vector3 value)
		{
			SetNormalOnBInWorld(obj, ref value);
		}

		public unsafe static void SetPointInWorld(this PointCollector obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.PointInWorld = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetPointInWorld(this PointCollector obj, OpenTK.Vector3 value)
		{
			SetPointInWorld(obj, ref value);
		}
	}
}
