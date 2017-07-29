using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConvexInternalShapeExtensions
	{
		public unsafe static void GetImplicitShapeDimensions(this ConvexInternalShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.ImplicitShapeDimensions;
			}
		}

		public static OpenTK.Vector3 GetImplicitShapeDimensions(this ConvexInternalShape obj)
		{
			OpenTK.Vector3 value;
			GetImplicitShapeDimensions(obj, out value);
			return value;
		}

		public unsafe static void GetLocalScalingNV(this ConvexInternalShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalScalingNV;
			}
		}

		public static OpenTK.Vector3 GetLocalScalingNV(this ConvexInternalShape obj)
		{
			OpenTK.Vector3 value;
			GetLocalScalingNV(obj, out value);
			return value;
		}

		public unsafe static void SetImplicitShapeDimensions(this ConvexInternalShape obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.ImplicitShapeDimensions = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetImplicitShapeDimensions(this ConvexInternalShape obj, OpenTK.Vector3 value)
		{
			SetImplicitShapeDimensions(obj, ref value);
		}

		public unsafe static void SetSafeMargin(this ConvexInternalShape obj, ref OpenTK.Vector3 halfExtents, float defaultMarginMultiplier)
		{
			fixed (OpenTK.Vector3* halfExtentsPtr = &halfExtents)
			{
				obj.SetSafeMargin(ref *(BulletSharp.Math.Vector3*)halfExtentsPtr, defaultMarginMultiplier);
			}
		}

		public unsafe static void SetSafeMargin(this ConvexInternalShape obj, ref OpenTK.Vector3 halfExtents)
		{
			fixed (OpenTK.Vector3* halfExtentsPtr = &halfExtents)
			{
				obj.SetSafeMargin(ref *(BulletSharp.Math.Vector3*)halfExtentsPtr);
			}
		}
	}
}
