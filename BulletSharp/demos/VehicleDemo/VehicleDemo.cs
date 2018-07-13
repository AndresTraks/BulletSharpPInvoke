using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BulletSharp;
using BulletSharp.Math;
using DemoFramework;

namespace VehicleDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            DemoRunner.Run<VehicleDemo>();
        }
    }

    internal sealed class VehicleDemo : IDemoConfiguration, IUpdateReceiver
    {
        const double maxEngineForce = 2000.0f;
        const double maxBreakingForce = 100.0f;
        const double steeringIncrement = 1.0f;
        const double steeringClamp = 0.3f;

        public string WindowTitle => "BulletSharp - Vehicle Demo";

        public ISimulation CreateSimulation(Demo demo)
        {
            demo.FreeLook.Eye = new Vector3(35, 45, -55);
            demo.FreeLook.Target = Vector3.Zero;
            demo.DemoText =
                "Drive with arrow keys\n" +
                "Space - break";
            demo.Graphics.FarPlane = 600.0f;
            //demo.DebugDrawMode = DebugDrawModes.DrawAabb;
            demo.IsDebugDrawEnabled = true;
            return new VehicleDemoSimulation();
        }

        public void Update(Demo demo)
        {
            var simulation = demo.Simulation as VehicleDemoSimulation;

            var keysDown = demo.Input.KeysDown;

            if (keysDown.Contains(Keys.Left))
            {
                simulation.VehicleSteering += demo.FrameDelta * steeringIncrement;
                if (simulation.VehicleSteering > steeringClamp)
                    simulation.VehicleSteering = steeringClamp;
            }
            else if (simulation.VehicleSteering - double.Epsilon > 0)
            {
                simulation.VehicleSteering -= demo.FrameDelta * steeringIncrement;
            }

            if (keysDown.Contains(Keys.Right))
            {
                simulation.VehicleSteering -= demo.FrameDelta * steeringIncrement;
                if (simulation.VehicleSteering < -steeringClamp)
                    simulation.VehicleSteering = -steeringClamp;
            }
            else if (simulation.VehicleSteering + double.Epsilon < 0)
            {
                simulation.VehicleSteering += demo.FrameDelta * steeringIncrement;
            }

            if (keysDown.Contains(Keys.Up))
            {
                simulation.EngineForce = maxEngineForce;
            }

            if (keysDown.Contains(Keys.Down))
            {
                simulation.EngineForce = -maxEngineForce;
            }

            if (keysDown.Contains(Keys.Space))
            {
                simulation.EngineForce = 0;
                simulation.BreakingForce = maxBreakingForce;
            }

            if (demo.Input.KeysReleased.Contains(Keys.Space))
            {
                simulation.BreakingForce = 0;
            }

            simulation.OnUpdate();
        }
    }

    internal sealed class VehicleDemoSimulation : ISimulation
    {
        private const int rightIndex = 0;
        private const int upIndex = 1;
        private const int forwardIndex = 2;

        private const int maxProxies = 32766;
        private const int maxOverlap = 65535;

        private const double wheelRadius = 0.7f;
        private const double wheelWidth = 0.4f;
        private const double wheelFriction = 1000; //double.MaxValue
        private const double suspensionStiffness = 20.0f;
        private const double suspensionDamping = 2.3f;
        private const double suspensionCompression = 4.4f;
        private const double rollInfluence = 0.1f; //1.0f;

        private const double suspensionRestLength = 0.6f;
        private const double CUBE_HALF_EXTENTS = 1;

        private IntPtr _terrainData;

        //public RaycastVehicle vehicle;
        public CustomVehicle vehicle;

        public VehicleDemoSimulation()
        {
            CollisionConfiguration = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConfiguration);

            Vector3 worldMin = new Vector3(-10000, -10000, -10000);
            Vector3 worldMax = new Vector3(10000, 10000, 10000);
            Broadphase = new AxisSweep3(worldMin, worldMax);
            //Broadphase = new DbvtBroadphase();

            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConfiguration);
            World.SetInternalTickCallback(PickingPreTickCallback, this, true);

            CreateScene();
        }

        public CollisionConfiguration CollisionConfiguration { get; }
        public CollisionDispatcher Dispatcher { get; }
        public BroadphaseInterface Broadphase { get; }
        public DiscreteDynamicsWorld World { get; }

        // RaycastVehicle is the interface for the constraint that implements the raycast vehicle
        // notice that for higher-quality slow-moving vehicles, another approach might be better
        // implementing explicit hinged-wheel constraints with cylinder collision, rather then raycasts
        public double EngineForce { get; set; } = 0.0f;
        public double BreakingForce { get; set; } = 0.0f;

        public double VehicleSteering { get; set; } = 0.0f;

        public void OnUpdate()
        {
            vehicle.ApplyEngineForce(EngineForce, 2);
            vehicle.SetBrake(BreakingForce, 2);
            vehicle.ApplyEngineForce(EngineForce, 3);
            vehicle.SetBrake(BreakingForce, 3);

            vehicle.SetSteeringValue(VehicleSteering, 0);
            vehicle.SetSteeringValue(VehicleSteering, 1);
        }

        public void Dispose()
        {
            if (_terrainData != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_terrainData);
            }

            this.StandardCleanup();
        }

        private void PickingPreTickCallback(DynamicsWorld world, double timeStep)
        {
            EngineForce *= (1.0f - timeStep);

            vehicle.ApplyEngineForce(EngineForce, 2);
            vehicle.SetBrake(BreakingForce, 2);
            vehicle.ApplyEngineForce(EngineForce, 3);
            vehicle.SetBrake(BreakingForce, 3);

            vehicle.SetSteeringValue(VehicleSteering, 0);
            vehicle.SetSteeringValue(VehicleSteering, 1);
        }

        private void CreateScene()
        {
            CreateTrimeshGround();
            //CreateHeightfieldTerrainFromFile();
            //CreateHeightfieldTerrain();
        }

        private void CreateTrimeshGround()
        {
            const double scale = 20.0f;

            //create a triangle-mesh ground
            const int NumVertsX = 20;
            const int NumVertsY = 20;
            const int totalVerts = NumVertsX * NumVertsY;

            const int totalTriangles = 2 * (NumVertsX - 1) * (NumVertsY - 1);

            var vertexArray = new TriangleIndexVertexArray();
            var mesh = new IndexedMesh();
            mesh.Allocate(totalTriangles, totalVerts);
            mesh.NumTriangles = totalTriangles;
            mesh.NumVertices = totalVerts;
            mesh.TriangleIndexStride = 3 * sizeof(int);
            mesh.VertexStride = Vector3.SizeInBytes;
            using (var indicesStream = mesh.GetTriangleStream())
            {
                var indices = new BinaryWriter(indicesStream);
                for (int i = 0; i < NumVertsX - 1; i++)
                {
                    for (int j = 0; j < NumVertsY - 1; j++)
                    {
                        indices.Write(j * NumVertsX + i);
                        indices.Write(j * NumVertsX + i + 1);
                        indices.Write((j + 1) * NumVertsX + i + 1);

                        indices.Write(j * NumVertsX + i);
                        indices.Write((j + 1) * NumVertsX + i + 1);
                        indices.Write((j + 1) * NumVertsX + i);
                    }
                }
                indices.Dispose();
            }

            using (var vertexStream = mesh.GetVertexStream())
            {
                var vertices = new BinaryWriter(vertexStream);
                for (int i = 0; i < NumVertsX; i++)
                {
                    for (int j = 0; j < NumVertsY; j++)
                    {
                        const double waveLength = .2f;
                        double height = Math.Sin(i * waveLength) * Math.Cos(j * waveLength);

                        vertices.Write((double)i - NumVertsX * 0.5f);
                        vertices.Write(height);
                        vertices.Write((double)j - NumVertsY * 0.5f);
                    }
                }
                vertices.Dispose();
            }

            vertexArray.AddIndexedMesh(mesh);
            var groundShape = new BvhTriangleMeshShape(vertexArray, true);
            var groundScaled = new ScaledBvhTriangleMeshShape(groundShape, new Vector3(scale));

            RigidBody ground = PhysicsHelper.CreateStaticBody(Matrix.Identity, groundScaled, World);
            ground.UserObject = "Ground";

            Matrix vehicleTransform = Matrix.Translation(0, -2, 0);
            CreateVehicle(vehicleTransform);
        }

        private void CreateHeightfieldTerrainFromFile()
        {
            const double minHeight = 0;
            const double maxHeight = 10.0f;
            const double heightScale = maxHeight / 256.0f;
            const int width = 128, length = 128;
            const int dataLength = width * length * sizeof(byte);
            const PhyScalarType scalarType = PhyScalarType.Byte;

            var scale = new Vector3(5.0f, maxHeight, 5.0f);

            string heightfieldFile = Path.Combine("data", "heightfield128x128.raw");
            _terrainData = Marshal.AllocHGlobal(dataLength);

            using (var stream = new FileStream(heightfieldFile, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    while (stream.Position < stream.Length)
                    {
                        int offset = (int)stream.Position;
                        byte height = reader.ReadByte();
                        Marshal.WriteByte(_terrainData, offset, height);
                    }
                }
            }

            var shape = new HeightfieldTerrainShape(width, length,
                _terrainData, heightScale, minHeight, maxHeight, upIndex, scalarType, false);
            shape.SetUseDiamondSubdivision(true);
            shape.LocalScaling = new Vector3(scale.X, 1, scale.Z);

            Matrix transform = Matrix.Translation(-scale.X / 2, scale.Y / 2, -scale.Z / 2);

            RigidBody ground = PhysicsHelper.CreateStaticBody(transform, shape, World);
            ground.UserObject = "Ground";

            Matrix vehicleTransform = Matrix.Translation(new Vector3(20, 3, -3));
            CreateVehicle(vehicleTransform);
        }

        private void CreateHeightfieldTerrain()
        {
            const double minHeight = 0;
            const double maxHeight = 10.0f;
            const double heightScale = maxHeight / 256.0f;
            const int width = 64, length = 64;
            const int dataLength = width * length * sizeof(double);
            const PhyScalarType scalarType = PhyScalarType.Double;

            var scale = new Vector3(15.0f, maxHeight, 15.0f);

            _terrainData = Marshal.AllocHGlobal(dataLength);
            var terrain = new byte[dataLength];

            using (var file = new MemoryStream(terrain))
            {
                using (var writer = new BinaryWriter(file))
                {
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            writer.Write((maxHeight / 2) + 4 * Math.Sin(j * 0.5f) * Math.Cos(i));
                        }
                    }
                }
            }

            Marshal.Copy(terrain, 0, _terrainData, terrain.Length);

            var groundShape = new HeightfieldTerrainShape(width, length,
                _terrainData, heightScale, minHeight, maxHeight, upIndex, scalarType, false);
            groundShape.SetUseDiamondSubdivision(true);
            groundShape.LocalScaling = new Vector3(scale.X, 1, scale.Z);

            Matrix transform = Matrix.Translation(-scale.X / 2, scale.Y / 2, -scale.Z / 2);

            RigidBody ground = PhysicsHelper.CreateStaticBody(transform, groundShape, World);
            ground.UserObject = "Ground";

            Matrix vehicleTransform = Matrix.Translation(new Vector3(20, 3, -3));
            CreateVehicle(vehicleTransform);
        }

        private void CreateVehicle(Matrix transform)
        {
            var chassisShape = new BoxShape(1.0f, 0.5f, 2.0f);

            var compound = new CompoundShape();

            //localTrans effectively shifts the center of mass with respect to the chassis
            Matrix localTrans = Matrix.Translation(Vector3.UnitY);
            compound.AddChildShape(localTrans, chassisShape);
            RigidBody carChassis = PhysicsHelper.CreateBody(800, Matrix.Identity, compound, World);
            carChassis.UserObject = "Chassis";
            //carChassis.SetDamping(0.2f, 0.2f);

            var tuning = new VehicleTuning();
            var vehicleRayCaster = new DefaultVehicleRaycaster(World);
            //vehicle = new RaycastVehicle(tuning, carChassis, vehicleRayCaster);
            vehicle = new CustomVehicle(tuning, carChassis, vehicleRayCaster);

            carChassis.ActivationState = ActivationState.DisableDeactivation;
            World.AddAction(vehicle);


            const double connectionHeight = 1.2f;

            // choose coordinate system
            vehicle.SetCoordinateSystem(rightIndex, upIndex, forwardIndex);

            Vector3 wheelDirection = Vector3.Zero;
            Vector3 wheelAxle = Vector3.Zero;

            wheelDirection[upIndex] = -1;
            wheelAxle[rightIndex] = -1;

            bool isFrontWheel = true;
            var connectionPoint = new Vector3(CUBE_HALF_EXTENTS - (0.3f * wheelWidth), connectionHeight, 2 * CUBE_HALF_EXTENTS - wheelRadius);
            vehicle.AddWheel(connectionPoint, wheelDirection, wheelAxle, suspensionRestLength, wheelRadius, tuning, isFrontWheel);

            connectionPoint = new Vector3(-CUBE_HALF_EXTENTS + (0.3f * wheelWidth), connectionHeight, 2 * CUBE_HALF_EXTENTS - wheelRadius);
            vehicle.AddWheel(connectionPoint, wheelDirection, wheelAxle, suspensionRestLength, wheelRadius, tuning, isFrontWheel);

            isFrontWheel = false;
            connectionPoint = new Vector3(-CUBE_HALF_EXTENTS + (0.3f * wheelWidth), connectionHeight, -2 * CUBE_HALF_EXTENTS + wheelRadius);
            vehicle.AddWheel(connectionPoint, wheelDirection, wheelAxle, suspensionRestLength, wheelRadius, tuning, isFrontWheel);

            connectionPoint = new Vector3(CUBE_HALF_EXTENTS - (0.3f * wheelWidth), connectionHeight, -2 * CUBE_HALF_EXTENTS + wheelRadius);
            vehicle.AddWheel(connectionPoint, wheelDirection, wheelAxle, suspensionRestLength, wheelRadius, tuning, isFrontWheel);


            for (int i = 0; i < vehicle.NumWheels; i++)
            {
                WheelInfo wheel = vehicle.GetWheelInfo(i);
                wheel.SuspensionStiffness = suspensionStiffness;
                wheel.WheelsDampingRelaxation = suspensionDamping;
                wheel.WheelsDampingCompression = suspensionCompression;
                wheel.FrictionSlip = wheelFriction;
                wheel.RollInfluence = rollInfluence;
            }

            vehicle.RigidBody.WorldTransform = transform;
        }
    }
}
