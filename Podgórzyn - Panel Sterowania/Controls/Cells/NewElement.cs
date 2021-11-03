using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PodgórzynPanelSterowania.Extensions;

namespace PodgórzynPanelSterowania.Controls.Cells
{
    public class NewElement
    {
        private Point gridLocation;
        private Size size;
        private Point location;

        public Point GridLocation
        {
            get
            {
                return gridLocation;
            }

            set
            {
                gridLocation = value;

                OnUpdateElement();
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
                OnUpdateElement();
            }
        }

        public bool DrawBottomBigger { get; set; }
        public bool DrawRightBigger { get; set; }


        public event EventHandler UpdateElement;

        public NewElement()
        {

        }

        private void OnUpdateElement()
        {
            UpdateElement?.Invoke(this, new EventArgs());
        }

        public void UpdateBitmap(ref Graphics g, Point pos, float scale)
        {
            using SolidBrush brush = new SolidBrush(Colors.BorderMain.ToColor());
            g.FillRectangle(brush, pos.X             , pos.Y             , scale     , size.Height);
            g.FillRectangle(brush, pos.X             , pos.Y             , size.Width, scale      );
            brush.Color = Colors.BorderSecond.ToColor();
            g.FillRectangle(brush, pos.X             , pos.Y + 12 * scale, scale     , 4 * scale  );
            g.FillRectangle(brush, pos.X             , pos.Y + 22 * scale, scale     , 4 * scale  );
            g.FillRectangle(brush, pos.X + 12 * scale, pos.Y             , 4 * scale , scale      );
            g.FillRectangle(brush, pos.X + 22 * scale, pos.Y             , 4 * scale , scale      );

            if (DrawBottomBigger)
            {
                brush.Color = Colors.BorderMain.ToColor();
                g.FillRectangle(brush, pos.X             , pos.Y + size.Height - scale.Ceiling(), size.Width, scale);
                brush.Color = Colors.BorderSecond.ToColor();
                g.FillRectangle(brush, pos.X + 12 * scale, pos.Y + size.Height - scale.Ceiling(), 4 * scale , scale);
                g.FillRectangle(brush, pos.X + 22 * scale, pos.Y + size.Height - scale.Ceiling(), 4 * scale , scale);
            }

            if (DrawRightBigger)
            {
                brush.Color = Colors.BorderMain.ToColor();
                g.FillRectangle(brush, pos.X + size.Width - scale.Ceiling(), pos.Y             , scale, size.Height);
                brush.Color = Colors.BorderSecond.ToColor();
                g.FillRectangle(brush, pos.X + size.Width - scale.Ceiling(), pos.Y + 12 * scale, scale, 4 * scale  );
                g.FillRectangle(brush, pos.X + size.Width - scale.Ceiling(), pos.Y + 22 * scale, scale, 4 * scale  );
            }
        }
    }
}
