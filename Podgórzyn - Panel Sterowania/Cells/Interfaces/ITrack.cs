namespace RyszardopolisPanelSterowania.Cells.Interfaces
{
    internal interface ITrack
    {
        public string TrackId { get; }

        public TrackStates TrackState { get; set; }

        public bool TrackHasGap { get; }
    }
}
