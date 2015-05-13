using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharp
{
	public class SequentialImpulseConstraintSolver : ConstraintSolver
	{
		internal SequentialImpulseConstraintSolver(IntPtr native, bool preventDelete)
            : base(native, preventDelete)
		{
		}

		public SequentialImpulseConstraintSolver()
			: base(btSequentialImpulseConstraintSolver_new(), false)
		{
		}

		public ulong BtRand2()
		{
			return btSequentialImpulseConstraintSolver_btRand2(_native);
		}

		public int BtRandInt2(int n)
		{
			return btSequentialImpulseConstraintSolver_btRandInt2(_native, n);
		}

		public ulong RandSeed
		{
			get { return btSequentialImpulseConstraintSolver_getRandSeed(_native); }
			set { btSequentialImpulseConstraintSolver_setRandSeed(_native, value); }
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr btSequentialImpulseConstraintSolver_new();
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern ulong btSequentialImpulseConstraintSolver_btRand2(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern int btSequentialImpulseConstraintSolver_btRandInt2(IntPtr obj, int n);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern ulong btSequentialImpulseConstraintSolver_getRandSeed(IntPtr obj);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern void btSequentialImpulseConstraintSolver_setRandSeed(IntPtr obj, ulong seed);
	}
}
