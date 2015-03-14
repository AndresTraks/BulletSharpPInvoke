using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CylinderShapeExtensions
	{
		public unsafe static void GetHalfExtentsWithMargin(this CylinderShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HalfExtentsWithMargin;
			}
		}

		public static OpenTK.Vector3 GetHalfExtentsWithMargin(this CylinderShape obj)
		{
			OpenTK.Vector3 value;
			GetHalfExtentsWithMargin(obj, out value);
			return value;
		}

		public unsafe static void GetHalfExtentsWithoutMargin(this CylinderShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HalfExtentsWithoutMargin;
			}
		}

		public static OpenTK.Vector3 GetHalfExtentsWithoutMargin(this CylinderShape obj)
		{
			OpenTK.Vector3 value;
			GetHalfExtentsWithoutMargin(obj, out value);
			return value;
		}
	}
}
