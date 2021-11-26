namespace RyszardopolisPanelSterowania
{
using System;
    using System.Drawing;
    using System.Windows.Forms;

    using RyszardopolisPanelSterowania.Cells;

    using Momiji.Bot.V3.Serialization.XmlSerializer;
    using RyszardopolisPanelSterowania.Serializer;

    public partial class MainForm : Form
    {
        private XmlObject<SerializedDataRoot> serializedData = new XmlObject<SerializedDataRoot>() { Version = new XmlSerializerVersion("1.0.0.0"), Data = new SerializedDataRoot() };
        private static XmlSerializerConfig<SerializedDataRoot> serializerConfig = new XmlSerializerConfig<SerializedDataRoot>()
        {
            Directory = "data",
            FileName = "pulpitData.xml"
        };

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public MainForm()
        {
            InitializeComponent();
            serializedData = XmlSerializer.Load(serializerConfig);

            pulpit1.Dimensions = serializedData.Data.Size;

            foreach (var cell in serializedData.Data.Elements)
            {
                pulpit1.UpdateCell(cell);
            }

            /*Junction j = new Junction();
            j.MainTrackId = "it_115";
            j.SecondTrackId = "it_116";
            j.JunctionId = "j_1";

            j.GridLocation = new Point(3, 1);

            pulpit1.UpdateCell(j);
            */

            pulpit1.ParseSerialData("j_1_S HIGH");
            serializedData.Data.Elements.Clear();
            serializedData.Data.Elements.AddRange(pulpit1.Cells);

            XmlSerializer.Save(serializerConfig, serializedData);
            Invalidate();
        }

        private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            pulpit1.LockScale = checkBox1.Checked;
        }


        private bool temp = false;
        private void Click_button1(object sender, System.EventArgs e)
        {
            pulpit1.ParseSerialData("it_116 " + ((temp = !temp) ? "HIGH" : "LOW"));
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            pulpit1.Invalidate();
        }
    }
}
