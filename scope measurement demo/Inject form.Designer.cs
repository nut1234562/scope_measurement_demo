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
            InjectShowtb = new TextBox();
            Inject = new Button();
            Serial2Connect = new Button();
            Serial2cb = new ComboBox();
            disconnect = new Button();
            InjectOrdertb = new TextBox();
            Clearbt2 = new Button();
            DUpdown = new NumericUpDown();
            L1L2Updown = new NumericUpDown();
            IAUpdown = new NumericUpDown();
            RUpdown = new NumericUpDown();
            LUpdown = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)DUpdown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)L1L2Updown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IAUpdown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RUpdown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LUpdown).BeginInit();
            SuspendLayout();
            // 
            // InjectShowtb
            // 
            InjectShowtb.Location = new Point(12, 44);
            InjectShowtb.Multiline = true;
            InjectShowtb.Name = "InjectShowtb";
            InjectShowtb.ScrollBars = ScrollBars.Vertical;
            InjectShowtb.Size = new Size(338, 377);
            InjectShowtb.TabIndex = 1;
            // 
            // Inject
            // 
            Inject.Location = new Point(452, 197);
            Inject.Name = "Inject";
            Inject.Size = new Size(75, 23);
            Inject.TabIndex = 2;
            Inject.Text = "Inject";
            Inject.UseVisualStyleBackColor = true;
            Inject.Click += Inject_Click;
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
            // disconnect
            // 
            disconnect.BackColor = Color.Red;
            disconnect.ForeColor = Color.Red;
            disconnect.Location = new Point(370, 380);
            disconnect.Name = "disconnect";
            disconnect.Size = new Size(75, 23);
            disconnect.TabIndex = 7;
            disconnect.Text = "button2";
            disconnect.UseVisualStyleBackColor = false;
            disconnect.Click += disconnect_Click;
            // 
            // InjectOrdertb
            // 
            InjectOrdertb.Location = new Point(606, 363);
            InjectOrdertb.Multiline = true;
            InjectOrdertb.Name = "InjectOrdertb";
            InjectOrdertb.Size = new Size(137, 23);
            InjectOrdertb.TabIndex = 8;
            // 
            // Clearbt2
            // 
            Clearbt2.Location = new Point(452, 250);
            Clearbt2.Name = "Clearbt2";
            Clearbt2.Size = new Size(75, 23);
            Clearbt2.TabIndex = 9;
            Clearbt2.Text = "Clear";
            Clearbt2.UseVisualStyleBackColor = true;
            Clearbt2.Click += Clearbt2_Click;
            // 
            // DUpdown
            // 
            DUpdown.Location = new Point(470, 116);
            DUpdown.Name = "DUpdown";
            DUpdown.Size = new Size(39, 23);
            DUpdown.TabIndex = 10;
            // 
            // L1L2Updown
            // 
            L1L2Updown.Location = new Point(563, 180);
            L1L2Updown.Name = "L1L2Updown";
            L1L2Updown.Size = new Size(39, 23);
            L1L2Updown.TabIndex = 10;
            // 
            // IAUpdown
            // 
            IAUpdown.Location = new Point(563, 217);
            IAUpdown.Name = "IAUpdown";
            IAUpdown.Size = new Size(39, 23);
            IAUpdown.TabIndex = 10;
            // 
            // RUpdown
            // 
            RUpdown.Location = new Point(393, 220);
            RUpdown.Name = "RUpdown";
            RUpdown.Size = new Size(39, 23);
            RUpdown.TabIndex = 10;
            // 
            // LUpdown
            // 
            LUpdown.Location = new Point(393, 174);
            LUpdown.Name = "LUpdown";
            LUpdown.Size = new Size(39, 23);
            LUpdown.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(482, 142);
            label1.Name = "label1";
            label1.Size = new Size(15, 15);
            label1.TabIndex = 11;
            label1.Text = "D";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(438, 176);
            label2.Name = "label2";
            label2.Size = new Size(13, 15);
            label2.TabIndex = 11;
            label2.Text = "L";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(438, 225);
            label3.Name = "label3";
            label3.Size = new Size(14, 15);
            label3.TabIndex = 11;
            label3.Text = "R";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(526, 182);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 11;
            label4.Text = "L1L2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(526, 219);
            label5.Name = "label5";
            label5.Size = new Size(18, 15);
            label5.TabIndex = 11;
            label5.Text = "IA";
            // 
            // Inject_form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LUpdown);
            Controls.Add(RUpdown);
            Controls.Add(IAUpdown);
            Controls.Add(L1L2Updown);
            Controls.Add(DUpdown);
            Controls.Add(Clearbt2);
            Controls.Add(InjectOrdertb);
            Controls.Add(disconnect);
            Controls.Add(Serial2cb);
            Controls.Add(Serial2Connect);
            Controls.Add(Inject);
            Controls.Add(InjectShowtb);
            Name = "Inject_form";
            Text = "Inject_form";
            FormClosing += Inject_form_FormClosing;
            ((System.ComponentModel.ISupportInitialize)DUpdown).EndInit();
            ((System.ComponentModel.ISupportInitialize)L1L2Updown).EndInit();
            ((System.ComponentModel.ISupportInitialize)IAUpdown).EndInit();
            ((System.ComponentModel.ISupportInitialize)RUpdown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LUpdown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox InjectShowtb;
        private Button Inject;
        private Button Serial2Connect;
        private ComboBox Serial2cb;
        private Button disconnect;
        private TextBox InjectOrdertb;
        private Button Clearbt2;
        private NumericUpDown DUpdown;
        private NumericUpDown L1L2Updown;
        private NumericUpDown IAUpdown;
        private NumericUpDown RUpdown;
        private NumericUpDown LUpdown;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}