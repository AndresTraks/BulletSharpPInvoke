using System;
using System.Runtime.InteropServices;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class CollisionShape : BulletDisposableObject
	{
		protected internal CollisionShape()
		{
		}

		protected internal void InitializeCollisionShape(IntPtr native, BulletObject owner = null)
		{
			if (owner != null)
			{
				InitializeSubObject(native, owner);
			}
			else
			{
				InitializeUserOwned(native);

				AllocateUnmanagedHandle();
			}
		}

		internal static CollisionShape GetManaged(IntPtr obj)
		{
			if (obj == IntPtr.Zero)
			{
				return null;
			}

			IntPtr userPtr = btCollisionShape_getUserPointer(obj);
			if (userPtr != IntPtr.Zero)
			{
				return GCHandle.FromIntPtr(userPtr).Target as CollisionShape;
			}

			throw new InvalidOperationException("Unknown collision object!");
		}

		public Vector3 CalculateLocalInertia(float mass)
		{
			Vector3 inertia;
			btCollisionShape_calculateLocalInertia(Native, mass, out inertia);
			return inertia;
		}

		public void CalculateLocalInertia(float mass, out Vector3 inertia)
		{
			btCollisionShape_calculateLocalInertia(Native, mass, out inertia);
		}

		public int CalculateSerializeBufferSize()
		{
			return btCollisionShape_calculateSerializeBufferSize(Native);
		}

		public void CalculateTemporalAabbRef(ref Matrix4x4 curTrans, ref Vector3 linvel, ref Vector3 angvel,
			float timeStep, out Vector3 temporalAabbMin, out Vector3 temporalAabbMax)
		{
			btCollisionShape_calculateTemporalAabb(Native, ref curTrans, ref linvel, ref angvel, timeStep, out temporalAabbMin, out temporalAabbMax);
		}

		public void CalculateTemporalAabb(Matrix4x4 curTrans, Vector3 linvel, Vector3 angvel,
			float timeStep, out Vector3 temporalAabbMin, out Vector3 temporalAabbMax)
		{
			btCollisionShape_calculateTemporalAabb(Native, ref curTrans, ref linvel,
				ref angvel, timeStep, out temporalAabbMin, out temporalAabbMax);
		}

		public void GetAabbRef(ref Matrix4x4 transformation, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btCollisionShape_getAabb(Native, ref transformation, out aabbMin, out aabbMax);
		}

		public void GetAabb(Matrix4x4 transformation, out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btCollisionShape_getAabb(Native, ref transformation, out aabbMin, out aabbMax);
		}

		public void GetBoundingSphere(out Vector3 center, out float radius)
		{
			btCollisionShape_getBoundingSphere(Native, out center, out radius);
		}

		public float GetContactBreakingThreshold(float defaultContactThresholdFactor)
		{
			return btCollisionShape_getContactBreakingThreshold(Native, defaultContactThresholdFactor);
		}

		public virtual string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btCollisionShape_serialize(Native, dataBuffer, serializer.Native));
			/*
			IntPtr name = serializer.FindNameForPointer(_native);
			IntPtr namePtr = serializer.GetUniquePointer(name);
			Marshal.WriteIntPtr(dataBuffer, namePtr);
			if (namePtr != IntPtr.Zero)
			{
				serializer.SerializeName(name);
			}
			Marshal.WriteInt32(dataBuffer, IntPtr.Size, (int)ShapeType);
			//Marshal.WriteInt32(dataBuffer, IntPtr.Size + sizeof(int), 0); //padding
			return "btCollisionShapeData";
			*/
		}

		public void SerializeSingleShape(Serializer serializer)
		{
			int len = CalculateSerializeBufferSize();
			Chunk chunk = serializer.Allocate((uint)len, 1);
			string structType = Serialize(chunk.OldPtr, serializer);
			serializer.FinalizeChunk(chunk, structType, DnaID.Shape, Native);
		}

		public float AngularMotionDisc => btCollisionShape_getAngularMotionDisc(Native);

		public Vector3 AnisotropicRollingFrictionDirection
		{
			get
			{
				Vector3 value;
				btCollisionShape_getAnisotropicRollingFrictionDirection(Native, out value);
				return value;
			}
		}

		public bool IsCompound => btCollisionShape_isCompound(Native);

		public bool IsConcave => btCollisionShape_isConcave(Native);

		public bool IsConvex => btCollisionShape_isConvex(Native);

		public bool IsConvex2D => btCollisionShape_isConvex2d(Native);

		public bool IsInfinite => btCollisionShape_isInfinite(Native);

		public bool IsNonMoving => btCollisionShape_isNonMoving(Native);

		public bool IsPolyhedral => btCollisionShape_isPolyhedral(Native);

		public bool IsSoftBody => btCollisionShape_isSoftBody(Native);

		public Vector3 LocalScaling
		{
			get
			{
				Vector3 value;
				btCollisionShape_getLocalScaling(Native, out value);
				return value;
			}
			set => btCollisionShape_setLocalScaling(Native, ref value);
		}

		public float Margin
		{
			get => btCollisionShape_getMargin(Native);
			set => btCollisionShape_setMargin(Native, value);
		}

		public string Name => Marshal.PtrToStringAnsi(btCollisionShape_getName(Native));

		public BroadphaseNativeType ShapeType => btCollisionShape_getShapeType(Native);

		public object UserObject { get; set; }

		public int UserIndex
		{
			get => btCollisionShape_getUserIndex(Native);
			set => btCollisionShape_setUserIndex(Native, value);
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj);
		}

		public override int GetHashCode()
		{
			return Native.GetHashCode();
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				FreeUnmanagedHandle();

				btCollisionShape_delete(Native);
			}
		}

		internal void AllocateUnmanagedHandle()
		{
			GCHandle handle = GCHandle.Alloc(this, GCHandleType.Weak);
			btCollisionShape_setUserPointer(Native, GCHandle.ToIntPtr(handle));
		}

		internal void FreeUnmanagedHandle()
		{
			IntPtr userPtr = btCollisionShape_getUserPointer(Native);
			GCHandle.FromIntPtr(userPtr).Free();
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct CollisionShapeData
	{
		public IntPtr Name;
		public int ShapeType;
		public int Padding;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(CollisionShapeData), fieldName).ToInt32(); }
	}
}
