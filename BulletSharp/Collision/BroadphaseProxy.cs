using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum BroadphaseNativeType
	{
		BoxShape,
		TriangleShape,
		TetrahedralShape,
		ConvexTriangleMeshShape,
		ConvexHullShape,
		CONVEX_POINT_CLOUD_SHAPE_PROXYTYPE,
		CUSTOM_POLYHEDRAL_SHAPE_TYPE,
		IMPLICIT_CONVEX_SHAPES_START_HERE,
		SphereShape,
		MultiSphereShape,
		CapsuleShape,
		ConeShape,
		ConvexShape,
		CylinderShape,
		UniformScalingShape,
		MinkowskiSumShape,
		MinkowskiDifferenceShape,
		Box2DShape,
		Convex2DShape,
		CUSTOM_CONVEX_SHAPE_TYPE,
		CONCAVE_SHAPES_START_HERE,
		TriangleMeshShape,
		ScaledTriangleMeshShape,
		FAST_CONCAVE_MESH_PROXYTYPE,
		TerrainShape,
		GImpactShape,
		MultiMaterialTriangleMesh,
		EmptyShape,
		StaticPlaneShape,
		CUSTOM_CONCAVE_SHAPE_TYPE,
		CONCAVE_SHAPES_END_HERE,
		CompoundShape,
		SoftBodyShape,
		HFFLUID_SHAPE_PROXYTYPE,
		HFFLUID_BUOYANT_CONVEX_SHAPE_PROXYTYPE,
		INVALID_SHAPE_PROXYTYPE,
		MAX_BROADPHASE_COLLISION_TYPES
	}

	[Flags]
	public enum CollisionFilterGroups
	{
		None = 0,
		DefaultFilter = 1,
		StaticFilter = 2,
		KinematicFilter = 4,
		DebrisFilter = 8,
		SensorTrigger = 16,
		CharacterFilter = 32,
		AllFilter = -1
	}

	public class BroadphaseProxy : BulletObject
	{
		private BulletObject _clientObject;

		internal BroadphaseProxy(IntPtr native)
		{
			Initialize(native);
		}

		internal static BroadphaseProxy GetManaged(IntPtr native)
		{
			if (native == IntPtr.Zero)
			{
				return null;
			}

			IntPtr clientObjectPtr = btBroadphaseProxy_getClientObject(native);
			if (clientObjectPtr != IntPtr.Zero) {
				CollisionObject clientObject = CollisionObject.GetManaged(clientObjectPtr);
				return clientObject.BroadphaseHandle;
			}

			throw new InvalidOperationException("Unknown broadphase proxy!");
			//return new BroadphaseProxy(native);
		}

		public static bool IsCompound(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isCompound(proxyType);
		}

		public static bool IsConcave(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isConcave(proxyType);
		}

		public static bool IsConvex(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isConvex(proxyType);
		}

		public static bool IsConvex2D(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isConvex2d(proxyType);
		}

		public static bool IsInfinite(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isInfinite(proxyType);
		}

		public static bool IsNonMoving(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isNonMoving(proxyType);
		}

		public static bool IsPolyhedral(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isPolyhedral(proxyType);
		}

		public static bool IsSoftBody(BroadphaseNativeType proxyType)
		{
			return btBroadphaseProxy_isSoftBody(proxyType);
		}

		public Vector3 AabbMax
		{
			get
			{
				Vector3 value;
				btBroadphaseProxy_getAabbMax(Native, out value);
				return value;
			}
			set => btBroadphaseProxy_setAabbMax(Native, ref value);
		}

		public Vector3 AabbMin
		{
			get
			{
				Vector3 value;
				btBroadphaseProxy_getAabbMin(Native, out value);
				return value;
			}
			set => btBroadphaseProxy_setAabbMin(Native, ref value);
		}

		public BulletObject ClientObject
		{
			get
			{
				IntPtr clientObjectPtr = btBroadphaseProxy_getClientObject(Native);
				if (clientObjectPtr != IntPtr.Zero)
				{
					_clientObject = CollisionObject.GetManaged(clientObjectPtr);
				}
				return _clientObject;
			}
			set
			{
				var collisionObject = value as CollisionObject;
				if (collisionObject != null)
				{
					btBroadphaseProxy_setClientObject(Native, collisionObject.Native);
				}
				else if (value == null)
				{
					btBroadphaseProxy_setClientObject(Native, IntPtr.Zero);
				}
				_clientObject = value;
			}
		}

		public int CollisionFilterGroup
		{
			get => btBroadphaseProxy_getCollisionFilterGroup(Native);
			set => btBroadphaseProxy_setCollisionFilterGroup(Native, value);
		}

		public int CollisionFilterMask
		{
			get => btBroadphaseProxy_getCollisionFilterMask(Native);
			set => btBroadphaseProxy_setCollisionFilterMask(Native, value);
		}

		public int Uid => btBroadphaseProxy_getUid(Native);

		public int UniqueId
		{
			get => btBroadphaseProxy_getUniqueId(Native);
			set => btBroadphaseProxy_setUniqueId(Native, value);
		}
	}

	public class BroadphasePair : BulletObject
	{
		internal BroadphasePair(IntPtr native)
		{
			Initialize(native);
		}

		public CollisionAlgorithm Algorithm
		{
			get
			{
				IntPtr valuePtr = btBroadphasePair_getAlgorithm(Native);
				return (valuePtr == IntPtr.Zero) ? null : new CollisionAlgorithm(valuePtr, this);
			}
			set => btBroadphasePair_setAlgorithm(Native, (value.Native == IntPtr.Zero) ? IntPtr.Zero : value.Native);
		}

		public BroadphaseProxy Proxy0
		{
			get => BroadphaseProxy.GetManaged(btBroadphasePair_getPProxy0(Native));
			set => btBroadphasePair_setPProxy0(Native, value.Native);
		}

		public BroadphaseProxy Proxy1
		{
			get => BroadphaseProxy.GetManaged(btBroadphasePair_getPProxy1(Native));
			set => btBroadphasePair_setPProxy1(Native, value.Native);
		}
	}
}
