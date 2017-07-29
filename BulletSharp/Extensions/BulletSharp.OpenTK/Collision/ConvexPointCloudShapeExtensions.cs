using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexPointCloudShapeExtensions
	{
		public unsafe static void GetUnscaledPoints(this ConvexPointCloudShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.UnscaledPoints;
			}
		}

		public static OpenTK.Vector3 GetUnscaledPoints(this ConvexPointCloudShape obj)
		{
			OpenTK.Vector3 value;
			GetUnscaledPoints(obj, out value);
			return value;
		}

		public unsafe static void SetPoints(this ConvexPointCloudShape obj, ref OpenTK.Vector3 points, int numPoints, bool computeAabb, ref OpenTK.Vector3 localScaling)
		{
			fixed (OpenTK.Vector3* pointsPtr = &points)
			{
				fixed (OpenTK.Vector3* localScalingPtr = &localScaling)
				{
					obj.SetPoints(ref *(BulletSharp.Math.Vector3*)pointsPtr, numPoints, computeAabb, ref *(BulletSharp.Math.Vector3*)localScalingPtr);
				}
			}
		}

		public unsafe static void SetPoints(this ConvexPointCloudShape obj, ref OpenTK.Vector3 points, int numPoints, bool computeAabb)
		{
			fixed (OpenTK.Vector3* pointsPtr = &points)
			{
				obj.SetPoints(ref *(BulletSharp.Math.Vector3*)pointsPtr, numPoints, computeAabb);
			}
		}

		public unsafe static void SetPoints(this ConvexPointCloudShape obj, ref OpenTK.Vector3 points, int numPoints)
		{
			fixed (OpenTK.Vector3* pointsPtr = &points)
			{
				obj.SetPoints(ref *(BulletSharp.Math.Vector3*)pointsPtr, numPoints);
			}
		}
	}
    */
}
