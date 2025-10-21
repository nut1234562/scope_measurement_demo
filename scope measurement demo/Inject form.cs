using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace scope_measurement_demo
{
    public partial class Inject_form : Form
    {
        private List<CheckBox> checkOrder = new List<CheckBox>();
        SerialPort serial2 = new SerialPort();
        public Inject_form()
        {
            InitializeComponent();
            this.FormClosing += Inject_form_FormClosing;

            foreach (var port in SerialPort.GetPortNames())
            {
                Serial2cb.Items.Add(port); // เพิ่มชื่อ port ทีละตัว
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Checked)
            {
                checkOrder.Add(cb);
                InjectOrdertb.Text = string.Join(" -> ", checkOrder.Select(c => c.Text));
            }
            else
            {
                checkOrder.Remove(cb);
                InjectOrdertb.Text = string.Join(" -> ", checkOrder.Select(c => c.Text));
            }
        }


        private void Inject_Click(object sender, EventArgs e)
        {
            bool addvalue = false;
            InjectShowtb.Clear();
            List<string> injectList = new List<string>();
            int repeat = 0;
            if (DUpdown.Value != 0)
            {
                repeat = DUpdown.Value > 0 ? (int)DUpdown.Value : 0;
                addvalue = true;
            }
            if (LUpdown.Value != 0)
            {
                repeat = LUpdown.Value > 0 ? (int)LUpdown.Value : 0;
                addvalue = true;
            }

            if (RUpdown.Value != 0)
            {
                repeat = RUpdown.Value > 0 ? (int)RUpdown.Value : 0;
                addvalue = true;
            }

            if (IAUpdown.Value != 0)
            {
                repeat = L1L2Updown.Value > 0 ? (int)IAUpdown.Value : 0;
                addvalue = true;
            }

            if (L1L2Updown.Value != 0)
            {
                repeat = IAUpdown.Value > 0 ? (int)L1L2Updown.Value : 0;
                addvalue = true;
            }

            while (addvalue)
            {
                for (int i = 0; i < repeat; i++)
                {
                    if (DUpdown.Value > 0)
                    {
                        injectList.Add("No.1\r\n\r\nCircle(Multi) 4/4\r\n-1-\r\n  LS\r\n  Xc 0.401\r\n  Yc 2.790\r\n  D 4.499\r\n  R 2.249");
                    }
                    if (LUpdown.Value > 0)
                    {
                        injectList.Add("No.1\r\nDistance(Point‑Point) 2/2\r\n-1-\r\n  L 1.501\r\n  dx 1.502\r\n  dY 0.001\r\n");
                    }
                    if (RUpdown.Value > 0)
                    {
                        injectList.Add("No.1\r\n\r\nCircle 3/3\r\n-1-\r\n  Xc -3.546\r\n  Yc 3.331\r\n  D 0.579\r\n  R 0.289\r\n");
                    }
                    if (IAUpdown.Value > 0)
                    {
                        injectList.Add("No.1\r\nRectangle 5/5\r\n-1-\r\n  X 22.506\r\n  Y 28.186\r\n  L1 9.013\r\n  L2 9.014");
                    }
                    if (L1L2Updown.Value > 0)
                    {
                        injectList.Add("No.2\r\nIntersection(Line‑Line) 2/2\r\n-1-\r\n  X 5.894\r\n  Y -15.165\r\n  IA 22:05:43");
                    }
                }
                addvalue = false;
                break;
            }


            if (injectList.Count == 0)
            {
                MessageBox.Show("Select a measurement and amount before injecting.");
                return;
            }

            string payload = string.Join("\r\n", injectList);
            InjectShowtb.AppendText(payload + Environment.NewLine);

            if (serial2 != null && serial2.IsOpen)
            {
                serial2.Write(payload);
            }
            else
            {
                MessageBox.Show("Serial port is not connected. Click Connect before injecting.");
            }
        }

        //private void RinjectCK_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox cb = sender as CheckBox;

        //    if (cb.Checked)
        //    {
        //        // Add to list if checked
        //        checkOrder.Add(cb);
        //    }
        //    else
        //    {
        //        // Remove if unchecked
        //        checkOrder.Remove(cb);
        //    }
        //}

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void Serial2Connect_Click(object sender, EventArgs e)
        {
            if (!serial2.IsOpen)
            {
                if (Serial2cb.SelectedItem == null)
                {
                    MessageBox.Show("Please select a serial port.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    try
                    {
                        serial2 = new SerialPort(); // assign to class-level variable
                        serial2.PortName = Serial2cb.SelectedItem.ToString();
                        serial2.BaudRate = 9600;
                        serial2.DataBits = 8;
                        serial2.StopBits = StopBits.One;
                        serial2.Parity = Parity.None;
                        serial2.DataReceived += SerialPort_DataReceived; // attach event
                        serial2.Open(); // open COM port
                        MessageBox.Show("Serial Port connected successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error connecting to Serial Port: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Serial Port is already open.");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serial2.ReadExisting();
                // Process the received data as needed
                this.Invoke(new Action(() =>
                {
                    // Update UI elements here if necessary
                    // For example, append data to a TextBox
                    // textBox.AppendText(data);
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show($"Error reading from Serial Port: {ex.Message}");
                }));
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            if (serial2.IsOpen)
            {
                serial2.Close();
                MessageBox.Show("Serial Port disconnected!");
            }
            else MessageBox.Show("Serial Port is not open.");
        }

        private void Clearbt2_Click(object sender, EventArgs e)
        {
            InjectShowtb.Clear();
        }

        private void Inject_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serial2.IsOpen)
            {
                serial2.Close();
            }
            //Injectwindowckb.Checked = false;
        }
    }
}
