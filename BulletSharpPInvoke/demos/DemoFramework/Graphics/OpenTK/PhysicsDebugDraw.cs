using BulletSharp;
using BulletSharp.Math;
using OpenTK.Graphics.OpenGL;

namespace DemoFramework.OpenTK
{
    public class PhysicsDebugDraw : BufferedDebugDraw
    {
        public void DrawDebugWorld(DynamicsWorld world)
        {
            world.DebugDrawWorld();

            if (LineIndex == 0)
                return;

            Vector3[] positionArray = new Vector3[LineIndex];
            int[] colorArray = new int[LineIndex];
            int i;
            for (i = 0; i < LineIndex; i++)
            {
                positionArray[i] = Lines[i].Position;
                colorArray[i] = Lines[i].Color;
            }
            LineIndex = 0;

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            GL.VertexPointer(3, VertexPointerType.Float, 0, positionArray);
            GL.ColorPointer(3, ColorPointerType.UnsignedByte, sizeof(int), colorArray);
            GL.DrawArrays(PrimitiveType.Lines, 0, positionArray.Length);

            GL.DisableClientState(ArrayCap.ColorArray);
            GL.DisableClientState(ArrayCap.VertexArray);
        }
    }
};
