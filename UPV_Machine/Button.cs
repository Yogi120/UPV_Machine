using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UPV_Machine
{
    public class RoundedButton : Button
    {
        private int cornerRadius = 15; // Adjust the radius for rounded corners

        public float CustomFontSize { get; set; }
        public Color CustomBackColor { get; set; }
        public Color CustomForeColor { get; set; }
        public StringAlignment CustomTextAlignment { get; set; }

        public RoundedButton()
        {

            //CustomFontSize = 10.0f; // Default font size
            //CustomBackColor = Color.Gray; // Default button color
            //CustomForeColor = Color.White; // Default text color
            //CustomTextAlignment = StringAlignment.Near; // Default text alignment

            //this.FlatStyle = FlatStyle.Flat;
            //this.FlatAppearance.BorderSize = 0;
            //this.BackColor = CustomBackColor; // Use custom button color
            //this.ForeColor = CustomForeColor; // Use custom text color



            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Gray;
            this.ForeColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            GraphicsPath path = new GraphicsPath();

            int width = Width;
            int height = Height;

            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // Top-left corner
            path.AddArc(width - 2 * cornerRadius, 0, cornerRadius * 2, cornerRadius * 2, 270, 90); // Top-right corner
            path.AddArc(width - 2 * cornerRadius, height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 0, 90); // Bottom-right corner
            path.AddArc(0, height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 90, 90); // Bottom-left corner
            path.CloseAllFigures();

            this.Region = new Region(path);

            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (Pen pen = new Pen(this.BackColor, 1.0f))
            {
                e.Graphics.DrawPath(pen, path);
            }

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            e.Graphics.DrawString(this.Text, this.Font, Brushes.White, ClientRectangle, format);
        }
    }
}
