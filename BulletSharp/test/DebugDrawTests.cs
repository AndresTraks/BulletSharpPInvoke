using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using NUnit.Framework;

namespace BulletSharpTest
{
    [TestFixture]
    [Category("DebugDraw")]
    public class DebugDrawTests
    {
        private const DrawFlags SoftDrawFlags = DrawFlags.Std | DrawFlags.Nodes | DrawFlags.Normals | DrawFlags.Contacts;
        private PhysicsContext _context;
        private CollisionShape _shape;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new PhysicsContext();
            _context.InitializeWorld();

            _shape = new BoxShape(2);
            RigidBody staticSphere = _context.AddStaticBody(_shape, Matrix.Translation(0, 0, 0));
            RigidBody dynamicSphere = _context.AddBody(_shape, Matrix.Translation(0, 1, 0), 1);
        }

        [Test]
        public void DebugDrawInterfaceTest()
        {
            DiscreteDynamicsWorld world = _context.World;

            world.StepSimulation(1.0f / 60.0f);

            using (var debugDrawer = new DebugDrawer())
            {
                world.DebugDrawer = debugDrawer;
                world.DebugDrawObject(Matrix.Identity, _shape, new Vector3(1, 0, 0));
                Assert.That(debugDrawer.DrawBoxCalled, Is.True);
                Assert.That(debugDrawer.DrawLineCalled, Is.True);
                world.DebugDrawer = null;
            }

            using (var debugDrawer = new DebugDrawer())
            {
                world.DebugDrawer = debugDrawer;
                world.DebugDrawWorld();
                Assert.That(debugDrawer.DrawBoxCalled, Is.True);
                Assert.That(debugDrawer.DrawLineCalled, Is.True);
                world.DebugDrawer = null;
            }
        }

        [Test]
        public void SoftBodyDebugDrawHelpersTest()
        {
            var softBodyWorldInfo = new SoftBodyWorldInfo();

            using (var debugDrawer = new DebugDrawer())
            {
                using (var softBody = SoftBodyHelpers.CreateEllipsoid(softBodyWorldInfo, Vector3.Zero, new Vector3(1), 4))
                {
                    SoftBodyHelpers.DrawInfos(softBody, debugDrawer, true, true, true);
                    Assert.That(debugDrawer.Draw3DTextCalled, Is.True);

                    SoftBodyHelpers.Draw(softBody, debugDrawer, SoftDrawFlags);
                    SoftBodyHelpers.DrawClusterTree(softBody, debugDrawer);
                    SoftBodyHelpers.DrawFaceTree(softBody, debugDrawer);
                    SoftBodyHelpers.DrawFrame(softBody, debugDrawer);
                    SoftBodyHelpers.DrawNodeTree(softBody, debugDrawer);
                }
                Assert.That(debugDrawer.DrawLineCalled, Is.True);
            }

            softBodyWorldInfo.Dispose();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
            _shape.Dispose();
        }

        private class DebugDrawer : DebugDraw
        {
            readonly DebugDrawModes _debugMode = DebugDrawModes.DrawWireframe | DebugDrawModes.DrawAabb;

            public override DebugDrawModes DebugMode
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

            public bool Draw3DTextCalled { get; private set; }
            public bool DrawBoxCalled { get; private set; }
            public bool DrawLineCalled { get; private set; }

            public override void Draw3DText(ref Vector3 location, string textString)
            {
                Draw3DTextCalled = true;
            }

            public override void DrawBox(ref Vector3 bbMin, ref Vector3 bbMax, ref Matrix trans, ref Vector3 color)
            {
                DrawBoxCalled = true;
                base.DrawBox(ref bbMin, ref bbMax, ref trans, ref color);
            }

            public override void DrawContactPoint(ref Vector3 pointOnB, ref Vector3 normalOnB, double distance, int lifeTime, ref Vector3 color)
            {
                throw new System.NotImplementedException();
            }

            public override void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 color)
            {
                DrawLineCalled = true;
            }

            public override void ReportErrorWarning(string warningString)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
