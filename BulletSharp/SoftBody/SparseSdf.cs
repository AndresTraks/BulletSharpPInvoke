using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class SparseSdf : BulletObject
	{
		internal SparseSdf(IntPtr native)
		{
			Initialize(native);
		}

		public double DefaultVoxelSize
		{
			get => btSparseSdf3_getDefaultVoxelsz(Native);
			set => btSparseSdf3_setDefaultVoxelsz(Native, value);
		}

		public void GarbageCollect(int lifetime = 256)
		{
			btSparseSdf3_GarbageCollect(Native, lifetime);
		}

		public void Initialize(int hashSize = 2383, int clampCells = 256 * 1024)
		{
			btSparseSdf3_Initialize(Native, hashSize, clampCells);
		}

		public int RemoveReferences(CollisionShape pcs)
		{
			return btSparseSdf3_RemoveReferences(Native, (pcs != null) ? pcs.Native : IntPtr.Zero);
		}

		public void Reset()
		{
			btSparseSdf3_Reset(Native);
		}
	}
}
