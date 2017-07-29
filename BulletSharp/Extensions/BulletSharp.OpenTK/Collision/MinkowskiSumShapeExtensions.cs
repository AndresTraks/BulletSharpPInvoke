using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class MinkowskiSumShapeExtensions
	{
		public unsafe static void GetTransformA(this MinkowskiSumShape obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.TransformA;
			}
		}

		public static OpenTK.Matrix4 GetTransformA(this MinkowskiSumShape obj)
		{
			OpenTK.Matrix4 value;
			GetTransformA(obj, out value);
			return value;
		}

		public unsafe static void GetTransformB(this MinkowskiSumShape obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.TransformB;
			}
		}

		public static OpenTK.Matrix4 GetTransformB(this MinkowskiSumShape obj)
		{
			OpenTK.Matrix4 value;
			GetTransformB(obj, out value);
			return value;
		}

		public unsafe static void SetTransformA(this MinkowskiSumShape obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.TransformA = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetTransformA(this MinkowskiSumShape obj, OpenTK.Matrix4 value)
		{
			SetTransformA(obj, ref value);
		}

		public unsafe static void SetTransformB(this MinkowskiSumShape obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.TransformB = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetTransformB(this MinkowskiSumShape obj, OpenTK.Matrix4 value)
		{
			SetTransformB(obj, ref value);
		}
	}
    */
}
