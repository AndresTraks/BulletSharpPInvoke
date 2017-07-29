using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CompoundShapeChildExtensions
	{
		public unsafe static void GetTransform(this CompoundShapeChild obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.Transform;
			}
		}

		public static OpenTK.Matrix4 GetTransform(this CompoundShapeChild obj)
		{
			OpenTK.Matrix4 value;
			GetTransform(obj, out value);
			return value;
		}

		public unsafe static void SetTransform(this CompoundShapeChild obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.Transform = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetTransform(this CompoundShapeChild obj, OpenTK.Matrix4 value)
		{
			SetTransform(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CompoundShapeExtensions
	{
		public unsafe static void AddChildShape(this CompoundShape obj, ref OpenTK.Matrix4 localTransform, CollisionShape shape)
		{
			fixed (OpenTK.Matrix4* localTransformPtr = &localTransform)
			{
				obj.AddChildShape(ref *(BulletSharp.Math.Matrix*)localTransformPtr, shape);
			}
		}

		public unsafe static void CalculatePrincipalAxisTransform(this CompoundShape obj, float[] masses, ref OpenTK.Matrix4 principal, ref OpenTK.Vector3 inertia)
		{
			fixed (OpenTK.Matrix4* principalPtr = &principal)
			{
				fixed (OpenTK.Vector3* inertiaPtr = &inertia)
				{
					obj.CalculatePrincipalAxisTransform(masses, ref *(BulletSharp.Math.Matrix*)principalPtr, ref *(BulletSharp.Math.Vector3*)inertiaPtr);
				}
			}
		}

		public unsafe static void UpdateChildTransform(this CompoundShape obj, int childIndex, ref OpenTK.Matrix4 newChildTransform, bool shouldRecalculateLocalAabb)
		{
			fixed (OpenTK.Matrix4* newChildTransformPtr = &newChildTransform)
			{
				obj.UpdateChildTransform(childIndex, *(BulletSharp.Math.Matrix*)newChildTransformPtr, shouldRecalculateLocalAabb);
			}
		}

		public unsafe static void UpdateChildTransform(this CompoundShape obj, int childIndex, ref OpenTK.Matrix4 newChildTransform)
		{
			fixed (OpenTK.Matrix4* newChildTransformPtr = &newChildTransform)
			{
				obj.UpdateChildTransform(childIndex, *(BulletSharp.Math.Matrix*)newChildTransformPtr);
			}
		}
	}
}
