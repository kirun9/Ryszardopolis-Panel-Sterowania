namespace RyszardopolisPanelSterowania.Cells;

using System.Drawing;
using System.Drawing.Drawing2D;

using RyszardopolisPanelSterowania.Cells.Interfaces;
using RyszardopolisPanelSterowania.Controls;

public class Junction : Element, IJunction
{
    public string JunctionId { get; set; } = "EmptyJunction";
    public string MainTrackId { get; set; } = "EmptyElement";
    public string SecondTrackId { get; set; } = "EmptyElement";
    public TrackStates MainTrackState { get; set; } = TrackStates.Junction;
    public TrackStates SecondTrackState { get; set; }

    public bool MainTrackHasGap { get; set; } = true;
    public bool SecondTrackHasGap { get; set; } = true;

    internal readonly Color Border = 0x919191.ToColor();
    internal readonly Color Background = 0xB4B4B4.ToColor();

    internal static TextureBrush bigGapBrush;
    internal static TextureBrush BigGapBrush
    {
        get
        {
            if (bigGapBrush == null)
                bigGapBrush = new TextureBrush(Bitmap.FromFile("GapBigAlpha.bmp"), WrapMode.Clamp);
            return bigGapBrush;
        }
    }

    public Junction()
    {
    }

    public virtual void OcupyTrack(DataChangedEventArgs e)
    {
        if (e.DataName == MainTrackId)
        {
            MainTrackState = e.Value ? TrackStates.Occupied : (DigitalData.GetData(JunctionId + "_M") ? TrackStates.Junction : TrackStates.None);
            UpdateElement();
        }
        if (e.DataName == SecondTrackId)
        {
            SecondTrackState = e.Value ? TrackStates.Occupied : (DigitalData.GetData(JunctionId + "_S") ? TrackStates.Junction : TrackStates.None);
            UpdateElement();
        }
    }

    public virtual void SwitchTrack(DataChangedEventArgs e)
    {
        if (e.DataName == JunctionId + "_M")
        {
            if (e.Value)
            {
                if (MainTrackState == TrackStates.None)
                {
                    MainTrackState = TrackStates.Junction;
                }
            }
            else
            {
                if (MainTrackState == TrackStates.Junction)
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
                    SecondTrackState = TrackStates.Junction;
                }
            }
            else
            {
                if (SecondTrackState == TrackStates.Junction)
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
            TrackStates.Junction => YellowGap,
            _ => EmptyGap,
        };

        Color sGapColor = SecondTrackState switch
        {
            TrackStates.None => EmptyGap,
            TrackStates.Locked => WhiteGap,
            TrackStates.Occupied => RedGap,
            TrackStates.Junction => YellowGap,
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
        path.Dispose();
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
        path.Dispose();
    }

    protected override void Dispose(bool dispose)
    {
        base.Dispose(dispose);

        if (dispose)
        {
            if (bigGapBrush != null)
            {
                bigGapBrush.Dispose();
                bigGapBrush = null;
            }
        }
    }
}
