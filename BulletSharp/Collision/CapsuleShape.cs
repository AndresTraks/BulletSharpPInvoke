using System;
using System.Runtime.InteropServices;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CapsuleShape : ConvexInternalShape
	{
		protected internal CapsuleShape()
		{
		}

		public CapsuleShape(float radius, float height)
		{
			IntPtr native = btCapsuleShape_new(radius, height);
			InitializeCollisionShape(native);
		}

		public float HalfHeight => btCapsuleShape_getHalfHeight(Native);

		public float Radius => btCapsuleShape_getRadius(Native);

		public int UpAxis => btCapsuleShape_getUpAxis(Native);
	}

	public class CapsuleShapeX : CapsuleShape
	{
		public CapsuleShapeX(float radius, float height)
		{
			IntPtr native = btCapsuleShapeX_new(radius, height);
			InitializeCollisionShape(native);
		}
	}

	public class CapsuleShapeZ : CapsuleShape
	{
		public CapsuleShapeZ(float radius, float height)
		{
			IntPtr native = btCapsuleShapeZ_new(radius, height);
			InitializeCollisionShape(native);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CapsuleShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public int UpAxis;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CapsuleShapeData), fieldName).ToInt32(); }
	}
}
