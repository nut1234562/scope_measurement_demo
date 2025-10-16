namespace scope_measurement_demo
{
    partial class Inject_form
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
            DinjectCK = new CheckBox();
            LinjectCK = new CheckBox();
            RinjectCK = new CheckBox();
            IAinjectCK = new CheckBox();
            L1L2injectCK = new CheckBox();
            InjectShowtb = new TextBox();
            Inject = new Button();
            DUpDown = new DomainUpDown();
            LUpDown = new DomainUpDown();
            RUpDown = new DomainUpDown();
            L12UpDown = new DomainUpDown();
            IAUpDown = new DomainUpDown();
            Serial2Connect = new Button();
            Serial2cb = new ComboBox();
            button2 = new Button();
            SuspendLayout();
            // 
            // DinjectCK
            // 
            DinjectCK.AutoSize = true;
            DinjectCK.Location = new Point(484, 145);
            DinjectCK.Name = "DinjectCK";
            DinjectCK.Size = new Size(34, 19);
            DinjectCK.TabIndex = 0;
            DinjectCK.Text = "D";
            DinjectCK.UseVisualStyleBackColor = true;
            // 
            // LinjectCK
            // 
            LinjectCK.AutoSize = true;
            LinjectCK.Location = new Point(438, 178);
            LinjectCK.Name = "LinjectCK";
            LinjectCK.Size = new Size(32, 19);
            LinjectCK.TabIndex = 0;
            LinjectCK.Text = "L";
            LinjectCK.UseVisualStyleBackColor = true;
            // 
            // RinjectCK
            // 
            RinjectCK.AutoSize = true;
            RinjectCK.Location = new Point(438, 221);
            RinjectCK.Name = "RinjectCK";
            RinjectCK.Size = new Size(33, 19);
            RinjectCK.TabIndex = 0;
            RinjectCK.Text = "R";
            RinjectCK.UseVisualStyleBackColor = true;
            // 
            // IAinjectCK
            // 
            IAinjectCK.AutoSize = true;
            IAinjectCK.Location = new Point(526, 221);
            IAinjectCK.Name = "IAinjectCK";
            IAinjectCK.Size = new Size(37, 19);
            IAinjectCK.TabIndex = 0;
            IAinjectCK.Text = "IA";
            IAinjectCK.UseVisualStyleBackColor = true;
            // 
            // L1L2injectCK
            // 
            L1L2injectCK.AutoSize = true;
            L1L2injectCK.Location = new Point(526, 181);
            L1L2injectCK.Name = "L1L2injectCK";
            L1L2injectCK.Size = new Size(53, 19);
            L1L2injectCK.TabIndex = 0;
            L1L2injectCK.Text = "L1 L2";
            L1L2injectCK.UseVisualStyleBackColor = true;
            // 
            // InjectShowtb
            // 
            InjectShowtb.Location = new Point(12, 44);
            InjectShowtb.Multiline = true;
            InjectShowtb.Name = "InjectShowtb";
            InjectShowtb.Size = new Size(338, 377);
            InjectShowtb.TabIndex = 1;
            // 
            // Inject
            // 
            Inject.Location = new Point(459, 268);
            Inject.Name = "Inject";
            Inject.Size = new Size(75, 23);
            Inject.TabIndex = 2;
            Inject.Text = "Inject";
            Inject.UseVisualStyleBackColor = true;
            Inject.Click += Inject_Click;
            // 
            // DUpDown
            // 
            DUpDown.Items.Add("0");
            DUpDown.Items.Add("1");
            DUpDown.Items.Add("2");
            DUpDown.Items.Add("3");
            DUpDown.Items.Add("4");
            DUpDown.Items.Add("5");
            DUpDown.Items.Add("6");
            DUpDown.Location = new Point(468, 107);
            DUpDown.Name = "DUpDown";
            DUpDown.Size = new Size(76, 23);
            DUpDown.TabIndex = 3;
            DUpDown.Text = "DUpDown";
            DUpDown.SelectedItemChanged += domainUpDown1_SelectedItemChanged;
            // 
            // LUpDown
            // 
            LUpDown.Items.Add("0");
            LUpDown.Items.Add("1");
            LUpDown.Items.Add("2");
            LUpDown.Items.Add("3");
            LUpDown.Items.Add("4");
            LUpDown.Items.Add("5");
            LUpDown.Items.Add("6");
            LUpDown.Location = new Point(356, 177);
            LUpDown.Name = "LUpDown";
            LUpDown.Size = new Size(76, 23);
            LUpDown.TabIndex = 3;
            LUpDown.Text = "LUpDown";
            LUpDown.SelectedItemChanged += domainUpDown1_SelectedItemChanged;
            // 
            // RUpDown
            // 
            RUpDown.Items.Add("0");
            RUpDown.Items.Add("1");
            RUpDown.Items.Add("2");
            RUpDown.Items.Add("3");
            RUpDown.Items.Add("4");
            RUpDown.Items.Add("5");
            RUpDown.Items.Add("6");
            RUpDown.Location = new Point(356, 217);
            RUpDown.Name = "RUpDown";
            RUpDown.Size = new Size(76, 23);
            RUpDown.TabIndex = 3;
            RUpDown.Text = "RUpDown";
            RUpDown.SelectedItemChanged += domainUpDown1_SelectedItemChanged;
            // 
            // L12UpDown
            // 
            L12UpDown.Items.Add("0");
            L12UpDown.Items.Add("1");
            L12UpDown.Items.Add("2");
            L12UpDown.Items.Add("3");
            L12UpDown.Items.Add("4");
            L12UpDown.Items.Add("5");
            L12UpDown.Items.Add("6");
            L12UpDown.Location = new Point(582, 180);
            L12UpDown.Name = "L12UpDown";
            L12UpDown.Size = new Size(76, 23);
            L12UpDown.TabIndex = 3;
            L12UpDown.Text = "L1 L2 UpDown";
            L12UpDown.SelectedItemChanged += domainUpDown1_SelectedItemChanged;
            // 
            // IAUpDown
            // 
            IAUpDown.Items.Add("0");
            IAUpDown.Items.Add("1");
            IAUpDown.Items.Add("2");
            IAUpDown.Items.Add("3");
            IAUpDown.Items.Add("4");
            IAUpDown.Items.Add("5");
            IAUpDown.Items.Add("6");
            IAUpDown.Location = new Point(582, 220);
            IAUpDown.Name = "IAUpDown";
            IAUpDown.Size = new Size(76, 23);
            IAUpDown.TabIndex = 3;
            IAUpDown.Text = "IAUpDown";
            IAUpDown.SelectedItemChanged += domainUpDown1_SelectedItemChanged;
            // 
            // Serial2Connect
            // 
            Serial2Connect.BackColor = Color.Lime;
            Serial2Connect.ForeColor = Color.Lime;
            Serial2Connect.Location = new Point(459, 380);
            Serial2Connect.Name = "Serial2Connect";
            Serial2Connect.Size = new Size(75, 23);
            Serial2Connect.TabIndex = 4;
            Serial2Connect.Text = "Connect";
            Serial2Connect.UseVisualStyleBackColor = false;
            Serial2Connect.Click += Serial2Connect_Click;
            // 
            // Serial2cb
            // 
            Serial2cb.FormattingEnabled = true;
            Serial2cb.Location = new Point(370, 348);
            Serial2cb.Name = "Serial2cb";
            Serial2cb.Size = new Size(121, 23);
            Serial2cb.TabIndex = 5;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.ForeColor = Color.Red;
            button2.Location = new Point(370, 380);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = false;
            // 
            // Inject_form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(Serial2cb);
            Controls.Add(Serial2Connect);
            Controls.Add(IAUpDown);
            Controls.Add(L12UpDown);
            Controls.Add(RUpDown);
            Controls.Add(LUpDown);
            Controls.Add(DUpDown);
            Controls.Add(Inject);
            Controls.Add(InjectShowtb);
            Controls.Add(L1L2injectCK);
            Controls.Add(IAinjectCK);
            Controls.Add(RinjectCK);
            Controls.Add(LinjectCK);
            Controls.Add(DinjectCK);
            Name = "Inject_form";
            Text = "Inject_form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox DinjectCK;
        private CheckBox LinjectCK;
        private CheckBox RinjectCK;
        private CheckBox IAinjectCK;
        private CheckBox L1L2injectCK;
        private TextBox InjectShowtb;
        private Button Inject;
        private DomainUpDown DUpDown;
        private DomainUpDown LUpDown;
        private DomainUpDown RUpDown;
        private DomainUpDown L12UpDown;
        private DomainUpDown IAUpDown;
        private Button Serial2Connect;
        private ComboBox Serial2cb;
        private Button button2;
    }
}