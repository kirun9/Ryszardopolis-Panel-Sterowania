namespace RyszardopolisPanelSterowania.Cells
{
    using System.Drawing;

    using RyszardopolisPanelSterowania.Controls;
    using RyszardopolisPanelSterowania.Cells.Interfaces;

    internal class Track : Element, ITrack
    {
        public System.String TrackId { get; set; }
        public TrackStates TrackState { get; set; }
        public System.Boolean TrackHasGap { get; }

        public RotateFlipType Rotation { get; set; } = RotateFlipType.Rotate180FlipNone;

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
            ElementRotation = RotateFlipType.Rotate270FlipX;
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

            DrawLine(g, Colors.Red, 1, 1, Size.Width, 3, false);

            FillRectangle(g, Background, 1, 16, Size.Width - 1, 7);
            DrawLine(g, Border, 1, 16, Size.Width, 16);
            DrawLine(g, Border, 1, 23, Size.Width, 23);

            FillRectangle(g, gapColor, 5, 18, Size.Width - 10, 3, false);
            if (TrackState != TrackStates.None)
            {
                DrawImage(g, "GapBigAlpha.bmp", 5, 18, Size.Width - 10, 3);
            }
        }
    }
}
