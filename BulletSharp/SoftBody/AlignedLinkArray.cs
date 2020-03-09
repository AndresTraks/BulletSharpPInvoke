﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class AlignedLinkArrayDebugView
	{
		private AlignedLinkArray _array;

		public AlignedLinkArrayDebugView(AlignedLinkArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Link[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Link[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public class AlignedLinkArrayEnumerator : IEnumerator<Link>
	{
		private int _i;
		private int _count;
		private AlignedLinkArray _array;

		public AlignedLinkArrayEnumerator(AlignedLinkArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Link Current => _array[_i];

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

	[Serializable, DebuggerTypeProxy(typeof(AlignedLinkArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedLinkArray : BulletObject, IList<Link>
	{
		internal AlignedLinkArray(IntPtr native)
		{
			Initialize(native);
		}

		public int IndexOf(Link item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Link item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Link this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Link(btAlignedObjectArray_btSoftBody_Link_at(Native, index));
			}
			set
			{
				btAlignedObjectArray_btSoftBody_Link_set(Native, value.Native, index);
			}
		}

		public void Add(Link item)
		{
			btAlignedObjectArray_btSoftBody_Link_push_back(Native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_Link_resizeNoInitialize(Native, 0);
		}

		public bool Contains(Link item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Link[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			int count = Count;
			if (array.Length - arrayIndex < count)
			{
				throw new ArgumentException("The number of elements in the source is greater than the available space from arrayIndex to the end of the destination array.");
			}

			for (int i = 0; i < count; i++)
			{
				array.SetValue(new Link(btAlignedObjectArray_btSoftBody_Link_at(Native, i)), i + arrayIndex);
			}
		}

		public int Count => btAlignedObjectArray_btSoftBody_Link_size(Native);

		public bool IsReadOnly => false;

		public bool Remove(Link item)
		{
			throw new NotImplementedException();
		}

		public IEnumerator<Link> GetEnumerator()
		{
			return new AlignedLinkArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedLinkArrayEnumerator(this);
		}
	}
}
