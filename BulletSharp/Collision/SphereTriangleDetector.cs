using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Numerics;

namespace BulletSharp
{
    /*
	public class SphereTriangleDetector : DiscreteCollisionDetectorInterface
	{
		internal SphereTriangleDetector(IntPtr native)
			: base(native)
		{
		}

		public SphereTriangleDetector(SphereShape sphere, TriangleShape triangle,
			float contactBreakingThreshold)
			: base(SphereTriangleDetector_new(sphere._native, triangle._native, contactBreakingThreshold))
		{
		}

		public bool Collide(Vector3 sphereCenter, out Vector3 point, out Vector3 resultNormal,
			out float depth, out float timeOfImpact, float contactBreakingThreshold)
		{
			return SphereTriangleDetector_collide(_native, ref sphereCenter, out point,
				out resultNormal, out depth, out timeOfImpact, contactBreakingThreshold);
		}

		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		static extern IntPtr SphereTriangleDetector_new(IntPtr sphere, IntPtr triangle, float contactBreakingThreshold);
		[DllImport(Native.Dll, CallingConvention = Native.Conv), SuppressUnmanagedCodeSecurity]
		[return: MarshalAs(UnmanagedType.I1)]
		static extern bool SphereTriangleDetector_collide(IntPtr obj, [In] ref Vector3 sphereCenter, out Vector3 point, out Vector3 resultNormal, out float depth, out float timeOfImpact, float contactBreakingThreshold);
	}
    */
}
