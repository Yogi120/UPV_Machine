using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace UPV_Machine
{
    public static class Common
    {
        //public static class SerialPortManager
        //{
        //    public static SerialPort mySerialPort; // Declare static SerialPort object
        //    //string[] static ports = SerialPort.GetPortNames();
        //    // Initialize the serial port with your desired settings
        //    public static void InitializeSerialPort()
        //    {
        //        mySerialPort = new SerialPort();
        //        mySerialPort.PortName = "COM1"; // Set your COM port name
        //        mySerialPort.BaudRate = 9600;   // Set your baud rate
        //                                        // Set other properties as needed
        //        mySerialPort.Open(); // Open the port
        //    }
        //}



        public static string[] stringValues = new string[80];

        public static string SystemStatus;
        public static string TimeModeSet { get; set; }

        public static string VelocityModeSet;
        public static string Signal;
        public static int BatteryStatus;
        public static string Gain;
        public static int elasticMode;
        public static string crackDepth;
        public static string timeCrack;
        public static string timeWoCrack;
        public static string ParameterMode;
        public static string shearTime;
        public static string longitudinalTime;
        public static string shearVelocity;
        public static string longitudinalVelocity;
        public static string poisonRatio;
        public static string youngModulus;
        public static string shearModulus;
        public static string bulkModulus;
        public static string paraFreeze;
        public static string UPVReadingSave;
        public static string readSaveCont;
        public static string readStableInd;

        public static int batstat;
        public static char PC_REC = (char)0x14;
        public static bool ULR0_Req, ULR1_Req, ULR2_Req, ULR3_Req, ULR4_Req = true;
        public static char[] tx_buf;
        public static char[] tx_buf1 = new char[200];
        public static char dc1 = '\u0011';
        public static char dc3 = '\u0013';

        public static char ulr_0 = '\u000A';
        public static char ulr_1 = '\u000B';

        public static char ula_0 = '\u001A';
        public static char ula_1 = '\u001B';
        public static char ula_2 = '\u001C';

        public static char Sec1 = '\u0001';
        public static char Sec2 = '\u0002';
        public static char Sec3 = '\u0003';
        public static int L;
        public static char[] tx_buff;

        public static bool isTimeModeClicked = false;
        public static bool isVelocityModeClicked = false;
        public static bool isParameterModeClicked = false;
        public static bool isElasticModeClicked = false;
        public static bool isCrack_depthModeClicked = false;
        public static bool isSignal_displayModeClicked = false;
        public static bool isNextBtnClicked = false;
        public static bool isLVelocityMode = false;
        public static bool isSTimeMode = false;
        public static bool isLTimeMode = false;
        public static bool isSVelocityMode = false;
        public static bool isElasticModeResult = false;

        public static int initialWidth;
        public static bool isHidden;


        ////////////////    PARAMETER SETTING       //////////////////

        public static bool IS516;
        public static bool IS13311;
        public static string Distance;
        public static string calReference;
        public static string xDistance;
        public static string PulseRepeatFreq;
        public static string velocityMin;
        public static string velocityMax;
        public static string parameterFreez;
        public static string UXNoOfPulse;
        public static string calRefShear;
        public static string txVoltage;
        public static string parameterHoldTime;
        public static string gainControl;
        public static string gainSetting;
        public static string filterFrequency;
        public static string windowOnTime;
        public static string windowDelay;
        public static string windowFunction;
        public static string deviceMode;
        public static string correctionFactor;
        public static string readingAutoSave;
        public static string readingSaveTime;
        public static string uxMode;
        public static string paraResponse;
        public static string Density;
        public static bool Pulse;
        public static bool Continuous;
        public static bool Auto;
        public static bool Manual;
        public static bool Slow;
        public static bool Medium;
        public static bool Fast;



    }
}
