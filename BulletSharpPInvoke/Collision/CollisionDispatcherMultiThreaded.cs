namespace BulletSharp
{
	public class CollisionDispatcherMultiThreaded : CollisionDispatcher
	{
		public CollisionDispatcherMultiThreaded(CollisionConfiguration configuration, int grainSize = 40)
			: base(UnsafeNativeMethods.btCollisionDispatcherMt_new(configuration._native, grainSize))
		{
			_collisionConfiguration = configuration;
		}
	}
}
