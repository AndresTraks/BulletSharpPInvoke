using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using DemoFramework;
using DemoFramework.Meshes;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SoftDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<SoftDemo>();
        }
    }

    internal sealed class SoftDemo : IDemoConfiguration, IUpdateReceiver
    {
        private int _demoIndex = 27;
        private Point _lastMousePosition;
        private Vector3 _impact;
        private SoftBodyRayCast _rayCast = new SoftBodyRayCast();
        private bool _drag;

        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(20, 20, 80);
            demo.FreeLook.Target = new Vector3(0, 0, 10);
            demo.DemoText =
                "B - Previous Demo\n" +
                "N - Next Demo";
            demo.Graphics.WindowTitle = "BulletSharp - SoftBody Demo";
            var simulation = new SoftDemoSimulation(_demoIndex);
            demo.Graphics.CullingEnabled = !simulation.HasTwoSidedFaces;
            return simulation;
        }

        public void Update(Demo demo)
        {
            var simulation = demo.Simulation as SoftDemoSimulation;
            simulation.OnUpdate();

            UpdateMotorControl(demo, simulation);
            UpdatePickConstraint(demo, simulation);

            if (demo.Input.KeysPressed.Contains(Keys.B))
            {
                _demoIndex--;
                demo.ResetScene();
            }
            else if (demo.Input.KeysPressed.Contains(Keys.N))
            {
                _demoIndex++;
                demo.ResetScene();
            }
        }

        private void UpdatePickConstraint(Demo demo, SoftDemoSimulation simulation)
        {
            if (demo.Input.MousePressed == MouseButtons.Right)
            {
                _rayCast.Fraction = 1;
                //if (pickConstraint == null)
                {
                    Vector3 rayFrom = demo.FreeLook.Eye;
                    Vector3 rayTo = demo.GetRayTo(demo.Input.MousePoint, demo.FreeLook.Eye, demo.FreeLook.Target, demo.Graphics.FieldOfView);
                    Vector3 rayDir = rayTo - rayFrom;
                    rayDir.Normalize();

                    var newRayCast = new SoftBodyRayCast();
                    if (simulation.SoftWorld.SoftBodyArray.Any(b => b.RayTestRef(ref rayFrom, ref rayTo, newRayCast)))
                    {
                        _rayCast = newRayCast;
                        _impact = rayFrom + (rayTo - rayFrom) * _rayCast.Fraction;
                        _lastMousePosition = demo.Input.MousePoint;

                        if (!simulation.Cutting)
                        {
                            _drag = true;
                            simulation.PickedNode = GetPickedNode(_rayCast);
                        }
                    }
                }
            }
            else if (demo.Input.MouseReleased == MouseButtons.Right)
            {
                if (!_drag && simulation.Cutting && _rayCast.Fraction < 1)
                {
                    using (var isphere = new ImplicitSphere(ref _impact, 1))
                    {
                        _rayCast.Body.Refine(isphere, 0.0001f, true);
                    }
                }
                _rayCast.Fraction = 1;
                _drag = false;
                simulation.PickedNode = null;
            }

            // Mouse movement
            if (demo.Input.MouseDown == MouseButtons.Right)
            {
                if (simulation.PickedNode != null && _rayCast.Fraction < 1)
                {
                    if (!_drag)
                    {
                        int x = demo.Input.MousePoint.X - _lastMousePosition.X;
                        int y = demo.Input.MousePoint.Y - _lastMousePosition.Y;
                        if ((x * x) + (y * y) > 6)
                        {
                            _drag = true;
                        }
                    }
                    if (_drag)
                    {
                        _lastMousePosition = demo.Input.MousePoint;
                    }
                }
            }

            if (_drag)
            {
                UpdatePickedNodeGoal(demo, simulation);
            }
        }

        private void UpdateMotorControl(Demo demo, SoftDemoSimulation simulation)
        {
            if (simulation.MotorControl == null)
            {
                return;
            }

            var keys = demo.Input.KeysDown;
            if (keys.Count == 0)
            {
                return;
            }

            if (keys.Contains(Keys.Up))
            {
                simulation.MotorControl.MaxTorque = 1;
                simulation.MotorControl.Goal += demo.FrameDelta * 2;
            }
            else if (keys.Contains(Keys.Down))
            {
                simulation.MotorControl.MaxTorque = 1;
                simulation.MotorControl.Goal -= demo.FrameDelta * 2;
            }
            if (keys.Contains(Keys.Left))
            {
                simulation.SteerControlFront.Angle += demo.FrameDelta;
                simulation.SteerControlRear.Angle += demo.FrameDelta;
            }
            else if (keys.Contains(Keys.Right))
            {
                simulation.SteerControlFront.Angle -= demo.FrameDelta;
                simulation.SteerControlRear.Angle -= demo.FrameDelta;
            }
        }

        private Node GetPickedNode(SoftBodyRayCast results)
        {
            NodePtrArray nodes;
            switch (results.Feature)
            {
                case FeatureType.Face:
                    nodes = results.Body.Faces[results.Index].Nodes;
                    break;
                case FeatureType.Tetra:
                    nodes = results.Body.Tetras[results.Index].Nodes;
                    break;
                default:
                    return null;
            }
            if (nodes != null)
            {
                return nodes.Aggregate((min, n) =>
                    (n.Position - _impact).LengthSquared <
                    (min.Position - _impact).LengthSquared ? n : min
                );
            }
            return null;
        }

        private void UpdatePickedNodeGoal(Demo demo, SoftDemoSimulation simulation)
        {
            Vector3 rayFrom = demo.FreeLook.Eye;
            Vector3 rayTo = demo.GetRayTo(_lastMousePosition, demo.FreeLook.Eye, demo.FreeLook.Target, demo.Graphics.FieldOfView);
            Vector3 rayDir = rayTo - rayFrom;
            rayDir.Normalize();
            Vector3 N = demo.FreeLook.Target - rayFrom;
            N.Normalize();
            float O = Vector3.Dot(_impact, N);
            float den = Vector3.Dot(N, rayDir);
            if ((den * den) > 0)
            {
                float num = O - Vector3.Dot(N, rayFrom);
                float hit = num / den;
                if (hit > 0 && hit < 1500)
                {
                    simulation.PickedNodeGoal = rayFrom + rayDir * hit;
                }
            }
        }
    }

    internal sealed class SoftDemoSimulation : ISimulation
    {
        private const int maxProxies = 32766;
        private readonly SoftBodyWorldInfo _softBodyWorldInfo;

        public SoftDemoSimulation(int demoIndex)
        {
            CollisionConfiguration = new SoftBodyRigidBodyCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            Broadphase = new AxisSweep3(new Vector3(-1000, -1000, -1000),
                new Vector3(1000, 1000, 1000), maxProxies);

            SoftWorld = new SoftRigidDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.DispatchInfo.EnableSpu = true;

            World.SetInternalTickCallback(PickingPreTickCallback, this, true);

            CreateGround();

            _softBodyWorldInfo = new SoftBodyWorldInfo
            {
                AirDensity = 1.2f,
                WaterDensity = 0,
                WaterOffset = 0,
                WaterNormal = Vector3.Zero,
                Gravity = new Vector3(0, -10, 0),
                Dispatcher = Dispatcher,
                Broadphase = Broadphase
            };
            _softBodyWorldInfo.SparseSdf.Initialize();

            var demos = new Action[] { InitCloth, InitLargeBallRollingStairs, InitSmallBallRollingStairs, InitRopes,
                InitRopeAttach, InitClothAttach, InitSticks, InitCapsuleClothCollision, InitTorusCollide,
                InitBunnyCollide, InitPatchCollide, InitImpact, InitAerodynamicFlyers, InitAerodynamicSheets,
                InitBoxFriction, InitTorus, InitTorusPoseMatching, InitBunny, InitBunnyPoseMatching, InitPatchCutting,
                InitClusterDeform, InitClothStackCollide, InitClusterTorusCollide, InitClusterSocket, InitClusterHinge,
                InitClusterCombine, InitClusterCar, InitClusterRobot, InitClusterTorusStack, InitClusterStackMixed,
                InitTetraCube, InitTetraBunny, InitBending
            };
            demos[PositiveMod(demoIndex, demos.Length)]();
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World => SoftWorld;

        public SoftRigidDynamicsWorld SoftWorld { get; }

        public bool Cutting { get; private set; }
        public bool HasTwoSidedFaces { get; private set; }

        public MotorControl MotorControl { get; private set; }
        public SteerControl SteerControlFront { get; private set; }
        public SteerControl SteerControlRear { get; private set; }

        public Node PickedNode { get; set; }
        public Vector3 PickedNodeGoal { get; set; }

        public void OnUpdate()
        {
            _softBodyWorldInfo.SparseSdf.GarbageCollect();
        }

        public void Dispose()
        {
            _softBodyWorldInfo.Dispose();

            if (MotorControl != null)
            {
                MotorControl.Dispose();
                SteerControlFront.Dispose();
                SteerControlRear.Dispose();
            }

            this.StandardCleanup();
        }

        private void CreateGround()
        {
            var groundShape = new BoxShape(50, 50, 50);
            RigidBody body = PhysicsHelper.CreateStaticBody(Matrix.Translation(0, -62, 0), groundShape, World);
            body.UserObject = "Ground";
        }

        private void InitCloth()
        {
            const float scale = 8;
            const int resolution = 31;
            const int fixedCorners = 1 + 2 + 4 + 8;

            SoftBody body = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo, new Vector3(-scale, 0, -scale),
                new Vector3(+scale, 0, -scale),
                new Vector3(-scale, 0, +scale),
                new Vector3(+scale, 0, +scale),
                resolution, resolution,
                fixedCorners, true);
            body.CollisionShape.Margin = 0.5f;
            Material material = body.AppendMaterial();
            material.LinearStiffness = 0.4f;
            material.Flags -= MaterialFlags.DebugDraw;
            body.GenerateBendingConstraints(2, material);
            body.TotalMass = 150;
            SoftWorld.AddSoftBody(body);

            CreateRigidBodyStack(10);
            Cutting = true;

            HasTwoSidedFaces = true;
        }

        private void CreateRigidBodyStack(int count)
        {
            const float mass = 10.0f;

            var cylinderCompound = new CompoundShape();

            var boxShape = new BoxShape(4, 1, 1);
            cylinderCompound.AddChildShape(Matrix.Identity, boxShape);

            Quaternion orn = Quaternion.RotationYawPitchRoll((float)Math.PI / 2.0f, 0, 0);
            Matrix localTransform = Matrix.RotationQuaternion(orn);
            //localTransform *= Matrix.Translation(new Vector3(1, 1, 1));
            var cylinderShape = new CylinderShapeX(4, 1, 1);
            cylinderCompound.AddChildShape(localTransform, cylinderShape);

            var shape = new CollisionShape[] {
                cylinderCompound,
                new BoxShape(1),
                new SphereShape(1.5f) };
            for (int i = 0; i < count; i++)
            {
                PhysicsHelper.CreateBody(mass, Matrix.Translation(0, 2 + 6 * i, 0), shape[i % shape.Length], World);
            }
        }

        private void InitLargeBallRollingStairs()
        {
            SoftBody body = SoftBodyHelpers.CreateEllipsoid(_softBodyWorldInfo, new Vector3(35, 25, 0),
                new Vector3(3, 3, 3), 512);
            body.Materials[0].LinearStiffness = 0.1f;
            body.Cfg.DynamicFriction = 1;
            body.Cfg.Damping = 0.001f; // fun factor...
            body.Cfg.Pressure = 2500;
            body.SetTotalMass(30, true);
            SoftWorld.AddSoftBody(body);

            CreateBigPlate();
            CreateLinearStair(10, Vector3.Zero, new Vector3(2, 1, 5));
        }


        private void InitSmallBallRollingStairs()
        {
            SoftBody body = SoftBodyHelpers.CreateEllipsoid(_softBodyWorldInfo, new Vector3(35, 25, 0),
                new Vector3(1, 1, 1) * 3, 512);
            body.Materials[0].LinearStiffness = 0.45f;
            body.Cfg.VolumeConversation = 20;
            body.SetTotalMass(50, true);
            body.SetPose(true, false);
            SoftWorld.AddSoftBody(body);

            CreateBigPlate();
            CreateLinearStair(10, Vector3.Zero, new Vector3(2, 1, 5));
        }

        private void InitRopes()
        {
            const int n = 15;
            for (int i = 0; i < n; i++)
            {
                SoftBody body = SoftBodyHelpers.CreateRope(_softBodyWorldInfo,
                    new Vector3(-10, 0, i * 0.25f),
                    new Vector3(10, 0, i * 0.25f), 16, 1 + 2);
                body.Cfg.PositionIterations = 4;
                body.Materials[0].LinearStiffness = 0.1f + (i / (float)(n - 1)) * 0.9f;
                body.TotalMass = 20;
                SoftWorld.AddSoftBody(body);
            }
        }

        private void InitRopeAttach()
        {
            _softBodyWorldInfo.SparseSdf.RemoveReferences(null);
            RigidBody body = PhysicsHelper.CreateBody(50, Matrix.Translation(12, 8, 0), new BoxShape(2, 6, 2), World);
            SoftBody rope1 = CreateRope(new Vector3(0, 8, -1));
            SoftBody rope2 = CreateRope(new Vector3(0, 8, +1));
            rope1.AppendAnchor(rope1.Nodes.Count - 1, body);
            rope2.AppendAnchor(rope2.Nodes.Count - 1, body);
        }

        private SoftBody CreateRope(Vector3 position)
        {
            SoftBody body = SoftBodyHelpers.CreateRope(_softBodyWorldInfo, position, position + new Vector3(10, 0, 0), 8, 1);
            body.TotalMass = 50;
            SoftWorld.AddSoftBody(body);
            return body;
        }

        private void InitClothAttach()
        {
            const float scale = 4;
            const float height = 6;
            const int resolution = 9;
            const int fixedCorners = 4 + 8;

            SoftBody cloth = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo, new Vector3(-scale, height, -scale),
                new Vector3(+scale, height, -scale),
                new Vector3(-scale, height, +scale),
                new Vector3(+scale, height, +scale), resolution, resolution, fixedCorners, true);
            SoftWorld.AddSoftBody(cloth);

            RigidBody body = PhysicsHelper.CreateBody(20, Matrix.Translation(0, height, -(scale + 3.5f)), new BoxShape(scale, 1, 3), World);
            cloth.AppendAnchor(0, body);
            cloth.AppendAnchor(resolution - 1, body);

            Cutting = true;
            HasTwoSidedFaces = true;
        }

        private void InitSticks()
        {
            const int numSticksPerSide = 16;
            const int resolution = 4;
            const float sideWidth = 7;
            const float height = 4;
            const float density = 1 / (float)(numSticksPerSide - 1);
            for (int y = 0; y < numSticksPerSide; y++)
            {
                for (int x = 0; x < numSticksPerSide; x++)
                {
                    var origin = new Vector3(-sideWidth + sideWidth * 2 * x * density,
                        -10, -sideWidth + sideWidth * 2 * y * density);

                    SoftBody body = SoftBodyHelpers.CreateRope(_softBodyWorldInfo, origin,
                        origin + new Vector3(height * 0.001f, height, 0), resolution, 1);

                    body.Cfg.Damping = 0.005f;
                    body.Cfg.RigidContactHardness = 0.1f;
                    for (int i = 0; i < 3; i++)
                    {
                        body.GenerateBendingConstraints(2 + i);
                    }
                    body.SetMass(1, 0);
                    body.TotalMass = 0.01f;
                    SoftWorld.AddSoftBody(body);
                }
            }
            CreateBigBall(new Vector3(0, 13, 0));
        }

        private void CreateBigBall(Vector3 position)
        {
            PhysicsHelper.CreateBody(10.0f, Matrix.Translation(position), new SphereShape(1.5f), World);
        }

        private void InitCapsuleClothCollision()
        {
            const float scale = 4;
            const float height = 6;
            const int resolution = 20;
            const int fixedCorners = 0; // 4 + 8;

            Matrix startTransform = Matrix.Translation(0, height - 2, 0);

            var capsuleShape = new CapsuleShapeX(1, 5);
            capsuleShape.Margin = 0.5f;

            //capsuleShape.LocalScaling = new Vector3(5, 1, 1);
            //RigidBody body = PhysicsHelper.CreateBody(20, startTransform, capsuleShape, World);
            RigidBody body = PhysicsHelper.CreateStaticBody(startTransform, capsuleShape, World);
            body.Friction = 0.8f;

            SoftBody cloth = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo, new Vector3(-scale, height, -scale),
                new Vector3(+scale, height, -scale),
                new Vector3(-scale, height, +scale),
                new Vector3(+scale, height, +scale), resolution, resolution, fixedCorners, true);
            SoftWorld.AddSoftBody(cloth);
            cloth.TotalMass = 0.1f;

            cloth.Cfg.PositionIterations = 10;
            cloth.Cfg.ClusterIterations = 10;
            cloth.Cfg.DriftIterations = 10;
            //cloth.Cfg.VelocityIterations = 10;

            //cloth.AppendAnchor(0, body);
            //cloth.AppendAnchor(r-1, body);

            Cutting = true;
            HasTwoSidedFaces = true;
        }

        private void InitTorusCollide()
        {
            for (int i = 0; i < 3; i++)
            {
                SoftBody torus = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Torus.Vertices, Torus.Indices);
                torus.GenerateBendingConstraints(2);
                torus.Cfg.PositionIterations = 2;
                torus.Cfg.Collisions |= Collisions.VertexFaceSoftSoft;
                torus.RandomizeConstraints();
                Matrix transform = Matrix.RotationYawPitchRoll((float)Math.PI / 2 * (i & 1), (float)Math.PI / 2 * (1 - (i & 1)), 0) *
                    Matrix.Translation(3 * i, 2, 0);
                torus.Transform(transform);
                torus.Scale(new Vector3(2));
                torus.SetTotalMass(50, true);
                SoftWorld.AddSoftBody(torus);
            }

            Cutting = true;
        }

        private void InitBunnyCollide()
        {
            for (int i = 0; i < 3; i++)
            {
                SoftBody bunny = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Bunny.Vertices, Bunny.Indices);
                Material material = bunny.AppendMaterial();
                material.LinearStiffness = 0.5f;
                material.Flags -= MaterialFlags.DebugDraw;
                bunny.GenerateBendingConstraints(2, material);
                bunny.Cfg.PositionIterations = 2;
                bunny.Cfg.DynamicFriction = 0.5f;
                bunny.Cfg.Collisions |= Collisions.VertexFaceSoftSoft;
                bunny.RandomizeConstraints();
                Matrix transform = Matrix.RotationYawPitchRoll((float)Math.PI / 2 * (i & 1), 0, 0) *
                    Matrix.Translation(0, -1 + 5 * i, 0);
                bunny.Transform(transform);
                bunny.Scale(new Vector3(6, 6, 6));
                bunny.SetTotalMass(100, true);
                SoftWorld.AddSoftBody(bunny);
            }

            Cutting = true;
        }

        private void InitPatchCollide()
        {
            const float patch1Scale = 8;
            const int resolution = 15;
            const int fixedCorners = 1 + 2 + 4 + 8;
            SoftBody patch1 = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo,
                new Vector3(-patch1Scale, 0, -patch1Scale),
                new Vector3(+patch1Scale, 0, -patch1Scale),
                new Vector3(-patch1Scale, 0, +patch1Scale),
                new Vector3(+patch1Scale, 0, +patch1Scale),
                resolution, resolution, fixedCorners, true);
            patch1.Materials[0].LinearStiffness = 0.4f;
            patch1.Cfg.Collisions |= Collisions.VertexFaceSoftSoft;
            patch1.TotalMass = 150;
            SoftWorld.AddSoftBody(patch1);

            const float patch2Scale = 4;
            var offset = new Vector3(5, 10, 0);
            SoftBody patch2 = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo,
                new Vector3(-patch2Scale, 0, -patch2Scale) + offset,
                new Vector3(+patch2Scale, 0, -patch2Scale) + offset,
                new Vector3(-patch2Scale, 0, +patch2Scale) + offset,
                new Vector3(+patch2Scale, 0, +patch2Scale) + offset,
                7, 7, 0, true);
            Material material = patch2.AppendMaterial();
            material.LinearStiffness = 0.1f;
            material.Flags -= MaterialFlags.DebugDraw;
            patch2.GenerateBendingConstraints(2, material);
            patch2.Materials[0].LinearStiffness = 0.5f;
            patch2.Cfg.Collisions |= Collisions.VertexFaceSoftSoft;
            patch2.TotalMass = 150;
            SoftWorld.AddSoftBody(patch2);

            Cutting = true;
            HasTwoSidedFaces = true;
        }

        private void InitImpact()
        {
            SoftBody body = SoftBodyHelpers.CreateRope(_softBodyWorldInfo,
                Vector3.Zero, new Vector3(0, -1, 0), 0, 1);
            SoftWorld.AddSoftBody(body);
            body.Cfg.RigidContactHardness = 0.5f;
            PhysicsHelper.CreateBody(10, Matrix.Translation(0, 20, 0), new BoxShape(2), World);
        }

        private void InitAerodynamicFlyers()
        {
            const float scale = 2;
            const int segments = 6;
            const int count = 50;
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                SoftBody patch = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo,
                    new Vector3(-scale, 0, -scale), new Vector3(+scale, 0, -scale),
                    new Vector3(-scale, 0, +scale), new Vector3(+scale, 0, +scale),
                    segments, segments, 0, true);
                Material material = patch.AppendMaterial();
                material.Flags -= MaterialFlags.DebugDraw;
                patch.GenerateBendingConstraints(2, material);
                patch.Cfg.Lift = 0.004f;
                patch.Cfg.Drag = 0.0003f;
                patch.Cfg.AeroModel = AeroModel.VertexTwoSided;

                float yaw = (float)(0.1f * random.NextDouble() + Math.PI / 8);
                float pitch = (float)(random.NextDouble() - Math.PI / 7);
                float roll = (float)random.NextDouble();
                Matrix transform = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                Vector3 randomPosition = 75 * GetRandomVector(random) + new Vector3(-50, 15, 0);
                transform *= Matrix.Translation(randomPosition);

                patch.Transform(transform);
                patch.TotalMass = 0.1f;
                patch.AddForce(new Vector3(0, (float)random.NextDouble(), 0), 0);
                SoftWorld.AddSoftBody(patch);
            }

            HasTwoSidedFaces = true;
        }

        private static Vector3 GetRandomVector(Random random)
        {
            return new Vector3((float)random.NextDouble(),
                    (float)random.NextDouble(), (float)random.NextDouble());
        }

        private void InitAerodynamicSheets()
        {
            const float scale = 5;
            const int segments = 10;
            const int count = 5;
            const float gap = 0.5f;
            const int fixedCorners = 1 + 2;
            Vector3 position = new Vector3(-scale * segments, 0, 0);

            for (int i = 0; i < count; ++i)
            {
                SoftBody sheet = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo,
                    new Vector3(-scale, 0, -scale * 3),
                    new Vector3(+scale, 0, -scale * 3),
                    new Vector3(-scale, 0, +scale),
                    new Vector3(+scale, 0, +scale),
                    segments, segments * 3,
                    fixedCorners, true);

                sheet.CollisionShape.Margin = 0.5f;
                Material material = sheet.AppendMaterial();
                material.LinearStiffness = 0.0004f;
                material.Flags -= MaterialFlags.DebugDraw;
                sheet.GenerateBendingConstraints(2, material);

                sheet.Cfg.Lift = 0.05f;
                sheet.Cfg.Drag = 0.01f;
                //sheet.Cfg.Lift = 0.004f;
                //sheet.Cfg.Drag = 0.0003f;

                sheet.Cfg.PositionIterations = 2;
                sheet.Cfg.AeroModel = AeroModel.VertexTwoSidedLiftDrag;

                sheet.WindVelocity = new Vector3(4, -12.0f, -25.0f);

                position += new Vector3(scale * 2 + gap, 0, 0);
                Matrix transform = Matrix.RotationX((float)Math.PI / 2) * Matrix.Translation(position);
                sheet.Transform(transform);
                sheet.TotalMass = 2.0f;

                SoftBodyHelpers.ReoptimizeLinkOrder(sheet);

                SoftWorld.AddSoftBody(sheet);
            }

            HasTwoSidedFaces = true;
        }

        private void InitBoxFriction()
        {
            const float boxScale = 2;
            const float distance = boxScale + boxScale / 4;
            const int numBoxes = 20;

            for (int i = 0; i < numBoxes; i++)
            {
                Vector3 p = new Vector3(-numBoxes * distance / 2 + i * distance, boxScale, 40);
                SoftBody box = CreateSoftBox(p, new Vector3(boxScale));
                box.Cfg.DynamicFriction = 0.1f * ((i + 1) / (float)numBoxes);
                box.AddVelocity(new Vector3(0, 0, -10));
            }
        }

        private SoftBody CreateSoftBox(Vector3 position, Vector3 scale)
        {
            Vector3 halfScale = scale * 0.5f;
            Vector3[] vertices = new[] {
                halfScale * new Vector3(-1, -1, -1),
                halfScale * new Vector3(+1, -1, -1),
                halfScale * new Vector3(-1, +1, -1),
                halfScale * new Vector3(+1, +1, -1),
                halfScale * new Vector3(-1, -1, +1),
                halfScale * new Vector3(+1, -1, +1),
                halfScale * new Vector3(-1, +1, +1),
                halfScale * new Vector3(+1, +1, +1) };
            SoftBody box = SoftBodyHelpers.CreateFromConvexHull(_softBodyWorldInfo, vertices);
            box.GenerateBendingConstraints(2);
            box.Translate(position);
            SoftWorld.AddSoftBody(box);

            return box;
        }

        private void InitTorus()
        {
            SoftBody torus = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Torus.Vertices, Torus.Indices);
            torus.GenerateBendingConstraints(2);
            torus.Cfg.PositionIterations = 2;
            torus.RandomizeConstraints();
            Matrix transform = Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2, 0) *
                Matrix.Translation(0, 4, 0);
            torus.Transform(transform);
            torus.Scale(new Vector3(2));
            torus.SetTotalMass(50, true);
            SoftWorld.AddSoftBody(torus);

            Cutting = true;
        }

        private void InitTorusPoseMatching()
        {
            SoftBody torus = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Torus.Vertices, Torus.Indices);
            torus.Materials[0].LinearStiffness = 0.1f;
            torus.Cfg.PoseMatching = 0.05f;
            torus.RandomizeConstraints();
            Matrix transform = Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2, 0) *
                Matrix.Translation(0, 4, 0);
            torus.Transform(transform);
            torus.Scale(new Vector3(2));
            torus.SetTotalMass(50, true);
            torus.SetPose(false, true);
            SoftWorld.AddSoftBody(torus);
        }

        private void InitBunny()
        {
            SoftBody bunny = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Bunny.Vertices, Bunny.Indices);
            Material material = bunny.AppendMaterial();
            material.LinearStiffness = 0.5f;
            material.Flags -= MaterialFlags.DebugDraw;
            bunny.GenerateBendingConstraints(2, material);
            bunny.Cfg.PositionIterations = 2;
            bunny.Cfg.DynamicFriction = 0.5f;
            bunny.RandomizeConstraints();
            Matrix tranform = Matrix.RotationYawPitchRoll(0, (float)Math.PI / 2, 0) *
                Matrix.Translation(0, 4, 0);
            bunny.Transform(tranform);
            bunny.Scale(new Vector3(6, 6, 6));
            bunny.SetTotalMass(100, true);
            SoftWorld.AddSoftBody(bunny);

            Cutting = true;
        }

        private void InitBunnyPoseMatching()
        {
            SoftBody bunny = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Bunny.Vertices, Bunny.Indices);
            bunny.Cfg.DynamicFriction = 0.5f;
            bunny.Cfg.PoseMatching = 0.05f;
            bunny.Cfg.PositionIterations = 5;
            bunny.RandomizeConstraints();
            bunny.Scale(new Vector3(6, 6, 6));
            bunny.SetTotalMass(100, true);
            bunny.SetPose(false, true);
            SoftWorld.AddSoftBody(bunny);
        }

        private void InitPatchCutting()
        {
            const float scale = 6;
            const float height = 2;
            const int resolution = 16;
            const int fixedCorners = 1 + 2 + 4 + 8;

            SoftBody patch = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo,
                new Vector3(+scale, height, -scale),
                new Vector3(-scale, height, -scale),
                new Vector3(+scale, height, +scale),
                new Vector3(-scale, height, +scale),
                resolution, resolution, fixedCorners, true);
            SoftWorld.AddSoftBody(patch);
            patch.Cfg.PositionIterations = 1;

            Cutting = true;
            HasTwoSidedFaces = true;
        }

        private void InitClusterDeform()
        {
            SoftBody torus = CreateClusterTorus(Vector3.Zero, new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2));
            torus.GenerateClusters(8);
            torus.Cfg.DynamicFriction = 1;
        }

        private void InitClothStackCollide()
        {
            const float scale = 8;
            const int resolution = 17; // 9, 31
            const int fixedCorners = 1 + 2 + 4 + 8;
            SoftBody cloth = SoftBodyHelpers.CreatePatch(_softBodyWorldInfo,
                new Vector3(-scale, 0, -scale),
                new Vector3(+scale, 0, -scale),
                new Vector3(-scale, 0, +scale),
                new Vector3(+scale, 0, +scale),
                resolution, resolution, fixedCorners, true);
            Material material = cloth.AppendMaterial();
            material.LinearStiffness = 0.4f;
            material.Flags -= MaterialFlags.DebugDraw;
            cloth.Cfg.DynamicFriction = 1;
            cloth.Cfg.SoftRigidHardness = 1;
            cloth.Cfg.SoftRigidImpulseSplit = 0;
            cloth.Cfg.Collisions = Collisions.ClusterClusterSoftSoft | Collisions.ClusterConvexRigidSoft;
            cloth.GenerateBendingConstraints(2, material);

            cloth.CollisionShape.Margin = 0.05f;
            cloth.TotalMass = 50;

            // pass zero in generateClusters to create cluster for each tetrahedron or triangle
            cloth.GenerateClusters(0);
            //cloth.GenerateClusters(64);

            SoftWorld.AddSoftBody(cloth);

            CreateRigidBodyStack(10);

            HasTwoSidedFaces = true;
        }

        private void InitClusterTorusCollide()
        {
            for (int i = 0; i < 3; i++)
            {
                SoftBody torus = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Torus.Vertices, Torus.Indices);
                Material material = torus.AppendMaterial();
                material.Flags -= MaterialFlags.DebugDraw;
                torus.GenerateBendingConstraints(2, material);
                torus.Cfg.PositionIterations = 2;
                torus.Cfg.DynamicFriction = 1;
                torus.Cfg.SoftSoftHardness = 1;
                torus.Cfg.SoftSoftImpulseSplit = 0;
                torus.Cfg.SoftKineticHardness = 0.1f;
                torus.Cfg.SoftKineticImpulseSplit = 1;
                torus.Cfg.Collisions = Collisions.ClusterClusterSoftSoft | Collisions.ClusterConvexRigidSoft;
                torus.RandomizeConstraints();
                Matrix transform = Matrix.RotationYawPitchRoll((float)Math.PI / 2 * (i & 1), (float)Math.PI / 2 * (1 - (i & 1)), 0)
                    * Matrix.Translation(3 * i, 2, 0);
                torus.Transform(transform);
                torus.Scale(new Vector3(2));
                torus.SetTotalMass(50, true);
                torus.GenerateClusters(16);
                SoftWorld.AddSoftBody(torus);
            }
        }

        private void InitClusterSocket()
        {
            SoftBody torus = CreateClusterTorus(Vector3.Zero, new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2));
            RigidBody plate = CreateBigPlate(50, 8);
            torus.Cfg.DynamicFriction = 1;
            using (var linearJoint = new LinearJoint.Specs
            {
                Position = new Vector3(0, 5, 0)
            })
            {
                torus.AppendLinearJoint(linearJoint, new Body(plate));
            }
        }

        private void InitClusterHinge()
        {
            SoftBody torus = CreateClusterTorus(Vector3.Zero, new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2));
            RigidBody plate = CreateBigPlate(50, 8);
            torus.Cfg.DynamicFriction = 1;
            using (var angularJointSpecs = new AngularJoint.Specs
            {
                Axis = new Vector3(0, 0, 1)
            })
            {
                torus.AppendAngularJoint(angularJointSpecs, new Body(plate));
            }
        }

        private void InitClusterCombine()
        {
            var scale = new Vector3(2, 4, 2);
            SoftBody torus1 = CreateClusterTorus(new Vector3(0, 8, 0), new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2), scale);
            SoftBody torus2 = CreateClusterTorus(new Vector3(0, 8, 10), new Vector3((float)Math.PI / 2, 0, (float)Math.PI / 2), scale);
            var bodies = new SoftBody[] { torus1, torus2 };
            for (int i = 0; i < 2; i++)
            {
                bodies[i].Cfg.DynamicFriction = 1;
                bodies[i].Cfg.DynamicFriction = 0;
                bodies[i].Cfg.PositionIterations = 1;
                bodies[i].Clusters[0].Matching = 0.05f;
                bodies[i].Clusters[0].NodeDamping = 0.05f;
            }

            InitMotorControl();

            using (var angularJoint = new AngularJoint.Specs
            {
                Axis = new Vector3(0, 0, 1),
                Control = MotorControl
            })
            {
                torus1.AppendAngularJoint(angularJoint, torus2);
            }

            using (var linearJoint = new LinearJoint.Specs
            {
                Position = new Vector3(0, 8, 5)
            })
            {
                torus1.AppendLinearJoint(linearJoint, torus2);
            }
        }

        private SoftBody CreateClusterBunny(Vector3 position, Vector3 yawPitchRoll)
        {
            SoftBody body = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Bunny.Vertices, Bunny.Indices);
            Material material = body.AppendMaterial();
            material.LinearStiffness = 1;
            material.Flags -= MaterialFlags.DebugDraw;
            body.GenerateBendingConstraints(2, material);
            body.Cfg.PositionIterations = 2;
            body.Cfg.DynamicFriction = 1;
            body.Cfg.Collisions = Collisions.ClusterClusterSoftSoft | Collisions.ClusterConvexRigidSoft;
            body.RandomizeConstraints();
            Matrix m = Matrix.RotationYawPitchRoll(yawPitchRoll.X, yawPitchRoll.Y, yawPitchRoll.Z) * Matrix.Translation(position);
            body.Transform(m);
            body.Scale(new Vector3(8));
            body.SetTotalMass(150, true);
            body.GenerateClusters(1);
            SoftWorld.AddSoftBody(body);
            return body;
        }

        private SoftBody CreateClusterTorus(Vector3 position, Vector3 yawPitchRoll, Vector3 scale)
        {
            SoftBody body = SoftBodyHelpers.CreateFromTriMesh(_softBodyWorldInfo, Torus.Vertices, Torus.Indices);
            Material material = body.AppendMaterial();
            material.LinearStiffness = 1;
            material.Flags -= MaterialFlags.DebugDraw;
            body.GenerateBendingConstraints(2, material);
            body.Cfg.PositionIterations = 2;
            body.Cfg.Collisions = Collisions.ClusterClusterSoftSoft | Collisions.ClusterConvexRigidSoft;
            body.RandomizeConstraints();
            body.Scale(scale);
            Matrix m = Matrix.RotationYawPitchRoll(yawPitchRoll.X, yawPitchRoll.Y, yawPitchRoll.Z) * Matrix.Translation(position);
            body.Transform(m);
            body.SetTotalMass(50, true);
            body.GenerateClusters(64);
            SoftWorld.AddSoftBody(body);
            return body;
        }

        private void InitClusterCar()
        {
            const float widthFront = 8;
            const float widthRear = 9;
            const float length = 8;
            const float height = 4;
            Vector3[] wheelPositions = {
                new Vector3(+widthFront,-height,+length), // Front left
                new Vector3(-widthFront,-height,+length), // Front right
                new Vector3(+widthRear,-height,-length), // Rear left
                new Vector3(-widthRear,-height,-length), // Rear right
            };
            SoftBody bunny = CreateClusterBunny(Vector3.Zero, Vector3.Zero);
            SoftBody frontLeftWheel = CreateClusterTorus(wheelPositions[0], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 4, 2));
            SoftBody frontRightWheel = CreateClusterTorus(wheelPositions[1], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 4, 2));
            SoftBody rearLeftWheel = CreateClusterTorus(wheelPositions[2], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 5, 2));
            SoftBody rearRightWheel = CreateClusterTorus(wheelPositions[3], new Vector3(0, 0, (float)Math.PI / 2), new Vector3(2, 5, 2));
            SoftBody[] wheels = new[] { frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel };

            InitMotorControl();

            var origin = new Vector3(100, 80, 0);
            Quaternion orientation = Quaternion.RotationYawPitchRoll(-(float)Math.PI / 2, 0, 0);

            bunny.Rotate(orientation);
            bunny.Translate(origin);

            using (var linearJoint = new LinearJoint.Specs
            {
                ConstraintForceMixing = 1,
                ErrorReductionParameter = 1,
            })
            {
                for (int i = 0; i< wheels.Length; i++)
                {
                    var wheel = wheels[i];
                    linearJoint.Position = wheelPositions[i];
                    bunny.AppendLinearJoint(linearJoint, wheel);

                    wheel.Cfg.DynamicFriction = 1;
                    wheel.Cfg.PositionIterations = 1;
                    wheel.Clusters[0].Matching = 0.05f;
                    wheel.Clusters[0].NodeDamping = 0.05f;
                    wheel.Rotate(orientation);
                    wheel.Translate(origin);
                }
            }

            using (var angularJoint = new AngularJoint.Specs
            {
                ConstraintForceMixing = 1,
                ErrorReductionParameter = 1,
                Axis = new Vector3(1, 0, 0)
            })
            {
                angularJoint.Control = SteerControlFront;
                bunny.AppendAngularJoint(angularJoint, frontLeftWheel);
                bunny.AppendAngularJoint(angularJoint, frontRightWheel);

                angularJoint.Control = MotorControl;
                bunny.AppendAngularJoint(angularJoint, rearLeftWheel);
                bunny.AppendAngularJoint(angularJoint, rearRightWheel);
            }

            CreateLinearStair(20, new Vector3(0, -8, 0), new Vector3(3, 2, 40));
            CreateRigidBodyStack(50);

            // Autocam = true;
        }

        private void InitClusterRobot()
        {
            var basePosition = new Vector3(0, 25, 8);
            SoftBody ball1 = CreateClusterRobotBall(basePosition + new Vector3(-8, 0, 0));
            SoftBody ball2 = CreateClusterRobotBall(basePosition + new Vector3(+8, 0, 0));
            SoftBody ball3 = CreateClusterRobotBall(basePosition + new Vector3(0, 0, +8 * (float)Math.Sqrt(2)));
            Vector3 center = (ball1.ClusterCom(0) + ball2.ClusterCom(0) + ball3.ClusterCom(0)) / 3;

            var cylinderShape = new CylinderShape(new Vector3(8, 1, 8));
            RigidBody cylinder = PhysicsHelper.CreateBody(50, Matrix.Translation(center + new Vector3(0, 5, 0)), cylinderShape, World);
            Body body = new Body(cylinder);
            using (var linearJoint = new LinearJoint.Specs
            {
                ErrorReductionParameter = 0.5f
            })
            {
                linearJoint.Position = ball1.ClusterCom(0); ball1.AppendLinearJoint(linearJoint, body);
                linearJoint.Position = ball2.ClusterCom(0); ball2.AppendLinearJoint(linearJoint, body);
                linearJoint.Position = ball3.ClusterCom(0); ball3.AppendLinearJoint(linearJoint, body);
            }

            var slope = new BoxShape(20, 1, 40);
            PhysicsHelper.CreateBody(0, Matrix.RotationZ(-(float)Math.PI / 4), slope, World);
        }

        private SoftBody CreateClusterRobotBall(Vector3 position)
        {
            SoftBody ball = SoftBodyHelpers.CreateEllipsoid(_softBodyWorldInfo, position, new Vector3(3), 512);
            ball.Materials[0].LinearStiffness = 0.45f;
            ball.Cfg.VolumeConversation = 20;
            ball.SetTotalMass(50, true);
            ball.SetPose(true, false);
            ball.GenerateClusters(1);
            SoftWorld.AddSoftBody(ball);
            return ball;
        }

        private void InitClusterTorusStack()
        {
            for (int i = 0; i < 10; ++i)
            {
                SoftBody body = CreateClusterTorus(new Vector3(0, -9 + 8.25f * i, 0), Vector3.Zero);
                body.Cfg.DynamicFriction = 1;
            }
        }

        private void InitClusterStackMixed()
        {
            for (int i = 0; i < 5; i++)
            {
                CreateBigPlate(50, -9 + 8.5f * i);

                SoftBody torus = CreateClusterTorus(new Vector3(0, -4.75f + 8.5f * i, 0), Vector3.Zero);
                torus.Cfg.DynamicFriction = 1;
            }
        }

        private void InitTetraBunny()
        {
            SoftBody bunny = SoftBodyHelpers.CreateFromTetGenData(_softBodyWorldInfo,
                BunnyNodes.GetElements(), null, BunnyNodes.GetNodes(), false, true, true);
            SoftWorld.AddSoftBody(bunny);
            bunny.Rotate(Quaternion.RotationYawPitchRoll((float)Math.PI / 2, 0, 0));
            bunny.SetVolumeMass(150);
            bunny.Cfg.PositionIterations = 2;
            //bunny.Cfg.PositionIterations = 1;
            //bunny.CollisionShape.Margin = 0.01f;
            bunny.Cfg.Collisions = Collisions.ClusterClusterSoftSoft | Collisions.ClusterConvexRigidSoft; //| Collisions.ClusterSelf;

            // pass zero in generateClusters to create cluster for each tetrahedron or triangle
            bunny.GenerateClusters(0);
            //bunny.Materials[0].LinearStiffness = 0.2f;
            bunny.Cfg.DynamicFriction = 10;

            Cutting = false;
            HasTwoSidedFaces = true;
        }

        private void InitTetraCube()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            SoftBody cube = SoftBodyHelpers.CreateFromTetGenFile(_softBodyWorldInfo,
                Path.Combine(path, "data", "cube.ele"), null,
                Path.Combine(path, "data", "cube.node"), false, true, true);
            SoftWorld.AddSoftBody(cube);
            cube.Scale(new Vector3(4));
            cube.Translate(0, 5, 0);
            cube.SetVolumeMass(300);

            // fix one vertex
            //cube.SetMass(0, 0);
            //cube.SetMass(10, 0);
            //cube.SetMass(20, 0);
            cube.Cfg.PositionIterations = 1;
            //cube.GenerateClusters(128);
            cube.GenerateClusters(16);
            //cube.CollisionShape.Margin = 0.5f;

            cube.CollisionShape.Margin = 0.01f;
            cube.Cfg.Collisions = Collisions.ClusterClusterSoftSoft | Collisions.ClusterConvexRigidSoft;
            // | Collision.ClusterSelf;
            cube.Materials[0].LinearStiffness = 0.8f;

            Cutting = false;
            HasTwoSidedFaces = true;
        }

        private void InitBending()
        {
            const float scale = 4;
            Vector3[] positions = new[] {
                new Vector3(-scale, 0, -scale),
                new Vector3(+scale, 0, -scale),
                new Vector3(+scale, 0, +scale),
                new Vector3(-scale, 0, +scale)};
            var masses = new float[] { 0, 0, 0, 1 };
            var body = new SoftBody(_softBodyWorldInfo, positions.Length, positions, masses);
            body.AppendLink(0, 1);
            body.AppendLink(1, 2);
            body.AppendLink(2, 3);
            body.AppendLink(3, 0);
            body.AppendLink(0, 2);

            SoftWorld.AddSoftBody(body);
        }

        private RigidBody CreateBigPlate(float mass, float height)
        {
            RigidBody body = PhysicsHelper.CreateBody(mass, Matrix.Translation(0, height, 0.5f), new BoxShape(5, 1, 5), World);
            body.Friction = 1;
            return body;
        }

        private RigidBody CreateBigPlate()
        {
            return CreateBigPlate(15, 4);
        }

        private void CreateLinearStair(int count, Vector3 position, Vector3 stepHalfExtents)
        {
            var shape = new BoxShape(stepHalfExtents);
            for (int i = 0; i < count; i++)
            {
                RigidBody body = PhysicsHelper.CreateStaticBody(
                    Matrix.Translation(position + new Vector3(stepHalfExtents.X * 2 * i, stepHalfExtents.Y * 2 * i, 0)), shape, World);
                body.Friction = 1;
            }
        }

        private SoftBody CreateClusterTorus(Vector3 position, Vector3 yawPitchRoll)
        {
            return CreateClusterTorus(position, yawPitchRoll, new Vector3(2));
        }

        private SoftBody CreateSoftBoulder(Vector3 position, Vector3 scale, int numPoints)
        {
            var random = new Random();
            var points = new Vector3[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                points[i] = GetRandomVector(random) * scale;
            }

            SoftBody boulder = SoftBodyHelpers.CreateFromConvexHull(_softBodyWorldInfo, points);
            boulder.GenerateBendingConstraints(2);
            boulder.Translate(position);
            SoftWorld.AddSoftBody(boulder);

            return boulder;
        }

        private void CreateGear(Vector3 position, float speed)
        {
            Matrix startTransform = Matrix.Translation(position);
            var shape = new CompoundShape();
#if true
            shape.AddChildShape(Matrix.Identity, new BoxShape(5, 1, 6));
            shape.AddChildShape(Matrix.RotationZ((float)Math.PI), new BoxShape(5, 1, 6));
#else
            shape.AddChildShape(Matrix.Identity, new CylinderShapeZ(5, 1, 7));
            shape.AddChildShape(Matrix.RotationZ((float)Math.PI), new BoxShape(4, 1, 8));
#endif
            RigidBody body = PhysicsHelper.CreateBody(10, startTransform, shape, World);
            body.Friction = 1;
            var hinge = new HingeConstraint(body, Matrix.Identity);
            if (speed != 0) hinge.EnableAngularMotor(true, speed, 3);
            World.AddConstraint(hinge);
        }

        private void PickingPreTickCallback(DynamicsWorld world, float timeStep)
        {
            if (PickedNode != null)
            {
                Vector3 delta = PickedNodeGoal - PickedNode.Position;
                float maxDrag = 10;
                if (delta.LengthSquared > (maxDrag * maxDrag))
                {
                    delta.Normalize();
                    delta *= maxDrag;
                }
                PickedNode.Velocity += delta / timeStep;
            }
        }

        private void InitMotorControl()
        {
            MotorControl = new MotorControl
            {
                Goal = 0,
                MaxTorque = 0
            };
            SteerControlFront = new SteerControl(1, MotorControl);
            SteerControlRear = new SteerControl(-1, MotorControl);
        }

        private int PositiveMod(int value, int n)
        {
            return (n + (value % n)) % n;
        }
    }

    internal sealed class ImplicitSphere : ImplicitFn
    {
        private Vector3 _center;
        private float _sqRadius;

        public ImplicitSphere(ref Vector3 center, float radius)
        {
            _center = center;
            _sqRadius = radius * radius;
        }

        public override float Eval(ref Vector3 x)
        {
            return (x - _center).LengthSquared - _sqRadius;
        }
    };

    internal sealed class MotorControl : AngularJoint.IControl
    {
        public float Goal { get; set; } = 0;
        public float MaxTorque { get; set; } = 0;

        public override float Speed(AngularJoint joint, float current)
        {
            return current + Math.Min(MaxTorque, Math.Max(-MaxTorque, Goal - current));
        }
    }

    internal sealed class SteerControl : AngularJoint.IControl
    {
        private float _sign;
        private MotorControl _motorControl;

        public float Angle { get; set; }

        public SteerControl(float sign, MotorControl motorControl)
        {
            _sign = sign;
            _motorControl = motorControl;
        }

        public override void Prepare(AngularJoint joint)
        {
            joint.Refs[0] = new Vector3((float)Math.Cos(Angle * _sign), 0, (float)Math.Sin(Angle * _sign));
        }

        public override float Speed(AngularJoint joint, float current)
        {
            return _motorControl.Speed(joint, current);
        }
    }
}
