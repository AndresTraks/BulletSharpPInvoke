using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyJointMotor : MultiBodyConstraint
	{
		public MultiBodyJointMotor(MultiBody body, int link, double desiredVelocity,
			double maxMotorImpulse)
		{
			IntPtr native = btMultiBodyJointMotor_new(body.Native, link, desiredVelocity,
				maxMotorImpulse);
			InitializeUserOwned(native);
			InitializeMembers(body, body);
		}

		public MultiBodyJointMotor(MultiBody body, int link, int linkDoF, double desiredVelocity,
			double maxMotorImpulse)
		{
			IntPtr native = btMultiBodyJointMotor_new2(body.Native, link, linkDoF, desiredVelocity,
				maxMotorImpulse);
			InitializeUserOwned(native);
			InitializeMembers(body, body);
		}

		public void SetVelocityTarget(double velTarget)
		{
			btMultiBodyJointMotor_setVelocityTarget(Native, velTarget);
		}
	}
}
