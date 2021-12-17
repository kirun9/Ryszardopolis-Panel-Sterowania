namespace RyszardopolisPanelSterowania.Cells.Interfaces;

using System.Xml.Serialization;

public interface IDoubleTrack : ITrack
{
    [XmlElement("Second ID")]
    public string SecondTrackId { get; }

    public TrackStates SecondTrackState { get; set; }

    [XmlAttribute("HasGap Second")]
    public bool SecondTrackHasGap { get; }
}
