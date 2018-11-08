using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class CollisionDispatcherMultiThreaded : CollisionDispatcher
	{
		public CollisionDispatcherMultiThreaded(CollisionConfiguration configuration, int grainSize = 40)
		{
			IntPtr native = btCollisionDispatcherMt_new(configuration.Native, grainSize);
			InitializeUserOwned(native);

			_collisionConfiguration = configuration;
		}
	}
}
