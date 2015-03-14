using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class JointFeedbackExtensions
	{
		public unsafe static void GetAppliedForceBodyA(this JointFeedback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AppliedForceBodyA;
			}
		}

		public static OpenTK.Vector3 GetAppliedForceBodyA(this JointFeedback obj)
		{
			OpenTK.Vector3 value;
			GetAppliedForceBodyA(obj, out value);
			return value;
		}

		public unsafe static void GetAppliedForceBodyB(this JointFeedback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AppliedForceBodyB;
			}
		}

		public static OpenTK.Vector3 GetAppliedForceBodyB(this JointFeedback obj)
		{
			OpenTK.Vector3 value;
			GetAppliedForceBodyB(obj, out value);
			return value;
		}

		public unsafe static void GetAppliedTorqueBodyA(this JointFeedback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AppliedTorqueBodyA;
			}
		}

		public static OpenTK.Vector3 GetAppliedTorqueBodyA(this JointFeedback obj)
		{
			OpenTK.Vector3 value;
			GetAppliedTorqueBodyA(obj, out value);
			return value;
		}

		public unsafe static void GetAppliedTorqueBodyB(this JointFeedback obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AppliedTorqueBodyB;
			}
		}

		public static OpenTK.Vector3 GetAppliedTorqueBodyB(this JointFeedback obj)
		{
			OpenTK.Vector3 value;
			GetAppliedTorqueBodyB(obj, out value);
			return value;
		}

		public unsafe static void SetAppliedForceBodyA(this JointFeedback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AppliedForceBodyA = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAppliedForceBodyA(this JointFeedback obj, OpenTK.Vector3 value)
		{
			SetAppliedForceBodyA(obj, ref value);
		}

		public unsafe static void SetAppliedForceBodyB(this JointFeedback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AppliedForceBodyB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAppliedForceBodyB(this JointFeedback obj, OpenTK.Vector3 value)
		{
			SetAppliedForceBodyB(obj, ref value);
		}

		public unsafe static void SetAppliedTorqueBodyA(this JointFeedback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AppliedTorqueBodyA = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAppliedTorqueBodyA(this JointFeedback obj, OpenTK.Vector3 value)
		{
			SetAppliedTorqueBodyA(obj, ref value);
		}

		public unsafe static void SetAppliedTorqueBodyB(this JointFeedback obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AppliedTorqueBodyB = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAppliedTorqueBodyB(this JointFeedback obj, OpenTK.Vector3 value)
		{
			SetAppliedTorqueBodyB(obj, ref value);
		}
	}
}
