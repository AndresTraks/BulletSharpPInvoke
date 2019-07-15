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
            LoadCollisions(link, baseDirectory, parentTransform);

            Matrix worldTransform;
            RigidBody body;
            if (_linkToRigidBody.TryGetValue(link.Name, out body))
            {
                worldTransform = body.WorldTransform;
            }
            else
            {
                worldTransform = Matrix.Identity;
            }

            var children = _linkToParentJoint.Where(l => l.Value?.Parent == link);
            foreach (KeyValuePair<UrdfLink, UrdfJoint> child in children)
            {
                LoadLink(child.Key, worldTransform, baseDirectory);
                LoadJoint(child.Value);
            }
        }

        private void LoadCollisions(UrdfLink link, string baseDirectory, Matrix parentTransform)
        {
            if (link.Collisions.Length == 0)
            {
                return;
            }

            UrdfInertial inertial = link.Inertial;
            double mass = inertial != null
                ? inertial.Mass
                : 0;

            Matrix origin = ParsePose(link.Inertial.Origin);
            //Matrix inertia = ParseInertia(link.Inertial.Inertia);
            parentTransform = parentTransform * origin;

            CollisionShape shape;
            Matrix pose;
            if (link.Collisions.Length == 1)
            {
                var collision = link.Collisions[0];
                shape = CreateShapeFromGeometry(collision.Geometry, mass, baseDirectory);
                pose = parentTransform * ParsePose(collision.Origin);
            }
            else
            {
                shape = LoadCompoundCollisionShape(link.Collisions, baseDirectory, mass, parentTransform);
                pose = parentTransform;
            }

            RigidBody body;
            if (mass == 0)
            {
                body = PhysicsHelper.CreateStaticBody(pose, shape, World);
            }
            else
            {
                body = PhysicsHelper.CreateBody(mass, pose, shape, World);
            }

            _linkToRigidBody[link.Name] = body;
        }

        private CompoundShape LoadCompoundCollisionShape(UrdfCollision[] collisions, string baseDirectory, double mass, Matrix parentTransform)
        {
            var compoundShape = new CompoundShape(true, collisions.Length);
            foreach (UrdfCollision collision in collisions)
            {
                Matrix origin = ParsePose(collision.Origin);
                Matrix childTransform = origin * parentTransform;
                CollisionShape shape = CreateShapeFromGeometry(collision.Geometry, mass, baseDirectory);
                compoundShape.AddChildShapeRef(ref childTransform, shape);
            }
            return compoundShape;
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

        private CollisionShape CreateShapeFromGeometry(UrdfGeometry geometry, double mass, string baseDirectory)
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
                    Vector3 scale;
                    if (mesh.Scale != null)
                    {
                        scale = ParseVector3(mesh.Scale);
                    }
                    else
                    {
                        scale = Vector3.One;
                    }
                    shape = LoadShapeFromFile(mesh.FileName, mass, scale, baseDirectory);
                    break;
                case UrdfGeometryType.Plane:
                    shape = CreatePlaneShape((UrdfPlane)geometry);
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
            var halfExtents = size * 0.5;
            return new BoxShape(halfExtents);
        }

        private static CollisionShape CreateCapsuleShape(UrdfCapsule capsule)
        {
            double radius = capsule.Radius * 0.5;
            double length = capsule.Length * 0.5;
            return new CapsuleShape(radius, length);
        }

        private static CollisionShape CreateCylinderShape(UrdfCylinder cylinder)
        {
            double radius = cylinder.Radius * 0.5;
            double length = cylinder.Length * 0.5;
            return new CylinderShape(radius, length, radius);
        }

        private static CollisionShape CreatePlaneShape(UrdfPlane plane)
        {
            const double planeConstant = 0;
            return new StaticPlaneShape(ParseVector3(plane.Normal), planeConstant);
        }

        private static CollisionShape CreateSphereShape(UrdfSphere sphere)
        {
            return new SphereShape(sphere.Radius);
        }

        private CollisionShape LoadShapeFromFile(string fileName, double mass, Vector3 scale, string baseDirectory)
        {
            string fullPath = Path.Combine(baseDirectory, fileName);
            string extension = Path.GetExtension(fullPath);
            switch (extension)
            {
                case ".obj":
                    WavefrontObj obj = WavefrontObj.Load(fullPath);
                    var mesh = CreateTriangleMesh(obj.Indices, obj.Vertices, scale);
                    if (mass == 0)
                    {
                        const bool useQuantization = true;
                        return new BvhTriangleMeshShape(mesh, useQuantization);
                    }
                    else
                    {
                        // TODO: convex decomposition
                        GImpactCollisionAlgorithm.RegisterAlgorithm((CollisionDispatcher)World.Dispatcher);
                        var shape = new GImpactMeshShape(mesh);
                        shape.Margin = 0;
                        shape.UpdateBound();
                        return shape;
                    }
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
            if (joint.Parent.Collisions.Any())
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
                Matrix childFrame = ParseInertia(joint.Child.Inertial.Inertia);
                childFrame = ParsePose(joint.Origin);

                constraint = CreateFixedJoint(childRigidBody, parentRigidBody, childFrame);
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

        private Generic6DofSpring2Constraint CreateFixedJoint(RigidBody rigidBodyA, RigidBody rigidBodyB, Matrix childFrame)
        {
            return new Generic6DofSpring2Constraint(rigidBodyA, rigidBodyB, childFrame, Matrix.Identity)
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
                inertia.XX, inertia.XY, inertia.XZ, 0,
                inertia.XY, inertia.YY, inertia.YZ, 0,
                inertia.XZ, inertia.YZ, inertia.ZZ, 0,
                0, 0, 0, 1);
        }
    }
}
