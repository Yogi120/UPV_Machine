
namespace UPV_Machine
{
    partial class ParaGraphofVelocity_Strength
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.gridviewVelocityStrength = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnGraphicalRepresentation = new System.Windows.Forms.Button();
            this.lblActCurve = new System.Windows.Forms.Label();
            this.pcActCurve = new System.Windows.Forms.PictureBox();
            this.lblFormula = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewVelocityStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcActCurve)).BeginInit();
            this.SuspendLayout();
            // 
            // gridviewVelocityStrength
            // 
            this.gridviewVelocityStrength.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridviewVelocityStrength.Dock = System.Windows.Forms.DockStyle.Right;
            this.gridviewVelocityStrength.Location = new System.Drawing.Point(1053, 0);
            this.gridviewVelocityStrength.Name = "gridviewVelocityStrength";
            this.gridviewVelocityStrength.Size = new System.Drawing.Size(317, 721);
            this.gridviewVelocityStrength.TabIndex = 0;
            this.gridviewVelocityStrength.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Left;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1047, 721);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // btnGraphicalRepresentation
            // 
            this.btnGraphicalRepresentation.BackColor = System.Drawing.Color.Blue;
            this.btnGraphicalRepresentation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGraphicalRepresentation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGraphicalRepresentation.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGraphicalRepresentation.ForeColor = System.Drawing.Color.White;
            this.btnGraphicalRepresentation.Location = new System.Drawing.Point(957, 644);
            this.btnGraphicalRepresentation.Name = "btnGraphicalRepresentation";
            this.btnGraphicalRepresentation.Size = new System.Drawing.Size(90, 46);
            this.btnGraphicalRepresentation.TabIndex = 3;
            this.btnGraphicalRepresentation.Text = "Graphical View !";
            this.btnGraphicalRepresentation.UseVisualStyleBackColor = false;
            this.btnGraphicalRepresentation.Click += new System.EventHandler(this.btnGraphicalRepresentation_Click_1);
            // 
            // lblActCurve
            // 
            this.lblActCurve.AutoSize = true;
            this.lblActCurve.BackColor = System.Drawing.Color.White;
            this.lblActCurve.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActCurve.ForeColor = System.Drawing.Color.Blue;
            this.lblActCurve.Location = new System.Drawing.Point(932, 56);
            this.lblActCurve.Name = "lblActCurve";
            this.lblActCurve.Size = new System.Drawing.Size(82, 16);
            this.lblActCurve.TabIndex = 4;
            this.lblActCurve.Text = "ACT CURVE";
            // 
            // pcActCurve
            // 
            this.pcActCurve.BackColor = System.Drawing.Color.Blue;
            this.pcActCurve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcActCurve.Location = new System.Drawing.Point(910, 56);
            this.pcActCurve.Name = "pcActCurve";
            this.pcActCurve.Size = new System.Drawing.Size(16, 16);
            this.pcActCurve.TabIndex = 6;
            this.pcActCurve.TabStop = false;
            // 
            // lblFormula
            // 
            this.lblFormula.AutoSize = true;
            this.lblFormula.BackColor = System.Drawing.Color.White;
            this.lblFormula.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormula.Location = new System.Drawing.Point(906, 78);
            this.lblFormula.Name = "lblFormula";
            this.lblFormula.Size = new System.Drawing.Size(141, 19);
            this.lblFormula.TabIndex = 7;
            this.lblFormula.Text = "Formula: y=mx+c";
            // 
            // ParaGraphofVelocity_Strength
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 721);
            this.Controls.Add(this.lblFormula);
            this.Controls.Add(this.pcActCurve);
            this.Controls.Add(this.lblActCurve);
            this.Controls.Add(this.btnGraphicalRepresentation);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.gridviewVelocityStrength);
            this.Name = "ParaGraphofVelocity_Strength";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.ParaGraphofVelocity_Strength_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridviewVelocityStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcActCurve)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridviewVelocityStrength;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnGraphicalRepresentation;
        private System.Windows.Forms.Label lblActCurve;
        private System.Windows.Forms.PictureBox pcActCurve;
        private System.Windows.Forms.Label lblFormula;
    }
}