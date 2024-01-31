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
    public partial class DateTimeSetting : Form
    {
        private string enteredPassword = "";
        private const string PlaceholderText = "Enter your password";
        //private string enteredPass = "";

        public DateTimeSetting()
        {
            InitializeComponent();

            txtPassword.Text = PlaceholderText;
            txtPassword.ForeColor = SystemColors.GrayText;

            // Attach event handlers
            txtPassword.GotFocus += PasswordTextBox_GotFocus;
            txtPassword.LostFocus += PasswordTextBox_LostFocus;

            btn0.Click += NumButton_Click;
            btn1.Click += NumButton_Click;
            btn2.Click += NumButton_Click;
            btn3.Click += NumButton_Click;
            btn4.Click += NumButton_Click;
            btn5.Click += NumButton_Click;
            btn6.Click += NumButton_Click;
            btn7.Click += NumButton_Click;
            btn8.Click += NumButton_Click;
            btn9.Click += NumButton_Click;
        }
        private void PasswordTextBox_GotFocus(object sender, EventArgs e)
        {
            // Clear placeholder text and change text color to black
            if (txtPassword.Text == PlaceholderText)
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = SystemColors.WindowText;
            }
        }

        private void PasswordTextBox_LostFocus(object sender, EventArgs e)
        {
            // Restore placeholder text if no password entered
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = PlaceholderText;
                txtPassword.ForeColor = SystemColors.GrayText;
            }
        }

        private void NumButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            enteredPassword += button.Text;
            UpdatePasswordTextBox();
        }

        private void UpdatePasswordTextBox()
        {
            // Display asterisks (*) for each entered character
            txtPassword.Text = new string('*', enteredPassword.Length);
        }

        private void ValidatePassword()
        {
            // Check the entered password (replace "1234" with your actual password)
            if (enteredPassword == "1234")
            {
                MessageBox.Show("Password is correct. Access granted!");
                // You can perform additional actions here
                Close(); // Close the form on successful validation
            }
            else
            {
                MessageBox.Show("Incorrect password. Access denied!");
                enteredPassword = "";
                UpdatePasswordTextBox();
            }
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {
            Close(); // Close the form on escape

        }

        private void btnEnt_Click(object sender, EventArgs e)
        {
            ValidatePassword();
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            enteredPassword = "";
            UpdatePasswordTextBox();
        }

        private void btnC_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
