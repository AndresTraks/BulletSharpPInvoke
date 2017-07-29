using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class SubSimplexClosestResultExtensions
	{
		public unsafe static void GetClosestPointOnSimplex(this SubSimplexClosestResult obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ClosestPointOnSimplex;
			}
		}

		public static OpenTK.Vector3 GetClosestPointOnSimplex(this SubSimplexClosestResult obj)
		{
			OpenTK.Vector3 value;
			GetClosestPointOnSimplex(obj, out value);
			return value;
		}

		public unsafe static void SetClosestPointOnSimplex(this SubSimplexClosestResult obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ClosestPointOnSimplex = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetClosestPointOnSimplex(this SubSimplexClosestResult obj, OpenTK.Vector3 value)
		{
			SetClosestPointOnSimplex(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class VoronoiSimplexSolverExtensions
	{
		public unsafe static void AddVertex(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 w, ref OpenTK.Vector3 p, ref OpenTK.Vector3 q)
		{
			fixed (OpenTK.Vector3* wPtr = &w)
			{
				fixed (OpenTK.Vector3* pPtr = &p)
				{
					fixed (OpenTK.Vector3* qPtr = &q)
					{
						obj.AddVertex(ref *(BulletSharp.Math.Vector3*)wPtr, ref *(BulletSharp.Math.Vector3*)pPtr, ref *(BulletSharp.Math.Vector3*)qPtr);
					}
				}
			}
		}

		public unsafe static void Backup_closest(this VoronoiSimplexSolver obj, out OpenTK.Vector3 v)
		{
			fixed (OpenTK.Vector3* vPtr = &v)
			{
                obj.BackupClosest(out *(BulletSharp.Math.Vector3*)vPtr);
			}
		}

        public unsafe static bool Closest(this VoronoiSimplexSolver obj, out OpenTK.Vector3 v)
		{
			fixed (OpenTK.Vector3* vPtr = &v)
			{
                return obj.Closest(out *(BulletSharp.Math.Vector3*)vPtr);
			}
		}

		public unsafe static bool ClosestPtPointTetrahedron(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 p, ref OpenTK.Vector3 a, ref OpenTK.Vector3 b, ref OpenTK.Vector3 c, ref OpenTK.Vector3 d, SubSimplexClosestResult finalResult)
		{
			fixed (OpenTK.Vector3* pPtr = &p)
			{
				fixed (OpenTK.Vector3* aPtr = &a)
				{
					fixed (OpenTK.Vector3* bPtr = &b)
					{
						fixed (OpenTK.Vector3* cPtr = &c)
						{
							fixed (OpenTK.Vector3* dPtr = &d)
							{
								return obj.ClosestPtPointTetrahedron(ref *(BulletSharp.Math.Vector3*)pPtr, ref *(BulletSharp.Math.Vector3*)aPtr, ref *(BulletSharp.Math.Vector3*)bPtr, ref *(BulletSharp.Math.Vector3*)cPtr, ref *(BulletSharp.Math.Vector3*)dPtr, finalResult);
							}
						}
					}
				}
			}
		}

		public unsafe static bool ClosestPtPointTriangle(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 p, ref OpenTK.Vector3 a, ref OpenTK.Vector3 b, ref OpenTK.Vector3 c, SubSimplexClosestResult result)
		{
			fixed (OpenTK.Vector3* pPtr = &p)
			{
				fixed (OpenTK.Vector3* aPtr = &a)
				{
					fixed (OpenTK.Vector3* bPtr = &b)
					{
						fixed (OpenTK.Vector3* cPtr = &c)
						{
							return obj.ClosestPtPointTriangle(ref *(BulletSharp.Math.Vector3*)pPtr, ref *(BulletSharp.Math.Vector3*)aPtr, ref *(BulletSharp.Math.Vector3*)bPtr, ref *(BulletSharp.Math.Vector3*)cPtr, result);
						}
					}
				}
			}
		}

        public unsafe static void ComputePoints(this VoronoiSimplexSolver obj, out OpenTK.Vector3 p1, out OpenTK.Vector3 p2)
		{
			fixed (OpenTK.Vector3* p1Ptr = &p1)
			{
				fixed (OpenTK.Vector3* p2Ptr = &p2)
				{
                    obj.ComputePoints(out *(BulletSharp.Math.Vector3*)p1Ptr, out *(BulletSharp.Math.Vector3*)p2Ptr);
				}
			}
		}

		public unsafe static void GetCachedP1(this VoronoiSimplexSolver obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.CachedP1;
			}
		}

		public static OpenTK.Vector3 GetCachedP1(this VoronoiSimplexSolver obj)
		{
			OpenTK.Vector3 value;
			GetCachedP1(obj, out value);
			return value;
		}

		public unsafe static void GetCachedP2(this VoronoiSimplexSolver obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.CachedP2;
			}
		}

		public static OpenTK.Vector3 GetCachedP2(this VoronoiSimplexSolver obj)
		{
			OpenTK.Vector3 value;
			GetCachedP2(obj, out value);
			return value;
		}

		public unsafe static void GetCachedV(this VoronoiSimplexSolver obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.CachedV;
			}
		}

		public static OpenTK.Vector3 GetCachedV(this VoronoiSimplexSolver obj)
		{
			OpenTK.Vector3 value;
			GetCachedV(obj, out value);
			return value;
		}

		public unsafe static void GetLastW(this VoronoiSimplexSolver obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LastW;
			}
		}

		public static OpenTK.Vector3 GetLastW(this VoronoiSimplexSolver obj)
		{
			OpenTK.Vector3 value;
			GetLastW(obj, out value);
			return value;
		}
        /*
		public unsafe static int GetSimplex(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 pBuf, ref OpenTK.Vector3 qBuf, ref OpenTK.Vector3 yBuf)
		{
			fixed (OpenTK.Vector3* pBufPtr = &pBuf)
			{
				fixed (OpenTK.Vector3* qBufPtr = &qBuf)
				{
					fixed (OpenTK.Vector3* yBufPtr = &yBuf)
					{
						return obj.GetSimplex(ref *(BulletSharp.Math.Vector3*)pBufPtr, ref *(BulletSharp.Math.Vector3*)qBufPtr, ref *(BulletSharp.Math.Vector3*)yBufPtr);
					}
				}
			}
		}

		public unsafe static void GetSimplexPointsP(this VoronoiSimplexSolver obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.SimplexPointsP;
			}
		}

		public static OpenTK.Vector3 GetSimplexPointsP(this VoronoiSimplexSolver obj)
		{
			OpenTK.Vector3 value;
			GetSimplexPointsP(obj, out value);
			return value;
		}

		public unsafe static void GetSimplexPointsQ(this VoronoiSimplexSolver obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.SimplexPointsQ;
			}
		}

		public static OpenTK.Vector3 GetSimplexPointsQ(this VoronoiSimplexSolver obj)
		{
			OpenTK.Vector3 value;
			GetSimplexPointsQ(obj, out value);
			return value;
		}

		public unsafe static void GetSimplexVectorW(this VoronoiSimplexSolver obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.SimplexVectorW;
			}
		}

		public static OpenTK.Vector3 GetSimplexVectorW(this VoronoiSimplexSolver obj)
		{
			OpenTK.Vector3 value;
			GetSimplexVectorW(obj, out value);
			return value;
		}
        */
		public unsafe static bool InSimplex(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 w)
		{
			fixed (OpenTK.Vector3* wPtr = &w)
			{
				return obj.InSimplex(*(BulletSharp.Math.Vector3*)wPtr);
			}
		}

		public unsafe static int PointOutsideOfPlane(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 p, ref OpenTK.Vector3 a, ref OpenTK.Vector3 b, ref OpenTK.Vector3 c, ref OpenTK.Vector3 d)
		{
			fixed (OpenTK.Vector3* pPtr = &p)
			{
				fixed (OpenTK.Vector3* aPtr = &a)
				{
					fixed (OpenTK.Vector3* bPtr = &b)
					{
						fixed (OpenTK.Vector3* cPtr = &c)
						{
							fixed (OpenTK.Vector3* dPtr = &d)
							{
								return obj.PointOutsideOfPlane(*(BulletSharp.Math.Vector3*)pPtr, *(BulletSharp.Math.Vector3*)aPtr, *(BulletSharp.Math.Vector3*)bPtr, *(BulletSharp.Math.Vector3*)cPtr, *(BulletSharp.Math.Vector3*)dPtr);
							}
						}
					}
				}
			}
		}

		public unsafe static void SetCachedP1(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.CachedP1 = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetCachedP1(this VoronoiSimplexSolver obj, OpenTK.Vector3 value)
		{
			SetCachedP1(obj, ref value);
		}

		public unsafe static void SetCachedP2(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.CachedP2 = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetCachedP2(this VoronoiSimplexSolver obj, OpenTK.Vector3 value)
		{
			SetCachedP2(obj, ref value);
		}

		public unsafe static void SetCachedV(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.CachedV = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetCachedV(this VoronoiSimplexSolver obj, OpenTK.Vector3 value)
		{
			SetCachedV(obj, ref value);
		}

		public unsafe static void SetLastW(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LastW = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLastW(this VoronoiSimplexSolver obj, OpenTK.Vector3 value)
		{
			SetLastW(obj, ref value);
		}
        /*
		public unsafe static void SetSimplexPointsP(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.SimplexPointsP = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetSimplexPointsP(this VoronoiSimplexSolver obj, OpenTK.Vector3 value)
		{
			SetSimplexPointsP(obj, ref value);
		}

		public unsafe static void SetSimplexPointsQ(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.SimplexPointsQ = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetSimplexPointsQ(this VoronoiSimplexSolver obj, OpenTK.Vector3 value)
		{
			SetSimplexPointsQ(obj, ref value);
		}

		public unsafe static void SetSimplexVectorW(this VoronoiSimplexSolver obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.SimplexVectorW = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetSimplexVectorW(this VoronoiSimplexSolver obj, OpenTK.Vector3 value)
		{
			SetSimplexVectorW(obj, ref value);
		}
        */
	}
}
