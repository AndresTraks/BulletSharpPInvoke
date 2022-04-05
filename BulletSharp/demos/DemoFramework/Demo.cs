﻿using BulletSharp;
using DemoFramework.DebugInfo;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DemoFramework
{
    public sealed class Demo
    {
        private IDemoConfiguration _configuration;
        private IUpdateReceiver _updateReceiver;

        public ISimulation Simulation { get; private set; }

        public Graphics Graphics { get; private set; }
        public FreeLook FreeLook { get; private set; }
        public Input Input { get; private set; }

        // Info text
        CultureInfo _culture = CultureInfo.InvariantCulture;
        string _demoText = "";
        public string DemoText
        {
            get { return _demoText; }
            set
            {
                _demoText = value;
                SetInfoText();
            }
        }

        // Frame counting
        public Clock Clock { get; private set; }

        public float FrameDelta { get; private set; }
        public float FramesPerSecond { get; private set; }
        float _frameAccumulator;

        // Physics
        private BoxShooter _boxShooter;
        private BodyPicker _bodyPicker;


        // Debug drawing
        bool _isDebugDrawEnabled;
        DebugDrawModes _debugDrawMode = DebugDrawModes.DrawWireframe;
        DebugDraw _debugDrawer;

        public DebugDrawModes DebugDrawMode
        {
            get { return _debugDrawMode; }
            set
            {
                _debugDrawMode = value;
                if (_debugDrawer != null)
                {
                    _debugDrawer.DebugMode = value;
                }
            }
        }

        public bool IsDebugDrawEnabled
        {
            get { return _isDebugDrawEnabled; }
            set
            {
                _isDebugDrawEnabled = value;
                if (value)
                {
                    InitializeDebugDrawer();
                }
                else
                {
                    UninitializeDebugDrawer();
                }
            }
        }

        public Demo(IDemoConfiguration configuration = null)
        {
            _configuration = configuration;
            _updateReceiver = configuration as IUpdateReceiver;
            Clock = new Clock();
        }

        private void VerifySimulation()
        {
            if (Simulation == null)
            {
                throw new NullReferenceException("Simulation not initialized");
            }
            if (Simulation.CollisionConfiguration == null)
            {
                throw new NullReferenceException("CollisionConfiguration not initialized");
            }
            if (Simulation.Broadphase == null)
            {
                throw new NullReferenceException("Broadphase not initialized");
            }
            if (Simulation.Dispatcher == null)
            {
                throw new NullReferenceException("Dispatcher not initialized");
            }
            if (Simulation.World == null)
            {
                throw new NullReferenceException("DynamicsWorld not initialized");
            }
        }

        private void InitializeDebugDrawer()
        {
            if (_debugDrawer == null)
            {
                _debugDrawer = Graphics.GetPhysicsDebugDrawer();
                _debugDrawer.DebugMode = DebugDrawMode;
            }
            if (Simulation != null)
            {
                Simulation.World.DebugDrawer = _debugDrawer;
            }
        }

        private void UninitializeDebugDrawer()
        {
            if (_debugDrawer != null)
            {
                Simulation.World.DebugDrawer = null;
                if (_debugDrawer is IDisposable)
                {
                    (_debugDrawer as IDisposable).Dispose();
                }
                _debugDrawer = null;
            }
        }

        private void InitializePhysics()
        {
            Simulation = _configuration.CreateSimulation(this);
            VerifySimulation();
            _boxShooter = new BoxShooter(Simulation.World);
            _bodyPicker = new BodyPicker(this);
            if (_debugDrawer != null)
            {
                Simulation.World.DebugDrawer = _debugDrawer;
            }
        }

        private void UninitializePhysics()
        {
            _bodyPicker.RemovePickingConstraint();
            Simulation.Dispose();
            _boxShooter.Dispose();

            if (BulletObjectTracker.Current != null)
            {
                if (BulletObjectTracker.Current.GetUserOwnedObjects().Count != 0)
                {
                    throw new Exception("Bullet has active objects that were not disposed.");
                }
            }
        }

        public void Run()
        {
            using (Graphics = GraphicsLibraryManager.GetGraphics(this))
            {
                if (Graphics == null)
                {
                    return;
                }

                Graphics.Initialize();
                Graphics.CullingEnabled = true;

                Input = new Input(Graphics.Form, Graphics.InputControl);
                FreeLook = new FreeLook(Input);

                InitializePhysics();
                if (_isDebugDrawEnabled)
                {
                    InitializeDebugDrawer();
                }

                Graphics.UpdateView();
                SetInfoText();

                Graphics.Run();
            }
            Graphics = null;

            UninitializeDebugDrawer();
            UninitializePhysics();
        }

        public void ResetScene()
        {
            UninitializePhysics();
            InitializePhysics();
        }

        private void SetInfoText()
        {
            Graphics.SetInfoText(
                string.Format("Physics: {0} ms\n" +
                "Render: {1} ms\n" +
                "{2} FPS\n" +
                "F1 - Help\n" +
                _demoText,
                Clock.PhysicsAverage.ToString("0.000", _culture),
                Clock.RenderAverage.ToString("0.000", _culture),
                Clock.FrameCount)
            );
        }

        public void OnUpdate()
        {
            FrameDelta = Clock.GetFrameDelta();
            _frameAccumulator += FrameDelta;
            if (_frameAccumulator >= 1.0f)
            {
                FramesPerSecond = Clock.FrameCount / _frameAccumulator;
                SetInfoText();

                _frameAccumulator = 0.0f;
                Clock.Reset();
            }

            if (_updateReceiver != null)
            {
                _updateReceiver.Update(this);
            }
            HandleKeyboardInput();
            _bodyPicker.Update();

            Clock.StartPhysics();
            int substepsPassed = Simulation.World.StepSimulation(FrameDelta);
            Clock.StopPhysics(substepsPassed);

            if (FreeLook.Update(FrameDelta))
                Graphics.UpdateView();

            Input.ClearKeyCache();
        }

        private void HandleKeyboardInput()
        {
            if (Input.KeysPressed.Count == 0)
            {
                return;
            }

            switch (Input.KeysPressed[0])
            {
                case Keys.Escape:
                case Keys.Q:
                    Graphics.Form.Close();
                    return;
                case Keys.F1:
                    MessageBox.Show(
                        "WASD + Shift\tMove\n" +
                        "Left click\t\tPoint camera\n" +
                        "Right click\tPick up an object using a Point2PointConstraint\n" +
                        "Shift + Right click\tPick up an object using a fixed constraint\n" +
                        "Space\t\tShoot box\n" +
                        "Return\t\tReset\n" +
                        "F11\t\tFullscreen\n" +
                        "Q\t\tQuit\n\n",
                        "Help");
                    // Key release won't be captured
                    Input.KeysDown.Remove(Keys.F1);
                    break;
                case Keys.F3:
                    IsDebugDrawEnabled = !IsDebugDrawEnabled;
                    break;
                case Keys.F5:
                    var debugForm = new DebugInfoForm(this);
                    debugForm.Show();
                    break;
                case Keys.F8:
                    Input.ClearKeyCache();
                    GraphicsLibraryManager.ExitWithReload = true;
                    Graphics.Form.Close();
                    break;
                case Keys.F11:
                    Graphics.IsFullScreen = !Graphics.IsFullScreen;
                    break;
                case Keys.Control | Keys.F:
                    const int maxSerializeBufferSize = 1024 * 1024 * 5;
                    using (var serializer = new DefaultSerializer(maxSerializeBufferSize))
                    {
                        Simulation.World.Serialize(serializer);
                        var dataBytes = new byte[serializer.CurrentBufferSize];
                        Marshal.Copy(serializer.BufferPointer, dataBytes, 0,
                            dataBytes.Length);
                        using (var file = new FileStream("world.bullet", FileMode.Create))
                        {
                            file.Write(dataBytes, 0, dataBytes.Length);
                        }
                    }
                    break;
                case Keys.G:
                    //shadowsEnabled = !shadowsEnabled;
                    break;
                case Keys.Space:
                    _boxShooter.Shoot(FreeLook.Eye, GetCameraRayTo());
                    break;
                case Keys.Return:
                    ResetScene();
                    break;
            }
        }
        
        public Vector3 GetCameraRayTo()
        {
            return GetRayTo(Input.MousePoint, FreeLook.Eye, FreeLook.Target, Graphics.FieldOfView);
        }

        public Vector3 GetRayTo(Point point, Vector3 eye, Vector3 target, float fieldOfView)
        {
            Vector3 rayForward = Vector3.Normalize(target - eye);
            const float farPlane = 10000.0f;
            rayForward *= farPlane;

            Vector3 horizontal = Vector3.Normalize(Vector3.Cross(rayForward, FreeLook.Up));
            Vector3 vertical = Vector3.Normalize(Vector3.Cross(horizontal, rayForward));

            float tanFov = (float)Math.Tan(fieldOfView / 2);
            horizontal *= 2.0f * farPlane * tanFov;
            vertical *= 2.0f * farPlane * tanFov;

            Size clientSize = Graphics.Form.ClientSize;
            if (clientSize.Width > clientSize.Height)
            {
                float aspect = (float)clientSize.Width / (float)clientSize.Height;
                horizontal *= aspect;
            }
            else
            {
                float aspect = (float)clientSize.Height / (float)clientSize.Width;
                vertical *= aspect;
            }

            Vector3 rayToCenter = eye + rayForward;
            Vector3 dHor = horizontal / (float)clientSize.Width;
            Vector3 dVert = vertical / (float)clientSize.Height;

            Vector3 rayTo = rayToCenter - 0.5f * horizontal + 0.5f * vertical;
            rayTo += (clientSize.Width - point.X) * dHor;
            rayTo -= point.Y * dVert;
            return rayTo;
        }
    }
}
