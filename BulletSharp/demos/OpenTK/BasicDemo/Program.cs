using System;
using System.Reflection;
using System.Windows.Forms;
using OpenTK.Graphics;
using OpenTK;

namespace BasicDemo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Assembly.Load("BulletSharp");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Error loading BulletSharp!");
                return;
            }

            BasicDemo demo = new BasicDemo(GraphicsMode.Default);
            demo.Run(60);
        }
    }
}
