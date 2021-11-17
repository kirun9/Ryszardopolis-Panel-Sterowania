namespace RyszardopolisPanelSterowania.Controls.Cells
{
    using System;
    using System.Drawing;

    public partial class Element
    {
        private Font font;
        private Font defaultFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point);
        private Point gridLocation;
        private Size size;
        private Point location;
        private float elementScale;
        private Func<float, int> roundingMethod = Extensions.Extensions.Floor;

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

        public bool DrawBottomBigger { get; set; }

        public bool DrawRightBigger { get; set; }

        public Point Location { get => location; set => location = value; }

        public virtual Font Font { get => font; set => font = value; }
        public Font DefaultFont => defaultFont;

        public void DrawCell(Graphics g)
        {
            DrawBorder(g);
            DrawContent(g);
        }

        protected virtual void DrawBorder(Graphics g)
        {
            FillRectangle(g, Colors.Background, 0, 0, Size.Width, Size.Height);

            DrawLine(g, Colors.BorderMain, 0, 0, 0, Size.Height);
            DrawLine(g, Colors.BorderMain, 0, 0, Size.Width, 0);

            DrawLineRect(g, Colors.BorderSecond, 0, 12, 0, 4);
            DrawLineRect(g, Colors.BorderSecond, 0, 22, 0, 4);
            DrawLineRect(g, Colors.BorderSecond, 12, 0, 4, 0);
            DrawLineRect(g, Colors.BorderSecond, 22, 0, 4, 0);

            if (DrawBottomBigger)
            {
                //DrawLineRect(g, Colors.Red, 0, Size.Height, Size.Width, 0);
                DrawLineRect(g, Colors.BorderMain, 0, Size.Height, Size.Width, 0);
                DrawLineRect(g, Colors.BorderSecond, 12, Size.Height, 4, 0);
                DrawLineRect(g, Colors.BorderSecond, 22, Size.Height, 4, 0);
            }

            if (DrawRightBigger)
            {
                DrawLineRect(g, Colors.BorderMain, Size.Width, 0, 0, Size.Height);
                DrawLineRect(g, Colors.BorderSecond, Size.Width, 12, 0, 4);
                DrawLineRect(g, Colors.BorderSecond, Size.Width, 22, 0, 4);
            }
        }

        protected virtual void DrawContent(Graphics g)
        {
        }

        public void SetRoundingMethod(RoundingMethod method)
        {
            roundingMethod = method switch
            {
                RoundingMethod.Round => Extensions.Extensions.Round,
                RoundingMethod.Floor => Extensions.Extensions.Floor,
                RoundingMethod.Ceiling => Extensions.Extensions.Ceiling,
                _ => Extensions.Extensions.Floor,
            };
        }
    }
}
