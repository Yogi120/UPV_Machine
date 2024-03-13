using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;

namespace UPV_Machine
{
    public partial class SignalDisplayGrapgh : Form
    {
        //private SerialPort serialPort;
        // private Chart chart;
        public SignalDisplayGrapgh()
        {
            //timer1.Start();


            InitializeComponent();
            timer1.Start();
        }


        private void SignalDisplayGrapgh_Load(object sender, EventArgs e)
        {
            string sysPara = Common.stringValues[2];

            if (sysPara == "1") 
            {
                lblStatus.Text = "System Ok";
            }
            else if(sysPara == "2")
            {
                lblStatus.Text = "BATTERY LOW";
            }
            else if(sysPara == "3")
            {
                lblStatus.Text = "VELOCITY LOW";
            }
            else if(sysPara == "4")
            {
                lblStatus.Text = "VELOCITY HIGH";
            }
            else if(sysPara == "5")
            {
                lblStatus.Text = "CALIBARTION ERROR";
            }
            else if(sysPara == "6")
            {
                lblStatus.Text = "TX BATTERY LOW";
            }
            else if(sysPara == "7")
            {
                lblStatus.Text = "RX BATTERY LOW";
            }
            else if(sysPara == "8")
            {
                lblStatus.Text = "ELASTIC CAL ERROR";
            }
            else if(sysPara == "9")
            {
                lblStatus.Text = "READING OUTOFLIMIT";
            }
            else if(sysPara == "10")
            {
                lblStatus.Text = "DUPLICATE SITE/OBJECT NAME";
            }
            else if(sysPara == "11")
            {
                lblStatus.Text = "DUPLICATE USER NAME";
            }
            else if(sysPara == "12")
            {
                lblStatus.Text = "INVALID PASSWORD";
            }
            else if(sysPara == "13")
            {
                lblStatus.Text = "INVALID READINGS";
            }
            else if(sysPara == "14")
            {
                lblStatus.Text = " PLEASE FOLLOW PROPER TETSING";
            }
            else if(sysPara == "15")
            {
                lblStatus.Text = "PLEASE REFER IS STANDARD";
            }
            else if(sysPara == "16")
            {
                lblStatus.Text = "PLEASE CONNECT PROPER CAL BLOCK";
            }
            else if(sysPara == "17")
            {
                lblStatus.Text = "CAL CERTIFICATE EXPIRED";
            }
            else if(sysPara == "18")
            {
                lblStatus.Text = " MAX READING SAVED,PLEASE CREATE NEW SITE";
            }

            lblMsrTime.Text = Common.stringValues[2];
            lblCsrTime.Text = Common.stringValues[3];
            lblBatStat.Text = Common.stringValues[5];
            lblGainValue.Text = Common.stringValues[7];



            // Check if Common.stringValues has data
            if (Common.stringValues != null && Common.stringValues.Length > 1)
            {
                // Clear previous data
                chart1.Series.Clear();

                // Add new series for sine wave
                var series = new Series();
                series.ChartType = SeriesChartType.Spline;
                series.Color = Color.Yellow;
                series.BorderWidth = 2;
                chart1.Series.Add(series);

                // chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 2;


                // Add center vertical line
                var verticalLine = new Series();
                verticalLine.ChartType = SeriesChartType.Line;
                verticalLine.Points.AddXY(125, 0); // Vertical line at x = 125
                verticalLine.Points.AddXY(125, 160); // Length of the line
                verticalLine.Color = Color.White;
                verticalLine.BorderWidth = 2;
                chart1.Series.Add(verticalLine);

                // Add Vertical line at last 
                var lastVerticalLine = new Series();
                lastVerticalLine.ChartType = SeriesChartType.Line;
                lastVerticalLine.Points.AddXY(250, 0);
                lastVerticalLine.Points.AddXY(250, 160);
                lastVerticalLine.Color = Color.White;
                lastVerticalLine.BorderWidth = 2;
                chart1.Series.Add(lastVerticalLine);

                // Add Horizontal line at last 
                var lastHorizontalLine = new Series();
                lastHorizontalLine.ChartType = SeriesChartType.Line;
                lastHorizontalLine.Points.AddXY(0, 160);
                lastHorizontalLine.Points.AddXY(250, 160);
                lastHorizontalLine.Color = Color.White;
                lastHorizontalLine.BorderWidth = 2;
                chart1.Series.Add(lastHorizontalLine);

                // Add center horizontal line
                var horizontalLine = new Series();
                horizontalLine.ChartType = SeriesChartType.Line;
                horizontalLine.Points.AddXY(0, 80); // Horizontal line at y = 80
                horizontalLine.Points.AddXY(250, 80); // Length of the line
                horizontalLine.Color = Color.White;
                horizontalLine.BorderWidth = 2;
                chart1.Series.Add(horizontalLine);

                for (double i = 0; i <= 250; i += 5) // Adjust step size as needed
                {
                    var verticalMarker = new Series();
                    verticalMarker.ChartType = SeriesChartType.Line;
                    verticalMarker.Points.AddXY(i, 78);
                    verticalMarker.Points.AddXY(i, 82); // Adjust length of vertical line
                    verticalMarker.Color = Color.White;
                    verticalMarker.BorderWidth = 2;
                    chart1.Series.Add(verticalMarker);
                }

                for (double i = 0; i <= 160; i += 5.33) // Adjust step size as needed
                {
                    var horizontalMarker = new Series();
                    horizontalMarker.ChartType = SeriesChartType.Line;
                    horizontalMarker.Points.AddXY(123, i); // Vertical line at x = 125
                    horizontalMarker.Points.AddXY(127, i); // Length of the line
                    horizontalMarker.Color = Color.White;
                    horizontalMarker.BorderWidth = 2;
                    chart1.Series.Add(horizontalMarker);
                }

                // Set chart area properties
                chart1.ChartAreas[0].BackColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.LineColor = Color.White;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
                //chart1.ChartAreas[0].AxisX.LineColor = Color.Transparent; // Hide x-axis line
                chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false; // Hide x-axis tick marks
                chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false; // Hide x-axis labels
                chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false; // Hide y-axis tick marks
                chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false; // Hide y-axis labels


                /*
                 * chart1.ChartAreas[0].BackColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LineColor = Color.Transparent; // Hide x-axis line
                chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false; // Hide x-axis tick marks
                chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false; // Hide x-axis labels
                chart1.ChartAreas[0].AxisY.LineColor = Color.Transparent; // Hide y-axis line
                chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false; // Hide y-axis tick marks
                chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false; // Hide y-axis labels
                 */

                // Increase spacing between the dots of the dotted grid lines
                chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 2;

                // Set y-axis range to 0 to 160 with interval of 10
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 160;
                chart1.ChartAreas[0].AxisY.Interval = 26.666;

                // Set x-axis range to start from 0 and go to 250 with intervals of 1
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = 250;
                chart1.ChartAreas[0].AxisX.Interval = 25;
            }
            else
            {
                MessageBox.Show("No data available to display.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double crsPosA = double.TryParse(Common.stringValues[10].Trim(), out double parsedValue) ? parsedValue : 0;
            double crsPosB = double.TryParse(Common.stringValues[11].Trim(), out double parsedValue2) ? parsedValue2 : 0;


            foreach (var series in chart1.Series)
            {
                series.IsVisibleInLegend = false; // Hide the series from the legend
            }

            if (chart1.Series.Count > 0)
            {
                chart1.Series[0].Points.Clear();
            }
            else
            {
                // If there are no series in the collection, add a new one
                chart1.Series.Add(new Series());
            }

            // Clear cursor position lines
            for (int i = chart1.Series.Count - 1; i >= 0; i--)
            {
                if (chart1.Series[i].Name.StartsWith("CursorPos"))
                {
                    chart1.Series.RemoveAt(i);
                }
            }

            if (Common.stringValues != null && Common.stringValues.Length > 1)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(Common.stringValues[1]);
                chart1.Series[0].Points.Clear();

                for (int i = 0; i < asciiBytes.Length; i++)
                {
                    double xValue = i; // Use index as x-value
                    double yValue = asciiBytes[i];
                    chart1.Series[0].Points.AddXY(xValue, yValue);
                }

                // Add Curson position A  line
                var cursorPosA = new Series();
                cursorPosA.Name = "CursorPosA"; // Set a unique name for the series
                cursorPosA.ChartType = SeriesChartType.Line;
                cursorPosA.Points.AddXY(crsPosA, 0); // Vertical line at x = 125
                cursorPosA.Points.AddXY(crsPosA, 160); // Length of the line
                cursorPosA.Color = Color.DarkGreen;
                cursorPosA.BorderWidth = 3; // Set the line width to 2 (or adjust as needed)
                chart1.Series.Add(cursorPosA);

                // Add Curson position B line
                var cursorPosB = new Series();
                cursorPosB.Name = "CursorPosB"; // Set a unique name for the series
                cursorPosB.ChartType = SeriesChartType.Line;
                cursorPosB.Points.AddXY(crsPosB, 0); // Vertical line at x = 125
                cursorPosB.Points.AddXY(crsPosB, 160); // Length of the line
                cursorPosB.Color = Color.Blue;
                cursorPosB.BorderWidth = 3; // Set the line width to 2 (or adjust as needed)
                chart1.Series.Add(cursorPosB);


            }

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
