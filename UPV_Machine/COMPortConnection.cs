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
    
    public partial class COMPortConnection : Form
    {
        public string cmbPortName;

        public COMPortConnection()
        {
            InitializeComponent();
        }

        private void COMPortConnection_Load(object sender, EventArgs e)
        {
            CmboBoxSelectPort.Text = cmbPortName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            cmbPortName = CmboBoxSelectPort.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            COMPortConnection portconnection = new COMPortConnection();
            portconnection.Close();
        }
    }
}
