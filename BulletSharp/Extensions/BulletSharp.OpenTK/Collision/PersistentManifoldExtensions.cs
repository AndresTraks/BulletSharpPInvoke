using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class PersistentManifoldExtensions
	{
		public unsafe static void RefreshContactPoints(this PersistentManifold obj, ref OpenTK.Matrix4 trA, ref OpenTK.Matrix4 trB)
		{
			fixed (OpenTK.Matrix4* trAPtr = &trA)
			{
				fixed (OpenTK.Matrix4* trBPtr = &trB)
				{
					obj.RefreshContactPoints(ref *(BulletSharp.Math.Matrix*)trAPtr, ref *(BulletSharp.Math.Matrix*)trBPtr);
				}
			}
		}
	}
}
