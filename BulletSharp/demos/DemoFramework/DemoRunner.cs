using System;
using System.Reflection;
using System.Windows.Forms;

namespace DemoFramework
{
    public class DemoRunner
    {
        public static void Run<T>() where T : IDemoConfiguration, new()
        {
            Application.EnableVisualStyles();

            TryLoadBulletSharp();

            T configuration = new T();
            var demo = new Demo(configuration);

            GraphicsLibraryManager.Run(demo);
        }

        private static bool TryLoadBulletSharp()
        {
            try
            {
                Assembly.Load("BulletSharp");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error loading BulletSharp");
                return false;
            }
        }
    }
}
