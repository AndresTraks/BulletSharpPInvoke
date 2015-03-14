using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TriangleMeshExtensions
	{
		public unsafe static void AddTriangle(this TriangleMesh obj, ref OpenTK.Vector3 vertex0, ref OpenTK.Vector3 vertex1, ref OpenTK.Vector3 vertex2, bool removeDuplicateVertices)
		{
			fixed (OpenTK.Vector3* vertex0Ptr = &vertex0)
			{
				fixed (OpenTK.Vector3* vertex1Ptr = &vertex1)
				{
					fixed (OpenTK.Vector3* vertex2Ptr = &vertex2)
					{
						obj.AddTriangle(ref *(BulletSharp.Math.Vector3*)vertex0Ptr, ref *(BulletSharp.Math.Vector3*)vertex1Ptr, ref *(BulletSharp.Math.Vector3*)vertex2Ptr, removeDuplicateVertices);
					}
				}
			}
		}

		public unsafe static void AddTriangle(this TriangleMesh obj, ref OpenTK.Vector3 vertex0, ref OpenTK.Vector3 vertex1, ref OpenTK.Vector3 vertex2)
		{
			fixed (OpenTK.Vector3* vertex0Ptr = &vertex0)
			{
				fixed (OpenTK.Vector3* vertex1Ptr = &vertex1)
				{
					fixed (OpenTK.Vector3* vertex2Ptr = &vertex2)
					{
						obj.AddTriangle(ref *(BulletSharp.Math.Vector3*)vertex0Ptr, ref *(BulletSharp.Math.Vector3*)vertex1Ptr, ref *(BulletSharp.Math.Vector3*)vertex2Ptr);
					}
				}
			}
		}

		public unsafe static int FindOrAddVertex(this TriangleMesh obj, ref OpenTK.Vector3 vertex, bool removeDuplicateVertices)
		{
			fixed (OpenTK.Vector3* vertexPtr = &vertex)
			{
				return obj.FindOrAddVertex(ref *(BulletSharp.Math.Vector3*)vertexPtr, removeDuplicateVertices);
			}
		}
	}
}
