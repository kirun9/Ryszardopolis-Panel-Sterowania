using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PodgórzynPanelSterowania.Extensions;

namespace PodgórzynPanelSterowania.Controls.Cells
{
    public class Element
    {
        private Point gridLocation;
        private Size size;
        private Point location;
        private float elementScale;

        public Point GridLocation
        {
            get
            {
                return gridLocation;
            }

            set
            {
                gridLocation = value;
            }
        }

        public Size Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public bool DrawBottomBigger { get; set; }
        public bool DrawRightBigger { get; set; }

        public Point Location { get => location; set => location = value; }

        public float ElementScale { get => elementScale; set => elementScale = value; }

        public virtual void UpdateBitmap(Graphics g)
        {
            DrawBorder(g);
            DrawContent(g);
        }

        public virtual void DrawBorder(Graphics g)
        {
            using SolidBrush brush = new SolidBrush(Colors.Background.ToColor());
            g.FillRectangle(brush, location.X, location.Y, Size.Width, Size.Height);

            brush.Color = Colors.BorderMain.ToColor();
            g.FillRectangle(brush, location.X, location.Y, elementScale, size.Height);
            g.FillRectangle(brush, location.X, location.Y, size.Width, elementScale);

            brush.Color = Colors.BorderSecond.ToColor();
            g.FillRectangle(brush, location.X, location.Y + 12 * elementScale, elementScale, 4 * elementScale);
            g.FillRectangle(brush, location.X, location.Y + 22 * elementScale, elementScale, 4 * elementScale);
            g.FillRectangle(brush, location.X + 12 * elementScale, location.Y, 4 * elementScale, elementScale);
            g.FillRectangle(brush, location.X + 22 * elementScale, location.Y, 4 * elementScale, elementScale);

            if (DrawBottomBigger)
            {
                brush.Color = Colors.BorderMain.ToColor();
                g.FillRectangle(brush, location.X, location.Y + size.Height - elementScale.Ceiling(), size.Width, elementScale);

                brush.Color = Colors.BorderSecond.ToColor();
                g.FillRectangle(brush, location.X + 12 * elementScale, location.Y + size.Height - elementScale.Ceiling(), 4 * elementScale, elementScale);
                g.FillRectangle(brush, location.X + 22 * elementScale, location.Y + size.Height - elementScale.Ceiling(), 4 * elementScale, elementScale);
            }

            if (DrawRightBigger)
            {
                brush.Color = Colors.BorderMain.ToColor();
                g.FillRectangle(brush, location.X + size.Width - elementScale.Ceiling(), location.Y, elementScale, size.Height);

                brush.Color = Colors.BorderSecond.ToColor();
                g.FillRectangle(brush, location.X + size.Width - elementScale.Ceiling(), location.Y + 12 * elementScale, elementScale, 4 * elementScale);
                g.FillRectangle(brush, location.X + size.Width - elementScale.Ceiling(), location.Y + 22 * elementScale, elementScale, 4 * elementScale);
            }
        }

        public virtual void DrawContent(Graphics g)
        {

        }
    }
}
