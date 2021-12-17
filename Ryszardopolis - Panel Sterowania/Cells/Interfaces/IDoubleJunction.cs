namespace RyszardopolisPanelSterowania.Cells.Interfaces;

public interface IDoubleJunction : IJunction
{
    public string SecondJunctionId { get; set; }

    public string ThirdTrackId { get; set; }

    public TrackStates ThirdTrackState { get; set; }

    public bool ThirdTrackHasGap { get; set; }
}
