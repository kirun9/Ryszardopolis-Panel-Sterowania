namespace PodgórzynPanelSterowania.Controls
{
    using System.Drawing;
    using System.Mathf;
    using System.Windows.Forms;
    using PodgórzynPanelSterowania.Extensions;

    public partial class Element : Control
    {
        private Point gridLocation;
        private bool drawBottomBigger;
        private bool drawRightBigger;

        public Element()
        {
            DoubleBuffered = true;
            InitializeComponent();
            BackColor = Colors.Background.ToColor();
        }

        public Point GridLocation
        {
            get
            {
                return Parent is Pulpit p ? gridLocation.Clamp(p.Dimensions.Add(2, 2)) : gridLocation.ClampMin();
            }

            set
            {
                gridLocation = Parent is Pulpit p ? value.Clamp(p.Dimensions.Add(2, 2)) : value.ClampMin();
            }
        }

        public new Size Size
        {
            get
            {
                return base.Size;
            }

            set
            {
                base.Size = value;
                MinimumSize = value;
                MaximumSize = value;
            }
        }

        internal float ElementScale => Parent is Pulpit p ? p.PulpitScale : 1;

        internal bool DrawBottomBigger { get => drawBottomBigger; set => drawBottomBigger = value; }
        internal bool DrawRightBigger { get => drawRightBigger; set => drawRightBigger = value; }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            Invalidate();
        }

        protected virtual void DrawElement(Graphics g)
        {

        }

        private void DrawBorder(Graphics g)
        {
            using SolidBrush brush = new SolidBrush(Colors.BorderMain.ToColor());
            g.FillRectangle(brush, 0, 0, ElementScale, Height);
            g.FillRectangle(brush, 0, 0, Width, ElementScale);
            brush.Color = Colors.BorderSecond.ToColor();
            g.FillRectangle(brush, 0, 12 * ElementScale, ElementScale, 4 * ElementScale);
            g.FillRectangle(brush, 0, 22 * ElementScale, ElementScale, 4 * ElementScale);
            g.FillRectangle(brush, 12 * ElementScale, 0, 4 * ElementScale, ElementScale);
            g.FillRectangle(brush, 22 * ElementScale, 0, 4 * ElementScale, ElementScale);
            if (drawBottomBigger)
            {
                brush.Color = Colors.BorderMain.ToColor();
                g.FillRectangle(brush, 0, Height - ElementScale.Ceiling(), Width, ElementScale);
                brush.Color = Colors.BorderSecond.ToColor();
                g.FillRectangle(brush, 12 * ElementScale, Height - ElementScale.Ceiling(), 4 * ElementScale, ElementScale);
                g.FillRectangle(brush, 22 * ElementScale, Height - ElementScale.Ceiling(), 4 * ElementScale, ElementScale);
            }
            if (drawRightBigger)
            {
                brush.Color = Colors.BorderMain.ToColor();
                g.FillRectangle(brush, Width - ElementScale.Ceiling(), 0, ElementScale, Height);
                brush.Color = Colors.BorderSecond.ToColor();
                g.FillRectangle(brush, Width - ElementScale.Ceiling(), 12 * ElementScale, ElementScale, 4 * ElementScale);
                g.FillRectangle(brush, Width - ElementScale.Ceiling(), 22 * ElementScale, ElementScale, 4 * ElementScale);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var font = new Font(Font.Name, Font.Size * ElementScale, DefaultFont.Style, DefaultFont.Unit);

            using Brush backBrush = new SolidBrush(Colors.Background.ToColor());
            e.Graphics.FillRectangle(backBrush, 0, 0, Size.Width, Size.Height);

            DrawBorder(e.Graphics);
            DrawElement(e.Graphics);

            /*var size = e.Graphics.MeasureString(Text, font);
            using (Brush b = new SolidBrush(Color.Black))
            {
                e.Graphics.DrawString(Text, font, b, (Size.Width / 2) - (size.Width / 2), (Size.Height / 2) - (size.Height / 2));
            }*/
        }
    }
}
