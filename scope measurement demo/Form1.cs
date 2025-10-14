using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace scope_measurement_demo
{


    public partial class Form1 : Form
    {
        List<double> lList = new List<double> { };
        List<double> rList = new List<double> { };
        List<double> dxList = new List<double> { };
        List<double> dyList = new List<double> { };
        private List<string> serialLineBuffer = new List<string>(128);
        List<Measurement> measurements = new List<Measurement>();

        double dx1 = 0, dx2 = 0, dx3 = 0, dy1 = 0, dy2 = 0, dy3 = 3, d = 0, r = 0, l1 = 0, l2 = 0, l3 = 0, l4 = 0, xc = 0, yc = 0, x = 0, y = 0, r1 = 0, r2 = 0, r3 = 0, r4 = 0; // ตัวเล็กใช้คำนวณและแสดงช่อง output
        double[] variables;
        double DX1 = 0, DX2 = 0, DX3 = 0, DY3 = 0, DY1 = 0, DY2 = 0, D = 0, R = 0, L1 = 0, L2 = 0, L3 = 0, L4 = 0, XC = 0, YC = 0, X = 0, Y = 0, R1 = 0, R2 = 0, R3 = 0, R4 = 0;  // ตัวใหญ่ใช้แสดงผลบน UI
        double l1Value = 0, l2Value = 0;
        int currentline = 0;
        string incoming;
        static readonly Stopwatch sw = Stopwatch.StartNew();
        static long last = 0;
        const long period = 1000;
        private readonly StringBuilder _serialBuffer = new StringBuilder();
        private const string MessageTerminator = "\n\n";
        private System.Windows.Forms.Timer serialTimeoutTimer;

        SerialPort serialPort;
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
            variables = new double[] { DX1, DY1, D, R, L1, L2, L3, L4, XC, YC, X, Y };

            serialPort = new SerialPort();
            foreach (var port in SerialPort.GetPortNames())
            {
                cbport.Items.Add(port); // เพิ่มชื่อ port ทีละตัว
            }

            // In your constructor or Form1_Load
            serialTimeoutTimer = new System.Windows.Forms.Timer();
            serialTimeoutTimer.Interval = 6000; // 2 seconds
            serialTimeoutTimer.Tick += SerialTimeoutTimer_Tick;

            cbport.SelectedIndex = 0;
            cbbaudrate.SelectedIndex = 0; // ตั้งค่า baudrate เป็นค่าเริ่มต้น
            cbdatabit.SelectedIndex = 0; // ตั้งค่า data bit เป็นค่าเริ่มต้น
            cbstopbit.SelectedIndex = 0; // ตั้งค่า stop bit เป็นค่าเริ่มต้น
            cbparitybit.SelectedIndex = 0; // ตั้งค่า parity เป็นค่าเริ่มต้น
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Modelcb_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Modelcb.SelectedIndex == 0)//1st gear
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("Base Tangent Length");
                Itemcb.Items.Add("Outer Diameter");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 1)//VHB30
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("Width Of Stopper");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 2)//Shaft
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("Radius 1 - 4");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 3)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("OD Lead Screw 1");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 4)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("Stopper Angle IA1 - IA3");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 5)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("OD3");
                Itemcb.Items.Add("Radius Of Stopper");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 6)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("Outer Diameter (OD2)");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 7)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("Outer Diameter (OD1) (Gate side and Ejector side)");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 8)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("Concentric (Gate Side)");
                Itemcb.Items.Add("Concentric (Ejector Side)");
                Itemcb.Items.Add("Thickness (Gate Side)");
                Itemcb.Items.Add("Thickness (Ejector Side)");
                Itemcb.Items.Add("OD 1 & 2 (Gate)");
                Itemcb.Items.Add("ID 1 & 2 (Gate)");
                Itemcb.Items.Add("Concentric 1 & 2 (Gate Side)");
                Itemcb.Items.Add("OD 1 & 2 (Ejector Side)");
                Itemcb.Items.Add("ID 1 & 2 (Ejector Side)");
                Itemcb.Items.Add("Concentric 1 2 (Ejector Side)");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 9)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("OD and ID (Gate side and Ejector side)");
                Itemcb.SelectedIndex = 0;

            }
            else if (Modelcb.SelectedIndex == 10)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("OD1");
                Itemcb.SelectedIndex = 0;
            }
            else if (Modelcb.SelectedIndex == 11)
            {
                Itemcb.Items.Clear();
                Itemcb.Items.Add("OD, ID (Gate Side)");
                Itemcb.Items.Add("OD, ID (Ejector Side)");
                Itemcb.SelectedIndex = 0;

            }

        }
        // Replace the Refresh_Click method with the following code
        private void Refresh_Click(object sender, EventArgs e)
        {
            cbport.Items.Clear();
            foreach (var port in SerialPort.GetPortNames())
            {
                cbport.Items.Add(port); // เพิ่มชื่อ port ทีละตัว
            }
            cbbaudrate.SelectedIndex = 0; // ตั้งค่า baudrate เป็นค่าเริ่มต้น
            cbdatabit.SelectedIndex = 0; // ตั้งค่า data bit เป็นค่าเริ่มต้น
            cbstopbit.SelectedIndex = 0; // ตั้งค่า stop bit เป็นค่าเริ่มต้น
            cbparitybit.SelectedIndex = 0; // ตั้งค่า parity เป็นค่าเริ่มต้น

            MessageBox.Show("Refresh Comport สำเร็จ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int GetExpectedLineCount()
        {
            int md = Modelcb.SelectedIndex;
            int it = Itemcb.SelectedIndex;
            if (md == 0 || md == 3)//เลือกว่าปริ้นกี่บรรทัด โดยเลือกตาม model และ item
                return 9;

            else if (md == 1)//ถ้าปริ้น 7 บรรทัด
                return 20;

            else if (md == 4 & it == 0)
                return 5;

            else if (md == 2)
                return 35;

            else if (md == 5 && it == 0 || md == 6 || md == 10)

                return 9;

            else if (md == 5 && it == 1)
                return 13;

            else if (md == 7 || md == 8 && it == 8)
                return 19;

            else if (md == 8 && it == 0 || md == 8 && it == 1)
                return 41;

            else if (md == 8 && it == 2 || it == 3)
                return 27;

            else if (md == 8 && it == 4 || it == 7)
                return 7;

            else if (md == 8 && it == 5)
                return 19;

            else if (md == 8 && it == 6 || md == 8 && it == 9)
                return 13;

            else if (md == 9)
                return 29;

            else if (md == 11 && it == 0 || it == 1)
                return 19;

            return 8; // ค่าเริ่มต้น

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                if (cbport.SelectedItem == null || cbbaudrate.SelectedItem == null || cbdatabit.SelectedItem == null ||
                cbstopbit.SelectedItem == null || cbparitybit.SelectedItem == null)
                {
                    MessageBox.Show("Please select all serial port settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    serialPort = new SerialPort(); // assign to class-level variable
                    serialPort.PortName = cbport.SelectedItem.ToString();
                    serialPort.BaudRate = int.Parse(cbbaudrate.SelectedItem.ToString());
                    serialPort.DataBits = int.Parse(cbdatabit.SelectedItem.ToString());
                    serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cbstopbit.SelectedItem.ToString());
                    serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), cbparitybit.SelectedItem.ToString());
                    serialPort.DataReceived += SerialPort_DataReceived; // attach event
                    serialPort.Open(); // open COM port
                    MessageBox.Show("Serial Port connected successfully!");
                    UpdateConnectionStatus(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error connecting to Serial Port: {ex.Message}");
                    UpdateConnectionStatus(false);
                }
            }
            else
            {
                MessageBox.Show("Serial Port is already open.");
            }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            string incoming = serialPort.ReadExisting();
            this.Invoke(new MethodInvoker(() =>
            {
                string[] lines = incoming.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(lines[i]))
                        continue;

                    // Merge '-1' and '-' if they appear consecutively
                    if (lines[i] == "-1" && i + 1 < lines.Length && lines[i + 1] == "-")
                    {
                        string merged = "-1-";
                        Textfromserial.AppendText(merged + Environment.NewLine);
                        serialLineBuffer.Add(merged);
                        i++; // Skip next line
                    }
                    else
                    {
                        Textfromserial.AppendText(lines[i] + Environment.NewLine);
                        serialLineBuffer.Add(lines[i]);
                    }
                }
                Statelb.Text = "AppendText";
                serialTimeoutTimer.Stop();
                serialTimeoutTimer.Start();
                int expectedLines = GetExpectedLineCount();
                Statelb.Text = "Get expected line count";
                serialLineBuffer.Add(incoming);
                if (serialLineBuffer.Count == expectedLines)
                {
                    ReceivedData.Clear();
                    ReceivedData.Text = string.Join(Environment.NewLine, serialLineBuffer);
                    Statelb.Text = "join string";
                    Dataprocess();
                    Statelb.Text = "Data process";
                    serialLineBuffer.Clear();
                }

            }));


        }

        private void SerialTimeoutTimer_Tick(object sender, EventArgs e)
        {
            Textfromserial.Clear();
            serialLineBuffer.Clear();
            ReceivedData.Clear();
            serialTimeoutTimer.Stop(); // Stop timer after clearing
        }

        private void UpdateConnectionStatus(bool isConnected)
        {
            lblConnectionStatus.Text = isConnected ? "Connected" : "Disconnected";
            lblConnectionStatus.ForeColor = isConnected ? Color.Green : Color.Red;
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                MessageBox.Show("Serial Port disconnected!");
                UpdateConnectionStatus(false);
            }
            else MessageBox.Show("Serial Port is not open.");
        }

        private void Dataprocess()
        {
            measurements.Clear();
            string[] lines = ReceivedData.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Statelb.Text = "Split text";
            foreach (string line in lines)
            {
                debugtextbox.AppendText("each line " + line + Environment.NewLine);
            }

            if (Dcheckbox.Checked == true)
            {
                DExtraction(lines);
            }
            if (Lcheckbox.Checked == true)
            {
                LExtraction(lines);
            }
            if (Rcheckbox.Checked == true)
            {
                RExtraction(lines);
            }
            if(IAcheckbox.Checked == true)
            {
                var (ia1, ia2, ia3) = ExtractMethod_2(lines);
            }
        }

        private void DExtraction(string[] lines)
        {
            Measurement currentMeasurement = new Measurement();
            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("D"))
                {
                    var valueStr = line.Substring(1).Trim();
                    if (double.TryParse(valueStr, out double dValue))
                    {
                        currentMeasurement.D = dValue;
                        measurements.Add(currentMeasurement);
                        //ConvertedData.AppendText($"L: {currentMeasurement.L}");
                    }
                }
            }
        }

        private void LExtraction(string[] lines)
        {

            Measurement currentMeasurement = new Measurement();
            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("L"))
                {
                    var valueStr = line.Substring(1).Trim();
                    if (double.TryParse(valueStr, out double lValue))
                    {
                        currentMeasurement.L = lValue;
                        measurements.Add(currentMeasurement);
                        //ConvertedData.AppendText($"L: {currentMeasurement.L}");
                    }
                }
            }
        }

        private void RExtraction(string[] lines)
        {
            Measurement currentMeasurement = new Measurement();
            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("R"))
                {
                    var valueStr = line.Substring(1).Trim();
                    if (double.TryParse(valueStr, out double rValue))
                    {
                        currentMeasurement.R = rValue;
                        measurements.Add(currentMeasurement);
                        //ConvertedData.AppendText($"L: {currentMeasurement.L}");
                    }
                }
            }
        }
        private (double ia1, double ia2, double ia3) ExtractMethod_2(string[] lines)
        {
            Statelb.Text = "Extract";
            string iaLine = lines.FirstOrDefault(line => line.Trim().StartsWith("IA"));
            double ia1 = 0, ia2 = 0, ia3 = 0;
            if (iaLine != null)
            {
                // Remove "IA" and trim the result
                string numbersPart = iaLine.Substring(iaLine.IndexOf("IA") + 2).Trim();
                string[] numberStrings = numbersPart.Split(':');
                if (numberStrings.Length >= 3 &&
                    double.TryParse(numberStrings[0], out ia1) &&
                    double.TryParse(numberStrings[1], out ia2) &&
                    double.TryParse(numberStrings[2], out ia3))
                {
                    // ia1, ia2, ia3 now hold the values after "IA"
                    // You can use them as needed, e.g.:
                }
            }

            if (multiplier.SelectedIndex == 1)
            {
                ia1 *= 10;
                ia2 *= 10;
                ia3 *= 10;
            }

            if (decimaal.SelectedIndex == -1)
            {
                ia1 = Math.Round(ia1, 3);
                ia2 = Math.Round(ia2, 3);
                ia3 = Math.Round(ia3, 3);
            }

            else if (decimaal.SelectedIndex != -1)
            {
                int decimalPlaces = decimaal.SelectedIndex + 1;
                ia1 = Math.Round(ia1, decimalPlaces);
                ia2 = Math.Round(ia2, decimalPlaces);
                ia3 = Math.Round(ia3, decimalPlaces);
            }

            return (ia1, ia2, ia3);
        }



        private void Keypress(int key, double value1, double value2 = 0, double value3 = 0, double value4 = 0)
        {
            Statelb.Text = "Keypress";
            IntPtr hExcel = FindWindow("XLMAIN", null);
            if (hExcel == IntPtr.Zero)
            {
                MessageBox.Show("Excel window not found. Open Excel and select a cell first.");
                return;
            }

            SetForegroundWindow(hExcel);
            Thread.Sleep(80);

            if (key == 0)
            {
                string text = value1.ToString();
                SendKeys.SendWait(text);
            }

            else if (key == 1)
            {
                string text1 = value1.ToString();
                string text2 = value2.ToString();
                string text3 = value3.ToString();
                string text4 = value4.ToString();
                SendKeys.SendWait(text1);
                Thread.Sleep(80);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text2);
                Thread.Sleep(80);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text3);
                Thread.Sleep(80); Thread.Sleep(100);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(80);
                SendKeys.SendWait(text4);

            }

            else if (key == 2)
            {
                string text1 = value1.ToString();
                string text2 = value2.ToString();
                string text3 = value3.ToString();
                SendKeys.SendWait(text1);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text2);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text3);
            }

            else if (key == 3)
            {
                string text1 = value1.ToString();
                string text2 = value2.ToString();
                SendKeys.SendWait(text1);
                Thread.Sleep(80);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(80);
                SendKeys.SendWait(text2);
            }

            else if (key == 4)
            {
                string text1 = value1.ToString();
                string text2 = value2.ToString();
                SendKeys.SendWait(text1);
                Thread.Sleep(80);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(80);
                SendKeys.SendWait("{RIGHT}");
                Thread.Sleep(80);
                SendKeys.SendWait(text2);
            }
        }

        private void testbt_Click(object sender, EventArgs e)
        {
            debugtextbox.Clear();
            ReceivedData.Clear();
            Textfromserial.Clear();
            debugtextbox2.Clear();
            ConvertedData.Clear();
            serialLineBuffer.Clear();
            measurements.Clear();
            l1Value = 0; l2Value = 0;
            incoming = string.Empty;
            Statelb.Text = "Clear all text";
        }

        private void lblConnectionStatus_Click(object sender, EventArgs e)
        {

        }

        private void injectbt_Click(object sender, EventArgs e)
        {

        }
    }

    public class Measurement
    {
        public double L { get; set; }
        public double dX { get; set; }
        public double dY { get; set; }
        public double Xc { get; set; }
        public double Yc { get; set; }
        public double D { get; set; }
        public double R { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class Generation
    {
        public double L { get; set; }
        public double dX { get; set; }
        public double dY { get; set; }
        public double Xc { get; set; }
        public double Yc { get; set; }
        public double D { get; set; }
        public double R { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }



}
