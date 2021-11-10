namespace PodgórzynPanelSterowania.Controls.Cells
{
    using System;
    using System.Drawing;

    public class SideElement : Element
    {
        private SideLocation side;

        public override Font Font => new Font(DefaultFont.FontFamily, 6, DefaultFont.Style, DefaultFont.Unit);

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
                    FillRectangle(g, Colors.SidePrimary, 0, 0, USize.Width, USize.Height);

                    DrawLineRect(g, Colors.SideSecondary, 0, 2, USize.Width, 0);
                    DrawLineRect(g, Colors.SideSecondary, 0, 36, USize.Width, 0);

                    DrawLineRect(g, Colors.SideTrinnary, 0, 1, USize.Width, 0);
                    DrawLineRect(g, Colors.SideTrinnary, 0, 37, USize.Width, 0);

                    DrawLineRect(g, Colors.SideFilling, 0, 0, USize.Width, 0);

                    DrawString(g, GridLocation.X.ToString("D2"), Font, Colors.Black, ContentAlignment.BottomCenter);

                    break;
                }

                case SideLocation.Bottom:
                {
                    FillRectangle(g, Colors.SidePrimary, 0, 0, USize.Width, USize.Height);

                    DrawLineRect(g, Colors.SideSecondary, 0, 1, USize.Width, 0);
                    DrawLineRect(g, Colors.SideSecondary, 0, 37, USize.Width, 0);

                    DrawLineRect(g, Colors.SideTrinnary, 0, 0, USize.Width, 0);
                    DrawLineRect(g, Colors.SideTrinnary, 0, 36, USize.Width, 0);

                    DrawLineRect(g, Colors.SideFilling, 0, 37, USize.Width, 0);

                    DrawString(g, GridLocation.X.ToString("D2"), Font, Colors.Black, ContentAlignment.TopCenter);

                    break;
                }

                case SideLocation.Left:
                {
                    FillRectangle(g, Colors.SidePrimary, 0, 0, USize.Width, USize.Height);

                    DrawLineRect(g, Colors.SideSecondary, 2, 0, 0, USize.Height);
                    DrawLineRect(g, Colors.SideSecondary, 36, 0, 0, USize.Height);

                    DrawLineRect(g, Colors.SideTrinnary, 1, 0, 0, USize.Height);
                    DrawLineRect(g, Colors.SideTrinnary, 37, 0, 0, USize.Height);

                    DrawLineRect(g, Colors.SideFilling, 0, 0, 0, USize.Height);

                    DrawString(g, GridLocation.Y.ToString("D2"), Font, Colors.Black, ContentAlignment.MiddleRight);

                    break;
                }

                case SideLocation.Right:
                {
                    FillRectangle(g, Colors.SidePrimary, 0, 0, USize.Width, USize.Height);

                    DrawLineRect(g, Colors.SideSecondary, 1, 0, 0, USize.Height);
                    DrawLineRect(g, Colors.SideSecondary, 35, 0, 0, USize.Height);

                    DrawLineRect(g, Colors.SideTrinnary, 0, 0, 0, USize.Height);
                    DrawLineRect(g, Colors.SideTrinnary, 36, 0, 0, USize.Height);

                    DrawLineRect(g, Colors.SideFilling, 37, 0, 0, USize.Height);

                    DrawString(g, GridLocation.Y.ToString("D2"), Font, Colors.Black, ContentAlignment.MiddleLeft);
                    break;
                }

                case SideLocation.TopLeft:
                {
                    FillRectangle(g, Colors.SideFilling, 0, 0, USize.Width, USize.Height);

                    FillRectangle(g, Colors.SidePrimary, 2, 2, USize.Width, USize.Height);

                    DrawLineRect(g, Colors.SideFilling, 0, 0, USize.Width, 0);
                    DrawLineRect(g, Colors.SideFilling, 0, 0, 0, USize.Height);

                    DrawLine(g, Colors.SideSecondary, 3, 3, USize.Width - 1, USize.Height - 1);

                    DrawLine(g, Colors.SideTrinnary, 2, 2, 3, 2);
                    DrawLine(g, Colors.SideTrinnary, 2, 2, 2, 3);

                    DrawLine(g, Colors.SideTrinnary, 4, 1, USize.Width, 1);
                    DrawLine(g, Colors.SideTrinnary, 1, 4, 1, USize.Width);

                    DrawLine(g, Colors.SideSecondary, 4, 2, USize.Width, 2);
                    DrawLine(g, Colors.SideSecondary, 2, 4, 2, USize.Width);

                    FillRectangle(g, Colors.SideSecondary, 36, 36, 2, 2);
                    FillRectangle(g, Colors.SideTrinnary, 37, 37, 1, 1);
                    break;
                }

                case SideLocation.TopRight:
                {
                    FillRectangle(g, Colors.SideFilling, 0, 0, USize.Width, USize.Height);

                    FillRectangle(g, Colors.SidePrimary, 0, 2, USize.Width - 2, USize.Height - 2);

                    DrawLineRect(g, Colors.SideFilling, 0, 0, USize.Width, 0);
                    DrawLineRect(g, Colors.SideFilling, USize.Width, 0, USize.Width, USize.Height);

                    DrawLine(g, Colors.SideSecondary, USize.Width - 4, 3, 1, USize.Height - 2);

                    DrawLine(g, Colors.SideTrinnary, USize.Width - 3, 2, USize.Width - 4, 2);
                    DrawLine(g, Colors.SideTrinnary, USize.Width - 3, 2, USize.Width - 3, 3);

                    DrawLine(g, Colors.SideTrinnary, 0, 1, USize.Width - 5, 1);
                    DrawLine(g, Colors.SideTrinnary, USize.Width - 2, 4, USize.Width - 2, USize.Width);

                    DrawLine(g, Colors.SideSecondary, 0, 2, USize.Width - 5, 2);
                    DrawLine(g, Colors.SideSecondary, USize.Width - 3, 4, USize.Width - 3, USize.Width);

                    FillRectangle(g, Colors.SideSecondary, 0, 36, 2, 2);
                    FillRectangle(g, Colors.SideTrinnary, 0, 37, 1, 1);
                    break;
                }

                case SideLocation.None:
                default:
                    break;
            }
        }
    }
}