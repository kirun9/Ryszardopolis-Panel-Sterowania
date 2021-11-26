namespace RyszardopolisPanelSterowania.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO.Ports;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using RyszardopolisPanelSterowania.Cells;
    using RyszardopolisPanelSterowania.Cells.Interfaces;
    using RyszardopolisPanelSterowania.Extensions;

    public partial class Pulpit : Control
    {
        private readonly float defaultCellSize = 38; // 9,5 * 4

        private Size dimensions;
        private CellHolder cells;
        private DigitalData Data;

        private bool lockScale = false;
        private float pulpitScale;

        internal static float bitmapScale = 10;

        public Pulpit()
        {
            cells = new CellHolder();
            Data = new DigitalData();
            DoubleBuffered = true;
            InitializeComponent();
            //SerialPort.Open();
        }

        public Size PulpitSize
        {
            get
            {
                return new Size((int) (PulpitScale + (dimensions.Width * defaultCellSize * PulpitScale)), (int) (PulpitScale + (dimensions.Height * defaultCellSize * PulpitScale)));
            }
        }

        public Size PulpitCellSize
        {
            get
            {
                return new Size((int) (PulpitScale * defaultCellSize), (int) (PulpitScale * defaultCellSize));
            }
        }

        public Size BitmapSize
        {
            get
            {
                return new Size((int) (bitmapScale + (dimensions.Width * defaultCellSize * bitmapScale)), (int) (bitmapScale + (dimensions.Height * defaultCellSize * bitmapScale)));
            }
        }

        public Size BitmapCellSize
        {
            get
            {
                return new Size((int) (bitmapScale * defaultCellSize), (int) (bitmapScale * defaultCellSize));
            }
        }

        public float PulpitScale
        {
            get
            {
                return lockScale ? 1.3f : pulpitScale;
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
                UpdateCells();
            }
        }

        public Element[] Cells => cells;

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
                        cell.Text = cell.Side == SideElement.SideLocation.Top || cell.Side == SideElement.SideLocation.Bottom ? cell.GridLocation.X.ToString("D2") : (cell.Side == SideElement.SideLocation.Left || cell.Side == SideElement.SideLocation.Right ? (dimensions.Height - cell.GridLocation.Y - 1).ToString("D2") : "");

                        cells[x, y] = cell;
                    }
                }
            }
        }

        private void RegisterEvents(Element cell)
        {
            if (cell is ITrack track)
            {
                Data.RegisterElement(track.TrackId, track.OccupyTrack);
            }
            if (cell is IJunction junction)
            {
                Data.RegisterElement(junction.MainTrackId, junction.OcupyTrack);
                Data.RegisterElement(junction.SecondTrackId, junction.OcupyTrack);

                Data.RegisterElement(junction.JunctionId + "_M", junction.SwitchTrack);
                Data.RegisterElement(junction.JunctionId + "_S", junction.SwitchTrack);
            }
        }

        private void UnregisterEvents(Element cell)
        {
            if (cell is ITrack track)
            {
                var key = track.TrackId;
                Data.UnregisterEvent(key, track.OccupyTrack);
            }
        }

        public void UpdateCell(Element cell)
        {
            if (cell.GridLocation.X.IsBetween(1, dimensions.Width - 2) && cell.GridLocation.Y.IsBetween(1, dimensions.Height - 2))
            {
                var ocell = cells[cell.GridLocation.X, cell.GridLocation.Y];
                UnregisterEvents(ocell);
                cell.Location = ocell.Location;
                cell.Size = ocell.Size;
                cell.DrawBottomBigger = ocell.DrawBottomBigger;
                cell.DrawRightBigger = ocell.DrawRightBigger;
                cells[cell.GridLocation.X, cell.GridLocation.Y] = cell;
                RegisterEvents(cell);
                Invalidate();
            }
        }

        public void UpdateCell(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(UpdateCell), sender, e);
            }
            else
            {
                Invalidate();
            }
        }

        public Element GetCell(int x, int y)
        {
            return x.IsBetween(1, dimensions.Width - 2) && y.IsBetween(1, dimensions.Height - 2) ? cells[x, y] : null;
        }

        protected internal void UpdateCells()
        {
            Element GetCell(int x, int y)
            {
                foreach (var cell in cells)
                {
                    if (cell.GridLocation.X == x && cell.GridLocation.Y == y)
                        return cell;
                }
                return null;
            }

            for (int y = 0; y < dimensions.Height; y++)
            {
                for (int x = 0; x < dimensions.Width; x++)
                {
                    if (x.IsBetween(1, dimensions.Width - 2) && y.IsBetween(1, dimensions.Height - 2))
                    {
                        var cell = GetCell(x, y) ?? new Element();
                        if (cell is SideElement)
                            cell = new Element();
                        cell.GridLocation = new Point(x, y);
                        cells[x, y] = cell;
                    }
                    else
                    {
                        var cell = GetCell(x, y) as SideElement ?? new SideElement();
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
                        cell.Text = cell.Side == SideElement.SideLocation.Top || cell.Side == SideElement.SideLocation.Bottom ? cell.GridLocation.X.ToString("D2") : (cell.Side == SideElement.SideLocation.Left || cell.Side == SideElement.SideLocation.Right ? ((dimensions.Height) - cell.GridLocation.Y - 1).ToString("D2") : "");
                        cells[x, y] = cell;
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            CalculateElementsLocations();

            Bitmap bitmap = new Bitmap(BitmapSize.Width, BitmapSize.Height);
            using Graphics g = Graphics.FromImage(bitmap);

            using (SolidBrush brush = new SolidBrush(Colors.Black.ToColor()))
            {
                g.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            }

            g.ScaleTransform(bitmapScale, bitmapScale);

            var origTransform = g.Transform;

            foreach (var cell in cells)
            {
                g.TranslateTransform(cell.Location.X, cell.Location.Y);
                cell.DrawCell(g);
                g.Transform = origTransform;
            }

            g.ResetTransform();

            Bitmap scaled = new Bitmap(bitmap, PulpitSize.Width, PulpitSize.Height);

            e.Graphics.DrawImage(scaled, 0, 0, PulpitSize.Width, PulpitSize.Height);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            CalculateScale();
            Invalidate();
        }

        private void CalculateScale()
        {
            float scaleX = Size.Width / (defaultCellSize * (dimensions.Width - 1) + (defaultCellSize + 1));
            float scaleY = Size.Height / (defaultCellSize * (dimensions.Height - 1) + (defaultCellSize + 1));

            float min = Math.Min(scaleX, scaleY);

            float width = (int) (min + (dimensions.Width * defaultCellSize * min));
            float height = (int) (min + (dimensions.Height * defaultCellSize * min));

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
                cell.Size = new Size((int) defaultCellSize, (int) defaultCellSize);
                int cellX = cell.GridLocation.X * cell.Size.Width;
                int cellY = cell.GridLocation.Y * cell.Size.Height;

                if (cell.GridLocation.X == dimensions.Width - 2)
                {
                    cell.Size = new Size((int) defaultCellSize + 1, cell.Size.Height);
                    cell.DrawRightBigger = true;
                }

                if (cell.GridLocation.Y == dimensions.Height - 2)
                {
                    cell.Size = new Size(cell.Size.Width, (int) defaultCellSize + 1);
                    cell.DrawBottomBigger = true;
                }

                if (cell.GridLocation.X == dimensions.Width - 1)
                {
                    cellX = (cell.GridLocation.X - 1) * cell.Size.Width + (int) (defaultCellSize + 1);
                }

                if (cell.GridLocation.Y == dimensions.Height - 1)
                {
                    cellY = (cell.GridLocation.Y - 1) * cell.Size.Height + (int) (defaultCellSize + 1);
                }

                cell.Location = new Point(cellX, cellY);
            }
        }

        private void SerialPort_DataReceived(Object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort) sender;
            string inData = port.ReadExisting();
            ParseSerialData(inData);
        }

        internal void SendData(string dataName, bool value)
        {
            SerialPort.WriteLine($"{dataName} {value}");
        }

        internal void ParseSerialData(string recivedData)
        {
            System.Diagnostics.Debug.WriteLine(recivedData);
            string[] lines = recivedData.Split(new char[] { '\n' });
            string errors = "";
            foreach (var line in lines)
            {
                if (Regex.IsMatch(line, @"(?:[a-zA-Z0-9_]+ )(?:(?:HIGH)|(?:LOW))"))
                {
                    string[] parts = line.Split(new char[] { ' ' });
                    Data[parts[0]] = parts[1] == "HIGH";
                }
                else
                {
                    errors += (errors == "") ? "" : "\n" + "Recived invalid data: " + line;
                }
            }
            if (errors == "")
                return;
            errors += "\nContinuing without quiting";
            MessageBox.Show(this, errors, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }
    }
}
