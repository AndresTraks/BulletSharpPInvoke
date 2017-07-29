using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class SliderConstraintExtensions
	{
		public unsafe static void CalculateTransforms(this SliderConstraint obj, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					obj.CalculateTransforms(ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr);
				}
			}
		}
        /*
		public unsafe static void GetAncorInA(this SliderConstraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AncorInA;
			}
		}

		public static OpenTK.Vector3 GetAncorInA(this SliderConstraint obj)
		{
			OpenTK.Vector3 value;
			GetAncorInA(obj, out value);
			return value;
		}

		public unsafe static void GetAncorInB(this SliderConstraint obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AncorInB;
			}
		}

		public static OpenTK.Vector3 GetAncorInB(this SliderConstraint obj)
		{
			OpenTK.Vector3 value;
			GetAncorInB(obj, out value);
			return value;
		}
        */
		public unsafe static void GetCalculatedTransformA(this SliderConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.CalculatedTransformA;
			}
		}

		public static OpenTK.Matrix4 GetCalculatedTransformA(this SliderConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetCalculatedTransformA(obj, out value);
			return value;
		}

		public unsafe static void GetCalculatedTransformB(this SliderConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.CalculatedTransformB;
			}
		}

		public static OpenTK.Matrix4 GetCalculatedTransformB(this SliderConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetCalculatedTransformB(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetA(this SliderConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetA;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetA(this SliderConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetA(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetB(this SliderConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetB;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetB(this SliderConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetB(obj, out value);
			return value;
		}
        /*
		public unsafe static void GetInfo2NonVirtual(this SliderConstraint obj, ConstraintInfo2 info, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 linVelA, ref OpenTK.Vector3 linVelB, float rbAinvMass, float rbBinvMass)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* linVelAPtr = &linVelA)
					{
						fixed (OpenTK.Vector3* linVelBPtr = &linVelB)
						{
							obj.GetInfo2NonVirtual(info, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)linVelAPtr, ref *(BulletSharp.Math.Vector3*)linVelBPtr, rbAinvMass, rbBinvMass);
						}
					}
				}
			}
		}
        */
		public unsafe static void SetFrames(this SliderConstraint obj, ref OpenTK.Matrix4 frameA, ref OpenTK.Matrix4 frameB)
		{
			fixed (OpenTK.Matrix4* frameAPtr = &frameA)
			{
				fixed (OpenTK.Matrix4* frameBPtr = &frameB)
				{
					obj.SetFrames(ref *(BulletSharp.Math.Matrix*)frameAPtr, ref *(BulletSharp.Math.Matrix*)frameBPtr);
				}
			}
		}
	}
}
