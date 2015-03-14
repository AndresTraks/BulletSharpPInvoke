using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class SphereTriangleDetectorExtensions
	{
		public unsafe static bool Collide(this SphereTriangleDetector obj, ref OpenTK.Vector3 sphereCenter, ref OpenTK.Vector3 point, ref OpenTK.Vector3 resultNormal, float depth, float timeOfImpact, float contactBreakingThreshold)
		{
			fixed (OpenTK.Vector3* sphereCenterPtr = &sphereCenter)
			{
				fixed (OpenTK.Vector3* pointPtr = &point)
				{
					fixed (OpenTK.Vector3* resultNormalPtr = &resultNormal)
					{
						return obj.Collide(ref *(BulletSharp.Math.Vector3*)sphereCenterPtr, ref *(BulletSharp.Math.Vector3*)pointPtr, ref *(BulletSharp.Math.Vector3*)resultNormalPtr, depth, timeOfImpact, contactBreakingThreshold);
					}
				}
			}
		}
	}
    */
}
