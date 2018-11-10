using System;

namespace BulletSharp
{
	public abstract class FixedSizeArray<T> : BulletObject
	{
		protected internal FixedSizeArray(IntPtr native, int count)
		{
			Initialize(native);
			Count = count;
		}

		public int Count { get; protected set; }
		public bool IsReadOnly => false;

		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}
	}
}
