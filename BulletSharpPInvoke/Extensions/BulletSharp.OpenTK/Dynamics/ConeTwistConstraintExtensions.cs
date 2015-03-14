using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ConeTwistConstraintExtensions
	{
		public unsafe static void CalcAngleInfo2(this ConeTwistConstraint obj, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Matrix4 invInertiaWorldA, ref OpenTK.Matrix4 invInertiaWorldB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Matrix4* invInertiaWorldAPtr = &invInertiaWorldA)
					{
						fixed (OpenTK.Matrix4* invInertiaWorldBPtr = &invInertiaWorldB)
						{
							obj.CalcAngleInfo2(ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Matrix*)invInertiaWorldAPtr, ref *(BulletSharp.Math.Matrix*)invInertiaWorldBPtr);
						}
					}
				}
			}
		}

		public unsafe static void GetAFrame(this ConeTwistConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.AFrame;
			}
		}

		public static OpenTK.Matrix4 GetAFrame(this ConeTwistConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetAFrame(obj, out value);
			return value;
		}

		public unsafe static void GetBFrame(this ConeTwistConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.BFrame;
			}
		}

		public static OpenTK.Matrix4 GetBFrame(this ConeTwistConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetBFrame(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetA(this ConeTwistConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetA;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetA(this ConeTwistConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetA(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetB(this ConeTwistConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetB;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetB(this ConeTwistConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetB(obj, out value);
			return value;
		}
        /*
		public unsafe static void GetInfo2NonVirtual(this ConeTwistConstraint obj, ConstraintInfo2 info, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Matrix4 invInertiaWorldA, ref OpenTK.Matrix4 invInertiaWorldB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Matrix4* invInertiaWorldAPtr = &invInertiaWorldA)
					{
						fixed (OpenTK.Matrix4* invInertiaWorldBPtr = &invInertiaWorldB)
						{
							obj.GetInfo2NonVirtual(info, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Matrix*)invInertiaWorldAPtr, ref *(BulletSharp.Math.Matrix*)invInertiaWorldBPtr);
						}
					}
				}
			}
		}
        */
		public unsafe static void SetFrames(this ConeTwistConstraint obj, ref OpenTK.Matrix4 frameA, ref OpenTK.Matrix4 frameB)
		{
			fixed (OpenTK.Matrix4* frameAPtr = &frameA)
			{
				fixed (OpenTK.Matrix4* frameBPtr = &frameB)
				{
					obj.SetFrames(ref *(BulletSharp.Math.Matrix*)frameAPtr, ref *(BulletSharp.Math.Matrix*)frameBPtr);
				}
			}
		}

		public unsafe static void SetMotorTarget(this ConeTwistConstraint obj, ref OpenTK.Quaternion q)
		{
			fixed (OpenTK.Quaternion* qPtr = &q)
			{
				obj.SetMotorTarget(ref *(BulletSharp.Math.Quaternion*)qPtr);
			}
		}

		public unsafe static void SetMotorTargetInConstraintSpace(this ConeTwistConstraint obj, ref OpenTK.Quaternion q)
		{
			fixed (OpenTK.Quaternion* qPtr = &q)
			{
				obj.SetMotorTargetInConstraintSpace(ref *(BulletSharp.Math.Quaternion*)qPtr);
			}
		}
	}
}
