using System;
using System.Runtime.InteropServices;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class ConeShape : ConvexInternalShape
	{
		protected internal ConeShape()
		{
		}

		public ConeShape(double radius, double height)
		{
			IntPtr native = btConeShape_new(radius, height);
			InitializeCollisionShape(native);
		}

		public int ConeUpIndex
		{
			get => btConeShape_getConeUpIndex(Native);
			set => btConeShape_setConeUpIndex(Native, value);
		}

		public double Height
		{
			get => btConeShape_getHeight(Native);
			set => btConeShape_setHeight(Native, value);
		}

		public double Radius
		{
			get => btConeShape_getRadius(Native);
			set => btConeShape_setRadius(Native, value);
		}
	}

	public class ConeShapeX : ConeShape
	{
		public ConeShapeX(double radius, double height)
		{
			IntPtr native = btConeShapeX_new(radius, height);
			InitializeCollisionShape(native);
		}
	}

	public class ConeShapeZ : ConeShape
	{
		public ConeShapeZ(double radius, double height)
		{
			IntPtr native = btConeShapeZ_new(radius, height);
			InitializeCollisionShape(native);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct ConeShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public int UpAxis;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(ConeShapeData), fieldName).ToInt32(); }
	}
}
