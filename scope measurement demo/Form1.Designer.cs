namespace scope_measurement_demo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ReceivedData = new TextBox();
            ConvertedData = new TextBox();
            multiplier = new ComboBox();
            decimaal = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            Statelb = new Label();
            debugtextbox2 = new TextBox();
            debugtextbox = new TextBox();
            Itemcb = new ComboBox();
            Modelcb = new ComboBox();
            testbt = new Button();
            button2 = new Button();
            tabPage2 = new TabPage();
            lblConnectionStatus = new Label();
            Textfromserial = new TextBox();
            cbparitybit = new ComboBox();
            cbstopbit = new ComboBox();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            Refresh = new Button();
            Disconnect = new Button();
            Connect = new Button();
            cbdatabit = new ComboBox();
            cbbaudrate = new ComboBox();
            cbport = new ComboBox();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            Dcheckbox = new CheckBox();
            Lcheckbox = new CheckBox();
            Rcheckbox = new CheckBox();
            IAcheckbox = new CheckBox();
            L1L2checkbox = new CheckBox();
            Injectwindowckb = new CheckBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // ReceivedData
            // 
            ReceivedData.BackColor = SystemColors.Window;
            ReceivedData.Location = new Point(57, 30);
            ReceivedData.Margin = new Padding(3, 2, 3, 2);
            ReceivedData.Multiline = true;
            ReceivedData.Name = "ReceivedData";
            ReceivedData.Size = new Size(218, 674);
            ReceivedData.TabIndex = 0;
            ReceivedData.TextChanged += textBox1_TextChanged;
            // 
            // ConvertedData
            // 
            ConvertedData.Location = new Point(320, 68);
            ConvertedData.Margin = new Padding(3, 2, 3, 2);
            ConvertedData.Multiline = true;
            ConvertedData.Name = "ConvertedData";
            ConvertedData.Size = new Size(453, 102);
            ConvertedData.TabIndex = 1;
            // 
            // multiplier
            // 
            multiplier.FormattingEnabled = true;
            multiplier.Items.AddRange(new object[] { "x1", "x10" });
            multiplier.Location = new Point(368, 309);
            multiplier.Margin = new Padding(3, 2, 3, 2);
            multiplier.Name = "multiplier";
            multiplier.Size = new Size(47, 23);
            multiplier.TabIndex = 3;
            // 
            // decimaal
            // 
            decimaal.FormattingEnabled = true;
            decimaal.Items.AddRange(new object[] { "0.1", "0.01", "0.001", "0.0001", "0.00001", "0.000001" });
            decimaal.Location = new Point(346, 350);
            decimaal.Margin = new Padding(3, 2, 3, 2);
            decimaal.Name = "decimaal";
            decimaal.Size = new Size(68, 23);
            decimaal.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(419, 311);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 6;
            label1.Text = "Multiplier";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(419, 352);
            label2.Name = "label2";
            label2.Size = new Size(86, 15);
            label2.TabIndex = 7;
            label2.Text = "Decimal places";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(419, 390);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(57, 13);
            label4.Name = "label4";
            label4.Size = new Size(88, 15);
            label4.TabIndex = 9;
            label4.Text = "Input Message";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(320, 50);
            label5.Name = "label5";
            label5.Size = new Size(98, 15);
            label5.TabIndex = 10;
            label5.Text = "Output Message";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(41, 31);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1244, 771);
            tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(Injectwindowckb);
            tabPage1.Controls.Add(L1L2checkbox);
            tabPage1.Controls.Add(IAcheckbox);
            tabPage1.Controls.Add(Rcheckbox);
            tabPage1.Controls.Add(Lcheckbox);
            tabPage1.Controls.Add(Dcheckbox);
            tabPage1.Controls.Add(Statelb);
            tabPage1.Controls.Add(debugtextbox2);
            tabPage1.Controls.Add(debugtextbox);
            tabPage1.Controls.Add(Itemcb);
            tabPage1.Controls.Add(Modelcb);
            tabPage1.Controls.Add(testbt);
            tabPage1.Controls.Add(button2);
            tabPage1.Controls.Add(ConvertedData);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(ReceivedData);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(multiplier);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(decimaal);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(3, 2, 3, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3, 2, 3, 2);
            tabPage1.Size = new Size(1236, 743);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Printing";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // Statelb
            // 
            Statelb.AutoSize = true;
            Statelb.Location = new Point(740, 50);
            Statelb.Name = "Statelb";
            Statelb.Size = new Size(33, 15);
            Statelb.TabIndex = 18;
            Statelb.Text = "State";
            // 
            // debugtextbox2
            // 
            debugtextbox2.Location = new Point(771, 203);
            debugtextbox2.Multiline = true;
            debugtextbox2.Name = "debugtextbox2";
            debugtextbox2.Size = new Size(445, 535);
            debugtextbox2.TabIndex = 17;
            // 
            // debugtextbox
            // 
            debugtextbox.Location = new Point(292, 390);
            debugtextbox.Multiline = true;
            debugtextbox.Name = "debugtextbox";
            debugtextbox.Size = new Size(444, 298);
            debugtextbox.TabIndex = 16;
            // 
            // Itemcb
            // 
            Itemcb.FormattingEnabled = true;
            Itemcb.Location = new Point(964, 59);
            Itemcb.Margin = new Padding(3, 2, 3, 2);
            Itemcb.Name = "Itemcb";
            Itemcb.Size = new Size(133, 23);
            Itemcb.TabIndex = 15;
            // 
            // Modelcb
            // 
            Modelcb.FormattingEnabled = true;
            Modelcb.Items.AddRange(new object[] { "1st gear", "VHB30", "Shaft", "Lead screw", "14RA-20T-3-18-12-PPS-S", "14RA-PM20TF80-2", "FG-Magnet", "PM25S", "SE-SERIE", "LED-EXAIL-FAN", "FAN Motor L09", "BLOM70" });
            Modelcb.Location = new Point(794, 59);
            Modelcb.Margin = new Padding(3, 2, 3, 2);
            Modelcb.Name = "Modelcb";
            Modelcb.Size = new Size(133, 23);
            Modelcb.TabIndex = 14;
            Modelcb.SelectedIndexChanged += Modelcb_SelectedIndexChanged;
            // 
            // testbt
            // 
            testbt.Location = new Point(443, 251);
            testbt.Margin = new Padding(3, 2, 3, 2);
            testbt.Name = "testbt";
            testbt.Size = new Size(82, 22);
            testbt.TabIndex = 13;
            testbt.Text = "clear";
            testbt.UseVisualStyleBackColor = true;
            testbt.Click += testbt_Click;
            // 
            // button2
            // 
            button2.Location = new Point(346, 251);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(82, 22);
            button2.TabIndex = 12;
            button2.Text = "Print";
            button2.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(lblConnectionStatus);
            tabPage2.Controls.Add(Textfromserial);
            tabPage2.Controls.Add(cbparitybit);
            tabPage2.Controls.Add(cbstopbit);
            tabPage2.Controls.Add(label11);
            tabPage2.Controls.Add(label12);
            tabPage2.Controls.Add(label13);
            tabPage2.Controls.Add(Refresh);
            tabPage2.Controls.Add(Disconnect);
            tabPage2.Controls.Add(Connect);
            tabPage2.Controls.Add(cbdatabit);
            tabPage2.Controls.Add(cbbaudrate);
            tabPage2.Controls.Add(cbport);
            tabPage2.Controls.Add(label10);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label6);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(3, 2, 3, 2);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 2, 3, 2);
            tabPage2.Size = new Size(1236, 743);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Port Settings";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.BackColor = Color.White;
            lblConnectionStatus.BorderStyle = BorderStyle.Fixed3D;
            lblConnectionStatus.ForeColor = Color.Red;
            lblConnectionStatus.Location = new Point(332, 124);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(88, 17);
            lblConnectionStatus.TabIndex = 17;
            lblConnectionStatus.Text = "Not connected";
            lblConnectionStatus.Click += lblConnectionStatus_Click;
            // 
            // Textfromserial
            // 
            Textfromserial.Location = new Point(594, 84);
            Textfromserial.Multiline = true;
            Textfromserial.Name = "Textfromserial";
            Textfromserial.Size = new Size(410, 505);
            Textfromserial.TabIndex = 16;
            // 
            // cbparitybit
            // 
            cbparitybit.AllowDrop = true;
            cbparitybit.FormattingEnabled = true;
            cbparitybit.Items.AddRange(new object[] { "None", "Even", "Odd", "Mark", "Space" });
            cbparitybit.Location = new Point(146, 262);
            cbparitybit.Margin = new Padding(3, 2, 3, 2);
            cbparitybit.Name = "cbparitybit";
            cbparitybit.Size = new Size(133, 23);
            cbparitybit.TabIndex = 15;
            // 
            // cbstopbit
            // 
            cbstopbit.FormattingEnabled = true;
            cbstopbit.Items.AddRange(new object[] { "1", "2" });
            cbstopbit.Location = new Point(146, 214);
            cbstopbit.Margin = new Padding(3, 2, 3, 2);
            cbstopbit.Name = "cbstopbit";
            cbstopbit.Size = new Size(133, 23);
            cbstopbit.TabIndex = 14;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.Location = new Point(49, 251);
            label11.Name = "label11";
            label11.Size = new Size(0, 19);
            label11.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.Location = new Point(49, 262);
            label12.Name = "label12";
            label12.Size = new Size(64, 19);
            label12.TabIndex = 12;
            label12.Text = "Parity Bit";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label13.Location = new Point(49, 214);
            label13.Name = "label13";
            label13.Size = new Size(57, 19);
            label13.TabIndex = 11;
            label13.Text = "Stop Bit";
            // 
            // Refresh
            // 
            Refresh.Location = new Point(321, 74);
            Refresh.Margin = new Padding(3, 2, 3, 2);
            Refresh.Name = "Refresh";
            Refresh.Size = new Size(82, 22);
            Refresh.TabIndex = 10;
            Refresh.Text = "Refresh";
            Refresh.UseVisualStyleBackColor = true;
            Refresh.Click += Refresh_Click;
            // 
            // Disconnect
            // 
            Disconnect.Location = new Point(225, 309);
            Disconnect.Margin = new Padding(3, 2, 3, 2);
            Disconnect.Name = "Disconnect";
            Disconnect.Size = new Size(82, 22);
            Disconnect.TabIndex = 9;
            Disconnect.Text = "Disconnect";
            Disconnect.UseVisualStyleBackColor = true;
            Disconnect.Click += Disconnect_Click;
            // 
            // Connect
            // 
            Connect.Location = new Point(49, 309);
            Connect.Margin = new Padding(3, 2, 3, 2);
            Connect.Name = "Connect";
            Connect.Size = new Size(82, 22);
            Connect.TabIndex = 8;
            Connect.Text = "Connect";
            Connect.UseVisualStyleBackColor = true;
            Connect.Click += Connect_Click;
            // 
            // cbdatabit
            // 
            cbdatabit.FormattingEnabled = true;
            cbdatabit.Items.AddRange(new object[] { "7", "8" });
            cbdatabit.Location = new Point(146, 173);
            cbdatabit.Margin = new Padding(3, 2, 3, 2);
            cbdatabit.Name = "cbdatabit";
            cbdatabit.Size = new Size(133, 23);
            cbdatabit.TabIndex = 7;
            // 
            // cbbaudrate
            // 
            cbbaudrate.FormattingEnabled = true;
            cbbaudrate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            cbbaudrate.Location = new Point(146, 124);
            cbbaudrate.Margin = new Padding(3, 2, 3, 2);
            cbbaudrate.Name = "cbbaudrate";
            cbbaudrate.Size = new Size(133, 23);
            cbbaudrate.TabIndex = 6;
            // 
            // cbport
            // 
            cbport.FormattingEnabled = true;
            cbport.Location = new Point(146, 78);
            cbport.Margin = new Padding(3, 2, 3, 2);
            cbport.Name = "cbport";
            cbport.Size = new Size(133, 23);
            cbport.TabIndex = 5;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(49, 162);
            label10.Name = "label10";
            label10.Size = new Size(0, 19);
            label10.TabIndex = 4;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(49, 173);
            label9.Name = "label9";
            label9.Size = new Size(58, 19);
            label9.TabIndex = 3;
            label9.Text = "Data Bit";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(49, 125);
            label8.Name = "label8";
            label8.Size = new Size(71, 19);
            label8.TabIndex = 2;
            label8.Text = "Baud Rate";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(49, 79);
            label7.Name = "label7";
            label7.Size = new Size(34, 19);
            label7.TabIndex = 1;
            label7.Text = "Port";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(49, 22);
            label6.Name = "label6";
            label6.Size = new Size(142, 20);
            label6.TabIndex = 0;
            label6.Text = "Serial Port Settings";
            // 
            // Dcheckbox
            // 
            Dcheckbox.AutoSize = true;
            Dcheckbox.Location = new Point(337, 187);
            Dcheckbox.Name = "Dcheckbox";
            Dcheckbox.Size = new Size(34, 19);
            Dcheckbox.TabIndex = 19;
            Dcheckbox.Text = "D";
            Dcheckbox.UseVisualStyleBackColor = true;
            // 
            // Lcheckbox
            // 
            Lcheckbox.AutoSize = true;
            Lcheckbox.Location = new Point(394, 187);
            Lcheckbox.Name = "Lcheckbox";
            Lcheckbox.Size = new Size(32, 19);
            Lcheckbox.TabIndex = 19;
            Lcheckbox.Text = "L";
            Lcheckbox.UseVisualStyleBackColor = true;
            // 
            // Rcheckbox
            // 
            Rcheckbox.AutoSize = true;
            Rcheckbox.Location = new Point(455, 187);
            Rcheckbox.Name = "Rcheckbox";
            Rcheckbox.Size = new Size(33, 19);
            Rcheckbox.TabIndex = 19;
            Rcheckbox.Text = "R";
            Rcheckbox.UseVisualStyleBackColor = true;
            // 
            // IAcheckbox
            // 
            IAcheckbox.AutoSize = true;
            IAcheckbox.Location = new Point(507, 187);
            IAcheckbox.Name = "IAcheckbox";
            IAcheckbox.Size = new Size(37, 19);
            IAcheckbox.TabIndex = 19;
            IAcheckbox.Text = "IA";
            IAcheckbox.UseVisualStyleBackColor = true;
            // 
            // L1L2checkbox
            // 
            L1L2checkbox.AutoSize = true;
            L1L2checkbox.Location = new Point(560, 187);
            L1L2checkbox.Name = "L1L2checkbox";
            L1L2checkbox.Size = new Size(53, 19);
            L1L2checkbox.TabIndex = 19;
            L1L2checkbox.Text = "L1 L2";
            L1L2checkbox.UseVisualStyleBackColor = true;
            // 
            // Injectwindowckb
            // 
            Injectwindowckb.AutoSize = true;
            Injectwindowckb.Location = new Point(594, 251);
            Injectwindowckb.Name = "Injectwindowckb";
            Injectwindowckb.Size = new Size(102, 19);
            Injectwindowckb.TabIndex = 20;
            Injectwindowckb.Text = "Inject Window";
            Injectwindowckb.UseVisualStyleBackColor = true;
            Injectwindowckb.CheckedChanged += Injectwindowckb_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1287, 813);
            Controls.Add(tabControl1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox ReceivedData;
        private TextBox ConvertedData;
        private ComboBox multiplier;
        private ComboBox decimaal;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label6;
        private Button Disconnect;
        private Button Connect;
        private ComboBox cbdatabit;
        private ComboBox cbbaudrate;
        private ComboBox cbport;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Button Refresh;
        private ComboBox cbparitybit;
        private ComboBox cbstopbit;
        private Label label11;
        private Label label12;
        private Label label13;
        private Button testbt;
        private Button button2;
        private ComboBox Modelcb;
        private ComboBox Itemcb;
        private TextBox debugtextbox;
        private TextBox debugtextbox2;
        private TextBox Textfromserial;
        private Label lblConnectionStatus;
        private Label Statelb;
        private CheckBox IAcheckbox;
        private CheckBox Rcheckbox;
        private CheckBox Lcheckbox;
        private CheckBox Dcheckbox;
        private CheckBox L1L2checkbox;
        private CheckBox Injectwindowckb;
    }
}
