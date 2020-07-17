using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public enum DynamicsWorldType
	{
		Simple = 1,
		Discrete = 2,
		Continuous = 3,
		SoftRigid = 4,
		Gpu = 5,
		SoftMultiBody = 6,
		DeformableMultiBody = 7
	}

	public abstract class DynamicsWorld : CollisionWorld
	{
		public delegate void InternalTickCallback(DynamicsWorld world, double timeStep);
		
		[UnmanagedFunctionPointer(CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		delegate void InternalTickCallbackUnmanaged(IntPtr world, double timeStep);

		private InternalTickCallback _preTickCallback;
		private InternalTickCallback _postTickCallback;
		private InternalTickCallbackUnmanaged _preTickCallbackUnmanaged;
		private InternalTickCallbackUnmanaged _postTickCallbackUnmanaged;
		private ConstraintSolver _constraintSolver;
		private ContactSolverInfo _solverInfo;

		private Dictionary<IAction, ActionInterfaceWrapper> _actions;
		private List<TypedConstraint> _constraints = new List<TypedConstraint>();

		internal DynamicsWorld()
		{
		}

		protected internal void InitializeMembers(Dispatcher dispatcher, BroadphaseInterface pairCache, ConstraintSolver constraintSolver)
		{
			InitializeMembers(dispatcher, pairCache);
			_constraintSolver = constraintSolver;
		}

		public void AddAction(IAction action)
		{
			if (_actions == null)
			{
				_actions = new Dictionary<IAction, ActionInterfaceWrapper>();
			}
			else if (_actions.ContainsKey(action))
			{
				return;
			}

			var wrapper = new ActionInterfaceWrapper(action, this);
			_actions.Add(action, wrapper);
			btDynamicsWorld_addAction(Native, wrapper.Native);
		}

		public void AddConstraint(TypedConstraint constraint, bool disableCollisionsBetweenLinkedBodies = false)
		{
			_constraints.Add(constraint);
			btDynamicsWorld_addConstraint(Native, constraint.Native, disableCollisionsBetweenLinkedBodies);

			if (disableCollisionsBetweenLinkedBodies)
			{
				RigidBody rigidBody = constraint.RigidBodyA;
				if (rigidBody._constraintRefs == null)
				{
					rigidBody._constraintRefs = new List<TypedConstraint>();
				}
				rigidBody._constraintRefs.Add(constraint);

				rigidBody = constraint.RigidBodyB;
				if (rigidBody._constraintRefs == null)
				{
					rigidBody._constraintRefs = new List<TypedConstraint>();
				}
				rigidBody._constraintRefs.Add(constraint);
			}
		}

		public void AddRigidBody(RigidBody body)
		{
			CollisionObjectArray.Add(body);
		}

		public void AddRigidBody(RigidBody body, CollisionFilterGroups group, CollisionFilterGroups mask)
		{
			CollisionObjectArray.Add(body, (int)group, (int)mask);
		}

		public void AddRigidBody(RigidBody body, int group, int mask)
		{
			CollisionObjectArray.Add(body, group, mask);
		}

		public void ClearForces()
		{
			btDynamicsWorld_clearForces(Native);
		}

		public TypedConstraint GetConstraint(int index)
		{
			System.Diagnostics.Debug.Assert(btDynamicsWorld_getConstraint(Native, index) == _constraints[index].Native);
			return _constraints[index];
		}

		public void GetGravity(out Vector3 gravity)
		{
			btDynamicsWorld_getGravity(Native, out gravity);
		}

		public void RemoveAction(IAction action)
		{
			if (_actions == null)
			{
				// No actions have been added
				return;
			}

			ActionInterfaceWrapper wrapper;
			if (_actions.TryGetValue(action, out wrapper))
			{
				btDynamicsWorld_removeAction(Native, wrapper.Native);
				_actions.Remove(action);
				wrapper.Dispose();
			}
		}

		public void RemoveConstraint(TypedConstraint constraint)
		{
			RigidBody rigidBody = constraint.RigidBodyA;
			if (rigidBody._constraintRefs != null)
			{
				rigidBody._constraintRefs.Remove(constraint);
			}
			rigidBody = constraint.RigidBodyB;
			if (rigidBody._constraintRefs != null)
			{
				rigidBody._constraintRefs.Remove(constraint);
			}

			int itemIndex = _constraints.IndexOf(constraint);
			if (itemIndex == -1)
			{
				return;
			}

			int lastIndex = _constraints.Count - 1;
			_constraints[itemIndex] = _constraints[lastIndex];
			_constraints.RemoveAt(lastIndex);
			btDynamicsWorld_removeConstraint(Native, constraint.Native);
		}

		public void RemoveRigidBody(RigidBody body)
		{
			CollisionObjectArray.Remove(body);
		}

		public void SetGravity(ref Vector3 gravity)
		{
			btDynamicsWorld_setGravity(Native, ref gravity);
		}

		private void InternalPreTickCallbackNative(IntPtr world, double timeStep)
		{
			_preTickCallback(this, timeStep);
		}

		private void InternalPostTickCallbackNative(IntPtr world, double timeStep)
		{
			_postTickCallback(this, timeStep);
		}

		public void SetInternalTickCallback(InternalTickCallback callback, object worldUserInfo = null,
			bool isPreTick = false)
		{
			if (isPreTick)
			{
				SetInternalPreTickCallback(callback);
			}
			else
			{
				SetInternalPostTickCallback(callback);
			}
			WorldUserInfo = worldUserInfo;
		}

		private void SetInternalPreTickCallback(InternalTickCallback callback)
		{
			if (_preTickCallback != callback)
			{
				_preTickCallback = callback;
				if (callback != null)
				{
					if (_preTickCallbackUnmanaged == null)
					{
						_preTickCallbackUnmanaged = new InternalTickCallbackUnmanaged(InternalPreTickCallbackNative);
					}
					btDynamicsWorld_setInternalTickCallback(Native,
						Marshal.GetFunctionPointerForDelegate(_preTickCallbackUnmanaged), IntPtr.Zero, true);
				}
				else
				{
					_preTickCallbackUnmanaged = null;
					btDynamicsWorld_setInternalTickCallback(Native, IntPtr.Zero, IntPtr.Zero, true);
				}
			}
		}

		private void SetInternalPostTickCallback(InternalTickCallback callback)
		{
			if (_postTickCallback != callback)
			{
				_postTickCallback = callback;
				if (callback != null)
				{
					if (_postTickCallbackUnmanaged == null)
					{
						_postTickCallbackUnmanaged = new InternalTickCallbackUnmanaged(InternalPostTickCallbackNative);
					}
					btDynamicsWorld_setInternalTickCallback(Native,
						Marshal.GetFunctionPointerForDelegate(_postTickCallbackUnmanaged), IntPtr.Zero, false);
				}
				else
				{
					_postTickCallbackUnmanaged = null;
					btDynamicsWorld_setInternalTickCallback(Native, IntPtr.Zero, IntPtr.Zero, false);
				}
			}
		}

		public int StepSimulation(double timeStep, int maxSubSteps = 1, double fixedTimeStep = 1.0f / 60.0f)
		{
			return btDynamicsWorld_stepSimulation(Native, timeStep, maxSubSteps,
				fixedTimeStep);
		}

		public void SynchronizeMotionStates()
		{
			btDynamicsWorld_synchronizeMotionStates(Native);
		}

		public ConstraintSolver ConstraintSolver
		{
			get
			{
				if (_constraintSolver == null)
				{
					_constraintSolver = new SequentialImpulseConstraintSolver(btDynamicsWorld_getConstraintSolver(Native), this);
				}
				return _constraintSolver;
			}
			set
			{
				_constraintSolver = value;
				btDynamicsWorld_setConstraintSolver(Native, value.Native);
			}
		}

		public Vector3 Gravity
		{
			get
			{
				Vector3 value;
				btDynamicsWorld_getGravity(Native, out value);
				return value;
			}
			set => btDynamicsWorld_setGravity(Native, ref value);
		}

		public int NumConstraints => btDynamicsWorld_getNumConstraints(Native);

		public ContactSolverInfo SolverInfo
		{
			get
			{
				if (_solverInfo == null)
				{
					_solverInfo = new ContactSolverInfo(btDynamicsWorld_getSolverInfo(Native), this);
				}
				return _solverInfo;
			}
		}

		public DynamicsWorldType WorldType => btDynamicsWorld_getWorldType(Native);

		public object WorldUserInfo { get; set; }

		protected override void Dispose(bool disposing)
		{
			if (_actions != null)
			{
				foreach (ActionInterfaceWrapper wrapper in _actions.Values)
				{
					wrapper.Dispose();
				}
			}

			base.Dispose(disposing);
		}
	}
}
