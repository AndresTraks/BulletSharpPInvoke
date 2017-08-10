using System;
using System.Windows.Forms;
using SlimDX.Direct3D9;

namespace DemoFramework.SlimDX
{
    /// <summary>
    /// Provides creation and management functionality for a Direct3D9 rendering device and related objects.
    /// </summary>
    public class DeviceContext9 : IDisposable
    {
        private Direct3D _direct3D;

        internal DeviceContext9(Form form, DeviceSettings9 settings)
        {
            if (form.Handle == IntPtr.Zero)
                throw new ArgumentException("Value must be a valid window handle.", "handle");
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            PresentParameters = new PresentParameters
            {
                BackBufferFormat = Format.X8R8G8B8,
                BackBufferCount = 1,
                BackBufferWidth = form.ClientSize.Width,
                BackBufferHeight = form.ClientSize.Height,
                Multisample = settings.MultisampleType,
                SwapEffect = SwapEffect.Discard,
                EnableAutoDepthStencil = true,
                AutoDepthStencilFormat = Format.D24S8,
                PresentFlags = PresentFlags.DiscardDepthStencil,
                PresentationInterval = PresentInterval.One,
                Windowed = settings.Windowed,
                DeviceWindowHandle = form.Handle
            };

            _direct3D = new Direct3D();
            Device = new Device(_direct3D, settings.AdapterOrdinal, DeviceType.Hardware, form.Handle, settings.CreationFlags, PresentParameters);
        }

        /// <summary>
        /// Gets the underlying Direct3D9 device.
        /// </summary>
        public Device Device { get; }

        public PresentParameters PresentParameters { get; }

        /// <summary>
        /// Performs object finalization.
        /// </summary>
        ~DeviceContext9()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of object resources.
        /// </summary>
        /// <param name="disposeManagedResources">If true, managed resources should be
        /// disposed of in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                Device.Dispose();
                _direct3D.Dispose();
            }
        }
    }
}
