namespace RyszardopolisPanelSterowania.Cells.Interfaces
{
    using RyszardopolisPanelSterowania.Controls;

    internal interface ITrack
    {
        public string TrackId { get; }

        public TrackStates TrackState { get; set; }

        public bool TrackHasGap { get; }

        public void OccupyTrack(DataChangedEventArgs e);
    }
}
