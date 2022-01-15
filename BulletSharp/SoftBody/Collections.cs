﻿using System;
using System.Collections.Generic;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public struct NodePtrArrayEnumerator : IEnumerator<Node>
	{
		private int _i;
		private int _count;
		private IList<Node> _array;

		public NodePtrArrayEnumerator(IList<Node> array)
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

		public Node Current => _array[_i];

		object System.Collections.IEnumerator.Current => _array[_i];
	}

	public class NodePtrArray : FixedSizeArray<Node>, IList<Node>
	{
		internal NodePtrArray(IntPtr native, int count)
			: base(native, count)
		{
		}

		public int IndexOf(Node item)
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
				return new Node(btSoftBodyNodePtrArray_at(Native, index));
			}
			set
			{
				btSoftBodyNodePtrArray_set(Native, value.Native, index);
			}
		}

		public bool Contains(Node item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Node[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public NodePtrArrayEnumerator GetEnumerator()
		{
			return new NodePtrArrayEnumerator(this);
		}

		IEnumerator<Node> IEnumerable<Node>.GetEnumerator()
		{
			return new NodePtrArrayEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new NodePtrArrayEnumerator(this);
		}
	}
}
