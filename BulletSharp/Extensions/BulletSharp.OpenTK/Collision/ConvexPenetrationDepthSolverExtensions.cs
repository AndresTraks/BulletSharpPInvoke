using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexPenetrationDepthSolverExtensions
	{
		public unsafe static bool CalcPenDepth(this ConvexPenetrationDepthSolver obj, VoronoiSimplexSolver simplexSolver, ConvexShape convexA, ConvexShape convexB, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 v, ref OpenTK.Vector3 pa, ref OpenTK.Vector3 pb, IDebugDraw debugDraw)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* vPtr = &v)
					{
						fixed (OpenTK.Vector3* paPtr = &pa)
						{
							fixed (OpenTK.Vector3* pbPtr = &pb)
							{
								return obj.CalcPenDepth(simplexSolver, convexA, convexB, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)vPtr, ref *(BulletSharp.Math.Vector3*)paPtr, ref *(BulletSharp.Math.Vector3*)pbPtr, debugDraw);
							}
						}
					}
				}
			}
		}
	}
    */
}
