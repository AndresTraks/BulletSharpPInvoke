﻿using System;
using System.Drawing;
using System.Windows.Forms;
using BulletSharp;

namespace DemoFramework
{
    public abstract class Graphics : IDisposable
    {
        public Demo Demo { get; }
        public Form Form { get; protected set; }

        public virtual Control InputControl => Form;

        public virtual float FarPlane { get; set; }
        public float FieldOfView { get; protected set; }

        public virtual float AspectRatio
        {
            get
            {
                Size clientSize = Form.ClientSize;
                return (float)clientSize.Width / (float)clientSize.Height;
            }
        }

        public virtual bool IsFullScreen { get; set; }
        public virtual bool CullingEnabled { get; set; }

        public MeshFactory MeshFactory;

        public abstract DebugDraw GetPhysicsDebugDrawer();

        protected Graphics(Demo demo)
        {
            Demo = demo;
            FarPlane = 400.0f;
            FieldOfView = (float)Math.PI / 4;
        }

        public virtual void Initialize()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public abstract void Run();
        public abstract void UpdateView();

        public string WindowTitle
        {
            get { return Form.Text; }
            set { Form.Text = value; }
        }

        public virtual void SetInfoText(string text)
        {
        }
    }
}
