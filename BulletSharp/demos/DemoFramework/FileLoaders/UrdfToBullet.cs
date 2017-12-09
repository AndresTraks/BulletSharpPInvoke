using BulletSharp;
using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DemoFramework.FileLoaders
{
    public class UrdfToBullet
    {
        private static char[] _spaceSeparator = new[] { ' ' };

        public UrdfToBullet(DiscreteDynamicsWorld world)
        {
            World = world;
        }

        public DiscreteDynamicsWorld World { get; private set; }

        public void Convert(UrdfRobot robot, string baseDirectory)
        {
            foreach (UrdfLink link in robot.Links.Values)
            {
                LoadLink(link, baseDirectory);
            }
        }

        private void LoadLink(UrdfLink link, string baseDirectory)
        {
            double mass = 0;
            UrdfInertial inertial = link.Inertial;
            if (inertial != null)
            {
                mass = (double)inertial.Mass;
            }

            UrdfCollision collision = link.Collision;
            if (collision != null)
            {
                Matrix origin = ParsePose(collision.Origin);
                UrdfGeometry geometry = collision.Geometry;
                CollisionShape shape = CreateShapeFromGeometry(baseDirectory, mass, geometry);
                if (mass == 0)
                {
                }
                else
                {
                    PhysicsHelper.CreateBody(mass, origin, shape, World);
                }
            }
        }

        private Matrix ParsePose(UrdfPose pose)
        {
            if (pose == null)
            {
                return Matrix.Identity;
            }
            Vector3 rpy = ParseVector3(pose.RollPitchYaw);
            Matrix matrix = Matrix.RotationYawPitchRoll(rpy.Z, rpy.Y, rpy.X);
            matrix.Origin = ParseVector3(pose.Position);
            return matrix;
        }

        private static Vector3 ParseVector3(string vector)
        {
            if (vector == null)
            {
                return Vector3.Zero;
            }
            string[] components = vector.Split(_spaceSeparator, StringSplitOptions.RemoveEmptyEntries);
            return new Vector3(
                double.Parse(components[0], CultureInfo.InvariantCulture),
                double.Parse(components[1], CultureInfo.InvariantCulture),
                double.Parse(components[2], CultureInfo.InvariantCulture));
        }

        private CollisionShape CreateShapeFromGeometry(string baseDirectory, double mass, UrdfGeometry geometry)
        {
            CollisionShape shape;
            switch (geometry.Type)
            {
                case UrdfGeometryType.Box:
                    shape = CreateBoxShape((UrdfBox)geometry);
                    break;
                case UrdfGeometryType.Capsule:
                    shape = CreateCapsuleShape((UrdfCapsule)geometry);
                    break;
                case UrdfGeometryType.Cylinder:
                    shape = CreateCylinderShape((UrdfCylinder)geometry);
                    break;
                case UrdfGeometryType.Mesh:
                    var mesh = geometry as UrdfMesh;
                    shape = LoadShapeFromFile(mesh.FileName, baseDirectory);
                    break;
                case UrdfGeometryType.Sphere:
                    shape = CreateSphereShape((UrdfSphere)geometry);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return shape;
        }

        private static CollisionShape CreateBoxShape(UrdfBox box)
        {
            Vector3 size = ParseVector3(box.Size);
            var halfExtents = size * 0.5f;
            return new BoxShape(halfExtents);
        }

        private static CollisionShape CreateCapsuleShape(UrdfCapsule capsule)
        {
            float radius = (float)capsule.Radius * 0.5f;
            float length = (float)capsule.Length * 0.5f;
            return new CapsuleShape(radius, length);
        }

        private static CollisionShape CreateCylinderShape(UrdfCylinder cylinder)
        {
            float radius = (float)cylinder.Radius * 0.5f;
            float length = (float)cylinder.Length * 0.5f;
            return new CylinderShape(radius, length, radius);
        }

        private static CollisionShape CreateSphereShape(UrdfSphere sphere)
        {
            return new SphereShape((float)sphere.Radius);
        }

        private CollisionShape LoadShapeFromFile(string fileName, string baseDirectory)
        {
            string fullPath = Path.Combine(baseDirectory, fileName);
            string extension = Path.GetExtension(fullPath);
            switch (extension)
            {
                case ".obj":
                    WavefrontObj obj = WavefrontObj.Load(fullPath);
                    var mesh = CreateTriangleMesh(obj.Indices, obj.Vertices, Vector3.One);
                    const bool useQuantization = true;
                    return new BvhTriangleMeshShape(mesh, useQuantization);
                default:
                    throw new NotSupportedException();
            }
        }

        private static TriangleMesh CreateTriangleMesh(List<int> indices, List<Vector3> vertices, Vector3 localScaling)
        {
            var triangleMesh = new TriangleMesh();

            int triangleCount = indices.Count / 3;
            for (int i = 0; i < triangleCount; i++)
            {
                int index0 = indices[i * 3];
                int index1 = indices[i * 3 + 1];
                int index2 = indices[i * 3 + 2];

                Vector3 vertex0 = vertices[index0] * localScaling;
                Vector3 vertex1 = vertices[index1] * localScaling;
                Vector3 vertex2 = vertices[index2] * localScaling;

                triangleMesh.AddTriangleRef(ref vertex0, ref vertex1, ref vertex2);
            }

            return triangleMesh;
        }
    }
}
