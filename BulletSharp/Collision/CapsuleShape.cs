using System;
using System.Runtime.InteropServices;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CapsuleShape : ConvexInternalShape
	{
		internal CapsuleShape(IntPtr native)
			: base(native)
		{
		}

		public CapsuleShape(float radius, float height)
			: base(btCapsuleShape_new(radius, height))
		{
		}

		public float HalfHeight => btCapsuleShape_getHalfHeight(Native);

		public float Radius => btCapsuleShape_getRadius(Native);

		public int UpAxis => btCapsuleShape_getUpAxis(Native);
	}

	public class CapsuleShapeX : CapsuleShape
	{
		public CapsuleShapeX(float radius, float height)
			: base(btCapsuleShapeX_new(radius, height))
		{
		}
	}

	public class CapsuleShapeZ : CapsuleShape
	{
		public CapsuleShapeZ(float radius, float height)
			: base(btCapsuleShapeZ_new(radius, height))
		{
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
