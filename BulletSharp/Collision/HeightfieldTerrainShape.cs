using BulletSharp.Math;
using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class HeightfieldTerrainShape : ConcaveShape
	{
		public HeightfieldTerrainShape(int heightStickWidth, int heightStickLength,
			IntPtr heightfieldData, double heightScale, double minHeight, double maxHeight,
			int upAxis, PhyScalarType heightDataType, bool flipQuadEdges)
		{
			IntPtr native = btHeightfieldTerrainShape_new(heightStickWidth, heightStickLength,
				heightfieldData, heightScale, minHeight, maxHeight, upAxis, heightDataType,
				flipQuadEdges);
			InitializeCollisionShape(native);
		}

		public void PerformRaycast(TriangleCallback callback, ref Vector3 raySource, ref Vector3 rayTarget)
		{
			btHeightfieldTerrainShape_performRaycast(Native, callback.Native, ref raySource, ref rayTarget);
		}

		public void BuildAccelerator(int chunkSize)
		{
			btHeightfieldTerrainShape_buildAccelerator(Native, chunkSize);
		}

		public void ClearAccelerator()
		{
			btHeightfieldTerrainShape_clearAccelerator(Native);
		}

		public void SetFlipTriangleWinding(bool flipTriangleWinding)
		{
			btHeightfieldTerrainShape_setFlipTriangleWinding(Native, flipTriangleWinding);
		}

		public void SetUseDiamondSubdivision()
		{
			btHeightfieldTerrainShape_setUseDiamondSubdivision(Native);
		}

		public void SetUseDiamondSubdivision(bool useDiamondSubdivision)
		{
			btHeightfieldTerrainShape_setUseDiamondSubdivision2(Native, useDiamondSubdivision);
		}

		public void SetUseZigzagSubdivision()
		{
			btHeightfieldTerrainShape_setUseZigzagSubdivision(Native);
		}

		public void SetUseZigzagSubdivision(bool useZigzagSubdivision)
		{
			btHeightfieldTerrainShape_setUseZigzagSubdivision2(Native, useZigzagSubdivision);
		}

		public int UpAxis => btHeightfieldTerrainShape_getUpAxis(Native);
	}
}
