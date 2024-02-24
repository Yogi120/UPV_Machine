﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UPV_Machine
{
    public partial class ParaGraphofVelocity_Strength : Form
    {
        public ParaGraphofVelocity_Strength()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ParaGraphofVelocity_Strength_Load(object sender, EventArgs e)
        {
            lblActCurve.Visible = false;
            lblFormula.Visible = false;
            pcActCurve.Visible = false;
            //MessageBox.Show($"Received values: {string.Join(", ", Common.stringValues)}");

            gridviewVelocityStrength.Rows.Clear();
            gridviewVelocityStrength.Columns.Clear();

            // Add serial number column
            DataGridViewTextBoxColumn serialNumberColumn = new DataGridViewTextBoxColumn();
            serialNumberColumn.Name = "SerialNumberColumn";
            serialNumberColumn.HeaderText = "Sr.No.";
            serialNumberColumn.ReadOnly = true; // Make the column non-editable
            gridviewVelocityStrength.Columns.Add(serialNumberColumn);

            // Add velocity column
            gridviewVelocityStrength.Columns.Add("VelocityColumn", "VELOCITY (M/s)");

            // Add strength column
            gridviewVelocityStrength.Columns.Add("StrengthColumn", "STRENGTH (N/mm^2)");

            // Add data to DataGridView
            for (int i = 1; i <= 40; i++)
            {
                // Serial number is from 1 to 40
                int serialNumber = i;

                // Velocity and strength values from your data array
                string velocity = Common.stringValues[i];
                string strength = Common.stringValues[i + 40]; // Strength values start from index 40

                // Add row to DataGridView
                gridviewVelocityStrength.Rows.Add(serialNumber, velocity, strength);
            }
            foreach (DataGridViewColumn column in gridviewVelocityStrength.Columns)
            {
                column.HeaderCell.Style.Font = new Font(gridviewVelocityStrength.Font, FontStyle.Bold);
            }
        }

        private void btnGraphicalRepresentation_Click_1(object sender, EventArgs e)
        {
            lblActCurve.Visible = true;
            lblFormula.Visible = true;
            pcActCurve.Visible = true;

            chart1.Series.Clear();

            // Create a new series for the chart
            Series series = new Series();
            series.ChartType = SeriesChartType.Line; // Set chart type to line

            // Variables to store maximum velocity and strength values
            double maxVelocity = double.MinValue;
            double maxStrength = double.MinValue;

            // Add data points to the series from DataGridView and calculate maximum values
            foreach (DataGridViewRow row in gridviewVelocityStrength.Rows)
            {
                if (!row.IsNewRow) // Exclude new row if any
                {
                    // Retrieve velocity and strength values from DataGridView
                    double velocity, strength;
                    if (double.TryParse(row.Cells["VelocityColumn"].Value?.ToString(), out velocity) &&
                        double.TryParse(row.Cells["StrengthColumn"].Value?.ToString(), out strength))
                    {
                        // Add data points to the series
                        series.Points.AddXY(velocity, strength);

                        // Update maximum velocity and strength values
                        if (velocity > maxVelocity)
                            maxVelocity = velocity;
                        if (strength > maxStrength)
                            maxStrength = strength;
                    }
                }
            }

            // Add the series to the chart
            chart1.Series.Add(series);

            // Customize chart appearance (if needed)
            chart1.ChartAreas[0].AxisX.Title = "Velocity (m/s)";
            chart1.ChartAreas[0].AxisY.Title = "Strength (N/mm^2)";
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false; // Disable major gridlines
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Minimum = 0; // Set minimum value for X axis
            chart1.ChartAreas[0].AxisY.Minimum = 0; // Set minimum value for Y axis

            // Calculate interval for X-axis
            int intervalX = (int)Math.Round(maxVelocity / 20);
            chart1.ChartAreas[0].AxisX.Interval = intervalX;

            // Calculate interval for Y-axis
            int intervalY = (int)Math.Round(maxStrength / 20.0);
            chart1.ChartAreas[0].AxisY.Interval = intervalY;


            chart1.ChartAreas[0].AxisX.Maximum = Math.Ceiling(maxVelocity / 10) * 10; // Round up to nearest multiple of 10
            chart1.ChartAreas[0].AxisY.Maximum = Math.Ceiling(maxStrength / 10) * 10; // Round up to nearest multiple of 10

            // Customize series appearance
            series.Color = Color.Blue; // Set line color
            series.BorderWidth = 2; // Set line thickness
            series.MarkerStyle = MarkerStyle.Circle; // Set marker style
            series.MarkerSize = 8; // Set marker size
            series.MarkerColor = Color.Red; // Set marker color
        }

        
        //{

        //}
    }
}
