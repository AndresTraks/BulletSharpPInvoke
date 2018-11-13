using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MaterialProperties : BulletDisposableObject
	{
		public MaterialProperties()
		{
			IntPtr native = btMaterialProperties_new();
			InitializeUserOwned(native);
		}

		public IntPtr MaterialBase
		{
			get => btMaterialProperties_getMaterialBase(Native);
			set => btMaterialProperties_setMaterialBase(Native, value);
		}

		public int MaterialStride
		{
			get => btMaterialProperties_getMaterialStride(Native);
			set => btMaterialProperties_setMaterialStride(Native, value);
		}

		public PhyScalarType MaterialType
		{
			get => btMaterialProperties_getMaterialType(Native);
			set => btMaterialProperties_setMaterialType(Native, value);
		}

		public int NumMaterials
		{
			get => btMaterialProperties_getNumMaterials(Native);
			set => btMaterialProperties_setNumMaterials(Native, value);
		}

		public int NumTriangles
		{
			get => btMaterialProperties_getNumTriangles(Native);
			set => btMaterialProperties_setNumTriangles(Native, value);
		}

		public IntPtr TriangleMaterialsBase
		{
			get => btMaterialProperties_getTriangleMaterialsBase(Native);
			set => btMaterialProperties_setTriangleMaterialsBase(Native, value);
		}

		public int TriangleMaterialStride
		{
			get => btMaterialProperties_getTriangleMaterialStride(Native);
			set => btMaterialProperties_setTriangleMaterialStride(Native, value);
		}

		public PhyScalarType TriangleType
		{
			get => btMaterialProperties_getTriangleType(Native);
			set => btMaterialProperties_setTriangleType(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			btMaterialProperties_delete(Native);
		}
	}

	public class TriangleIndexVertexMaterialArray : TriangleIndexVertexArray
	{
		public TriangleIndexVertexMaterialArray()
		{
			IntPtr native = btTriangleIndexVertexMaterialArray_new();
			InitializeUserOwned(native);
		}

		public TriangleIndexVertexMaterialArray(int numTriangles, IntPtr triangleIndexBase,
			int triangleIndexStride, int numVertices, IntPtr vertexBase, int vertexStride,
			int numMaterials, IntPtr materialBase, int materialStride, IntPtr triangleMaterialsBase,
			int materialIndexStride)
		{
			IntPtr native = btTriangleIndexVertexMaterialArray_new2(numTriangles, triangleIndexBase,
				triangleIndexStride, numVertices, vertexBase, vertexStride,
				numMaterials, materialBase, materialStride, triangleMaterialsBase,
				materialIndexStride);
			InitializeUserOwned(native);
		}

		public void AddMaterialProperties(MaterialProperties mat, PhyScalarType triangleType = PhyScalarType.Int32)
		{
			btTriangleIndexVertexMaterialArray_addMaterialProperties(Native, mat.Native,
				triangleType);
		}

		public void GetLockedMaterialBase(out IntPtr materialBase, out int numMaterials,
			out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase,
			out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType,
			int subpart = 0)
		{
			btTriangleIndexVertexMaterialArray_getLockedMaterialBase(Native, out materialBase,
				out numMaterials, out materialType, out materialStride, out triangleMaterialBase,
				out numTriangles, out triangleMaterialStride, out triangleType, subpart);
		}

		public void GetLockedReadOnlyMaterialBase(out IntPtr materialBase, out int numMaterials,
			out PhyScalarType materialType, out int materialStride, out IntPtr triangleMaterialBase,
			out int numTriangles, out int triangleMaterialStride, out PhyScalarType triangleType,
			int subpart = 0)
		{
			btTriangleIndexVertexMaterialArray_getLockedReadOnlyMaterialBase(Native,
				out materialBase, out numMaterials, out materialType, out materialStride,
				out triangleMaterialBase, out numTriangles, out triangleMaterialStride,
				out triangleType, subpart);
		}
	}
}
