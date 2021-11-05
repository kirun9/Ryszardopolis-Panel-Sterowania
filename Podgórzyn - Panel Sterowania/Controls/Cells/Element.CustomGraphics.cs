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
    }
}
