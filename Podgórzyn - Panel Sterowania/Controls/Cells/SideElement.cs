namespace PodgórzynPanelSterowania.Controls.Cells
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
using PodgórzynPanelSterowania.Extensions;

    public class SideElement : NewElement
    {
        private SideLocation side;

        [Flags]
        public enum SideLocation
        {
            None = 0b0000,
            Top = 0b0001,
            Bottom = 0b0010,
            Left = 0b0100,
            Right = 0b1000,

            TopLeft = 0b0101,
            TopRight = 0b1001,
            BottomLeft = 0b0110,
            BottomRight = 0b1010,
        }

        public SideLocation Side { get => side; set => side = value; }

        public override void DrawBorder(Graphics g)
        {

        }

        public override void DrawContent(Graphics g)
        {
            switch (Side)
            {
                case SideLocation.Top:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, Location.X, Location.Y, Size.Width, Size.Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, Location.X, Location.Y + (2 * ElementScale).Floor(), Size.Width, ElementScale);
                    g.FillRectangle(brush, Location.X, Location.Y + (36 * ElementScale).Floor(), Size.Width, ElementScale);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, Location.X, Location.Y + (1 * ElementScale).Floor(), Size.Width, ElementScale);
                    g.FillRectangle(brush, Location.X, Location.Y + (37 * ElementScale).Floor(), Size.Width, ElementScale);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, Location.X, Location.Y + (0 * ElementScale).Floor(), Size.Width, ElementScale);
                    break;
                }
                case SideLocation.Bottom:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, Location.X, Location.Y, Size.Width, Size.Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, Location.X, Location.Y + (1 * ElementScale).Floor(), Size.Width, ElementScale);
                    g.FillRectangle(brush, Location.X, Location.Y + (35 * ElementScale).Floor(), Size.Width, ElementScale);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, Location.X, Location.Y + (0 * ElementScale).Floor(), Size.Width, ElementScale);
                    g.FillRectangle(brush, Location.X, Location.Y + (36 * ElementScale).Floor(), Size.Width, ElementScale);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, Location.X, Location.Y + (37 * ElementScale).Floor(), Size.Width, ElementScale);
                    break;
                }
                case SideLocation.Left:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, Location.X, Location.Y, Size.Width, Size.Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, Location.X + (2 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    g.FillRectangle(brush, Location.X + (36 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, Location.X + (1 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    g.FillRectangle(brush, Location.X + (37 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, Location.X + (0 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    break;
                }
                case SideLocation.Right:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, Location.X, Location.Y, Size.Width, Size.Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, Location.X + (1 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    g.FillRectangle(brush, Location.X + (35 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, Location.X + (0 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    g.FillRectangle(brush, Location.X + (36 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, Location.X + (37 * ElementScale).Floor(), Location.Y, ElementScale, Size.Height);
                    break;
                }
                case SideLocation.TopLeft:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, Location.X, 0, Size.Width, Size.Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, Location.X, (2 * ElementScale).Floor(), Size.Width, ElementScale);
                    g.FillRectangle(brush, Location.X, (36 * ElementScale).Floor(), Size.Width, ElementScale);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    /*g.FillRectangle(brush, 0, (1 * ElementScale).Floor(), Size.Width, ElementScale);
                    g.FillRectangle(brush, 0, (37 * ElementScale).Floor(), Size.Width, ElementScale);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, 0, (0 * ElementScale).Floor(), Size.Width, ElementScale);*/
                    break;
                }

                case SideLocation.None:
                default:
                    break;
            }
        }
    }
}