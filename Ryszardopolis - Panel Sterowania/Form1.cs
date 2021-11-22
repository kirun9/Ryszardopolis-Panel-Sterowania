namespace RyszardopolisPanelSterowania
{
    using System.Drawing;
    using System.Windows.Forms;

    using RyszardopolisPanelSterowania.Cells;

    public partial class Form1 : Form
    {
        private Track t1 = new Track();
        private Track t2 = new Track();
        private Track t3 = new Track();
        private Track t4 = new Track();

        public Form1()
        {
            InitializeComponent();
            pulpit1.PopulateWithEmptyCells();
            t1.TrackId = "it_115";
            t2.TrackId = "it_115";
            t3.TrackId = "it_116";
            t4.TrackId = "it_116";
            t1.GridLocation = new Point(1, 1);
            t2.GridLocation = new Point(2, 1);
            t3.GridLocation = new Point(3, 1);
            t4.GridLocation = new Point(3, 2);

            t3.IsDiagonal = true;
            t4.ElementRotation = RotateFlipType.Rotate90FlipNone;

            pulpit1.UpdateCell(t1);
            pulpit1.UpdateCell(t2);
            pulpit1.UpdateCell(t3);
            pulpit1.UpdateCell(t4);
            Invalidate();
        }

        private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            pulpit1.LockScale = checkBox1.Checked;
        }

        private void Click_button1(object sender, System.EventArgs e)
        {
            t4.ElementRotation += 1;
            if ((int) t4.ElementRotation > 7)
                t4.ElementRotation = 0;
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            pulpit1.Invalidate();
        }
    }
}
