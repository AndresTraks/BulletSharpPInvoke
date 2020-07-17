using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum TypedMultiBodyConstraintType
	{
		Limit = 3,
		OneDofJointMotor,
		Gear,
		PointToPoint,
		Slider,
		SphericalMotor,
		Fixed
	}

	public abstract class MultiBodyConstraint : BulletDisposableObject
	{
		protected internal MultiBodyConstraint()
		{
		}

		protected internal void InitializeMembers(MultiBody bodyA, MultiBody bodyB)
		{
			MultiBodyA = bodyA;
			MultiBodyB = bodyB;
		}

		public void AllocateJacobiansMultiDof()
		{
			btMultiBodyConstraint_allocateJacobiansMultiDof(Native);
		}
		/*
		public void CreateConstraintRows(MultiBodyConstraintArray constraintRows,
			MultiBodyJacobianData data, ContactSolverInfo infoGlobal)
		{
			btMultiBodyConstraint_createConstraintRows(Native, constraintRows.Native,
				data.Native, infoGlobal.Native);
		}
		*/
		public void DebugDraw(DebugDraw drawer)
		{
			btMultiBodyConstraint_debugDraw(Native, drawer.Native);
		}

		public void FinalizeMultiDof()
		{
			btMultiBodyConstraint_finalizeMultiDof(Native);
		}

		public double GetAppliedImpulse(int dof)
		{
			return btMultiBodyConstraint_getAppliedImpulse(Native, dof);
		}

		public double GetPosition(int row)
		{
			return btMultiBodyConstraint_getPosition(Native, row);
		}

		public void InternalSetAppliedImpulse(int dof, double appliedImpulse)
		{
			btMultiBodyConstraint_internalSetAppliedImpulse(Native, dof, appliedImpulse);
		}
		/*
		public double JacobianA(int row)
		{
			return btMultiBodyConstraint_jacobianA(Native, row);
		}

		public double JacobianB(int row)
		{
			return btMultiBodyConstraint_jacobianB(Native, row);
		}
		*/
		public void SetPosition(int row, double pos)
		{
			btMultiBodyConstraint_setPosition(Native, row, pos);
		}

		public void UpdateJacobianSizes()
		{
			btMultiBodyConstraint_updateJacobianSizes(Native);
		}

		public TypedMultiBodyConstraintType ConstraintType => (TypedMultiBodyConstraintType)btMultiBodyConstraint_getConstraintType(Native);

		public int IslandIdA => btMultiBodyConstraint_getIslandIdA(Native);

		public int IslandIdB => btMultiBodyConstraint_getIslandIdB(Native);

		public bool IsUnilateral => btMultiBodyConstraint_isUnilateral(Native);

		public double MaxAppliedImpulse
		{
			get => btMultiBodyConstraint_getMaxAppliedImpulse(Native);
			set => btMultiBodyConstraint_setMaxAppliedImpulse(Native, value);
		}

		public MultiBody MultiBodyA { get; private set; }

		public MultiBody MultiBodyB { get; private set; }

		public int NumRows => btMultiBodyConstraint_getNumRows(Native);

		protected override void Dispose(bool disposing)
		{
			btMultiBodyConstraint_delete(Native);
		}
	}
}
