using System.ComponentModel;

namespace BulletSharp
{
    /*
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ShapeHullExtensions
	{
		public unsafe static void GetVertexPointer(this ShapeHull obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.VertexPointer;
			}
		}

		public static OpenTK.Vector3 GetVertexPointer(this ShapeHull obj)
		{
			OpenTK.Vector3 value;
			GetVertexPointer(obj, out value);
			return value;
		}
	}
    */
}
