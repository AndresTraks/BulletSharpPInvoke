using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ConvexCast : BulletDisposableObject
	{
		public class CastResult : BulletDisposableObject
		{
			private DebugDraw _debugDrawer;

			public CastResult()
			{
				IntPtr native = btConvexCast_CastResult_new();
				InitializeUserOwned(native);
			}

			public void DebugDraw(float fraction)
			{
				btConvexCast_CastResult_DebugDraw(Native, fraction);
			}

			public void DrawCoordSystem(Matrix trans)
			{
				btConvexCast_CastResult_drawCoordSystem(Native, ref trans);
			}

			public void ReportFailure(int errNo, int numIterations)
			{
				btConvexCast_CastResult_reportFailure(Native, errNo, numIterations);
			}

			public float AllowedPenetration
			{
				get => btConvexCast_CastResult_getAllowedPenetration(Native);
				set => btConvexCast_CastResult_setAllowedPenetration(Native, value);
			}

			public DebugDraw DebugDrawer
			{
				get => _debugDrawer;
				set
				{
					_debugDrawer = value;
					btConvexCast_CastResult_setDebugDrawer(Native, value != null ? value.Native : IntPtr.Zero);
				}
			}

			public float Fraction
			{
				get => btConvexCast_CastResult_getFraction(Native);
				set => btConvexCast_CastResult_setFraction(Native, value);
			}

			public Vector3 HitPoint
			{
				get
				{
					Vector3 value;
					btConvexCast_CastResult_getHitPoint(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setHitPoint(Native, ref value);
			}

			public Matrix HitTransformA
			{
				get
				{
					Matrix value;
					btConvexCast_CastResult_getHitTransformA(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setHitTransformA(Native, ref value);
			}

			public Matrix HitTransformB
			{
				get
				{
					Matrix value;
					btConvexCast_CastResult_getHitTransformB(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setHitTransformB(Native, ref value);
			}

			public Vector3 Normal
			{
				get
				{
					Vector3 value;
					btConvexCast_CastResult_getNormal(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setNormal(Native, ref value);
			}

			protected override void Dispose(bool disposing)
			{
				btConvexCast_CastResult_delete(Native);
			}
		}

		protected internal ConvexCast()
		{
		}

		public bool CalcTimeOfImpact(Matrix fromA, Matrix toA, Matrix fromB, Matrix toB,
			CastResult result)
		{
			return btConvexCast_calcTimeOfImpact(Native, ref fromA, ref toA, ref fromB,
				ref toB, result.Native);
		}

		protected override void Dispose(bool disposing)
		{
			btConvexCast_delete(Native);
		}
	}
}
