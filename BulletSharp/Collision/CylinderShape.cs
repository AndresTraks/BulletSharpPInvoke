using System;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CylinderShape : ConvexInternalShape
	{
		protected internal CylinderShape()
		{
		}

		public CylinderShape(Vector3 halfExtents)
		{
			IntPtr native = btCylinderShape_new(ref halfExtents);
			InitializeCollisionShape(native);
		}

		public CylinderShape(double halfExtentX, double halfExtentY, double halfExtentZ)
		{
			IntPtr native = btCylinderShape_new2(halfExtentX, halfExtentY, halfExtentZ);
			InitializeCollisionShape(native);
		}

		public Vector3 HalfExtentsWithMargin
		{
			get
			{
				Vector3 value;
				btCylinderShape_getHalfExtentsWithMargin(Native, out value);
				return value;
			}
		}

		public Vector3 HalfExtentsWithoutMargin
		{
			get
			{
				Vector3 value;
				btCylinderShape_getHalfExtentsWithoutMargin(Native, out value);
				return value;
			}
		}

		public double Radius => btCylinderShape_getRadius(Native);

		public int UpAxis => btCylinderShape_getUpAxis(Native);
	}

	public class CylinderShapeX : CylinderShape
	{
		public CylinderShapeX(Vector3 halfExtents)
		{
			IntPtr native = btCylinderShapeX_new(ref halfExtents);
			InitializeCollisionShape(native);
		}

		public CylinderShapeX(double halfExtentX, double halfExtentY, double halfExtentZ)
		{
			IntPtr native = btCylinderShapeX_new2(halfExtentX, halfExtentY, halfExtentZ);
			InitializeCollisionShape(native);
		}
	}

	public class CylinderShapeZ : CylinderShape
	{
		public CylinderShapeZ(Vector3 halfExtents)
		{
			IntPtr native = btCylinderShapeZ_new(ref halfExtents);
			InitializeCollisionShape(native);
		}

		public CylinderShapeZ(double halfExtentX, double halfExtentY, double halfExtentZ)
		{
			IntPtr native = btCylinderShapeZ_new2(halfExtentX, halfExtentY, halfExtentZ);
			InitializeCollisionShape(native);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CylinderShapeData
	{
		public ConvexInternalShapeData ConvexInternalShapeData;
		public int UpAxis;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CylinderShapeData), fieldName).ToInt32(); }
	}
}
