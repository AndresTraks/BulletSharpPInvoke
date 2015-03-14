using System.ComponentModel;

namespace BulletSharp
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RigidBodyConstructionInfoExtensions
	{
		public unsafe static void GetLocalInertia(this RigidBodyConstructionInfo obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LocalInertia;
			}
		}

		public static OpenTK.Vector3 GetLocalInertia(this RigidBodyConstructionInfo obj)
		{
			OpenTK.Vector3 value;
			GetLocalInertia(obj, out value);
			return value;
		}

		public unsafe static void GetStartWorldTransform(this RigidBodyConstructionInfo obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.StartWorldTransform;
			}
		}

		public static OpenTK.Matrix4 GetStartWorldTransform(this RigidBodyConstructionInfo obj)
		{
			OpenTK.Matrix4 value;
			GetStartWorldTransform(obj, out value);
			return value;
		}

		public unsafe static void SetLocalInertia(this RigidBodyConstructionInfo obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LocalInertia = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLocalInertia(this RigidBodyConstructionInfo obj, OpenTK.Vector3 value)
		{
			SetLocalInertia(obj, ref value);
		}

		public unsafe static void SetStartWorldTransform(this RigidBodyConstructionInfo obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.StartWorldTransform = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetStartWorldTransform(this RigidBodyConstructionInfo obj, OpenTK.Matrix4 value)
		{
			SetStartWorldTransform(obj, ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class RigidBodyExtensions
	{
		public unsafe static void ApplyCentralForce(this RigidBody obj, ref OpenTK.Vector3 force)
		{
			fixed (OpenTK.Vector3* forcePtr = &force)
			{
				obj.ApplyCentralForce(ref *(BulletSharp.Math.Vector3*)forcePtr);
			}
		}

		public unsafe static void ApplyCentralImpulse(this RigidBody obj, ref OpenTK.Vector3 impulse)
		{
			fixed (OpenTK.Vector3* impulsePtr = &impulse)
			{
				obj.ApplyCentralImpulse(ref *(BulletSharp.Math.Vector3*)impulsePtr);
			}
		}

		public unsafe static void ApplyForce(this RigidBody obj, ref OpenTK.Vector3 force, ref OpenTK.Vector3 rel_pos)
		{
			fixed (OpenTK.Vector3* forcePtr = &force)
			{
				fixed (OpenTK.Vector3* rel_posPtr = &rel_pos)
				{
					obj.ApplyForce(ref *(BulletSharp.Math.Vector3*)forcePtr, ref *(BulletSharp.Math.Vector3*)rel_posPtr);
				}
			}
		}

		public unsafe static void ApplyImpulse(this RigidBody obj, ref OpenTK.Vector3 impulse, ref OpenTK.Vector3 rel_pos)
		{
			fixed (OpenTK.Vector3* impulsePtr = &impulse)
			{
				fixed (OpenTK.Vector3* rel_posPtr = &rel_pos)
				{
					obj.ApplyImpulse(ref *(BulletSharp.Math.Vector3*)impulsePtr, ref *(BulletSharp.Math.Vector3*)rel_posPtr);
				}
			}
		}

		public unsafe static void ApplyTorque(this RigidBody obj, ref OpenTK.Vector3 torque)
		{
			fixed (OpenTK.Vector3* torquePtr = &torque)
			{
				obj.ApplyTorque(ref *(BulletSharp.Math.Vector3*)torquePtr);
			}
		}

		public unsafe static void ApplyTorqueImpulse(this RigidBody obj, ref OpenTK.Vector3 torque)
		{
			fixed (OpenTK.Vector3* torquePtr = &torque)
			{
				obj.ApplyTorqueImpulse(ref *(BulletSharp.Math.Vector3*)torquePtr);
			}
		}

		public unsafe static float ComputeAngularImpulseDenominator(this RigidBody obj, ref OpenTK.Vector3 axis)
		{
			fixed (OpenTK.Vector3* axisPtr = &axis)
			{
				return obj.ComputeAngularImpulseDenominator(ref *(BulletSharp.Math.Vector3*)axisPtr);
			}
		}

        public unsafe static float ComputeImpulseDenominator(this RigidBody obj, ref OpenTK.Vector3 pos, ref OpenTK.Vector3 normal)
		{
			fixed (OpenTK.Vector3* posPtr = &pos)
			{
				fixed (OpenTK.Vector3* normalPtr = &normal)
				{
					return obj.ComputeImpulseDenominator(ref *(BulletSharp.Math.Vector3*)posPtr, ref *(BulletSharp.Math.Vector3*)normalPtr);
				}
			}
		}

        public unsafe static void GetAabb(this RigidBody obj, out OpenTK.Vector3 aabbMin, out OpenTK.Vector3 aabbMax)
		{
			fixed (OpenTK.Vector3* aabbMinPtr = &aabbMin)
			{
				fixed (OpenTK.Vector3* aabbMaxPtr = &aabbMax)
				{
                    obj.GetAabb(out *(BulletSharp.Math.Vector3*)aabbMinPtr, out *(BulletSharp.Math.Vector3*)aabbMaxPtr);
				}
			}
		}

		public unsafe static void GetAngularFactor(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AngularFactor;
			}
		}

		public static OpenTK.Vector3 GetAngularFactor(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetAngularFactor(obj, out value);
			return value;
		}

		public unsafe static void GetAngularVelocity(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.AngularVelocity;
			}
		}

		public static OpenTK.Vector3 GetAngularVelocity(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetAngularVelocity(obj, out value);
			return value;
		}

		public unsafe static void GetCenterOfMassPosition(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.CenterOfMassPosition;
			}
		}

		public static OpenTK.Vector3 GetCenterOfMassPosition(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetCenterOfMassPosition(obj, out value);
			return value;
		}

		public unsafe static void GetCenterOfMassTransform(this RigidBody obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.CenterOfMassTransform;
			}
		}

		public static OpenTK.Matrix4 GetCenterOfMassTransform(this RigidBody obj)
		{
			OpenTK.Matrix4 value;
			GetCenterOfMassTransform(obj, out value);
			return value;
		}

		public unsafe static void GetGravity(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.Gravity;
			}
		}

		public static OpenTK.Vector3 GetGravity(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetGravity(obj, out value);
			return value;
		}

		public unsafe static void GetInvInertiaDiagLocal(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.InvInertiaDiagLocal;
			}
		}

		public static OpenTK.Vector3 GetInvInertiaDiagLocal(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetInvInertiaDiagLocal(obj, out value);
			return value;
		}

		public unsafe static void GetInvInertiaTensorWorld(this RigidBody obj, out OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				*(BulletSharp.Math.Matrix*)valuePtr = obj.InvInertiaTensorWorld;
			}
		}

		public static OpenTK.Matrix4 GetInvInertiaTensorWorld(this RigidBody obj)
		{
			OpenTK.Matrix4 value;
			GetInvInertiaTensorWorld(obj, out value);
			return value;
		}

		public unsafe static void GetLinearFactor(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LinearFactor;
			}
		}

		public static OpenTK.Vector3 GetLinearFactor(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetLinearFactor(obj, out value);
			return value;
		}

		public unsafe static void GetLinearVelocity(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.LinearVelocity;
			}
		}

		public static OpenTK.Vector3 GetLinearVelocity(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetLinearVelocity(obj, out value);
			return value;
		}

		public unsafe static void GetOrientation(this RigidBody obj, out OpenTK.Quaternion value)
		{
			fixed (OpenTK.Quaternion* valuePtr = &value)
			{
				*(BulletSharp.Math.Quaternion*)valuePtr = obj.Orientation;
			}
		}

		public static OpenTK.Quaternion GetOrientation(this RigidBody obj)
		{
			OpenTK.Quaternion value;
			GetOrientation(obj, out value);
			return value;
		}

		public unsafe static void GetTotalForce(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.TotalForce;
			}
		}

		public static OpenTK.Vector3 GetTotalForce(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetTotalForce(obj, out value);
			return value;
		}

		public unsafe static void GetTotalTorque(this RigidBody obj, out OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				*(BulletSharp.Math.Vector3*)valuePtr = obj.TotalTorque;
			}
		}

		public static OpenTK.Vector3 GetTotalTorque(this RigidBody obj)
		{
			OpenTK.Vector3 value;
			GetTotalTorque(obj, out value);
			return value;
		}

		public unsafe static OpenTK.Vector3 GetVelocityInLocalPoint(this RigidBody obj, ref OpenTK.Vector3 rel_pos)
		{
			fixed (OpenTK.Vector3* rel_posPtr = &rel_pos)
			{
				return obj.GetVelocityInLocalPoint(ref *(BulletSharp.Math.Vector3*)rel_posPtr).ToOpenTK();
			}
		}

		public unsafe static void PredictIntegratedTransform(this RigidBody obj, float step, out OpenTK.Matrix4 predictedTransform)
		{
			fixed (OpenTK.Matrix4* predictedTransformPtr = &predictedTransform)
			{
                obj.PredictIntegratedTransform(step, out *(BulletSharp.Math.Matrix*)predictedTransformPtr);
			}
		}

		public unsafe static void ProceedToTransform(this RigidBody obj, ref OpenTK.Matrix4 newTrans)
		{
			fixed (OpenTK.Matrix4* newTransPtr = &newTrans)
			{
				obj.ProceedToTransform(ref *(BulletSharp.Math.Matrix*)newTransPtr);
			}
		}

		public unsafe static void SetAngularFactor(this RigidBody obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AngularFactor = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAngularFactor(this RigidBody obj, OpenTK.Vector3 value)
		{
			SetAngularFactor(obj, ref value);
		}

		public unsafe static void SetAngularVelocity(this RigidBody obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.AngularVelocity = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetAngularVelocity(this RigidBody obj, OpenTK.Vector3 value)
		{
			SetAngularVelocity(obj, ref value);
		}

		public unsafe static void SetCenterOfMassTransform(this RigidBody obj, ref OpenTK.Matrix4 value)
		{
			fixed (OpenTK.Matrix4* valuePtr = &value)
			{
				obj.CenterOfMassTransform = *(BulletSharp.Math.Matrix*)valuePtr;
			}
		}

		public static void SetCenterOfMassTransform(this RigidBody obj, OpenTK.Matrix4 value)
		{
			SetCenterOfMassTransform(obj, ref value);
		}

		public unsafe static void SetGravity(this RigidBody obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.Gravity = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetGravity(this RigidBody obj, OpenTK.Vector3 value)
		{
			SetGravity(obj, ref value);
		}

		public unsafe static void SetInvInertiaDiagLocal(this RigidBody obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.InvInertiaDiagLocal = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetInvInertiaDiagLocal(this RigidBody obj, OpenTK.Vector3 value)
		{
			SetInvInertiaDiagLocal(obj, ref value);
		}

		public unsafe static void SetLinearFactor(this RigidBody obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LinearFactor = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLinearFactor(this RigidBody obj, OpenTK.Vector3 value)
		{
			SetLinearFactor(obj, ref value);
		}

		public unsafe static void SetLinearVelocity(this RigidBody obj, ref OpenTK.Vector3 value)
		{
			fixed (OpenTK.Vector3* valuePtr = &value)
			{
				obj.LinearVelocity = *(BulletSharp.Math.Vector3*)valuePtr;
			}
		}

		public static void SetLinearVelocity(this RigidBody obj, OpenTK.Vector3 value)
		{
			SetLinearVelocity(obj, ref value);
		}

		public unsafe static void SetMassProps(this RigidBody obj, float mass, ref OpenTK.Vector3 inertia)
		{
			fixed (OpenTK.Vector3* inertiaPtr = &inertia)
			{
				obj.SetMassProps(mass, ref *(BulletSharp.Math.Vector3*)inertiaPtr);
			}
		}

		public unsafe static void Translate(this RigidBody obj, ref OpenTK.Vector3 v)
		{
			fixed (OpenTK.Vector3* vPtr = &v)
			{
				obj.Translate(ref *(BulletSharp.Math.Vector3*)vPtr);
			}
		}
	}
}
