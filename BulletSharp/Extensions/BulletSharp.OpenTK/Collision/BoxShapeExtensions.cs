using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BoxShapeExtensions
	{
		public unsafe static void GetHalfExtentsWithMargin(this BoxShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HalfExtentsWithMargin;
			}
		}

		public static OpenTK.Vector3 GetHalfExtentsWithMargin(this BoxShape obj)
		{
			OpenTK.Vector3 value;
			GetHalfExtentsWithMargin(obj, out value);
			return value;
		}

		public unsafe static void GetHalfExtentsWithoutMargin(this BoxShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.HalfExtentsWithoutMargin;
			}
		}

		public static OpenTK.Vector3 GetHalfExtentsWithoutMargin(this BoxShape obj)
		{
			OpenTK.Vector3 value;
			GetHalfExtentsWithoutMargin(obj, out value);
			return value;
		}
	}
}
