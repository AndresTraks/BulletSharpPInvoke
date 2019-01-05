using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using DemoFramework.FileLoaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ConvexDecompositionDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<ConvexDecompositionDemo>();
        }
    }

    internal sealed class ConvexDecompositionDemo : IDemoConfiguration, IUpdateReceiver
    {
        private bool _enableSat = false;

        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(35, 10, 35);
            demo.FreeLook.Target = new Vector3(0, 5, 0);
            demo.Graphics.WindowTitle = "BulletSharp - Hierarchical Approximate Convex Decomposition Demo";
            SetDemoText(demo);
            return new ConvexDecompositionDemoSimulation(_enableSat);
        }

        public void Update(Demo demo)
        {
            if (demo.Input.KeysPressed.Contains(Keys.T))
            {
                _enableSat = !_enableSat;
                demo.ResetScene();
            }
        }

        private void SetDemoText(Demo demo)
        {
            if (_enableSat)
            {
                demo.DemoText = "SAT enabled";
            }
            else
            {
                demo.DemoText = "SAT disabled";
            }
        }
    }

    internal sealed class ConvexDecompositionDemoSimulation : ISimulation
    {
        private TriangleMesh _triangleMesh;
        private readonly bool _enableSat;

        public ConvexDecompositionDemoSimulation(bool enableSat)
        {
            _enableSat = enableSat;

            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);
            Broadphase = new AxisSweep3(new Vector3(-10000, -10000, -10000), new Vector3(10000, 10000, 10000));
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);

            CreateGround();

            ManifoldPoint.ContactAdded += MyContactCallback;
            //CompoundCollisionAlgorithm.CompoundChildShapePairCallback = MyCompoundChildShapeCallback;

            string path = Path.Combine("data", "file.obj");
            var wavefrontModel = WavefrontObj.Load(path);
            if (wavefrontModel.Indices.Count == 0)
            {
                return;
            }

            var localScaling = new Vector3(6, 6, 6);
            _triangleMesh = CreateTriangleMesh(wavefrontModel.Indices, wavefrontModel.Vertices, localScaling);

            // Convex hull approximation
            ConvexHullShape convexShape = CreateHullApproximation(_triangleMesh);
            float mass = 1.0f;
            PhysicsHelper.CreateBody(mass, Matrix.Translation(0, 2, 14), convexShape, World);

            // Non-moving body
            var objectOffset = new Vector3(10, 0, 0);
            const bool useQuantization = true;
            var concaveShape = new BvhTriangleMeshShape(_triangleMesh, useQuantization);
            PhysicsHelper.CreateStaticBody(Matrix.Translation(objectOffset), concaveShape, World);

            CompoundShape compoundShape;
            using (Hacd hacd = ComputeHacd(wavefrontModel))
            {
                hacd.Save("output.wrl", false);

                compoundShape = CreateCompoundShape(hacd, localScaling);
            }

            mass = 10.0f;
            objectOffset = new Vector3(-10, 0, -6);
            var body2 = PhysicsHelper.CreateBody(mass, Matrix.Translation(objectOffset), compoundShape, World);
            body2.CollisionFlags |= CollisionFlags.CustomMaterialCallback;

            objectOffset.Z += 6;
            body2 = PhysicsHelper.CreateBody(mass, Matrix.Translation(objectOffset), compoundShape, World);
            body2.CollisionFlags |= CollisionFlags.CustomMaterialCallback;

            objectOffset.Z += 6;
            body2 = PhysicsHelper.CreateBody(mass, Matrix.Translation(objectOffset), compoundShape, World);
            body2.CollisionFlags |= CollisionFlags.CustomMaterialCallback;
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        public void Dispose()
        {
            _triangleMesh.Dispose();

            this.StandardCleanup();
        }

        private bool MyCompoundChildShapeCallback(CollisionShape shape0, CollisionShape shape1)
        {
            return true;
        }

        // MyContactCallback is just an example to show how to get access to the child shape that collided
        private void MyContactCallback(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1)
        {
            if (colObj0Wrap.CollisionObject.CollisionShape.ShapeType == BroadphaseNativeType.CompoundShape)
            {
                CompoundShape compound = colObj0Wrap.CollisionObject.CollisionShape as CompoundShape;
                CollisionShape childShape = compound.GetChildShape(index0);
            }

            if (colObj1Wrap.CollisionObject.CollisionShape.ShapeType == BroadphaseNativeType.CompoundShape)
            {
                CompoundShape compound = colObj1Wrap.CollisionObject.CollisionShape as CompoundShape;
                CollisionShape childShape = compound.GetChildShape(index1);
            }
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(30, 2, 30);
            CollisionObject ground = PhysicsHelper.CreateStaticBody(Matrix.Translation(0, -4.5f, 0), groundShape, World);
            ground.UserObject = "Ground";
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

        private ConvexHullShape CreateHullApproximation(TriangleMesh triangleMesh)
        {
            using (var tmpConvexShape = new ConvexTriangleMeshShape(triangleMesh))
            {
                using (var hull = new ShapeHull(tmpConvexShape))
                {
                    hull.BuildHull(tmpConvexShape.Margin);
                    var convexShape = new ConvexHullShape(hull.Vertices);
                    if (_enableSat)
                    {
                        convexShape.InitializePolyhedralFeatures();
                    }
                    return convexShape;
                }
            }
        }

        private Hacd ComputeHacd(WavefrontObj model)
        {
            var hacd = new Hacd()
            {
                VerticesPerConvexHull = 100,
                CompacityWeight = 0.1,
                VolumeWeight = 0,

                // Recommended HACD parameters
                NClusters = 2,
                Concavity = 100,
                AddExtraDistPoints = false,
                AddFacesPoints = false,
                AddNeighboursDistPoints = false
            };
            hacd.SetPoints(model.Vertices);
            hacd.SetTriangles(model.Indices);

            hacd.Compute();
            return hacd;
        }

        private CompoundShape CreateCompoundShape(Hacd hacd, Vector3 localScaling)
        {
            var wavefrontWriter = new WavefrontWriter("file_convex.obj");
            var convexDecomposition = new ConvexDecomposition(wavefrontWriter)
            {
                LocalScaling = localScaling
            };

            for (int c = 0; c < hacd.NClusters; c++)
            {
                int trianglesLen = hacd.GetNTrianglesCH(c) * 3;
                if (trianglesLen == 0)
                {
                    continue;
                }
                var triangles = new long[trianglesLen];

                int nVertices = hacd.GetNPointsCH(c);
                var points = new double[nVertices * 3];
                
                hacd.GetCH(c, points, triangles);

                var verticesArray = new Vector3[nVertices];
                int vi3 = 0;
                for (int vi = 0; vi < nVertices; vi++)
                {
                    verticesArray[vi] = new Vector3(
                        (float)points[vi3], (float)points[vi3 + 1], (float)points[vi3 + 2]);
                    vi3 += 3;
                }

                convexDecomposition.Result(verticesArray, triangles);
            }

            wavefrontWriter.Dispose();

            // Combine convex shapes into a compound shape
            var compoundShape = new CompoundShape();
            for (int i = 0; i < convexDecomposition.ConvexShapes.Count; i++)
            {
                Vector3 centroid = convexDecomposition.ConvexCentroids[i];
                var convexShape = convexDecomposition.ConvexShapes[i];
                Matrix trans = Matrix.Translation(centroid);
                if (_enableSat)
                {
                    convexShape.InitializePolyhedralFeatures();
                }
                compoundShape.AddChildShape(trans, convexShape);

                PhysicsHelper.CreateBody(1.0f, trans, convexShape, World);
            }

            return compoundShape;
        }
    }
}
