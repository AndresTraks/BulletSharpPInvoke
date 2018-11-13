using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class MultiBodyJointLimitConstraint : MultiBodyConstraint
	{
		public MultiBodyJointLimitConstraint(MultiBody body, int link, float lower,
			float upper)
		{
			IntPtr native = btMultiBodyJointLimitConstraint_new(body.Native, link, lower,
				upper);
			InitializeUserOwned(native);
			InitializeMembers(body, body);
		}
	}
}
