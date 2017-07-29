using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class GearConstraintExtensions
	{
		public unsafe static void GetAxisA(this GearConstraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AxisA;
			}
		}

		public static OpenTK.Vector3 GetAxisA(this GearConstraint obj)
		{
			OpenTK.Vector3 value;
			GetAxisA(obj, out value);
			return value;
		}

		public unsafe static void GetAxisB(this GearConstraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AxisB;
			}
		}

		public static OpenTK.Vector3 GetAxisB(this GearConstraint obj)
		{
			OpenTK.Vector3 value;
			GetAxisB(obj, out value);
			return value;
		}

		public unsafe static void SetAxisA(this GearConstraint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AxisA = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAxisA(this GearConstraint obj, OpenTK.Vector3 value)
		{
			SetAxisA(obj, ref value);
		}

		public unsafe static void SetAxisB(this GearConstraint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AxisB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAxisB(this GearConstraint obj, OpenTK.Vector3 value)
		{
			SetAxisB(obj, ref value);
		}
	}
}
