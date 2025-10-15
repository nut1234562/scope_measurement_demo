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

namespace scope_measurement_demo
{
    public partial class Inject_form : Form
    {
        private List<CheckBox> checkOrder = new List<CheckBox>();
        SerialPort serial2 = new SerialPort();
        public Inject_form()
        {
            InitializeComponent();

            DinjectCK.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            L1L2injectCK.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            LinjectCK.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            RinjectCK.CheckedChanged += new EventHandler(RinjectCK_CheckedChanged);
            IAinjectCK.CheckedChanged += new EventHandler(checkBox_CheckedChanged);

            DUpDown.SelectedItem = DUpDown.Items[0];
            LUpDown.SelectedItem = LUpDown.Items[0];
            RUpDown.SelectedItem = RUpDown.Items[0];
            L12UpDown.SelectedItem = L12UpDown.Items[0];
            IAUpDown.SelectedItem = IAUpDown.Items[0];
            

        }


        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Checked)
            {
                checkOrder.Add(cb);
                InjectShowtb.Text = string.Join(" -> ", checkOrder.Select(c => c.Text));
            }
            else
            {
                checkOrder.Remove(cb);
                InjectShowtb.Text = string.Join(" -> ", checkOrder.Select(c => c.Text));
            }
        }


        private void Inject_Click(object sender, EventArgs e)
        {
            List<string> injectList = new List<string>();
            foreach (var cb in checkOrder)
            {
                string check = cb.ToString();
                if (check == "D")
                {
                    int n = DUpDown.SelectedIndex;
                    for (int i = 0; i < n; i++)
                        injectList.Add("No.1\r\n\r\nCircle(Multi) 4/4\r\n-1-\r\n  LS\r\n  Xc 0.401\r\n  Yc 2.790\r\n  D 4.499\r\n  R 2.249");
                }
                else if (check == "L")
                {
                    int n = LUpDown.SelectedIndex;
                    for (int i = 0; i < n; i++)
                        injectList.Add("No.1\r\nDistance(Point-Point) 2/2\r\n-1-\r\n  L 1.501\r\n  dx 1.502\r\n  dY 0.001\r\n");
                }
                else if (check == "R")
                {
                    int n = RUpDown.SelectedIndex;
                    for (int i = 0; i < n; i++)
                        injectList.Add("No.1\r\n\r\nCircle 3/3\r\n-1-\r\n  Xc -3.546\r\n  Yc 3.331\r\n  D 0.579\r\n  R 0.289\r\n");
                }
                else if (check == "L1L2")
                {
                    int n = L12UpDown.SelectedIndex;
                    for (int i = 0; i < n; i++)
                        injectList.Add("No.1\r\nRectangle 5/5\r\n-1-\r\n  X 22.506\r\n  Y 28.186\r\n  L1 9.013\r\n  L2 9.014");
                }
                else if (check == "IA")
                {
                    int n = IAUpDown.SelectedIndex;
                    for (int i = 0; i < n; i++)
                        injectList.Add("No.2\r\nIntersection(Line-Line) 2/2\r\n-1-\r\n  X 5.894\r\n  Y -15.165\r\n  IA 22:05:43");
                }
            }
        }

        private void RinjectCK_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.Checked)
            {
                // Add to list if checked
                checkOrder.Add(cb);
            }
            else
            {
                // Remove if unchecked
                checkOrder.Remove(cb);
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void Serial2Connect_Click(object sender, EventArgs e)
        {
            try
            {
                serial2 = new SerialPort(); // assign to class-level variable
                serial2.PortName = "COM11";
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
}
