namespace PodgórzynPanelSterowania.Controls
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using PodgórzynPanelSterowania.Controls.Cells;

    public class CellHolder : IEnumerable<NewElement>
    {
        protected Size grid { get; set; } = new Size(0, 0);

        private NewElement[] Cells { get; set; } = new NewElement[0];

        public NewElement this[int index]
        {
            get
            {
                return Cells[index];
            }

            set
            {
                Cells[index] = value;
            }
        }

        public NewElement this[int x, int y]
        {
            get
            {
                return Cells[x + (y * grid.Width)];
            }

            set
            {
                Cells[x + (y * grid.Width)] = value;
            }
        }

        public void ModifySize(int x, int y)
        {
            grid = new Size(x, y);
            Cells = Cells.ModifySize(x * y);
        }

        public IEnumerator<NewElement> GetEnumerator()
        {
            return Cells.OfType<NewElement>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Cells.GetEnumerator();
        }
    }
}
