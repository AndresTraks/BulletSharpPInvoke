using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class Element : BulletObject
	{
		internal Element(IntPtr native)
		{
			Initialize(native);
		}

		public int Id
		{
			get => btElement_getId(Native);
			set => btElement_setId(Native, value);
		}

		public int Sz
		{
			get => btElement_getSz(Native);
			set => btElement_setSz(Native, value);
		}
	}

	public class UnionFind : BulletObject
	{
		internal UnionFind(IntPtr native)
		{
			Initialize(native);
		}

		public void Allocate(int n)
		{
			btUnionFind_allocate(Native, n);
		}

		public int Find(int p, int q)
		{
			return btUnionFind_find(Native, p, q);
		}

		public int Find(int x)
		{
			return btUnionFind_find2(Native, x);
		}

		public void Free()
		{
			btUnionFind_Free(Native);
		}

		public Element GetElement(int index)
		{
			return new Element(btUnionFind_getElement(Native, index));
		}

		public bool IsRoot(int x)
		{
			return btUnionFind_isRoot(Native, x);
		}

		public void Reset(int n)
		{
			btUnionFind_reset(Native, n);
		}

		public void SortIslands()
		{
			btUnionFind_sortIslands(Native);
		}

		public void Unite(int p, int q)
		{
			btUnionFind_unite(Native, p, q);
		}

		public int NumElements => btUnionFind_getNumElements(Native);
	}
}
