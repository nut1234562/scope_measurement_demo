using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;


namespace scope_measurement_demo
{


    public partial class Form1 : Form
    {
        private List<string> serialLineBuffer = new List<string>(128);
        List<Measurement> measurements = new List<Measurement>();

        //double dx1 = 0, dx2 = 0, dx3 = 0, dy1 = 0, dy2 = 0, dy3 = 3, d = 0, r = 0, l1 = 0, l2 = 0, l3 = 0, l4 = 0, xc = 0, yc = 0, x = 0, y = 0, r1 = 0, r2 = 0, r3 = 0, r4 = 0; // ตัวเล็กใช้คำนวณและแสดงช่อง output
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


        private Excel.Application _excel;
        private bool _weStartedExcel;
        private static readonly Regex NumberAfterColon = new(
                    @":\s*([+-]?(?:\d+(?:\.\d+)?|\.\d+)(?:[eE][+-]?\d+)?)",
                    RegexOptions.Compiled);


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
            serialTimeoutTimer.Interval = 2000; // 2 seconds
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
        // Replace the Refresh_Click method with the following code

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string incoming = serialPort.ReadExisting();
            this.Invoke(new MethodInvoker(() =>
            {
                ReceivedData.Clear();
                ConvertedData.Clear();
                string[] lines = incoming.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    Textfromserial.AppendText(lines[i] + Environment.NewLine);
                    serialLineBuffer.Add(lines[i]);
                }
                Statelb.Text = "AppendText";
                serialTimeoutTimer.Stop();
                serialTimeoutTimer.Start();

            }));


        }

        private void SerialTimeoutTimer_Tick(object sender, EventArgs e)
        {
            ReceivedData.Text += string.Join(Environment.NewLine, serialLineBuffer) + Environment.NewLine;
            Dataprocess();
            SendValueToExcel();
            Textfromserial.Clear();
            serialLineBuffer.Clear();
            serialTimeoutTimer.Stop(); // Stop timer after clearing
        }

        private void UpdateConnectionStatus(bool isConnected)
        {
            lblConnectionStatus.Text = isConnected ? "Connected" : "Disconnected";
            lblConnectionStatus.ForeColor = isConnected ? Color.Green : Color.Red;
        }

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

            if (DCheckbox.Checked)
            {
                DExtraction(lines);
            }
            if (LCheckbox.Checked)
            {
                LExtraction(lines);
            }
            if (RCheckbox.Checked)
            {
                RExtraction(lines);
            }
            if (L1L2Checkbox.Checked)
            {
                L1L2Extraction(lines);
            }
            if (IACheckbox.Checked)
            {
                var (ia1, ia2, ia3) = IAExtraction(lines);
                ConvertedData.AppendText($"IA1: {ia1}\tIA2: {ia2}\tIA3: {ia3},\t");
                //Keypress(1, ia1, ia2, ia3);
            }

        }

        private void DExtraction(string[] lines)
        {
            Statelb.Text = "Extract D";
            int n = 0;
            bool inDMeasurement = false;

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("No.", StringComparison.OrdinalIgnoreCase))
                {
                    inDMeasurement = false;
                    continue;
                }

                if (IsCircleMultiMeasurementHeader(line))
                {
                    inDMeasurement = true;
                    continue;
                }

                if (IsCircleStandardMeasurementHeader(line))
                {
                    inDMeasurement = false;
                    continue;
                }

                if (line.StartsWith("Circle", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Distance", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Rectangle", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Intersection", StringComparison.OrdinalIgnoreCase))
                {
                    inDMeasurement = false;
                    continue;
                }

                if (!inDMeasurement || !line.StartsWith("D", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1 &&
                    double.TryParse(parts[1], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double dValue))
                {
                    if (measurements.Count > n)
                    {
                        measurements[n].D = dValue;
                    }
                    else
                    {
                        measurements.Add(new Measurement { D = dValue });
                    }

                    ConvertedData.AppendText($"D: {dValue},\t");
                    n++;
                }
            }
        }

        private void LExtraction(string[] lines)
        {
            int n = 0;
            Measurement currentMeasurement = new Measurement();
            foreach (string line in lines)
            {
                if (line.Trim().StartsWith("L"))
                {
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 1 && double.TryParse(parts[1], out double lValue))
                    {
                        if (measurements.Count > n)
                        {
                            measurements[n].L = lValue;
                        }
                        else
                        {
                            measurements.Add(new Measurement { L = lValue });
                        }

                        ConvertedData.AppendText($"L: {lValue},\t");
                        n++;
                    }
                }
            }
        }


        private void RExtraction(string[] lines)
        {
            int measurementIndex = 0;
            bool inDMeasurement = false;

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("No.", StringComparison.OrdinalIgnoreCase))
                {
                    inDMeasurement = false;
                    continue;
                }

                if (IsCircleStandardMeasurementHeader(line))
                {
                    inDMeasurement = true;
                    continue;
                }

                if (IsCircleMultiMeasurementHeader(line))
                {
                    inDMeasurement = false;
                    continue;
                }

                if (line.StartsWith("Circle", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Distance", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Rectangle", StringComparison.OrdinalIgnoreCase) ||
                    line.StartsWith("Intersection", StringComparison.OrdinalIgnoreCase))
                {
                    inDMeasurement = false;
                    continue;
                }

                if (!inDMeasurement || !line.StartsWith("R", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1 &&
                    double.TryParse(parts[1], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double rValue))
                {
                    if (measurements.Count > measurementIndex)
                    {
                        measurements[measurementIndex].R = rValue;
                    }
                    else
                    {
                        measurements.Add(new Measurement { R = rValue });
                    }

                    ConvertedData.AppendText($"R: {rValue},\t");
                    measurementIndex++;
                }
            }
        }

        private static bool IsCircleMeasurementHeader(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return false;
            }

            if (!line.StartsWith("Circle", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (line.Length == "Circle".Length)
            {
                return true;
            }

            char nextChar = line["Circle".Length];
            return char.IsWhiteSpace(nextChar) || nextChar == '(' || nextChar == ':' || nextChar == '-';
        }

        private static bool IsCircleMultiMeasurementHeader(string line)
        {
            return IsCircleMeasurementHeader(line) &&
                   line.IndexOf("(Multi", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsCircleStandardMeasurementHeader(string line)
        {
            return IsCircleMeasurementHeader(line) &&
                   line.IndexOf("(Multi", StringComparison.OrdinalIgnoreCase) < 0;
        }

        private void L1L2Extraction(string[] lines)
        {
            string inputText = ReceivedData.Text;
            string l1Line = inputText
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault(line => line.Trim().StartsWith("L1"));

            string l2Line = inputText
            .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault(line => line.Trim().StartsWith("L2"));

            ExtractMethod_4(l1Line, l2Line);
            ConvertedData.AppendText($"L1: {l1Value}\tL2: {l2Value},\t");
            //Keypress(3, l1Value, l2Value);
        }

        private (double ia1, double ia2, double ia3) IAExtraction(string[] lines)
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

        private void ExtractMethod_4(string l1Line, string l2Line)
        {
            Statelb.Text = "Extract";
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

        private void Injectwindowckb_CheckedChanged(object sender, EventArgs e)
        {
            if (Injectwindowckb.Checked)
            {
                // Create and show the InjectForm
                Inject_form injectForm = new Inject_form();
                injectForm.Show();
            }
            else
            {
                // Optionally close it when unchecked
                foreach (Form form in Application.OpenForms)
                {
                    if (form is Inject_form)
                    {
                        form.Close();
                        break;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateStatus()
        {
            if (_excel == null)
            {
                lblStatus.Text = "Excel: (not attached)";
                return;
            }

            try
            {
                string wbName = _excel.ActiveWorkbook != null
                    ? _excel.ActiveWorkbook.Name
                    : "(no workbook)";
                lblStatus.Text = $"Excel: attached ({_excel.Version}) — {wbName}";
            }
            catch
            {
                lblStatus.Text = "Excel: lost connection";
                _excel = null;
            }
        }

        private void Excel_SheetSelectionChange(object Sh, Excel.Range Target)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    RefreshSelectionList();
                    UpdateStatus();
                }));
            }
            else
            {
                RefreshSelectionList();
                UpdateStatus();
            }
        }

        private void RefreshSelectionList()
        {
            lstSelection.Items.Clear();

            if (_excel == null) return;

            try
            {
                var sel = _excel.Selection as Excel.Range;
                if (sel == null)
                {
                    lstSelection.Items.Add("(No selection)");
                    return;
                }

                foreach (Excel.Range area in sel.Areas)
                {
                    foreach (Excel.Range cell in area.Cells)
                    {
                        lstSelection.Items.Add(cell.Address[false, false]);
                        Marshal.ReleaseComObject(cell);
                    }
                    Marshal.ReleaseComObject(area);
                }
                Marshal.ReleaseComObject(sel);
            }
            catch
            {
                lstSelection.Items.Add("(Cannot read selection)");
            }
        }

        private List<double> ExtractNumbersFromConvertedData(string text)
        {
            var nums = new List<double>();
            if (string.IsNullOrWhiteSpace(text)) return nums;

            foreach (Match m in NumberAfterColon.Matches(text))
            {
                var s = m.Groups[1].Value.Trim();
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
                    nums.Add(d);
            }
            return nums;
        }

        private void SendValueToExcel()
        {
            if (_excel == null) { lblStatus.Text = "Excel is not attached."; return; }

            // Read the textbox (rename here if your control name differs)
            string raw = ConvertedData.Text;
            var values = ExtractNumbersFromConvertedData(raw);
            if (values.Count == 0) { lblStatus.Text = "No numbers found after ':'."; return; }

            Excel.Range sel = null;
            try { sel = _excel.Selection as Excel.Range; } catch { }
            if (sel == null) { lblStatus.Text = "Select a cell/range in Excel first."; return; }

            int written = 0;
            int want = values.Count;

            try
            {
                foreach (Excel.Range area in sel.Areas)
                {
                    long areaCount;
                    try { areaCount = (long)area.CountLarge; } catch { areaCount = area.Count; }

                    int remaining = want - written;
                    if (remaining <= 0) break;

                    // Try fast block write if we can fill the whole area
                    if (areaCount <= int.MaxValue && areaCount <= remaining && area.Rows.Count > 0 && area.Columns.Count > 0)
                    {
                        int rows = area.Rows.Count;
                        int cols = area.Columns.Count;
                        var block = new object[rows, cols];
                        int idx = 0;
                        for (int r = 0; r < rows; r++)
                        {
                            for (int c = 0; c < cols; c++)
                            {
                                if (idx >= remaining) break;
                                block[r, c] = values[written + idx];
                                idx++;
                            }
                        }

                        if (idx == rows * cols)
                        {
                            area.Value2 = block;
                            written += idx;
                            continue;
                        }
                    }

                    // Fallback: cell-by-cell row-major
                    foreach (Excel.Range cell in area.Cells)
                    {
                        if (written >= want) break;
                        cell.Value2 = values[written];
                        written++;
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Write error: " + ex.Message;
                return;
            }

            lblStatus.Text = written == 0
                ? "Selection has no writable cells."
                : written < want
                    ? $"Wrote {written}/{want} values (selection too small)."
                    : $"Wrote {written} values to Excel.";
        }

        internal static class NativeMethods
        {
            [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
            internal static extern int CLSIDFromProgID(string lpszProgID, out Guid pclsid);

            [DllImport("oleaut32.dll", PreserveSig = false)]
            private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved,
                [MarshalAs(UnmanagedType.IUnknown)] out object ppunk);

            internal static object GetActiveObjectNoMarshal(Guid clsid)
            {
                GetActiveObject(ref clsid, IntPtr.Zero, out object obj);
                return obj;
            }
        }

        private void AttachExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (NativeMethods.CLSIDFromProgID("Excel.Application", out Guid clsid) != 0)
                    throw new COMException("CLSIDFromProgID failed");

                object excelObj = NativeMethods.GetActiveObjectNoMarshal(clsid);
                if (excelObj == null)
                    throw new COMException("Excel is not running");

                _excel = (Excel.Application)excelObj;
                _excel.SheetSelectionChange += Excel_SheetSelectionChange;

                lblStatus.Text = "Excel: attached (" + _excel.Version + ")";
                RefreshSelectionList();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Excel: not attached (" + ex.Message + ")";
            }
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
}
