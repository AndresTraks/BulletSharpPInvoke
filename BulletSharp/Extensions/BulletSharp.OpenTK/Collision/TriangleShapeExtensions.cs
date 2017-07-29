using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TriangleShapeExtensions
	{
		public unsafe static void CalcNormal(this TriangleShape obj, ref OpenTK.Vector3 normal)
		{
			fixed (OpenTK.Vector3* normalPtr = &normal)
			{
				obj.CalcNormal(ref *(BulletSharp.Math.Vector3*)normalPtr);
			}
		}

		public unsafe static void GetPlaneEquation(this TriangleShape obj, int i, ref OpenTK.Vector3 planeNormal, ref OpenTK.Vector3 planeSupport)
		{
			fixed (OpenTK.Vector3* planeNormalPtr = &planeNormal)
			{
				fixed (OpenTK.Vector3* planeSupportPtr = &planeSupport)
				{
					obj.GetPlaneEquation(i, ref *(BulletSharp.Math.Vector3*)planeNormalPtr, ref *(BulletSharp.Math.Vector3*)planeSupportPtr);
				}
			}
		}

		public unsafe static void GetVertices1(this TriangleShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Vertices1;
			}
		}

		public static OpenTK.Vector3 GetVertices1(this TriangleShape obj)
		{
			OpenTK.Vector3 value;
			GetVertices1(obj, out value);
			return value;
		}

		public unsafe static void SetVertices1(this TriangleShape obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.Vertices1 = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetVertices1(this TriangleShape obj, OpenTK.Vector3 value)
		{
			SetVertices1(obj, ref value);
		}
	}
    */
}
