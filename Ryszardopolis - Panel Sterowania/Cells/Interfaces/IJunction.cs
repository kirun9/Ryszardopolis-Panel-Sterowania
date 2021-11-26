namespace RyszardopolisPanelSterowania.Cells.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RyszardopolisPanelSterowania.Controls;

    internal interface IJunction
    {
        public string JunctionId { get; set; }

        public string MainTrackId { get; set; }
        public string SecondTrackId { get; set; }

        public TrackStates MainTrackState { get; set; }
        public TrackStates SecondTrackState { get; set; }

        public bool MainTrackHasGap { get; set; }
        public bool SecondTrackHasGap { get; set; }

        public void OcupyTrack(DataChangedEventArgs e);
        public void SwitchTrack(DataChangedEventArgs e);
    }
}
