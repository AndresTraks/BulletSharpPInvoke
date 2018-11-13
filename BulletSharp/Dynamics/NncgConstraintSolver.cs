using System;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class NncgConstraintSolver : SequentialImpulseConstraintSolver
	{
		public NncgConstraintSolver()
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btNNCGConstraintSolver_new();
			InitializeUserOwned(native);
		}

		public bool OnlyForNoneContact
		{
			get => btNNCGConstraintSolver_getOnlyForNoneContact(Native);
			set => btNNCGConstraintSolver_setOnlyForNoneContact(Native, value);
		}
	}
}
