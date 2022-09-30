using System;
using System.IO;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public class DiscreteDynamicsWorld : DynamicsWorld
	{
		private SimulationIslandManager _simulationIslandManager;

		protected internal DiscreteDynamicsWorld()
		{
		}

		public DiscreteDynamicsWorld(Dispatcher dispatcher, BroadphaseInterface pairCache,
			ConstraintSolver constraintSolver, CollisionConfiguration collisionConfiguration)
		{
			IntPtr native = btDiscreteDynamicsWorld_new(
				dispatcher != null ? dispatcher.Native : IntPtr.Zero,
				pairCache != null ? pairCache.Native : IntPtr.Zero,
				constraintSolver != null ? constraintSolver.Native : IntPtr.Zero,
				collisionConfiguration != null ? collisionConfiguration.Native : IntPtr.Zero);
			InitializeUserOwned(native);
			InitializeMembers(dispatcher, pairCache, constraintSolver);
		}

		public void ApplyGravity()
		{
			btDiscreteDynamicsWorld_applyGravity(Native);
		}

		public void DebugDrawConstraint(TypedConstraint constraint)
		{
			btDiscreteDynamicsWorld_debugDrawConstraint(Native, constraint.Native);
		}

		private unsafe void SerializeDynamicsWorldInfo(Serializer serializer)
		{
			int len = Environment.Is64BitProcess ? 156 : 88;
			Chunk chunk = serializer.Allocate((uint)len, 1);

			using (var stream = new UnmanagedMemoryStream((byte*)chunk.OldPtr.ToPointer(), len, len, FileAccess.Write))
			{
				using (var writer = new BinaryWriter(stream))
				{
					ContactSolverInfo solverInfo = SolverInfo;
					writer.Write(solverInfo.Tau);
					writer.Write(solverInfo.Damping);
					writer.Write(solverInfo.Friction);
					writer.Write(solverInfo.TimeStep);

					writer.Write(solverInfo.Restitution);
					writer.Write(solverInfo.MaxErrorReduction);
					writer.Write(solverInfo.Sor);
					writer.Write(solverInfo.Erp);

					writer.Write(solverInfo.Erp2);
					writer.Write(solverInfo.GlobalCfm);
					writer.Write(solverInfo.SplitImpulsePenetrationThreshold);
					writer.Write(solverInfo.SplitImpulseTurnErp);

					writer.Write(solverInfo.LinearSlop);
					writer.Write(solverInfo.WarmStartingFactor);
					writer.Write(solverInfo.MaxGyroscopicForce);
					writer.Write(solverInfo.SingleAxisRollingFrictionThreshold);

					writer.Write(solverInfo.NumIterations);
					writer.Write((int)solverInfo.SolverMode);
					writer.Write(solverInfo.RestingContactRestitutionThreshold);
					writer.Write(solverInfo.MinimumSolverBatchSize);

					writer.Write(solverInfo.SplitImpulse);
					writer.Write((int)0); // padding
				}
			}

			serializer.FinalizeChunk(chunk, "btDynamicsWorldDoubleData", DnaID.DynamicsWorld, chunk.OldPtr);
		}

		void SerializeRigidBodies(Serializer serializer)
		{
			foreach (CollisionObject colObj in CollisionObjectArray)
			{
				if (colObj.InternalType == CollisionObjectTypes.RigidBody)
				{
					int len = colObj.CalculateSerializeBufferSize();
					Chunk chunk = serializer.Allocate((uint)len, 1);
					string structType = colObj.Serialize(chunk.OldPtr, serializer);
					serializer.FinalizeChunk(chunk, structType, DnaID.RigidBody, colObj.Native);
				}
			}

			for (int i = 0; i < NumConstraints; i++)
			{
				TypedConstraint constraint = GetConstraint(i);
				int len = constraint.CalculateSerializeBufferSize();
				Chunk chunk = serializer.Allocate((uint)len, 1);
				string structType = constraint.Serialize(chunk.OldPtr, serializer);
				serializer.FinalizeChunk(chunk, structType, DnaID.Constraint, constraint.Native);
			}
		}

		public override void Serialize(Serializer serializer)
		{
			serializer.StartSerialization();
			SerializeDynamicsWorldInfo(serializer);
			SerializeCollisionObjects(serializer);
			SerializeRigidBodies(serializer);
			serializer.FinishSerialization();
		}

		public void SetNumTasks(int numTasks)
		{
			btDiscreteDynamicsWorld_setNumTasks(Native, numTasks);
		}

		public void SolveConstraints(ContactSolverInfo solverInfo)
		{
			btDiscreteDynamicsWorld_solveConstraints(Native, solverInfo.Native);
		}

		public void SynchronizeSingleMotionState(RigidBody body)
		{
			btDiscreteDynamicsWorld_synchronizeSingleMotionState(Native, body.Native);
		}

		public void UpdateVehicles(double timeStep)
		{
			btDiscreteDynamicsWorld_updateVehicles(Native, timeStep);
		}

		public bool ApplySpeculativeContactRestitution
		{
			get => btDiscreteDynamicsWorld_getApplySpeculativeContactRestitution(Native);
			set => btDiscreteDynamicsWorld_setApplySpeculativeContactRestitution(Native, value);
		}

		public bool LatencyMotionStateInterpolation
		{
			get => btDiscreteDynamicsWorld_getLatencyMotionStateInterpolation(Native);
			set => btDiscreteDynamicsWorld_setLatencyMotionStateInterpolation(Native, value);
		}

		public SimulationIslandManager SimulationIslandManager
		{
			get
			{
				if (_simulationIslandManager == null)
				{
					_simulationIslandManager = new SimulationIslandManager(btDiscreteDynamicsWorld_getSimulationIslandManager(Native));
				}
				return _simulationIslandManager;
			}
		}

		public bool SynchronizeAllMotionStates
		{
			get => btDiscreteDynamicsWorld_getSynchronizeAllMotionStates(Native);
			set => btDiscreteDynamicsWorld_setSynchronizeAllMotionStates(Native, value);
		}
	}
}
