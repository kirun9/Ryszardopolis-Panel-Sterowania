namespace System.Mathf;

using System.Drawing;

[Diagnostics.DebuggerStepThrough]
public static class Mathf
{
    public static float Map(this float value, float oldMin, float oldMax, float newMin, float newMax)
    {
        float oldRange = oldMax - oldMin;
        float newRange = newMax - newMin;
        float newValue = (((value - oldMin) * newRange) / oldRange) + newMin;
        return newValue;
    }

    public static int ClampMin(this int value) => value.ClampMin(0);

    public static int ClampMin(this int value, int min)
    {
        value = (value < min) ? min : value;
        return value;
    }

    public static float ClampMin(this float value) => value.ClampMin(0);

    public static float ClampMin(this float value, float min)
    {
        value = (value < min) ? min : value;
        return value;
    }

    public static int ClampMax(this int value) => value.ClampMax(0);

    public static int ClampMax(this int value, int max)
    {
        value = (value > max) ? max : value;
        return value;
    }

    public static float ClampMax(this float value) => value.ClampMax(0);

    public static float ClampMax(this float value, float max)
    {
        value = (value > max) ? max : value;
        return value;
    }

    public static int Clamp(this int value, int max) => value.Clamp(0, max);

    public static int Clamp(this int value, int min, int max)
    {
        value = (value < min) ? min : (value > max) ? max : value;
        return value;
    }

    public static float Clamp(this float value, float max) => value.Clamp(0, max);

    public static float Clamp(this float value, float min, float max)
    {
        value = (value < min) ? min : (value > max) ? max : value;
        return value;
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
