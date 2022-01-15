using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public struct DbvtArrayEnumerator : IEnumerator<Dbvt>
	{
		private int _i;
		private int _count;
		private IList<Dbvt> _array;

		public DbvtArrayEnumerator(IList<Dbvt> array)
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

		public Dbvt Current => _array[_i];

		object System.Collections.IEnumerator.Current => _array[_i];
	}

	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView))]
	public class DbvtArray : FixedSizeArray<Dbvt>, IList<Dbvt>
	{
		internal DbvtArray(IntPtr native, int count)
			: base(native, count)
		{
		}

		public int IndexOf(Dbvt item)
		{
			return btDbvt_array_index_of(Native, item != null ? item.Native : IntPtr.Zero, Count);
		}

		public Dbvt this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				IntPtr ptr = btDbvt_array_at(Native, index);
				return new Dbvt(ptr);
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public bool Contains(Dbvt item)
		{
			return IndexOf(item) != -1;
		}

		public void CopyTo(Dbvt[] array, int arrayIndex)
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

		public DbvtArrayEnumerator GetEnumerator()
		{
			return new DbvtArrayEnumerator(this);
		}

		IEnumerator<Dbvt> IEnumerable<Dbvt>.GetEnumerator()
		{
			return new DbvtArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new DbvtArrayEnumerator(this);
		}
	}
}
