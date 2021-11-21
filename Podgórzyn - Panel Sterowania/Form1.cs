namespace RyszardopolisPanelSterowania
{
    using System.Drawing;
    using System.Windows.Forms;

    using RyszardopolisPanelSterowania.Cells;

    public partial class Form1 : Form
    {
        private Track t = new Track();

        public Form1()
        {
            InitializeComponent();
            pulpit1.PopulateWithEmptyCells();

            t.GridLocation = new Point(2, 1);
            pulpit1.UpdateCell(t);
            Invalidate();
        }

        private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            pulpit1.LockScale = checkBox1.Checked;
        }

        private void Click_button1(object sender, System.EventArgs e)
        {
            t.TrackState += 1;
            if (t.TrackState > TrackStates.Juntion)
                t.TrackState = TrackStates.None;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            pulpit1.Invalidate();
        }
    }
}
