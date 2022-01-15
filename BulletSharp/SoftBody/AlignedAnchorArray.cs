using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class AlignedAnchorArrayDebugView
	{
		private AlignedAnchorArray _array;

		public AlignedAnchorArrayDebugView(AlignedAnchorArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Anchor[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Anchor[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public struct AlignedAnchorArrayEnumerator : IEnumerator<Anchor>
	{
		private int _i;
		private int _count;
		private AlignedAnchorArray _array;

		public AlignedAnchorArrayEnumerator(AlignedAnchorArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Anchor Current => _array[_i];

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
			_i = -1;
		}
	}

	[Serializable, DebuggerTypeProxy(typeof(AlignedAnchorArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedAnchorArray : BulletObject, IList<Anchor>
	{
		internal AlignedAnchorArray(IntPtr native)
		{
			Initialize(native);
		}

		public int IndexOf(Anchor item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Anchor item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Anchor this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Anchor(btAlignedObjectArray_btSoftBody_Anchor_at(Native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(Anchor item)
		{
			btAlignedObjectArray_btSoftBody_Anchor_push_back(Native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_Anchor_resizeNoInitialize(Native, 0);
		}

		public bool Contains(Anchor item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Anchor[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBody_Anchor_size(Native);

		public bool IsReadOnly => false;

		public bool Remove(Anchor item)
		{
			throw new NotImplementedException();
		}

		public AlignedAnchorArrayEnumerator GetEnumerator()
		{
			return new AlignedAnchorArrayEnumerator(this);
		}

		IEnumerator<Anchor> IEnumerable<Anchor>.GetEnumerator()
		{
			return new AlignedAnchorArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedAnchorArrayEnumerator(this);
		}
	}
}
