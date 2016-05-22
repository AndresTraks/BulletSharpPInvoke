using BulletSharp;
using SlimDX.Direct3D9;

namespace DemoFramework.SlimDX
{
    public class PhysicsDebugDraw : BufferedDebugDraw
    {
        Device device;

        public PhysicsDebugDraw(Device device)
        {
            this.device = device;
        }

        public void DrawDebugWorld(DynamicsWorld world)
        {
            world.DebugDrawWorld();
            if (LineIndex == 0)
                return;

            int lighting = device.GetRenderState(RenderState.Lighting);
            device.SetRenderState(RenderState.Lighting, false);
            device.SetTransform(TransformState.World, global::SlimDX.Matrix.Identity);
            device.VertexFormat = VertexFormat.Position | VertexFormat.Diffuse;

            device.DrawUserPrimitives(PrimitiveType.LineList, LineIndex / 2, Lines);
            LineIndex = 0;

            device.SetRenderState(RenderState.Lighting, lighting);
        }
    };
};
