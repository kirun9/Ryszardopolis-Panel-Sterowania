namespace System.Mathf
{
    using System.Drawing;

    [System.Diagnostics.DebuggerStepThrough]
    public static class Mathf
    {
        public static int ClampMin(int value) => ClampMin(value, 0);

        public static int ClampMin(int value, int min)
        {
            return (value < min) ? min : value;
        }

        public static float ClampMin(float value) => ClampMin(value, 0);

        public static float ClampMin(float value, float min)
        {
            return (value < min) ? min : value;
        }

        public static int ClampMax(int value) => ClampMax(value, 0);

        public static int ClampMax(int value, int max)
        {
            return (value > max) ? max : value;
        }

        public static float ClampMax(float value) => ClampMax(value, 0);

        public static float ClampMax(float value, float max)
        {
            return (value > max) ? max : value;
        }

        public static int Clamp(int value, int max) => Clamp(value, 0, max);

        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static float Clamp(float value, float max) => Clamp(value, 0, max);

        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static Point Max(this Point value, Point max)
        {
            return new Point(Math.Max(value.X, max.X), Math.Max(value.Y, max.Y));
        }

        public static Point Min(this Point value, Point min)
        {
            return new Point(Math.Min(value.X, min.X), Math.Min(value.Y, min.Y));
        }

        public static Point ClampMax(this Point value, int maxX, int maxY) => value.ClampMax(new Point(maxX, maxY));

        public static Point ClampMax(this Point value) => value.ClampMax(Point.Empty);

        public static Point ClampMax(this Point value, Point min)
        {
            return new Point(ClampMax(value.X, min.X), ClampMax(value.Y, min.Y));
        }

        public static Point ClampMin(this Point value, int maxX, int maxY) => value.ClampMin(new Point(maxX, maxY));

        public static Point ClampMin(this Point value) => value.ClampMin(Point.Empty);

        public static Point ClampMin(this Point value, Point min)
        {
            return new Point(ClampMin(value.X, min.X), ClampMin(value.Y, min.Y));
        }

        public static Point Clamp(this Point value, Size size) => value.Clamp(Point.Empty, new Point(size));

        public static Point Clamp(this Point value, Point min, Size size) => value.Clamp(min, new Point(min.X + size.Width, min.Y + size.Height));

        public static Point Clamp(this Point value, int maxX, int maxY) => value.Clamp(Point.Empty, new Point(maxX, maxY));

        public static Point Clamp(this Point value, int minX, int minY, int maxX, int maxY) => value.Clamp(new Point(minX, minY), new Point(maxX, maxY));

        public static Point Clamp(this Point value, Point max) => value.Clamp(Point.Empty, max);

        public static Point Clamp(this Point value, Point min, Point max)
        {
            return new Point(Clamp(value.X, min.X, max.X), Clamp(value.Y, min.Y, max.Y));
        }
    }
}
