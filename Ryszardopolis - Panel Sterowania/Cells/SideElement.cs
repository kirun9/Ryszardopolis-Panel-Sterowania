namespace RyszardopolisPanelSterowania.Cells;

using System;
using System.Drawing;

using RyszardopolisPanelSterowania.Controls;

public class SideElement : Element
{
    private SideLocation side;

    public override Font Font => new Font(DefaultFont.FontFamily, 6, DefaultFont.Style, DefaultFont.Unit);

    public string Text { get; set; }

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
        switch (Side)
        {
            case SideLocation.Top:
            {
                FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                DrawLineRect(g, Colors.SideSecondary, 0, 2, Size.Width, 0);
                DrawLineRect(g, Colors.SideSecondary, 0, 37, Size.Width, 0);

                DrawLineRect(g, Colors.SideTrinnary, 0, 1, Size.Width, 0);
                DrawLineRect(g, Colors.SideTrinnary, 0, 38, Size.Width, 0);

                DrawLineRect(g, Colors.SideFilling, 0, 0, Size.Width, 0);

                DrawString(g, Text, Font, Colors.Black, ContentAlignment.BottomCenter);

                break;
            }

            case SideLocation.Bottom:
            {
                FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                DrawLineRect(g, Colors.SideSecondary, 0, 1, Size.Width, 0);
                DrawLineRect(g, Colors.SideSecondary, 0, 36, Size.Width, 0);

                DrawLineRect(g, Colors.SideTrinnary, 0, 0, Size.Width, 0);
                DrawLineRect(g, Colors.SideTrinnary, 0, 37, Size.Width, 0);

                DrawLineRect(g, Colors.SideFilling, 0, 38, Size.Width, 0);

                DrawString(g, Text, Font, Colors.Black, ContentAlignment.TopCenter);

                break;
            }

            case SideLocation.Left:
            {
                FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                DrawLineRect(g, Colors.SideSecondary, 2, 0, 0, Size.Height);
                DrawLineRect(g, Colors.SideSecondary, 37, 0, 0, Size.Height);

                DrawLineRect(g, Colors.SideTrinnary, 1, 0, 0, Size.Height);
                DrawLineRect(g, Colors.SideTrinnary, 38, 0, 0, Size.Height);

                DrawLineRect(g, Colors.SideFilling, 0, 0, 0, Size.Height);

                DrawString(g, Text, Font, Colors.Black, ContentAlignment.MiddleRight);

                break;
            }

            case SideLocation.Right:
            {
                FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width, Size.Height);

                DrawLineRect(g, Colors.SideSecondary, 1, 0, 0, Size.Height);
                DrawLineRect(g, Colors.SideSecondary, 36, 0, 0, Size.Height);

                DrawLineRect(g, Colors.SideTrinnary, 0, 0, 0, Size.Height);
                DrawLineRect(g, Colors.SideTrinnary, 37, 0, 0, Size.Height);

                DrawLineRect(g, Colors.SideFilling, 38, 0, 0, Size.Height);

                DrawString(g, Text, Font, Colors.Black, ContentAlignment.MiddleLeft);
                break;
            }

            case SideLocation.TopLeft:
            {
                FillRectangle(g, Colors.SideFilling, 0, 0, Size.Width, Size.Height);
                FillRectangle(g, Colors.SidePrimary, 2, 2, Size.Width, Size.Height);

                // Diagonal Line
                DrawLine(g, Colors.SideSecondary, 3, 3, Size.Width - 1, Size.Height - 1);

                DrawArc(g, Colors.SideSecondary, 2.5f, 2.5f, 2.5f, 2.5f, -180, 90);
                DrawArc(g, Colors.SideTrinnary, 1.5f, 1.5f, 5, 5, -180, 90);

                DrawLine(g, Colors.SideTrinnary, 4, 1, Size.Width, 1);
                DrawLine(g, Colors.SideTrinnary, 1, 4, 1, Size.Width); // Czarne linie

                DrawLine(g, Colors.SideSecondary, 4, 2, Size.Width, 2); // Szare Linie
                DrawLine(g, Colors.SideSecondary, 2, 4, 2, Size.Width);

                DrawRectangle(g, Colors.SideTrinnary, 37.5f, 37.5f, 0.5f, 0.5f);

                DrawLine(g, Colors.SideSecondary, 37, 37, 37, 38); // Small Corner
                DrawLine(g, Colors.SideSecondary, 37, 37, 38, 37);

                break;
            }

            case SideLocation.TopRight:
            {
                FillRectangle(g, Colors.SideFilling, 0, 0, Size.Width, Size.Height);
                FillRectangle(g, Colors.SidePrimary, 0, 2, Size.Width - 2, Size.Height - 2);

                // Diagonal Line
                DrawLine(g, Colors.SideSecondary, Size.Width - 3, 3, 1, Size.Height - 1);

                DrawArc(g, Colors.SideSecondary, 33f, 2.5f, 2.5f, 2.5f, -90, 90);
                DrawArc(g, Colors.SideTrinnary, 31.5f, 1.5f, 5, 5, -90, 90);

                DrawLine(g, Colors.SideTrinnary, 0, 1, Size.Width - 4, 1);
                DrawLine(g, Colors.SideTrinnary, Size.Width - 1, 4, Size.Width - 1, Size.Height);

                DrawLine(g, Colors.SideSecondary, 0, 2, Size.Width - 4, 2);
                DrawLine(g, Colors.SideSecondary, Size.Width - 2, 4, Size.Width - 2, Size.Height);

                DrawRectangle(g, Colors.SideTrinnary, 0.5f, 37.5f, 0.5f, 0.5f);

                DrawLine(g, Colors.SideSecondary, 1, 37, 1, 38);
                DrawLine(g, Colors.SideSecondary, 0, 37, 1, 37);

                break;
            }

            case SideLocation.BottomLeft:
            {
                FillRectangle(g, Colors.SideFilling, 0, 0, Size.Width, Size.Height);
                FillRectangle(g, Colors.SidePrimary, 2, 0, Size.Width - 2, Size.Height - 2);

                // Diagonal Line
                DrawLine(g, Colors.SideSecondary, Size.Width - 1, 1, 3, Size.Height - 3);

                DrawArc(g, Colors.SideSecondary, 2.5f, 33, 2.5f, 2.5f, -270, 90);
                DrawArc(g, Colors.SideTrinnary, 1.5f, 31.5f, 5, 5, -270, 90);

                DrawLine(g, Colors.SideTrinnary, 4, Size.Height - 1, Size.Width, Size.Height - 1);
                DrawLine(g, Colors.SideTrinnary, 1, 0, 1, Size.Width - 4); // Czarne linie

                DrawLine(g, Colors.SideSecondary, 4, Size.Height - 2, Size.Width, Size.Height - 2); // Szare Linie
                DrawLine(g, Colors.SideSecondary, 2, 0, 2, Size.Width - 4);

                DrawRectangle(g, Colors.SideTrinnary, 37.5f, 0.5f, 0.5f, 0.5f);

                DrawLine(g, Colors.SideSecondary, 37, 0, 37, 1); // Small Corner
                DrawLine(g, Colors.SideSecondary, 37, 1, 38, 1);

                break;
            }

            case SideLocation.BottomRight:
            {
                FillRectangle(g, Colors.SideFilling, 0, 0, Size.Width, Size.Height);
                FillRectangle(g, Colors.SidePrimary, 0, 0, Size.Width - 2, Size.Height - 2);

                // Diagonal Line
                DrawLine(g, Colors.SideSecondary, 1, 1, Size.Width - 3, Size.Height - 3);

                DrawArc(g, Colors.SideSecondary, 33f, 33f, 2.5f, 2.5f, 0, 90);
                DrawArc(g, Colors.SideTrinnary, 31.5f, 31.5f, 5, 5, 0, 90);

                DrawLine(g, Colors.SideTrinnary, 0, Size.Height - 1, Size.Width - 4, Size.Height - 1);
                DrawLine(g, Colors.SideTrinnary, Size.Width - 1, 0, Size.Width - 1, Size.Height - 4);

                DrawLine(g, Colors.SideSecondary, 0, Size.Height - 2, Size.Width - 4, Size.Height - 2);
                DrawLine(g, Colors.SideSecondary, Size.Width - 2, 0, Size.Width - 2, Size.Height - 4);

                DrawRectangle(g, Colors.SideTrinnary, 0.5f, 0.5f, 0.5f, 0.5f);

                DrawLine(g, Colors.SideSecondary, 1, 1, 1, 0);
                DrawLine(g, Colors.SideSecondary, 0, 1, 1, 1);

                break;
            }

            case SideLocation.None:
            default:
                break;
        }
    }

    protected override void DrawContent(Graphics g)
    {
    }
}
