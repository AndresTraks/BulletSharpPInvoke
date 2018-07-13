using BulletSharp;
using BulletSharp.Math;

namespace BulletSharpTest
{
    class DebugDrawTest : IDebugDraw
    {
        DebugDrawModes _debugMode = DebugDrawModes.DrawWireframe | DebugDrawModes.DrawAabb;

        public DebugDrawModes DebugMode
        {
            get
            {
                return _debugMode;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void Draw3dText(ref Vector3 location, string textString)
        {
            throw new System.NotImplementedException();
        }

        public void DrawAabb(ref Vector3 from, ref Vector3 to, ref Vector3 color)
        {
            //throw new System.NotImplementedException();
        }

        public void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, double radiusA, double radiusB, double minAngle, double maxAngle, ref Vector3 color, bool drawSect, double stepDegrees)
        {
            throw new System.NotImplementedException();
        }

        public void DrawArc(ref Vector3 center, ref Vector3 normal, ref Vector3 axis, double radiusA, double radiusB, double minAngle, double maxAngle, ref Vector3 color, bool drawSect)
        {
            throw new System.NotImplementedException();
        }

        public void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color)
        {
            //throw new System.NotImplementedException();
        }

        public void DrawCapsule(double radius, double halfHeight, int upAxis, ref Matrix transform, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawCone(double radius, double height, int upAxis, ref Matrix transform, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawContactPoint(ref Vector3 PointOnB, ref Vector3 normalOnB, double distance, int lifeTime, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawCylinder(double radius, double halfHeight, int upAxis, ref Matrix transform, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 fromColor, ref Vector3 toColor)
        {
            throw new System.NotImplementedException();
        }

        public void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawPlane(ref Vector3 planeNormal, double planeConst, ref Matrix transform, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawSphere(ref Vector3 p, double radius, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawSphere(double radius, ref Matrix transform, ref Vector3 color)
        {
            //throw new System.NotImplementedException();
        }

        public void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, double radius, double minTh, double maxTh, double minPs, double maxPs, ref Vector3 color, double stepDegrees, bool drawCenter)
        {
            throw new System.NotImplementedException();
        }

        public void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, double radius, double minTh, double maxTh, double minPs, double maxPs, ref Vector3 color, double stepDegrees)
        {
            throw new System.NotImplementedException();
        }

        public void DrawSpherePatch(ref Vector3 center, ref Vector3 up, ref Vector3 axis, double radius, double minTh, double maxTh, double minPs, double maxPs, ref Vector3 color)
        {
            throw new System.NotImplementedException();
        }

        public void DrawTransform(ref Matrix transform, double orthoLen)
        {
            throw new System.NotImplementedException();
        }

        public void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 __unnamed3, ref Vector3 __unnamed4, ref Vector3 __unnamed5, ref Vector3 color, double alpha)
        {
            throw new System.NotImplementedException();
        }

        public void DrawTriangle(ref Vector3 v0, ref Vector3 v1, ref Vector3 v2, ref Vector3 color, double alpha)
        {
            throw new System.NotImplementedException();
        }

        public void FlushLines()
        {
            //throw new System.NotImplementedException();
        }

        public void ReportErrorWarning(string warningString)
        {
            throw new System.NotImplementedException();
        }
    }
}
