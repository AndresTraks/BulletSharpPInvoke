using System;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public static class TransformUtil
	{
		public static void CalculateDiffAxisAngle(ref Matrix transform0, ref Matrix transform1,
			out Vector3 axis, out double angle)
		{
			btTransformUtil_calculateDiffAxisAngle(ref transform0, ref transform1,
				out axis, out angle);
		}

		public static void CalculateDiffAxisAngleQuaternion(ref Quaternion orn0, ref Quaternion orn1a,
			out Vector3 axis, out double angle)
		{
			btTransformUtil_calculateDiffAxisAngleQuaternion(ref orn0, ref orn1a,
				out axis, out angle);
		}

		public static void CalculateVelocity(ref Matrix transform0, ref Matrix transform1,
			double timeStep, out Vector3 linVel, out Vector3 angVel)
		{
			btTransformUtil_calculateVelocity(ref transform0, ref transform1, timeStep,
				out linVel, out angVel);
		}

		public static void CalculateVelocityQuaternion(ref Vector3 pos0, ref Vector3 pos1,
			ref Quaternion orn0, ref Quaternion orn1, double timeStep, out Vector3 linVel, out Vector3 angVel)
		{
			btTransformUtil_calculateVelocityQuaternion(ref pos0, ref pos1, ref orn0,
				ref orn1, timeStep, out linVel, out angVel);
		}

		public static void IntegrateTransform(ref Matrix curTrans, ref Vector3 linvel, ref Vector3 angvel,
			double timeStep, out Matrix predictedTransform)
		{
			btTransformUtil_integrateTransform(ref curTrans, ref linvel, ref angvel,
				timeStep, out predictedTransform);
		}
	}

	public class ConvexSeparatingDistanceUtil : BulletDisposableObject
	{
		public ConvexSeparatingDistanceUtil(double boundingRadiusA, double boundingRadiusB)
		{
			IntPtr native = btConvexSeparatingDistanceUtil_new(boundingRadiusA, boundingRadiusB);
			InitializeUserOwned(native);
		}

		public void InitSeparatingDistance(ref Vector3 separatingVector, double separatingDistance,
			ref Matrix transA, ref Matrix transB)
		{
			btConvexSeparatingDistanceUtil_initSeparatingDistance(Native, ref separatingVector,
				separatingDistance, ref transA, ref transB);
		}

		public void UpdateSeparatingDistance(ref Matrix transA, ref Matrix transB)
		{
			btConvexSeparatingDistanceUtil_updateSeparatingDistance(Native, ref transA,
				ref transB);
		}

		public double ConservativeSeparatingDistance => btConvexSeparatingDistanceUtil_getConservativeSeparatingDistance(Native);

		protected override void Dispose(bool disposing)
		{
			btConvexSeparatingDistanceUtil_delete(Native);
		}
	}
}
