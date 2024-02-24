using System;
using System.IO.Ports;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;



namespace UPV_Machine
{

    public partial class Form1 : Form
    {
        // Variable_Declaration variableDeclaration = new Variable_Declaration();

        //  private Dictionary<int, List<string>> dataDictionary = new Dictionary<int, List<string>>();

        char ULR_0 = Common.ulr_0;
        char ULR_1 = Common.ulr_1;
        char sec1 = Common.Sec1;
        char sec2 = Common.Sec2;
        char sec3 = Common.Sec3;
        char ULA_0 = Common.ula_0;
        char ULA_1 = Common.ula_1;
        char ULA_2 = Common.ula_2;

        SerialPort mySerialPort = new SerialPort();


        private Timer timer;
        private Timer btryTimer;

        public SqlConnection myCon = new SqlConnection();
        public SqlCommand sqlCmd;

        public Form1()
        {
            InitializeComponent();

            //RoundButtonCorners(this);


            Common.initialWidth = 241;

            //  initialWidth = 241;
            Common.isHidden = false;

            // btnMemorySetting.Click += btnMemory_Click;

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




        private void btnHide_Click(object sender, EventArgs e)
        {
            timer1.Start();
            //timer2.Start();
            panel3.Height = 0;
            panel4.Height = 0;
            panel5.Height = 0;
            pnlVelocityType.Height = 0;
            pictureBox2.Image = Properties.Resources.RightArr;
            pictureBox1.Image = Properties.Resources.RightArr;
            pictureBox3.Image = Properties.Resources.RightArr;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Common.isHidden)
            {
                panel2.Width += 20;
                if (panel2.Width >= Common.initialWidth)
                {
                    timer1.Stop();
                    Common.isHidden = false;
                    //isHiddenPanel2 = false;
                }
            }

            else
            {
                panel2.Width -= 20;
                if (panel2.Width <= 0)
                {
                    timer1.Stop();
                    Common.isHidden = true;
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
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RTUL"))
            {
                string foldersel = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\RTUL";
                Directory.CreateDirectory(foldersel);
            }


            SqlDataReader mysqlrdr1;
            DataTable mydttbl1 = new DataTable();

            string filepath = "your_filepath_here";
            string readFirstline = "";

            if (File.Exists(filepath))
            {
                try
                {
                    using (StreamReader objReader = new StreamReader(filepath))
                    {
                        readFirstline = objReader.ReadLine();
                    }

                    //TestDecoding(readFirstline); // Assuming TestDecoding is defined somewhere
                    //string[] readbuf = decrypString.Split('.');

                    myCon.Close();
                    myCon.ConnectionString = "Data Source=103.228.112.55;port=3306;Initial Catalog=rtulgrou_machine_db;User ID=rtulgrou_machine;Password=k!(kI=XNmi0i;SslMode=Preferred";
                    sqlCmd = new SqlCommand("select CID,PID,status,isActivated,MACaddr,HDDSr,Enddt,LicDays from Customer_DB where CID = @CID and PID = @PID", myCon);
                    //sqlCmd.Parameters.AddWithValue("@CID", readbuf[0]);
                    //sqlCmd.Parameters.AddWithValue("@PID", readbuf[1]);

                    myCon.Open();
                    mysqlrdr1 = sqlCmd.ExecuteReader();
                    mydttbl1.Load(mysqlrdr1);
                    myCon.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }


            Common.isHidden = true;

            panel3.BringToFront();
            panel4.BringToFront();
            panel5.BringToFront();

            panel7.Width = 1130;
            //// btnDisconnect.Enabled = false;
            // btnConnection.Enabled = true;

            TimeMode();

            string[] ports = SerialPort.GetPortNames();
            if (mySerialPort.IsOpen == false)
            {
                mySerialPort.PortName = ports[0]; // Use the first available port
                mySerialPort.Open();
                mySerialPort.ReadTimeout = 2000;
                mySerialPort.BaudRate = 115200;
                btnConnection.Enabled = false;
                btnDisconnect.Enabled = true;
            }
            //else
            //{
            //    btnConnection.Enabled = true;
            //    btnDisconnect.Enabled = false;
            //}

            // Hiding Elastic Mode result panel on Page Load

            Timer2.Interval = 100;
            Timer2.Enabled = true;
            Timer2.Start();
            //  Common.ULR0_Req = true;
            Common.ULR1_Req = true;
        }

        private void btnParaSetting_Click(object sender, EventArgs e)
        {
            Common.ULR1_Req = true;
            Common.ULR0_Req = false;
            SerialAction();
            ParaSetting paraSetting = new ParaSetting();
            paraSetting.ShowDialog();
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            pcbVelMode.Image = Properties.Resources.RightArr;
            pictureBox1.Image = Properties.Resources.RightArr;
            pictureBox3.Image = Properties.Resources.RightArr;
            panel3.BringToFront();
            panel4.Height = 0;
            panel5.Height = 0;

            if (panel3.Height == 0)
            {
                pictureBox2.Image = Properties.Resources.leftArrow;
                panel3.Height = 199;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.RightArr;
                panel3.Height = 0;
                pnlVelocityType.Height = 0;
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
            else
            {
                pictureBox1.Image = Properties.Resources.RightArr;
                panel4.Height = 0;
            };
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
            }
            else
            {
                pictureBox3.Image = Properties.Resources.RightArr;
                panel5.Height = 0;
            };
        }

        //public void Serila_Port()
        //{

        //}


        //private void pictureBox1_Click(object sender, EventArgs e)
        //{

        //}

        //private void button6_Click(object sender, EventArgs e)
        //{

        //}

        //private void button3_Click(object sender, EventArgs e)
        //{

        //}

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            //panel6.BringToFront();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            //panel8.SendToBack();
        }

        //private void panel7_Paint(object sender, PaintEventArgs e)
        //{
        //    panel7.SendToBack();

        //}

        //private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        //private void pbBattery_Click(object sender, EventArgs e)
        //{

        //}

        //private void pictureBox5_Click(object sender, EventArgs e)
        //{

        //}

        //private void pictureBox19_Click(object sender, EventArgs e)
        //{

        //}



        //private void pictureBox32_Click(object sender, EventArgs e)
        //{

        //}

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

        //private void panel4_Paint(object sender, PaintEventArgs e)
        //{ }

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
            if (Common.isHidden)
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
            lblUnituS.Visible = true;
            lblUnitMpS.Visible = false;
            lblUnitNpMM2.Visible = false;
            lblUNITmm.Visible = false;
            lblSmallValue1.Visible = false;
            lblMinTime.Visible = false;
            btnASave.Visible = false;
            btnSSave.Visible = false;
            lblDistance.Visible = false;
            lblDistanceValue.Visible = false;
            lblMode.Text = "Time Mode";
            lblDrtMethod.Visible = false;
            lblMinVelocity.Visible = false;
            lblSmallValue2.Visible = false;
            lblIS_Standard.Visible = false;
            btnNext.Visible = false;
            lblElasticModes.Visible = false;
            btnSave.Visible = true;
            lblTimeWCrack.Visible = false;
            lblTimeWoCrack.Visible = false;
            picSignal100.Visible = false;
            picSignal80.Visible = false;
            picSignal60.Visible = false;
            picSignal40.Visible = false;
            picSignal15.Visible = false;
            picNoSignal.Visible = true;
            
            lblL_TimeMode.Visible = false;
            lblTmeWCrack.Visible = false;
            lblCrackMode.Visible = false;
            pnlElasticModeResult.Visible = false;
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            Common.isTimeModeClicked = true;
            Common.isVelocityModeClicked = false;
            Common.isElasticModeClicked = false;
            Common.isCrack_depthModeClicked = false;
            Common.isParameterModeClicked = false;
            Common.isSignal_displayModeClicked = false;

            // lblMainValue2.Text = TimeMode;
            lblMainValue2.Text = Common.TimeModeSet;
            TimeMode();
        }
        private void btnVelocitySetting_Click(object sender, EventArgs e)
        {
            pnlVelocityType.BringToFront();

            if (pnlVelocityType.Height == 0)
            {
                pcbVelMode.Image = Properties.Resources.leftArrow;
                pnlVelocityType.Height = 103;
            }
            else
            {
                pcbVelMode.Image = Properties.Resources.RightArr;
                pnlVelocityType.Height = 0;
            }


            Common.isVelocityModeClicked = true;
            Common.isTimeModeClicked = false;
            Common.isElasticModeClicked = false;
            Common.isCrack_depthModeClicked = false;
            Common.isParameterModeClicked = false;
            Common.isSignal_displayModeClicked = false;
            Common.isNextBtnClicked = false;

            //lblMainValue2.Text = VelocityModeSet;
            //lblMainValue3.Text = VelocityModeSet;
            //lblUnituS.Visible = false;
            //lblUnitMpS.Visible = true;
            //lblUnitNpMM2.Visible = false;
            //lblUNITmm.Visible = false;
            //lblMode.Text = "Velocity Mode";
            ////lblMinTimeValue.Text = TimeModeSet + "uS";
            //lblDistance.Visible = true;
            //lblDistanceValue.Visible = true;
            //lblDisUnit.Visible = true;
            //btnASave.Visible = true;
            //btnSSave.Visible = true;
            //lblMinTime.Visible = true;
            //lblDrtMethod.Visible = true;
            //lblIS_Standard.Visible = false;
            //lblMinVelocity.Visible = false;
            //lblMinVelValue.Visible = false;
            //btnNext.Visible = false;
            //lblElasticMode.Visible = false;
            //btnSave.Visible = true;
            //lblTimeWCrack.Visible = false;
            //lblTimeWoCrack.Visible = false;
            //lblTmeWCrack.Visible = false;
        }

        private void btnParameterSet_Click(object sender, EventArgs e)
        {
            Common.isParameterModeClicked = true;
            Common.isTimeModeClicked = false;
            Common.isVelocityModeClicked = false;
            Common.isElasticModeClicked = false;
            Common.isCrack_depthModeClicked = false;
            Common.isSignal_displayModeClicked = false;
            Common.isNextBtnClicked = false;

            lblMainValue2.Text = "0000 ";
            lblUnituS.Visible = false;
            lblUnitMpS.Visible = false;
            lblUnitNpMM2.Visible = true;
            lblUNITmm.Visible = false;
            lblMode.Text = "Parameter Mode";
            lblSmallValue2.Text = Common.VelocityModeSet + " " + "m/s";
            lblIS_Standard.Visible = true;
            lblMinTime.Visible = true;
            lblSmallValue1.Visible = true;
            lblMinVelocity.Visible = true;
            lblSmallValue2.Visible = true;
            lblDrtMethod.Visible = true;
            btnSSave.Visible = false;
            btnASave.Visible = false;
            btnNext.Visible = false;
            lblElasticModes.Visible = false;
            btnSave.Visible = true;
            lblTimeWCrack.Visible = false;
            lblTimeWoCrack.Visible = false;
            lblTmeWCrack.Visible = false;
        }

        private void btnElasticModeSet_Click(object sender, EventArgs e)
        {
            Common.isElasticModeClicked = true;
            Common.isTimeModeClicked = false;
            Common.isVelocityModeClicked = false;
            Common.isCrack_depthModeClicked = false;
            Common.isParameterModeClicked = false;
            Common.isSignal_displayModeClicked = false;
            Common.isNextBtnClicked = false;

            lblMainValue2.Text = "0000";
            lblMode.Text = "Elastic Mode";
            lblUnituS.Visible = true;
            lblUnitMpS.Visible = false;
            lblUnitNpMM2.Visible = false;
            lblUNITmm.Visible = false;
            lblElasticModes.Visible = true;
            lblIS_Standard.Visible = false;
            lblMinTime.Visible = false;
            lblSmallValue1.Visible = false;
            lblMinVelocity.Visible = false;
            lblSmallValue2.Visible = false;
            lblDistance.Visible = false;
            lblDistanceValue.Visible = false;
            btnASave.Visible = false;
            btnSSave.Visible = false;
            btnSave.Visible = false;
            btnNext.Visible = true;
            lblTimeWCrack.Visible = false;
            lblTimeWoCrack.Visible = false;
            //lblL_Velocity.Visible = false;
            lblL_TimeMode.Visible = false;
            lblTmeWCrack.Visible = false;
            lblDrtMethod.Visible = false;
        }

        private void btnCrackDepthSet_Click(object sender, EventArgs e)
        {
            Common.isCrack_depthModeClicked = true;
            Common.isTimeModeClicked = false;
            Common.isVelocityModeClicked = false;
            Common.isElasticModeClicked = false;
            Common.isParameterModeClicked = false;
            Common.isSignal_displayModeClicked = false;
            Common.isNextBtnClicked = false;

            lblL_TimeMode.Visible = false;
            lblTmeWCrack.Visible = true;
            btnSSave.Visible = false;
            lblMainValue2.Text = "00";
            lblUnituS.Visible = false;
            lblUnitMpS.Visible = false;
            lblUnitNpMM2.Visible = false;
            lblUNITmm.Visible = true;
            lblMode.Text = "Crack Depth";
            lblIS_Standard.Visible = true;
            lblElasticModes.Visible = false;
            lblMinTime.Visible = false;
            lblSmallValue1.Visible = false;
            lblMinVelocity.Visible = false;
            lblSmallValue2.Visible = false;
            // lblTimeWCrack.Visible = true;
            lblTimeWoCrack.Visible = true;
            btnNext.Visible = true;
            btnSave.Visible = false;
            btnSave.Visible = false;
            btnASave.Visible = false;
            lblDistance.Visible = true;
            lblDistanceValue.Visible = true;
            lblSmallValue1.Visible = true;
            lblSmallValue2.Visible = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //isCrack_depthModeClicked = false;
            //isTimeModeClicked = false;
            //isVelocityModeClicked = false;
            //isElasticModeClicked = true;
            //isParameterModeClicked = false;
            //isSignal_displayModeClicked = false;
            //isNextBtnClicked = true;

            if (lblElasticModes.Text == "L-Time Mode")
            {
                lblElasticModes.Text = "L-Velocity Mode";
                Common.isLVelocityMode = true;
                Common.isSTimeMode = false;
                Common.isSVelocityMode = false;
                Common.isLTimeMode = false;
            }
            //lblElasticMode.Text = "L-Velocity Mode";

            else if (lblElasticModes.Text == "L-Velocity Mode")
            {
                lblElasticModes.Text = "S-Time Mode";
                Common.isSTimeMode = true;
                Common.isLVelocityMode = false;
                Common.isSVelocityMode = false;
                Common.isLTimeMode = false;
            }

            else if (lblElasticModes.Text == "S-Time Mode")
            {
                lblElasticModes.Text = "S-Velocity Mode";


                Common.isSVelocityMode = true;
                Common.isSTimeMode = false;
                Common.isLVelocityMode = false;
                Common.isLTimeMode = false;
            }

            else if (lblElasticModes.Text == "S-Velocity Mode")
            {
                pnlElasticModeResult.Visible = true;

                lblLongTimeValue.Text = $"{Common.longitudinalTime}  uS";
                lblShearTimeValue.Text = $"{Common.shearTime}  uS";
                lblDisValue.Text = $"{Common.Distance}  mm" ;
                lblLongVelValue.Text = $"{Common.longitudinalVelocity}  m/s";
                lblShearVelValue.Text = $"{Common.shearVelocity}  m/s";
                lblDensityVaue.Text = Common.Density;
                lblPoisonRatValue.Text = Common.poisonRatio;
                lblYounModValue.Text = $"{Common.youngModulus}  Kg/ms^2";
                lblBulkModValue.Text = $"{Common.bulkModulus}  Kg/ms^2";
                lblShearModValue.Text = $"{Common.shearModulus}  Kg/ms^2";

                btnSave.Visible = true;
                btnCalibration.Visible = false;
                lblCrackMode.Visible = false;

                Common.isElasticModeResult = true;
                Common.isSVelocityMode = false;
                Common.isSTimeMode = false;
                Common.isLVelocityMode = false;
                Common.isLTimeMode = false;
                lblDistanceValue.Visible = false;
                //Timer2.Stop();
            }


        }

        private void lblBatteryPer_Click(object sender, EventArgs e)
        {
            // lblBatteryPer1.BringToFront();
        }

        public void Timer2_Tick_1(object sender, EventArgs e)
        {
            // bool ischecked = true;

            Timer2.Stop();

            if (Common.isVelocityModeClicked == true)
            {
                lblMainValue2.Text = Common.VelocityModeSet;
                //lblMainValue3.Text = VelocityModeSet;
                lblUnituS.Visible = false;
                lblUnitMpS.Visible = true;
                lblUnitNpMM2.Visible = false;
                lblUNITmm.Visible = false;
                lblMode.Text = "Velocity Mode";
                lblSmallValue1.Visible = true;
                lblSmallValue1.Text = Common.TimeModeSet + " " + "uS";
                lblDistance.Visible = true;
                lblDistanceValue.Visible = true;
                btnASave.Visible = true;
                btnSSave.Visible = true;
                lblMinTime.Visible = true;
                lblDrtMethod.Visible = true;
                lblIS_Standard.Visible = false;
                lblMinVelocity.Visible = false;
                lblSmallValue2.Visible = false;
                btnNext.Visible = false;
                lblElasticModes.Visible = false;
                btnSave.Visible = true;
                lblTimeWCrack.Visible = false;
                lblTimeWoCrack.Visible = false;
                lblTmeWCrack.Visible = false;
                lblL_TimeMode.Visible = false;
                pnlElasticModeResult.Visible = false;
                lblCrackMode.Visible = false;
                pnlElasticModeResult.Visible = false;
            }

            else if (Common.isParameterModeClicked == true)
            {
                lblMainValue2.Text = Common.ParameterMode;
                lblUnituS.Visible = false;
                lblUnitMpS.Visible = false;
                lblUnitNpMM2.Visible = true;
                lblUNITmm.Visible = false;
                lblMode.Text = "Parameter Mode";
                lblSmallValue1.Text = Common.TimeModeSet + " " + "uS";
                lblSmallValue2.Text = Common.VelocityModeSet + " " + "m/s";
                lblIS_Standard.Visible = true;
                lblMinTime.Visible = true;
                lblSmallValue1.Visible = true;
                lblMinVelocity.Visible = true;
                lblSmallValue2.Visible = true;
                lblDrtMethod.Visible = true;
                btnSSave.Visible = false;
                btnASave.Visible = false;
                btnNext.Visible = false;
                lblElasticModes.Visible = false;
                btnSave.Visible = true;
                lblTimeWCrack.Visible = false;
                lblTimeWoCrack.Visible = false;
                pnlElasticModeResult.Visible = false;
                lblCrackMode.Visible = false;
                pnlElasticModeResult.Visible = false;
            }

            else if (Common.isElasticModeClicked)
            {
                lblMode.Text = "Elastic Mode";
                lblMainValue2.Text = "0000";
                lblUnitNpMM2.Visible = false;
                lblUNITmm.Visible = false;
                lblElasticModes.Visible = true;
                lblIS_Standard.Visible = false;
                lblMinTime.Visible = false;
                lblSmallValue1.Visible = false;
                lblMinVelocity.Visible = false;
                lblSmallValue2.Visible = false;
                lblDistance.Visible = false;
                lblDistanceValue.Visible = false;
                btnASave.Visible = false;
                btnSSave.Visible = false;
                btnSave.Visible = false;
                btnNext.Visible = true;
                lblTimeWCrack.Visible = false;
                lblTimeWoCrack.Visible = false;

                if (Common.isLVelocityMode)
                {
                    lblMainValue2.Text = Common.longitudinalVelocity;
                    lblSmallValue1.Visible = true;
                    lblSmallValue1.Text = $"{Common.TimeModeSet} uS";
                    lblDistance.Visible = true;
                    lblDistanceValue.Visible = true;
                    lblUnitMpS.Visible = true;
                    lblUnituS.Visible = false;
                    btnCalibration.Visible = false;
                    lblL_TimeMode.Visible = true;
                }
                else if (Common.isSTimeMode)
                {
                    lblMainValue2.Text = Common.TimeModeSet;
                    lblUnituS.Visible = true;
                    lblUnitMpS.Visible = false;
                    lblL_TimeMode.Visible = false;
                    btnCalibration.Visible = true;
                }
                else if (Common.isSVelocityMode)
                {
                    lblMainValue2.Text = Common.shearVelocity;
                    lblSmallValue1.Visible = true;
                    lblSmallValue1.Text = $"{Common.TimeModeSet} uS";
                    lblDistance.Visible = true;
                    lblDistanceValue.Visible = true;
                    lblUnituS.Visible = false;
                    lblUnitMpS.Visible = true;
                    lblL_TimeMode.Visible = true;
                    btnCalibration.Visible = false;
                }

                //else if (isElasticModeResult)
                //{
                //    pnlElasticModeResult.Visible = true;
                //    btnSave.Visible = true;
                //    lblDistance.Visible = false;
                //    lblDistanceValue.Visible = false;
                //}
                else
                {
                    lblElasticModes.Text = "L-Time Mode";
                    lblMainValue2.Text = Common.TimeModeSet;
                    lblUnituS.Visible = true;
                    lblUnitMpS.Visible = false;
                    lblL_TimeMode.Visible = false;
                    btnCalibration.Visible = true;
                }
            }


            else if (Common.isCrack_depthModeClicked == true)
            {
                //lblCrackMode
                lblMainValue2.Text = "00";
                lblUnituS.Visible = false;
                lblUnitMpS.Visible = false;
                lblUnitNpMM2.Visible = false;
                lblUNITmm.Visible = true;
                lblCrackMode.Visible = true;
                lblMode.Text = "Crack Depth";

                //if(lblIS_Standard.Text == "IS-516 Standard")
                //{
                if (Common.crackDepth == "0")
                {
                    lblCrackMode.Text = "Time With Crack (uS)";
                }
                else if (Common.crackDepth == "1")
                {
                    lblCrackMode.Text = "Time without Crack (uS)";
                }
                else if (Common.crackDepth == "2")
                {
                    lblCrackMode.Text = "RESULT (mm)";
                }
                //}
                //else
                //{
                //    if (Common.crackDepth == "0")
                //    {
                //        lblCrackMode.Text = "TIME 1 AT DIST: 150mm";
                //    }
                //    else if (Common.crackDepth == "1")
                //    {
                //        lblCrackMode.Text = "TIME 2 AT DIST: 300mm";
                //    }
                //    else if(Common.crackDepth == "2")
                //    {
                //        lblCrackMode.Text = "RESULT";
                //    }
                //}

                //lblMainValue2.Text = crackDepth;
                lblSmallValue1.Text = $"{ Common.timeCrack} uS";
                lblSmallValue2.Text = $"{Common.timeWoCrack} uS";
                lblIS_Standard.Visible = true;
                lblElasticModes.Visible = false;
                lblMinTime.Visible = false;
                lblSmallValue1.Visible = false;
                lblMinVelocity.Visible = false;
                lblSmallValue2.Visible = false;
                //  lblTimeWCrack.Visible = true;
                lblTimeWoCrack.Visible = true;
                btnNext.Visible = true;
                btnSave.Visible = false;
                btnSave.Visible = false;
                btnASave.Visible = false;
                lblDistance.Visible = true;
                lblDistanceValue.Visible = true;
                lblSmallValue1.Visible = true;
                lblSmallValue2.Visible = true;
                lblTmeWCrack.Visible = true;
                pnlElasticModeResult.Visible = false;
                lblCrackMode.Visible = false;
                // lblCrackMode.Visible = false;
                pnlElasticModeResult.Visible = false;
            }

            else
            {
                lblMainValue2.Text = Common.TimeModeSet;
                lblMainValue2.Visible = true;
                lblUnituS.Visible = true;
                lblUnitMpS.Visible = false;
                lblUnitNpMM2.Visible = false;
                lblUNITmm.Visible = false;
                lblSmallValue1.Visible = false;
                lblMinTime.Visible = false;
                btnASave.Visible = false;
                btnSSave.Visible = false;
                lblDistance.Visible = false;
                lblDistanceValue.Visible = false;
                lblMode.Text = "Time Mode";
                lblDrtMethod.Visible = false;
                lblMinVelocity.Visible = false;
                lblSmallValue2.Visible = false;
                lblIS_Standard.Visible = false;
                btnNext.Visible = false;
                lblElasticModes.Visible = false;
                btnSave.Visible = true;
                lblTimeWCrack.Visible = false;
                lblTimeWoCrack.Visible = false;
                picSignal100.Visible = false;
                picSignal80.Visible = false;
                picSignal60.Visible = false;
                picSignal40.Visible = false;
                picSignal15.Visible = false;
                picNoSignal.Visible = true;
                pnlElasticModeResult.Visible = false;
            }

            //////////// For Freezing Parameter 

            if (Common.paraFreeze == "1")  // Freeze
            {
                pbAsterisk.BackgroundImage = Properties.Resources.asterisk__4_;
            }
            else if (Common.paraFreeze == "0" || Common.paraFreeze == "2")  // Unfreeze
            {
                pbAsterisk.BackgroundImage = Properties.Resources.asterisk__3_;
            }

            ///////////// For Stable Reading indicator

            if (Common.readStableInd == "1")   // Stable Reading
            {
                pbRectangle.BackgroundImage = Properties.Resources.rounded_rectangle__1_;
            }
            else if (Common.readStableInd == "0")  // Unstable Reading
            {
                pbRectangle.BackgroundImage = Properties.Resources.rounded_rectangle;
            }





            /////////// Defining tx_buf characters

            if (mySerialPort.IsOpen == true)
            {
                //if (ULR0_Req == true)
                //{
                //    char[] tx_buf = new char[] { DC1, ULR_0, DC3 };
                //}

                //else if (ULR1_Req == true)
                //{
                //    char[] tx_buf = new char[] { DC1, ULR_1, DC3 };
                //}

                SerialAction();
                if (int.TryParse(lblStatus.Text, out int statusValue))
                {
                    switch (statusValue)
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
                            lblStatus.Text = " RX BATTERY LOW";
                            break;
                        case 7:
                            lblStatus.Text = "ELASTIC CAL ERROR";
                            break;
                        case 8:
                            lblStatus.Text = "READING OUT OF LIMIT";
                            break;
                        case 9:
                            lblStatus.Text = "DUPLICATESITE/OBJECTNAME";
                            break;
                        case 10:
                            lblStatus.Text = "DUPLICATEUSERNAME";
                            break;
                        case 11:
                            lblStatus.Text = "INVALIDPASSWORD";
                            break;
                        case 12:
                            lblStatus.Text = "INCVALID v READINGS";
                            break;
                        case 13:
                            lblStatus.Text = "PLEASE FOLLOW PROPER TETSING";
                            break;
                        case 14:
                            lblStatus.Text = "PLEASE REFER IS STANDARD";
                            break;
                        // Add more cases for other status values
                        default:
                            lblStatus.Text = "UNKNOWN STATUS";
                            break;
                    }
                }

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

                    lblBatteryPer.Text = $"{BatteryStatus} %";
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
                //if (int.TryParse(lblGain.Text, out int Gain))
                //{
                //    picBoxGainMin.Visible = false;
                //    picBoxGainMedium.Visible = false;
                //    picBoxGainHigh.Visible = false;

                //    switch (Gain)
                //    {
                //        case int n when n > 0 && n <= 34:
                //            picBoxGainMin.Visible = true;
                //            break;

                //        case int n when n >= 35 && n <= 65:
                //            picBoxGainMedium.Visible = true;
                //            break;

                //        case int n when n >= 66 && n <= 100:
                //            picBoxGainHigh.Visible = true;
                //            break;
                //    }
                    
                //    lblSignalStrength.Text = $"{Gain}dB";

                //}
            }



            //else
            //{
            //    lblStatus.Text = "INVALID STATUS FORMAT";
            //}


            //lblMainValue2.Text = TimeModeSet;
            // lblMainValue2.Text = VelocityModeSet + " " + "m/s";


            Timer2.Start();
        }

        public void SerialAction()
        {
            char DC1 = Common.dc1;
            char DC3 = Common.dc3;
            //char ULA0 = Common.ula_0;
            int l = Common.L;

            if (Common.ULR2_Req == true)
            {
                mySerialPort.Write(Common.tx_buf1, 0, l);
            }
            else if (Common.ULR1_Req == true)
            {
                char[] tx_buf = new char[] { DC1, ULR_1, sec3, DC3 };
                mySerialPort.Write(tx_buf, 0, 4);
            }
            else if (Common.ULR0_Req == true) //|| ULR3_Req == true || ULR4_Req == true)
            {
                char[] tx_buf = new char[] { DC1, ULR_0, sec1, DC3 };
                mySerialPort.Write(tx_buf, 0, 4);
            }

            string rx_buf = mySerialPort.ReadExisting(); // Reading the response from the serial port
            int rxlength = rx_buf.Length;
            try
            {
                if (rx_buf[rxlength - 1] == DC3)
                {
                    if (rx_buf[1] == ULA_0)
                    {
                        if (rx_buf[2] == sec1)
                        {
                            //string[] stringValues = Common.stringValues;

                            string[] stringValues = rx_buf.Split(Common.PC_REC);

                            lblStatus.Text = stringValues[1];
                            Common.TimeModeSet = stringValues[2];
                            Common.VelocityModeSet = stringValues[3];

                            lblBatteryPer.Text = stringValues[5];
                            lblSignalStrength.Text = stringValues[6];
                            lblGain.Text = stringValues[7] + "dB";

                            Common.shearVelocity = stringValues[9];
                            Common.shearTime = "00";
                            Common.longitudinalTime = "00";
                            Common.poisonRatio = stringValues[10];
                            Common.youngModulus = stringValues[11];
                            Common.shearModulus = stringValues[12];
                            Common.bulkModulus = stringValues[13];
                            Common.longitudinalVelocity = stringValues[14];
                            Common.timeCrack = stringValues[15];
                            Common.timeWoCrack = stringValues[16];
                            Common.crackDepth = stringValues[18];

                            Common.ParameterMode = stringValues[20];
                            Common.paraFreeze = stringValues[21];
                            Common.UPVReadingSave = stringValues[22];
                            Common.readSaveCont = stringValues[23];

                            if (Common.UPVReadingSave == "1")
                            {
                                MessageBox.Show($"DATA SAVED, DATA NUM {Common.readSaveCont}");
                            }

                            Common.readStableInd = stringValues[24];
                            Common.longitudinalTime = stringValues[25];
                            Common.shearTime = stringValues[26];

                            if (stringValues[8] == "1")
                            {
                                Common.ULR0_Req = false;
                                Common.ULR1_Req = true;
                                Common.ULR2_Req = false;

                            }
                        }
                        //stopcntr = 0;
                        
                    }

                    else if (rx_buf[1] == ULA_1)
                    {
                        if (rx_buf[2] == sec1)
                        {
                            string[] stringValues = rx_buf.Split(Common.PC_REC);

                            if (stringValues[1] == "1")
                            {
                                lblDeviceName.Text = "UX4600L";
                            }
                            else if (stringValues[1] == "2")
                            {
                                lblDeviceName.Text = "UX4605";
                            }
                            else if (stringValues[1] == "3")
                            {
                                lblDeviceName.Text = "UX4606";
                            }
                            else if (stringValues[1] == "4")
                            {
                                lblDeviceName.Text = "UX4607";
                            }
                            else if (stringValues[1] == "5")
                            {
                                lblDeviceName.Text = "UX4612";
                            }
                            else if (stringValues[1] == "6")
                            {
                                lblDeviceName.Text = "UX4620";
                            }


                            Common.Distance = stringValues[3];
                            Common.calReference = stringValues[4];
                            Common.velocityMax = stringValues[5];
                            Common.velocityMin = stringValues[6];
                            Common.Density = stringValues[7];

                            if (stringValues[9] == "1")
                            {
                                Common.Pulse = true;
                            }
                            else if (stringValues[9] == "2")
                            {
                                Common.Continuous = true;
                            }

                            if (stringValues[10] == "1")
                            {
                                Common.windowFunction = "ON";
                            }
                            else if (stringValues[10] == "0" || stringValues[10] == "2")
                            {
                                Common.windowFunction = "OFF";
                            }
                            if (stringValues[11] == "0")
                            {
                                Common.IS516 = true;
                            }
                            else if (stringValues[11] == "1")
                            {
                                Common.IS13311 = true;
                            }

                            Common.xDistance = stringValues[12];
                            Common.PulseRepeatFreq = stringValues[13];
                            Common.windowDelay = stringValues[17];
                            Common.windowOnTime = stringValues[16];

                            if (stringValues[14] == "10")
                            {
                                Common.filterFrequency = "10";
                            }
                            else if (stringValues[14] == "20")
                            {
                                Common.filterFrequency = "20";
                            }
                            else if (stringValues[14] == "30")
                            {
                                Common.filterFrequency = "30";
                            }
                            else if (stringValues[14] == "40")
                            {
                                Common.filterFrequency = "40";
                            }
                            else if (stringValues[14] == "50")
                            {
                                Common.filterFrequency = "50";
                            }
                            else if (stringValues[14] == "60")
                            {
                                Common.filterFrequency = "60";
                            }
                            else if (stringValues[14] == "70")
                            {
                                Common.filterFrequency = "70";
                            }
                            else if (stringValues[14] == "80")
                            {
                                Common.filterFrequency = "80";
                            }
                            else if (stringValues[14] == "90")
                            {
                                Common.filterFrequency = "90";
                            }
                            else if (stringValues[14] == "100")
                            {
                                Common.filterFrequency = "100";
                            }
                            else if (stringValues[14] == "110")
                            {
                                Common.filterFrequency = "110";
                            }
                            else if (stringValues[14] == "120")
                            {
                                Common.filterFrequency = "120";
                            }
                            else if (stringValues[14] == "130")
                            {
                                Common.filterFrequency = "130";
                            }
                            else if (stringValues[14] == "140")
                            {
                                Common.filterFrequency = "140";
                            }
                            else if (stringValues[14] == "150")
                            {
                                Common.filterFrequency = "150";
                            }

                            if (stringValues[18] == "1")
                            {
                                Common.gainControl = "AUTO";
                            }
                            else if (stringValues[18] == "0" || stringValues[18] == "2")
                            {
                                Common.gainControl = "MANUAL";
                            }

                            Common.readingSaveTime = stringValues[33];
                            Common.calRefShear = stringValues[35];

                            if (stringValues[39] == "1")
                            {
                                Common.Auto = true;
                            }
                            else if (stringValues[39] == "2")
                            {
                                Common.Manual = true;
                            }

                            Common.parameterHoldTime = stringValues[40];
                            Common.gainSetting = stringValues[42];
                            Common.txVoltage = stringValues[43];

                            if (stringValues[45] == "0")
                            {
                                Common.Slow = true;
                            }
                            else if (stringValues[45] == "1")
                            {
                                Common.Medium = true;
                            }
                            else if (stringValues[45] == "2")
                            {
                                Common.Fast = true;
                            }

                            //ParaSetting paraSetting = new ParaSetting();
                            Common.ULR0_Req = true;
                            Common.ULR1_Req = false;
                            MessageBox.Show($"Received values: {string.Join(", ", stringValues)}");

                        }

                        else if(rx_buf[2] == sec2)
                        {

                        }

                        else if(rx_buf[2] == sec3)
                        {
                            Common.stringValues = rx_buf.Split(Common.PC_REC);
                            Common.ULR0_Req = false;
                            Common.ULR1_Req = true;
                            Common.ULR2_Req = false;

                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
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
                        Timer2.Interval = 100;
                        Timer2.Enabled = true;
                        Timer2.Start();

                        MessageBox.Show($"Connected to {portConnection.cmbPortName}");
                        Common.ULR1_Req = true;
                        Common.ULR0_Req = false;
                        Common.ULR2_Req = false;
                        Common.ULR3_Req = false;
                        Common.ULR4_Req = false;
                        //SerialAction();
                        //  ReceiveDataViaSerialPort();
                        //lblMainValue2.Text = TimeModeSet + " " + "uS";
                        // 
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

        private void btnParameterGraph_Click(object sender, EventArgs e)
        {
            Common.ULR1_Req = true;
            Common.ULR0_Req = false;
            SerialAction();
            ParaGraphofVelocity_Strength parameterGraph = new ParaGraphofVelocity_Strength();
            parameterGraph.ShowDialog();
        }
    }
}