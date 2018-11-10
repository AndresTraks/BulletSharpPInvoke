using System.Collections;
using System.Collections.Generic;

namespace BulletSharp
{
	public sealed class GenericListEnumerator<T> : IEnumerator<T>
	{
		private int _i;
		private readonly int _count;
		private readonly IList<T> _list;

		public GenericListEnumerator(IList<T> list)
		{
			_list = list;
			_count = list.Count;
			_i = -1;
		}

		public T Current => _list[_i];

		object IEnumerator.Current => _list[_i];

		public bool MoveNext()
		{
			_i++;
			return _i != _count;
		}

		public void Reset()
		{
			_i = 0;
		}

		public void Dispose()
		{
		}
	}
}
