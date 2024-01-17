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
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace UPV_Machine
{
    public partial class Form1 : Form
    {
        int initialWidth;
        bool isHidden;
        private Dictionary<int, List<string>> dataDictionary = new Dictionary<int, List<string>>();

        SerialPort mySerialPort = new SerialPort();
        //private DataGridView dataGridView1 = new DataGridView(); // Create a new instance

        private Timer timer;
       // private int batteryPercentage = 50;
        private Timer btryTimer;

        private SqlConnection sqlConnection;


        private DataTable dataTable;
        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();

            // Setup SerialPort and subscribe to DataReceived event

            // Load data into the DataGridView
            //LoadDataIntoGridView();
            //InitializeDataGridView();

            RoundButtonCorners(this);

            initialWidth = panel2.Width;
            isHidden = false;

            //isHiddenPanel2 = true;
            //isHiddenPanel3 = true;
            //isHiddenPanel4 = true;
            //isHiddenPanel5 = true;

            btnMemory.Click += btnMemory_Click;



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
            //mySerialPort.StopBits = StopBits.One;
            //mySerialPort.Handshake = Handshake.None;

            Load += Form1_Load;

            btryTimer = new Timer();
            btryTimer.Interval = 1000; // Set the interval to 1 second
            //btryTimer.Tick += BatteryTImer_Tick;
            btryTimer.Start();
            UpdateBatteryStatus();
        }

        // Global variables
        //private SerialPort serialPort;
       // private SqlConnection sqlConnection;
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;

        // Initialize the database and tables
        private void InitializeDatabase()
        {
            sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB; Database=SerialConnection; Integrated Security=True;");
            try
            {
                sqlConnection.Open();

                // Drop existing table if it exists
                string dropTableQuery = "IF OBJECT_ID('SerialData', 'U') IS NOT NULL DROP TABLE SerialData";
                using (var cmdDrop = new SqlCommand(dropTableQuery, sqlConnection))
                {
                    cmdDrop.ExecuteNonQuery();
                }

                // Create new table
                string createTableQuery = "CREATE TABLE SerialData (ID INT PRIMARY KEY, Data VARCHAR(50))";
                using (var cmdCreate = new SqlCommand(createTableQuery, sqlConnection))
                {
                    cmdCreate.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing database: " + ex.Message);
            }
        }

        // Method to handle serial port data reception
        //private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    string[] ports = SerialPort.GetPortNames();
        //    // Read data from the serial port
        //    mySerialPort.Close();
        //    mySerialPort.PortName = ports[0]; // Use the first available port
        //    mySerialPort.Open();

        //    MessageBox.Show(ports[0], "Port is open");

        //    string receivedData = mySerialPort.ReadLine();
        //    MessageBox.Show(receivedData);

        //    // Store data in the database
        //    string insertDataQuery = "INSERT INTO SerialData (Data) VALUES (@data)";
        //    using (var cmd = new SqlCommand(insertDataQuery, sqlConnection))
        //    {
        //        cmd.Parameters.AddWithValue("@data", receivedData);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        // Method to load data from database into DataGridView
        //private void LoadDataIntoGridView()
        //{
        //    string selectQuery = "SELECT * FROM SerialData";
        //    dataAdapter = new SqlDataAdapter(selectQuery, sqlConnection);
        //    dataSet = new DataSet();
        //    dataAdapter.Fill(dataSet, "SerialData");
        //    dataGridView2.DataSource = dataSet.Tables["SerialData"];
        //}

        // Event handler for when the form loads
        private void Form_Load(object sender, EventArgs e)
        {
            InitializeDatabase(); // Initialize or reset the database

            // Setup SerialPort and subscribe to DataReceived event
            //mySerialPort = new SerialPort("COM10", 9600);
            //serialPort.DataReceived += SerialPort_DataReceived;

            

           // LoadDataIntoGridView(); // Load data into the DataGridView
        }

        // Event handler for when the form is closed
       

        private void RoundButtonCorners(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is Button)
                {
                    Button button = (Button)ctrl;

                    // Adjust the corner radius as needed
                    int cornerRadius = 10;
                        

                    GraphicsPath path = new GraphicsPath();
                    int width = button.Width;
                    int height = button.Height;

                    path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90); // Top-left corner
                    path.AddArc(width - 2 * cornerRadius, 0, cornerRadius * 2, cornerRadius * 2, 270, 90); // Top-right corner
                    path.AddArc(width - 2 * cornerRadius, height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 0, 90); // Bottom-right corner
                    path.AddArc(0, height - 2 * cornerRadius, cornerRadius * 2, cornerRadius * 2, 90, 90); // Bottom-left corner
                    path.CloseAllFigures();

                    button.Region = new Region(path);
                }

                // Recursively call the method for nested controls
                if (ctrl.HasChildren)
                {
                    RoundButtonCorners(ctrl);
                }
            }
        }
        private void BatteryTImer_Tick(object sender, EventArgs e)
        {
            UpdateBatteryStatus();
        }
        private void UpdateBatteryStatus()
        {
            int batteryLevel = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
            UpdateBatteryDisplay(batteryLevel);
        }
        private void UpdateBatteryDisplay(int batteryLevel)
        {
            pbBattery.Value = batteryLevel;

            if (batteryLevel < 30)
            {
                pbBattery.ForeColor = System.Drawing.Color.Red;
            }
            else if (batteryLevel >= 30 && batteryLevel < 70)
            {
                pbBattery.ForeColor = System.Drawing.Color.Orange;
            }
            else
            {
                pbBattery.ForeColor = System.Drawing.Color.Green;
            }

            lblBatteryPer.Text = $"{batteryLevel}%";
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            timer1.Start();
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

        //private void button16_Click(object sender, EventArgs e)
        //{
        //    panel4.BringToFront();
        //    panel3.Height = 28;
        //    panel5.Height = 28;

        //    if (panel4.Height == 85)
        //    {
        //        panel4.Height = 28;
        //       // isHiddenPanel4 = false;

        //    }
        //    else
        //    {
        //        panel4.Height = 85;
        //       // isHiddenPanel4 = true;
        //    }
        //}

        //private void btnStrAnalysis_Click(object sender, EventArgs e)
        //{

        //}

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{
        //}



        public string value1 = "";
        public string value2 = "";
        public string value3 = "";
        public string value4 = "";
        public string value5 = "";
        
        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Width -= 241;
            //timer1.Stop();
            isHidden = true;
            //isHiddenPanel2 = true;
            panel3.BringToFront();
            panel4.BringToFront();
            panel5.BringToFront();
          //  int[] dataValues = null;
            //ReceiveDataViaSerialPort();


        }

        private void ReceiveDataViaSerialPort2()
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                mySerialPort.Close();
                mySerialPort.PortName = ports[0]; // Use the first available port
                mySerialPort.Open(); // Open the serial port

                MessageBox.Show(ports[0], "Port Is Open");
                // Subscribe to the DataReceived event
                // string receivedData = mySerialPort.ReadLine(); // Read data from the serial port
                // Process the received data (parse, split, etc.) and populate the DataGridView

                int bytesToRead = mySerialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                int bytesRead = mySerialPort.Read(buffer, 0, bytesToRead);

                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                string delimiter = "\u001A";
                string[] stringValues = receivedData.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

                //string[] stringValues = receivedData.Split(',');          // split by space
                // string[] stringValues = receivedData.Split('\u001A'); // Split by non-printable ASCII character (ASCII 26)

                //List<double> numericValues = new List<double>();
                //foreach (string value in stringValues)
                //{
                //    double numericValue;
                //    if (double.TryParse(value.Trim(), out numericValue))
                //    {
                //        numericValues.Add(numericValue);
                //    }
                //}


                //string numericValuesMessage = "Extracted Numeric Values:\n";
                //foreach (double numericValue in numericValues)
                //{
                //    numericValuesMessage += numericValue.ToString() + "\n";
                //}
                //MessageBox.Show(numericValuesMessage, "Extracted Numeric Values");

                //MessageBox.Show(stringValues[0]);
                // MessageBox.Show(stringValues[1]);
                //MessageBox.Show(stringValues[2]);
                //MessageBox.Show(stringValues[3]);

                //     dataValues = Array.ConvertAll(stringValues, int.Parse);


                //dataValues = Array.ConvertAll(stringValues, int.Parse);
                //dataGridView2.Rows.Add(dataValues); // Add data to the DataGridView as a new row

                //string value1 = stringValues[0];
                //string value2 = stringValues[1];
                //string value3 = stringValues[2];
                //string value4 = stringValues[3];
                //string value5 = stringValues[4];

                //if (stringValues.Length >= 16) // Ensure there are enough values to assign
                //{
                //    value1 = stringValues[0];
                //    value2 = stringValues[1];
                //    value3 = stringValues[2];
                //    value4 = stringValues[3];
                //    value5 = stringValues[4];
                //}

                Invoke(new Action(() =>
                {
                    if (dataGridView2.ColumnCount == 0) // If no columns are added yet
                    {
                        // Add columns based on the received data
                        for (int i = 0; i < stringValues.Length; i++)
                        {
                            dataGridView2.Columns.Add($"Column{i + 1}", $"Column {i + 1}"); // Add columns dynamically
                        }
                    }
                   // dataGridView2.Rows.Add(stringValues);
                    int columnIndexToSet = 0; // Replace this value with the desired column index

                    if (dataGridView2.Columns.Count > columnIndexToSet && stringValues.Length >= 5)
                    {
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[columnIndexToSet].Value = stringValues[0]; // Set value for ColumnN
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[columnIndexToSet + 1].Value = stringValues[1]; // Set value for ColumnN+1
                        //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[columnIndexToSet + 2].Value = stringValues[2]; // Set value for ColumnN+2
                        //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[columnIndexToSet + 3].Value = stringValues[3]; // Set value for ColumnN+3
                        //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[columnIndexToSet + 4].Value = stringValues[4]; // Set value for ColumnN+4
                    }
                    //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[columnIndexToSet].Value = value1;
                    // dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[columnIndexToSet+1].Value = value2;
                    //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["ColumnN+2"].Value = value3;
                    //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["ColumnN+3"].Value = value4;
                    //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["ColumnN+4"].Value = value5;
                }));
                //Invoke(new Action(() =>
                //{
                //    // Your existing code to handle DataGridView population
                //    if (dataGridView2.ColumnCount == 0)
                //    {
                //        for (int i = 0; i < stringValues.Length; i++)
                //        {
                //            dataGridView2.Columns.Add($"Column{i + 1}", $"Column {i + 1}");
                //        }
                //    }

                //    if (dataGridView2.Columns.Count > 0 && stringValues.Length >= dataGridView2.Columns.Count)
                //    {
                //        dataGridView2.Rows.Add();

                //        for (int i = 0; i < dataGridView2.Columns.Count; i++)
                //        {
                //            dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[i].Value = stringValues[i];
                //        }
                //    }
                //}));
            }
            else
            {
                MessageBox.Show("No COM ports available.");
                Application.Exit(); // Close the application if no COM ports are available
            }
        }
            
        private void ReceiveDataViaSerialPort3()
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                mySerialPort.Close();
                mySerialPort.PortName = ports[0]; // Use the first available port
                mySerialPort.Open(); // Open the serial port

                MessageBox.Show(ports[0], "Port Is Open");

                int bytesToRead = mySerialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                int bytesRead = mySerialPort.Read(buffer, 0, bytesToRead);

                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
               //    string[] stringValues = receivedData.Split('\u001A'); // Split by non-printable ASCII character (ASCII 26)
                string[] stringValues = receivedData.Split(','); // Split by non-printable ASCII character (ASCII 26)

                if (stringValues.Length >= 18) // Ensure there are at least 18 values to assign
                {
                    // Assign values to local variables
                    value1 = stringValues[0];
                    value2 = stringValues[1];
                    value3 = stringValues[2];
                    value4 = stringValues[3];
                    value5 = stringValues[4];
                    // ... assign other values similarly

                    //// Update the DataGridView in the UI thread
                    //Invoke(new Action(() =>
                    //{
                    //    // Clear existing rows and columns before adding new data
                    //    dataGridView2.Rows.Clear();
                    //    dataGridView2.Columns.Clear();

                    //    // Add columns based on the received data
                    //    for (int i = 0; i < stringValues.Length; i++)
                    //    {
                    //        dataGridView2.Columns.Add($"Column{i + 1}", $"Column {i + 1}"); // Add columns dynamically
                    //    }

                    //    // Add a new row and populate individual cells with stringValues
                    //    dataGridView2.Rows.Add();
                    //    for (int i = 0; i < stringValues.Length; i++)
                    //    {
                    //        dataGridView2.Rows[0].Cells[i].Value = stringValues[i];
                    //    }
                    //}));

                    //// Display received values in a message box for debugging purposes
                    //MessageBox.Show($"Received values: {string.Join(", ", stringValues)}");
                }

                // Clear existing rows and columns before adding new data
                //Invoke(new Action(() =>
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                // Add columns based on the received data
                for (int i = 0; i < stringValues.Length; i++)
                {
                    dataGridView2.Columns.Add($"Column{i + 1}", $"Column {i + 1}"); // Add columns dynamically
                }

                // Add a new row and populate individual cells with stringValues
                dataGridView2.Rows.Add();
                for (int i = 0; i < stringValues.Length; i++)
                {
                    dataGridView2.Rows[0].Cells[i].Value = stringValues[i];
                }
                //}));



                //// Display received values in a message box for debugging purposes
                MessageBox.Show($"Received values: {string.Join(", ", stringValues)}");

            }
            else
            {
                MessageBox.Show("No COM ports available.");
                Application.Exit(); // Close the application if no COM ports are available
            }
        }

        private void ReceiveDataViaSerialPort5()
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                mySerialPort.Close();
                mySerialPort.PortName = ports[0]; // Use the first available port
                mySerialPort.Open(); // Open the serial port

                MessageBox.Show(ports[0], "Port Is Open");

                int bytesToRead = mySerialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                int bytesRead = mySerialPort.Read(buffer, 0, bytesToRead);

                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                string[] stringValues = receivedData.Split(''); // Split by ASCII character 

                // Clear existing rows and columns before adding new data
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                // Assuming the received data contains multiple sets of values separated by ASCII 
                foreach (string dataSet in stringValues)
                {
                    //char delimiter = Convert.ToChar("\u001A");
                    //string[] stringValues = receivedData.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);


                    string[] dataParts = dataSet.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    if (dataParts.Length > 0)
                    {
                        int serialNumber;
                        if (int.TryParse(dataParts[0], out serialNumber))
                        {
                            // Add columns if they don't exist
                            if (dataGridView2.Columns.Count == 0)
                            {
                                dataGridView2.Columns.Add("SerialNumber", "Serial Number");
                                for (int i = 1; i < dataParts.Length; i++)
                                {
                                    dataGridView2.Columns.Add($"value{i}", $"Value {i}");
                                }
                            }

                            // Ensure enough rows exist in the DataGridView to accommodate the serialNumber
                            while (dataGridView2.Rows.Count < serialNumber)
                            {
                                dataGridView2.Rows.Add();
                            }

                            DataGridViewRow row = null;

                            // Check if the row for serialNumber exists within the range
                            if (serialNumber > 0 && serialNumber <= dataGridView2.Rows.Count)
                            {
                                row = dataGridView2.Rows[serialNumber - 1];
                            }
                            else
                            {
                                // Add a new row if the serialNumber is out of range
                                row = dataGridView2.Rows[dataGridView2.Rows.Add()];
                            }

                            // Update cell values
                            row.Cells[0].Value = $"value={dataParts[0]}";
                            for (int i = 2; i < dataParts.Length && i < dataGridView2.Columns.Count; i++)
                            {
                                row.Cells[i].Value = $"value{i}={dataParts[i]}";
                            }
                        }
                    }
                }
                MessageBox.Show($"Received values: {string.Join(", ", stringValues)}");
               // string startofString = stringValues[0];
               //// string sResponse = stringValues[1];
               // string sAcknowledege = stringValues[5];
               // //string sEndofString = stringValues[3];

               // MessageBox.Show("startofString : ", startofString);
               // //  MessageBox.Show("sResponse : ", sResponse);
               // MessageBox.Show("sAcknowledege : ", sAcknowledege);
               //// MessageBox.Show("sEndofString : ", sEndofString);

            }
            else
            {
                MessageBox.Show("No COM ports available.");
                Application.Exit(); // Close the application if no COM ports are available
            }
        }

        private void ReceiveDataViaSerialPort4()
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length > 0)
            {
                mySerialPort.Close();
                mySerialPort.PortName = ports[0]; // Use the first available port
                mySerialPort.Open(); // Open the serial port

                MessageBox.Show(ports[0], "Port Is Open");

                int bytesToRead = mySerialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                int bytesRead = mySerialPort.Read(buffer, 0, bytesToRead);

                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                string[] stringValues = receivedData.Split(''); // Split by ASCII character 

                // Assuming the received data contains multiple sets of values separated by ASCII 
                foreach (string dataSet in stringValues)
                {
                    string[] dataParts = dataSet.Split(',');

                    if (dataParts.Length > 0)
                    {
                        int serialNumber;
                        if (int.TryParse(dataParts[0], out serialNumber))
                        {
                            List<string> valuesForSerialNumber = new List<string>();

                            // Start from index 1 as index 0 is already used for serialNumber
                            for (int i = 1; i < dataParts.Length; i++)
                            {
                                valuesForSerialNumber.Add(dataParts[i].Trim());
                            }

                            if (!dataDictionary.ContainsKey(serialNumber))
                            {
                                dataDictionary.Add(serialNumber, valuesForSerialNumber);
                            }
                            else
                            {
                                dataDictionary[serialNumber] = valuesForSerialNumber;
                            }
                        }
                    }
                }

                // After processing the data, update the DataGridView
                MessageBox.Show($"Received values: {string.Join(", ", stringValues)}");

                UpdateDataGridView();
            }
            else
            {
                MessageBox.Show("No COM ports available.");
                Application.Exit();
            }
        }

        private void UpdateDataGridView()
        {
            // Clear existing rows and columns before adding new data
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // Add columns based on the maximum number of values in a set
            int maxValuesCount = dataDictionary.Values.Select(v => v.Count).DefaultIfEmpty(0).Max();
            dataGridView2.Columns.Add("SerialNumber", "Serial Number");
            for (int i = 1; i <= maxValuesCount; i++)
            {
                dataGridView2.Columns.Add($"Value{i}", $"Value {i}");
            }

            // Add rows and fill in the data
            foreach (var entry in dataDictionary)
            {
                int serialNumber = entry.Key;
                List<string> values = entry.Value;

                int rowIndex = dataGridView2.Rows.Add();
                dataGridView2.Rows[rowIndex].Cells[0].Value = serialNumber;

                for (int i = 0; i < values.Count; i++)
                {
                    dataGridView2.Rows[rowIndex].Cells[i + 1].Value = values[i];
                }
            }
        }


        private void SendDataViaSerialPort(byte[] bytesToSend)
        {
            if (mySerialPort.IsOpen) // Check if the serial port is open
            {
                try
                {
                    // Convert the string data to bytes
                   // byte[] bytesToSend = Encoding.ASCII.GetBytes(data);
                    
                    // Send the data through the serial port
                    mySerialPort.Write(bytesToSend, 0, bytesToSend.Length);
                }
                catch (Exception ex)
                {
                    // Handle exceptions, e.g., display an error message or log the exception
                    MessageBox.Show($"Error sending data: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Serial port is not open. Cannot send data.");
            }
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            int[] hexValue = new int[] { 0x11, 0x0A, 0x13 };

            // string[] num = new string[] { "0*11", "0*0A", "0*13" };

            // Concatenate the strings in the array
            byte[] bytesToSend = hexValue.Select(x => (byte)x).ToArray();

            SendDataViaSerialPort(bytesToSend);
            //ReceiveDataViaSerialPort();
        }

        private void btnParaSetting_Click(object sender, EventArgs e)
        {
            //btnParaSetting.FlatStyle = FlatStyle.Flat;
            //btnParaSetting.FlatAppearance.BorderSize = 0;
            ParaSetting paraSetting = new ParaSetting();

            paraSetting.Show();
            //paraSetting.ShowDialog();
            


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
            //panel4.Height = 70;

            if (panel4.Height == 0)
            {
                pictureBox2.Image = Properties.Resources.leftArrow;
                panel4.Height = 70;
            }
            //else
            //{
            //    pictureBox2.Image = Properties.Resources.RightArr;
            //    panel4.Height = 0;
            //}
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

        //public void SerialAction()
        //{
        //    if (ULR2_Req == true)
        //    {
        //        mySerialPort.Write(tx_buf1, 0, l);
        //    }
        //    else if (ULR1_Req == true)
        //    {
        //        mySerialPort.Write(tx_buf, 0, 3);
        //    }
        //    else if (ULR0_Req == true)
        //    {
        //        mySerialPort.Write(tx_buf, 0, 4);
        //    }
        //    else if (ULR3_Req == true || ULR4_Req == true)
        //    {
        //        mySerialPort.Write(tx_buf, 0, 4);
        //    }

        //    rx_buf = mySerialPort.ReadExisting();
        //    rxlength = rx_buf.Length;
        //    try
        //    {
        //        if (rx_buf[rxlength - 1] == DC3)
        //        {
        //            if (rx_buf[1] == ULA_0)
        //            {
        //                stopcntr = 0;
        //                rx_split = rx_buf.Split(PC_REC);
        //                SysStatus = rx_split[1];
        //                ActTime = rx_split[2]; // Added for CD
        //                lblMainActLVel.Text = rx_split[3];
        //                lblMainActCD.Text = rx_split[4];
        //                inc1 = int.Parse(rx_split[5]);
        //                inc2 = int.Parse(rx_split[6]);
        //                lblmaingain.Text = rx_split[7];

        //                if (rx_split[14] == "0")
        //                {
        //                    if (rx_split[8] == "1")
        //                    {
        //                        ULR1_Req = true;
        //                        ULR0_Req = false;
        //                    }
        //                    else
        //                    {
        //                        ULR1_Req = false;
        //                        ULR0_Req = true;
        //                    }
        //                }
        //                if (rx_split[8] == "0")
        //                {
        //                    if (rx_split[14] == "1")
        //                    {
        //                        ULR4_Req = true;
        //                        ULR0_Req = false;
        //                    }
        //                    else
        //                    {
        //                        ULR4_Req = false;
        //                        ULR0_Req = true;
        //                    }
        //                }

        //                lblMainActSVel.Text = rx_split[9];
        //                lblMainActPR.Text = rx_split[10];
        //                lblMainActYM.Text = rx_split[11];
        //                lblMainActSM.Text = rx_split[12];
        //                lblMainActBM.Text = rx_split[13];
        //            }
        //            else if (rx_buf[1] == ULA_1)
        //            {
        //                stopcntr = 0;
        //                ULR1_Req = false;
        //                ULR0_Req = true;
        //                config_split = rx_buf.Split(PC_REC);
        //                ConfigAction();
        //            }
        //            else if (rx_buf[1] == ULA_2)
        //            {
        //                stopcntr = 0;
        //                ULR0_Req = true;
        //                ULR1_Req = false;
        //                ULR2_Req = false;
        //            }
        //            else if (rx_buf[1] == ULA_3)
        //            {
        //                if (rx_buf[3] == (char)0x1)
        //                {
        //                    timebuf = rx_buf.Split((char)0x14);
        //                    txpac = DISTNORMAL;
        //                    timelen = timebuf.Length;
        //                }
        //                else if (rx_buf[3] == (char)0x2)
        //                {
        //                    Distbuf = rx_buf.Split((char)0x14);
        //                    txpac = VELONORMAL;
        //                }
        //                else if (rx_buf[3] == (char)0x3)
        //                {
        //                    Velobuf = rx_buf.Split((char)0x14);
        //                    txpac = SSNORMAL;
        //                }
        //                else if (rx_buf[3] == (char)0x4)
        //                {
        //                    SSbuf = rx_buf.Split((char)0x14);
        //                    ULR3_Req = false;
        //                    ULR0_Req = true;
        //                    stopcntr = 0;
        //                }
        //            }
        //            else if (rx_buf[1] == ULA_4)
        //            {
        //                stopcntr = 0;
        //                ULR4_Req = false;
        //                ULR0_Req = true;
        //                String_split = rx_buf.Split(PC_REC);
        //                ConfigAction_ULR4();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //    }
        //}

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

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            int panelHeight = panel2.Height;
            int totalScrollableHeight = vScrollBar1.Maximum - vScrollBar1.LargeChange + 1;
            int scrollValue = vScrollBar1.Value;

            int newPosition = (int)(((float)scrollValue / totalScrollableHeight) * (this.ClientSize.Height - panelHeight));
            panel2.Location = new Point(panel2.Location.X, newPosition);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //if (btnMode_Click =)
            //{

            //}
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {   }

        private void button15_Click(object sender, EventArgs e)
        {
            ModeMem memory = new ModeMem();
            memory.Show();
        }
    }
}