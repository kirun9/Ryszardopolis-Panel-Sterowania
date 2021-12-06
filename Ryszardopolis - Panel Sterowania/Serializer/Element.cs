namespace RyszardopolisPanelSterowania.Serializer
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using RyszardopolisPanelSterowania.Cells;

    public class SerializedDataRoot
    {
        [XmlElement("PulpitSize")]
        public Size Size { get; set; } = new Size();

        [XmlArray("Elements")]
        [XmlArrayItem("Cell", typeof(Element))]
        public List<Element> Elements { get; set; } = new List<Element>();
    }
}
