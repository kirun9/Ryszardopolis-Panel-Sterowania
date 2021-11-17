namespace RyszardopolisPanelSterowania.Controls.Cells
{
    using System.Drawing;

    public partial class Element
    {
        internal void FillRectangle(Graphics g, Colors color, float x, float y, float width, float height) => FillRectangle(g, color.ToColor(), x, y, width, height);

        internal void FillRectangle(Graphics g, Color color, float x, float y, float width, float height)
        {
            using Brush brush = new SolidBrush(color);
            g.FillRectangle(brush, x, y, width, height);
        }

        internal void DrawRectangle(Graphics g, Colors color, float x, float y, float width, float height) => DrawRectangle(g, color.ToColor(), x, y, width, height);

        internal void DrawRectangle(Graphics g, Color color, float x, float y, float width, float height)
        {
            using Pen pen = new Pen(color, 1);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
            DrawRectangle(g, pen, x, y, width, height);
        }

        internal void DrawRectangle(Graphics g, Pen pen, float x, float y, float width, float height)
        {
            g.DrawRectangle(pen, x, y, width, height);
        }

        internal void DrawLineRect(Graphics g, Colors color, RectangleF rect) => DrawLine(g, color, rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);

        internal void DrawLineRect(Graphics g, Colors color, float x, float y, float width, float height) => DrawLine(g, color, x, y, x + width, y + height);

        internal void DrawLine(Graphics g, Colors color, Point p1, Point p2) => DrawLine(g, color.ToColor(), p1, p2);

        internal void DrawLine(Graphics g, Color color, Point p1, Point p2) => DrawLine(g, color, p1.X, p1.Y, p2.X, p2.Y);

        internal void DrawLine(Graphics g, Pen pen, Point p1, Point p2) => DrawLine(g, pen, p1.X, p2.Y, p2.X, p2.Y);

        internal void DrawLine(Graphics g, Colors color, float p1x, float p1y, float p2x, float p2y) => DrawLine(g, color.ToColor(), p1x, p1y, p2x, p2y);

        internal void DrawLine(Graphics g, Color color, float p1x, float p1y, float p2x, float p2y)
        {
            using Pen pen = new Pen(color, 1);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
            DrawLine(g, pen, p1x, p1y, p2x, p2y);
        }

        internal void DrawLine(Graphics g, Pen pen, float p1x, float p1y, float p2x, float p2y)
        {
            float penW = pen.Width / 2;

            p1x += (p1x > Size.Width / 2 ? -penW : (p1x == Size.Width / 2 ? 0 : penW));
            p1y += (p1y > Size.Height / 2 ? -penW : (p1y == Size.Height / 2 ? 0 : penW));
            p2x += (p2x > Size.Width / 2 ? -penW : (p2x == Size.Width / 2 ? 0 : penW));
            p2y += (p2y > Size.Height / 2 ? -penW : (p2y == Size.Height / 2 ? 0 : penW));

            PointF p1 = new PointF(p1x, p1y);
            PointF p2 = new PointF(p2x, p2y);
            g.DrawLine(pen, p1, p2);
        }

        internal void DrawString(Graphics g, string s, Font font, Colors color, PointF point) => DrawString(g, s, font, color, point.X, point.Y);

        internal void DrawString(Graphics g, string s, Font font, Color color, PointF point) => DrawString(g, s, font, color, point.X, point.Y);

        internal void DrawString(Graphics g, string s, Font font, Colors color, float x, float y) => DrawString(g, s, font, color.ToColor(), x, y);

        internal void DrawString(Graphics g, string s, Font font, Color color, float x, float y)
        {
            using Brush brush = new SolidBrush(color);
            g.DrawString(s, font, brush, x, y);
        }

        internal void DrawString(Graphics g, string s, Font font, Colors color, ContentAlignment alignment) => DrawString(g, s, font, color.ToColor(), new StringLocation() { Alignment = alignment });

        internal void DrawString(Graphics g, string s, Font font, Color color, ContentAlignment alignment) => DrawString(g, s, font, color, new StringLocation() { Alignment = alignment });

        internal void DrawString(Graphics g, string s, Font font, Colors color, StringLocation stringLocation) => DrawString(g, s, font, color.ToColor(), stringLocation);

        internal void DrawString(Graphics g, string s, Font font, Color color, StringLocation stringLocation)
        {
            var stringSize = g.MeasureString(s, font);
            using SolidBrush brush = new SolidBrush(color);

            PointF point = stringLocation.Alignment switch
            {
                ContentAlignment.TopCenter    => new PointF((Size.Width / 2) - (stringSize.Width / 2) + stringLocation.DeltaX         , (Size.Height / 4) * 1 - (stringSize.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.MiddleCenter => new PointF((Size.Width / 2) - (stringSize.Width / 2) + stringLocation.DeltaX         , (Size.Height / 4) * 2 - (stringSize.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.BottomCenter => new PointF((Size.Width / 2) - (stringSize.Width / 2) + stringLocation.DeltaX         , (Size.Height / 4) * 3 - (stringSize.Height / 2) + stringLocation.DeltaY),

                ContentAlignment.TopLeft      => new PointF(stringSize.Width + stringSize.Height / 2 + stringLocation.DeltaX          , (Size.Height / 4) * 1 - (stringSize.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.MiddleLeft   => new PointF(stringSize.Width + stringSize.Height / 2 + stringLocation.DeltaX          , (Size.Height / 4) * 2 - (stringSize.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.BottomLeft   => new PointF(stringSize.Width + stringSize.Height / 2 + stringLocation.DeltaX          , (Size.Height / 4) * 3 - (stringSize.Height / 2) + stringLocation.DeltaY),

                ContentAlignment.TopRight     => new PointF(Size.Width - stringSize.Width - stringSize.Height + stringLocation.DeltaX , (Size.Height / 4) * 1 - (stringSize.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.MiddleRight  => new PointF(Size.Width - stringSize.Width - stringSize.Height + stringLocation.DeltaX , (Size.Height / 4) * 2 - (stringSize.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.BottomRight  => new PointF(Size.Width - stringSize.Width - stringSize.Height + stringLocation.DeltaX , (Size.Height / 4) * 3 - (stringSize.Height / 2) + stringLocation.DeltaY),

                _ => new PointF(stringLocation.X + stringLocation.DeltaX, stringLocation.Y + stringLocation.DeltaY),
            };

            g.DrawString(s, font, brush, point);
        }
    }

    public struct StringLocation
    {
        private ContentAlignment alignment = ContentAlignment.MiddleCenter;
        private float rotation;

        private float x;
        private float y;

        private float dX;
        private float dY;

        public float Rotation { get => rotation; set => rotation = value % 360; }

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public float DeltaX
        {
            get
            {
                return dX;
            }
            set
            {
                dX = value;
            }
        }

        public float DeltaY
        {
            get
            {
                return dY;
            }
            set
            {
                dY = value;
            }
        }

        public ContentAlignment Alignment { get => alignment; set => alignment = value; }
    }
}
