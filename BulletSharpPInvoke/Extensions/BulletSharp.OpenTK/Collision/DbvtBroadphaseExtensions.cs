using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class DbvtBroadphaseExtensions
	{
		public unsafe static void SetAabbForceUpdate(this DbvtBroadphase obj, BroadphaseProxy absproxy, ref OpenTK.Vector3 aabbMin, ref OpenTK.Vector3 aabbMax, Dispatcher __unnamed3)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
					obj.SetAabbForceUpdate(absproxy, ref *(BulletSharp.Math.Vector3*)aabbMinPtr, ref *(BulletSharp.Math.Vector3*)aabbMaxPtr, __unnamed3);
				}
			}
		}
	}
}
