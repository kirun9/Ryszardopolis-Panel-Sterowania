namespace RyszardopolisPanelSterowania.Cells
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    using RyszardopolisPanelSterowania.Controls;
    using RyszardopolisPanelSterowania.Utils;

    public partial class Element
    {
        public event EventHandler ElementUpdated;

        private Font font;
        private readonly Font defaultFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point);
        private Point gridLocation;
        private Size size;
        private Point location;
        private RotateFlipType elementRotation;

        internal readonly Color EmptyGap = 0x333333.ToColor();
        internal readonly Color WhiteGap = 0xFAFBEA.ToColor();
        internal readonly Color YellowGap = 0xFECE3C.ToColor();
        internal readonly Color RedGap = 0xFC4E44.ToColor();

        private static Brush textureBrush;
        internal static Brush TextureBrush
        {
            get
            {
                if (textureBrush is null)
                    //textureBrush = new TextureBrush(Noise.GenerateNoise(512, 1, max: byte.MaxValue / 2), System.Drawing.Drawing2D.WrapMode.Clamp, new Rectangle(0, 0, 512, 512));
                    textureBrush = new TextureBrush(Noise.NoiseFromImage(), System.Drawing.Drawing2D.WrapMode.Clamp, new RectangleF(0, 0, 512, 512));
                return textureBrush;
            }
        }

        public Point GridLocation
        {
            get
            {
                return gridLocation;
            }

            set
            {
                gridLocation = value;
            }
        }

        public Size Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public bool DrawBottomBigger { get; set; }

        public bool DrawRightBigger { get; set; }

        public Point Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public virtual Font Font
        {
            get
            {
                return font;
            }

            set
            {
                font = value;
                UpdateElement();
            }
        }
        public Font DefaultFont => defaultFont;

        public int Width => Size.Width;
        public int Height => Size.Height;

        public RotateFlipType ElementRotation
        {
            get
            {
                return elementRotation;
            }

            set
            {
                elementRotation = value;
                UpdateElement();
            }
        }

        private void UpdateElement()
        {
            ElementUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void DrawCell(Graphics g)
        {
            DrawBorder(g);
            var transform = RotateElement(g);
            DrawContent(g);
            g.Transform = transform;
        }

        protected virtual void DrawBorder(Graphics g)
        {
            FillRectangle(g, Colors.Background, 0, 0, Size.Width, Size.Height);

            DrawLine(g, Colors.BorderMain, 0, 0, 0, Size.Height);
            DrawLine(g, Colors.BorderMain, 0, 0, Size.Width, 0);

            DrawLineRect(g, Colors.BorderSecond, 0, 12, 0, 4);
            DrawLineRect(g, Colors.BorderSecond, 0, 23, 0, 4);
            DrawLineRect(g, Colors.BorderSecond, 12, 0, 4, 0);
            DrawLineRect(g, Colors.BorderSecond, 23, 0, 4, 0);

            if (DrawBottomBigger)
            {
                DrawLineRect(g, Colors.BorderMain, 0, Size.Height, Size.Width, 0);
                DrawLineRect(g, Colors.BorderSecond, 12, Size.Height, 4, 0);
                DrawLineRect(g, Colors.BorderSecond, 23, Size.Height, 4, 0);
            }

            if (DrawRightBigger)
            {
                DrawLineRect(g, Colors.BorderMain, Size.Width, 0, 0, Size.Height);
                DrawLineRect(g, Colors.BorderSecond, Size.Width, 12, 0, 4);
                DrawLineRect(g, Colors.BorderSecond, Size.Width, 23, 0, 4);
            }
        }

        protected virtual void DrawContent(Graphics g)
        {
        }

        private Matrix RotateElement(Graphics g)
        {
            var transform = g.Transform;

            switch (ElementRotation)
            {
                case RotateFlipType.RotateNoneFlipNone:
                {
                    g.RotateTransform(0);
                    g.ScaleTransform(1, 1);
                    g.TranslateTransform(0, 0);
                    break;
                }
                case RotateFlipType.Rotate90FlipNone:
                {
                    g.RotateTransform(90, MatrixOrder.Prepend);
                    g.ScaleTransform(1, 1);
                    g.TranslateTransform(0, -Size.Height - 1);
                    break;
                }
                case RotateFlipType.Rotate180FlipNone:
                {
                    g.RotateTransform(180, MatrixOrder.Prepend);
                    g.ScaleTransform(1, 1);
                    g.TranslateTransform(-Size.Width - 1, -Size.Height - 1);
                    break;
                }
                case RotateFlipType.Rotate270FlipNone:
                {
                    g.RotateTransform(270, MatrixOrder.Prepend);
                    g.ScaleTransform(1, 1);
                    g.TranslateTransform(-Size.Width - 1, 0);
                    break;
                }
                case RotateFlipType.RotateNoneFlipX:
                {
                    g.RotateTransform(0);
                    g.ScaleTransform(-1, 1, MatrixOrder.Prepend);
                    g.TranslateTransform(-Size.Width - 1, 0);
                    break;
                }
                case RotateFlipType.Rotate90FlipX:
                {
                    g.RotateTransform(90, MatrixOrder.Prepend);
                    g.ScaleTransform(-1, 1, MatrixOrder.Prepend);
                    g.TranslateTransform(-Size.Width - 1, -Size.Height - 1);
                    break;
                }
                case RotateFlipType.Rotate180FlipX:
                {
                    g.RotateTransform(180, MatrixOrder.Prepend);
                    g.ScaleTransform(-1, 1, MatrixOrder.Prepend);
                    g.TranslateTransform(0, -Size.Height - 1);
                    break;
                }
                case RotateFlipType.Rotate270FlipX:
                {
                    g.RotateTransform(270, MatrixOrder.Prepend);
                    g.ScaleTransform(-1, 1, MatrixOrder.Prepend);
                    g.TranslateTransform(0, 0);
                    break;
                }
            }

            return transform;
        }
    }
}
