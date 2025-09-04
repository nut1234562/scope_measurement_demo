using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
namespace scope_measurement_demo
{


    public partial class Form1 : Form
    {
        List<double> lList = new List<double> { };
        List<double> rList = new List<double> { };
        List<double> dxList = new List<double> { };
        List<double> dyList = new List<double> { };
        private List<string> serialLineBuffer = new List<string>();
        List<Measurement> measurements = new List<Measurement>();

        double dx1 = 0, dx2 = 0, dx3 = 0, dy1 = 0, dy2 = 0, dy3 = 3, d = 0, r = 0, l1 = 0, l2 = 0, l3 = 0, l4 = 0, xc = 0, yc = 0, x = 0, y = 0, r1 = 0, r2 = 0, r3 = 0, r4 = 0; // ตัวเล็กใช้คำนวณและแสดงช่อง output
        double[] variables;
        double DX1 = 0, DX2 = 0, DX3 = 0, DY3 = 0, DY1 = 0, DY2 = 0, D = 0, R = 0, L1 = 0, L2 = 0, L3 = 0, L4 = 0, XC = 0, YC = 0, X = 0, Y = 0, R1 = 0, R2 = 0, R3 = 0, R4 = 0;  // ตัวใหญ่ใช้แสดงผลบน UI
        double l1Value = 0, l2Value = 0;
        int currentline = 0;
        string incoming;
        private readonly StringBuilder _serialBuffer = new StringBuilder();
        private const string MessageTerminator = "\n\n";
        static readonly Stopwatch sw = Stopwatch.StartNew();
        static long last = 0;
        const long period = 1000;

        SerialPort serialPort;
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
            variables = new double[] { DX1, DY1, D, R, L1, L2, L3, L4, XC, YC, X, Y };

            SerialPort serialPort = new SerialPort();
            foreach (var port in SerialPort.GetPortNames())
            {
                cbport.Items.Add(port); // เพิ่มชื่อ port ทีละตัว
            }

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
                Itemcb.Items.Add("OD 1 & 2 (Gate)");
                Itemcb.Items.Add("ID 1 & 2 (Gate)");
                Itemcb.Items.Add("Concentric 1 & 2 (Gate Side)");
                Itemcb.Items.Add("OD 1 & 2 (Ejector Side)");
                Itemcb.Items.Add("ID 1 & 2 (Ejector Side");
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

            }

        }


        private void Printbt_Click(object sender, EventArgs e)
        {

            /*foreach(double things in variables){
                double variant = things;
                variant = Math.Round(random.NextDouble() * 100, 6);
            }
            XC = Math.Round(random.NextDouble() * 100, 6); YC = Math.Round(random.NextDouble() * 100, 6); D = Math.Round(random.NextDouble() * 100, 6); R = Math.Round(random.NextDouble() * 100, 6);
            L1 = Math.Round(random.NextDouble() * 100, 6); L2 = Math.Round(random.NextDouble() * 100, 6); DX1 = Math.Round(random.NextDouble() * 100, 6); DY1 = Math.Round(random.NextDouble() * 100, 6);
            X = Math.Round(random.NextDouble() * 100, 6); Y = Math.Round(random.NextDouble() * 100, 6);
            L3 = Math.Round(random.NextDouble() * 100, 6); L4 = Math.Round(random.NextDouble() * 100, 6);
            currentline = 0;
            ConvertedData.Clear();
            ReceivedData.Clear();
            ReceivedData.AppendText($"No.1\r\nRectangle 5/5\r\n-1-\r\n    X {X}\r\n    Y {Y}\r\n    L1 {L1}\r\n    L2 {L2}\r\n\r\n");
            ReceivedData.AppendText($"No.2\r\nCircle(Multi) 4/4\r\n-1-\r\n    LS\r\n    Xc {XC}\r\n    Yc {YC}\r\n    D {D}\r\n    R {R}\r\n\r\n");
            ReceivedData.AppendText($"No.3\r\nCircle(Multi) 4/4\r\n-1-\r\n    LS\r\n    Xc {XC}\r\n    Yc {YC}\r\n    D {D}\r\n    R {R}\r\n\r\n");
            ReceivedData.AppendText($"No.4\r\nDistance(Point-Point) 2/2\r\n-1-\r\n    L {L1}\r\n    dX {DX1}\r\n    dY {DY1}\r\n\r\n");
            ReceivedData.AppendText($"No.5\r\nDistance(Point-Point) 2/2\r\n-1-\r\n    L {L2}\r\n    dX {DX1}\r\n    dY {DY1}\r\n");

            string inputText = ReceivedData.Text;
            string[] eachlines = inputText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in eachlines)
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("dX"))
                {
                    // Split the line by space and get the second element
                    string[] parts = trimmedLine.Split(' ');
                    if (parts.Length > 1 && double.TryParse(parts[1], out double dxValue))
                    {
                        dx1 = dxValue;
                    }
                }
                else if (trimmedLine.StartsWith("No.4"))
                {
                    trimmedLine = eachlines[currentline + 3].Trim(); ;
                    if (trimmedLine.StartsWith("L"))
                    {
                        string[] parts = trimmedLine.Split(' ');
                        if (parts.Length > 1 && double.TryParse(parts[1], out double l3Value))
                        {
                            l3 = l3Value;
                        }
                    }
                }
                else if (trimmedLine.StartsWith("No.5"))
                {
                    trimmedLine = eachlines[currentline + 3].Trim(); ;
                    if (trimmedLine.StartsWith("L"))
                    {
                        string[] parts = trimmedLine.Split(' ');
                        if (parts.Length > 1 && double.TryParse(parts[1], out double l4Value))
                        {
                            l4 = l4Value;
                        }
                    }
                }
                else if (trimmedLine.StartsWith("dY"))
                {
                    // Split the line by space and get the second element
                    string[] parts = trimmedLine.Split(' ');
                    if (parts.Length > 1 && double.TryParse(parts[1], out double dyValue))
                    {
                        dy1 = dyValue;
                    }
                }
                else if (trimmedLine.StartsWith("dY"))
                {
                    string[] parts = trimmedLine.Split(' ');
                    if (parts.Length > 1 && double.TryParse(parts[1], out double yValue))
                    {
                        dy1 = yValue;
                    }
                }

                currentline++;
            }

            if (multiplier.SelectedIndex == 1)
            {
                xc *= 10;
                yc *= 10;
                d *= 10;
                r *= 10;
                l1 *= 10;
                l2 *= 10;
                l3 *= 10;
                l4 *= 10;
                dx1 *= 10;
                dy1 *= 10;
            }

            int decimalPlaces = decimaal.SelectedIndex + 1;
            xc = Math.Round(xc, decimalPlaces);
            yc = Math.Round(yc, decimalPlaces);
            d = Math.Round(d, decimalPlaces);
            r = Math.Round(r, decimalPlaces);
            l1 = Math.Round(l1, decimalPlaces);
            l2 = Math.Round(l2, decimalPlaces);
            l3 = Math.Round(l3, decimalPlaces);
            l4 = Math.Round(l4, decimalPlaces);
            dx1 = Math.Round(dx1, decimalPlaces);
            dy1 = Math.Round(dy1, decimalPlaces);

            ConvertedData.AppendText($"L: {l3} \r\n L: {l4} \r\n dY: {dy1} \r\n ");*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void decimaal_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            if (md == 0 || md == 3 || md == 4)//เลือกว่าปริ้นกี่บรรทัด โดยเลือกตาม model และ item
                return 6;

            else if (md == 1)//ถ้าปริ้น 7 บรรทัด
                return 20;

            else if (md == 2)
                return 35;

            else if (md == 5 && it == 0 || md == 6 || md == 10)

                return 9;

            else if (md == 5 && it == 1)
                return 13;

            else if (md == 7 || md == 8 && it == 8)
                return 19;

            else if (md == 8 && it == 0 || it == 1)
                return 39;

            else if (md == 8 && it == 2 || it == 3)
                return 27;

            else if (md == 8 && it == 4 || it == 7)
                return 7;

            else if (md == 8 && it == 5)
                return 15;

            else if (md == 8 && it == 6)
                return 13;

            else if(md == 9)
                return 29;

            else if (md == 11 && it == 0 || it == 1)
                return 19;

            return 6; // ค่าเริ่มต้น

        }

        private void Connect_Click(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to Serial Port: {ex.Message}");
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

                incoming = serialPort.ReadLine();
                this.Invoke(new MethodInvoker(() =>
                {
                    Textfromserial.AppendText(incoming + Environment.NewLine);
                    int expectedLines = GetExpectedLineCount();
                    serialLineBuffer.Add(incoming);
                    if (serialLineBuffer.Count == expectedLines)
                     {
                     ReceivedData.Text = string.Join(Environment.NewLine, serialLineBuffer);
                     Dataprocess();
                     serialLineBuffer.Clear();
                     }
                    
                }));

            
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                MessageBox.Show("Serial Port disconnected!");
            }
            else MessageBox.Show("Serial Port is not open.");
        }

        private void Dataprocess() 
        {
            string[] lines = ReceivedData.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                debugtextbox.AppendText("each line " + line + Environment.NewLine);
            }
            if(Modelcb.SelectedIndex == 0)//1st gear
            {
                ExtractMethod_0(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"L: {measurements[0].L}");
                Keypress(0, measurements[0].L);
            }

            else if(Modelcb.SelectedIndex == 1)//VHB30
            {
                ExtractMethod_0(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"L: {measurements[0].L}\t L: {measurements[1].L}\t L: {measurements[2].L}");
                Keypress(2, measurements[0].L, measurements[1].L, measurements[2].L);
            }

            else if (Modelcb.SelectedIndex == 2 && Itemcb.SelectedIndex == 0)//Shaft
            {
                ExtractMethod_1(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"R: {measurements[0].R}\t R: {measurements[1].R}\t R: {measurements[2].R}\t R: {measurements[3].R}\t ");
                Keypress(1, measurements[0].R, measurements[1].R, measurements[2].R, measurements[3].R);
            }

            else if (Modelcb.SelectedIndex == 3)//lead screw
            {
                ExtractMethod_0(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"L: {measurements[0].L}");
                Keypress(0, measurements[0].L);
            }

            else if (Modelcb.SelectedIndex == 4)//14RA-20T-3-18-12-PPS-S
            {
                ExtractMethod_2(lines);
                ConvertedData.Clear();
                var (ia1,ia2,ia3) = ExtractMethod_2(lines);
                ConvertedData.AppendText($"IA1: {ia1}\tIA2: {ia2}\tIA3: {ia3}\r\n");
                Keypress(2, ia1, ia2, ia3);
            }

            else if (Modelcb.SelectedIndex == 5)//14RA-PM20TF80-2
            {
                if(Itemcb.SelectedIndex == 0)//OD3
                {
                    ExtractMethod_3(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"D: {measurements[0].D}\r\n");
                    Keypress(0, measurements[0].D);

                }
                else if (Itemcb.SelectedIndex == 1)//Radius Of Stopper
                {
                    ExtractMethod_0(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L: {measurements[0].L}\r\n" +
                                             $"L: {measurements[1].L}\r\n");
                    Keypress(3, measurements[0].L, measurements[1].L);
                }
            }

            else if (Modelcb.SelectedIndex == 6)//FG-Magnet
            {
                ExtractMethod_3(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"D: {measurements[0].D}\r\n");
                Keypress(0, measurements[0].D);
            }

            else if (Modelcb.SelectedIndex == 7)//PM255
            {   
                ExtractMethod_3(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"D: {measurements[0].L}\r\n" +
                                         $"D: {measurements[1].L}\r\n");
                Keypress(3, measurements[0].L, measurements[1].L);
            }

            else if (Modelcb.SelectedIndex == 8)//Se-Series
            {
                if (Itemcb.SelectedIndex == 0)//Concentric (Gate Side)
                {
                    ExtractMethod_0(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L: {measurements[0].L}" +
                                             $"L: {measurements[1].L}");
                    Keypress(3, measurements[0].L, measurements[1].L);
                }

                else if (Itemcb.SelectedIndex == 1)//Concentric (Ejector Side)
                {
                    ExtractMethod_0(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L: {measurements[0].L}" +
                                             $"L: {measurements[1].L}");
                    Keypress(3, measurements[0].L, measurements[1].L);
                }

                else if (Itemcb.SelectedIndex == 2)//Thickness(Gate side)
                {
                    ExtractMethod_0(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L: {measurements[0].L}" +
                                             $"L: {measurements[1].L}" +
                                             $"L: {measurements[2].L}" +
                                             $"L: {measurements[3].L}");
                    Keypress(1, measurements[0].L, measurements[1].L, measurements[2].L, measurements[3].L);
                }

                else if (Itemcb.SelectedIndex == 3)//Thickness(Ejector side)
                {
                    ExtractMethod_0(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L: {measurements[0].L}" +
                                             $"L: {measurements[1].L}" +
                                             $"L: {measurements[2].L}" +
                                             $"L: {measurements[3].L}");
                    Keypress(1, measurements[0].L, measurements[1].L, measurements[2].L, measurements[3].L);
                }

                else if (Itemcb.SelectedIndex == 4)// OD 1 & 2 (Gate)
                {
                    string inputText = ReceivedData.Text;
                    string l1Line = inputText
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(line => line.Trim().StartsWith("L1"));

                    string l2Line = inputText
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(line => line.Trim().StartsWith("L2"));

                    ExtractMethod_4(l1Line,l2Line);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L1: {l1Value}\r\nL2: {l2Value}\r\n");
                    Keypress(3,l2Value,l2Value);
                }

                else if (Itemcb.SelectedIndex == 5)// ID 1 & 2 (Gate)
                {
                    ExtractMethod_3(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"D: {measurements[0].D}\r\nD: {measurements[1].D}\r\n");
                    Keypress(3, measurements[0].D, measurements[1].D);
                }

                else if (Itemcb.SelectedIndex == 6)//Concentric 1 & 2 (Gate Side)
                {
                    ExtractMethod_0(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L: {measurements[0].L}\r\nL: {measurements[1].L}\r\n");
                    Keypress(3, measurements[0].L, measurements[1].L);
                }

                else if (Itemcb.SelectedIndex == 7)// OD 1 & 2 (Ejector Side)
                {
                    string inputText = ReceivedData.Text;
                    string l1Line = inputText
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(line => line.Trim().StartsWith("L1"));

                    string l2Line = inputText
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(line => line.Trim().StartsWith("L2"));

                    ExtractMethod_4(l1Line,l2Line);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L1: {l1Value}\r\nL2: {l2Value}\r\n");
                    Keypress(3,l1Value, l2Value);
                }

                else if (Itemcb.SelectedIndex == 8)// ID 1 & 2 (Ejector Side)
                {
                    ExtractMethod_3(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"D: {measurements[0].D}\r\nD: {measurements[1].D}\r\n");
                    Keypress(3, measurements[0].D, measurements[1].D);
                }

                else if (Itemcb.SelectedIndex == 9)//Concentric 1 & 2 (Ejector Side)
                {
                    ExtractMethod_0(lines);
                    ConvertedData.Clear();
                    ConvertedData.AppendText($"L: {measurements[0].L}\r\nL: {measurements[1].L}\r\n");
                    Keypress(3, measurements[0].L, measurements[1].L);
                }
            }

            else if (Modelcb.SelectedIndex == 9)//LED-Exial-Fan
            {
                ExtractMethod_3(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"D: {measurements[0].D}\r\nD: {measurements[1].D}\r\nD: {measurements[2].D}");
                Keypress(2, measurements[0].D, measurements[1].D, measurements[2].D);
            }

            else if (Modelcb.SelectedIndex == 10)//FAN MOTOR
            {
                ExtractMethod_3(lines);
                ConvertedData.Clear();
                ConvertedData.AppendText($"D: {measurements[0].D}\r\n");
                Keypress(0, measurements[0].D);
            }

            else if (Modelcb.SelectedIndex == 11)//BLOM70
            {
                ExtractMethod_3(lines);
                ConvertedData.Clear();
                if(Itemcb.SelectedIndex == 0)//OD, ID (Gate Side)
                {
                    ConvertedData.AppendText($"D: {measurements[0].D}\r\nD: {measurements[1].D}\r\n");
                    Keypress(4, measurements[0].D, measurements[1].D);
                }
                else if(Itemcb.SelectedIndex == 1)//OD, ID (Ejector Side)
                {
                    ConvertedData.AppendText($"D: {measurements[0].D}\r\nD: {measurements[1].D}\r\n");
                    Keypress(4, measurements[0].D, measurements[1].D);
                }
            }
            //ReceivedData.Clear();
            //Textfromserial.Clear();

        }

        private void ExtractMethod_0(string[] lines)
        {

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("N"))
                {
                    Measurement currentMeasurement = new Measurement();


                    int linesToRead = 6;//ห่างจาก No.กี่บรรทัด บวกไปอีก 3
                    for (int j = 0; j < linesToRead && i + j < lines.Length; j++)
                    {
                        string trimmedLine = lines[i + j].Trim();
                        string[] parts = trimmedLine.Split(' ');
                        if (parts.Length > 1)
                        {
                            if (trimmedLine.StartsWith("L"))
                            {
                                var valueStr = trimmedLine.Substring(1).Trim();
                                if (double.TryParse(valueStr, out double lValue))
                                {
                                    currentMeasurement.L = lValue;
                                    //ConvertedData.AppendText($"L: {currentMeasurement.L}");
                                }
                            }
                        }
                    }

                    measurements.Add(currentMeasurement);
                }
            }

            if (multiplier.SelectedIndex == 1)
            {
                foreach (Measurement measurement in measurements)
                {
                    measurement.L *= 10;
                }
            }

            if (decimaal.SelectedIndex == -1)
            {
                foreach (Measurement measurement in measurements)
                {
                    measurement.L = Math.Round(measurement.L, 3);
                }
            }

            else if (decimaal.SelectedIndex != -1)
            {
                int decimalPlaces = decimaal.SelectedIndex + 1;
                foreach (Measurement measurement in measurements)
                {
                    measurement.L = Math.Round(measurement.L, decimalPlaces);
                }
            }
        }

        private void ExtractMethod_1(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("No."))
                {
                    Measurement currentMeasurement = new Measurement();

                    int linesToRead = 7;
                    for (int j = 0; j < linesToRead && i + j < lines.Length; j++)
                    {
                        string trimmedLine = lines[i + j].Trim();
                        string[] parts = trimmedLine.Split(' ');
                        if (parts.Length > 1)
                        {
                            if (trimmedLine.StartsWith("R") && double.TryParse(parts[1], out double RValue))
                            {
                                currentMeasurement.R = RValue;
                                debugtextbox2.AppendText($" R: {currentMeasurement.R}");
                            }
                        }
                    }

                    measurements.Add(currentMeasurement);
                }
            }

            if (multiplier.SelectedIndex == 1)
            {
                foreach (Measurement measurement in measurements)
                {
                    measurement.R *= 10;
                }
            }

            if (decimaal.SelectedIndex == -1)
            {
                foreach (Measurement measurement in measurements)
                {
                    measurement.R = Math.Round(measurement.R, 3);
                }
            }

            else if (decimaal.SelectedIndex != -1)
            { int decimalPlaces = decimaal.SelectedIndex + 1;
                foreach (Measurement measurement in measurements)
                {
                    measurement.R = Math.Round(measurement.R, decimalPlaces);
                }
            }
        }

        private (double ia1, double ia2, double ia3) ExtractMethod_2(string[] lines)
        {
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

        private void ExtractMethod_3(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith("N"))
                {
                    Measurement currentMeasurement = new Measurement();

                    int linesToRead = 10;
                    for (int j = 0; j < linesToRead && i + j < lines.Length; j++)
                    {
                        string trimmedLine = lines[i + j].Trim();
                        string[] parts = trimmedLine.Split(' ');
                        if (parts.Length > 1)
                        {
                            if (trimmedLine.StartsWith("D") && double.TryParse(parts[1], out double DValue))
                            {
                                currentMeasurement.D = DValue;
                            }
                        }
                    }
                    measurements.Add(currentMeasurement);
                }
            }

            if (decimaal.SelectedIndex == -1)
            {
                foreach (Measurement measurement in measurements)
                {
                    measurement.D = Math.Round(measurement.D, 3);
                }
            }

            else if (decimaal.SelectedIndex != -1)
            {
                int decimalPlaces = decimaal.SelectedIndex + 1;
                foreach (Measurement measurement in measurements)
                {
                    measurement.D = Math.Round(measurement.D, decimalPlaces);
                }
            }
        }

        private void ExtractMethod_4(string l1Line, string l2Line)
        { 

            // 2) If found, split the line by spaces -> ["L1", "123.45"]
            if (l1Line != null)
            {
                string[] parts = l1Line.Trim().Split(' ');
                if (parts.Length > 1 && double.TryParse(parts[1], out double val))
                    l1Value = val; // save the number
            }

            if (l2Line != null)
            {
                string[] parts = l2Line.Trim().Split(' ');
                if (parts.Length > 1 && double.TryParse(parts[1], out double val))
                    l2Value = val;
            }

            if (decimaal.SelectedIndex == -1)
            {
                foreach (Measurement measurement in measurements)
                {
                    l1Value = Math.Round(l1Value, 3);
                }
            }

            else if (decimaal.SelectedIndex != -1)
            {
                int decimalPlaces = decimaal.SelectedIndex + 1;
                foreach (Measurement measurement in measurements)
                {
                    l2Value = Math.Round(l2Value, decimalPlaces);
                }
            }
        }

        private void Keypress(int key, double value1, double value2 = 0 , double value3 = 0, double value4 = 0)
        {
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
                String text = value1.ToString();
                SendKeys.SendWait(text);
            }

            else if (key == 1)
            {
                String text1 = value1.ToString();
                String text2 = value2.ToString();
                String text3 = value3.ToString();
                String text4 = value4.ToString();
                SendKeys.SendWait(text1);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text2);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text3);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text4);

            }

            else if (key == 2)
            {
                String text1 = value1.ToString();
                String text2 = value2.ToString();
                String text3 = value3.ToString();
                SendKeys.SendWait(text1);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text2);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text3);
            }

            else if (key == 3)
            {
                String text1 = value1.ToString();
                String text2 = value2.ToString();
                SendKeys.SendWait(text1);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text2);
            }

            else if (key == 4) 
            {
                String text1 = value1.ToString();
                String text2 = value2.ToString();
                SendKeys.SendWait(text1);
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait("{RIGHT}");
                SendKeys.SendWait(text2);
            }
        }


        private void cbparitybit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void testbt_Click(object sender, EventArgs e)
        {
            debugtextbox.Clear();
            ReceivedData.Clear();
            Textfromserial.Clear();
            debugtextbox2.Clear();
        }

        private void cbport_SelectedIndexChanged(object sender, EventArgs e)
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
