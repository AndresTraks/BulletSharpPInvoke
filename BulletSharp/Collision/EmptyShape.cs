using System;
using static BulletSharp.UnsafeNativeMethods;
namespace BulletSharp
{
	public class EmptyShape : ConcaveShape
	{
		public EmptyShape()
		{
			IntPtr native = btEmptyShape_new();
			InitializeCollisionShape(native);
		}
	}
}
