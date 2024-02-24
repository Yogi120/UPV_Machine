using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace UPV_Machine
{
    public partial class ParaSetting : Form
    {
        //private TextBox textBox;
        public ParaSetting()
        {

            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ParaSetting_Load(object sender, EventArgs e)
        {
            if (Common.IS516 == true)
            {
                rbIS516.Checked = true;
            }
            else if (Common.IS13311 == true)
            {
                rbIS13311.Checked = true;
            }

            txtDistance.Text = Common.Distance;
            txtVelMax.Text = Common.velocityMax;
            txtVelMin.Text = Common.velocityMin;

            if (Common.Pulse == true)
            {
                rbPulse.Checked = true;
            }
            else if (Common.Continuous == true)
            {
                rbContinuous.Checked = true;
            }

            txtWindowFunc.Text = Common.windowFunction;

            txtFilterFreq.Text = Common.filterFrequency;
            txtGainControl.Text = Common.gainControl;

            if(Common.Auto == true)
            {
                rbAuto.Checked = true;
            }
            else if(Common.Manual == true)
            {
                rbManual.Checked = true;
            }
            if(Common.Slow == true)
            {
                rbParaResSlow.Checked = true;
            }
            else if(Common.Medium == true)
            {
                rbParaResMedium.Checked = true;
            }
            else if(Common.Fast == true)
            {
                rbParaResFast.Checked = true;
            }
            txtParaHoldTime.Text = Common.parameterHoldTime;
            txtGainSetting.Text = Common.gainSetting;
            txtTxVoltage.Text = Common.txVoltage;
            txtXDistance.Text = Common.xDistance;
            txtPulseRepeatFreq.Text = Common.PulseRepeatFreq;
            txtWindowDelay.Text = Common.windowDelay;
            txtWindowTime.Text = Common.windowOnTime;
            txtCalReference.Text = Common.calReference;
            txtCalRefeShear.Text = Common.calRefShear;
            txtReadingSaveTime.Text = Common.readingSaveTime;

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
