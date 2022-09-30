using System;
using System.Collections.Generic;
using System.Diagnostics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.Math
{
    internal class Vector3ListDebugView
    {
        private IList<Vector3> _list;

        public Vector3ListDebugView(IList<Vector3> list)
        {
            _list = list;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public Vector3[] Items
        {
            get
            {
                var arr = new Vector3[_list.Count];
                _list.CopyTo(arr, 0);
                return arr;
            }
        }
    };

    public class Vector3ArrayEnumerator : IEnumerator<Vector3>
    {
        private int _i;
        private readonly int _count;
        private readonly IList<Vector3> _array;

        public Vector3ArrayEnumerator(IList<Vector3> array)
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
            _i = 0;
        }

        public Vector3 Current => _array[_i];

        object System.Collections.IEnumerator.Current => _array[_i];
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(Vector3ListDebugView))]
    public class Vector3Array : FixedSizeArray<Vector3>, IList<Vector3>
    {
        internal Vector3Array(IntPtr native, int count)
            : base(native, count)
        {
        }

        public int IndexOf(Vector3 item)
        {
            for (int i = 0; i < Count; i++)
            {
                Vector3 value;
                btVector3_array_at(Native, i, out value);
                if (value.Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public Vector3 this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                Vector3 value;
                btVector3_array_at(Native, index, out value);
                return value;
            }
            set
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                btVector3_array_set(Native, index, ref value);
            }
        }

        public bool Contains(Vector3 item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(Vector3[] array, int arrayIndex)
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

        public IEnumerator<Vector3> GetEnumerator()
        {
            return new Vector3ArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new Vector3ArrayEnumerator(this);
        }
    }
}
