using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexPolyhedronExtensions
	{
		public unsafe static void GetExtents(this ConvexPolyhedron obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Extents;
			}
		}

		public static OpenTK.Vector3 GetExtents(this ConvexPolyhedron obj)
		{
			OpenTK.Vector3 value;
			GetExtents(obj, out value);
			return value;
		}

		public unsafe static void GetLocalCenter(this ConvexPolyhedron obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalCenter;
			}
		}

		public static OpenTK.Vector3 GetLocalCenter(this ConvexPolyhedron obj)
		{
			OpenTK.Vector3 value;
			GetLocalCenter(obj, out value);
			return value;
		}

		public unsafe static void GetMC(this ConvexPolyhedron obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.C;
			}
		}

		public static OpenTK.Vector3 GetMC(this ConvexPolyhedron obj)
		{
			OpenTK.Vector3 value;
			GetMC(obj, out value);
			return value;
		}

		public unsafe static void GetME(this ConvexPolyhedron obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.E;
			}
		}

		public static OpenTK.Vector3 GetME(this ConvexPolyhedron obj)
		{
			OpenTK.Vector3 value;
			GetME(obj, out value);
			return value;
		}

        public unsafe static void Project(this ConvexPolyhedron obj, ref OpenTK.Matrix4 trans, ref OpenTK.Vector3 dir, out float minProj, out float maxProj, out OpenTK.Vector3 witnesPtMin, out OpenTK.Vector3 witnesPtMax)
		{
			fixed (OpenTK.Matrix4* transPtr = &trans)
			{
				fixed (OpenTK.Vector3* dirPtr = &dir)
				{
					fixed (OpenTK.Vector3* witnesPtMinPtr = &witnesPtMin)
					{
						fixed (OpenTK.Vector3* witnesPtMaxPtr = &witnesPtMax)
						{
                            obj.Project(ref *(BulletSharp.Math.Matrix*)transPtr, ref *(BulletSharp.Math.Vector3*)dirPtr, out minProj, out maxProj, out *(BulletSharp.Math.Vector3*)witnesPtMinPtr, out *(BulletSharp.Math.Vector3*)witnesPtMaxPtr);
						}
					}
				}
			}
		}

		public unsafe static void SetExtents(this ConvexPolyhedron obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.Extents = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetExtents(this ConvexPolyhedron obj, OpenTK.Vector3 value)
		{
			SetExtents(obj, ref value);
		}

		public unsafe static void SetLocalCenter(this ConvexPolyhedron obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LocalCenter = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLocalCenter(this ConvexPolyhedron obj, OpenTK.Vector3 value)
		{
			SetLocalCenter(obj, ref value);
		}

		public unsafe static void SetMC(this ConvexPolyhedron obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.C = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetMC(this ConvexPolyhedron obj, OpenTK.Vector3 value)
		{
			SetMC(obj, ref value);
		}

		public unsafe static void SetME(this ConvexPolyhedron obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.E = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetME(this ConvexPolyhedron obj, OpenTK.Vector3 value)
		{
			SetME(obj, ref value);
		}
	}
}
