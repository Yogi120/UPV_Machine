using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPV_Machine
{
    public partial class ImageVideo : Form
    {
        public ImageVideo()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Height == 0)
            {
                button1.Image = Properties.Resources.upload;
                flowLayoutPanel1.Height = 160;

                //isHiddenPanel3 = false;
            }
            else
            {
                button1.Image = Properties.Resources.down_arrow;
                flowLayoutPanel1.Height = 0;
                // isHiddenPanel3 = true;

            };
        }
    }
}
