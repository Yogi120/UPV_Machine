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
    public partial class ModeMem : Form
    {
        public ModeMem()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r1 = new RadioButton();
            r1.Size = new Size(100, 40);
            this.Controls.Add(r1);
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (panel2.Height == 0)
            {
                button2.Image = Properties.Resources.upload;
                panel2.Height = 168;

                //isHiddenPanel3 = false;
            }
            else
            {
                button2.Image = Properties.Resources.down_arrow;
                panel2.Height = 0;
                // isHiddenPanel3 = true;

            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel1.Height == 0)
            {
                button1.Image = Properties.Resources.upload;
                panel1.Height = 139;

                //isHiddenPanel3 = false;
            }
            else
            {
                button1.Image = Properties.Resources.down_arrow;
                panel1.Height = 0;
                // isHiddenPanel3 = true;

            };
        }

        private void ModeMem_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (panel3.Height == 0)
            {
                button3.Image = Properties.Resources.upload;
                panel3.Height = 110;

                //isHiddenPanel3 = false;
            }
            else
            {
                button3.Image = Properties.Resources.down_arrow;
                panel3.Height = 0;
                // isHiddenPanel3 = true;

            };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
