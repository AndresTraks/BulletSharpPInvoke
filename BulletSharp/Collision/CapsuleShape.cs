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

		public CapsuleShape(double radius, double height)
		{
			IntPtr native = btCapsuleShape_new(radius, height);
			InitializeCollisionShape(native);
		}

		public double HalfHeight => btCapsuleShape_getHalfHeight(Native);

		public double Radius => btCapsuleShape_getRadius(Native);

		public int UpAxis => btCapsuleShape_getUpAxis(Native);
	}

	public class CapsuleShapeX : CapsuleShape
	{
		public CapsuleShapeX(double radius, double height)
		{
			IntPtr native = btCapsuleShapeX_new(radius, height);
			InitializeCollisionShape(native);
		}
	}

	public class CapsuleShapeZ : CapsuleShape
	{
		public CapsuleShapeZ(double radius, double height)
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
