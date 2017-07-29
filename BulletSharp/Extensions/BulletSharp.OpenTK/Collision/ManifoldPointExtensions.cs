using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ManifoldPointExtensions
	{
		public unsafe static void GetLateralFrictionDir1(this ManifoldPoint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LateralFrictionDir1;
			}
		}

		public static OpenTK.Vector3 GetLateralFrictionDir1(this ManifoldPoint obj)
		{
			OpenTK.Vector3 value;
			GetLateralFrictionDir1(obj, out value);
			return value;
		}

		public unsafe static void GetLateralFrictionDir2(this ManifoldPoint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LateralFrictionDir2;
			}
		}

		public static OpenTK.Vector3 GetLateralFrictionDir2(this ManifoldPoint obj)
		{
			OpenTK.Vector3 value;
			GetLateralFrictionDir2(obj, out value);
			return value;
		}

		public unsafe static void GetLocalPointA(this ManifoldPoint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalPointA;
			}
		}

		public static OpenTK.Vector3 GetLocalPointA(this ManifoldPoint obj)
		{
			OpenTK.Vector3 value;
			GetLocalPointA(obj, out value);
			return value;
		}

		public unsafe static void GetLocalPointB(this ManifoldPoint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalPointB;
			}
		}

		public static OpenTK.Vector3 GetLocalPointB(this ManifoldPoint obj)
		{
			OpenTK.Vector3 value;
			GetLocalPointB(obj, out value);
			return value;
		}

		public unsafe static void GetNormalWorldOnB(this ManifoldPoint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.NormalWorldOnB;
			}
		}

		public static OpenTK.Vector3 GetNormalWorldOnB(this ManifoldPoint obj)
		{
			OpenTK.Vector3 value;
			GetNormalWorldOnB(obj, out value);
			return value;
		}

		public unsafe static void GetPositionWorldOnA(this ManifoldPoint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.PositionWorldOnA;
			}
		}

		public static OpenTK.Vector3 GetPositionWorldOnA(this ManifoldPoint obj)
		{
			OpenTK.Vector3 value;
			GetPositionWorldOnA(obj, out value);
			return value;
		}

		public unsafe static void GetPositionWorldOnB(this ManifoldPoint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.PositionWorldOnB;
			}
		}

		public static OpenTK.Vector3 GetPositionWorldOnB(this ManifoldPoint obj)
		{
			OpenTK.Vector3 value;
			GetPositionWorldOnB(obj, out value);
			return value;
		}

		public unsafe static void SetLateralFrictionDir1(this ManifoldPoint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LateralFrictionDir1 = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLateralFrictionDir1(this ManifoldPoint obj, OpenTK.Vector3 value)
		{
			SetLateralFrictionDir1(obj, ref value);
		}

		public unsafe static void SetLateralFrictionDir2(this ManifoldPoint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LateralFrictionDir2 = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLateralFrictionDir2(this ManifoldPoint obj, OpenTK.Vector3 value)
		{
			SetLateralFrictionDir2(obj, ref value);
		}

		public unsafe static void SetLocalPointA(this ManifoldPoint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LocalPointA = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLocalPointA(this ManifoldPoint obj, OpenTK.Vector3 value)
		{
			SetLocalPointA(obj, ref value);
		}

		public unsafe static void SetLocalPointB(this ManifoldPoint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LocalPointB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLocalPointB(this ManifoldPoint obj, OpenTK.Vector3 value)
		{
			SetLocalPointB(obj, ref value);
		}

		public unsafe static void SetNormalWorldOnB(this ManifoldPoint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.NormalWorldOnB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetNormalWorldOnB(this ManifoldPoint obj, OpenTK.Vector3 value)
		{
			SetNormalWorldOnB(obj, ref value);
		}

		public unsafe static void SetPositionWorldOnA(this ManifoldPoint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.PositionWorldOnA = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetPositionWorldOnA(this ManifoldPoint obj, OpenTK.Vector3 value)
		{
			SetPositionWorldOnA(obj, ref value);
		}

		public unsafe static void SetPositionWorldOnB(this ManifoldPoint obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.PositionWorldOnB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetPositionWorldOnB(this ManifoldPoint obj, OpenTK.Vector3 value)
		{
			SetPositionWorldOnB(obj, ref value);
		}
	}
}
