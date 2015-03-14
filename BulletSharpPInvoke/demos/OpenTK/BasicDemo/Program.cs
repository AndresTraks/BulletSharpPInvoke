using SiliconStudio.Core.Mathematics;
﻿using System;
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
                Assembly.Load("OpenTK");
            }
            catch
            {
                MessageBox.Show("OpenTK not found." +
                    "Please download it from http://www.opentk.com.", "Error!");
                return;
            }

            try
            {
                Assembly.Load("BulletSharp");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "BulletSharp Error!");
                return;
            }

            BasicDemo demo = new BasicDemo(GraphicsMode.Default);
            demo.Run(60);
        }
    }
}
