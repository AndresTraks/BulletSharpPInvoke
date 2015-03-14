using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class CollisionObjectExtensions
	{
		public unsafe static void GetAnisotropicFriction(this CollisionObject obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AnisotropicFriction;
			}
		}

		public static OpenTK.Vector3 GetAnisotropicFriction(this CollisionObject obj)
		{
			OpenTK.Vector3 value;
			GetAnisotropicFriction(obj, out value);
			return value;
		}

		public unsafe static void GetInterpolationAngularVelocity(this CollisionObject obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.InterpolationAngularVelocity;
			}
		}

		public static OpenTK.Vector3 GetInterpolationAngularVelocity(this CollisionObject obj)
		{
			OpenTK.Vector3 value;
			GetInterpolationAngularVelocity(obj, out value);
			return value;
		}

		public unsafe static void GetInterpolationLinearVelocity(this CollisionObject obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.InterpolationLinearVelocity;
			}
		}

		public static OpenTK.Vector3 GetInterpolationLinearVelocity(this CollisionObject obj)
		{
			OpenTK.Vector3 value;
			GetInterpolationLinearVelocity(obj, out value);
			return value;
		}

		public unsafe static void GetInterpolationWorldTransform(this CollisionObject obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.InterpolationWorldTransform;
			}
		}

		public static OpenTK.Matrix4 GetInterpolationWorldTransform(this CollisionObject obj)
		{
			OpenTK.Matrix4 value;
			GetInterpolationWorldTransform(obj, out value);
			return value;
		}

		public unsafe static void GetWorldTransform(this CollisionObject obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.WorldTransform;
			}
		}

		public static OpenTK.Matrix4 GetWorldTransform(this CollisionObject obj)
		{
			OpenTK.Matrix4 value;
			GetWorldTransform(obj, out value);
			return value;
		}

		public unsafe static void SetAnisotropicFriction(this CollisionObject obj, ref OpenTK.Vector3 anisotropicFriction, AnisotropicFrictionFlags frictionMode)
		{
			fixed (OpenTK.Vector3* anisotropicFrictionPtr = &anisotropicFriction)
			{
				obj.SetAnisotropicFriction(ref *(BulletSharp.Math.Vector3*)anisotropicFrictionPtr, frictionMode);
			}
		}

		public unsafe static void SetAnisotropicFriction(this CollisionObject obj, ref OpenTK.Vector3 anisotropicFriction)
		{
			fixed (OpenTK.Vector3* anisotropicFrictionPtr = &anisotropicFriction)
			{
				obj.SetAnisotropicFriction(ref *(BulletSharp.Math.Vector3*)anisotropicFrictionPtr);
			}
		}

		public unsafe static void SetInterpolationAngularVelocity(this CollisionObject obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.InterpolationAngularVelocity = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetInterpolationAngularVelocity(this CollisionObject obj, OpenTK.Vector3 value)
		{
			SetInterpolationAngularVelocity(obj, ref value);
		}

		public unsafe static void SetInterpolationLinearVelocity(this CollisionObject obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.InterpolationLinearVelocity = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetInterpolationLinearVelocity(this CollisionObject obj, OpenTK.Vector3 value)
		{
			SetInterpolationLinearVelocity(obj, ref value);
		}

		public unsafe static void SetInterpolationWorldTransform(this CollisionObject obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.InterpolationWorldTransform = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetInterpolationWorldTransform(this CollisionObject obj, OpenTK.Matrix4 value)
		{
			SetInterpolationWorldTransform(obj, ref value);
		}

		public unsafe static void SetWorldTransform(this CollisionObject obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.WorldTransform = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetWorldTransform(this CollisionObject obj, OpenTK.Matrix4 value)
		{
			SetWorldTransform(obj, ref value);
		}
	}
}
