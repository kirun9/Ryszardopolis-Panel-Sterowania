namespace RyszardopolisPanelSterowania.Cells
{
    using System.Drawing;

    using RyszardopolisPanelSterowania.Controls;
    using RyszardopolisPanelSterowania.Cells.Interfaces;
    using System.Drawing.Drawing2D;

    internal class Track : Element, ITrack
    {
        public System.String TrackId { get; set; }
        public TrackStates TrackState { get; set; }
        public System.Boolean TrackHasGap { get; }
        public bool IsDiagonal { get; set; } = false;

        private readonly Color Border = 0x919191.ToColor();
        private readonly Color Background = 0xB4B4B4.ToColor();

        private static TextureBrush bigGapBrush;
        private static TextureBrush BigGapBrush
        {
            get
            {
                if (bigGapBrush == null)
                    bigGapBrush = new TextureBrush(Bitmap.FromFile("GapBigAlpha.bmp"), System.Drawing.Drawing2D.WrapMode.Clamp);
                return bigGapBrush;
            }
        }

        public Track()
        {
            ElementRotation = RotateFlipType.RotateNoneFlipNone;
        }

        public void OccupyTrack(DataChangedEventArgs e)
        {
            if (e.DataName == TrackId)
            {
                TrackState = e.Value ? TrackStates.Occupied : TrackStates.None;
                UpdateElement();
            }
        }

        protected override void DrawContent(Graphics g)
        {
            base.DrawContent(g);

            Color gapColor = TrackState switch
            {
                TrackStates.None => EmptyGap,
                TrackStates.Locked => WhiteGap,
                TrackStates.Occupied => RedGap,
                TrackStates.Juntion => YellowGap,
                _ => EmptyGap,
            };

            if (!IsDiagonal)
            {
                FillRectangle(g, Background, 1, 16, Size.Width - 1, 7);
                DrawLine(g, Border, 1, 16, Size.Width, 16);
                DrawLine(g, Border, 1, 23, Size.Width, 23);

                FillRectangle(g, gapColor, 5, 18, Size.Width - 10, 3, false);

                using Pen pen = new Pen(Border, 1);
                pen.StartCap = LineCap.Square;
                pen.EndCap = LineCap.Square;
                pen.Alignment = PenAlignment.Inset;

                DrawArc(g, pen, 1.5f, 18, 3, 3, 0, 360, false);
                DrawArc(g, pen, Size.Width - 4, 18, 3, 3, 0, 360, false);
                if (TrackState != TrackStates.None)
                {
                    DrawImage(g, "GapBigAlpha.bmp", 5, 18, Size.Width - 10, 3);
                }
            }
            else
            {
                GraphicsPath path = new GraphicsPath();
                path.AddLine(1, 16, 23, Height);
                path.AddLine(23, Height, 16, Height);
                path.AddLine(16, Height, 1, 23);
                path.AddLine(1, 23, 1, 16);

                FillPath(g, Background, path);
                DrawPath(g, Border, path);

                path = new GraphicsPath();
                path.AddLine(5, 22, 15, Height - 6);
                path.AddLine(15, Height - 6, 15, Height - 3);
                path.AddLine(15, Height - 3, 5, 25);
                path.AddLine(5, 25, 5, 22);

                FillPath(g, gapColor, path);

                using Pen pen = new Pen(Border, 1);
                pen.StartCap = LineCap.Square;
                pen.EndCap = LineCap.Square;
                pen.Alignment = PenAlignment.Inset;

                DrawArc(g, pen, 1.5f, 20, 3, 3, 0, 360, false);
                DrawArc(g, pen, 15.5f, Size.Height - 4, 3, 3, 0, 360, false);
            }
        }
    }
}
