namespace PodgórzynPanelSterowania.Controls.Cells
{
    using PodgórzynPanelSterowania.Extensions;

    using System.Drawing;
    using System.Mathf;

    public partial class Element
    {
        internal void FillRectangle(Graphics g, Colors color, float x, float y, float width, float height) => FillRectangle(g, color.ToColor(), x, y, width, height);

        internal void FillRectangle(Graphics g, Color color, float x, float y, float width, float height)
        {
            using Brush brush = new SolidBrush(color);
            x = (x * elementScale).Floor() + location.X;
            y = (y * elementScale).Floor() + location.Y;
            width = (width * elementScale).Floor();
            height = (height * elementScale).Floor();
            g.FillRectangle(brush, x, y, width, height);
        }

        internal void DrawRectangle(Graphics g, Colors color, float x, float y, float width, float height) => DrawRectangle(g, color.ToColor(), x, y, width, height);

        internal void DrawRectangle(Graphics g, Color color, float x, float y, float width, float height)
        {
            using Pen pen = new Pen(color, elementScale);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
            DrawRectangle(g, pen, x, y, width, height);
        }

        internal void DrawRectangle(Graphics g, Pen pen, float x, float y, float width, float height)
        {
            x += Location.X;
            y += Location.Y;

            x *= ElementScale;
            y *= ElementScale;
            width *= ElementScale;
            height *= ElementScale;
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
            using Pen pen = new Pen(color, elementScale);
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
            DrawLine(g, pen, p1x, p1y, p2x, p2y);
        }

        internal void DrawLine(Graphics g, Pen pen, float p1x, float p1y, float p2x, float p2y)
        {
            float penW = pen.Width / 2;
            PointF p1 = new PointF();
            PointF p2 = new PointF();
            p1.X = (Location.X + (p1x * ElementScale) + penW).Clamp(Location.X + penW, Location.X + Size.Width - penW).Floor();
            p1.Y = (Location.Y + (p1y * ElementScale) + penW).Clamp(Location.Y + penW, Location.Y + Size.Height - penW).Floor();
            p2.X = (Location.X + (p2x * ElementScale) + penW).Clamp(Location.X + penW, Location.X + Size.Width - penW).Floor();
            p2.Y = (Location.Y + (p2y * ElementScale) + penW).Clamp(Location.Y + penW, Location.Y + Size.Height - penW).Floor();
            g.DrawLine(pen, p1, p2);
        }

        internal void DrawString(Graphics g, string s, Font font, Colors color, PointF point) => DrawString(g, s, font, color, point.X, point.Y);

        internal void DrawString(Graphics g, string s, Font font, Color color, PointF point) => DrawString(g, s, font, color, point.X, point.Y);

        internal void DrawString(Graphics g, string s, Font font, Colors color, float x, float y) => DrawString(g, s, font, color.ToColor(), x, y);

        internal void DrawString(Graphics g, string s, Font font, Color color, float x, float y)
        {
            using Brush brush = new SolidBrush(color);
            g.DrawString(s, font, brush, Location.X * elementScale + x * elementScale, Location.Y * elementScale + y * elementScale);
        }

        internal void DrawString(Graphics g, string s, Font font, Colors color, ContentAlignment alignment) => DrawString(g, s, font, color.ToColor(), new StringLocation() { Alignment = alignment });

        internal void DrawString(Graphics g, string s, Font font, Color color, ContentAlignment alignment) => DrawString(g, s, font, color, new StringLocation() { Alignment = alignment });

        internal void DrawString(Graphics g, string s, Font font, Colors color, StringLocation stringLocation) => DrawString(g, s, font, color.ToColor(), stringLocation);

        internal void DrawString(Graphics g, string s, Font font, Color color, StringLocation stringLocation)
        {
            var size = g.MeasureString(s, font);
            using SolidBrush brush = new SolidBrush(color);

            PointF point = stringLocation.Alignment switch
            {
                ContentAlignment.TopCenter => new PointF((USize.Width / 2) - (size.Width / 2) + stringLocation.DeltaX, (USize.Width / 4) * 1 - (size.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.MiddleCenter => new PointF((USize.Width / 2) - (size.Width / 2) + stringLocation.DeltaX, (USize.Width / 4) * 2 - (size.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.BottomCenter => new PointF((USize.Width / 2) - (size.Width / 2) + stringLocation.DeltaX, (USize.Width / 4) * 3 - (size.Height / 2) + stringLocation.DeltaY),

                ContentAlignment.TopLeft => new PointF(size.Width + size.Height / 2 + stringLocation.DeltaX, (USize.Width / 4) * 1 - (size.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.MiddleLeft => new PointF(size.Width + size.Height / 2 + stringLocation.DeltaX, (USize.Width / 4) * 2 - (size.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.BottomLeft => new PointF(size.Width + size.Height / 2 + stringLocation.DeltaX, (USize.Width / 4) * 3 - (size.Height / 2) + stringLocation.DeltaY),

                ContentAlignment.TopRight => new PointF(USize.Width - size.Width - size.Height, (USize.Width / 4) * 1 - (size.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.MiddleRight => new PointF(USize.Width - size.Width - size.Height, (USize.Width / 4) * 2 - (size.Height / 2) + stringLocation.DeltaY),
                ContentAlignment.BottomRight => new PointF(USize.Width - size.Width - size.Height, (USize.Width / 4) * 3 - (size.Height / 2) + stringLocation.DeltaY),

                _ => new PointF(stringLocation.X + stringLocation.DeltaX, stringLocation.Y + stringLocation.DeltaY),
            };

            font = new Font(font.Name, font.Size * elementScale, font.Style, font.Unit);

            point = new PointF((point.X) * ElementScale + Location.X, (point.Y) * ElementScale + Location.Y);

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
