using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CollisionObjectWrapperExtensions
	{
		public unsafe static void GetWorldTransform(this CollisionObjectWrapper obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.WorldTransform;
			}
		}

		public static OpenTK.Matrix4 GetWorldTransform(this CollisionObjectWrapper obj)
		{
			OpenTK.Matrix4 value;
			GetWorldTransform(obj, out value);
			return value;
		}
	}
}
