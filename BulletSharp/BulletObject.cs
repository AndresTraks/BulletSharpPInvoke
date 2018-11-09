using System;

namespace BulletSharp
{
	public abstract class BulletObject
	{
		internal IntPtr Native;

		protected internal void Initialize(IntPtr native)
		{
			if (Native == null)
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
			BulletObjectTracker.Add(this);
		}

		// Initialize an object that is part of another object or deleted by another object.
		// These objects should not be deleted in the Dispose method of this wrapper class.
		protected internal void InitializeSubObject(IntPtr native, BulletObject owner)
		{
			Initialize(native);
			Owner = owner;
			BulletObjectTracker.Add(this);
		}

		public bool IsDisposed { get; private set; }

		internal BulletObject Owner { get; private set; }

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
