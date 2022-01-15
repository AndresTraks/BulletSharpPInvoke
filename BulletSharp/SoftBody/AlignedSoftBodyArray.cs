﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class AlignedSoftBodyArrayDebugView
	{
		private AlignedSoftBodyArray _array;

		public AlignedSoftBodyArrayDebugView(AlignedSoftBodyArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public SoftBody[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new SoftBody[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public struct AlignedSoftBodyArrayEnumerator : IEnumerator<SoftBody>
	{
		private int _i;
		private int _count;
		private AlignedSoftBodyArray _array;

		public AlignedSoftBodyArrayEnumerator(AlignedSoftBodyArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public SoftBody Current => _array[_i];

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

	[Serializable, DebuggerTypeProxy(typeof(AlignedSoftBodyArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedSoftBodyArray : BulletObject, IList<SoftBody>
	{
		internal AlignedSoftBodyArray(IntPtr native)
		{
			Initialize(native);
		}

		public int IndexOf(SoftBody item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, SoftBody item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public SoftBody this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return CollisionObject.GetManaged(btAlignedObjectArray_btSoftBodyPtr_at(Native, index)) as SoftBody;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(SoftBody item)
		{
			btAlignedObjectArray_btSoftBodyPtr_push_back(Native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBodyPtr_resizeNoInitialize(Native, 0);
		}

		public bool Contains(SoftBody item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(SoftBody[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBodyPtr_size(Native);

		public bool IsReadOnly => false;

		public bool Remove(SoftBody item)
		{
			throw new NotImplementedException();
		}

		public AlignedSoftBodyArrayEnumerator GetEnumerator()
		{
			return new AlignedSoftBodyArrayEnumerator(this);
		}

		IEnumerator<SoftBody> IEnumerable<SoftBody>.GetEnumerator()
		{
			return new AlignedSoftBodyArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedSoftBodyArrayEnumerator(this);
		}
	}
}
