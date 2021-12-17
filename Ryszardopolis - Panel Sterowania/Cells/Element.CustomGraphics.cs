namespace RyszardopolisPanelSterowania.Cells;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

using RyszardopolisPanelSterowania.Controls;

public partial class Element
{
    #region DrawImage
    [DebuggerStepThrough]
    internal void DrawImage(Graphics g, string imageName, int x, int y, int width, int height)
    {
        using Image image = Image.FromFile(imageName);
        DrawImage(g, image as Bitmap, x, y, width, height);
    }

    [DebuggerStepThrough]
    internal void DrawImage(Graphics g, Bitmap image, int x, int y, int width, int height)
    {
        using Bitmap b = new Bitmap(image, width, height);
        g.DrawImage(b, x, y, width, height);
    }
    #endregion

    #region DrawArc
    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Colors color, RectangleF rect, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, color, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Colors color, Rectangle rect, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, color, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Colors color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, color.ToColor(), x, y, width, height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Colors color, int x, int y, int width, int height, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, color.ToColor(), x, y, width, height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Color color, RectangleF rect, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, color, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Color color, Rectangle rect, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, color, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Color color, float x, float y, float width, float height, float startAngle, float sweepAngle, bool useTexture = true)
    {
        using Pen pen = new Pen(color, 1);
        pen.StartCap = LineCap.Square;
        pen.EndCap = LineCap.Square;
        DrawArc(g, pen, x, y, width, height, startAngle, sweepAngle, useTexture);
    }

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Color color, int x, int y, int width, int height, float startAngle, float sweepAngle, bool useTexture = true)
    {
        using Pen pen = new Pen(color, 1);
        pen.StartCap = LineCap.Square;
        pen.EndCap = LineCap.Square;
        DrawArc(g, pen, x, y, width, height, startAngle, sweepAngle, useTexture);
    }

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Pen pen, RectangleF rect, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Pen pen, Rectangle rect, float startAngle, float sweepAngle, bool useTexture = true) => DrawArc(g, pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle, useTexture);

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle, bool useTexture = true)
    {
        g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        if (useTexture)
        {
            pen.Brush = textureBrush;
            g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }
    }

    [DebuggerStepThrough]
    internal void DrawArc(Graphics g, Pen pen, int x, int y, int width, int height, float startAngle, float sweepAngle, bool useTexture = true)
    {
        g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        if (useTexture)
        {
            pen.Brush = textureBrush;
            g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }
    }
    #endregion

    #region FillRectangle

    [DebuggerStepThrough]
    internal void FillRectangle(Graphics g, TextureBrush brush, float x, float y, float width, float height)
    {
        using Matrix t = brush.Transform;

        brush.ScaleTransform(1f / width, 1f / height);
        g.FillRectangle(brush, x, y, width, height);
        brush.Transform = t;
    }

    [DebuggerStepThrough]
    internal void FillRectangle(Graphics g, Colors color, float x, float y, float width, float height, bool useTexture = true) => FillRectangle(g, color.ToColor(), x, y, width, height, useTexture);

    [DebuggerStepThrough]
    internal void FillRectangle(Graphics g, Color color, float x, float y, float width, float height, bool useTexture = true)
    {
        using Brush brush = new SolidBrush(color);

        g.FillRectangle(brush, x, y, width, height);
        if (useTexture)
        {
            g.FillRectangle(TextureBrush, x, y, width, height);
        }
    }
    #endregion

    #region FillPath

    [DebuggerStepThrough]
    internal void FillPath(Graphics g, Colors color, GraphicsPath path, bool useTexture = true) => FillPath(g, color.ToColor(), path, useTexture);

    [DebuggerStepThrough]
    internal void FillPath(Graphics g, Color color, GraphicsPath path, bool useTexture = true)
    {
        for (int i = 0; i < path.PointCount; i++)
        {
            path.PathPoints[i] = FixPoint(path.PathPoints[i], 0.5f);
        }

        using Brush brush = new SolidBrush(color);
        g.FillPath(brush, path);
        if (useTexture)
            g.FillPath(TextureBrush, path);
    }

    #endregion

    #region DrawPath

    [DebuggerStepThrough]
    internal void DrawPath(Graphics g, Colors color, GraphicsPath path, bool useTexture = true) => DrawPath(g, color.ToColor(), path, useTexture);

    [DebuggerStepThrough]
    internal void DrawPath(Graphics g, Color color, GraphicsPath path, bool useTexture = true)
    {
        using Pen pen = new Pen(color);
        pen.StartCap = pen.EndCap = LineCap.Square;
        pen.Alignment = PenAlignment.Inset;
        DrawPath(g, pen, path, useTexture);
    }

    [DebuggerStepThrough]
    internal void DrawPath(Graphics g, Pen pen, GraphicsPath path, bool useTexture = true)
    {
        PointF[] points = path.PathPoints;
        //for (int i = 0; i < points.Length; i++)
        //{
        //    points[i] = /*FixPoint(points[i], pen)*/ points[i];
        //}
        using GraphicsPath newPath = new GraphicsPath(points, path.PathTypes, path.FillMode);

        g.DrawPath(pen, newPath);
        if (useTexture)
        {
            pen.Brush = textureBrush;
            g.DrawPath(pen, newPath);
        }
    }
    #endregion

    #region DrawRectangle

    [DebuggerStepThrough]
    internal void DrawRectangle(Graphics g, Colors color, RectangleF rect, bool useTexture = true) => DrawRectangle(g, color, rect.X, rect.Y, rect.Width, rect.Height, useTexture);

    [DebuggerStepThrough]
    internal void DrawRectangle(Graphics g, Color color, RectangleF rect, bool useTexture = true) => DrawRectangle(g, color, rect.X, rect.Y, rect.Width, rect.Height, useTexture);

    [DebuggerStepThrough]
    internal void DrawRectangle(Graphics g, Pen pen, RectangleF rect, bool useTexture = true) => DrawRectangle(g, pen, rect.X, rect.Y, rect.Width, rect.Height, useTexture);

    [DebuggerStepThrough]
    internal void DrawRectangle(Graphics g, Colors color, float x, float y, float width, float height, bool useTexture = true) => DrawRectangle(g, color.ToColor(), x, y, width, height, useTexture);

    [DebuggerStepThrough]
    internal void DrawRectangle(Graphics g, Color color, float x, float y, float width, float height, bool useTexture = true)
    {
        using Pen pen = new Pen(color, 1);
        pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
        DrawRectangle(g, pen, x, y, width, height, useTexture);
    }

    [DebuggerStepThrough]
    internal void DrawRectangle(Graphics g, Pen pen, float x, float y, float width, float height, bool useTexture = true)
    {
        g.DrawRectangle(pen, x, y, width, height);
        if (useTexture)
        {
            pen.Brush = TextureBrush;
            g.DrawRectangle(pen, x, y, width, height);
        }
    }
    #endregion

    #region DrawLineRect, DrawLine
    [DebuggerStepThrough]
    internal void DrawLineRect(Graphics g, Colors color, RectangleF rect, bool useTexture = true) => DrawLine(g, color, rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height, useTexture);

    [DebuggerStepThrough]
    internal void DrawLineRect(Graphics g, Colors color, float x, float y, float width, float height, bool useTexture = true) => DrawLine(g, color, x, y, x + width, y + height, useTexture);

    [DebuggerStepThrough]
    internal void DrawLine(Graphics g, Colors color, Point p1, Point p2, bool useTexture = true) => DrawLine(g, color.ToColor(), p1, p2, useTexture);

    [DebuggerStepThrough]
    internal void DrawLine(Graphics g, Color color, Point p1, Point p2, bool useTexture = true) => DrawLine(g, color, p1.X, p1.Y, p2.X, p2.Y, useTexture);

    [DebuggerStepThrough]
    internal void DrawLine(Graphics g, Pen pen, Point p1, Point p2, bool useTexture = true) => DrawLine(g, pen, p1.X, p1.Y, p2.X, p2.Y, useTexture);

    [DebuggerStepThrough]
    internal void DrawLine(Graphics g, Colors color, float p1x, float p1y, float p2x, float p2y, bool useTexture = true) => DrawLine(g, color.ToColor(), p1x, p1y, p2x, p2y, useTexture);

    [DebuggerStepThrough]
    internal void DrawLine(Graphics g, Color color, float p1x, float p1y, float p2x, float p2y, bool useTexture = true)
    {
        using Pen pen = new Pen(color, 1);
        pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
        DrawLine(g, pen, p1x, p1y, p2x, p2y, useTexture);
    }

    [DebuggerStepThrough]
    internal void DrawLine(Graphics g, Pen pen, float p1x, float p1y, float p2x, float p2y, bool useTexture = true)
    {
        /*float penW = pen.Width / 2;

        p1x += (p1x > Size.Width / 2 ? -penW : (p1x == Size.Width / 2 ? 0 : penW));
        p1y += (p1y > Size.Height / 2 ? -penW : (p1y == Size.Height / 2 ? 0 : penW));
        p2x += (p2x > Size.Width / 2 ? -penW : (p2x == Size.Width / 2 ? 0 : penW));
        p2y += (p2y > Size.Height / 2 ? -penW : (p2y == Size.Height / 2 ? 0 : penW));*/

        PointF p1 = FixPoint(new PointF(p1x, p1y), pen);
        PointF p2 = FixPoint(new PointF(p2x, p2y), pen);
        g.DrawLine(pen, p1, p2);
        if (useTexture)
        {
            pen.Brush = TextureBrush;
            g.DrawLine(pen, p1, p2);
        }

    }
    #endregion

    #region DrawString
    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Colors color, PointF point, bool useTexture = true) => DrawString(g, s, font, color, point.X, point.Y, useTexture);

    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Color color, PointF point, bool useTexture = true) => DrawString(g, s, font, color, point.X, point.Y, useTexture);

    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Colors color, float x, float y, bool useTexture = true) => DrawString(g, s, font, color.ToColor(), x, y, useTexture);

    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Color color, float x, float y, bool useTexture = true)
    {
        using Brush brush = new SolidBrush(color);
        g.DrawString(s, font, brush, x, y);
        if (useTexture)
        {
            g.DrawString(s, font, TextureBrush, x, y);
        }
    }

    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Colors color, ContentAlignment alignment, bool useTexture = true) => DrawString(g, s, font, color.ToColor(), new StringLocation() { Alignment = alignment }, useTexture);

    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Color color, ContentAlignment alignment, bool useTexture = true) => DrawString(g, s, font, color, new StringLocation() { Alignment = alignment }, useTexture);

    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Colors color, StringLocation stringLocation, bool useTexture = true) => DrawString(g, s, font, color.ToColor(), stringLocation, useTexture);

    [DebuggerStepThrough]
    internal void DrawString(Graphics g, string s, Font font, Color color, StringLocation stringLocation, bool useTexture = true)
    {
        var stringSize = g.MeasureString(s, font);
        using SolidBrush brush = new SolidBrush(color);

        PointF point = stringLocation.Alignment switch
        {
            ContentAlignment.TopCenter => new PointF((Size.Width / 2) - (stringSize.Width / 2) + stringLocation.DeltaX, (Size.Height / 4) * 1 - (stringSize.Height / 2) + stringLocation.DeltaY),
            ContentAlignment.MiddleCenter => new PointF((Size.Width / 2) - (stringSize.Width / 2) + stringLocation.DeltaX, (Size.Height / 4) * 2 - (stringSize.Height / 2) + stringLocation.DeltaY),
            ContentAlignment.BottomCenter => new PointF((Size.Width / 2) - (stringSize.Width / 2) + stringLocation.DeltaX, (Size.Height / 4) * 3 - (stringSize.Height / 2) + stringLocation.DeltaY),

            ContentAlignment.TopLeft => new PointF(stringSize.Height / 2 + stringLocation.DeltaX, (Size.Height / 4) * 1 - (stringSize.Height / 2) + stringLocation.DeltaY),
            ContentAlignment.MiddleLeft => new PointF(stringSize.Height / 2 + stringLocation.DeltaX, (Size.Height / 4) * 2 - (stringSize.Height / 2) + stringLocation.DeltaY),
            ContentAlignment.BottomLeft => new PointF(stringSize.Height / 2 + stringLocation.DeltaX, (Size.Height / 4) * 3 - (stringSize.Height / 2) + stringLocation.DeltaY),

            ContentAlignment.TopRight => new PointF(Size.Width - stringSize.Width - stringSize.Height / 2 + stringLocation.DeltaX, (Size.Height / 4) * 1 - (stringSize.Height / 2) + stringLocation.DeltaY),
            ContentAlignment.MiddleRight => new PointF(Size.Width - stringSize.Width - stringSize.Height / 2 + stringLocation.DeltaX, (Size.Height / 4) * 2 - (stringSize.Height / 2) + stringLocation.DeltaY),
            ContentAlignment.BottomRight => new PointF(Size.Width - stringSize.Width - stringSize.Height / 2 + stringLocation.DeltaX, (Size.Height / 4) * 3 - (stringSize.Height / 2) + stringLocation.DeltaY),

            _ => new PointF(stringLocation.X + stringLocation.DeltaX, stringLocation.Y + stringLocation.DeltaY),
        };

        g.DrawString(s, font, brush, point);
        if (useTexture)
        {
            g.DrawString(s, font, TextureBrush, point);
        }
    }
    #endregion

    #region AdditionalMethods
    [DebuggerStepThrough]
    public PointF FixPoint(PointF point, Pen pen) => FixPoint(point, pen.Width / 2);

    [DebuggerStepThrough]
    public PointF FixPoint(PointF point, float penWidth)
    {
        point.X += (point.X > Size.Width / 2 ? -penWidth : (point.X == Size.Width / 2 ? 0 : penWidth));
        point.Y += (point.Y > Size.Height / 2 ? -penWidth : (point.Y == Size.Height / 2 ? 0 : penWidth));
        return point;
    }

    [DebuggerStepThrough]
    public PointF TransformPoint(PointF point, PointF center, float angle, bool sx, bool sy)
    {
        // Check if need swapping
        if (sx)
            point.X = center.X + (center.X - point.X);
        if (sy)
            point.Y = center.Y + (center.Y - point.Y);
        return RotatePoint(point, center, angle);
    }

    [DebuggerStepThrough]
    public PointF RotatePoint(PointF point, PointF center, float angle)
    {
        float angleInRadians = angle * ((float) Math.PI / 180);
        float cosTheta = (float) Math.Cos(angleInRadians);
        float sinTheta = (float) Math.Sin(angleInRadians);

        return new PointF
        {
            X = cosTheta * (point.X - center.X) - sinTheta * (point.Y - center.Y) + center.X,
            Y = sinTheta * (point.X - center.X) + cosTheta * (point.Y - center.Y) + center.Y
        };
    }
    #endregion
}
