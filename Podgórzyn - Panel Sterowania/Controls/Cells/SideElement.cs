namespace PodgórzynPanelSterowania.Controls.Cells
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
using PodgórzynPanelSterowania.Extensions;

    public class SideElement : Element
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

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;
            switch (Side)
            {
                case SideLocation.Top:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, 0, 0, Width, Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, 0, (2 * ElementScale).Floor(), Width, ElementScale);
                    g.FillRectangle(brush, 0, (36 * ElementScale).Floor(), Width, ElementScale);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, 0, (1 * ElementScale).Floor(), Width, ElementScale);
                    g.FillRectangle(brush, 0, (37 * ElementScale).Floor(), Width, ElementScale);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, 0, (0 * ElementScale).Floor(), Width, ElementScale);
                    break;
                }
                case SideLocation.Bottom:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, 0, 0, Width, Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, 0, (1 * ElementScale).Floor(), Width, ElementScale);
                    g.FillRectangle(brush, 0, (35 * ElementScale).Floor(), Width, ElementScale);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, 0, (0 * ElementScale).Floor(), Width, ElementScale);
                    g.FillRectangle(brush, 0, (36 * ElementScale).Floor(), Width, ElementScale);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, 0, (37 * ElementScale).Floor(), Width, ElementScale);
                    break;
                }
                case SideLocation.Left:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, 0, 0, Width, Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, (2 * ElementScale).Floor(), 0, ElementScale, Height);
                    g.FillRectangle(brush, (36 * ElementScale).Floor(), 0, ElementScale, Height);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, (1 * ElementScale).Floor(), 0, ElementScale, Height);
                    g.FillRectangle(brush, (37 * ElementScale).Floor(), 0, ElementScale, Height);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, (0 * ElementScale).Floor(), 0, ElementScale, Height);
                    break;
                }
                case SideLocation.Right:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, 0, 0, Width, Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, (1 * ElementScale).Floor(), 0, ElementScale, Height);
                    g.FillRectangle(brush, (35 * ElementScale).Floor(), 0, ElementScale, Height);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    g.FillRectangle(brush, (0 * ElementScale).Floor(), 0, ElementScale, Height);
                    g.FillRectangle(brush, (36 * ElementScale).Floor(), 0, ElementScale, Height);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, (37 * ElementScale).Floor(), 0, ElementScale, Height);
                    break;
                }
                case SideLocation.TopLeft:
                {
                    using SolidBrush brush = new SolidBrush(Colors.SidePrimary.ToColor());
                    g.FillRectangle(brush, 0, 0, Width, Height);
                    brush.Color = Colors.SideScondary.ToColor();
                    g.FillRectangle(brush, 0, (2 * ElementScale).Floor(), Width, ElementScale);
                    g.FillRectangle(brush, 0, (36 * ElementScale).Floor(), Width, ElementScale);
                    brush.Color = Colors.SideTrinnary.ToColor();
                    /*g.FillRectangle(brush, 0, (1 * ElementScale).Floor(), Width, ElementScale);
                    g.FillRectangle(brush, 0, (37 * ElementScale).Floor(), Width, ElementScale);
                    brush.Color = Colors.SideFilling.ToColor();
                    g.FillRectangle(brush, 0, (0 * ElementScale).Floor(), Width, ElementScale);*/
                    break;
                }

                case SideLocation.None:
                default:
                    break;
            }
        }
    }
}