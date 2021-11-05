namespace PodgórzynPanelSterowania.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using PodgórzynPanelSterowania.Controls.Cells;
    using PodgórzynPanelSterowania.Extensions;

    public partial class Pulpit : Control
    {
        private readonly float defaultCellSize = 4 * 9.5f;

        private Size dimensions;
        private CellHolder cells;

        private bool lockScale = true;
        private float pulpitScale;

        public Pulpit()
        {
            cells = new CellHolder();
            DoubleBuffered = true;
            InitializeComponent();
        }

        public Size PulpitSize
        {
            get
            {
                return new Size((int) (PulpitScale + (dimensions.Width * defaultCellSize * PulpitScale)), (int) (PulpitScale + (dimensions.Height * defaultCellSize * PulpitScale)));
            }
        }

        public float PulpitScale
        {
            get
            {
                return lockScale ? 3 : pulpitScale;
            }
        }

        public bool LockScale
        {
            get
            {
                return lockScale;
            }

            set
            {
                lockScale = value;
                Invalidate();
            }
        }

        public Size Dimensions
        {
            get
            {
                return dimensions;
            }

            set
            {
                dimensions = value;
                int internalX, internalY;
                internalX = value.Width;
                internalY = value.Height;
                Size = new Size((int) ((internalX * (defaultCellSize + 1) + 1) * PulpitScale) + 1, (int) ((internalY * (defaultCellSize + 1) + 1) * PulpitScale) + 1);
                cells.ModifySize(dimensions.Width, dimensions.Height);
            }
        }

        protected internal void PopulateWithEmptyCells()
        {
            for (int y = 0; y < dimensions.Height; y++)
            {
                for (int x = 0; x < dimensions.Width; x++)
                {
                    if (x.IsBetween(1, dimensions.Width - 2) && y.IsBetween(1, dimensions.Height - 2))
                    {
                        var cell = new Element();
                        cell.GridLocation = new Point(x, y);
                        cells[x, y] = cell;
                    }
                    else
                    {
                        var cell = new SideElement();
                        cell.Side = (x == 0)
                            ? SideElement.SideLocation.Left
                            : (x == dimensions.Width - 1)
                                ? SideElement.SideLocation.Right
                                : SideElement.SideLocation.None;

                        cell.Side |= (y == 0)
                            ? SideElement.SideLocation.Top
                            : (y == dimensions.Height - 1)
                                ? SideElement.SideLocation.Bottom
                                : SideElement.SideLocation.None;

                        cell.GridLocation = new Point(x, y);
                        cells[x, y] = cell;
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            CalculateElementsLocations();

            Bitmap bitmap = new Bitmap(PulpitSize.Width, PulpitSize.Height);
            Graphics g = Graphics.FromImage(bitmap);

            using (SolidBrush b = new SolidBrush(Colors.Black.ToColor()))
            {
                g.FillRectangle(b, 0, 0, Width, Height);
            }

            foreach (var cell in cells)
            {
                cell.UpdateBitmap(g);
            }

            e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            CalculateScale();
            Invalidate();
        }

        private void CalculateScale()
        {
            var scaleX = Size.Width / (defaultCellSize * (dimensions.Width - 1) + (defaultCellSize + 1));
            var scaleY = Size.Height / (defaultCellSize * (dimensions.Height - 1) + (defaultCellSize + 1));

            var min = Math.Min(scaleX, scaleY);

            var width = (int) (min + (dimensions.Width * defaultCellSize * min));
            var height = (int) (min + (dimensions.Height * defaultCellSize * min));

            scaleX = width / (defaultCellSize * dimensions.Width + 1);
            scaleY = height / (defaultCellSize * dimensions.Height + 1);

            min = Math.Min(scaleX, scaleY);
            pulpitScale = min;
        }

        private IEnumerable<Element> GetMainCells()
        {
            foreach (var control in Controls)
            {
                if (control is Element element)
                {
                    if (element.GridLocation.X.IsBetween(1, dimensions.Width - 2) && element.GridLocation.Y.IsBetween(1, dimensions.Height - 2))
                    {
                        yield return element;
                    }
                }
            }
        }

        private IEnumerable<SideElement> GetSideCells(SideElement.SideLocation location = SideElement.SideLocation.None)
        {
            foreach (var control in Controls)
            {
                if (control is SideElement sideElem)
                {
                    if (location == SideElement.SideLocation.None)
                    {
                        yield return sideElem;
                    }
                    else
                    {
                        if (sideElem.Side.HasFlag(location))
                        {
                            yield return sideElem;
                        }
                    }
                }
            }
        }

        private void UpdateElement(object sender, EventArgs e)
        {
        }

        private void CalculateElementsLocations()
        {
            foreach (var cell in cells)
            {
                cell.Size = new Size((int) (defaultCellSize * PulpitScale), (int) (defaultCellSize * PulpitScale));
                cell.USize = new Size((int) defaultCellSize, (int) defaultCellSize);
                int cellX = cell.GridLocation.X * cell.Size.Width;
                int cellY = cell.GridLocation.Y * cell.Size.Height;
                cell.ElementScale = PulpitScale;

                if (cell.GridLocation.X == dimensions.Width - 2)
                {
                    cell.Size = new Size((int) ((defaultCellSize + 1) * PulpitScale), cell.Size.Height);
                    cell.USize = new Size((int) defaultCellSize + 1, cell.USize.Height);
                    cell.DrawRightBigger = true;
                }

                if (cell.GridLocation.Y == dimensions.Height - 2)
                {
                    cell.Size = new Size(cell.Size.Width, (int) ((defaultCellSize + 1) * PulpitScale));
                    cell.USize = new Size(cell.USize.Width, (int) defaultCellSize + 1);
                    cell.DrawBottomBigger = true;
                }

                if (cell.GridLocation.X == dimensions.Width - 1)
                {
                    cellX = (cell.GridLocation.X - 1) * cell.Size.Width + (int) ((defaultCellSize + 1) * PulpitScale);
                }

                if (cell.GridLocation.Y == dimensions.Height - 1)
                {
                    cellY = (cell.GridLocation.Y - 1) * cell.Size.Height + (int) ((defaultCellSize + 1) * PulpitScale);
                }

                cell.Location = new Point(cellX, cellY);
            }
        }
    }
}
