﻿namespace PodgórzynPanelSterowania
{
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pulpit1.PopulateWithEmptyCells();
            Invalidate();
        }

        private void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            pulpit1.LockScale = checkBox1.Checked;
        }
    }
}
