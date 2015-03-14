using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TriangleMeshShapeExtensions
	{
		public unsafe static void GetLocalAabbMax(this TriangleMeshShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalAabbMax;
			}
		}

		public static OpenTK.Vector3 GetLocalAabbMax(this TriangleMeshShape obj)
		{
			OpenTK.Vector3 value;
			GetLocalAabbMax(obj, out value);
			return value;
		}

		public unsafe static void GetLocalAabbMin(this TriangleMeshShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalAabbMin;
			}
		}

		public static OpenTK.Vector3 GetLocalAabbMin(this TriangleMeshShape obj)
		{
			OpenTK.Vector3 value;
			GetLocalAabbMin(obj, out value);
			return value;
		}

		public unsafe static OpenTK.Vector3 LocalGetSupportingVertex(this TriangleMeshShape obj, ref OpenTK.Vector3 vec)
		{
			fixed (OpenTK.Vector3* vecPtr = &vec)
			{
				return obj.LocalGetSupportingVertex(ref *(BulletSharp.Math.Vector3*)vecPtr).ToOpenTK();
			}
		}

		public unsafe static OpenTK.Vector3 LocalGetSupportingVertexWithoutMargin(this TriangleMeshShape obj, ref OpenTK.Vector3 vec)
		{
			fixed (OpenTK.Vector3* vecPtr = &vec)
			{
				return obj.LocalGetSupportingVertexWithoutMargin(ref *(BulletSharp.Math.Vector3*)vecPtr).ToOpenTK();
			}
		}
	}
}
