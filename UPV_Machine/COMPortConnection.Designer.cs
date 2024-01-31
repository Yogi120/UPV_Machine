
namespace UPV_Machine
{
    partial class COMPortConnection
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
            this.CmboBoxSelectPort = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CmboBoxSelectPort
            // 
            this.CmboBoxSelectPort.BackColor = System.Drawing.SystemColors.Highlight;
            this.CmboBoxSelectPort.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmboBoxSelectPort.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmboBoxSelectPort.FormattingEnabled = true;
            this.CmboBoxSelectPort.ItemHeight = 22;
            this.CmboBoxSelectPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.CmboBoxSelectPort.Location = new System.Drawing.Point(126, 27);
            this.CmboBoxSelectPort.Name = "CmboBoxSelectPort";
            this.CmboBoxSelectPort.Size = new System.Drawing.Size(244, 30);
            this.CmboBoxSelectPort.TabIndex = 2;
            this.CmboBoxSelectPort.Text = "Select Port";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(126, 114);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(104, 32);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(266, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // COMPortConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(524, 201);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.CmboBoxSelectPort);
            this.Name = "COMPortConnection";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.COMPortConnection_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CmboBoxSelectPort;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}