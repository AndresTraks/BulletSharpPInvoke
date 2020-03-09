using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(ListDebugView))]
	public sealed class BodyArray : FixedSizeArray<Body>, IList<Body>
	{
		internal BodyArray(IntPtr native, int count)
			: base(native, count)
		{
		}

		public Body this[int index]
		{
			get
			{
				if ((uint)index >= (uint)Count)
				{
					throw new ArgumentOutOfRangeException(nameof(index));
				}
				IntPtr ptr = btSoftBody_Body_array_at(Native, index);
				return new Body(ptr, this);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public bool Contains(Body item)
		{
			return IndexOf(item) != -1;
		}

		public void CopyTo(Body[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(Body item)
		{
			throw new NotImplementedException();
		}

		public GenericListEnumerator<Body> GetEnumerator()
		{
			return new GenericListEnumerator<Body>(this);
		}

		IEnumerator<Body> IEnumerable<Body>.GetEnumerator()
		{
			return new GenericListEnumerator<Body>(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new GenericListEnumerator<Body>(this);
		}
	}
}
