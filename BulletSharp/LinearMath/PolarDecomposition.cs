using System;
using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class PolarDecomposition : BulletDisposableObject
	{
		public PolarDecomposition(float tolerance = 0.0001f, int maxIterations = 16)
		{
			IntPtr native = btPolarDecomposition_new(tolerance, (uint)maxIterations);
			InitializeUserOwned(native);
		}

		public uint Decompose(ref Matrix4x4 a, out Matrix4x4 u, out Matrix4x4 h)
		{
			return btPolarDecomposition_decompose(Native, ref a, out u, out h);
		}

		public uint MaxIterations()
		{
			return btPolarDecomposition_maxIterations(Native);
		}

		protected override void Dispose(bool disposing)
		{
			btPolarDecomposition_delete(Native);
		}
	}
}
