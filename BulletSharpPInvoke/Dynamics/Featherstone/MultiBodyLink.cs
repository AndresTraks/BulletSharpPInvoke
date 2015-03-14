using System;
using System.Runtime.InteropServices;
using System.Security;
using BulletSharp.Math;

namespace BulletSharp
{
	public class MultiBodyLink
	{
		internal IntPtr _native;

		internal MultiBodyLink(IntPtr native)
		{
			_native = native;
		}

		public void UpdateCache()
		{
			btMultibodyLink_updateCache(_native);
		}

		public void UpdateCacheMultiDof()
		{
			btMultibodyLink_updateCacheMultiDof(_native);
		}
/*
		public void UpdateCacheMultiDof(float pq)
		{
			btMultibodyLink_updateCacheMultiDof2(_native, pq._native);
		}
*/
		public Vector3 AppliedForce
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAppliedForce(_native, out value);
				return value;
			}
			set { btMultibodyLink_setAppliedForce(_native, ref value); }
		}

		public Vector3 AppliedTorque
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getAppliedTorque(_native, out value);
				return value;
			}
			set { btMultibodyLink_setAppliedTorque(_native, ref value); }
		}

		public Quaternion CachedRotParentToThis
		{
			get
			{
				Quaternion value;
				btMultibodyLink_getCachedRotParentToThis(_native, out value);
				return value;
			}
			set { btMultibodyLink_setCachedRotParentToThis(_native, ref value); }
		}

		public Vector3 CachedRVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getCachedRVector(_native, out value);
				return value;
			}
			set { btMultibodyLink_setCachedRVector(_native, ref value); }
		}

		public int CfgOffset
		{
			get { return btMultibodyLink_getCfgOffset(_native); }
			set { btMultibodyLink_setCfgOffset(_native, value); }
		}

		public MultiBodyLinkCollider Collider
		{
            get { return CollisionObject.GetManaged(btMultibodyLink_getCollider(_native)) as MultiBodyLinkCollider; }
			set { btMultibodyLink_setCollider(_native, value._native); }
		}

		public int DofCount
		{
			get { return btMultibodyLink_getDofCount(_native); }
			set { btMultibodyLink_setDofCount(_native, value); }
		}

		public int DofOffset
		{
			get { return btMultibodyLink_getDofOffset(_native); }
			set { btMultibodyLink_setDofOffset(_native, value); }
		}

		public Vector3 DVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getDVector(_native, out value);
				return value;
			}
			set { btMultibodyLink_setDVector(_native, ref value); }
		}

		public Vector3 EVector
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getEVector(_native, out value);
				return value;
			}
			set { btMultibodyLink_setEVector(_native, ref value); }
		}

		public int Flags
		{
			get { return btMultibodyLink_getFlags(_native); }
			set { btMultibodyLink_setFlags(_native, value); }
		}

		public Vector3 InertiaLocal
		{
			get
			{
				Vector3 value;
				btMultibodyLink_getInertiaLocal(_native, out value);
				return value;
			}
			set { btMultibodyLink_setInertiaLocal(_native, ref value); }
		}

		public float JointPos
		{
			get { return btMultibodyLink_getJointPos(_native); }
		}

		public float JointTorque
		{
			get { return btMultibodyLink_getJointTorque(_native); }
		}
/*
		public eFeatherstoneJointType JointType
		{
			get { return btMultibodyLink_getJointType(_native); }
			set { btMultibodyLink_setJointType(_native, value); }
		}
*/
		public float Mass
		{
			get { return btMultibodyLink_getMass(_native); }
			set { btMultibodyLink_setMass(_native, value); }
		}

		public int Parent
		{
			get { return btMultibodyLink_getParent(_native); }
			set { btMultibodyLink_setParent(_native, value); }
		}

		public int PosVarCount
		{
			get { return btMultibodyLink_getPosVarCount(_native); }
			set { btMultibodyLink_setPosVarCount(_native, value); }
		}

		public Quaternion ZeroRotParentToThis
		{
			get
			{
				Quaternion value;
				btMultibodyLink_getZeroRotParentToThis(_native, out value);
				return value;
			}
			set { btMultibodyLink_setZeroRotParentToThis(_native, ref value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultibodyLink_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getAbsFrameLocVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getAbsFrameTotVelocity(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getAppliedForce(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getAppliedTorque(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultibodyLink_getAxes(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getAxisBottom(IntPtr obj, int dof, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getAxisTop(IntPtr obj, int dof, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getCachedRotParentToThis(IntPtr obj, [Out] out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getCachedRVector(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getCfgOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btMultibodyLink_getCollider(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getDofCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getDofOffset(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getDVector(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getEVector(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getFlags(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getInertiaLocal(IntPtr obj, [Out] out Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultibodyLink_getJointPos(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultibodyLink_getJointTorque(IntPtr obj);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern eFeatherstoneJointType btMultibodyLink_getJointType(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern float btMultibodyLink_getMass(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getParent(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btMultibodyLink_getPosVarCount(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_getZeroRotParentToThis(IntPtr obj, [Out] out Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAppliedForce(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAppliedTorque(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAxisBottom(IntPtr obj, int dof, IntPtr x, IntPtr y, IntPtr z);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAxisBottom2(IntPtr obj, int dof, [In] ref Vector3 axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAxisTop(IntPtr obj, int dof, [In] ref Vector3 axis);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setAxisTop2(IntPtr obj, int dof, IntPtr x, IntPtr y, IntPtr z);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setCachedRotParentToThis(IntPtr obj, [In] ref Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setCachedRVector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setCfgOffset(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setCollider(IntPtr obj, IntPtr value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setDofCount(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setDofOffset(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setDVector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setEVector(IntPtr obj, [In] ref Vector3 value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setFlags(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setInertiaLocal(IntPtr obj, [In] ref Vector3 value);
		//[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		//static extern void btMultibodyLink_setJointType(IntPtr obj, eFeatherstoneJointType value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setMass(IntPtr obj, float value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setParent(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setPosVarCount(IntPtr obj, int value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_setZeroRotParentToThis(IntPtr obj, [In] ref Quaternion value);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_updateCache(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_updateCacheMultiDof(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_updateCacheMultiDof2(IntPtr obj, IntPtr pq);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btMultibodyLink_delete(IntPtr obj);
	}
}
