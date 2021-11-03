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
        private Size _dimensions;
        private CellHolder Cells;

        private readonly float defaultCellSize = 4 * 9.5f;
        private bool lockScale = true;
        private float pulpitScale;

        public Size PulpitSize
        {
            get
            {
                return new Size((int) (PulpitScale + (_dimensions.Width * defaultCellSize * PulpitScale)), (int) (PulpitScale + (_dimensions.Height * defaultCellSize * PulpitScale)));
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
                return _dimensions;
            }
            set
            {
                _dimensions = value.Add(2, 2);
                int internalX, internalY;
                internalX = value.Width + 2;
                internalY = value.Height + 2;

                //var tempCells = GetMainCells();

                Size = new Size((int) ((internalX * (defaultCellSize + 1) + 1) * PulpitScale) + 1, (int) ((internalY * (defaultCellSize + 1) + 1) * PulpitScale) + 1);
                Cells.ModifySize(_dimensions.Width, _dimensions.Height);
            }
        }

        public Pulpit()
        {
            Cells = new CellHolder();
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void CalculateScale()
        {
            var scaleX = Size.Width / (defaultCellSize * (_dimensions.Width - 1) + (defaultCellSize + 1));
            var scaleY = Size.Height / (defaultCellSize * (_dimensions.Height - 1) + (defaultCellSize + 1));

            var min = Math.Min(scaleX, scaleY);

            var width = (int) (min + (_dimensions.Width * defaultCellSize * min));
            var height = (int) (min + (_dimensions.Height * defaultCellSize * min));

            scaleX = width / (defaultCellSize * _dimensions.Width + 1);
            scaleY = height / (defaultCellSize * _dimensions.Height + 1);

            min = Math.Min(scaleX, scaleY);
            var width2 = (int) (min + (_dimensions.Width * defaultCellSize * min));
            pulpitScale = min;
        }

        private IEnumerable<NewElement> GetMainCells()
        {
            foreach (var control in Controls)
            {
                if (control is NewElement element)
                {
                    if (element.GridLocation.X.IsBetween(1, _dimensions.Width - 2) && element.GridLocation.Y.IsBetween(1, _dimensions.Height - 2))
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

        protected internal void PopulateWithEmptyCells()
        {
            for (int y = 0; y < _dimensions.Height; y++)
            {
                for (int x = 0; x < _dimensions.Width; x++)
                {
                    if (x.IsBetween(1, _dimensions.Width - 2) && y.IsBetween(1, _dimensions.Height - 2))
                    {
                        var cell = new NewElement();
                        ////cell.Text = x + ", " + y;
                        cell.GridLocation = new Point(x, y);
                        ////Controls.Add(cell);
                        Cells[x, y] = cell;
                        cell.UpdateElement += UpdateElement;
                    }
                    else
                    {
                        var cell = new SideElement();
                        cell.Side = (x == 0 ? SideElement.SideLocation.Left : (x == _dimensions.Width - 1 ? SideElement.SideLocation.Right : SideElement.SideLocation.None)) | (y == 0 ? SideElement.SideLocation.Top : (y == _dimensions.Width - 1 ? SideElement.SideLocation.Bottom : SideElement.SideLocation.None));
                        ////Controls.Add(cell);
                        cell.GridLocation = new Point(x, y);
                        Cells[x, y] = cell;
                        cell.UpdateElement += UpdateElement;
                    }
                }
            }
        }

        private void UpdateElement(object sender, EventArgs e)
        {

        }

        private void DrawElements()
        {
            foreach (var cell in Cells)
            {
                cell.Size = new Size((int) (defaultCellSize * PulpitScale), (int) (defaultCellSize * PulpitScale));
                int cellX = cell.GridLocation.X * cell.Size.Width;
                int cellY = cell.GridLocation.Y * cell.Size.Height;

                if (cell.GridLocation.X == _dimensions.Width - 2)
                {
                    cell.Size = new Size((int) ((defaultCellSize + 1) * PulpitScale), cell.Size.Height);
                    cell.DrawRightBigger = true;
                }
                if (cell.GridLocation.Y == _dimensions.Height - 2)
                {
                    cell.Size = new Size(cell.Size.Width, (int) ((defaultCellSize + 1) * PulpitScale));
                    cell.DrawBottomBigger = true;
                }

                if (cell.GridLocation.X == _dimensions.Width - 1)
                {
                    cellX = (cell.GridLocation.X - 1) * cell.Size.Width + (int) ((defaultCellSize + 1) * PulpitScale);
                }
                if (cell.GridLocation.Y == _dimensions.Height - 1)
                {
                    cellY = (cell.GridLocation.Y - 1) * cell.Size.Height + (int) ((defaultCellSize + 1) * PulpitScale);
                }

                cell.Location = new Point(cellX, cellY);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Bitmap bitmap = new Bitmap(PulpitSize.Width, PulpitSize.Height);
            Graphics g = Graphics.FromImage(bitmap);

            //Graphics g = e.Graphics;

            using (SolidBrush b = new SolidBrush(Colors.Black.ToColor()))
            {
                g.FillRectangle(b, 0, 0, Width, Height);
            }

            //DrawElements();
            DrawNewElements(ref g);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            CalculateScale();
            Invalidate();
        }
    }
}
