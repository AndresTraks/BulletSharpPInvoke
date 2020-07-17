using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp.SoftBody
{
	public class SoftBodyCollisionShape : ConvexShape
	{
		internal SoftBodyCollisionShape(IntPtr native, SoftBody owner)
		{
			InitializeCollisionShape(native, owner);
		}
	}

	public enum AeroModel
	{
		VertexPoint,
		VertexTwoSided,
		VertexTwoSidedLiftDrag,
		VertexOneSided,
		FaceTwoSided,
		FaceTwoSidedLiftDrag,
		FaceOneSided
	}

	[Flags]
	public enum Collisions
	{
		None = 0,
		RigidSoftMask = 0x000f,
		SdfRigidSoft = 0x0001,
		ClusterConvexRigidSoft = 0x0002,
		SdfRigidDeformable = 0x0004,
		SoftSoftMask = 0x00F0,
		VertexFaceSoftSoft = 0x0010,
		ClusterClusterSoftSoft = 0x0020,
		ClusterSelf = 0x0040,
		VertexFaceDeformable = 0x0080,
		RigidDeformableFaceMask = 0x0F00,
		SdfRigidDeformableFace = 0x0100,
		SdfMultibodyDeformableFace = 0x0200,
		Default = SdfRigidSoft
	}

	public enum FeatureType
	{
		None,
		Node,
		Link,
		Face,
		Tetra
	}

	public enum JointType
	{
		Linear,
		Angular,
		Contact
	}

	[Flags]
	public enum MaterialFlags
	{
		None = 0,
		DebugDraw = 0x0001,
		Default = DebugDraw
	}

	public enum PositionSolver
	{
		Linear,
		Anchors,
		RigidContacts,
		SoftContacts
	}

	public enum SolverPresets
	{
		Positions,
		Velocities,
		Default
	}

	public enum VelocitySolver
	{
		Linear
	}

	public class SoftBodyWorldInfo : BulletDisposableObject
	{
		private BroadphaseInterface _broadphase;
		private Dispatcher _dispatcher;
		private SparseSdf _sparseSdf;

		internal SoftBodyWorldInfo(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public SoftBodyWorldInfo()
		{
			IntPtr native = btSoftBodyWorldInfo_new();
			InitializeUserOwned(native);
		}

		public double AirDensity
		{
			get => btSoftBodyWorldInfo_getAir_density(Native);
			set => btSoftBodyWorldInfo_setAir_density(Native, value);
		}

		public BroadphaseInterface Broadphase
		{
			get => _broadphase;
			set
			{
				btSoftBodyWorldInfo_setBroadphase(Native, (value != null) ? value.Native : IntPtr.Zero);
				_broadphase = value;
			}
		}

		public Dispatcher Dispatcher
		{
			get => _dispatcher;
			set
			{
				btSoftBodyWorldInfo_setDispatcher(Native, (value != null) ? value.Native : IntPtr.Zero);
				_dispatcher = value;
			}
		}

		public Vector3 Gravity
		{
			get
			{
				Vector3 value;
				btSoftBodyWorldInfo_getGravity(Native, out value);
				return value;
			}
			set => btSoftBodyWorldInfo_setGravity(Native, ref value);
		}

		public double MaxDisplacement
		{
			get => btSoftBodyWorldInfo_getMaxDisplacement(Native);
			set => btSoftBodyWorldInfo_setMaxDisplacement(Native, value);
		}

		public SparseSdf SparseSdf
		{
			get
			{
				if (_sparseSdf == null)
				{
					_sparseSdf = new SparseSdf(btSoftBodyWorldInfo_getSparsesdf(Native));
				}
				return _sparseSdf;
			}
		}

		public double WaterDensity
		{
			get => btSoftBodyWorldInfo_getWater_density(Native);
			set => btSoftBodyWorldInfo_setWater_density(Native, value);
		}

		public Vector3 WaterNormal
		{
			get
			{
				Vector3 value;
				btSoftBodyWorldInfo_getWater_normal(Native, out value);
				return value;
			}
			set => btSoftBodyWorldInfo_setWater_normal(Native, ref value);
		}

		public double WaterOffset
		{
			get => btSoftBodyWorldInfo_getWater_offset(Native);
			set => btSoftBodyWorldInfo_setWater_offset(Native, value);
		}

		protected override void Dispose(bool disposing)
		{
			if (IsUserOwned)
			{
				btSoftBodyWorldInfo_delete(Native);
			}
		}
	}

	public class AngularJoint : Joint
	{
		public class IControl : BulletDisposableObject
		{
			[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
			private delegate void PrepareUnmanagedDelegate(IntPtr angularJoint);
			[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
			private delegate double SpeedUnmanagedDelegate(IntPtr angularJoint, double current);

			private readonly PrepareUnmanagedDelegate _prepare;
			private readonly SpeedUnmanagedDelegate _speed;

			internal static IControl GetManaged(IntPtr native)
			{
				if (native == IntPtr.Zero)
				{
					return null;
				}

				if (native == Default.Native)
				{
					return Default;
				}

				IntPtr handle = btSoftBody_AJoint_IControlWrapper_getWrapperData(native);
				return GCHandle.FromIntPtr(handle).Target as IControl;
			}

			internal IControl(IntPtr native)
			{
				InitializeSubObject(native, this);
			}

			public IControl()
			{
				_prepare = PrepareUnmanaged;
				_speed = SpeedUnmanaged;

				IntPtr native = btSoftBody_AJoint_IControlWrapper_new(
					Marshal.GetFunctionPointerForDelegate(_prepare),
					Marshal.GetFunctionPointerForDelegate(_speed));
				InitializeUserOwned(native);

				GCHandle handle = GCHandle.Alloc(this, GCHandleType.Weak);
				btSoftBody_AJoint_IControlWrapper_setWrapperData(Native, GCHandle.ToIntPtr(handle));
			}

			static IControl _default;
			public static IControl Default =>
				_default ?? (_default = new IControl(btSoftBody_AJoint_IControl_Default()));

			private void PrepareUnmanaged(IntPtr angularJoint)
			{
				Prepare(new AngularJoint(angularJoint));
			}

			public double SpeedUnmanaged(IntPtr angularJoint, double current)
			{
				return Speed(new AngularJoint(angularJoint), current);
			}

			public virtual void Prepare(AngularJoint angularJoint)
			{
			}

			public virtual double Speed(AngularJoint angularJoint, double current)
			{
				return current;
			}

			protected override void Dispose(bool disposing)
			{
				if (IsUserOwned)
				{
					IntPtr handle = btSoftBody_AJoint_IControlWrapper_getWrapperData(Native);
					GCHandle.FromIntPtr(handle).Free();
					btSoftBody_AJoint_IControl_delete(Native);
				}
			}
		}

		public new class Specs : Joint.Specs
		{
			private IControl _iControl;

			public Specs()
			{
				IntPtr native = btSoftBody_AJoint_Specs_new();
				InitializeUserOwned(native);
			}

			public Vector3 Axis
			{
				get
				{
					Vector3 value;
					btSoftBody_AJoint_Specs_getAxis(Native, out value);
					return value;
				}
				set => btSoftBody_AJoint_Specs_setAxis(Native, ref value);
			}

			public IControl Control
			{
				get
				{
					if (_iControl == null)
					{
						_iControl = IControl.GetManaged(btSoftBody_AJoint_Specs_getIcontrol(Native));
					}
					return _iControl;
				}
				set
				{
					_iControl = value;
					btSoftBody_AJoint_Specs_setIcontrol(Native, value.Native);
				}
			}
		}

		private IControl _iControl;
		private Vector3Array _axis;

		internal AngularJoint(IntPtr native)
		{
			Initialize(native);
		}

		public Vector3Array Axis
		{
			get
			{
				if (_axis == null)
				{
					_axis = new Vector3Array(btSoftBody_AJoint_getAxis(Native), 2);
				}
				return _axis;
			}
		}

		public IControl Control
		{
			get
			{
				if (_iControl == null)
				{
					_iControl = IControl.GetManaged(btSoftBody_AJoint_getIcontrol(Native));
				}
				return _iControl;
			}
			set
			{
				_iControl = value;
				btSoftBody_AJoint_setIcontrol(Native, value.Native);
			}
		}
	}

	public class Anchor : BulletObject
	{
		private Node _node;

		internal Anchor(IntPtr native)
		{
			Initialize(native);
		}

		public RigidBody Body
		{
			get => CollisionObject.GetManaged(btSoftBody_Anchor_getBody(Native)) as RigidBody;
			set => btSoftBody_Anchor_setBody(Native, (value != null) ? value.Native : IntPtr.Zero);
		}

		public Matrix C0
		{
			get
			{
				Matrix value;
				btSoftBody_Anchor_getC0(Native, out value);
				return value;
			}
			set => btSoftBody_Anchor_setC0(Native, ref value);
		}

		public Vector3 C1
		{
			get
			{
				Vector3 value;
				btSoftBody_Anchor_getC1(Native, out value);
				return value;
			}
			set => btSoftBody_Anchor_setC1(Native, ref value);
		}

		public double C2
		{
			get => btSoftBody_Anchor_getC2(Native);
			set => btSoftBody_Anchor_setC2(Native, value);
		}

		public double Influence
		{
			get => btSoftBody_Anchor_getInfluence(Native);
			set => btSoftBody_Anchor_setInfluence(Native, value);
		}

		public Vector3 Local
		{
			get
			{
				Vector3 value;
				btSoftBody_Anchor_getLocal(Native, out value);
				return value;
			}
			set => btSoftBody_Anchor_setLocal(Native, ref value);
		}

		public Node Node
		{
			get
			{
				IntPtr nodePtr = btSoftBody_Anchor_getNode(Native);
				if (_node != null && _node.Native == nodePtr) return _node;
				if (nodePtr == IntPtr.Zero) return null;
				_node = new Node(nodePtr);
				return _node;
			}
			set
			{
				btSoftBody_Anchor_setNode(Native, (value != null) ? value.Native : IntPtr.Zero);
				_node = value;
			}
		}
	}

	public class Body : BulletDisposableObject
	{
		private Cluster _soft;

		internal Body(IntPtr native, BulletObject owner)
		{
			InitializeSubObject(native, owner);
		}

		public Body()
		{
			IntPtr native = btSoftBody_Body_new();
			InitializeUserOwned(native);
		}

		public Body(CollisionObject colObj)
		{
			IntPtr native = btSoftBody_Body_new2(colObj.Native);
			InitializeUserOwned(native);
		}

		public Body(Cluster p)
		{
			IntPtr native = btSoftBody_Body_new3(p.Native);
			InitializeUserOwned(native);
			_soft = p;
		}

		public void Activate()
		{
			btSoftBody_Body_activate(Native);
		}

		public void ApplyAImpulse(Impulse impulse)
		{
			btSoftBody_Body_applyAImpulse(Native, impulse.Native);
		}

		public void ApplyDAImpulse(Vector3 impulse)
		{
			btSoftBody_Body_applyDAImpulse(Native, ref impulse);
		}

		public void ApplyDCImpulse(Vector3 impulse)
		{
			btSoftBody_Body_applyDCImpulse(Native, ref impulse);
		}

		public void ApplyDImpulse(Vector3 impulse, Vector3 rpos)
		{
			btSoftBody_Body_applyDImpulse(Native, ref impulse, ref rpos);
		}

		public void ApplyImpulse(Impulse impulse, Vector3 rpos)
		{
			btSoftBody_Body_applyImpulse(Native, impulse.Native, ref rpos);
		}

		public void ApplyVAImpulse(Vector3 impulse)
		{
			btSoftBody_Body_applyVAImpulse(Native, ref impulse);
		}

		public void ApplyVImpulse(Vector3 impulse, Vector3 rpos)
		{
			btSoftBody_Body_applyVImpulse(Native, ref impulse, ref rpos);
		}

		public Vector3 GetAngularVelocity(Vector3 rpos)
		{
			Vector3 value;
			btSoftBody_Body_angularVelocity(Native, ref rpos, out value);
			return value;
		}

		public Vector3 Velocity(Vector3 rpos)
		{
			Vector3 value;
			btSoftBody_Body_velocity(Native, ref rpos, out value);
			return value;
		}

		public Vector3 AngularVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Body_angularVelocity2(Native, out value);
				return value;
			}
		}

		public CollisionObject CollisionObject
		{
			get => CollisionObject.GetManaged(btSoftBody_Body_getCollisionObject(Native));
			set => btSoftBody_Body_setCollisionObject(Native, value.Native);
		}

		public double InverseMass => btSoftBody_Body_invMass(Native);

		public Matrix InverseWorldInertia
		{
			get
			{
				Matrix value;
				btSoftBody_Body_invWorldInertia(Native, out value);
				return value;
			}
		}

		public Vector3 LinearVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Body_linearVelocity(Native, out value);
				return value;
			}
		}

		public RigidBody Rigid
		{
			get => CollisionObject.GetManaged(btSoftBody_Body_getRigid(Native)) as RigidBody;
			set => btSoftBody_Body_setRigid(Native, value.Native);
		}

		public Cluster Soft
		{
			get
			{
				IntPtr softPtr = btSoftBody_Body_getSoft(Native);
				if (_soft != null && _soft.Native == softPtr) return _soft;
				if (softPtr == IntPtr.Zero) return null;
				_soft = new Cluster(softPtr);
				return _soft;
			}
			set
			{
				btSoftBody_Body_setSoft(Native, (value != null) ? value.Native : IntPtr.Zero);
				_soft = value;
			}
		}

		public Matrix Transform
		{
			get
			{
				Matrix value;
				btSoftBody_Body_xform(Native, out value);
				return value;
			}
		}

		protected override void Dispose(bool disposing)
		{
			btSoftBody_Body_delete(Native);
		}
	}

	public class ContactJoint : Joint
	{
		private Vector3Array _rPos;

		internal ContactJoint(IntPtr native)
		{
			Initialize(native);
		}

		public double Friction
		{
			get => btSoftBody_CJoint_getFriction(Native);
			set => btSoftBody_CJoint_setFriction(Native, value);
		}

		public int Life
		{
			get => btSoftBody_CJoint_getLife(Native);
			set => btSoftBody_CJoint_setLife(Native, value);
		}

		public int MaxLife
		{
			get => btSoftBody_CJoint_getMaxlife(Native);
			set => btSoftBody_CJoint_setMaxlife(Native, value);
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_CJoint_getNormal(Native, out value);
				return value;
			}
			set => btSoftBody_CJoint_setNormal(Native, ref value);
		}

		public Vector3Array RPosition
		{
			get
			{
				if (_rPos == null)
				{
					_rPos = new Vector3Array(btSoftBody_CJoint_getRpos(Native), 2);
				}
				return _rPos;
			}
		}
	}

	public class Cluster
	{
		internal IntPtr Native;

		private AlignedVector3Array _framerefs;
		private Vector3Array _dImpulses;
		private DbvtNode _leaf;
		//private AlignedScalarArray _masses;
		private AlignedNodeArray _nodes;
		private Vector3Array _vImpulses;

		internal Cluster(IntPtr native)
		{
			Native = native;
		}

		public double AngularDamping
		{
			get => btSoftBody_Cluster_getAdamping(Native);
			set => btSoftBody_Cluster_setAdamping(Native, value);
		}

		public Vector3 AngularVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Cluster_getAv(Native, out value);
				return value;
			}
			set => btSoftBody_Cluster_setAv(Native, ref value);
		}

		public int ClusterIndex
		{
			get => btSoftBody_Cluster_getClusterIndex(Native);
			set => btSoftBody_Cluster_setClusterIndex(Native, value);
		}

		public bool Collide
		{
			get => btSoftBody_Cluster_getCollide(Native);
			set => btSoftBody_Cluster_setCollide(Native, value);
		}

		public Vector3 CenterOfMass
		{
			get
			{
				Vector3 value;
				btSoftBody_Cluster_getCom(Native, out value);
				return value;
			}
			set => btSoftBody_Cluster_setCom(Native, ref value);
		}

		public bool ContainsAnchor
		{
			get => btSoftBody_Cluster_getContainsAnchor(Native);
			set => btSoftBody_Cluster_setContainsAnchor(Native, value);
		}

		public Vector3Array DImpulses
		{
			get
			{
				if (_dImpulses == null)
				{
					_dImpulses = new Vector3Array(btSoftBody_Cluster_getDimpulses(Native), 2);
				}
				return _dImpulses;
			}
		}

		public AlignedVector3Array FrameRefs
		{
			get
			{
				if (_framerefs == null)
				{
					_framerefs = new AlignedVector3Array(btSoftBody_Cluster_getFramerefs(Native));
				}
				return _framerefs;
			}
		}

		public Matrix FrameTransform
		{
			get
			{
				Matrix value;
				btSoftBody_Cluster_getFramexform(Native, out value);
				return value;
			}
			set => btSoftBody_Cluster_setFramexform(Native, ref value);
		}

		public double Idmass
		{
			get => btSoftBody_Cluster_getIdmass(Native);
			set => btSoftBody_Cluster_setIdmass(Native, value);
		}

		public double InverseMass
		{
			get => btSoftBody_Cluster_getImass(Native);
			set => btSoftBody_Cluster_setImass(Native, value);
		}

		public Matrix InverseWorldInertia
		{
			get
			{
				Matrix value;
				btSoftBody_Cluster_getInvwi(Native, out value);
				return value;
			}
			set => btSoftBody_Cluster_setInvwi(Native, ref value);
		}

		public double LinearDamping
		{
			get => btSoftBody_Cluster_getLdamping(Native);
			set => btSoftBody_Cluster_setLdamping(Native, value);
		}

		public DbvtNode Leaf
		{
			get
			{
				IntPtr leafPtr = btSoftBody_Cluster_getLeaf(Native);
				if (_leaf != null && _leaf.Native == leafPtr) return _leaf;
				if (leafPtr == IntPtr.Zero) return null;
				_leaf = new DbvtNode(leafPtr);
				return _leaf;
			}
			set
			{
				btSoftBody_Cluster_setLeaf(Native, (value != null) ? value.Native : IntPtr.Zero);
				_leaf = value;
			}
		}

		public Vector3 LinearVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Cluster_getLv(Native, out value);
				return value;
			}
			set => btSoftBody_Cluster_setLv(Native, ref value);
		}

		public Matrix Locii
		{
			get
			{
				Matrix value;
				btSoftBody_Cluster_getLocii(Native, out value);
				return value;
			}
			set => btSoftBody_Cluster_setLocii(Native, ref value);
		}

		/*
		public AlignedScalarArray Masses
		{
			get
			{
				if (_masses == null)
				{
					_masses = new AlignedVector3Array(btSoftBody_Cluster_getMasses(Native));
				}
				return _masses;
			}
		}
		*/
		public double Matching
		{
			get => btSoftBody_Cluster_getMatching(Native);
			set => btSoftBody_Cluster_setMatching(Native, value);
		}

		public double MaxSelfCollisionImpulse
		{
			get => btSoftBody_Cluster_getMaxSelfCollisionImpulse(Native);
			set => btSoftBody_Cluster_setMaxSelfCollisionImpulse(Native, value);
		}

		public double NodeDamping
		{
			get => btSoftBody_Cluster_getNdamping(Native);
			set => btSoftBody_Cluster_setNdamping(Native, value);
		}

		public AlignedNodeArray Nodes
		{
			get
			{
				if (_nodes == null)
				{
					_nodes = new AlignedNodeArray(btSoftBody_Cluster_getNodes(Native));
				}
				return _nodes;
			}
		}

		public int NumDImpulses
		{
			get => btSoftBody_Cluster_getNdimpulses(Native);
			set => btSoftBody_Cluster_setNdimpulses(Native, value);
		}

		public int NumVImpulses
		{
			get => btSoftBody_Cluster_getNvimpulses(Native);
			set => btSoftBody_Cluster_setNvimpulses(Native, value);
		}

		public double SelfCollisionImpulseFactor
		{
			get => btSoftBody_Cluster_getSelfCollisionImpulseFactor(Native);
			set => btSoftBody_Cluster_setSelfCollisionImpulseFactor(Native, value);
		}

		public Vector3Array VImpulses
		{
			get
			{
				if (_vImpulses == null)
				{
					_vImpulses = new Vector3Array(btSoftBody_Cluster_getVimpulses(Native), 2);
				}
				return _vImpulses;
			}
		}
	}

	public class Config
	{
		internal IntPtr Native;

		//private AlignedPSolverArray _dSequence;
		//private AlignedPSolverArray _pSequence;
		//private AlignedVSolverArray _vSequence;

		internal Config(IntPtr native)
		{
			Native = native;
		}

		public AeroModel AeroModel
		{
			get => btSoftBody_Config_getAeromodel(Native);
			set => btSoftBody_Config_setAeromodel(Native, value);
		}

		public double AnchorHardness
		{
			get => btSoftBody_Config_getKAHR(Native);
			set => btSoftBody_Config_setKAHR(Native, value);
		}

		public int ClusterIterations
		{
			get => btSoftBody_Config_getCiterations(Native);
			set => btSoftBody_Config_setCiterations(Native, value);
		}

		public Collisions Collisions
		{
			get => btSoftBody_Config_getCollisions(Native);
			set => btSoftBody_Config_setCollisions(Native, value);
		}

		public double Damping
		{
			get => btSoftBody_Config_getKDP(Native);
			set => btSoftBody_Config_setKDP(Native, value);
		}

		public double DynamicFriction
		{
			get => btSoftBody_Config_getKDF(Native);
			set => btSoftBody_Config_setKDF(Native, value);
		}

		public double Drag
		{
			get => btSoftBody_Config_getKDG(Native);
			set => btSoftBody_Config_setKDG(Native, value);
		}

		public int DriftIterations
		{
			get => btSoftBody_Config_getDiterations(Native);
			set => btSoftBody_Config_setDiterations(Native, value);
		}
		/*
		public AlignedPSolverArray DriftSequence
		{
			get
			{
				if (_dSequence == null)
				{
					_dSequence = new AlignedPSolverArray(btSoftBody_Config_getDsequence(Native));
				}
				return _dsequence;
			}
		}
		*/
		public double KineticContactHardness
		{
			get => btSoftBody_Config_getKKHR(Native);
			set => btSoftBody_Config_setKKHR(Native, value);
		}

		public double Lift
		{
			get => btSoftBody_Config_getKLF(Native);
			set => btSoftBody_Config_setKLF(Native, value);
		}

		public double MaxVolume
		{
			get => btSoftBody_Config_getMaxvolume(Native);
			set => btSoftBody_Config_setMaxvolume(Native, value);
		}

		public double PoseMatching
		{
			get => btSoftBody_Config_getKMT(Native);
			set => btSoftBody_Config_setKMT(Native, value);
		}

		public int PositionIterations
		{
			get => btSoftBody_Config_getPiterations(Native);
			set => btSoftBody_Config_setPiterations(Native, value);
		}
		/*
		public AlignedPSolverArray PositionSequence
		{
			get
			{
				if (_pSequence == null)
				{
					_pSequence = new AlignedPSolverArray(btSoftBody_Config_getPsequence(Native));
				}
				return _psequence;
			}
		}
		*/
		public double Pressure
		{
			get => btSoftBody_Config_getKPR(Native);
			set => btSoftBody_Config_setKPR(Native, value);
		}

		public double RigidContactHardness
		{
			get => btSoftBody_Config_getKCHR(Native);
			set => btSoftBody_Config_setKCHR(Native, value);
		}

		public double SoftContactHardness
		{
			get => btSoftBody_Config_getKSHR(Native);
			set => btSoftBody_Config_setKSHR(Native, value);
		}

		public double SoftKineticHardness
		{
			get => btSoftBody_Config_getKSKHR_CL(Native);
			set => btSoftBody_Config_setKSKHR_CL(Native, value);
		}

		public double SoftKineticImpulseSplit
		{
			get => btSoftBody_Config_getKSK_SPLT_CL(Native);
			set => btSoftBody_Config_setKSK_SPLT_CL(Native, value);
		}

		public double SoftRigidHardness
		{
			get => btSoftBody_Config_getKSRHR_CL(Native);
			set => btSoftBody_Config_setKSRHR_CL(Native, value);
		}

		public double SoftRigidImpulseSplit
		{
			get => btSoftBody_Config_getKSR_SPLT_CL(Native);
			set => btSoftBody_Config_setKSR_SPLT_CL(Native, value);
		}

		public double SoftSoftHardness
		{
			get => btSoftBody_Config_getKSSHR_CL(Native);
			set => btSoftBody_Config_setKSSHR_CL(Native, value);
		}

		public double SoftSoftImpulseSplit
		{
			get => btSoftBody_Config_getKSS_SPLT_CL(Native);
			set => btSoftBody_Config_setKSS_SPLT_CL(Native, value);
		}

		public double VolumeConversation
		{
			get => btSoftBody_Config_getKVC(Native);
			set => btSoftBody_Config_setKVC(Native, value);
		}

		public double VelocityCorrectionFactor
		{
			get => btSoftBody_Config_getKVCF(Native);
			set => btSoftBody_Config_setKVCF(Native, value);
		}

		public double Timescale
		{
			get => btSoftBody_Config_getTimescale(Native);
			set => btSoftBody_Config_setTimescale(Native, value);
		}

		public int VelocityIterations
		{
			get => btSoftBody_Config_getViterations(Native);
			set => btSoftBody_Config_setViterations(Native, value);
		}
		/*
		public AlignedVSolverArray VelocitySequence
		{
			get
			{
				if (_vSequence == null)
				{
					_vSequence = new AlignedPSolverArray(btSoftBody_Config_getVsequence(Native));
				}
				return _vsequence;
			}
		}
		*/
	}

	public class Element
	{
		internal IntPtr Native;

		internal Element(IntPtr native)
		{
			Native = native;
		}

		public IntPtr Tag
		{
			get => btSoftBody_Element_getTag(Native);
			set => btSoftBody_Element_setTag(Native, value);
		}

		public override bool Equals(object obj)
		{
			Element element = obj as Element;
			if (element == null)
			{
				return false;
			}
			return Native == element.Native;
		}

		public override int GetHashCode()
		{
			return Native.GetHashCode();
		}
	}

	public class Face : Feature
	{
		private DbvtNode _leaf;
		private NodePtrArray _n;

		internal Face(IntPtr native)
			: base(native)
		{
		}

		public DbvtNode Leaf
		{
			get
			{
				IntPtr leafPtr = btSoftBody_Face_getLeaf(Native);
				if (_leaf != null && _leaf.Native == leafPtr) return _leaf;
				if (leafPtr == IntPtr.Zero) return null;
				_leaf = new DbvtNode(leafPtr);
				return _leaf;
			}
			set
			{
				btSoftBody_Face_setLeaf(Native, (value != null) ? value.Native : IntPtr.Zero);
				_leaf = value;
			}
		}

		public NodePtrArray Nodes
		{
			get
			{
				if (_n == null)
				{
					_n = new NodePtrArray(btSoftBody_Face_getN(Native), 3);
				}
				return _n;
			}
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_Face_getNormal(Native, out value);
				return value;
			}
			set => btSoftBody_Face_setNormal(Native, ref value);
		}

		public double RestArea
		{
			get => btSoftBody_Face_getRa(Native);
			set => btSoftBody_Face_setRa(Native, value);
		}
	}

	public class Feature : Element
	{
		private Material _material;

		internal Feature(IntPtr native)
			: base(native)
		{
		}

		public Material Material
		{
			get
			{
				IntPtr materialPtr = btSoftBody_Feature_getMaterial(Native);
				if (_material != null && _material.Native == materialPtr) return _material;
				if (materialPtr == IntPtr.Zero) return null;
				_material = new Material(materialPtr);
				return _material;
			}
			set
			{
				btSoftBody_Feature_setMaterial(Native, (value != null) ? value.Native : IntPtr.Zero);
				_material = value;
			}
		}
	}

	public abstract class ImplicitFn : IDisposable
	{
		internal IntPtr Native;

		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate double EvalUnmanagedDelegate([In] ref Vector3 x);

		private EvalUnmanagedDelegate _eval;

		protected ImplicitFn()
		{
			_eval = Eval;

			Native = btSoftBody_ImplicitFnWrapper_new(Marshal.GetFunctionPointerForDelegate(_eval));
		}

		public abstract double Eval(ref Vector3 x);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btSoftBody_ImplicitFn_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ImplicitFn()
		{
			Dispose(false);
		}
	}

	public class Impulse : IDisposable
	{
		internal IntPtr Native;

		internal Impulse(IntPtr native)
		{
			Native = native;
		}

		public Impulse()
		{
			Native = btSoftBody_Impulse_new();
		}
		/*
		public Impulse operator-()
		{
			return btSoftBody_Impulse_operator_n(Native);
		}

		public Impulse operator*(double x)
		{
			return btSoftBody_Impulse_operator_m(Native, x);
		}
		*/
		public int AsDrift
		{
			get => btSoftBody_Impulse_getAsDrift(Native);
			set => btSoftBody_Impulse_setAsDrift(Native, value);
		}

		public int AsVelocity
		{
			get => btSoftBody_Impulse_getAsVelocity(Native);
			set => btSoftBody_Impulse_setAsVelocity(Native, value);
		}

		public Vector3 Drift
		{
			get
			{
				Vector3 value;
				btSoftBody_Impulse_getDrift(Native, out value);
				return value;
			}
			set => btSoftBody_Impulse_setDrift(Native, ref value);
		}

		public Vector3 Velocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Impulse_getVelocity(Native, out value);
				return value;
			}
			set => btSoftBody_Impulse_setVelocity(Native, ref value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btSoftBody_Impulse_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~Impulse()
		{
			Dispose(false);
		}
	}

	public abstract class Joint : BulletObject
	{
		public class Specs : BulletDisposableObject
		{
			protected internal Specs()
			{
			}

			public double ConstraintForceMixing
			{
				get => btSoftBody_Joint_Specs_getCfm(Native);
				set => btSoftBody_Joint_Specs_setCfm(Native, value);
			}

			public double ErrorReductionParameter
			{
				get => btSoftBody_Joint_Specs_getErp(Native);
				set => btSoftBody_Joint_Specs_setErp(Native, value);
			}

			public double Split
			{
				get => btSoftBody_Joint_Specs_getSplit(Native);
				set => btSoftBody_Joint_Specs_setSplit(Native, value);
			}

			protected override void Dispose(bool disposing)
			{
				btSoftBody_Joint_Specs_delete(Native);
			}
		}

		private BodyArray _bodies;
		private Vector3Array _refs;

		internal static Joint GetManaged(IntPtr native)
		{
			if (native == IntPtr.Zero)
			{
				return null;
			}

			switch (btSoftBody_Joint_Type(native))
			{
				case JointType.Angular:
					return new AngularJoint(native);
				case JointType.Contact:
					return new ContactJoint(native);
				case JointType.Linear:
					return new LinearJoint(native);
				default:
					throw new NotImplementedException();
			}
		}

		protected internal Joint()
		{
		}

		public void Prepare(double deltaTime, int iterations)
		{
			btSoftBody_Joint_Prepare(Native, deltaTime, iterations);
		}

		public void Solve(double deltaTime, double sor)
		{
			btSoftBody_Joint_Solve(Native, deltaTime, sor);
		}

		public void Terminate(double deltaTime)
		{
			btSoftBody_Joint_Terminate(Native, deltaTime);
		}

		public BodyArray Bodies => _bodies ?? (_bodies = new BodyArray(btSoftBody_Joint_getBodies(Native), 2));

		public double ConstraintForceMixing
		{
			get => btSoftBody_Joint_getCfm(Native);
			set => btSoftBody_Joint_setCfm(Native, value);
		}

		public bool Delete
		{
			get => btSoftBody_Joint_getDelete(Native);
			set => btSoftBody_Joint_setDelete(Native, value);
		}

		public Vector3 Drift
		{
			get
			{
				Vector3 value;
				btSoftBody_Joint_getDrift(Native, out value);
				return value;
			}
			set => btSoftBody_Joint_setDrift(Native, ref value);
		}

		public double ErrorReductionParameter
		{
			get => btSoftBody_Joint_getErp(Native);
			set => btSoftBody_Joint_setErp(Native, value);
		}

		public Matrix MassMatrix
		{
			get
			{
				Matrix value;
				btSoftBody_Joint_getMassmatrix(Native, out value);
				return value;
			}
			set => btSoftBody_Joint_setMassmatrix(Native, ref value);
		}

		public Vector3Array Refs
		{
			get
			{
				if (_refs == null)
				{
					_refs = new Vector3Array(btSoftBody_Joint_getRefs(Native), 2);
				}
				return _refs;
			}
		}

		public Vector3 SplitDrift
		{
			get
			{
				Vector3 value;
				btSoftBody_Joint_getSdrift(Native, out value);
				return value;
			}
			set => btSoftBody_Joint_setSdrift(Native, ref value);
		}

		public double Split
		{
			get => btSoftBody_Joint_getSplit(Native);
			set => btSoftBody_Joint_setSplit(Native, value);
		}

		public JointType Type => btSoftBody_Joint_Type(Native);
	}

	public class Link : Feature
	{
		private NodePtrArray _n;

		internal Link(IntPtr native)
			: base(native)
		{
		}

		public double C0
		{
			get => btSoftBody_Link_getC0(Native);
			set => btSoftBody_Link_setC0(Native, value);
		}

		public double C1
		{
			get => btSoftBody_Link_getC1(Native);
			set => btSoftBody_Link_setC1(Native, value);
		}

		public double C2
		{
			get => btSoftBody_Link_getC2(Native);
			set => btSoftBody_Link_setC2(Native, value);
		}

		public Vector3 C3
		{
			get
			{
				Vector3 value;
				btSoftBody_Link_getC3(Native, out value);
				return value;
			}
			set => btSoftBody_Link_setC3(Native, ref value);
		}

		public int IsBending
		{
			get => btSoftBody_Link_getBbending(Native);
			set => btSoftBody_Link_setBbending(Native, value);
		}

		public NodePtrArray Nodes
		{
			get
			{
				if (_n == null)
				{
					_n = new NodePtrArray(btSoftBody_Link_getN(Native), 2);
				}
				return _n;
			}
		}

		public double RestLength
		{
			get => btSoftBody_Link_getRl(Native);
			set => btSoftBody_Link_setRl(Native, value);
		}
	}

	public class LinearJoint : Joint
	{
		public new class Specs : Joint.Specs
		{
			public Specs()
			{
				IntPtr native = btSoftBody_LJoint_Specs_new();
				InitializeUserOwned(native);
			}

			public Vector3 Position
			{
				get
				{
					Vector3 value;
					btSoftBody_LJoint_Specs_getPosition(Native, out value);
					return value;
				}
				set => btSoftBody_LJoint_Specs_setPosition(Native, ref value);
			}
		}

		private Vector3Array _rPos;

		internal LinearJoint(IntPtr native)
		{
			Initialize(native);
		}

		public Vector3Array RPos
		{
			get
			{
				if (_rPos == null)
				{
					_rPos = new Vector3Array(btSoftBody_LJoint_getRpos(Native), 2);
				}
				return _rPos;
			}
		}
	}

	public class Material : Element
	{
		internal Material(IntPtr native)
			: base(native)
		{
		}

		public double AngularStiffness
		{
			get => btSoftBody_Material_getKAST(Native);
			set => btSoftBody_Material_setKAST(Native, value);
		}

		public MaterialFlags Flags
		{
			get => btSoftBody_Material_getFlags(Native);
			set => btSoftBody_Material_setFlags(Native, value);
		}

		public double LinearStiffness
		{
			get => btSoftBody_Material_getKLST(Native);
			set => btSoftBody_Material_setKLST(Native, value);
		}

		public double VolumeStiffness
		{
			get => btSoftBody_Material_getKVST(Native);
			set => btSoftBody_Material_setKVST(Native, value);
		}
	}

	public class Node : Feature
	{
		private DbvtNode _leaf;

		internal Node(IntPtr native)
			: base(native)
		{
		}

		public double Area
		{
			get => btSoftBody_Node_getArea(Native);
			set => btSoftBody_Node_setArea(Native, value);
		}

		public Vector3 Force
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getF(Native, out value);
				return value;
			}
			set => btSoftBody_Node_setF(Native, ref value);
		}

		public int Index
		{
			get => btSoftBody_Node_getIndex(Native);
			set => btSoftBody_Node_setIndex(Native, value);
		}

		public double InverseMass
		{
			get => btSoftBody_Node_getIm(Native);
			set => btSoftBody_Node_setIm(Native, value);
		}

		public int IsAttached
		{
			get => btSoftBody_Node_getBattach(Native);
			set => btSoftBody_Node_setBattach(Native, value);
		}

		public DbvtNode Leaf
		{
			get
			{
				IntPtr leafPtr = btSoftBody_Node_getLeaf(Native);
				if (_leaf != null && _leaf.Native == leafPtr) return _leaf;
				if (leafPtr == IntPtr.Zero) return null;
				_leaf = new DbvtNode(leafPtr);
				return _leaf;
			}
			set
			{
				btSoftBody_Node_setLeaf(Native, (value != null) ? value.Native : IntPtr.Zero);
				_leaf = value;
			}
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getN(Native, out value);
				return value;
			}
			set => btSoftBody_Node_setN(Native, ref value);
		}

		public Vector3 Position
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getX(Native, out value);
				return value;
			}
			set => btSoftBody_Node_setX(Native, ref value);
		}

		public Vector3 Q
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getQ(Native, out value);
				return value;
			}
			set => btSoftBody_Node_setQ(Native, ref value);
		}

		public Vector3 Velocity
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getV(Native, out value);
				return value;
			}
			set => btSoftBody_Node_setV(Native, ref value);
		}

		public Vector3 VelocityPrevious
		{
			get
			{
				Vector3 value;
				btSoftBody_Node_getVN(Native, out value);
				return value;
			}
			set => btSoftBody_Node_setVN(Native, ref value);
		}
	}

	public class Note : Element
	{
		private NodePtrArray _nodes;

		internal Note(IntPtr native)
			: base(native)
		{
		}
		/*
		public doubleArray Coords
		{
			get { return btSoftBody_Note_getCoords(Native); }
		}
		*/
		public NodePtrArray Nodes
		{
			get
			{
				if (_nodes == null)
				{
					_nodes = new NodePtrArray(btSoftBody_Note_getNodes(Native), 4);
				}
				return _nodes;
			}
		}

		public Vector3 Offset
		{
			get
			{
				Vector3 value;
				btSoftBody_Note_getOffset(Native, out value);
				return value;
			}
			set => btSoftBody_Note_setOffset(Native, ref value);
		}

		public int Rank
		{
			get => btSoftBody_Note_getRank(Native);
			set => btSoftBody_Note_setRank(Native, value);
		}

		// TODO: free memory
		public string Text
		{
			get => btSoftBody_Note_getText(Native);
			set => btSoftBody_Note_setText(Native, Marshal.StringToHGlobalAnsi(value));
		}
	}

	public class Pose
	{
		internal IntPtr Native;

		private AlignedVector3Array _pos;
		//private AlignedScalarArray _wgh;

		internal Pose(IntPtr native)
		{
			Native = native;
		}

		public Matrix Aqq
		{
			get
			{
				Matrix value;
				btSoftBody_Pose_getAqq(Native, out value);
				return value;
			}
			set => btSoftBody_Pose_setAqq(Native, ref value);
		}

		public Vector3 Com
		{
			get
			{
				Vector3 value;
				btSoftBody_Pose_getCom(Native, out value);
				return value;
			}
			set => btSoftBody_Pose_setCom(Native, ref value);
		}

		public bool IsFrameValid
		{
			get => btSoftBody_Pose_getBframe(Native);
			set => btSoftBody_Pose_setBframe(Native, value);
		}

		public bool IsVolumeValid
		{
			get => btSoftBody_Pose_getBvolume(Native);
			set => btSoftBody_Pose_setBvolume(Native, value);
		}

		public AlignedVector3Array Positions
		{
			get
			{
				if (_pos == null)
				{
					_pos = new AlignedVector3Array(btSoftBody_Pose_getPos(Native));
				}
				return _pos;
			}
		}

		public Matrix Rotation
		{
			get
			{
				Matrix value;
				btSoftBody_Pose_getRot(Native, out value);
				return value;
			}
			set => btSoftBody_Pose_setRot(Native, ref value);
		}

		public Matrix Scale
		{
			get
			{
				Matrix value;
				btSoftBody_Pose_getScl(Native, out value);
				return value;
			}
			set => btSoftBody_Pose_setScl(Native, ref value);
		}
		/*
		public AlignedScalarArray Weights
		{
			get
			{
				if (_wgh == null)
				{
					_wgh = new AlignedScalarArray(btSoftBody_Pose_getWgh(_native));
				}
				return _wgh;
			}
		}
		*/
		public double Volume
		{
			get => btSoftBody_Pose_getVolume(Native);
			set => btSoftBody_Pose_setVolume(Native, value);
		}
	}

	public class RayFromToCaster : Dbvt.ICollide
	{
		private Face _face;

		public RayFromToCaster(Vector3 rayFrom, Vector3 rayTo, double mxt)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btSoftBody_RayFromToCaster_new(ref rayFrom, ref rayTo, mxt);
			InitializeUserOwned(native);
		}

		public static double RayFromToTriangle(Vector3 rayFrom, Vector3 rayTo,
			Vector3 rayNormalizedDirection, Vector3 a, Vector3 b, Vector3 c, double maxt = double.MaxValue)
		{
			return btSoftBody_RayFromToCaster_rayFromToTriangle(ref rayFrom,
				ref rayTo, ref rayNormalizedDirection, ref a, ref b, ref c, maxt);
		}

		public Face Face
		{
			get
			{
				if (_face == null)
				{
					IntPtr facePtr = btSoftBody_RayFromToCaster_getFace(Native);
					if (facePtr != IntPtr.Zero)
					{
						_face = new Face(facePtr);
					}
				}
				return _face;
			}
			set
			{
				btSoftBody_RayFromToCaster_setFace(Native, value.Native);
				_face = value;
			}
		}

		public double Mint
		{
			get => btSoftBody_RayFromToCaster_getMint(Native);
			set => btSoftBody_RayFromToCaster_setMint(Native, value);
		}

		public Vector3 RayFrom
		{
			get
			{
				Vector3 value;
				btSoftBody_RayFromToCaster_getRayFrom(Native, out value);
				return value;
			}
			set => btSoftBody_RayFromToCaster_setRayFrom(Native, ref value);
		}

		public Vector3 RayNormalizedDirection
		{
			get
			{
				Vector3 value;
				btSoftBody_RayFromToCaster_getRayNormalizedDirection(Native, out value);
				return value;
			}
			set => btSoftBody_RayFromToCaster_setRayNormalizedDirection(Native, ref value);
		}

		public Vector3 RayTo
		{
			get
			{
				Vector3 value;
				btSoftBody_RayFromToCaster_getRayTo(Native, out value);
				return value;
			}
			set => btSoftBody_RayFromToCaster_setRayTo(Native, ref value);
		}

		public int Tests
		{
			get => btSoftBody_RayFromToCaster_getTests(Native);
			set => btSoftBody_RayFromToCaster_setTests(Native, value);
		}
	}

	public class RigidContact : IDisposable
	{
		internal IntPtr Native;

		private Node _node;
		private ContactInfo _cti;

		internal RigidContact(IntPtr native)
		{
			Native = native;
		}

		public RigidContact()
		{
			Native = btSoftBody_RContact_new();
		}

		public Matrix C0
		{
			get
			{
				Matrix value;
				btSoftBody_RContact_getC0(Native, out value);
				return value;
			}
			set => btSoftBody_RContact_setC0(Native, ref value);
		}

		public Vector3 C1
		{
			get
			{
				Vector3 value;
				btSoftBody_RContact_getC1(Native, out value);
				return value;
			}
			set => btSoftBody_RContact_setC1(Native, ref value);
		}

		public double C2
		{
			get => btSoftBody_RContact_getC2(Native);
			set => btSoftBody_RContact_setC2(Native, value);
		}

		public double C3
		{
			get => btSoftBody_RContact_getC3(Native);
			set => btSoftBody_RContact_setC3(Native, value);
		}

		public double C4
		{
			get => btSoftBody_RContact_getC4(Native);
			set => btSoftBody_RContact_setC4(Native, value);
		}

		public ContactInfo Cti
		{
			get
			{
				if (_cti == null)
				{
					_cti = new ContactInfo(btSoftBody_RContact_getCti(Native));
				}
				return _cti;
			}
		}

		public Node Node
		{
			get
			{
				IntPtr nodePtr = btSoftBody_RContact_getNode(Native);
				if (_node != null && _node.Native == nodePtr) return _node;
				if (nodePtr == IntPtr.Zero) return null;
				_node = new Node(nodePtr);
				return _node;
			}
			set
			{
				btSoftBody_RContact_setNode(Native, (value != null) ? value.Native : IntPtr.Zero);
				_node = value;
			}
		}

		public Vector3 T1
		{
			get
			{
				Vector3 value;
				btSoftBody_RContact_getT1(Native, out value);
				return value;
			}
			set => btSoftBody_RContact_setT1(Native, ref value);
		}

		public Vector3 T2
		{
			get
			{
				Vector3 value;
				btSoftBody_RContact_getT2(Native, out value);
				return value;
			}
			set => btSoftBody_RContact_setT2(Native, ref value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btSoftBody_RContact_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~RigidContact()
		{
			Dispose(false);
		}
	}

	public class ContactInfo : IDisposable
	{
		internal IntPtr Native;

		internal ContactInfo(IntPtr native)
		{
			Native = native;
		}

		public ContactInfo()
		{
			Native = btSoftBody_sCti_new();
		}

		public CollisionObject CollisionObject
		{
			get => CollisionObject.GetManaged(btSoftBody_sCti_getColObj(Native));
			set => btSoftBody_sCti_setColObj(Native, (value != null) ? value.Native : IntPtr.Zero);
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_sCti_getNormal(Native, out value);
				return value;
			}
			set => btSoftBody_sCti_setNormal(Native, ref value);
		}

		public double Offset
		{
			get => btSoftBody_sCti_getOffset(Native);
			set => btSoftBody_sCti_setOffset(Native, value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btSoftBody_sCti_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ContactInfo()
		{
			Dispose(false);
		}
	}

	public class SoftContact : IDisposable
	{
		internal IntPtr Native;

		//private ScalarArray _cfm;
		private Face _face;
		private Node _node;

		internal SoftContact(IntPtr native)
		{
			Native = native;
		}

		public SoftContact()
		{
			Native = btSoftBody_SContact_new();
		}
		/*
		public ScalarArray ConstraintForceMixing
		{
			get
			{
				if (_cfm == null)
				{
					_cfm = new ScalarArray(btSoftBody_SContact_getCfm(Native), 2);
				}
				return _cfm;
			}
		}
		*/
		public Face Face
		{
			get
			{
				if (_face == null)
				{
					IntPtr facePtr = btSoftBody_SContact_getFace(Native);
					if (facePtr != IntPtr.Zero)
					{
						_face = new Face(facePtr);
					}
				}
				return _face;
			}
			set
			{
				btSoftBody_SContact_setFace(Native, (value != null) ? value.Native : IntPtr.Zero);
				_face = value;
			}
		}

		public double Friction
		{
			get => btSoftBody_SContact_getFriction(Native);
			set => btSoftBody_SContact_setFriction(Native, value);
		}

		public double Margin
		{
			get => btSoftBody_SContact_getMargin(Native);
			set => btSoftBody_SContact_setMargin(Native, value);
		}

		public Node Node
		{
			get
			{
				IntPtr nodePtr = btSoftBody_SContact_getNode(Native);
				if (_node != null && _node.Native == nodePtr) return _node;
				if (nodePtr == IntPtr.Zero) return null;
				_node = new Node(nodePtr);
				return _node;
			}
			set
			{
				btSoftBody_SContact_setNode(Native, value.Native);
				_node = value;
			}
		}

		public Vector3 Normal
		{
			get
			{
				Vector3 value;
				btSoftBody_SContact_getNormal(Native, out value);
				return value;
			}
			set => btSoftBody_SContact_setNormal(Native, ref value);
		}

		public Vector3 Weights
		{
			get
			{
				Vector3 value;
				btSoftBody_SContact_getWeights(Native, out value);
				return value;
			}
			set => btSoftBody_SContact_setWeights(Native, ref value);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btSoftBody_SContact_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~SoftContact()
		{
			Dispose(false);
		}
	}

	public class SolverState
	{
		internal IntPtr Native;

		internal SolverState(IntPtr native)
		{
			Native = native;
		}

		public double InverseSdt
		{
			get => btSoftBody_SolverState_getIsdt(Native);
			set => btSoftBody_SolverState_setIsdt(Native, value);
		}

		public double RadialMargin
		{
			get => btSoftBody_SolverState_getRadmrg(Native);
			set => btSoftBody_SolverState_setRadmrg(Native, value);
		}

		public double Sdt
		{
			get => btSoftBody_SolverState_getSdt(Native);
			set => btSoftBody_SolverState_setSdt(Native, value);
		}

		public double UpdateMargin
		{
			get => btSoftBody_SolverState_getUpdmrg(Native);
			set => btSoftBody_SolverState_setUpdmrg(Native, value);
		}

		public double VelocityMargin
		{
			get => btSoftBody_SolverState_getVelmrg(Native);
			set => btSoftBody_SolverState_setVelmrg(Native, value);
		}
	}

	public class SoftBodyRayCast
	{
		public SoftBody Body { get; set; }
		public FeatureType Feature { get; set; }
		public double Fraction { get; set; }
		public int Index { get; set; }
	}

	public class Tetra : Feature
	{
		private Vector3Array _c0;
		private DbvtNode _leaf;
		private NodePtrArray _nodes;

		internal Tetra(IntPtr native)
			: base(native)
		{
		}

		public Vector3Array C0
		{
			get
			{
				if (_c0 == null)
				{
					_c0 = new Vector3Array(btSoftBody_Tetra_getC0(Native), 4);
				}
				return _c0;
			}
		}

		public double C1
		{
			get => btSoftBody_Tetra_getC1(Native);
			set => btSoftBody_Tetra_setC1(Native, value);
		}

		public double C2
		{
			get => btSoftBody_Tetra_getC2(Native);
			set => btSoftBody_Tetra_setC2(Native, value);
		}

		public DbvtNode Leaf
		{
			get
			{
				IntPtr leafPtr = btSoftBody_Tetra_getLeaf(Native);
				if (_leaf != null && _leaf.Native == leafPtr) return _leaf;
				if (leafPtr == IntPtr.Zero) return null;
				_leaf = new DbvtNode(leafPtr);
				return _leaf;
			}
			set
			{
				btSoftBody_Tetra_setLeaf(Native, (value != null) ? value.Native : IntPtr.Zero);
				_leaf = value;
			}
		}

		public NodePtrArray Nodes
		{
			get
			{
				if (_nodes == null)
				{
					_nodes = new NodePtrArray(btSoftBody_Tetra_getN(Native), 4);
				}
				return _nodes;
			}
		}

		public double RestVolume
		{
			get => btSoftBody_Tetra_getRv(Native);
			set => btSoftBody_Tetra_setRv(Native, value);
		}
	}

	public class TetraScratch
	{
		internal IntPtr Native;

		internal TetraScratch(IntPtr native)
		{
			Native = native;
		}

		public Matrix F
		{
			get
			{
				Matrix value;
				btSoftBody_TetraScratch_getF(Native, out value);
				return value;
			}
			set => btSoftBody_TetraScratch_setF(Native, ref value);
		}
	}

	public class SoftBody : CollisionObject
	{
		private AlignedAnchorArray _anchors;
		private Vector3Array _bounds;
		private Dbvt _clusterDbvt;
		private Config _config;
		//private AlignedBoolArray _clusterConnectivity;
		private AlignedClusterArray _clusters;
		//private AlignedCollisionObjectArray _collisionDisabledObjects;
		private Dbvt _faceDbvt;
		private AlignedFaceArray _faces;
		private AlignedJointArray _joints;
		private AlignedLinkArray _links;
		private AlignedMaterialArray _materials;
		private Dbvt _nodeDbvt;
		private AlignedNodeArray _nodes;
		private AlignedNoteArray _notes;
		private Pose _pose;
		//private AlignedRigidContactArray _rigidContacts;
		//private AlignedSoftContactArray _softContacts;
		private SoftBodySolver _softBodySolver;
		private SolverState _solverState;
		private AlignedTetraArray _tetras;
		private AlignedTetraScratchArray _tetraScratches;
		private AlignedTetraScratchArray _tetraScratchesTn;
		//private AlignedIntArray _userIndexMapping;
		private SoftBodyWorldInfo _worldInfo;

		private List<AngularJoint.IControl> _aJointControls = new List<AngularJoint.IControl>();

		internal SoftBody(IntPtr native)
			: base(ConstructionInfo.Null)
		{
			InitializeCollisionObject(native);

			_collisionShape = new SoftBodyCollisionShape(btCollisionObject_getCollisionShape(Native), this);
			_collisionShape.AllocateUnmanagedHandle();

		}

		public SoftBody(SoftBodyWorldInfo worldInfo, int nodeCount, Vector3[] positions, double[] masses)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btSoftBody_new(worldInfo.Native, nodeCount, positions, masses);
			InitializeCollisionObject(native);

			_collisionShape = new SoftBodyCollisionShape(btCollisionObject_getCollisionShape(Native), this);
			_collisionShape.AllocateUnmanagedHandle();
			_worldInfo = worldInfo;
		}

		public SoftBody(SoftBodyWorldInfo worldInfo)
			: base(ConstructionInfo.Null)
		{
			IntPtr native = btSoftBody_new2(worldInfo.Native);
			InitializeCollisionObject(native);

			_collisionShape = new SoftBodyCollisionShape(btCollisionObject_getCollisionShape(Native), this);
			_collisionShape.AllocateUnmanagedHandle();
			_worldInfo = worldInfo;
		}

		public void AddAeroForceToFace(Vector3 windVelocity, int faceIndex)
		{
			btSoftBody_addAeroForceToFace(Native, ref windVelocity, faceIndex);
		}

		public void AddAeroForceToNode(Vector3 windVelocity, int nodeIndex)
		{
			btSoftBody_addAeroForceToNode(Native, ref windVelocity, nodeIndex);
		}

		public void AddForce(Vector3 force)
		{
			btSoftBody_addForce(Native, ref force);
		}

		public void AddForce(Vector3 force, int node)
		{
			btSoftBody_addForce2(Native, ref force, node);
		}

		public void AddVelocity(Vector3 velocity)
		{
			btSoftBody_addVelocity(Native, ref velocity);
		}

		public void AddVelocity(Vector3 velocity, int node)
		{
			btSoftBody_addVelocity2(Native, ref velocity, node);
		}

		public void AppendAnchor(int node, RigidBody body, Vector3 localPivot, bool disableCollisionBetweenLinkedBodies = false,
			double influence = 1.0f)
		{
			btSoftBody_appendAnchor(Native, node, body.Native, ref localPivot,
				disableCollisionBetweenLinkedBodies, influence);
		}

		public void AppendAnchor(int node, RigidBody body, bool disableCollisionBetweenLinkedBodies = false,
			double influence = 1.0f)
		{
			btSoftBody_appendAnchor2(Native, node, body.Native, disableCollisionBetweenLinkedBodies,
				influence);
		}

		private void StoreAngularJointControlRef(AngularJoint.Specs specs)
		{
			if (specs.Control != null && specs.Control != AngularJoint.IControl.Default)
			{
				_aJointControls.Add(specs.Control);
			}
		}

		public void AppendAngularJoint(AngularJoint.Specs specs)
		{
			StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint(Native, specs.Native);
		}

		public void AppendAngularJoint(AngularJoint.Specs specs, Body body)
		{
			StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint2(Native, specs.Native, body.Native);
		}

		public void AppendAngularJoint(AngularJoint.Specs specs, SoftBody body)
		{
			StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint3(Native, specs.Native, body.Native);
		}

		public void AppendAngularJoint(AngularJoint.Specs specs, Cluster body0, Body body1)
		{
			StoreAngularJointControlRef(specs);
			btSoftBody_appendAngularJoint4(Native, specs.Native, body0.Native, body1.Native);
		}

		public void AppendFace(int model = -1, Material mat = null)
		{
			btSoftBody_appendFace(Native, model, mat != null ? mat.Native : IntPtr.Zero);
		}

		public void AppendFace(int node0, int node1, int node2, Material mat = null)
		{
			btSoftBody_appendFace2(Native, node0, node1, node2, mat != null ? mat.Native : IntPtr.Zero);
		}

		public void AppendLinearJoint(LinearJoint.Specs specs, SoftBody body)
		{
			btSoftBody_appendLinearJoint(Native, specs.Native, body.Native);
		}

		public void AppendLinearJoint(LinearJoint.Specs specs)
		{
			btSoftBody_appendLinearJoint2(Native, specs.Native);
		}

		public void AppendLinearJoint(LinearJoint.Specs specs, Body body)
		{
			btSoftBody_appendLinearJoint3(Native, specs.Native, body.Native);
		}

		public void AppendLinearJoint(LinearJoint.Specs specs, Cluster body0, Body body1)
		{
			btSoftBody_appendLinearJoint4(Native, specs.Native, body0.Native, body1.Native);
		}

		public void AppendLink(int node0, int node1, Material mat = null, bool checkExist = false)
		{
			btSoftBody_appendLink(Native, node0, node1, (mat != null) ? mat.Native : IntPtr.Zero, checkExist);
		}

		public void AppendLink(int model = -1, Material mat = null)
		{
			btSoftBody_appendLink2(Native, model, (mat != null) ? mat.Native : IntPtr.Zero);
		}

		public void AppendLink(Node node0, Node node1, Material mat = null, bool checkExist = false)
		{
			btSoftBody_appendLink3(Native, node0.Native, node1.Native, (mat != null) ? mat.Native : IntPtr.Zero, checkExist);
		}

		public Material AppendMaterial()
		{
			return new Material(btSoftBody_appendMaterial(Native));
		}

		public void AppendNode(Vector3 x, double m)
		{
			btSoftBody_appendNode(Native, ref x, m);
		}

		public void AppendNote(string text, Vector3 o, Face feature)
		{
			btSoftBody_appendNote(Native, Marshal.StringToHGlobalAnsi(text), ref o, feature.Native);
		}

		public void AppendNote(string text, Vector3 o, Link feature)
		{
			btSoftBody_appendNote2(Native, Marshal.StringToHGlobalAnsi(text), ref o, feature.Native);
		}

		public void AppendNote(string text, Vector3 o, Node feature)
		{
			btSoftBody_appendNote3(Native, Marshal.StringToHGlobalAnsi(text), ref o, feature.Native);
		}

		public void AppendNote(string text, Vector3 o)
		{
			btSoftBody_appendNote4(Native, Marshal.StringToHGlobalAnsi(text), ref o);
		}

		public void AppendNote(string text, Vector3 o, Vector4 c,
			Node n0 = null, Node n1 = null, Node n2 = null, Node n3 = null)
		{
			btSoftBody_appendNote5(Native, Marshal.StringToHGlobalAnsi(text), ref o, ref c,
				n0 != null ? n0.Native : IntPtr.Zero,
				n1 != null ? n1.Native : IntPtr.Zero,
				n2 != null ? n2.Native : IntPtr.Zero,
				n3 != null ? n3.Native : IntPtr.Zero);
		}

		public void AppendTetra(int model, Material mat)
		{
			btSoftBody_appendTetra(Native, model, mat != null ? mat.Native : IntPtr.Zero);
		}

		public void AppendTetra(int node0, int node1, int node2, int node3, Material mat = null)
		{
			btSoftBody_appendTetra2(Native, node0, node1, node2, node3, mat != null ? mat.Native : IntPtr.Zero);
		}

		public void ApplyClusters(bool drift)
		{
			btSoftBody_applyClusters(Native, drift);
		}

		public void ApplyForces()
		{
			btSoftBody_applyForces(Native);
		}

		public bool CheckContact(CollisionObjectWrapper colObjWrap, Vector3 x, double margin,
			ContactInfo cti)
		{
			return btSoftBody_checkContact(Native, colObjWrap.Native, ref x, margin,
				cti.Native);
		}

		public bool CheckDeformableContact(CollisionObjectWrapper colObjWrap, Vector3 x, float margin,
			ContactInfo cti, bool predict = false)
		{
			return btSoftBody_checkDeformableContact(Native, colObjWrap.Native, ref x, margin,
				cti.Native, predict);
		}

		public bool CheckFace(int node0, int node1, int node2)
		{
			return btSoftBody_checkFace(Native, node0, node1, node2);
		}

		public bool CheckLink(Node node0, Node node1)
		{
			return btSoftBody_checkLink(Native, node0.Native, node1.Native);
		}

		public bool CheckLink(int node0, int node1)
		{
			return btSoftBody_checkLink2(Native, node0, node1);
		}

		public void CleanupClusters()
		{
			_aJointControls.Clear();
			btSoftBody_cleanupClusters(Native);
		}

		public static void ClusterAImpulse(Cluster cluster, Impulse impulse)
		{
			btSoftBody_clusterAImpulse(cluster.Native, impulse.Native);
		}

		public Vector3 ClusterCom(int cluster)
		{
			Vector3 value;
			btSoftBody_clusterCom(Native, cluster, out value);
			return value;
		}

		public static Vector3 ClusterCom(Cluster cluster)
		{
			Vector3 value;
			btSoftBody_clusterCom2(cluster.Native, out value);
			return value;
		}

		public int ClusterCount()
		{
			return btSoftBody_clusterCount(Native);
		}

		public static void ClusterDAImpulse(Cluster cluster, Vector3 impulse)
		{
			btSoftBody_clusterDAImpulse(cluster.Native, ref impulse);
		}

		public static void ClusterDCImpulse(Cluster cluster, Vector3 impulse)
		{
			btSoftBody_clusterDCImpulse(cluster.Native, ref impulse);
		}

		public static void ClusterDImpulse(Cluster cluster, Vector3 rpos, Vector3 impulse)
		{
			btSoftBody_clusterDImpulse(cluster.Native, ref rpos, ref impulse);
		}

		public static void ClusterImpulse(Cluster cluster, Vector3 rpos, Impulse impulse)
		{
			btSoftBody_clusterImpulse(cluster.Native, ref rpos, impulse.Native);
		}

		public static void ClusterVAImpulse(Cluster cluster, Vector3 impulse)
		{
			btSoftBody_clusterVAImpulse(cluster.Native, ref impulse);
		}

		public static Vector3 ClusterVelocity(Cluster cluster, Vector3 rpos)
		{
			Vector3 value;
			btSoftBody_clusterVelocity(cluster.Native, ref rpos, out value);
			return value;
		}

		public static void ClusterVImpulse(Cluster cluster, Vector3 rpos, Vector3 impulse)
		{
			btSoftBody_clusterVImpulse(cluster.Native, ref rpos, ref impulse);
		}

		public bool CutLink(Node node0, Node node1, double position)
		{
			return btSoftBody_cutLink(Native, node0.Native, node1.Native, position);
		}

		public bool CutLink(int node0, int node1, double position)
		{
			return btSoftBody_cutLink2(Native, node0, node1, position);
		}

		public void DampClusters()
		{
			btSoftBody_dampClusters(Native);
		}

		public void DefaultCollisionHandler(CollisionObjectWrapper pcoWrap)
		{
			btSoftBody_defaultCollisionHandler(Native, pcoWrap.Native);
		}

		public void DefaultCollisionHandler(SoftBody psb)
		{
			btSoftBody_defaultCollisionHandler2(Native, psb.Native);
		}

		public Vector3 EvaluateCom()
		{
			Vector3 value;
			btSoftBody_evaluateCom(Native, out value);
			return value;
		}

		public int GenerateBendingConstraints(int distance, Material mat = null)
		{
			return btSoftBody_generateBendingConstraints(Native, distance, mat != null ? mat.Native : IntPtr.Zero);
		}

		public int GenerateClusters(int k)
		{
			return btSoftBody_generateClusters(Native, k);
		}

		public int GenerateClusters(int k, int maxIterations)
		{
			return btSoftBody_generateClusters2(Native, k, maxIterations);
		}

		public void GetAabb(out Vector3 aabbMin, out Vector3 aabbMax)
		{
			btSoftBody_getAabb(Native, out aabbMin, out aabbMax);
		}

		public double GetMass(int node)
		{
			return btSoftBody_getMass(Native, node);
		}
		/*
		public static psolver_t GetSolver(PositionSolver solver)
		{
			return btSoftBody_getSolver(solver._native);
		}

		public static vsolver_t GetSolver(VelocitySolver solver)
		{
			return btSoftBody_getSolver2(solver._native);
		}
		*/
		public void IndicesToPointers(int[] map = null)
		{
			btSoftBody_indicesToPointers(Native, map);
		}

		public void InitDefaults()
		{
			btSoftBody_initDefaults(Native);
		}

		public void InitializeClusters()
		{
			btSoftBody_initializeClusters(Native);
		}

		public void InitializeFaceTree()
		{
			btSoftBody_initializeFaceTree(Native);
		}

		public void InitializeDmInverse()
		{
			btSoftBody_initializeDmInverse(Native);
		}

		public void IntegrateMotion()
		{
			btSoftBody_integrateMotion(Native);
		}

		public void PointersToIndices()
		{
			btSoftBody_pointersToIndices(Native);
		}

		public void PredictMotion(double deltaTime)
		{
			btSoftBody_predictMotion(Native, deltaTime);
		}

		public void PrepareClusters(int iterations)
		{
			btSoftBody_prepareClusters(Native, iterations);
		}

		public static void PSolveAnchors(SoftBody psb, double kst, double ti)
		{
			btSoftBody_PSolve_Anchors(psb.Native, kst, ti);
		}

		public static void PSolveLinks(SoftBody psb, double kst, double ti)
		{
			btSoftBody_PSolve_Links(psb.Native, kst, ti);
		}

		public static void PSolveRContacts(SoftBody psb, double kst, double ti)
		{
			btSoftBody_PSolve_RContacts(psb.Native, kst, ti);
		}

		public static void PSolveSContacts(SoftBody psb, double __unnamed1, double ti)
		{
			btSoftBody_PSolve_SContacts(psb.Native, __unnamed1, ti);
		}

		public void RandomizeConstraints()
		{
			btSoftBody_randomizeConstraints(Native);
		}

		public bool RayTestRef(ref Vector3 rayFrom, ref Vector3 rayTo, SoftBodyRayCast results)
		{
			IntPtr rayCast = btSoftBody_sRayCast_new();
			bool ret = btSoftBody_rayTest(Native, ref rayFrom, ref rayTo, rayCast);
			results.Body = this;
			results.Feature = btSoftBody_sRayCast_getFeature(rayCast);
			results.Fraction = btSoftBody_sRayCast_getFraction(rayCast);
			results.Index = btSoftBody_sRayCast_getIndex(rayCast);
			btSoftBody_sRayCast_delete(rayCast);
			return ret;
		}

		public bool RayTest(Vector3 rayFrom, Vector3 rayTo, SoftBodyRayCast results)
		{
			IntPtr rayCast = btSoftBody_sRayCast_new();
			bool ret = btSoftBody_rayTest(Native, ref rayFrom, ref rayTo, rayCast);
			results.Body = this;
			results.Feature = btSoftBody_sRayCast_getFeature(rayCast);
			results.Fraction = btSoftBody_sRayCast_getFraction(rayCast);
			results.Index = btSoftBody_sRayCast_getIndex(rayCast);
			btSoftBody_sRayCast_delete(rayCast);
			return ret;
		}

		public int RayTest(Vector3 rayFrom, Vector3 rayTo, ref double mint, out FeatureType feature,
			out int index, bool countOnly)
		{
			return btSoftBody_rayTest2(Native, ref rayFrom, ref rayTo, ref mint,
				out feature, out index, countOnly);
		}

		public void Refine(ImplicitFn ifn, double accurary, bool cut)
		{
			btSoftBody_refine(Native, ifn.Native, accurary, cut);
		}

		public void ReleaseCluster(int index)
		{
			btSoftBody_releaseCluster(Native, index);
		}

		public void ReleaseClusters()
		{
			btSoftBody_releaseClusters(Native);
		}

		public void ResetLinkRestLengths()
		{
			btSoftBody_resetLinkRestLengths(Native);
		}

		public void Rotate(Quaternion rot)
		{
			btSoftBody_rotate(Native, ref rot);
		}

		public void Scale(Vector3 scl)
		{
			btSoftBody_scale(Native, ref scl);
		}

		public void SetMass(int node, double mass)
		{
			btSoftBody_setMass(Native, node, mass);
		}

		public void SetPose(bool bvolume, bool bframe)
		{
			btSoftBody_setPose(Native, bvolume, bframe);
		}
		/*
		public void SetSolver(SolverPresets preset)
		{
			btSoftBody_setSolver(_native, preset._native);
		}
		*/
		public void SetTotalDensity(double density)
		{
			btSoftBody_setTotalDensity(Native, density);
		}

		public void SetTotalMass(double mass, bool fromFaces = false)
		{
			btSoftBody_setTotalMass(Native, mass, fromFaces);
		}

		public void SetVelocity(Vector3 velocity)
		{
			btSoftBody_setVelocity(Native, ref velocity);
		}

		public void SetVolumeDensity(double density)
		{
			btSoftBody_setVolumeDensity(Native, density);
		}

		public void SetVolumeMass(double mass)
		{
			btSoftBody_setVolumeMass(Native, mass);
		}

		public static void SolveClusters(AlignedSoftBodyArray bodies)
		{
			btSoftBody_solveClusters(bodies.Native);
		}

		public void SolveClusters(double sor)
		{
			btSoftBody_solveClusters2(Native, sor);
		}

		public static void SolveCommonConstraints(SoftBody bodies, int count, int iterations)
		{
			btSoftBody_solveCommonConstraints(bodies.Native, count, iterations);
		}

		public void SolveConstraints()
		{
			btSoftBody_solveConstraints(Native);
		}

		public void StaticSolve(int iterations)
		{
			btSoftBody_staticSolve(Native, iterations);
		}

		public void Transform(Matrix trs)
		{
			btSoftBody_transform(Native, ref trs);
		}

		public void Translate(Vector3 trs)
		{
			btSoftBody_translate(Native, ref trs);
		}

		public void Translate(double x, double y, double z)
		{
			Vector3 trs = new Vector3(x, y, z);
			btSoftBody_translate(Native, ref trs);
		}
		/*
		public static SoftBody Upcast(CollisionObject colObj)
		{
			return btSoftBody_upcast(colObj._native);
		}
		*/
		public void UpdateArea(bool averageArea = true)
		{
			btSoftBody_updateArea(Native, averageArea);
		}

		public void UpdateBounds()
		{
			btSoftBody_updateBounds(Native);
		}

		public void UpdateClusters()
		{
			btSoftBody_updateClusters(Native);
		}

		public void UpdateConstants()
		{
			btSoftBody_updateConstants(Native);
		}

		public void UpdateLinkConstants()
		{
			btSoftBody_updateLinkConstants(Native);
		}

		public void UpdateNormals()
		{
			btSoftBody_updateNormals(Native);
		}

		public void UpdatePose()
		{
			btSoftBody_updatePose(Native);
		}

		public static void VSolveLinks(SoftBody psb, double kst)
		{
			btSoftBody_VSolve_Links(psb.Native, kst);
		}

		public int GetFaceVertexData(ref double[] vertices)
		{
			int doubleCount = Faces.Count * 3 * 3;

			// Do not use Array.Resize, because it copies the old data
			if (vertices == null || vertices.Length != doubleCount)
			{
				vertices = new double[doubleCount];
			}

			return btSoftBody_getFaceVertexData(Native, vertices);
		}

		public int GetFaceVertexData(ref Vector3[] vertices)
		{
			int vertexCount = Faces.Count * 3;

			if (vertices == null || vertices.Length != vertexCount)
			{
				vertices = new Vector3[vertexCount];
			}

			return btSoftBody_getFaceVertexData(Native, vertices);
		}

		public int GetFaceVertexNormalData(ref Vector3[] vertices)
		{
			int vertexNormalCount = Faces.Count * 3 * 2;

			if (vertices == null || vertices.Length != vertexNormalCount)
			{
				vertices = new Vector3[vertexNormalCount];
			}

			return btSoftBody_getFaceVertexNormalData(Native, vertices);
		}

		public int GetFaceVertexNormalData(ref Vector3[] vertices, ref Vector3[] normals)
		{
			int vertexCount = Faces.Count * 3;

			if (vertices == null || vertices.Length != vertexCount)
			{
				vertices = new Vector3[vertexCount];
			}
			if (normals == null || normals.Length != vertexCount)
			{
				normals = new Vector3[vertexCount];
			}

			return btSoftBody_getFaceVertexNormalData2(Native, vertices, normals);
		}

		public int GetLinkVertexData(ref Vector3[] vertices)
		{
			int vertexCount = Links.Count * 2;

			if (vertices == null || vertices.Length != vertexCount)
			{
				vertices = new Vector3[vertexCount];
			}

			return btSoftBody_getLinkVertexData(Native, vertices);
		}

		public int GetLinkVertexNormalData(ref Vector3[] vertices)
		{
			int vertexNormalCount = Links.Count * 2 * 2;

			if (vertices == null || vertices.Length != vertexNormalCount)
			{
				vertices = new Vector3[vertexNormalCount];
			}

			return btSoftBody_getLinkVertexNormalData(Native, vertices);
		}

		int GetTetraVertexData(ref Vector3[] vertices)
		{
			int vertexCount = Tetras.Count * 12;

			if (vertices == null || vertices.Length != vertexCount)
			{
				vertices = new Vector3[vertexCount];
			}

			return btSoftBody_getTetraVertexData(Native, vertices);
		}

		int GetTetraVertexNormalData(ref Vector3[] vertices)
		{
			int vertexNormalCount = Tetras.Count * 12 * 2;

			if (vertices == null || vertices.Length != vertexNormalCount)
			{
				vertices = new Vector3[vertexNormalCount];
			}

			return btSoftBody_getTetraVertexNormalData(Native, vertices);
		}

		int GetTetraVertexNormalData(ref Vector3[] vertices, ref Vector3[] normals)
		{
			int vertexCount = Tetras.Count * 12;

			if (vertices == null || vertices.Length != vertexCount)
			{
				vertices = new Vector3[vertexCount];
			}
			if (normals == null || normals.Length != vertexCount)
			{
				normals = new Vector3[vertexCount];
			}

			return btSoftBody_getTetraVertexNormalData2(Native, vertices, normals);
		}

		public int GetVertexNormalData(ref Vector3[] data)
		{
			if (Faces.Count != 0)
			{
				return GetFaceVertexNormalData(ref data);
			}
			else if (Tetras.Count != 0)
			{
				return GetTetraVertexNormalData(ref data);
			}
			return GetLinkVertexNormalData(ref data);
		}

		public int GetVertexNormalData(ref Vector3[] vertices, ref Vector3[] normals)
		{
			if (Faces.Count != 0)
			{
				return GetFaceVertexNormalData(ref vertices, ref normals);
			}
			else if (Tetras.Count != 0)
			{
				return GetTetraVertexNormalData(ref vertices, ref normals);
			}
			normals = null;
			return GetLinkVertexData(ref vertices);
		}

		protected override void Dispose(bool disposing)
		{
			_collisionShape.FreeUnmanagedHandle();

			base.Dispose(disposing);
		}

		public AlignedAnchorArray Anchors
		{
			get
			{
				if (_anchors == null)
				{
					_anchors = new AlignedAnchorArray(btSoftBody_getAnchors(Native));
				}
				return _anchors;
			}
		}

		public Vector3Array Bounds
		{
			get
			{
				if (_bounds == null)
				{
					_bounds = new Vector3Array(btSoftBody_getBounds(Native), 2);
				}
				return _bounds;
			}
		}

		public Dbvt ClusterDbvt
		{
			get
			{
				if (_clusterDbvt == null)
				{
					_clusterDbvt = new Dbvt(btSoftBody_getCdbvt(Native));
				}
				return _clusterDbvt;
			}
		}

		public Config Cfg
		{
			get
			{
				if (_config == null)
				{
					_config = new Config(btSoftBody_getCfg(Native));
				}
				return _config;
			}
		}
		/*
		public AlignedObjectArray ClusterConnectivity
		{
			get { return btSoftBody_getClusterConnectivity(_native); }
			set { btSoftBody_setClusterConnectivity(_native, value._native); }
		}
		*/
		public AlignedClusterArray Clusters
		{
			get
			{
				if (_clusters == null)
				{
					_clusters = new AlignedClusterArray(btSoftBody_getClusters(Native));
				}
				return _clusters;
			}
		}
		/*
		public AlignedObjectArray<CollisionObject> CollisionDisabledObjects
		{
			get { return btSoftBody_getCollisionDisabledObjects(_native); }
			set { btSoftBody_setCollisionDisabledObjects(_native, value._native); }
		}
		*/
		public double DampingCoefficient
		{
			get { return btSoftBody_getDampingCoefficient(Native); }
			set { btSoftBody_setDampingCoefficient(Native, value); }
		}

		public AlignedFaceArray Faces
		{
			get
			{
				if (_faces == null)
				{
					_faces = new AlignedFaceArray(btSoftBody_getFaces(Native));
				}
				return _faces;
			}
		}

		public Dbvt FaceDbvt
		{
			get
			{
				if (_faceDbvt == null)
				{
					_faceDbvt = new Dbvt(btSoftBody_getFdbvt(Native));
				}
				return _faceDbvt;
			}
		}

		public AlignedJointArray Joints
		{
			get
			{
				if (_joints == null)
				{
					_joints = new AlignedJointArray(btSoftBody_getJoints(Native));
				}
				return _joints;
			}
		}

		public AlignedLinkArray Links
		{
			get
			{
				if (_links == null)
				{
					_links = new AlignedLinkArray(btSoftBody_getLinks(Native));
				}
				return _links;
			}
		}

		public AlignedMaterialArray Materials
		{
			get
			{
				if (_materials == null)
				{
					_materials = new AlignedMaterialArray(btSoftBody_getMaterials(Native));
				}
				return _materials;
			}
		}

		public Dbvt NodeDbvt
		{
			get
			{
				if (_nodeDbvt == null)
				{
					_nodeDbvt = new Dbvt(btSoftBody_getNdbvt(Native));
				}
				return _nodeDbvt;
			}
		}

		public AlignedNodeArray Nodes
		{
			get
			{
				if (_nodes == null)
				{
					_nodes = new AlignedNodeArray(btSoftBody_getNodes(Native));
				}
				return _nodes;
			}
		}

		public AlignedNoteArray Notes
		{
			get
			{
				if (_notes == null)
				{
					_notes = new AlignedNoteArray(btSoftBody_getNotes(Native));
				}
				return _notes;
			}
		}

		public Pose Pose
		{
			get
			{
				if (_pose == null)
				{
					_pose = new Pose(btSoftBody_getPose(Native));
				}
				return _pose;
			}
		}
		/*
		public tRContactArray Rcontacts
		{
			get { return btSoftBody_getRcontacts(_native); }
			set { btSoftBody_setRcontacts(_native, value._native); }
		}
		*/
		public double RestLengthScale
		{
			get => btSoftBody_getRestLengthScale(Native);
			set => btSoftBody_setRestLengthScale(Native, value);
		}
		/*
		public tSContactArray Scontacts
		{
			get { return btSoftBody_getScontacts(_native); }
			set { btSoftBody_setScontacts(_native, value._native); }
		}
		*/
		public SoftBodySolver SoftBodySolver
		{
			get => _softBodySolver;
			set
			{
				btSoftBody_setSoftBodySolver(Native, (value != null) ? value.Native : IntPtr.Zero);
				_softBodySolver = value;
			}
		}

		public SolverState SolverState
		{
			get
			{
				if (_solverState == null)
				{
					_solverState = new SolverState(btSoftBody_getSst(Native));
				}
				return _solverState;
			}
		}

		public object Tag { get; set; }

		public AlignedTetraArray Tetras
		{
			get
			{
				if (_tetras == null)
				{
					_tetras = new AlignedTetraArray(btSoftBody_getTetras(Native));
				}
				return _tetras;
			}
		}

		public AlignedTetraScratchArray TetraScratches
		{
			get
			{
				if (_tetraScratches == null)
				{
					_tetraScratches = new AlignedTetraScratchArray(btSoftBody_getTetraScratches(Native));
				}
				return _tetraScratches;
			}
		}

		public AlignedTetraScratchArray TetraScratchesTn
		{
			get
			{
				if (_tetraScratchesTn == null)
				{
					_tetraScratchesTn = new AlignedTetraScratchArray(btSoftBody_getTetraScratchesTn(Native));
				}
				return _tetraScratchesTn;
			}
		}

		public double Timeacc
		{
			get => btSoftBody_getTimeacc(Native);
			set => btSoftBody_setTimeacc(Native, value);
		}

		public double TotalMass
		{
			get => btSoftBody_getTotalMass(Native);
			set => SetTotalMass(value);
		}

		public bool UpdateRuntimeConstants
		{
			get => btSoftBody_getBUpdateRtCst(Native);
			set => btSoftBody_setBUpdateRtCst(Native, value);
		}
		/*
		public AlignedObjectArray UserIndexMapping
		{
			get { return btSoftBody_getUserIndexMapping(_native); }
			set { btSoftBody_setUserIndexMapping(_native, value._native); }
		}
		*/

		public bool UseSelfCollision
		{
			get => btSoftBody_useSelfCollision(Native);
			set => btSoftBody_setSelfCollision(Native, value);
		}

		public Vector3 WindVelocity
		{
			get
			{
				Vector3 value;
				btSoftBody_getWindVelocity(Native, out value);
				return value;
			}
			set => btSoftBody_setWindVelocity(Native, ref value);
		}

		public double Volume => btSoftBody_getVolume(Native);

		public SoftBodyWorldInfo WorldInfo
		{
			get => _worldInfo; set
			{
				btSoftBody_setWorldInfo(Native, value.Native);
				_worldInfo = value;
			}
		}
	}
}
