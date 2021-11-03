namespace PodgórzynPanelSterowania.Controls
{
    using System.Drawing;

    public enum Colors
    {
        Background   = 0xDCE0DE,
        BorderMain   = 0x999999,
        BorderSecond = 0x000000,
        Black        = 0x000000,
        SidePrimary  = 0x303030,
        SideScondary = 0x202020,
        SideTrinnary = 0x000000,
        SideFilling  = 0x999999
    }

    public static class ColorsExtensions
    {
        public static Color ToColor(this Colors c)
        {
            return Color.FromArgb((int) c >> 16 & 0xFF, (int) c >> 8 & 0xFF, (int) c >> 0 & 0xFF);
        }

        public static Color ToColor(this int c)
        {
            return Color.FromArgb(c >> 16 & 0xFF, c >> 8 & 0xFF, c >> 0 & 0xFF);
        }

        public static Color ToColor(this uint c)
        {
            return Color.FromArgb((int) (c >> 24 & 0xFF), Color.FromArgb((int) (c >> 16 & 0xFF), (int) (c >> 8 & 0xFF), (int) (c >> 0 & 0xFF)));
        }
    }
}
