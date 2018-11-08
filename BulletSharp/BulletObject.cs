using System;

namespace BulletSharp
{
	public abstract class BulletObject
	{
		internal IntPtr Native;
	}

	public abstract class BulletDisposableObject : BulletObject, IDisposable
	{
		// Initialize an object that should be disposed by the user.
		protected internal void InitializeUserOwned(IntPtr native)
		{
			if (Native == null)
			{
				throw new InvalidOperationException("Bullet object already initialized.");
			}

			Native = native;

			BulletObjectTracker.Add(this);
		}

		// Initialize an object that is part of another object or deleted by another object.
		// These objects should not be deleted in the Dispose method of this wrapper class.
		protected internal void InitializeSubObject(IntPtr native, BulletObject owner)
		{
			if (Native == null)
			{
				throw new InvalidOperationException("Bullet object already initialized.");
			}

			Native = native;
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

	internal sealed class ConstructionInfo
	{
		public static ConstructionInfo Null = null;
	}
}
