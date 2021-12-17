namespace RyszardopolisPanelSterowania.Cells;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using RyszardopolisPanelSterowania.Cells.Interfaces;
using RyszardopolisPanelSterowania.Controls;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

public class DoubleJunction : Junction, IDoubleJunction
{
    public string SecondJunctionId { get; set; }
    public string ThirdTrackId { get; set; }
    public TrackStates ThirdTrackState { get; set; }
    public bool ThirdTrackHasGap { get; set; }

    public override void OcupyTrack(DataChangedEventArgs e)
    {
        if (e.DataName == MainTrackId)
        {
            MainTrackState = e.Value ? TrackStates.Occupied : (DigitalData.GetData(JunctionId + "_M") ? TrackStates.Junction : TrackStates.None);
            UpdateElement();
        }
        if (e.DataName == SecondTrackId)
        {
            SecondTrackState = e.Value ? TrackStates.Occupied : (DigitalData.GetData(JunctionId + "_S") || DigitalData.GetData(SecondJunctionId + "_S") ? TrackStates.Junction : TrackStates.None);
            UpdateElement();
        }
        if (e.DataName == ThirdTrackId)
        {
            ThirdTrackState = e.Value ? TrackStates.Occupied : (DigitalData.GetData(SecondJunctionId + "_M") ? TrackStates.Junction : TrackStates.None);
            UpdateElement();
        }
    }

    public override void SwitchTrack(DataChangedEventArgs e)
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
        if (e.DataName == JunctionId + "_S" || e.DataName == SecondJunctionId + "_S")
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
        if (e.DataName == SecondJunctionId + "_M")
        {
            if (e.Value)
            {
                if (ThirdTrackState == TrackStates.None)
                {
                    ThirdTrackState = TrackStates.Junction;
                }
            }
            else
            {
                if (ThirdTrackState == TrackStates.None)
                {
                    ThirdTrackState = TrackStates.Junction;
                }
            }
        }

        if (e.DataName.StartsWith(JunctionId))
        {
            if (e.DataName == JunctionId + "_M")
                Pulpit.SendData(SecondJunctionId + "_JM", e.Value);
            if (e.DataName == JunctionId + "_S")
                Pulpit.SendData(SecondJunctionId + "_JS", e.Value);
        }
        if (e.DataName.StartsWith(SecondJunctionId))
        {
            if (e.DataName == SecondJunctionId + "_M")
                Pulpit.SendData(JunctionId + "_JM", e.Value);
            if (e.DataName == SecondJunctionId + "_S")
                Pulpit.SendData(JunctionId + "_JS", e.Value);
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

        Color tGapColor = ThirdTrackState switch
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

        if (MainTrackHasGap)
        {
            FillPath(g, mGapColor, path);
        }

        if (SecondTrackHasGap)
        {
            FillRectangle(g, sGapColor, 5, 18, 29, 3);
            if (SecondTrackState != TrackStates.None)
            {
                DrawImage(g, "GapBigAlpha.bmp", 5, 18, 29, 3);
            }
        }

        path.Dispose();
    }
}
