using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class AlignedTetraScratchArrayDebugView
	{
		private AlignedTetraScratchArray _array;

		public AlignedTetraScratchArrayDebugView(AlignedTetraScratchArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TetraScratch[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new TetraScratch[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedTetraScratchArrayEnumerator : IEnumerator<TetraScratch>
	{
		private int _i;
		private int _count;
		private AlignedTetraScratchArray _array;

		public AlignedTetraScratchArrayEnumerator(AlignedTetraScratchArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public TetraScratch Current => _array[_i];

		public void Dispose()
		{
		}

		object System.Collections.IEnumerator.Current => _array[_i];

		public bool MoveNext()
		{
			_i++;
			return _i != _count;
		}

		public void Reset()
		{
			_i = 0;
		}
	}

	[Serializable, DebuggerTypeProxy(typeof(AlignedTetraScratchArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedTetraScratchArray : BulletObject, IList<TetraScratch>
	{
		internal AlignedTetraScratchArray(IntPtr native)
		{
			Initialize(native);
		}

		public int IndexOf(TetraScratch item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, TetraScratch item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public TetraScratch this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new TetraScratch(btAlignedObjectArray_btSoftBody_TetraScratch_at(Native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(TetraScratch item)
		{
			btAlignedObjectArray_btSoftBody_TetraScratch_push_back(Native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_TetraScratch_resizeNoInitialize(Native, 0);
		}

		public bool Contains(TetraScratch item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(TetraScratch[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBody_TetraScratch_size(Native);

		public bool IsReadOnly => false;

		public void Resize(int newSize)
		{
			btAlignedObjectArray_btSoftBody_TetraScratch_resize(Native, newSize);
		}

		public bool Remove(TetraScratch item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<TetraScratch> GetEnumerator()
		{
			return new AlignedTetraScratchArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedTetraScratchArrayEnumerator(this);
		}
	}
}
