using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CompoundShapeChild : BulletObject
	{
		private CollisionShape _childShape;

		internal CompoundShapeChild(IntPtr native, CollisionShape childShape)
		{
			Initialize(native);
			_childShape = childShape;
		}

		public float ChildMargin
		{
			get => btCompoundShapeChild_getChildMargin(Native);
			set => btCompoundShapeChild_setChildMargin(Native, value);
		}

		public CollisionShape ChildShape
		{
			get => _childShape;
			set
			{
				btCompoundShapeChild_setChildShape(Native, value.Native);
				_childShape = value;
			}
		}

		public BroadphaseNativeType ChildShapeType
		{
			get => btCompoundShapeChild_getChildShapeType(Native);
			set => btCompoundShapeChild_setChildShapeType(Native, value);
		}

		public DbvtNode Node
		{
			get
			{
				IntPtr ptr = btCompoundShapeChild_getNode(Native);
				return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
			}
			set => btCompoundShapeChild_setNode(Native, (value != null) ? value.Native : IntPtr.Zero);
		}

		public Matrix4x4 Transform
		{
			get
			{
				Matrix4x4 value;
				btCompoundShapeChild_getTransform(Native, out value);
				return value;
			}
			set => btCompoundShapeChild_setTransform(Native, ref value);
		}
	}

	public class CompoundShape : CollisionShape
	{
		public CompoundShape(bool enableDynamicAabbTree = true, int initialChildCapacity = 0)
		{
			IntPtr native = btCompoundShape_new(enableDynamicAabbTree, initialChildCapacity);
			InitializeCollisionShape(native);

			ChildList = new CompoundShapeChildArray(Native);
		}

		public void AddChildShapeRef(ref Matrix4x4 localTransform, CollisionShape shape)
		{
			ChildList.AddChildShape(ref localTransform, shape);
		}

		public void AddChildShape(Matrix4x4 localTransform, CollisionShape shape)
		{
			ChildList.AddChildShape(ref localTransform, shape);
		}

	   public void CalculatePrincipalAxisTransform(float[] masses, ref Matrix4x4 principal,
			out Vector3 inertia)
		{
			btCompoundShape_calculatePrincipalAxisTransform(Native, masses,
				ref principal, out inertia);
		}

		public void CreateAabbTreeFromChildren()
		{
			btCompoundShape_createAabbTreeFromChildren(Native);
		}

		public CollisionShape GetChildShape(int index)
		{
			return ChildList[index].ChildShape;
		}

		public void GetChildTransform(int index, out Matrix4x4 value)
		{
			btCompoundShape_getChildTransform(Native, index, out value);
		}

		public Matrix4x4 GetChildTransform(int index)
		{
			Matrix4x4 value;
			btCompoundShape_getChildTransform(Native, index, out value);
			return value;
		}

		public void RecalculateLocalAabb()
		{
			btCompoundShape_recalculateLocalAabb(Native);
		}

		public void RemoveChildShape(CollisionShape shape)
		{
			ChildList.RemoveChildShape(shape);
		}

		public void RemoveChildShapeByIndex(int childShapeIndex)
		{
			ChildList.RemoveChildShapeByIndex(childShapeIndex);
		}

		public void UpdateChildTransform(int childIndex, Matrix4x4 newChildTransform,
			bool shouldRecalculateLocalAabb = true)
		{
			btCompoundShape_updateChildTransform(Native, childIndex, ref newChildTransform,
				shouldRecalculateLocalAabb);
		}

		public CompoundShapeChildArray ChildList { get; }

		public Dbvt DynamicAabbTree => new Dbvt(btCompoundShape_getDynamicAabbTree(Native));

		public int NumChildShapes => ChildList.Count;

		public int UpdateRevision => btCompoundShape_getUpdateRevision(Native);
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CompoundShapeData
	{
		public CollisionShapeData CollisionShapeData;
		public IntPtr ChildShapePtr;
		public int NumChildShapes;
		public float CollisionMargin;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CompoundShapeData), fieldName).ToInt32(); }
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CompoundShapeChildData
	{
		public TransformFloatData Transform;
		public IntPtr ChildShape;
		public int ChildShapeType;
		public float ChildMargin;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CompoundShapeChildData), fieldName).ToInt32(); }
	}
}
