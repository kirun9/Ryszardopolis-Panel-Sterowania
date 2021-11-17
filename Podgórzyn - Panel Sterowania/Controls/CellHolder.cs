namespace RyszardopolisPanelSterowania.Controls
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using RyszardopolisPanelSterowania.Controls.Cells;

    [System.Diagnostics.DebuggerStepThrough]
    public class CellHolder : IEnumerable<Element>
    {
        protected Size Grid { get; set; } = new Size(0, 0);

        private Element[] Cells { get; set; } = new Element[0];

        public Element this[int index]
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

        public Element this[int x, int y]
        {
            get
            {
                return Cells[x + (y * Grid.Width)];
            }

            set
            {
                Cells[x + (y * Grid.Width)] = value;
            }
        }

        public void ModifySize(int x, int y)
        {
            Grid = new Size(x, y);
            Cells = Cells.ModifySize(x * y);
        }

        public IEnumerator<Element> GetEnumerator()
        {
            return Cells.OfType<Element>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Cells.GetEnumerator();
        }
    }
}
