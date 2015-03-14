using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class StaticPlaneShapeExtensions
	{
		public unsafe static void GetPlaneNormal(this StaticPlaneShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.PlaneNormal;
			}
		}

		public static OpenTK.Vector3 GetPlaneNormal(this StaticPlaneShape obj)
		{
			OpenTK.Vector3 value;
			GetPlaneNormal(obj, out value);
			return value;
		}
	}
}
