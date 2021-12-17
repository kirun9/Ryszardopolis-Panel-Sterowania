namespace RyszardopolisPanelSterowania.Cells.Interfaces;

using System.Xml.Serialization;

using RyszardopolisPanelSterowania.Controls;

public interface ITrack
{
    [XmlElement("Id")]
    public string TrackId { get; }

    public TrackStates TrackState { get; set; }

    [XmlAttribute("HasGap")]
    public bool TrackHasGap { get; }

    public void OccupyTrack(DataChangedEventArgs e);
}
