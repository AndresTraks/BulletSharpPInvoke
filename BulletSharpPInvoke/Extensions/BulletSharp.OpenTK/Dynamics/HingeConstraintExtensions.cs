using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HingeConstraintExtensions
	{
		public unsafe static void GetAFrame(this HingeConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.AFrame;
			}
		}

		public static OpenTK.Matrix4 GetAFrame(this HingeConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetAFrame(obj, out value);
			return value;
		}

		public unsafe static void GetBFrame(this HingeConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.BFrame;
			}
		}

		public static OpenTK.Matrix4 GetBFrame(this HingeConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetBFrame(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetA(this HingeConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetA;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetA(this HingeConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetA(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetB(this HingeConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetB;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetB(this HingeConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetB(obj, out value);
			return value;
		}

		public unsafe static float GetHingeAngle(this HingeConstraint obj, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					return obj.GetHingeAngle(ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr);
				}
			}
		}
        /*
		public unsafe static void GetInfo2Internal(this HingeConstraint obj, ConstraintInfo2 info, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 angVelA, ref OpenTK.Vector3 angVelB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* angVelAPtr = &angVelA)
					{
						fixed (OpenTK.Vector3* angVelBPtr = &angVelB)
						{
							obj.GetInfo2Internal(info, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)angVelAPtr, ref *(BulletSharp.Math.Vector3*)angVelBPtr);
						}
					}
				}
			}
		}

		public unsafe static void GetInfo2InternalUsingFrameOffset(this HingeConstraint obj, ConstraintInfo2 info, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 angVelA, ref OpenTK.Vector3 angVelB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* angVelAPtr = &angVelA)
					{
						fixed (OpenTK.Vector3* angVelBPtr = &angVelB)
						{
							obj.GetInfo2InternalUsingFrameOffset(info, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)angVelAPtr, ref *(BulletSharp.Math.Vector3*)angVelBPtr);
						}
					}
				}
			}
		}

		public unsafe static void GetInfo2NonVirtual(this HingeConstraint obj, ConstraintInfo2 info, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 angVelA, ref OpenTK.Vector3 angVelB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* angVelAPtr = &angVelA)
					{
						fixed (OpenTK.Vector3* angVelBPtr = &angVelB)
						{
							obj.GetInfo2NonVirtual(info, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)angVelAPtr, ref *(BulletSharp.Math.Vector3*)angVelBPtr);
						}
					}
				}
			}
		}
        */
		public unsafe static void SetAxis(this HingeConstraint obj, ref OpenTK.Vector3 axisInA)
		{
			fixed (OpenTK.Vector3* axisInAPtr = &axisInA)
			{
				obj.SetAxis(ref *(BulletSharp.Math.Vector3*)axisInAPtr);
			}
		}

		public unsafe static void SetFrames(this HingeConstraint obj, ref OpenTK.Matrix4 frameA, ref OpenTK.Matrix4 frameB)
		{
			fixed (OpenTK.Matrix4* frameAPtr = &frameA)
			{
				fixed (OpenTK.Matrix4* frameBPtr = &frameB)
				{
					obj.SetFrames(ref *(BulletSharp.Math.Matrix*)frameAPtr, ref *(BulletSharp.Math.Matrix*)frameBPtr);
				}
			}
		}

		public unsafe static void SetMotorTarget(this HingeConstraint obj, ref OpenTK.Quaternion qAinB, float dt)
		{
			fixed (OpenTK.Quaternion* qAinBPtr = &qAinB)
			{
				obj.SetMotorTarget(ref *(BulletSharp.Math.Quaternion*)qAinBPtr, dt);
			}
		}

		public unsafe static void TestLimit(this HingeConstraint obj, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					obj.TestLimit(ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr);
				}
			}
		}
	}
}
