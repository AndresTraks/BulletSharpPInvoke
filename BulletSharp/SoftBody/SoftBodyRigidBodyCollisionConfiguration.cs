using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public sealed class SoftBodyRigidBodyCollisionConfiguration : DefaultCollisionConfiguration
	{
		public SoftBodyRigidBodyCollisionConfiguration()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btSoftBodyRigidBodyCollisionConfiguration_new();
			InitializeUserOwned(native);
		}

		public SoftBodyRigidBodyCollisionConfiguration(DefaultCollisionConstructionInfo constructionInfo)
			: base(ConstructionInfo.Null)
		{
			if (constructionInfo == null)
			{
				throw new ArgumentNullException(nameof(constructionInfo));
			}

			IntPtr native = btSoftBodyRigidBodyCollisionConfiguration_new2(constructionInfo.Native);
			InitializeUserOwned(Native);

			_collisionAlgorithmPool = constructionInfo.CollisionAlgorithmPool;
			_persistentManifoldPool = constructionInfo.PersistentManifoldPool;
		}

		public override CollisionAlgorithmCreateFunc GetCollisionAlgorithmCreateFunc(BroadphaseNativeType proxyType0,
			BroadphaseNativeType proxyType1)
		{
			IntPtr createFunc = btCollisionConfiguration_getCollisionAlgorithmCreateFunc(Native, (int)proxyType0, (int)proxyType1);
			if (proxyType0 == BroadphaseNativeType.SoftBodyShape && proxyType1 == BroadphaseNativeType.SoftBodyShape)
			{
				return new SoftSoftCollisionAlgorithm.CreateFunc(createFunc, this);
			}
			if (proxyType0 == BroadphaseNativeType.SoftBodyShape && BroadphaseProxy.IsConvex(proxyType1))
			{
				return new SoftRigidCollisionAlgorithm.CreateFunc(createFunc, this);
			}
			if (BroadphaseProxy.IsConvex(proxyType0) && proxyType1 == BroadphaseNativeType.SoftBodyShape)
			{
				return new SoftRigidCollisionAlgorithm.CreateFunc(createFunc, this);
			}
			if (proxyType0 == BroadphaseNativeType.SoftBodyShape && BroadphaseProxy.IsConcave(proxyType1))
			{
				return new SoftBodyConcaveCollisionAlgorithm.CreateFunc(createFunc, this);
			}
			if (BroadphaseProxy.IsConcave(proxyType0) && proxyType1 == BroadphaseNativeType.SoftBodyShape)
			{
				return new SoftBodyConcaveCollisionAlgorithm.SwappedCreateFunc(createFunc, this);
			}
			return base.GetCollisionAlgorithmCreateFunc(proxyType0, proxyType1);
		}
	}
}
