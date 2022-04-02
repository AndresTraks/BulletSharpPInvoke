using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MinkowskiSumShape : ConvexInternalShape
	{
		public MinkowskiSumShape(ConvexShape shapeA, ConvexShape shapeB)
		{
			IntPtr native = btMinkowskiSumShape_new(shapeA.Native, shapeB.Native);
			InitializeCollisionShape(native);

			ShapeA = shapeA;
			ShapeB = shapeB;
		}

		public ConvexShape ShapeA { get; }

		public ConvexShape ShapeB { get; }

		public Matrix4x4 TransformA
		{
			get
			{
				Matrix4x4 value;
				btMinkowskiSumShape_getTransformA(Native, out value);
				return value;
			}
			set => btMinkowskiSumShape_setTransformA(Native, ref value);
		}

		public Matrix4x4 TransformB
		{
			get
			{
				Matrix4x4 value;
				btMinkowskiSumShape_GetTransformB(Native, out value);
				return value;
			}
			set => btMinkowskiSumShape_setTransformB(Native, ref value);
		}
	}
}
