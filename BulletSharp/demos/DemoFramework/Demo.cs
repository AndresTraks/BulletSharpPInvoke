using BulletSharp;
using BulletSharp.Math;
using System;
using System.Drawing;
using System.Globalization;
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
        RigidBody pickedBody;
        TypedConstraint pickConstraint;
        float oldPickingDist;
        bool prevCanSleep;
        MultiBodyPoint2Point pickingMultiBodyPoint2Point;

        private BoxShooter _boxShooter;

        // Debug drawing
        bool _isDebugDrawEnabled;
        DebugDrawModes _debugDrawMode = DebugDrawModes.DrawWireframe;
        IDebugDraw _debugDrawer;

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
            if (_debugDrawer != null)
            {
                Simulation.World.DebugDrawer = _debugDrawer;
            }
        }

        private void UninitializePhysics()
        {
            RemovePickingConstraint();
            Simulation.Dispose();
            _boxShooter.Dispose();
        }

        public void Run()
        {
            using (Graphics = GraphicsLibraryManager.GetGraphics(this))
            {
                Graphics.Initialize();
                Graphics.CullingEnabled = true;

                Input = new Input(Graphics.Form);
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
            HandleMouseInput();

            Clock.StartPhysics();
            int substepsPassed = Simulation.World.StepSimulation(FrameDelta);
            Clock.StopPhysics(substepsPassed);

            if (FreeLook.Update(FrameDelta))
                Graphics.UpdateView();

            Input.ClearKeyCache();
        }

        private void HandleMouseInput()
        {
            if (Input.MousePressed != MouseButtons.None)
            {
                Vector3 rayTo = GetRayTo(Input.MousePoint, FreeLook.Eye, FreeLook.Target, Graphics.FieldOfView);

                if (Input.MousePressed == MouseButtons.Right)
                {
                    Vector3 rayFrom = FreeLook.Eye;

                    var rayCallback = new ClosestRayResultCallback(ref rayFrom, ref rayTo);
                    Simulation.World.RayTestRef(ref rayFrom, ref rayTo, rayCallback);
                    if (rayCallback.HasHit)
                    {
                        Vector3 pickPos = rayCallback.HitPointWorld;
                        RigidBody body = rayCallback.CollisionObject as RigidBody;
                        if (body != null)
                        {
                            if (!(body.IsStaticObject || body.IsKinematicObject))
                            {
                                pickedBody = body;
                                pickedBody.ActivationState = ActivationState.DisableDeactivation;

                                Vector3 localPivot = Vector3.TransformCoordinate(pickPos, Matrix.Invert(body.CenterOfMassTransform));

                                if (Input.KeysDown.Contains(Keys.ShiftKey))
                                {
                                    Generic6DofConstraint dof6 = new Generic6DofConstraint(body, Matrix.Translation(localPivot), false)
                                    {
                                        LinearLowerLimit = Vector3.Zero,
                                        LinearUpperLimit = Vector3.Zero,
                                        AngularLowerLimit = Vector3.Zero,
                                        AngularUpperLimit = Vector3.Zero
                                    };

                                    Simulation.World.AddConstraint(dof6);
                                    pickConstraint = dof6;

                                    dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 0);
                                    dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 1);
                                    dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 2);
                                    dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 3);
                                    dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 4);
                                    dof6.SetParam(ConstraintParam.StopCfm, 0.8f, 5);

                                    dof6.SetParam(ConstraintParam.StopErp, 0.1f, 0);
                                    dof6.SetParam(ConstraintParam.StopErp, 0.1f, 1);
                                    dof6.SetParam(ConstraintParam.StopErp, 0.1f, 2);
                                    dof6.SetParam(ConstraintParam.StopErp, 0.1f, 3);
                                    dof6.SetParam(ConstraintParam.StopErp, 0.1f, 4);
                                    dof6.SetParam(ConstraintParam.StopErp, 0.1f, 5);
                                }
                                else
                                {
                                    Point2PointConstraint p2p = new Point2PointConstraint(body, localPivot);
                                    Simulation.World.AddConstraint(p2p);
                                    pickConstraint = p2p;
                                    p2p.Setting.ImpulseClamp = 30;
                                    //very weak constraint for picking
                                    p2p.Setting.Tau = 0.001f;
                                    /*
                                    p2p.SetParam(ConstraintParams.Cfm, 0.8f, 0);
                                    p2p.SetParam(ConstraintParams.Cfm, 0.8f, 1);
                                    p2p.SetParam(ConstraintParams.Cfm, 0.8f, 2);
                                    p2p.SetParam(ConstraintParams.Erp, 0.1f, 0);
                                    p2p.SetParam(ConstraintParams.Erp, 0.1f, 1);
                                    p2p.SetParam(ConstraintParams.Erp, 0.1f, 2);
                                    */
                                }
                            }
                        }
                        else
                        {
                            var multiCol = rayCallback.CollisionObject as MultiBodyLinkCollider;
                            if (multiCol != null && multiCol.MultiBody != null)
                            {
                                MultiBody mb = multiCol.MultiBody;

                                prevCanSleep = mb.CanSleep;
                                mb.CanSleep = false;
                                Vector3 pivotInA = mb.WorldPosToLocal(multiCol.Link, pickPos);

                                var p2p = new MultiBodyPoint2Point(mb, multiCol.Link, null, pivotInA, pickPos);
                                p2p.MaxAppliedImpulse = 2;

                                (Simulation.World as MultiBodyDynamicsWorld).AddMultiBodyConstraint(p2p);
                                pickingMultiBodyPoint2Point = p2p;
                            }
                        }
                        oldPickingDist = (pickPos - rayFrom).Length;
                    }
                    rayCallback.Dispose();
                }
            }
            else if (Input.MouseReleased == MouseButtons.Right)
            {
                RemovePickingConstraint();
            }

            // Mouse movement
            if (Input.MouseDown == MouseButtons.Right)
            {
                MovePickedBody();
            }
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
                        "Right click\t\tPick up an object using a Point2PointConstraint\n" +
                        "Shift + Right click\tPick up an object using a fixed Generic6DofConstraint\n" +
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
                case Keys.F8:
                    Input.ClearKeyCache();
                    GraphicsLibraryManager.ExitWithReload = true;
                    Graphics.Form.Close();
                    break;
                case Keys.F11:
                    Graphics.IsFullScreen = !Graphics.IsFullScreen;
                    break;
                case (Keys.Control | Keys.F):
                    const int maxSerializeBufferSize = 1024 * 1024 * 5;
                    using (var serializer = new DefaultSerializer(maxSerializeBufferSize))
                    {
                        Simulation.World.Serialize(serializer);
                        var dataBytes = new byte[serializer.CurrentBufferSize];
                        Marshal.Copy(serializer.BufferPointer, dataBytes, 0,
                            dataBytes.Length);
                        using (var file = new System.IO.FileStream("world.bullet", System.IO.FileMode.Create))
                        {
                            file.Write(dataBytes, 0, dataBytes.Length);
                        }
                    }
                    break;
                case Keys.G:
                    //shadowsEnabled = !shadowsEnabled;
                    break;
                case Keys.Space:
                    Vector3 destination = GetRayTo(Input.MousePoint, FreeLook.Eye, FreeLook.Target, Graphics.FieldOfView);
                    _boxShooter.Shoot(FreeLook.Eye, destination);
                    break;
                case Keys.Return:
                    ResetScene();
                    break;
            }
        }

        private void MovePickedBody()
        {
            if (pickConstraint != null)
            {
                Vector3 rayFrom = FreeLook.Eye;
                Vector3 newRayTo = GetRayTo(Input.MousePoint, rayFrom, FreeLook.Target, Graphics.FieldOfView);

                //keep it at the same picking distance
                Vector3 dir = newRayTo - rayFrom;
                dir.Normalize();
                dir *= oldPickingDist;

                if (pickConstraint.ConstraintType == TypedConstraintType.D6)
                {
                    var pickCon = pickConstraint as Generic6DofConstraint;

                    //keep it at the same picking distance
                    Matrix tempFrameOffsetA = pickCon.FrameOffsetA;
                    tempFrameOffsetA.Origin = rayFrom + dir;
                    pickCon.SetFrames(tempFrameOffsetA, pickCon.FrameOffsetB);
                }
                else
                {
                    var pickCon = pickConstraint as Point2PointConstraint;

                    //keep it at the same picking distance
                    pickCon.PivotInB = rayFrom + dir;
                }
            }
            else if (pickingMultiBodyPoint2Point != null)
            {
                Vector3 rayFrom = FreeLook.Eye;
                Vector3 newRayTo = GetRayTo(Input.MousePoint, FreeLook.Eye, FreeLook.Target, Graphics.FieldOfView);

                Vector3 dir = (newRayTo - rayFrom);
                dir.Normalize();
                dir *= oldPickingDist;
                pickingMultiBodyPoint2Point.PivotInB = rayFrom + dir;
            }
        }

        private void RemovePickingConstraint()
        {
            if (pickConstraint != null)
            {
                Simulation.World.RemoveConstraint(pickConstraint);
                pickConstraint.Dispose();
                pickConstraint = null;
                pickedBody.ForceActivationState(ActivationState.ActiveTag);
                pickedBody.DeactivationTime = 0;
                pickedBody = null;
            }

            if (pickingMultiBodyPoint2Point != null)
            {
                pickingMultiBodyPoint2Point.MultiBodyA.CanSleep = prevCanSleep;
                (Simulation.World as MultiBodyDynamicsWorld).RemoveMultiBodyConstraint(pickingMultiBodyPoint2Point);
                pickingMultiBodyPoint2Point.Dispose();
                pickingMultiBodyPoint2Point = null;
            }
        }

        public Vector3 GetRayTo(Point point, Vector3 eye, Vector3 target, float fieldOfView)
        {
            Vector3 rayForward = target - eye;
            rayForward.Normalize();
            const float farPlane = 10000.0f;
            rayForward *= farPlane;

            Vector3 horizontal = Vector3.Cross(rayForward, FreeLook.Up);
            horizontal.Normalize();
            Vector3 vertical = Vector3.Cross(horizontal, rayForward);
            vertical.Normalize();

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
