namespace PodgórzynPanelSterowania.Extensions
{
    using System;
    using System.Drawing;

    [System.Diagnostics.DebuggerStepThrough]
    public static class Extensions
    {
        public static T Clone<T>(this T val) where T : struct => val;

        public static Point Add(this Point value, int x, int y) => value.Add(new Size(x, y));

        public static Point Add(this Point value, Point add) => value.Add((Size) add);

        public static Point Add(this Point value, Size add)
        {
            return Point.Add(value, add);
        }

        public static Size Add(this Size value, int x, int y) => value.Add(new Size(x, y));

        public static Size Add(this Size value, Point add) => value.Add((Size) add);

        public static Size Add(this Size value, Size add)
        {
            return Size.Add(value, add);
        }

        public static bool IsBetween(this int val, int boundary1, int boundary2)
        {
            return (boundary1 < boundary2) ? (val >= boundary1 && val <= boundary2) : (val <= boundary1 && val >= boundary2);
        }

        public static int Ceiling(this float value)
        {
            return (int) Math.Ceiling(value);
        }

        public static int Floor(this float value)
        {
            return (int) Math.Floor(value);
        }
    }
}
