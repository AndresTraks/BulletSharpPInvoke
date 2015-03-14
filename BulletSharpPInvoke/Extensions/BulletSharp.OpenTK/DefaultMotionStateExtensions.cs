using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class DefaultMotionStateExtensions
	{
		public unsafe static void GetCenterOfMassOffset(this DefaultMotionState obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.CenterOfMassOffset;
			}
		}

		public static OpenTK.Matrix4 GetCenterOfMassOffset(this DefaultMotionState obj)
		{
			OpenTK.Matrix4 value;
			GetCenterOfMassOffset(obj, out value);
			return value;
		}

		public unsafe static void GetGraphicsWorldTrans(this DefaultMotionState obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.GraphicsWorldTrans;
			}
		}

		public static OpenTK.Matrix4 GetGraphicsWorldTrans(this DefaultMotionState obj)
		{
			OpenTK.Matrix4 value;
			GetGraphicsWorldTrans(obj, out value);
			return value;
		}

		public unsafe static void GetStartWorldTrans(this DefaultMotionState obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.StartWorldTrans;
			}
		}

		public static OpenTK.Matrix4 GetStartWorldTrans(this DefaultMotionState obj)
		{
			OpenTK.Matrix4 value;
			GetStartWorldTrans(obj, out value);
			return value;
		}

		public unsafe static void SetCenterOfMassOffset(this DefaultMotionState obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.CenterOfMassOffset = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetCenterOfMassOffset(this DefaultMotionState obj, OpenTK.Matrix4 value)
		{
			SetCenterOfMassOffset(obj, ref value);
		}

		public unsafe static void SetGraphicsWorldTrans(this DefaultMotionState obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.GraphicsWorldTrans = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetGraphicsWorldTrans(this DefaultMotionState obj, OpenTK.Matrix4 value)
		{
			SetGraphicsWorldTrans(obj, ref value);
		}

		public unsafe static void SetStartWorldTrans(this DefaultMotionState obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.StartWorldTrans = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetStartWorldTrans(this DefaultMotionState obj, OpenTK.Matrix4 value)
		{
			SetStartWorldTrans(obj, ref value);
		}
	}
}
