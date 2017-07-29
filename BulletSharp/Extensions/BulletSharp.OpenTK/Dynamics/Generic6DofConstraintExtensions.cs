using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RotationalLimitMotorExtensions
	{
		public unsafe static float SolveAngularLimits(this RotationalLimitMotor obj, float timeStep, ref OpenTK.Vector3 axis, float jacDiagABInv, RigidBody body0, RigidBody body1)
		{
			fixed (OpenTK.Vector3* axisPtr = &axis)
			{
				return obj.SolveAngularLimits(timeStep, ref *(BulletSharp.Math.Vector3*)axisPtr, jacDiagABInv, body0, body1);
			}
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TranslationalLimitMotorExtensions
	{
		public unsafe static void GetAccumulatedImpulse(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AccumulatedImpulse;
			}
		}

		public static OpenTK.Vector3 GetAccumulatedImpulse(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetAccumulatedImpulse(obj, out value);
			return value;
		}

		public unsafe static void GetCurrentLimitError(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.CurrentLimitError;
			}
		}

		public static OpenTK.Vector3 GetCurrentLimitError(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetCurrentLimitError(obj, out value);
			return value;
		}

		public unsafe static void GetCurrentLinearDiff(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.CurrentLinearDiff;
			}
		}

		public static OpenTK.Vector3 GetCurrentLinearDiff(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetCurrentLinearDiff(obj, out value);
			return value;
		}

		public unsafe static void GetLowerLimit(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LowerLimit;
			}
		}

		public static OpenTK.Vector3 GetLowerLimit(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetLowerLimit(obj, out value);
			return value;
		}

		public unsafe static void GetMaxMotorForce(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.MaxMotorForce;
			}
		}

		public static OpenTK.Vector3 GetMaxMotorForce(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetMaxMotorForce(obj, out value);
			return value;
		}

		public unsafe static void GetNormalCFM(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.NormalCfm;
			}
		}

		public static OpenTK.Vector3 GetNormalCFM(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetNormalCFM(obj, out value);
			return value;
		}

		public unsafe static void GetStopCFM(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.StopCfm;
			}
		}

		public static OpenTK.Vector3 GetStopCFM(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetStopCFM(obj, out value);
			return value;
		}

		public unsafe static void GetStopERP(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.StopErp;
			}
		}

		public static OpenTK.Vector3 GetStopERP(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetStopERP(obj, out value);
			return value;
		}

		public unsafe static void GetTargetVelocity(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.TargetVelocity;
			}
		}

		public static OpenTK.Vector3 GetTargetVelocity(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetTargetVelocity(obj, out value);
			return value;
		}

		public unsafe static void GetUpperLimit(this TranslationalLimitMotor obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.UpperLimit;
			}
		}

		public static OpenTK.Vector3 GetUpperLimit(this TranslationalLimitMotor obj)
		{
			OpenTK.Vector3 value;
			GetUpperLimit(obj, out value);
			return value;
		}

		public unsafe static void SetAccumulatedImpulse(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AccumulatedImpulse = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAccumulatedImpulse(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetAccumulatedImpulse(obj, ref value);
		}

		public unsafe static void SetCurrentLimitError(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.CurrentLimitError = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetCurrentLimitError(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetCurrentLimitError(obj, ref value);
		}

		public unsafe static void SetCurrentLinearDiff(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.CurrentLinearDiff = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetCurrentLinearDiff(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetCurrentLinearDiff(obj, ref value);
		}

		public unsafe static void SetLowerLimit(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LowerLimit = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLowerLimit(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetLowerLimit(obj, ref value);
		}

		public unsafe static void SetMaxMotorForce(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.MaxMotorForce = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetMaxMotorForce(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetMaxMotorForce(obj, ref value);
		}

		public unsafe static void SetNormalCFM(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.NormalCfm = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetNormalCFM(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetNormalCFM(obj, ref value);
		}

		public unsafe static void SetStopCFM(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.StopCfm = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetStopCFM(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetStopCFM(obj, ref value);
		}

		public unsafe static void SetStopERP(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.StopErp = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetStopERP(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetStopERP(obj, ref value);
		}

		public unsafe static void SetTargetVelocity(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.TargetVelocity = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetTargetVelocity(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetTargetVelocity(obj, ref value);
		}

		public unsafe static void SetUpperLimit(this TranslationalLimitMotor obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.UpperLimit = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetUpperLimit(this TranslationalLimitMotor obj, OpenTK.Vector3 value)
		{
			SetUpperLimit(obj, ref value);
		}

		public unsafe static float SolveLinearAxis(this TranslationalLimitMotor obj, float timeStep, float jacDiagABInv, RigidBody body1, ref OpenTK.Vector3 pointInA, RigidBody body2, ref OpenTK.Vector3 pointInB, int limit_index, ref OpenTK.Vector3 axis_normal_on_a, ref OpenTK.Vector3 anchorPos)
		{
			fixed (OpenTK.Vector3* pointInAPtr = &pointInA)
			{
				fixed (OpenTK.Vector3* pointInBPtr = &pointInB)
				{
					fixed (OpenTK.Vector3* axis_normal_on_aPtr = &axis_normal_on_a)
					{
						fixed (OpenTK.Vector3* anchorPosPtr = &anchorPos)
						{
							return obj.SolveLinearAxis(timeStep, jacDiagABInv, body1, ref *(BulletSharp.Math.Vector3*)pointInAPtr, body2, ref *(BulletSharp.Math.Vector3*)pointInBPtr, limit_index, ref *(BulletSharp.Math.Vector3*)axis_normal_on_aPtr, ref *(BulletSharp.Math.Vector3*)anchorPosPtr);
						}
					}
				}
			}
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class Generic6DofConstraintExtensions
	{
		public unsafe static void CalculateTransforms(this Generic6DofConstraint obj, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB)
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
		public unsafe static int Get_limit_motor_info2(this Generic6DofConstraint obj, RotationalLimitMotor limot, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 linVelA, ref OpenTK.Vector3 linVelB, ref OpenTK.Vector3 angVelA, ref OpenTK.Vector3 angVelB, ConstraintInfo2 info, int row, ref OpenTK.Vector3 ax1, int rotational, int rotAllowed)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* linVelAPtr = &linVelA)
					{
						fixed (OpenTK.Vector3* linVelBPtr = &linVelB)
						{
							fixed (OpenTK.Vector3* angVelAPtr = &angVelA)
							{
								fixed (OpenTK.Vector3* angVelBPtr = &angVelB)
								{
									fixed (OpenTK.Vector3* ax1Ptr = &ax1)
									{
										return obj.Get_limit_motor_info2(limot, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)linVelAPtr, ref *(BulletSharp.Math.Vector3*)linVelBPtr, ref *(BulletSharp.Math.Vector3*)angVelAPtr, ref *(BulletSharp.Math.Vector3*)angVelBPtr, info, row, ref *(BulletSharp.Math.Vector3*)ax1Ptr, rotational, rotAllowed);
									}
								}
							}
						}
					}
				}
			}
		}

		public unsafe static int Get_limit_motor_info2(this Generic6DofConstraint obj, RotationalLimitMotor limot, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 linVelA, ref OpenTK.Vector3 linVelB, ref OpenTK.Vector3 angVelA, ref OpenTK.Vector3 angVelB, ConstraintInfo2 info, int row, ref OpenTK.Vector3 ax1, int rotational)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* linVelAPtr = &linVelA)
					{
						fixed (OpenTK.Vector3* linVelBPtr = &linVelB)
						{
							fixed (OpenTK.Vector3* angVelAPtr = &angVelA)
							{
								fixed (OpenTK.Vector3* angVelBPtr = &angVelB)
								{
									fixed (OpenTK.Vector3* ax1Ptr = &ax1)
									{
										return obj.Get_limit_motor_info2(limot, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)linVelAPtr, ref *(BulletSharp.Math.Vector3*)linVelBPtr, ref *(BulletSharp.Math.Vector3*)angVelAPtr, ref *(BulletSharp.Math.Vector3*)angVelBPtr, info, row, ref *(BulletSharp.Math.Vector3*)ax1Ptr, rotational);
									}
								}
							}
						}
					}
				}
			}
		}
        */
		public unsafe static void GetCalculatedTransformA(this Generic6DofConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.CalculatedTransformA;
			}
		}

		public static OpenTK.Matrix4 GetCalculatedTransformA(this Generic6DofConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetCalculatedTransformA(obj, out value);
			return value;
		}

		public unsafe static void GetCalculatedTransformB(this Generic6DofConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.CalculatedTransformB;
			}
		}

		public static OpenTK.Matrix4 GetCalculatedTransformB(this Generic6DofConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetCalculatedTransformB(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetA(this Generic6DofConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetA;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetA(this Generic6DofConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetA(obj, out value);
			return value;
		}

		public unsafe static void GetFrameOffsetB(this Generic6DofConstraint obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.FrameOffsetB;
			}
		}

		public static OpenTK.Matrix4 GetFrameOffsetB(this Generic6DofConstraint obj)
		{
			OpenTK.Matrix4 value;
			GetFrameOffsetB(obj, out value);
			return value;
		}
        /*
		public unsafe static void GetInfo2NonVirtual(this Generic6DofConstraint obj, ConstraintInfo2 info, ref OpenTK.Matrix4 transA, ref OpenTK.Matrix4 transB, ref OpenTK.Vector3 linVelA, ref OpenTK.Vector3 linVelB, ref OpenTK.Vector3 angVelA, ref OpenTK.Vector3 angVelB)
		{
			fixed (OpenTK.Matrix4* transAPtr = &transA)
			{
				fixed (OpenTK.Matrix4* transBPtr = &transB)
				{
					fixed (OpenTK.Vector3* linVelAPtr = &linVelA)
					{
						fixed (OpenTK.Vector3* linVelBPtr = &linVelB)
						{
							fixed (OpenTK.Vector3* angVelAPtr = &angVelA)
							{
								fixed (OpenTK.Vector3* angVelBPtr = &angVelB)
								{
									obj.GetInfo2NonVirtual(info, ref *(BulletSharp.Math.Matrix*)transAPtr, ref *(BulletSharp.Math.Matrix*)transBPtr, ref *(BulletSharp.Math.Vector3*)linVelAPtr, ref *(BulletSharp.Math.Vector3*)linVelBPtr, ref *(BulletSharp.Math.Vector3*)angVelAPtr, ref *(BulletSharp.Math.Vector3*)angVelBPtr);
								}
							}
						}
					}
				}
			}
		}
        */

		public unsafe static void SetAxis(this Generic6DofConstraint obj, ref OpenTK.Vector3 axis1, ref OpenTK.Vector3 axis2)
		{
			fixed (OpenTK.Vector3* axis1Ptr = &axis1)
			{
				fixed (OpenTK.Vector3* axis2Ptr = &axis2)
				{
					obj.SetAxis(ref *(BulletSharp.Math.Vector3*)axis1Ptr, ref *(BulletSharp.Math.Vector3*)axis2Ptr);
				}
			}
		}

		public unsafe static void SetFrames(this Generic6DofConstraint obj, ref OpenTK.Matrix4 frameA, ref OpenTK.Matrix4 frameB)
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
