using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StridingMeshInterfaceExtensions
	{
        public unsafe static void CalculateAabbBruteForce(this StridingMeshInterface obj, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
                    obj.CalculateAabbBruteForce(out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}

        public unsafe static void GetPremadeAabb(this StridingMeshInterface obj, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
                    obj.GetPremadeAabb(out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}

		public unsafe static void GetScaling(this StridingMeshInterface obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Scaling;
			}
		}

		public static OpenTK.Vector3 GetScaling(this StridingMeshInterface obj)
		{
			OpenTK.Vector3 value;
			GetScaling(obj, out value);
			return value;
		}
        /*
		public unsafe static void InternalProcessAllTriangles(this StridingMeshInterface obj, InternalTriangleIndexCallback callback, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.InternalProcessAllTriangles(callback, ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}
        */
		public unsafe static void SetPremadeAabb(this StridingMeshInterface obj, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.SetPremadeAabb(ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}

		public unsafe static void SetScaling(this StridingMeshInterface obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.Scaling = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetScaling(this StridingMeshInterface obj, OpenTK.Vector3 value)
		{
			SetScaling(obj, ref value);
		}
	}
}
