using System;

namespace BulletSharp
{
	public abstract class BulletObject
	{
		internal IntPtr Native;

		protected internal void Initialize(IntPtr native)
		{
			if (native == IntPtr.Zero)
			{
				throw new ArgumentNullException(nameof(native));
			}

			if (Native != IntPtr.Zero)
			{
				throw new InvalidOperationException("Bullet object already initialized.");
			}

			Native = native;
		}
	}

	public abstract class BulletDisposableObject : BulletObject, IDisposable
	{
		// Initialize an object that should be disposed by the user.
		protected internal void InitializeUserOwned(IntPtr native)
		{
			Initialize(native);
#if !BULLET_OBJECT_TRACKING
			IsUserOwned = true;
#endif
			BulletObjectTracker.Add(this);
		}

		// Initialize an object that is part of another object or deleted by another object.
		// These objects should not be deleted in the Dispose method of this wrapper class.
		protected internal void InitializeSubObject(IntPtr native, BulletObject owner)
		{
			Initialize(native);
#if BULLET_OBJECT_TRACKING
			Owner = owner;
#endif
			BulletObjectTracker.Add(this);
			GC.SuppressFinalize(this);
		}

		public bool IsDisposed { get; private set; }
#if BULLET_OBJECT_TRACKING
		internal BulletObject Owner { get; private set; }

		internal bool IsUserOwned => Owner == null;
#else
		internal bool IsUserOwned { get; private set; }
#endif

		public void Dispose()
		{
			if (IsDisposed == false)
			{
				Dispose(true);

				IsDisposed = true;
				BulletObjectTracker.Remove(this);

				GC.SuppressFinalize(this);
			}
		}

		protected abstract void Dispose(bool disposing);

		~BulletDisposableObject()
		{
			if (IsDisposed == false)
			{
				Dispose(false);

				IsDisposed = true;
				BulletObjectTracker.Remove(this);
			}
		}
	}

	// This class is used to differentiate between a public constructor
	// without parameters and an internal constructor that initializes a base class.
	internal sealed class ConstructionInfo
	{
		public static ConstructionInfo Null = null;
	}
}
