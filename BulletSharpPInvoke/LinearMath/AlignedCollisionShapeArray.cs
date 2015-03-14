using SiliconStudio.Core.Mathematics;
﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics;

namespace BulletSharp
{
    public class AlignedCollisionShapeArrayDebugView
    {
        private AlignedCollisionShapeArray _array;

        public AlignedCollisionShapeArrayDebugView(AlignedCollisionShapeArray array)
        {
            _array = array;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public CollisionShape[] Items
        {
            get
            {
                CollisionShape[] array = new CollisionShape[_array.Count];
                for (int i = 0; i < _array.Count; i++)
                {
                    array[i] = _array[i];
                }
                return array;
            }
        }
    }

    public class AlignedCollisionShapeArrayEnumerator : IEnumerator<CollisionShape>
    {
        int _i;
        int _count;
        AlignedCollisionShapeArray _array;

        public AlignedCollisionShapeArrayEnumerator(AlignedCollisionShapeArray array)
        {
            _array = array;
            _count = array.Count;
            _i = -1;
        }

        public CollisionShape Current
        {
            get { return _array[_i]; }
        }

        public void Dispose()
        {
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _array[_i]; }
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
    }

    [Serializable, DebuggerTypeProxy(typeof(AlignedCollisionShapeArrayDebugView)), DebuggerDisplay("Count = {Count}")]
    public class AlignedCollisionShapeArray : AlignedObjectArray, IList<CollisionShape>, IDisposable
    {
        bool _preventDelete;

        internal AlignedCollisionShapeArray(IntPtr native, bool preventDelete = false)
            : base(native)
        {
            _preventDelete = preventDelete;
        }

        public AlignedCollisionShapeArray()
            : base(btAlignedCollisionShapeArray_new())
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_native != IntPtr.Zero)
            {
                if (!_preventDelete)
                {
                    btAlignedCollisionShapeArray_delete(_native);
                }
                _native = IntPtr.Zero;
            }
        }

        ~AlignedCollisionShapeArray()
        {
            Dispose(false);
        }

        public int IndexOf(CollisionShape item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, CollisionShape item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public CollisionShape this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)

                    throw new ArgumentOutOfRangeException("index");

                return CollisionShape.GetManaged(btAlignedCollisionShapeArray_at(_native, index));
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(CollisionShape item)
        {
            btAlignedCollisionShapeArray_push_back(_native, item._native);
        }

        public void Clear()
        {
            btAlignedCollisionShapeArray_resizeNoInitialize(_native, 0);
        }

        public bool Contains(CollisionShape item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(CollisionShape[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return btAlignedCollisionShapeArray_size(_native); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CollisionShape item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<CollisionShape> GetEnumerator()
        {
            return new AlignedCollisionShapeArrayEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new AlignedCollisionShapeArrayEnumerator(this);
        }

        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btAlignedCollisionShapeArray_new();
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern IntPtr btAlignedCollisionShapeArray_at(IntPtr obj, int n);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedCollisionShapeArray_push_back(IntPtr obj, IntPtr val);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedCollisionShapeArray_resizeNoInitialize(IntPtr obj, int newSize);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern int btAlignedCollisionShapeArray_size(IntPtr obj);
        [DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
        protected static extern void btAlignedCollisionShapeArray_delete(IntPtr obj);
    }
}
