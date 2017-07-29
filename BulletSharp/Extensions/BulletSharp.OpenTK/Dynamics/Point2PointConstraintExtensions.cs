using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Point2PointConstraintExtensions
	{
        /*
		public unsafe static void GetInfo2NonVirtual(this Point2PointConstraint obj, ConstraintInfo2 info, ref OpenTK.Matrix4 body0_trans, ref OpenTK.Matrix4 body1_trans)
		{
			fixed (OpenTK.Matrix4* body0_transPtr = &body0_trans)
			{
				fixed (OpenTK.Matrix4* body1_transPtr = &body1_trans)
				{
					obj.GetInfo2NonVirtual(info, ref *(BulletSharp.Math.Matrix*)body0_transPtr, ref *(BulletSharp.Math.Matrix*)body1_transPtr);
				}
			}
		}
        */
		public unsafe static void GetPivotInA(this Point2PointConstraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.PivotInA;
			}
		}

		public static OpenTK.Vector3 GetPivotInA(this Point2PointConstraint obj)
		{
			OpenTK.Vector3 value;
			GetPivotInA(obj, out value);
			return value;
		}

		public unsafe static void GetPivotInB(this Point2PointConstraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.PivotInB;
			}
		}

		public static OpenTK.Vector3 GetPivotInB(this Point2PointConstraint obj)
		{
			OpenTK.Vector3 value;
			GetPivotInB(obj, out value);
			return value;
		}

		public unsafe static void SetPivotA(this Point2PointConstraint obj, ref OpenTK.Vector3 pivotA)
		{
			fixed (OpenTK.Vector3* pivotAPtr = &pivotA)
			{
				obj.PivotInA = *(BulletSharp.Math.Vector3*)pivotAPtr;
			}
		}

		public unsafe static void SetPivotB(this Point2PointConstraint obj, ref OpenTK.Vector3 pivotB)
		{
			fixed (OpenTK.Vector3* pivotBPtr = &pivotB)
			{
				obj.PivotInB = *(BulletSharp.Math.Vector3*)pivotBPtr;
			}
		}
	}
}
