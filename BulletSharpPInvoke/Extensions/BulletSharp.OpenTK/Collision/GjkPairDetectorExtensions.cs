using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class GjkPairDetectorExtensions
	{
		public unsafe static void GetCachedSeparatingAxis(this GjkPairDetector obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.CachedSeparatingAxis;
			}
		}

		public static OpenTK.Vector3 GetCachedSeparatingAxis(this GjkPairDetector obj)
		{
			OpenTK.Vector3 value;
			GetCachedSeparatingAxis(obj, out value);
			return value;
		}

		public unsafe static void SetCachedSeparatingAxis(this GjkPairDetector obj, ref OpenTK.Vector3 seperatingAxis)
		{
			fixed (OpenTK.Vector3* seperatingAxisPtr = &seperatingAxis)
			{
				obj.CachedSeparatingAxis = *(BulletSharp.Math.Vector3*)seperatingAxisPtr;
			}
		}
	}
}
