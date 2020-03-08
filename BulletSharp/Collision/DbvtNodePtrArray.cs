using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class DbvtNodePtrArrayEnumerator : IEnumerator<DbvtNode>
	{
		private int _i;
		private int _count;
		private IList<DbvtNode> _array;

		public DbvtNodePtrArrayEnumerator(IList<DbvtNode> array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			_i++;
			return _i != _count;
		}

		public void Reset()
		{
			_i = -1;
		}

		public DbvtNode Current => _array[_i];

		object System.Collections.IEnumerator.Current => _array[_i];
	}

	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView))]
	public class DbvtNodePtrArray : FixedSizeArray<DbvtNode>, IList<DbvtNode>
	{
		internal DbvtNodePtrArray(IntPtr native, int count)
			: base(native, count)
		{
		}

		public int IndexOf(DbvtNode item)
		{
			return btDbvtNodePtr_array_index_of(Native, item != null ? item.Native : IntPtr.Zero, Count);
		}

		public DbvtNode this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				IntPtr ptr = btDbvtNodePtr_array_at(Native, index);
				return (ptr != IntPtr.Zero) ? new DbvtNode(ptr) : null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public bool Contains(DbvtNode item)
		{
			return IndexOf(item) != -1;
		}

		public void CopyTo(DbvtNode[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException(nameof(array));

			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(array));

			int count = Count;
			if (arrayIndex + count > array.Length)
				throw new ArgumentException("Array too small.", "array");

			for (int i = 0; i < count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		public IEnumerator<DbvtNode> GetEnumerator()
		{
			return new DbvtNodePtrArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new DbvtNodePtrArrayEnumerator(this);
		}
	}
}
