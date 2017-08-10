using OpenTK;
using System;
using System.Windows.Forms;

namespace DemoFramework.OpenTK
{
    public partial class GLForm : Form
    {
        private OpenTKGraphics _graphics;

        public GLForm(OpenTKGraphics graphics)
        {
            _graphics = graphics;

            InitializeComponent();

            //GLControl = new GLControl(new GraphicsMode(new ColorFormat(24), 24, 0, 4));
            GLControl = new GLControl
            {
                BackColor = System.Drawing.Color.Black,
                Dock = DockStyle.Fill,
                TabIndex = 0,
                VSync = true
            };

            Controls.Add(GLControl);

            GLControl.Paint += new PaintEventHandler(glControl_Paint);
            GLControl.Disposed += new EventHandler(glControl_Disposed);

            GLControl.KeyDown += new KeyEventHandler(glControl_KeyDown);
            GLControl.KeyUp += new KeyEventHandler(glControl_KeyUp);
            GLControl.MouseDown += new MouseEventHandler(glControl_MouseDown);
            GLControl.MouseUp += new MouseEventHandler(glControl_MouseUp);
            GLControl.MouseMove += new MouseEventHandler(glControl_MouseMove);
            GLControl.MouseWheel += new MouseEventHandler(glControl_MouseWheel);
            GLControl.PreviewKeyDown += new PreviewKeyDownEventHandler(glControl_PreviewKeyDown);

            Resize += new EventHandler(GLForm_Resize);
        }

        public GLControl GLControl { get; }

        void glControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        void GLForm_Resize(object sender, EventArgs e)
        {
            _graphics.UpdateView();
        }

        void glControl_Disposed(object sender, EventArgs e)
        {
            Application.Idle -= Application_Idle;
        }

        void glControl_Paint(object sender, PaintEventArgs e)
        {
            _graphics.Paint();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // GLControl loaded, start demo
            _graphics.InitializeDevice();

            Application.Idle += new EventHandler(Application_Idle);
            Focus();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            GLControl.Invalidate();
        }

        void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            OnMouseWheel(e);
        }
    }
}
