using BulletSharp;
using BulletSharp.Math;
using System;
using System.Collections.Generic;

namespace DemoFramework
{
    public abstract class BufferedDebugDraw : DebugDraw
    {
        protected List<PositionColored> lines = new List<PositionColored>();

        public override DebugDrawModes DebugMode { get; set; }

        protected virtual int ColorToInt(ref Vector3 c)
        {
            return ((int)(c.X * 255.0f)) + ((int)(c.Y * 255.0f) << 8) + ((int)(c.Z * 255.0f) << 16);
        }

        public override void Draw3dText(ref Vector3 location, string textString)
        {
            throw new NotImplementedException();
        }

        public override void DrawLine(ref Vector3 from, ref Vector3 to, ref Vector3 color)
        {
            int intColor = ColorToInt(ref color);
            lines.Add(new PositionColored(ref from, intColor));
            lines.Add(new PositionColored(ref to, intColor));
        }

        public override void ReportErrorWarning(string warningString)
        {
            System.Windows.Forms.MessageBox.Show(warningString);
        }
    }
};
