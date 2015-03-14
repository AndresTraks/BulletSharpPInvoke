using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class SphereBoxCollisionAlgorithmExtensions
	{
		public unsafe static bool GetSphereDistance(this SphereBoxCollisionAlgorithm obj, CollisionObjectWrapper boxObjWrap, ref OpenTK.Vector3 v3PointOnBox, ref OpenTK.Vector3 normal, out float penetrationDepth, ref OpenTK.Vector3 v3SphereCenter, float fRadius, float maxContactDistance)
		{
			fixed (OpenTK.Vector3* v3PointOnBoxPtr = &v3PointOnBox)
			{
				fixed (OpenTK.Vector3* normalPtr = &normal)
				{
					fixed (OpenTK.Vector3* v3SphereCenterPtr = &v3SphereCenter)
					{
						return obj.GetSphereDistance(boxObjWrap, ref *(BulletSharp.Math.Vector3*)v3PointOnBoxPtr, ref *(BulletSharp.Math.Vector3*)normalPtr, out penetrationDepth, ref *(BulletSharp.Math.Vector3*)v3SphereCenterPtr, fRadius, maxContactDistance);
					}
				}
			}
		}

		public unsafe static float GetSpherePenetration(this SphereBoxCollisionAlgorithm obj, ref OpenTK.Vector3 boxHalfExtent, ref OpenTK.Vector3 sphereRelPos, ref OpenTK.Vector3 closestPoint, ref OpenTK.Vector3 normal)
		{
			fixed (OpenTK.Vector3* boxHalfExtentPtr = &boxHalfExtent)
			{
				fixed (OpenTK.Vector3* sphereRelPosPtr = &sphereRelPos)
				{
					fixed (OpenTK.Vector3* closestPointPtr = &closestPoint)
					{
						fixed (OpenTK.Vector3* normalPtr = &normal)
						{
							return obj.GetSpherePenetration(ref *(BulletSharp.Math.Vector3*)boxHalfExtentPtr, ref *(BulletSharp.Math.Vector3*)sphereRelPosPtr, ref *(BulletSharp.Math.Vector3*)closestPointPtr, ref *(BulletSharp.Math.Vector3*)normalPtr);
						}
					}
				}
			}
		}
	}
}
