using System.Numerics;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum PhyScalarType
	{
		Single,
		Double,
		Int32,
		Int16,
		FixedPoint88,
		Byte
	}

	public abstract class ConcaveShape : CollisionShape
	{
		protected internal ConcaveShape()
		{
		}

		public void ProcessAllTriangles(TriangleCallback callback, Vector3 aabbMin,
			Vector3 aabbMax)
		{
			btConcaveShape_processAllTriangles(Native, callback.Native, ref aabbMin,
				ref aabbMax);
		}
	}
}
