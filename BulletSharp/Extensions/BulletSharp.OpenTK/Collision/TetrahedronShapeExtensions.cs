using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BU_Simplex1to4Extensions
	{
		public unsafe static void AddVertex(this BuSimplex1To4 obj, ref OpenTK.Vector3 pt)
		{
			fixed (OpenTK.Vector3* ptPtr = &pt)
			{
				obj.AddVertex(ref *(BulletSharp.Math.Vector3*)ptPtr);
			}
		}
	}
}
