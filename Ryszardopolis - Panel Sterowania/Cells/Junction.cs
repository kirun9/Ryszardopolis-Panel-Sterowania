namespace RyszardopolisPanelSterowania.Cells
{
using System.Drawing;
    using System.Drawing.Drawing2D;

    using RyszardopolisPanelSterowania.Cells.Interfaces;
    using RyszardopolisPanelSterowania.Controls;

    public class Junction : Element, IJunction
    {
        public System.String JunctionId { get; set; } = "EmptyJunction";
        public System.String MainTrackId { get; set; } = "EmptyElement";
        public System.String SecondTrackId { get; set; } = "EmptyElement";
        public TrackStates MainTrackState { get; set; } = TrackStates.Juntion;
        public TrackStates SecondTrackState { get; set; }

        public System.Boolean MainTrackHasGap { get; set; } = true;
        public System.Boolean SecondTrackHasGap { get; set; } = true;

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

        public Junction()
        {
        }

        public void OcupyTrack(DataChangedEventArgs e)
        {
            if (e.DataName == MainTrackId)
            {
                MainTrackState = e.Value ? TrackStates.Occupied : (DigitalData.GetData(JunctionId + "_M") ? TrackStates.Juntion : TrackStates.None);
                UpdateElement();
            }
            if (e.DataName == SecondTrackId)
            {
                SecondTrackState = e.Value ? TrackStates.Occupied : (DigitalData.GetData(JunctionId + "_S") ? TrackStates.Juntion : TrackStates.None);
                UpdateElement();
            }
        }

        public void SwitchTrack(DataChangedEventArgs e)
        {
            if (e.DataName == JunctionId + "_M")
            {
                if (e.Value)
                {
                    if (MainTrackState == TrackStates.None)
                    {
                        MainTrackState = TrackStates.Juntion;
                    }
                }
                else
                {
                    if (MainTrackState == TrackStates.Juntion)
                    {
                        MainTrackState = TrackStates.None;
                    }
                }
            }
            if (e.DataName == JunctionId + "_S")
            {
                if (e.Value)
                {
                    if (SecondTrackState == TrackStates.None)
                    {
                        SecondTrackState = TrackStates.Juntion;
                    }
                }
                else
                {
                    if (SecondTrackState == TrackStates.Juntion)
                    {
                        SecondTrackState = TrackStates.None;
                    }
                }
            }
        }

        protected override void DrawContent(Graphics g)
        {
            base.DrawContent(g);

            Color mGapColor = MainTrackState switch
            {
                TrackStates.None => EmptyGap,
                TrackStates.Locked => WhiteGap,
                TrackStates.Occupied => RedGap,
                TrackStates.Juntion => YellowGap,
                _ => EmptyGap,
            };

            Color sGapColor = SecondTrackState switch
            {
                TrackStates.None => EmptyGap,
                TrackStates.Locked => WhiteGap,
                TrackStates.Occupied => RedGap,
                TrackStates.Juntion => YellowGap,
                _ => EmptyGap,
            };

            GraphicsPath path = new GraphicsPath();
            path.AddLine(15, 0, 30, 15);
            path.AddLine(30, 15, 30, 16);
            path.AddLine(30, 16, 0, 16);
            //path.AddLine(0, 16, 0, 22);
            path.AddLine(0, 23, 38, 23);
            //path.AddLine(38, 22, 38, 15);
            path.AddLine(38, 15, 23, 0);
            path.AddLine(23, 0, 15, 0);

            FillPath(g, Background, path);

            DrawPath(g, Border, path);

            path = new GraphicsPath();
            path.AddLine(22, 5, 26, 5);
            path.AddLine(26, 5, 35, 14);
            path.AddLine(35, 14, 31, 14);
            path.AddLine(31, 14, 22, 5);

            if (SecondTrackHasGap)
            {
                FillPath(g, sGapColor, path);
            }

            if (MainTrackHasGap)
            {
                FillRectangle(g, mGapColor, 5, 18, 29, 3);
                if (MainTrackState != TrackStates.None)
                {
                    DrawImage(g, "GapBigAlpha.bmp", 5, 18, 29, 3);
                }
            }
        }
    }
}
