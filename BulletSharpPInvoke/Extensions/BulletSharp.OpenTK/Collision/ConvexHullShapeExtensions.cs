using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexHullShapeExtensions
	{
		public unsafe static void AddPoint(this ConvexHullShape obj, ref OpenTK.Vector3 point, bool recalculateLocalAabb)
		{
			fixed (OpenTK.Vector3* pointPtr = &point)
			{
				obj.AddPoint(ref *(BulletSharp.Math.Vector3*)pointPtr, recalculateLocalAabb);
			}
		}

		public unsafe static void AddPoint(this ConvexHullShape obj, ref OpenTK.Vector3 point)
		{
			fixed (OpenTK.Vector3* pointPtr = &point)
			{
				obj.AddPoint(ref *(BulletSharp.Math.Vector3*)pointPtr);
			}
		}
        /*
		public unsafe static void GetPoints(this ConvexHullShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Points;
			}
		}

		public static OpenTK.Vector3 GetPoints(this ConvexHullShape obj)
		{
			OpenTK.Vector3 value;
			GetPoints(obj, out value);
			return value;
		}

		public unsafe static void GetUnscaledPoints(this ConvexHullShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.UnscaledPoints;
			}
		}

		public static OpenTK.Vector3 GetUnscaledPoints(this ConvexHullShape obj)
		{
			OpenTK.Vector3 value;
			GetUnscaledPoints(obj, out value);
			return value;
		}
        
		public unsafe static void Project(this ConvexHullShape obj, ref OpenTK.Matrix4 trans, ref OpenTK.Vector3 dir, float minProj, float maxProj, ref OpenTK.Vector3 witnesPtMin, ref OpenTK.Vector3 witnesPtMax)
		{
			fixed (OpenTK.Matrix4* transPtr = &trans)
			{
				fixed (OpenTK.Vector3* dirPtr = &dir)
				{
					fixed (OpenTK.Vector3* witnesPtMinPtr = &witnesPtMin)
					{
						fixed (OpenTK.Vector3* witnesPtMaxPtr = &witnesPtMax)
						{
							obj.Project(ref *(BulletSharp.Math.Matrix*)transPtr, ref *(BulletSharp.Math.Vector3*)dirPtr, minProj, maxProj, ref *(BulletSharp.Math.Vector3*)witnesPtMinPtr, ref *(BulletSharp.Math.Vector3*)witnesPtMaxPtr);
						}
					}
				}
			}
		}
        */
	}
}
