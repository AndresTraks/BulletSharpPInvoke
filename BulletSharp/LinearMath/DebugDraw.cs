﻿/*
 * C# / XNA  port of Bullet (c) 2011 Mark Neale <xexuxjy@hotmail.com>
 *
 * Bullet Continuous Collision Detection and Physics Library
 * Copyright (c) 2003-2008 Erwin Coumans  http://www.bulletphysics.com/
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose, 
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *	claim that you wrote the original software. If you use this software
 *	in a product, an acknowledgment in the product documentation would be
 *	appreciated but is not required.
 * 2. Altered source versions must be plainly marked as such, and must not be
 *	misrepresented as being the original software.
 * 3. This notice may not be removed or altered from any source distribution.
 */

using System;
using System.Security;
using System.Runtime.InteropServices;
using BulletSharp.Math;
using static BulletSharp.UnsafeNativeMethods;

namespace BulletSharp
{
	public abstract class DebugDraw : BulletDisposableObject
	{
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawAabbUnmanagedDelegate([In] ref Vector3 from, [In] ref Vector3 to, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawArcUnmanagedDelegate([In] ref Vector3 center, [In] ref Vector3 normal, [In] ref Vector3 axis, double radiusA, double radiusB,
			double minAngle, double maxAngle, ref Vector3 color, bool drawSect, double stepDegrees);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawBoxUnmanagedDelegate([In] ref Vector3 bbMin, [In] ref Vector3 bbMax, [In] ref Matrix trans, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawCapsuleUnmanagedDelegate(double radius, double halfHeight, int upAxis, [In] ref Matrix transform, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawConeUnmanagedDelegate(double radius, double height, int upAxis, [In] ref Matrix transform, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawContactPointUnmanagedDelegate([In] ref Vector3 pointOnB, [In] ref Vector3 normalOnB, double distance, int lifeTime, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawCylinderUnmanagedDelegate(double radius, double halfHeight, int upAxis, [In] ref Matrix transform, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawLineUnmanagedDelegate([In] ref Vector3 from, [In] ref Vector3 to, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawPlaneUnmanagedDelegate([In] ref Vector3 planeNormal, double planeConst, [In] ref Matrix transform, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawSphereUnmanagedDelegate(double radius, [In] ref Matrix transform, [In] ref Vector3 color);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawSpherePatchUnmanagedDelegate([In] ref Vector3 center, [In] ref Vector3 up, [In] ref Vector3 axis, double radius,
			double minTh, double maxTh, double minPs, double maxPs, [In] ref Vector3 color, double stepDegrees);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawTransformUnmanagedDelegate([In] ref Matrix transform, double orthoLen);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void DrawTriangleUnmanagedDelegate([In] ref Vector3 v0, [In] ref Vector3 v1, [In] ref Vector3 v2, [In] ref Vector3 color, double alpha);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate void SimpleCallback(int x);
		[UnmanagedFunctionPointer(BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		delegate DebugDrawModes GetDebugModeUnmanagedDelegate();

		private readonly DrawAabbUnmanagedDelegate _drawAabb;
		private readonly DrawArcUnmanagedDelegate _drawArc;
		private readonly DrawBoxUnmanagedDelegate _drawBox;
		private readonly DrawCapsuleUnmanagedDelegate _drawCapsule;
		private readonly DrawConeUnmanagedDelegate _drawCone;
		private readonly DrawContactPointUnmanagedDelegate _drawContactPoint;
		private readonly DrawCylinderUnmanagedDelegate _drawCylinder;
		private readonly DrawLineUnmanagedDelegate _drawLine;
		private readonly DrawPlaneUnmanagedDelegate _drawPlane;
		private readonly DrawSphereUnmanagedDelegate _drawSphere;
		private readonly DrawSpherePatchUnmanagedDelegate _drawSpherePatch;
		private readonly DrawTransformUnmanagedDelegate _drawTransform;
		private readonly DrawTriangleUnmanagedDelegate _drawTriangle;
		private readonly GetDebugModeUnmanagedDelegate _getDebugMode;
		private readonly SimpleCallback _cb;

		internal static DebugDraw GetManaged(IntPtr debugDrawer)
		{
			if (debugDrawer == IntPtr.Zero)
			{
				return null;
			}

			IntPtr handle = btIDebugDrawWrapper_getGCHandle(debugDrawer);
			return GCHandle.FromIntPtr(handle).Target as DebugDraw;
		}
		
		private void SimpleCallbackUnmanaged(int x)
		{
			throw new NotImplementedException();
		}

		private DebugDrawModes GetDebugModeUnmanaged()
		{
			return DebugMode;
		}

		public DebugDraw()
		{
			_drawAabb = new DrawAabbUnmanagedDelegate(DrawAabb);
			_drawArc = new DrawArcUnmanagedDelegate(DrawArc);
			_drawBox = new DrawBoxUnmanagedDelegate(DrawBox);
			_drawCapsule = new DrawCapsuleUnmanagedDelegate(DrawCapsule);
			_drawCone = new DrawConeUnmanagedDelegate(DrawCone);
			_drawContactPoint = new DrawContactPointUnmanagedDelegate(DrawContactPoint);
			_drawCylinder = new DrawCylinderUnmanagedDelegate(DrawCylinder);
			_drawLine = new DrawLineUnmanagedDelegate(DrawLine);
			_drawPlane = new DrawPlaneUnmanagedDelegate(DrawPlane);
			_drawSphere = new DrawSphereUnmanagedDelegate(DrawSphere);
			_drawSpherePatch = new DrawSpherePatchUnmanagedDelegate(DrawSpherePatch);
			_drawTransform = new DrawTransformUnmanagedDelegate(DrawTransform);
			_drawTriangle = new DrawTriangleUnmanagedDelegate(DrawTriangle);
			_getDebugMode = new GetDebugModeUnmanagedDelegate(GetDebugModeUnmanaged);
			_cb = new SimpleCallback(SimpleCallbackUnmanaged);

			IntPtr native = btIDebugDrawWrapper_new(
				GCHandle.ToIntPtr(GCHandle.Alloc(this)),
				Marshal.GetFunctionPointerForDelegate(_drawAabb),
				Marshal.GetFunctionPointerForDelegate(_drawArc),
				Marshal.GetFunctionPointerForDelegate(_drawBox),
				Marshal.GetFunctionPointerForDelegate(_drawCapsule),
				Marshal.GetFunctionPointerForDelegate(_drawCone),
				Marshal.GetFunctionPointerForDelegate(_drawContactPoint),
				Marshal.GetFunctionPointerForDelegate(_drawCylinder),
				Marshal.GetFunctionPointerForDelegate(_drawLine),
				Marshal.GetFunctionPointerForDelegate(_drawPlane),
				Marshal.GetFunctionPointerForDelegate(_drawSphere),
				Marshal.GetFunctionPointerForDelegate(_drawSpherePatch),
				Marshal.GetFunctionPointerForDelegate(_drawTransform),
				Marshal.GetFunctionPointerForDelegate(_drawTriangle),
				Marshal.GetFunctionPointerForDelegate(_getDebugMode),
				Marshal.GetFunctionPointerForDelegate(_cb));
			InitializeUserOwned(native);
		}

		public abstract void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 color);
		public abstract void Draw3DText(ref Vector3 location, String textString);
		public abstract void ReportErrorWarning(String warningString);
		public abstract DebugDrawModes DebugMode { get; set; }

		public void DrawLine(Vector3 from, Vector3 to, Vector3 color)
		{
			DrawLine(ref from, ref to, ref color);
		}

		public virtual void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor, ref Vector3 toColor)
		{
			DrawLine(ref from, ref to, ref fromColor);
		}

		public virtual void DrawAabb(ref Vector3 from, ref Vector3 to, ref Vector3 color)
		{
			Vector3 a = from;
			a.X = to.X;
			DrawLine(ref from, ref a, ref color);

			Vector3 b = to;
			b.Y = from.Y;
			DrawLine(ref b, ref to, ref color);
			DrawLine(ref a, ref b, ref color);

			Vector3 c = from;
			c.Z = to.Z;
			DrawLine(ref from, ref c, ref color);
			DrawLine(ref b, ref c, ref color);

			b.Y = to.Y;
			b.Z = from.Z;
			DrawLine(ref b, ref to, ref color);
			DrawLine(ref a, ref b, ref color);

			a.Y = to.Y;
			a.X = from.X;
			DrawLine(ref from, ref a, ref color);
			DrawLine(ref a, ref b, ref color);

			b.X = from.X;
			b.Z = to.Z;
			DrawLine(ref c, ref b, ref color);
			DrawLine(ref a, ref b, ref color);
			DrawLine(ref b, ref to, ref color);
		}

		public virtual void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, double radiusA, double radiusB,
			double minAngle, double maxAngle, ref Vector3 color, bool drawSect, double stepDegrees = 10.0f)
		{
			Vector3 xAxis = radiusA * axis;
			Vector3 yAxis = radiusB * Vector3.Cross(normal, axis);
			double step = stepDegrees * MathUtil.SIMD_RADS_PER_DEG;
			int nSteps = (int)((maxAngle - minAngle) / step);
			if (nSteps == 0)
			{
				nSteps = 1;
			}
			Vector3 prev = center + xAxis * (float)System.Math.Cos(minAngle) + yAxis * (float)System.Math.Sin(minAngle);
			if (drawSect)
			{
				DrawLine(ref center, ref prev, ref color);
			}
			for (int i = 1; i <= nSteps; i++)
			{
				double angle = minAngle + (maxAngle - minAngle) * i / nSteps;
				Vector3 next = center + xAxis * (float)System.Math.Cos(angle) + yAxis * (float)System.Math.Sin(angle);
				DrawLine(ref prev, ref next, ref color);
				prev = next;
			}
			if (drawSect)
			{
				DrawLine(ref center, ref prev, ref color);
			}
		}

		public virtual void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Vector3 color)
		{
			//Vector3 p1 = bbMin;
			Vector3 p2 = new Vector3(bbMax.X, bbMin.Y, bbMin.Z);
			Vector3 p3 = new Vector3(bbMax.X, bbMax.Y, bbMin.Z);
			Vector3 p4 = new Vector3(bbMin.X, bbMax.Y, bbMin.Z);
			Vector3 p5 = new Vector3(bbMin.X, bbMin.Y, bbMax.Z);
			Vector3 p6 = new Vector3(bbMax.X, bbMin.Y, bbMax.Z);
			//Vector3 p7 = bbMax;
			Vector3 p8 = new Vector3(bbMin.X, bbMax.Y, bbMax.Z);

			DrawLine(ref bbMin, ref p2, ref color);
			DrawLine(ref p2, ref p3, ref color);
			DrawLine(ref p3, ref p4, ref color);
			DrawLine(ref p4, ref bbMin, ref color);

			DrawLine(ref bbMin, ref p5, ref color);
			DrawLine(ref p2, ref p6, ref color);
			DrawLine(ref p3, ref bbMax, ref color);
			DrawLine(ref p4, ref p8, ref color);

			DrawLine(ref p5, ref p6, ref color);
			DrawLine(ref p6, ref bbMax, ref color);
			DrawLine(ref bbMax, ref p8, ref color);
			DrawLine(ref p8, ref p5, ref color);
		}

		public virtual void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color)
		{
			Vector3 p1, p2, p3, p4, p5, p6, p7, p8;
			Vector3 point = bbMin;
			Vector3.TransformCoordinate(ref point, ref trans, out p1);
			point.X = bbMax.X;
			Vector3.TransformCoordinate(ref point, ref trans, out p2);
			point.Y = bbMax.Y;
			Vector3.TransformCoordinate(ref point, ref trans, out p3);
			point.X = bbMin.X;
			Vector3.TransformCoordinate(ref point, ref trans, out p4);
			point.Z = bbMax.Z;
			Vector3.TransformCoordinate(ref point, ref trans, out p8);
			point.X = bbMax.X;
			Vector3.TransformCoordinate(ref point, ref trans, out p7);
			point.Y = bbMin.Y;
			Vector3.TransformCoordinate(ref point, ref trans, out p6);
			point.X = bbMin.X;
			Vector3.TransformCoordinate(ref point, ref trans, out p5);

			DrawLine(ref p1, ref p2, ref color);
			DrawLine(ref p2, ref p3, ref color);
			DrawLine(ref p3, ref p4, ref color);
			DrawLine(ref p4, ref p1, ref color);

			DrawLine(ref p1, ref p5, ref color);
			DrawLine(ref p2, ref p6, ref color);
			DrawLine(ref p3, ref p7, ref color);
			DrawLine(ref p4, ref p8, ref color);

			DrawLine(ref p5, ref p6, ref color);
			DrawLine(ref p6, ref p7, ref color);
			DrawLine(ref p7, ref p8, ref color);
			DrawLine(ref p8, ref p5, ref color);
		}

		public virtual void DrawCapsule(double radius, double halfHeight, int upAxis, ref Matrix transform, ref Vector3 color)
		{
			const int stepDegrees = 30;

			void DrawCapsuleCap(ref Vector3 capOffset, ref Matrix capTransform, ref Vector3 capColor, float axisDirection)
			{
				Matrix childTransform = capTransform;
				childTransform.Origin = Vector3.TransformCoordinate(capOffset, capTransform);
				Matrix childBasis = childTransform.Basis;
				Vector3 center = childTransform.Origin;
				Vector3 up = childBasis.GetColumn((upAxis + 1) % 3);
				Vector3 axis = axisDirection * childBasis.GetColumn(upAxis);
				double minTh = -MathUtil.SIMD_HALF_PI;
				double maxTh = MathUtil.SIMD_HALF_PI;
				double minPs = -MathUtil.SIMD_HALF_PI;
				double maxPs = MathUtil.SIMD_HALF_PI;

				DrawSpherePatch(ref center, ref up, ref axis, radius, minTh, maxTh, minPs, maxPs, ref capColor, stepDegrees);
			}

			Vector3 capStart = Vector3.Zero;
			capStart[upAxis] = -halfHeight;

			Vector3 capEnd = Vector3.Zero;
			capEnd[upAxis] = halfHeight;

			DrawCapsuleCap(ref capStart, ref transform, ref color, -1);
			DrawCapsuleCap(ref capEnd, ref transform, ref color, 1);

			// Draw some additional lines
			Vector3 start = transform.Origin;
			Matrix basis = transform.Basis;
			for (int i = 0; i < 360; i += stepDegrees)
			{
				capEnd[(upAxis + 1) % 3] = capStart[(upAxis + 1) % 3] = (float) System.Math.Sin(i * MathUtil.SIMD_RADS_PER_DEG) * radius;
				capEnd[(upAxis + 2) % 3] = capStart[(upAxis + 2) % 3] = (float) System.Math.Cos(i * MathUtil.SIMD_RADS_PER_DEG) * radius;
				DrawLine(start + Vector3.TransformCoordinate(capStart, basis), start + Vector3.TransformCoordinate(capEnd, basis), color);
			}
		}

		public virtual void DrawCone(double radius, double height, int upAxis, ref Matrix transform, ref Vector3 color)
		{
			Vector3 start = transform.Origin;

			Vector3 offsetHeight = Vector3.Zero;
			offsetHeight[upAxis] = height * 0.5f;
			Vector3 offsetRadius = Vector3.Zero;
			offsetRadius[(upAxis + 1) % 3] = radius;

			Vector3 offset2Radius = Vector3.Zero;
			offsetRadius[(upAxis + 2) % 3] = radius;

			Matrix basis = transform.Basis;
			Vector3 from = start + Vector3.TransformCoordinate(offsetHeight, basis);
			Vector3 to = start + Vector3.TransformCoordinate(-offsetHeight, basis);
			DrawLine(from, to + offsetRadius, color);
			DrawLine(from, to - offsetRadius, color);
			DrawLine(from, to + offset2Radius, color);
			DrawLine(from, to - offset2Radius, color);
		}

		public virtual void DrawContactPoint(ref Vector3 pointOnB, ref Vector3 normalOnB, double distance, int lifeTime, ref Vector3 color)
		{
			Vector3 to = pointOnB + normalOnB * 1; // distance
			DrawLine(ref pointOnB, ref to, ref color);
		}

		public virtual void DrawCylinder(double radius, double halfHeight, int upAxis, ref Matrix transform, ref Vector3 color)
		{
			Vector3 start = transform.Origin;
			Matrix basis = transform.Basis;
			Vector3 offsetHeight = Vector3.Zero;
			offsetHeight[upAxis] = halfHeight;
			Vector3 offsetRadius = Vector3.Zero;
			offsetRadius[(upAxis + 1) % 3] = radius;
			DrawLine(start + Vector3.TransformCoordinate(offsetHeight + offsetRadius, basis), start + Vector3.TransformCoordinate(-offsetHeight + offsetRadius, basis), color);
			DrawLine(start + Vector3.TransformCoordinate(offsetHeight - offsetRadius, basis), start + Vector3.TransformCoordinate(-offsetHeight - offsetRadius, basis), color);
		}

		public virtual void DrawPlane(ref Vector3 planeNormal, double planeConst, ref Matrix transform, ref Vector3 color)
		{
			Vector3 planeOrigin = planeNormal * planeConst;
			Vector3 vec0, vec1;
			PlaneSpace1(ref planeNormal, out vec0, out vec1);
			const double vecLen = 100f;
			Vector3 pt0 = planeOrigin + vec0 * vecLen;
			Vector3 pt1 = planeOrigin - vec0 * vecLen;
			Vector3 pt2 = planeOrigin + vec1 * vecLen;
			Vector3 pt3 = planeOrigin - vec1 * vecLen;
			Vector3.TransformCoordinate(ref pt0, ref transform, out pt0);
			Vector3.TransformCoordinate(ref pt1, ref transform, out pt1);
			Vector3.TransformCoordinate(ref pt2, ref transform, out pt2);
			Vector3.TransformCoordinate(ref pt3, ref transform, out pt3);
			DrawLine(ref pt0, ref pt1, ref color);
			DrawLine(ref pt2, ref pt3, ref color);
		}

		public virtual void DrawSphere(double radius, ref Matrix transform, ref Vector3 color)
		{
			Vector3 start = transform.Origin;
			Matrix basis = transform.Basis;

			Vector3 xoffs = Vector3.TransformCoordinate(new Vector3(radius, 0, 0), basis);
			Vector3 yoffs = Vector3.TransformCoordinate(new Vector3(0, radius, 0), basis);
			Vector3 zoffs = Vector3.TransformCoordinate(new Vector3(0, 0, radius), basis);

			Vector3 xn = start - xoffs;
			Vector3 xp = start + xoffs;
			Vector3 yn = start - yoffs;
			Vector3 yp = start + yoffs;
			Vector3 zn = start - zoffs;
			Vector3 zp = start + zoffs;

			// XY
			DrawLine(ref xn, ref yp, ref color);
			DrawLine(ref yp, ref xp, ref color);
			DrawLine(ref xp, ref yn, ref color);
			DrawLine(ref yn, ref xn, ref color);

			// XZ
			DrawLine(ref xn, ref zp, ref color);
			DrawLine(ref zp, ref xp, ref color);
			DrawLine(ref xp, ref zn, ref color);
			DrawLine(ref zn, ref xn, ref color);

			// YZ
			DrawLine(ref yn, ref zp, ref color);
			DrawLine(ref zp, ref yp, ref color);
			DrawLine(ref yp, ref zn, ref color);
			DrawLine(ref zn, ref yn, ref color);
		}

		public virtual void DrawSphere(ref Vector3 p, double radius, ref Vector3 color)
		{
			Matrix tr = Matrix.Translation(p);
			DrawSphere(radius, ref tr, ref color);
		}

		public virtual void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, double radius,
			double minTh, double maxTh, double minPs, double maxPs, ref Vector3 color)
		{
			DrawSpherePatch(ref center, ref up, ref axis, radius, minTh, maxTh, minPs, maxPs, ref color, 10.0f);
		}

		public virtual void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, double radius,
			double minTh, double maxTh, double minPs, double maxPs, ref Vector3 color, double stepDegrees)
		{
			Vector3[] vA;
			Vector3[] vB;
			Vector3[] pvA, pvB, pT;
			Vector3 npole = center + up * radius;
			Vector3 spole = center - up * radius;
			Vector3 arcStart = Vector3.Zero;
			double step = stepDegrees * MathUtil.SIMD_RADS_PER_DEG;
			Vector3 kv = up;
			Vector3 iv = axis;

			Vector3 jv = Vector3.Cross(kv, iv);
			bool drawN = false;
			bool drawS = false;
			if (minTh <= -MathUtil.SIMD_HALF_PI)
			{
				minTh = -MathUtil.SIMD_HALF_PI + step;
				drawN = true;
			}
			if (maxTh >= MathUtil.SIMD_HALF_PI)
			{
				maxTh = MathUtil.SIMD_HALF_PI - step;
				drawS = true;
			}
			if (minTh > maxTh)
			{
				minTh = -MathUtil.SIMD_HALF_PI + step;
				maxTh = MathUtil.SIMD_HALF_PI - step;
				drawN = drawS = true;
			}
			int n_hor = (int)((maxTh - minTh) / step) + 1;
			if (n_hor < 2) n_hor = 2;
			double step_h = (maxTh - minTh) / (n_hor - 1);
			bool isClosed;
			if (minPs > maxPs)
			{
				minPs = -MathUtil.SIMD_PI + step;
				maxPs = MathUtil.SIMD_PI;
				isClosed = true;
			}
			else if ((maxPs - minPs) >= MathUtil.SIMD_PI * 2f)
			{
				isClosed = true;
			}
			else
			{
				isClosed = false;
			}
			int n_vert = (int)((maxPs - minPs) / step) + 1;
			if (n_vert < 2) n_vert = 2;

			vA = new Vector3[n_vert];
			vB = new Vector3[n_vert];
			pvA = vA; pvB = vB;

			double step_v = (maxPs - minPs) / (double)(n_vert - 1);
			for (int i = 0; i < n_hor; i++)
			{
				double th = minTh + i * step_h;
				double sth = radius * (double)System.Math.Sin(th);
				double cth = radius * (double)System.Math.Cos(th);
				for (int j = 0; j < n_vert; j++)
				{
					double psi = minPs + (double)j * step_v;
					double sps = (double)System.Math.Sin(psi);
					double cps = (double)System.Math.Cos(psi);
					pvB[j] = center + cth * cps * iv + cth * sps * jv + sth * kv;
					if (i != 0)
					{
						DrawLine(ref pvA[j], ref pvB[j], ref color);
					}
					else if (drawS)
					{
						DrawLine(ref spole, ref pvB[j], ref color);
					}
					if (j != 0)
					{
						DrawLine(ref pvB[j - 1], ref pvB[j], ref color);
					}
					else
					{
						arcStart = pvB[j];
					}
					if ((i == (n_hor - 1)) && drawN)
					{
						DrawLine(ref npole, ref pvB[j], ref color);
					}
					if (isClosed)
					{
						if (j == (n_vert - 1))
						{
							DrawLine(ref arcStart, ref pvB[j], ref color);
						}
					}
					else
					{
						if (((i == 0) || (i == (n_hor - 1))) && ((j == 0) || (j == (n_vert - 1))))
						{
							DrawLine(ref center, ref pvB[j], ref color);
						}
					}
				}
				pT = pvA; pvA = pvB; pvB = pT;
			}
		}

		public virtual void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 n0, ref Vector3 n1, ref Vector3 n2, ref Vector3 color, double alpha)
		{
			DrawTriangle(ref v0, ref v1, ref v2, ref color, alpha);
		}

		public virtual void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 color, double alpha)
		{
			DrawLine(ref v0, ref v1, ref color);
			DrawLine(ref v1, ref v2, ref color);
			DrawLine(ref v2, ref v0, ref color);
		}

		public virtual void DrawTransform(ref Matrix transform, double orthoLen)
		{
			Vector3 start = transform.Origin;
			Matrix basis = transform.Basis;

			Vector3 ortho = new Vector3(orthoLen, 0, 0);
			Vector3 colour = new Vector3(0.7f, 0, 0);
			Vector3 temp;
			Vector3.TransformCoordinate(ref ortho, ref basis, out temp);
			temp += start;
			DrawLine(ref start, ref temp, ref colour);

			ortho.X = 0;
			ortho.Y = orthoLen;
			colour.X = 0;
			colour.Y = 0.7f;
			Vector3.TransformCoordinate(ref ortho, ref basis, out temp);
			temp += start;
			DrawLine(ref start, ref temp, ref colour);

			ortho.Y = 0;
			ortho.Z = orthoLen;
			colour.Y = 0;
			colour.Z = 0.7f;
			Vector3.TransformCoordinate(ref ortho, ref basis, out temp);
			temp += start;
			DrawLine(ref start, ref temp, ref colour);
		}

		public static void PlaneSpace1(ref Vector3 n, out Vector3 p, out Vector3 q)
		{
			if (System.Math.Abs(n.Z) > MathUtil.SIMDSQRT12)
			{
				// choose p in y-z plane
				double a = n.Y * n.Y + n.Z * n.Z;
				double k = MathUtil.RecipSqrt(a);
				p = new Vector3(0, -n.Z * k, n.Y * k);
				// set q = n x p
				q = new Vector3(a * k, -n.X * p.Z, n.X * p.Y);
			}
			else
			{
				// choose p in x-y plane
				double a = n.X * n.X + n.Y * n.Y;
				double k = MathUtil.RecipSqrt(a);
				p = new Vector3(-n.Y * k, n.X * k, 0);
				// set q = n x p
				q = new Vector3(-n.Z * p.Y, n.Z * p.X, a * k);
			}
		}

		protected override void Dispose(bool disposing)
		{
			btIDebugDraw_delete(Native);
		}
	}
}
