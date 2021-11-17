namespace RyszardopolisPanelSterowania.Controls.Cells
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

        protected override void DrawBorder(Graphics g)
        {
        }

        protected override void DrawContent(Graphics g)
        {
            switch (Side)
            {
                case SideLocation.Top:
                {
                    FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                    DrawLineRect(g, Colors.SideSecondary, 0, 2, Size.Width, 0);
                    DrawLineRect(g, Colors.SideSecondary, 0, 36, Size.Width, 0);

                    DrawLineRect(g, Colors.SideTrinnary, 0, 1, Size.Width, 0);
                    DrawLineRect(g, Colors.SideTrinnary, 0, 37, Size.Width, 0);

                    DrawLineRect(g, Colors.SideFilling, 0, 0, Size.Width, 0);

                    DrawString(g, GridLocation.X.ToString("D2"), Font, Colors.Black, ContentAlignment.BottomCenter);

                    DrawLine(g, Colors.Red, Size.Width, 0, Size.Width, Size.Height);

                    break;
                }

                case SideLocation.Bottom:
                {
                    FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                    DrawLineRect(g, Colors.SideSecondary, 0, 1, Size.Width, 0);
                    DrawLineRect(g, Colors.SideSecondary, 0, 37, Size.Width, 0);

                    DrawLineRect(g, Colors.SideTrinnary, 0, 0, Size.Width, 0);
                    DrawLineRect(g, Colors.SideTrinnary, 0, 36, Size.Width, 0);

                    DrawLineRect(g, Colors.SideFilling, 0, 37, Size.Width, 0);

                    DrawString(g, GridLocation.X.ToString("D2"), Font, Colors.Black, ContentAlignment.TopCenter);

                    break;
                }

                case SideLocation.Left:
                {
                    FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                    DrawLineRect(g, Colors.SideSecondary, 2, 0, 0, Size.Height);
                    DrawLineRect(g, Colors.SideSecondary, 36, 0, 0, Size.Height);

                    DrawLineRect(g, Colors.SideTrinnary, 1, 0, 0, Size.Height);
                    DrawLineRect(g, Colors.SideTrinnary, 37, 0, 0, Size.Height);

                    DrawLineRect(g, Colors.SideFilling, 0, 0, 0, Size.Height);

                    DrawString(g, GridLocation.Y.ToString("D2"), Font, Colors.Black, ContentAlignment.MiddleRight);

                    break;
                }

                case SideLocation.Right:
                {
                    FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                    DrawLineRect(g, Colors.SideSecondary, 1, 0, 0, Size.Height);
                    DrawLineRect(g, Colors.SideSecondary, 35, 0, 0, Size.Height);

                    DrawLineRect(g, Colors.SideTrinnary, 0, 0, 0, Size.Height);
                    DrawLineRect(g, Colors.SideTrinnary, 36, 0, 0, Size.Height);

                    DrawLineRect(g, Colors.SideFilling, 37, 0, 0, Size.Height);

                    DrawString(g, GridLocation.Y.ToString("D2"), Font, Colors.Black, ContentAlignment.MiddleLeft);
                    break;
                }

                case SideLocation.TopLeft:
                {
                    FillRectangle(g, Colors.SideFilling, 0, 0, Size.Width, Size.Height);
                    FillRectangle(g, Colors.SidePrimary, 2, 2, Size.Width, Size.Height);

                    // Diagonal Line
                    DrawLine(g, Colors.SideSecondary, 3, 3, Size.Width - 1, Size.Height - 1);

                    using Pen pen = new Pen(Colors.SideSecondary.ToColor(), 1);
                    pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
                    g.DrawArc(pen, 2, 2, 2.5f, 2.5f, -180, 90);
                    pen.Color = Colors.SideTrinnary.ToColor();
                    g.DrawArc(pen, 1, 1, 5, 5, -180, 90);

                    DrawLine(g, Colors.SideTrinnary, 4, 1, Size.Width, 1);
                    DrawLine(g, Colors.SideTrinnary, 1, 4, 1, Size.Width);

                    DrawLine(g, Colors.SideSecondary, 4, 2, Size.Width, 2);
                    DrawLine(g, Colors.SideSecondary, 2, 4, 2, Size.Width);

                    DrawLine(g, Colors.SideSecondary, 36, 36, 36, 38); //// Small Corner
                    DrawLine(g, Colors.SideSecondary, 36, 36, 38, 36);

                    DrawRectangle(g, Colors.SideTrinnary, 37, 37, 1, 1);
                    break;
                }

                case SideLocation.TopRight:
                {
                    FillRectangle(g, Colors.SideFilling, 0, 0, Size.Width, Size.Height);
                    FillRectangle(g, Colors.SidePrimary, 0, 2, Size.Width - 2, Size.Height - 2);

                    // Diagonal Line
                    DrawLine(g, Colors.SideSecondary, Size.Width - 3, 3, 1, Size.Height - 1);


                    //DrawLineRect(g, Colors.SideFilling, 0, 0, Size.Width, 0);
                    ////DrawLineRect(g, Colors.SideFilling, Size.Width, 0, Size.Width, Size.Height);
                    //
                    //DrawLine(g, Colors.SideSecondary, Size.Width - 4, 3, 1, Size.Height - 2);
                    //
                    //DrawLine(g, Colors.SideTrinnary, Size.Width - 3, 2, Size.Width - 4, 2);
                    //DrawLine(g, Colors.SideTrinnary, Size.Width - 3, 2, Size.Width - 3, 3);
                    //
                    //DrawLine(g, Colors.SideTrinnary, 0, 1, Size.Width - 5, 1);
                    //DrawLine(g, Colors.SideTrinnary, Size.Width - 2, 4, Size.Width - 2, Size.Width);
                    //
                    //DrawLine(g, Colors.SideSecondary, 0, 2, Size.Width - 5, 2);
                    //DrawLine(g, Colors.SideSecondary, Size.Width - 3, 4, Size.Width - 3, Size.Width);
                    //
                    //FillRectangle(g, Colors.SideSecondary, 0, 36, 2, 2);
                    //FillRectangle(g, Colors.SideTrinnary, 0, 37, 1, 1);
                    break;
                }

                case SideLocation.None:
                default:
                    break;
            }
        }
    }
}