namespace PodgórzynPanelSterowania.Controls.Cells
{
    using System.Drawing;

    public partial class Element
    {
        private Font font;
        private Font defaultFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point);
        private Point gridLocation;
        private Size size;
        private Size usize;
        private Point location;
        private float elementScale;

        public Point GridLocation
        {
            get
            {
                return gridLocation;
            }

            set
            {
                gridLocation = value;
            }
        }

        public Size Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public Size USize { get => usize; set => usize = value; }

        public bool DrawBottomBigger { get; set; }

        public bool DrawRightBigger { get; set; }

        public Point Location { get => location; set => location = value; }

        public float ElementScale { get => elementScale; set => elementScale = value; }

        public Font DefaultFont => defaultFont;

        public virtual Font Font { get => font ?? defaultFont; set => font = value; }

        public Font ScaledFont => new Font(Font.Name, Font.Size * elementScale, Font.Style, Font.Unit);

        public virtual void UpdateBitmap(Graphics g)
        {
            DrawBorder(g);
            DrawContent(g);
        }

        public virtual void DrawBorder(Graphics g)
        {
            FillRectangle(g, Colors.Background, 0, 0, USize.Width, USize.Height);

            DrawLine(g, Colors.BorderMain, 0, 0, 0, USize.Height);
            DrawLine(g, Colors.BorderMain, 0, 0, USize.Width, 0);

            DrawLineRect(g, Colors.BorderSecond, 0, 12, 0, 4);
            DrawLineRect(g, Colors.BorderSecond, 0, 22, 0, 4);
            DrawLineRect(g, Colors.BorderSecond, 12, 0, 4, 0);
            DrawLineRect(g, Colors.BorderSecond, 22, 0, 4, 0);

            if (DrawBottomBigger)
            {
                DrawLineRect(g, Colors.BorderMain, 0, USize.Height, USize.Width, 0);
                DrawLineRect(g, Colors.BorderSecond, 12, USize.Height, 4, 0);
                DrawLineRect(g, Colors.BorderSecond, 22, USize.Height, 4, 0);
            }

            if (DrawRightBigger)
            {
                DrawLineRect(g, Colors.BorderMain, USize.Width, 0, 0, USize.Height);
                DrawLineRect(g, Colors.BorderSecond, USize.Width, 12, 0, 4);
                DrawLineRect(g, Colors.BorderSecond, USize.Width, 22, 0, 4);
            }
        }

        public virtual void DrawContent(Graphics g)
        {
        }
    }
}
