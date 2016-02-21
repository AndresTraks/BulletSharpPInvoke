using BulletSharp;
using BulletSharp.Math;
using System;
using System.Runtime.InteropServices;

namespace DemoFramework
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PositionColored
    {
        public static readonly int Stride = Vector3.SizeInBytes + sizeof(int);

        public Vector3 Position;
        public int Color;

        public PositionColored(Vector3 pos, int col)
        {
            Position = pos;
            Color = col;
        }

        public PositionColored(ref Vector3 pos, int col)
        {
            Position = pos;
            Color = col;
        }
    }

    public abstract class BufferedDebugDraw : DebugDraw
    {
        PositionColored[] _lines = new PositionColored[0];
        protected PositionColored[] Lines
        {
            get { return _lines; }
        }

        protected int LineIndex { get; set; }

        public override DebugDrawModes DebugMode { get; set; }

        int ColorToInt(ref Vector3 c)
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

            int line2Index = LineIndex + 1;
            if (line2Index >= Lines.Length)
            {
                Array.Resize(ref _lines, line2Index + 255);
            }

            _lines[LineIndex].Position = from;
            _lines[LineIndex].Color = intColor;
            _lines[line2Index].Position = to;
            _lines[line2Index].Color = intColor;

            LineIndex = line2Index + 1;
        }

        public override void ReportErrorWarning(string warningString)
        {
            System.Windows.Forms.MessageBox.Show(warningString);
        }
    }
};
