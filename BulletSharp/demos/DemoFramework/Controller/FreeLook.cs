using System;
using System.Numerics;
using System.Windows.Forms;

namespace DemoFramework
{
    public sealed class FreeLook
    {
        private Input _input;
        private MouseController _mouseController;
        private bool _doUpdate;
        private Matrix4x4 _yToUpTransform, _upToYTransform;
        private Vector3 _eye, _target, _up;

        public FreeLook(Input input)
        {
            _input = input;
            _mouseController = new MouseController(input);
            Target = Vector3.UnitX;
            Up = Vector3.UnitY;
        }

        public Vector3 Eye
        {
            get { return _eye; }
            set
            {
                _eye = value;
                UpdateMouseController();
            }
        }

        public Vector3 Target
        {
            get { return _target; }
            set
            {
                _target = value;
                UpdateMouseController();
            }
        }

        public Vector3 Up
        {
            get { return _up; }
            set
            {
                _up = value;

                // MouseController uses UnitY as the up-vector,
                // create transforms for converting between UnitY-up and Up-up
                _yToUpTransform = Matrix4x4.CreateFromAxisAngle(Vector3.Cross(Vector3.UnitY, _up), Angle(_up, Vector3.UnitY));
                Matrix4x4.Invert(_yToUpTransform, out _upToYTransform);
                UpdateMouseController();
            }
        }

        public bool Update(float frameDelta)
        {
            if (!_mouseController.Update() && _input.KeysDown.Count == 0)
            {
                if (!_doUpdate)
                    return false;
                _doUpdate = false;
            }

            Vector3 direction = Vector3.Transform(-_mouseController.Vector, _yToUpTransform);

            if (_input.KeysDown.Count != 0)
            {
                Vector3 relDirection = frameDelta * direction;
                float flySpeed = _input.KeysDown.Contains(Keys.ShiftKey) ? 15 : 5;

                if (_input.KeysDown.Contains(Keys.W))
                {
                    _eye += flySpeed * relDirection;
                }
                if (_input.KeysDown.Contains(Keys.S))
                {
                    _eye -= flySpeed * relDirection;
                }

                if (_input.KeysDown.Contains(Keys.A))
                {
                    _eye += Vector3.Cross(relDirection, _up);
                }
                if (_input.KeysDown.Contains(Keys.D))
                {
                    _eye -= Vector3.Cross(relDirection, _up);
                }
            }
            _target = _eye + (_eye - _target).Length() * direction;

            return true;
        }

        private void UpdateMouseController()
        {
            Vector3 direction = Vector3.Normalize(_eye - _target);
            _mouseController.Vector = Vector3.Transform(direction, _upToYTransform);
            _doUpdate = true;
        }

        // vertices must be normalized
        private static float Angle(Vector3 v1, Vector3 v2)
        {
            return (float)Math.Acos(Vector3.Dot(v1, v2));
        }
    }
}
