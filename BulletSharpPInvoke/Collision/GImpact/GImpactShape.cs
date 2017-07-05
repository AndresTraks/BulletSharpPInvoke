using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BulletSharp
{
	public enum GImpactShapeType
	{
		CompoundShape,
		TrimeshShapePart,
		TrimeshShape
	}

	public class TetrahedronShapeEx : BuSimplex1To4
	{
		public TetrahedronShapeEx()
			: base(UnsafeNativeMethods.btTetrahedronShapeEx_new())
		{
		}

		public void SetVertices(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
		{
			UnsafeNativeMethods.btTetrahedronShapeEx_setVertices(_native, ref v0, ref v1, ref v2, ref v3);
		}
	}

	public abstract class GImpactShapeInterface : ConcaveShape
	{
		internal GImpactShapeInterface(IntPtr native)
			: base(native)
		{
		}

		public void GetBulletTetrahedron(int primitiveIndex, TetrahedronShapeEx tetrahedron)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_getBulletTetrahedron(_native, primitiveIndex, tetrahedron._native);
		}

		public void GetBulletTriangle(int primitiveIndex, TriangleShapeEx triangle)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_getBulletTriangle(_native, primitiveIndex, triangle._native);
		}

		public void GetChildAabb(int childIndex, Matrix transform, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_getChildAabb(_native, childIndex, ref transform,
				out aabbMin, out aabbMax);
		}

		public abstract CollisionShape GetChildShape(int index);

		public Matrix GetChildTransform(int index)
		{
			Matrix value;
			UnsafeNativeMethods.btGImpactShapeInterface_getChildTransform(_native, index, out value);
			return value;
		}

		public void GetPrimitiveTriangle(int index, PrimitiveTriangle triangle)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_getPrimitiveTriangle(_native, index, triangle.Native);
		}

		public void LockChildShapes()
		{
			UnsafeNativeMethods.btGImpactShapeInterface_lockChildShapes(_native);
		}

		public void PostUpdate()
		{
			UnsafeNativeMethods.btGImpactShapeInterface_postUpdate(_native);
		}

		public void ProcessAllTrianglesRayRef(TriangleCallback callback, ref Vector3 rayFrom,
			ref Vector3 rayTo)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_processAllTrianglesRay(_native, callback._native,
				ref rayFrom, ref rayTo);
		}

		public void ProcessAllTrianglesRay(TriangleCallback callback, Vector3 rayFrom,
			Vector3 rayTo)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_processAllTrianglesRay(_native, callback._native,
				ref rayFrom, ref rayTo);
		}

		public void RayTestRef(ref Vector3 rayFrom, ref Vector3 rayTo, RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_rayTest(_native, ref rayFrom, ref rayTo, resultCallback._native);
		}

		public void RayTest(Vector3 rayFrom, Vector3 rayTo, RayResultCallback resultCallback)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_rayTest(_native, ref rayFrom, ref rayTo, resultCallback._native);
		}

		public void SetChildTransform(int index, Matrix transform)
		{
			UnsafeNativeMethods.btGImpactShapeInterface_setChildTransform(_native, index, ref transform);
		}

		public void UnlockChildShapes()
		{
			UnsafeNativeMethods.btGImpactShapeInterface_unlockChildShapes(_native);
		}

		public void UpdateBound()
		{
			UnsafeNativeMethods.btGImpactShapeInterface_updateBound(_native);
		}
		/*
		public GImpactBoxSet BoxSet
		{
			get { return UnsafeNativeMethods.btGImpactShapeInterface_getBoxSet(_native); }
		}
		*/
		public bool ChildrenHasTransform => UnsafeNativeMethods.btGImpactShapeInterface_childrenHasTransform(_native);

		public abstract GImpactShapeType GImpactShapeType { get; }

		public bool HasBoxSet => UnsafeNativeMethods.btGImpactShapeInterface_hasBoxSet(_native);

		public Aabb LocalBox => new Aabb(UnsafeNativeMethods.btGImpactShapeInterface_getLocalBox(_native));

		public bool NeedsRetrieveTetrahedrons => UnsafeNativeMethods.btGImpactShapeInterface_needsRetrieveTetrahedrons(_native);

		public bool NeedsRetrieveTriangles => UnsafeNativeMethods.btGImpactShapeInterface_needsRetrieveTriangles(_native);

		public int NumChildShapes => UnsafeNativeMethods.btGImpactShapeInterface_getNumChildShapes(_native);

		public abstract PrimitiveManagerBase PrimitiveManager { get; }
	}

	public sealed class CompoundPrimitiveManager : PrimitiveManagerBase
	{
		internal CompoundPrimitiveManager(IntPtr native, GImpactCompoundShape compoundShape)
			: base(native)
		{
			CompoundShape = compoundShape;
		}

		public GImpactCompoundShape CompoundShape { get; }
	}

	public class GImpactCompoundShape : GImpactShapeInterface
	{
		private CompoundPrimitiveManager _compoundPrimitiveManager;
		private List<CollisionShape> _childShapes = new List<CollisionShape>();

		internal GImpactCompoundShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactCompoundShape(bool childrenHasTransform = true)
			: base(UnsafeNativeMethods.btGImpactCompoundShape_new(childrenHasTransform))
		{
		}

		public void AddChildShape(Matrix localTransform, CollisionShape shape)
		{
			_childShapes.Add(shape);
			UnsafeNativeMethods.btGImpactCompoundShape_addChildShape(_native, ref localTransform, shape._native);
		}

		public void AddChildShape(CollisionShape shape)
		{
			_childShapes.Add(shape);
			UnsafeNativeMethods.btGImpactCompoundShape_addChildShape2(_native, shape._native);
		}

		public override CollisionShape GetChildShape(int index)
		{
			return _childShapes[index];
		}

		public override PrimitiveManagerBase PrimitiveManager => CompoundPrimitiveManager;

		public CompoundPrimitiveManager CompoundPrimitiveManager
		{
			get
			{
				if (_compoundPrimitiveManager == null)
				{
					_compoundPrimitiveManager = new CompoundPrimitiveManager(
						UnsafeNativeMethods.btGImpactCompoundShape_getCompoundPrimitiveManager(_native), this);
				}
				return _compoundPrimitiveManager;
			}
		}

		public override GImpactShapeType GImpactShapeType => GImpactShapeType.CompoundShape;
	}

	public sealed class TrimeshPrimitiveManager : PrimitiveManagerBase
	{
		private StridingMeshInterface _meshInterface;

		internal TrimeshPrimitiveManager(IntPtr native)
			: base(native)
		{
		}

		public TrimeshPrimitiveManager(StridingMeshInterface meshInterface, int part)
			: base(UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_new(meshInterface._native,
				part))
		{
			_meshInterface = meshInterface;
		}

		public TrimeshPrimitiveManager(TrimeshPrimitiveManager manager)
			: base(UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_new2(manager.Native))
		{
		}

		public TrimeshPrimitiveManager()
			: base(UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_new3())
		{
		}

		public void GetBulletTriangle(int primIndex, TriangleShapeEx triangle)
		{
			UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_get_bullet_triangle(
				Native, primIndex, triangle._native);
		}

		public void GetIndices(int faceIndex, out uint i0, out uint i1, out uint i2b)
		{
			UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_get_indices(Native,
				faceIndex, out i0, out i1, out i2b);
		}

		public void GetVertex(uint vertexIndex, out Vector3 vertex)
		{
			UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex(Native,
				vertexIndex, out vertex);
		}

		public void Lock()
		{
			UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_lock(Native);
		}

		public void Unlock()
		{
			UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_unlock(Native);
		}

		public IntPtr IndexBase
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexbase(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexbase(Native, value);
		}

		public int IndexStride
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndexstride(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndexstride(Native, value);
		}

		public PhyScalarType IndicesType
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getIndicestype(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setIndicestype(Native, value);
		}

		public int LockCount
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getLock_count(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setLock_count(Native, value);
		}

		public float Margin
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getMargin(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setMargin(Native, value);
		}

		public StridingMeshInterface MeshInterface
		{
			get => _meshInterface;
			set
			{
				UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setMeshInterface(Native, value._native);
				_meshInterface = value;
			}
		}

		public int Numfaces
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumfaces(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumfaces(Native, value);
		}

		public int Numverts
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getNumverts(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setNumverts(Native, value);
		}

		public int Part
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getPart(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setPart(Native, value);
		}

		public Vector3 Scale
		{
			get
			{
				Vector3 value;
				UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getScale(Native, out value);
				return value;
			}
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setScale(Native, ref value);
		}

		public int Stride
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getStride(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setStride(Native, value);
		}

		public PhyScalarType Type
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getType(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setType(Native, value);
		}

		public IntPtr VertexBase
		{
			get => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_getVertexbase(Native);
			set => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_setVertexbase(Native, value);
		}

		public int VertexCount => UnsafeNativeMethods.btGImpactMeshShapePart_TrimeshPrimitiveManager_get_vertex_count(Native);
	}

	public class GImpactMeshShapePart : GImpactShapeInterface
	{
		private TrimeshPrimitiveManager _gImpactTrimeshPrimitiveManager;

		internal GImpactMeshShapePart(IntPtr native)
			: base(native)
		{
		}

		public GImpactMeshShapePart()
			: base(UnsafeNativeMethods.btGImpactMeshShapePart_new())
		{
		}

		public GImpactMeshShapePart(StridingMeshInterface meshInterface, int part)
			: base(UnsafeNativeMethods.btGImpactMeshShapePart_new2(meshInterface._native, part))
		{
		}

		public override CollisionShape GetChildShape(int index)
		{
			throw new InvalidOperationException();
		}

		public void GetVertex(int vertexIndex, out Vector3 vertex)
		{
			UnsafeNativeMethods.btGImpactMeshShapePart_getVertex(_native, vertexIndex, out vertex);
		}

		public override GImpactShapeType GImpactShapeType => GImpactShapeType.TrimeshShapePart;

		public TrimeshPrimitiveManager GImpactTrimeshPrimitiveManager
		{
			get
			{
				if (_gImpactTrimeshPrimitiveManager == null)
				{
					_gImpactTrimeshPrimitiveManager = new TrimeshPrimitiveManager(
						UnsafeNativeMethods.btGImpactMeshShapePart_getTrimeshPrimitiveManager(_native));
				}
				return _gImpactTrimeshPrimitiveManager;
			}
		}

		public int Part => UnsafeNativeMethods.btGImpactMeshShapePart_getPart(_native);

		public override PrimitiveManagerBase PrimitiveManager => GImpactTrimeshPrimitiveManager;

		public int VertexCount => UnsafeNativeMethods.btGImpactMeshShapePart_getVertexCount(_native);
	}

	public class GImpactMeshShape : GImpactShapeInterface
	{
		private StridingMeshInterface _meshInterface;
		private bool _disposeMeshInterface;

		internal GImpactMeshShape(IntPtr native)
			: base(native)
		{
		}

		public GImpactMeshShape(StridingMeshInterface meshInterface)
			: base(UnsafeNativeMethods.btGImpactMeshShape_new(meshInterface._native))
		{
			_meshInterface = meshInterface;
		}

		public override CollisionShape GetChildShape(int index)
		{
			throw new InvalidOperationException();
		}

		public GImpactMeshShapePart GetMeshPart(int index)
		{
			return new GImpactMeshShapePart(UnsafeNativeMethods.btGImpactMeshShape_getMeshPart(_native, index));
		}

		public StridingMeshInterface MeshInterface
		{
			get
			{
				if (_meshInterface == null)
				{
					_meshInterface = new StridingMeshInterface(UnsafeNativeMethods.btGImpactMeshShape_getMeshInterface(_native));
					_disposeMeshInterface = true;
				}
				return _meshInterface;
			}
		}

		public int MeshPartCount => UnsafeNativeMethods.btGImpactMeshShape_getMeshPartCount(_native);

		public override PrimitiveManagerBase PrimitiveManager => null;

		public override GImpactShapeType GImpactShapeType => GImpactShapeType.TrimeshShape;

		protected override void Dispose(bool disposing)
		{
			if (disposing && _disposeMeshInterface)
			{
				_meshInterface.Dispose();
				_disposeMeshInterface = false;
			}
			base.Dispose(disposing);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct GImpactMeshShapeData
	{
		public CollisionShapeFloatData CollisionShapeData;
		public StridingMeshInterfaceData MeshInterface;
		public Vector3FloatData LocalScaling;
		public float CollisionMargin;
		public int GImpactSubType;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(GImpactMeshShapeData), fieldName).ToInt32(); }
	}
}
