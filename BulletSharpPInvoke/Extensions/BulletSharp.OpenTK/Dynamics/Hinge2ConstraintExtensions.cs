using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Hinge2ConstraintExtensions
	{
		public unsafe static void GetAnchor(this Hinge2Constraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Anchor;
			}
		}

		public static OpenTK.Vector3 GetAnchor(this Hinge2Constraint obj)
		{
			OpenTK.Vector3 value;
			GetAnchor(obj, out value);
			return value;
		}

		public unsafe static void GetAnchor2(this Hinge2Constraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Anchor2;
			}
		}

		public static OpenTK.Vector3 GetAnchor2(this Hinge2Constraint obj)
		{
			OpenTK.Vector3 value;
			GetAnchor2(obj, out value);
			return value;
		}

		public unsafe static void GetAxis1(this Hinge2Constraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Axis1;
			}
		}

		public static OpenTK.Vector3 GetAxis1(this Hinge2Constraint obj)
		{
			OpenTK.Vector3 value;
			GetAxis1(obj, out value);
			return value;
		}

		public unsafe static void GetAxis2(this Hinge2Constraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Axis2;
			}
		}

		public static OpenTK.Vector3 GetAxis2(this Hinge2Constraint obj)
		{
			OpenTK.Vector3 value;
			GetAxis2(obj, out value);
			return value;
		}
	}
}
