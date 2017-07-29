using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CollisionShapeExtensions
	{
        public unsafe static void CalculateLocalInertia(this CollisionShape obj, float mass, out OpenTK.Vector3 inertia)
		{
			fixed (OpenTK.Vector3* inertiaPtr = &inertia)
			{
				obj.CalculateLocalInertia(mass, out *(BulletSharp.Math.Vector3*)inertiaPtr);
			}
		}

        public unsafe static void CalculateTemporalAabb(this CollisionShape obj, ref OpenTK.Matrix4 curTrans, ref OpenTK.Vector3 linvel, ref OpenTK.Vector3 angvel, float timeStep, out OpenTK.Vector3 temporalAabbMin, out OpenTK.Vector3 temporalAabbMax)
		{
			fixed (OpenTK.Matrix4* curTransPtr = &curTrans)
			{
				fixed (OpenTK.Vector3* linvelPtr = &linvel)
				{
					fixed (OpenTK.Vector3* angvelPtr = &angvel)
					{
						fixed (OpenTK.Vector3* temporalAabbMinPtr = &temporalAabbMin)
						{
							fixed (OpenTK.Vector3* temporalAabbMaxPtr = &temporalAabbMax)
							{
                                obj.CalculateTemporalAabb(ref *(BulletSharp.Math.Matrix*)curTransPtr, ref *(BulletSharp.Math.Vector3*)linvelPtr, ref *(BulletSharp.Math.Vector3*)angvelPtr, timeStep, out *(BulletSharp.Math.Vector3*)temporalAabbMinPtr, out *(BulletSharp.Math.Vector3*)temporalAabbMaxPtr);
							}
						}
					}
				}
			}
		}

        public unsafe static void GetAabb(this CollisionShape obj, ref OpenTK.Matrix4 t, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Matrix4* tPtr = &t)
			{
				fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
				{
					fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
					{
                        obj.GetAabb(ref *(BulletSharp.Math.Matrix*)tPtr, out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
					}
				}
			}
		}

		public unsafe static void GetAnisotropicRollingFrictionDirection(this CollisionShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AnisotropicRollingFrictionDirection;
			}
		}

		public static OpenTK.Vector3 GetAnisotropicRollingFrictionDirection(this CollisionShape obj)
		{
			OpenTK.Vector3 value;
			GetAnisotropicRollingFrictionDirection(obj, out value);
			return value;
		}

		public unsafe static void GetBoundingSphere(this CollisionShape obj, out OpenTK.Vector3 center, out float radius)
		{
			fixed (OpenTK.Vector3* centerPtr = &center)
			{
                obj.GetBoundingSphere(out *(BulletSharp.Math.Vector3*)centerPtr, out radius);
			}
		}

		public unsafe static void GetLocalScaling(this CollisionShape obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalScaling;
			}
		}

		public static OpenTK.Vector3 GetLocalScaling(this CollisionShape obj)
		{
			OpenTK.Vector3 value;
			GetLocalScaling(obj, out value);
			return value;
		}

		public unsafe static void SetLocalScaling(this CollisionShape obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LocalScaling = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLocalScaling(this CollisionShape obj, OpenTK.Vector3 value)
		{
			SetLocalScaling(obj, ref value);
		}
	}
}
