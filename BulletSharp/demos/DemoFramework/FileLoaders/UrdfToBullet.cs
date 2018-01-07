using BulletSharp;
using BulletSharp.Math;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DemoFramework.FileLoaders
{
    public class UrdfToBullet
    {
        private static char[] _spaceSeparator = new[] { ' ' };

        private IDictionary<UrdfLink, UrdfJoint> _linkToParentJoint;
        private IDictionary<string, RigidBody> _linkToRigidBody = new Dictionary<string, RigidBody>();

        public UrdfToBullet(DiscreteDynamicsWorld world)
        {
            World = world;
        }

        public DiscreteDynamicsWorld World { get; private set; }

        public void Convert(UrdfRobot robot, string baseDirectory)
        {
            _linkToParentJoint = FindLinkParents(robot);

            var rootLink = _linkToParentJoint.FirstOrDefault(link => link.Value == null).Key;
            if (rootLink != null)
            {
                LoadLink(rootLink, Matrix.Identity, baseDirectory);
            }
        }

        private static IDictionary<UrdfLink, UrdfJoint> FindLinkParents(UrdfRobot robot)
        {
            var linkToParent = new Dictionary<UrdfLink, UrdfJoint>();
            foreach (UrdfLink link in robot.Links.Values)
            {
                UrdfJoint parentJoint = robot.Joints.FirstOrDefault(joint => joint.Child == link);
                linkToParent[link] = parentJoint;
            }
            return linkToParent;
        }

        private void LoadLink(UrdfLink link, Matrix parentTransform, string baseDirectory)
        {
            float mass = 0;
            UrdfInertial inertial = link.Inertial;
            if (inertial != null)
            {
                mass = (float)inertial.Mass;
            }

            Matrix worldTransform = parentTransform;

            UrdfCollision collision = link.Collision;
            if (collision != null)
            {
                Matrix origin = ParsePose(collision.Origin);
                worldTransform = worldTransform * origin;
                UrdfGeometry geometry = collision.Geometry;
                CollisionShape shape = CreateShapeFromGeometry(baseDirectory, mass, geometry);
                RigidBody body;
                if (mass == 0)
                {
                    body = PhysicsHelper.CreateStaticBody(worldTransform, shape, World);
                }
                else
                {
                    body = PhysicsHelper.CreateBody(mass, worldTransform, shape, World);
                }

                _linkToRigidBody[link.Name] = body;
            }

            var children = _linkToParentJoint.Where(l => l.Value?.Parent == link);
            foreach (KeyValuePair<UrdfLink, UrdfJoint> child in children)
            {
                LoadLink(child.Key, worldTransform, baseDirectory);
                LoadJoint(child.Value);
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
                float.Parse(components[0], CultureInfo.InvariantCulture),
                float.Parse(components[1], CultureInfo.InvariantCulture),
                float.Parse(components[2], CultureInfo.InvariantCulture));
        }

        private CollisionShape CreateShapeFromGeometry(string baseDirectory, float mass, UrdfGeometry geometry)
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

        private void LoadJoint(UrdfJoint joint)
        {
            RigidBody childRigidBody;
            if (!_linkToRigidBody.TryGetValue(joint.Child.Name, out childRigidBody))
            {
                return;
            }

            RigidBody parentRigidBody;
            if (joint.Parent.Collision != null)
            {
                if (!_linkToRigidBody.TryGetValue(joint.Parent.Name, out parentRigidBody))
                {
                    return;
                }
            }
            else
            {
                parentRigidBody = TypedConstraint.GetFixedBody();
            }

            TypedConstraint constraint;
            if (joint is UrdfContinuousJoint)
            {
                constraint = CreateRevoluteJoint(childRigidBody, parentRigidBody);
            }
            else if (joint is UrdfFixedJoint)
            {
                Matrix inertia = ParseInertia(joint.Child.Inertial.Inertia);

                constraint = CreateFixedJoint(childRigidBody, parentRigidBody);
            }
            else
            {
                //throw new NotImplementedException();
                return;
            }
            World.AddConstraint(constraint, true);
        }

        private Generic6DofSpring2Constraint CreateRevoluteJoint(RigidBody rigidBodyA, RigidBody rigidBodyB)
        {
            return new Generic6DofSpring2Constraint(rigidBodyA, rigidBodyB, Matrix.Identity, Matrix.Identity)
            {
                LinearLowerLimit = Vector3.Zero,
                LinearUpperLimit = Vector3.Zero
            };
        }

        private Generic6DofSpring2Constraint CreateFixedJoint(RigidBody rigidBodyA, RigidBody rigidBodyB)
        {
            return new Generic6DofSpring2Constraint(rigidBodyA, rigidBodyB, Matrix.Identity, Matrix.Identity)
            {
                AngularLowerLimit = Vector3.Zero,
                AngularUpperLimit = Vector3.Zero,
                LinearLowerLimit = Vector3.Zero,
                LinearUpperLimit = Vector3.Zero
            };
        }

        private Matrix ParseInertia(UrdfInertia inertia)
        {
            if (inertia.XY == 0 && inertia.XZ == 0 && inertia.YZ == 0)
            {
                return Matrix.Identity;
            }

            return new Matrix(
                (float)inertia.XX, (float)inertia.XY, (float)inertia.XZ, 0,
                (float)inertia.XY, (float)inertia.YY, (float)inertia.YZ, 0,
                (float)inertia.XZ, (float)inertia.YZ, (float)inertia.ZZ, 0,
                0, 0, 0, 1);
        }
    }
}
