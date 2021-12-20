namespace RyszardopolisPanelSterowania;

using System;
using System.Windows.Forms;


using Momiji.Bot.V3.Serialization.XmlSerializer;
using RyszardopolisPanelSterowania.Serializer;
using RyszardopolisPanelSterowania.Controls;

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
        if (System.IO.File.Exists(@"C:\Users\Krystian\Desktop\Data.txt"))
        {
            System.IO.File.Delete(@"C:\Users\Krystian\Desktop\Data.txt");
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        using MainForm form = new MainForm();
        Application.Run(form);
    }

    public MainForm()
    {
        InitializeComponent();
        pulpit1 = new Pulpit();
        panel1.Controls.Add(pulpit1);
        pulpit1.Dock = DockStyle.Fill;
        pulpit1.LockBitmapScale = false;

        serializedData = XmlSerializer.Load(serializerConfig);

        pulpit1.Dimensions = serializedData.Data.Size;

        foreach (var cell in serializedData.Data.Elements)
        {
            pulpit1.UpdateCell(cell);
        }

        /*DoubleJunction j = new DoubleJunction();
        j.MainTrackId = "it_115";
        j.SecondTrackId = "it_116";
        j.ThirdTrackId = "it_115_S;
        j.JunctionId = "j_1";
        j.SecondJunctionId = "j_2";

        j.GridLocation = new Point(3, 1);

        pulpit1.UpdateCell(j);
        */

        Pulpit.ParseSerialData("j_1_S HIGH");
        serializedData.Data.Elements.Clear();
        serializedData.Data.Elements.AddRange(pulpit1.Cells);

        //XmlSerializer.Save(serializerConfig, serializedData);
        //pulpit1.Dimensions = new System.Drawing.Size(46, 22);
        Invalidate();
    }

    private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
    {
        pulpit1.LockScale = checkBox1.Checked;
    }


    private bool temp = false;
    private void Click_button1(object sender, System.EventArgs e)
    {
        Pulpit.ParseSerialData("it_116 " + ((temp = !temp) ? "HIGH" : "LOW"));
    }

    private void timer1_Tick(object sender, System.EventArgs e)
    {
        pulpit1.Invalidate();
    }
}
