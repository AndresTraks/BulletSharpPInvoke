using OpenTK.WinForms;
using System;
using System.Windows.Forms;

namespace DemoFramework.OpenTK
{
    public partial class GLForm : Form
    {
        private OpenTKGraphics _graphics;
        private bool _hasInitialFocus = false;

        public GLForm(OpenTKGraphics graphics)
        {
            _graphics = graphics;

            InitializeComponent();

            glControl.Disposed += new EventHandler(glControl_Disposed);
            glControl.PreviewKeyDown += new PreviewKeyDownEventHandler(glControl_PreviewKeyDown);
        }

        public GLControl GLControl => glControl;

        private void glControl_Load(object? sender, EventArgs e)
        {
            glControl.Resize += glControl_Resize;
            glControl.Paint += glControl_Paint;

            glControl_Resize(glControl, EventArgs.Empty);
        }

        void glControl_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        void glControl_Resize(object? sender, EventArgs e)
        {
            _graphics.UpdateView();
        }

        void glControl_Paint(object? sender, PaintEventArgs e)
        {
            // Workaround for OpenTK GLControl not getting focuss initially
            if (!_hasInitialFocus)
            {
                _hasInitialFocus = true;
                glControl.Focus();
            }

            _graphics.Paint();
        }

        void glControl_Disposed(object? sender, EventArgs e)
        {
            Application.Idle -= Application_Idle;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _graphics.InitializeDevice();

            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object? sender, EventArgs e)
        {
            glControl.Invalidate();
        }
    }
}
