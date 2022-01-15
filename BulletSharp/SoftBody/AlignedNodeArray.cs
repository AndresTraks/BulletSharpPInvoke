using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class AlignedNodeArrayDebugView
	{
		private AlignedNodeArray _array;

		public AlignedNodeArrayDebugView(AlignedNodeArray array)
		{
			_array = array;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public Node[] Items
		{
			get
			{
				int count = _array.Count;
				var array = new Node[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = _array[i];
				}
				return array;
			}
		}
	}

	public struct AlignedNodeArrayEnumerator : IEnumerator<Node>
	{
		private int _i;
		private int _count;
		private AlignedNodeArray _array;

		public AlignedNodeArrayEnumerator(AlignedNodeArray array)
		{
			_array = array;
			_count = array.Count;
			_i = -1;
		}

		public Node Current => _array[_i];

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

	[Serializable, DebuggerTypeProxy(typeof(AlignedNodeArrayDebugView)), DebuggerDisplay("Count = {Count}")]
	public class AlignedNodeArray : BulletObject, IList<Node>
	{
		internal AlignedNodeArray(IntPtr native)
		{
			Initialize(native);
		}

		public int IndexOf(Node item)
		{
			return btAlignedObjectArray_btSoftBody_Node_index_of(Native, item.Native);
		}

		public void Insert(int index, Node item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public Node this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				return new Node(btAlignedObjectArray_btSoftBody_Node_at(Native, index));
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void Add(Node item)
		{
			btAlignedObjectArray_btSoftBody_Node_push_back(Native, item.Native);
		}

		public void Clear()
		{
			btAlignedObjectArray_btSoftBody_Node_resizeNoInitialize(Native, 0);
		}

		public bool Contains(Node item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Node[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count => btAlignedObjectArray_btSoftBody_Node_size(Native);

		public bool IsReadOnly => false;

		public bool Remove(Node item)
		{
			throw new NotImplementedException();
		}

		public AlignedNodeArrayEnumerator GetEnumerator()
		{
			return new AlignedNodeArrayEnumerator(this);
		}

		IEnumerator<Node> IEnumerable<Node>.GetEnumerator()
		{
			return new AlignedNodeArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new AlignedNodeArrayEnumerator(this);
		}
	}
}
