using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
    public class SequentialImpulseConstraintSolverMultiThreaded : SequentialImpulseConstraintSolver
    {
        public SequentialImpulseConstraintSolverMultiThreaded()
            : base(btSequentialImpulseConstraintSolverMt_new(), false)
        {
        }
    }
}
