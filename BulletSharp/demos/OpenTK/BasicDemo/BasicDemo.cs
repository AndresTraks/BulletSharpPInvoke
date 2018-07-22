using System.Drawing;
using BulletSharp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Vector3 = OpenTK.Vector3;

namespace BasicDemo
{
    class BasicDemo : GameWindow
    {
        private Physics _physics;
        private float _frameTime;
        private int _fps;

        public BasicDemo(GraphicsMode mode)
            : base(800, 600,
            mode, "BulletSharp OpenTK Demo")
        {
            VSync = VSyncMode.Off;
            _physics = new Physics();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(Color.MidnightBlue);

            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Lighting);
        }

        protected override void OnUnload(System.EventArgs e)
        {
            _physics.ExitPhysics();
            base.OnUnload(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            _physics.Update((float)e.Time);

            var keyboard = Keyboard.GetState();
            if (keyboard[Key.Escape] || keyboard[Key.Q])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _frameTime += (float)e.Time;
            _fps++;
            if (_frameTime >= 1)
            {
                _frameTime = 0;
                Title = $"BulletSharp OpenTK Demo, FPS = {_fps}";
                _fps = 0;
            }

            GL.Viewport(0, 0, Width, Height);

            float aspectRatio = Width / (float)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.1f, 100);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookAt = Matrix4.LookAt(new Vector3(10, 20, 30), Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            InitCubeBuffer();

            foreach (RigidBody body in _physics.World.CollisionObjectArray)
            {
                Matrix4 modelLookAt = Convert(body.MotionState.WorldTransform) * lookAt;
                GL.LoadMatrix(ref modelLookAt);

                if ("Ground".Equals(body.UserObject))
                {
                    DrawCube(Color.Green, 50.0f);
                    continue;
                }

                var color = body.ActivationState == ActivationState.ActiveTag
                    ? Color.Orange
                    : Color.Red;
                DrawCubeBuffer(color, 1);
            }

            UninitCubeBuffer();

            SwapBuffers();
        }

        private void DrawCube(Color color)
        {
            DrawCube(color, 1.0f);
        }

        private void DrawCube(Color color, float size)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(color);
            GL.Vertex3(-size, -size, -size);
            GL.Vertex3(-size, size, -size);
            GL.Vertex3(size, size, -size);
            GL.Vertex3(size, -size, -size);

            GL.Vertex3(-size, -size, -size);
            GL.Vertex3(size, -size, -size);
            GL.Vertex3(size, -size, size);
            GL.Vertex3(-size, -size, size);

            GL.Vertex3(-size, -size, -size);
            GL.Vertex3(-size, -size, size);
            GL.Vertex3(-size, size, size);
            GL.Vertex3(-size, size, -size);
            
            GL.Vertex3(-size, -size, size);
            GL.Vertex3(size, -size, size);
            GL.Vertex3(size, size, size);
            GL.Vertex3(-size, size, size);

            GL.Vertex3(-size, size, -size);
            GL.Vertex3(-size, size, size);
            GL.Vertex3(size, size, size);
            GL.Vertex3(size, size, -size);

            GL.Vertex3(size, -size, -size);
            GL.Vertex3(size, size, -size);
            GL.Vertex3(size, size, size);
            GL.Vertex3(size, -size, size);

            GL.End();
        }

        private readonly float[] _vertices = new float[] {
            1,1,1,  -1,1,1,  -1,-1,1,  1,-1,1,
            1,1,1,  1,-1,1,  1,-1,-1,  1,1,-1,
            1,1,1,  1,1,-1,  -1,1,-1,  -1,1,1,
            -1,1,1,  -1,1,-1,  -1,-1,-1,  -1,-1,1,
            -1,-1,-1,  1,-1,-1,  1,-1,1,  -1,-1,1,
            1,-1,-1,  -1,-1,-1,  -1,1,-1,  1,1,-1};

        private readonly float[] _normals = new float[] {
            0,0,1,  0,0,1,  0,0,1,  0,0,1,
            1,0,0,  1,0,0,  1,0,0,  1,0,0,
            0,1,0,  0,1,0,  0,1,0,  0,1,0,
            -1,0,0,  -1,0,0, -1,0,0,  -1,0,0,
            0,-1,0,  0,-1,0,  0,-1,0,  0,-1,0,
            0,0,-1,  0,0,-1,  0,0,-1,  0,0,-1};

        private readonly byte[] _indices = {
            0,1,2,3,
            4,5,6,7,
            8,9,10,11,
            12,13,14,15,
            16,17,18,19,
            20,21,22,23};

        private void InitCubeBuffer()
        {
            GL.EnableClientState(ArrayCap.NormalArray);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.NormalPointer(NormalPointerType.Float, 0, _normals);
            GL.VertexPointer(3, VertexPointerType.Float, 0, _vertices);
        }

        private void UninitCubeBuffer()
        {
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
        }

        private void DrawCubeBuffer(Color color, float size)
        {
            GL.Color3(color);
            GL.DrawElements(PrimitiveType.Quads, 24, DrawElementsType.UnsignedByte, _indices);
        }

        private static Matrix4 Convert(BulletSharp.Math.Matrix m)
        {
            return new Matrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44);
        }
    }
}
