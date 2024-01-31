using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UPV_Machine
{

    public partial class Form1 : Form
    {

        //public string TimeModeSet;
        string TimeModeSet = Variable_Declaration.TimeModeSetting;
        public string VelocityModeSet;
        public string Signal;
        public int BatteryStatus;
        //public int batstat;
        public const char PC_REC = (char)0x14;
        public bool ULR0_Req, ULR1_Req, ULR2_Req, ULR3_Req, ULR4_Req = true;
        public char[] tx_buf;
        char[] tx_buf1 = new char[200];
        public const char DC1 = '\u0011';
        public const char ULR_0 = '\u000A';
        public const char DC3 = '\u0013';
        public const char ULR_1 = '\u000B';


        int initialWidth;
        bool isHidden;
        private Dictionary<int, List<string>> dataDictionary = new Dictionary<int, List<string>>();

        

        SerialPort mySerialPort = new SerialPort();
        //private DataGridView dataGridView1 = new DataGridView(); // Create a new instance

        private Timer timer;
        // private int batteryPercentage = 50;
        private Timer btryTimer;

        public Form1()
        {
            InitializeComponent();

            //RoundButtonCorners(this);

            initialWidth = panel2.Width;
            isHidden = false;

            btnMemorySetting.Click += btnMemory_Click;



            timer = new Timer();
            timer.Interval = 5000;
            //timer.Tick += timer1_Tick;
            timer.Start();

            //mySerialPort.DataReceived += MySerialPort_DataReceived;
            mySerialPort.ReadTimeout = 2000;
            mySerialPort.WriteTimeout = 1000;

            mySerialPort.BaudRate = 115200;
            mySerialPort.Parity = Parity.None;
            mySerialPort.DataBits = 8;

            Load += Form1_Load;

            btryTimer = new Timer();
            btryTimer.Interval = 1000; // Set the interval to 1 second
            btryTimer.Start();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //InitializeDatabase(); // Initialize or reset the database
        }


        //private void RoundButtonCorners(Control control)
        //{
        //    foreach (Control ctrl in control.Controls)
        //    {
        //        if (ctrl is Button)
        //        {
        //            Button button = (Button)ctrl;

        //            // Adjust the corner radius as needed
        //            int cornerRadius = 10;


        //            GraphicsPath path = new GraphicsPath();
        //            int width = button.Width;
        //            int height = button.Height;

        //            path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // Top-left corner
        //            path.AddArc(width - 2 * cornerRadius, 0, cornerRadius * 2, cornerRadius * 2, 270, 90); // Top-right corner
        //            path.AddArc(width - 2 * cornerRadius, height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 0, 90); // Bottom-right corner
        //            path.AddArc(0, height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 90, 90); // Bottom-left corner
        //            path.CloseAllFigures();

        //            button.Region = new Region(path);
        //        }

        //        // Recursively call the method for nested controls
        //        if (ctrl.HasChildren)
        //        {
        //            RoundButtonCorners(ctrl);
        //        }
        //    }
        //}

        private void btnHide_Click(object sender, EventArgs e)
        {
            timer1.Start();
            //timer2.Start();
            panel3.Height = 0;
            panel4.Height = 0;
            panel5.Height = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isHidden)
            {
                panel2.Width += 20;
                if (panel2.Width >= initialWidth)
                {
                    timer1.Stop();
                    isHidden = false;
                    //isHiddenPanel2 = false;
                }
            }

            else
            {
                panel2.Width -= 20;
                if (panel2.Width <= 0)
                {
                    timer1.Stop();
                    isHidden = true;
                    //isHiddenPanel2 = true;
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BringToFront();
        }

        public string value1 = "";
        public string value2 = "";
        public string value3 = "";
        public string value4 = "";
        public string value5 = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Width -= 241;

            isHidden = true;

            panel3.BringToFront();
            panel4.BringToFront();
            panel5.BringToFront();

            panel7.Width = 1130;
            btnDisconnect.Enabled = false;
            btnConnection.Enabled = true;

            TimeMode();
            //UpdateBatteryDisplay();

            Timer2.Interval = 100;
            Timer2.Enabled = true;
            Timer2.Start();
        }

        private void btnParaSetting_Click(object sender, EventArgs e)
        {
            ParaSetting paraSetting = new ParaSetting();
            paraSetting.ShowDialog();
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.RightArr;
            pictureBox3.Image = Properties.Resources.RightArr;
            panel3.BringToFront();
            panel4.Height = 0;
            panel5.Height = 0;

            if (panel3.Height == 0)
            {
                pictureBox2.Image = Properties.Resources.leftArrow;
                panel3.Height = 199;

                //isHiddenPanel3 = false;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.RightArr;
                panel3.Height = 0;
                // isHiddenPanel3 = true;

            };
        }

        private void btnMemory_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.RightArr;
            pictureBox3.Image = Properties.Resources.RightArr;
            panel4.BringToFront();
            panel3.Height = 0;
            panel5.Height = 0;

            if (panel4.Height == 0)
            {
                panel4.Height = 70;
                pictureBox1.Image = Properties.Resources.leftArrow;

            }
            //else if (panel4.Height == 70)
            //{
            //    panel4.Height = 0;
            //    pictureBox1.Image = Properties.Resources.RightArr;
            //}
            //else
            //{
            //    pictureBox1.Image = Properties.Resources.RightArr;
            //    panel4.Height = 0;

            //};

        }

        private void btnStrAnalysis_Click_1(object sender, EventArgs e)
        {
            panel5.BringToFront();
            panel3.Height = 0;
            panel4.Height = 0;

            pictureBox2.Image = Properties.Resources.RightArr;
            pictureBox1.Image = Properties.Resources.RightArr;

            if (panel5.Height == 0)
            {
                pictureBox3.Image = Properties.Resources.leftArrow;
                panel5.Height = 166;

                //isHiddenPanel3 = false;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.RightArr;
                panel5.Height = 0;
                // isHiddenPanel3 = true;

            };
        }

        public void Serila_Port()
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            //panel6.BringToFront();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            //panel8.SendToBack();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            panel7.SendToBack();

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pbBattery_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        

        private void pictureBox32_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint_1(object sender, PaintEventArgs e)
        {
            if (panel2.Width == 241)
            {
                panel7.Width = 1130;
            }
            else
            {
                panel7.Width = 1370;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        { }

        private void button15_Click(object sender, EventArgs e)
        {
            ModeMemory memory = new ModeMemory();
            memory.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ImageVideo imagevideo = new ImageVideo();
            imagevideo.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DateTimeSetting datetime = new DateTimeSetting();
            datetime.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isHidden)
            {
                panel7.Width -= 20;
                if (panel7.Width >= 1130)
                {
                    //timer2.Stop();
                    // isHidden = false;
                    //isHiddenPanel2 = false;

                }
            }

            else
            {
                panel7.Width += 20;
                if (panel7.Width <= 1370)
                {
                    //timer2.Stop();
                    // isHidden = true;
                    //isHiddenPanel2 = true;
                }
            }
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

       

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (mySerialPort.IsOpen == true)
            {
                mySerialPort.Close();
                btnConnection.Enabled = true;
                btnDisconnect.Enabled = false;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void TimeMode()
        {
            // lblMainValue2.Visible = true;
            lblMainValue2.Visible = true;
            lblMinTimeValue.Visible = false;
            lblMinTime.Visible = false;
            btnASave.Visible = false;
            btnSSave.Visible = false;
            lblDistance.Visible = false;
            lblDistanceValue.Visible = false;
            lblDisUnit.Visible = false;
            lblMode.Text = "Time Mode";
            lblDrtMethod.Visible = false;
            lblMinVelocity.Visible = false;
            lblMinVelValue.Visible = false;
            lblIS_Standard.Visible = false;
            btnNext.Visible = false;
            lblLTimeMode.Visible = false;
            btnSave.Visible = true;
            lblTimeWCrack.Visible = false;
            lblTimeWoCrack.Visible = false;
            picSignal100.Visible = false;
            picSignal80.Visible = false;
            picSignal60.Visible = false;
            picSignal40.Visible = false;
            picSignal15.Visible = false;
            picNoSignal.Visible = true;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // lblMainValue2.Text = TimeMode;
            lblMainValue2.Text = TimeModeSet + " " + "uS";
            TimeMode();
        }
        private void btnVelocitySetting_Click(object sender, EventArgs e)
        {
            lblMainValue2.Text = VelocityModeSet + " " + "m/s";
            lblMode.Text = "Velocity Mode";
            lblMinTimeValue.Text = TimeModeSet + " " + "uS";
            lblDistance.Visible = true;
            lblDistanceValue.Visible = true;
            lblDisUnit.Visible = true;
            btnASave.Visible = true;
            btnSSave.Visible = true;
            lblMinTime.Visible = true;
            lblMinTimeValue.Visible = true;
            lblDrtMethod.Visible = true;
            lblIS_Standard.Visible = false;
            lblMinVelocity.Visible = false;
            lblMinVelValue.Visible = false;
            btnNext.Visible = false;
            lblLTimeMode.Visible = false;
            btnSave.Visible = true;
            lblTimeWCrack.Visible = false;
            lblTimeWoCrack.Visible = false;
        }

        private void btnParameterSet_Click(object sender, EventArgs e)
        {
            lblMode.Text = "Parameter Mode";
            lblMinVelValue.Text = VelocityModeSet + " " + "m/s";
            lblIS_Standard.Visible = true;
            lblMinTime.Visible = true;
            lblMinTimeValue.Visible = true;
            lblMinVelocity.Visible = true;
            lblMinVelValue.Visible = true;
            lblDrtMethod.Visible = true;
            btnSSave.Visible = false;
            btnASave.Visible = false;
            btnNext.Visible = false;
            lblLTimeMode.Visible = false;
            btnSave.Visible = true;
            lblTimeWCrack.Visible = false;
            lblTimeWoCrack.Visible = false;
        }

        private void btnElasticModeSet_Click(object sender, EventArgs e)
        {
            lblMode.Text = "Elastic Mode";
            lblLTimeMode.Visible = true;
            lblIS_Standard.Visible = false;
            lblMinTime.Visible = false;
            lblMinTimeValue.Visible = false;
            lblMinVelocity.Visible = false;
            lblMinVelValue.Visible = false;
            lblDistance.Visible = false;
            lblDistanceValue.Visible = false;
            lblDisUnit.Visible = false;
            btnASave.Visible = false;
            btnSSave.Visible = false;
            btnSave.Visible = false;
            btnNext.Visible = true;
            lblTimeWCrack.Visible = false;
            lblTimeWoCrack.Visible = false;
        }

        private void btnCrackDepthSet_Click(object sender, EventArgs e)
        {
            lblMainValue2.Text = "00 mm";
            lblMode.Text = "Crack Depth";
            lblIS_Standard.Visible = true;
            lblLTimeMode.Visible = false;
            lblMinTime.Visible = false;
            lblMinTimeValue.Visible = false;
            lblMinVelocity.Visible = false;
            lblMinVelValue.Visible = false;
            lblTimeWCrack.Visible = true;
            lblTimeWoCrack.Visible = true;
            btnNext.Visible = true;
            btnSave.Visible = false;
            btnSave.Visible = false;
            btnASave.Visible = false;
            lblDistance.Visible = true;
            lblDistanceValue.Visible = true;
            lblMinTimeValue.Visible = true;
            lblMinVelValue.Visible = true;
        }

        public void Timer2_Tick_1(object sender, EventArgs e)
        {
            Timer2.Stop();
            // If mySerialPort.IsOpen = True Then

            if (mySerialPort.IsOpen == true)
            {
                if(ULR0_Req == true)
                {
                    char[] tx_buf = new char[] { DC1, ULR_0, DC3 };
                }

                else if(ULR1_Req == true)
                {
                    char[] tx_buf = new char[] { DC1, ULR_1, DC3 };
                }

                SerialAction();

                if (int.TryParse(lblBatteryPer.Text, out int BatteryStatus))
                {
                    picBattery30.Visible = false;
                    picBattery10.Visible = false;
                    picBattery100.Visible = false;
                    picBattery70.Visible = false;

                    switch (BatteryStatus)
                    {
                        case int n when n <= 30:
                            picBattery30.Visible = true;
                            break;

                        case int n when n >= 31 && n <= 70:
                            picBattery70.Visible = true;
                            break;

                        case int n when n > 70:
                            picBattery100.Visible = true;
                            break;

                        default:
                            picBattery10.Visible = true;
                            break;
                    }

                    lblBatteryPer.Text = $"{BatteryStatus}%";
                }

                if (int.TryParse(lblSignalStrength.Text, out int Signal))
                {
                    picSignal100.Visible = false;
                    picSignal80.Visible = false;
                    picSignal60.Visible = false;
                    picSignal40.Visible = false;
                    picSignal15.Visible = false;
                    picNoSignal.Visible = false;

                    switch (Signal)
                    {
                        case int n when n > 0 && n <= 15:
                            picSignal15.Visible = true;
                            break;

                        case int n when n >= 16 && n <= 40:
                            picSignal40.Visible = true;
                            break;

                        case int n when n >= 41 && n <= 60:
                            picSignal60.Visible = true;
                            break;

                        case int n when n >= 61 && n <= 80:
                            picSignal80.Visible = true;
                            break;

                        case int n when n >= 81 && n <= 100:
                            picSignal100.Visible = true;
                            break;

                        default:
                            picNoSignal.Visible = true;
                            break;
                    }

                    lblSignalStrength.Text = $"{Signal}%";
                }
            }

            // Update the label with the most recent data
        

           // TimeMode();

            Timer2.Start();
        }

        //private void Timer2_Tick_1(object sender, EventArgs e)
        //{
        //    Timer2.Stop();
        //    if(mySerialPort.IsOpen == true)
        //    {
        //        SerialAction();
        //        lblSignalStrength.Text = signal;
        //    }
        //    Timer2.Start();
        //}



        // tx_buf(0) = DC1
        //tx_buf(1) = ULR_0
        //tx_buf(2) = para  
        //tx_buf(3) = DC3

        // public const char CharVal1 = '\u0011';
        //public const char CharVal2 = '\u000A';
        //public const char CharVal3 = '\u0013';

         // = new char[] //{ CharVal1, CharVal2, CharVal3 };
        

        //byte[] bytesToSend;

        public int l;

        public void SerialAction()
        {
            if (ULR2_Req == true)
            {
                mySerialPort.Write(tx_buf1, 0, l);
            }
            else if (ULR1_Req == true)
            {
                mySerialPort.Write(tx_buf, 0, 3);
            }
            else if (ULR0_Req == true ) //|| ULR3_Req == true || ULR4_Req == true)
            {
                char[] tx_buf = new char[] { DC1, ULR_0, DC3 };
                mySerialPort.Write(tx_buf, 0, 3);
            }

            //byte[] bytesToSend = tx_buf.Select(x => (byte)x).ToArray();

          //  mySerialPort.Write(bytesToSend, 0, bytesToSend.Length);



            int bytesToRead = mySerialPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            int bytesRead = mySerialPort.Read(buffer, 0, bytesToRead);

            string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //    string[] stringValues = receivedData.Split('\u001A'); // Split by non-printable ASCII character (ASCII 26)
            string[] stringValues = receivedData.Split(PC_REC); // Split by non-printable ASCII character (ASCII 26)

            if (stringValues.Length >= 18) // Ensure there are at least 18 values to assign
            {
                // Assign values to local variables


                value1 = stringValues[0];

                switch (int.Parse(stringValues[1]))
                {
                    case 1:
                        lblStatus.Text = "BATTERY LOW";
                        break;
                    case 2:
                        lblStatus.Text = "VELOCITY LOW";
                        break;
                    case 3:
                        lblStatus.Text = "VELOCITY HIGH";
                        break;
                    case 4:
                        lblStatus.Text = "CALIBARTION ERROR";
                        break;
                    case 5:
                        lblStatus.Text = "TX BATTERY LOW";
                        break;
                    case 6:
                        lblStatus.Text = "RX BATTERY LOW";
                        break;
                    case 7:
                        lblStatus.Text = "ELASTIC CAL ERRO";
                        break;
                    case 8:
                        lblStatus.Text = "READING OUT OF LIMI";
                        break;
                    case 9:
                        lblStatus.Text = "DUPLICATE SITE/OBJEC";
                        break;
                    case 10:
                        lblStatus.Text = "DUPLICATE USER NAME";
                        break;
                    case 11:
                        lblStatus.Text = "INVALID PASSWORD";
                        break;
                    case 12:
                        lblStatus.Text = "INCVALID READING";
                        break;
                    case 13:
                        lblStatus.Text = "PLEASE FOLLOW PROPER TETS";
                        break;
                    case 14:
                        lblStatus.Text = "PLEASE REFER IS STANDARD";
                        break;
                }
                //lblMainValue2.Text = stringValues[2] + " " + "uS";
                TimeModeSet = stringValues[2];
                VelocityModeSet = stringValues[3];
                lblBatteryPer.Text = stringValues[5];
                lblSignalStrength.Text = stringValues[6];
            }
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            COMPortConnection portConnection = new COMPortConnection();
            portConnection.ShowDialog();
            try
            {

                if (portConnection.DialogResult == DialogResult.OK)
                {
                    if (mySerialPort.IsOpen == false)
                    {
                        mySerialPort.PortName = portConnection.cmbPortName;
                        mySerialPort.Open();
                        btnConnection.Enabled = false;
                        btnDisconnect.Enabled = true;
                        //MessageBox.Show($"Connected to {portConnection.cmbPortName}");
                        ULR0_Req = true;
                        ULR1_Req = false;
                        ULR2_Req = false;
                        ULR3_Req = false;
                        ULR4_Req = false;
                        //SerialAction();
                      //  ReceiveDataViaSerialPort();
                        lblMainValue2.Text = TimeModeSet + " " + "uS";
                    }
                }
                else
                {
                    Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please Select Proper Port");
            }
        }
    }
}