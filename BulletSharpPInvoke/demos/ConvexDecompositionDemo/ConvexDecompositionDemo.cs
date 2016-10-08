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
    class ConvexDecompositionDemo : Demo
    {
        Vector3 eye = new Vector3(35, 10, 35);
        Vector3 target = new Vector3(0, 5, 0);

        TriangleMesh triangleMesh;
        bool enableSat = false;

        protected override void OnInitialize()
        {
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Convex Decomposition Demo");
        }

        bool MyCompoundChildShapeCallback(CollisionShape shape0, CollisionShape shape1)
        {
            return true;
        }

        // MyContactCallback is just an example to show how to get access to the child shape that collided
        void MyContactCallback(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1)
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

        protected override void OnInitializePhysics()
        {
            ManifoldPoint.ContactAdded += MyContactCallback;

            SetupEmptyDynamicsWorld();
            CreateGround();

            //CompoundCollisionAlgorithm.CompoundChildShapePairCallback = MyCompoundChildShapeCallback;

            var wo = WavefrontObj.Load("data/file.obj");
            if (wo.Indices.Count == 0)
            {
                return;
            }

            var localScaling = new Vector3(6, 6, 6);
            triangleMesh = CreateTriangleMesh(wo.Indices, wo.Vertices, localScaling);

            // Convex hull approximation
            ConvexHullShape convexShape = CreateHullApproximation(triangleMesh);
            CollisionShapes.Add(convexShape);
            float mass = 1.0f;
            LocalCreateRigidBody(mass, Matrix.Translation(0, 2, 14), convexShape);

            // Non-moving body
            Vector3 convexDecompositionObjectOffset = new Vector3(10, 0, 0);
            const bool useQuantization = true;
            var concaveShape = new BvhTriangleMeshShape(triangleMesh, useQuantization);
            CollisionShapes.Add(concaveShape);
            LocalCreateRigidBody(0, Matrix.Translation(convexDecompositionObjectOffset), concaveShape);


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
            hacd.SetPoints(wo.Vertices);
            hacd.SetTriangles(wo.Indices);

            hacd.Compute();
            hacd.Save("output.wrl", false);


            // Generate convex result
            var outputFile = new FileStream("file_convex.obj", FileMode.Create, FileAccess.Write);
            var writer = new StreamWriter(outputFile);
            var convexDecomposition = new ConvexDecomposition(writer) { LocalScaling = localScaling };

            for (int c = 0; c < hacd.NClusters; c++)
            {
                int nVertices = hacd.GetNPointsCH(c);
                int trianglesLen = hacd.GetNTrianglesCH(c) * 3;
                double[] points = new double[nVertices * 3];
                long[] triangles = new long[trianglesLen];
                hacd.GetCH(c, points, triangles);

                if (trianglesLen == 0)
                {
                    continue;
                }

                Vector3[] verticesArray = new Vector3[nVertices];
                int vi3 = 0;
                for (int vi = 0; vi < nVertices; vi++)
                {
                    verticesArray[vi] = new Vector3(
                        (float)points[vi3], (float)points[vi3 + 1], (float)points[vi3 + 2]);
                    vi3 += 3;
                }

                int[] trianglesInt = new int[trianglesLen];
                for (int ti = 0; ti < trianglesLen; ti++)
                {
                    trianglesInt[ti] = (int)triangles[ti];
                }

                convexDecomposition.Result(verticesArray, trianglesInt);
            }

            writer.Dispose();
            outputFile.Dispose();


            // Combine convex shapes into a compound shape
            var compound = new CompoundShape();
            for (int i = 0; i < convexDecomposition.convexShapes.Count; i++)
            {
                Vector3 centroid = convexDecomposition.convexCentroids[i];
                var convexShape2 = convexDecomposition.convexShapes[i];
                Matrix trans = Matrix.Translation(centroid);
                if (enableSat)
                {
                    convexShape2.InitializePolyhedralFeatures();
                }
                CollisionShapes.Add(convexShape2);
                compound.AddChildShape(trans, convexShape2);

                LocalCreateRigidBody(1.0f, trans, convexShape2);
            }
            CollisionShapes.Add(compound);

#if true
            mass = 10.0f;
            var body2 = LocalCreateRigidBody(mass, Matrix.Translation(-convexDecompositionObjectOffset), compound);
            body2.CollisionFlags |= CollisionFlags.CustomMaterialCallback;

            convexDecompositionObjectOffset.Z = 6;
            body2 = LocalCreateRigidBody(mass, Matrix.Translation(-convexDecompositionObjectOffset), compound);
            body2.CollisionFlags |= CollisionFlags.CustomMaterialCallback;

            convexDecompositionObjectOffset.Z = -6;
            body2 = LocalCreateRigidBody(mass, Matrix.Translation(-convexDecompositionObjectOffset), compound);
            body2.CollisionFlags |= CollisionFlags.CustomMaterialCallback;
#endif
        }

        public void SetupEmptyDynamicsWorld()
        {
            // collision configuration contains default setup for memory, collision setup
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);
            Broadphase = new AxisSweep3(new Vector3(-10000, -10000, -10000), new Vector3(10000, 10000, 10000));
            Solver = new SequentialImpulseConstraintSolver();
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, Solver, CollisionConf);
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(30, 2, 30);
            CollisionShapes.Add(groundShape);
            CollisionObject ground = LocalCreateRigidBody(0, Matrix.Translation(0, -4.5f, 0), groundShape);
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
                    if (enableSat)
                    {
                        convexShape.InitializePolyhedralFeatures();
                    }
                    return convexShape;
                }
            }
        }

        public override void OnHandleInput()
        {
            base.OnHandleInput();

            if (Input.KeysPressed.Contains(Keys.T))
            {
                enableSat = !enableSat;
                if (enableSat)
                {
                    Console.WriteLine("SAT enabled after the next restart of the demo");
                }
                else
                {
                    Console.WriteLine("SAT disabled after the next restart of the demo");
                }
            }
        }

        public override void ExitPhysics()
        {
            base.ExitPhysics();

            triangleMesh.Dispose();
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new ConvexDecompositionDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
