namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.TestRFPrint = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.txtPrinterName = new System.Windows.Forms.TextBox();
            this.txtPrinterPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrinterDriver = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "TestReport";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(176, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(420, 180);
            this.textBox1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TestRFPrint
            // 
            this.TestRFPrint.Location = new System.Drawing.Point(602, 7);
            this.TestRFPrint.Name = "TestRFPrint";
            this.TestRFPrint.Size = new System.Drawing.Size(115, 30);
            this.TestRFPrint.TabIndex = 4;
            this.TestRFPrint.Text = "StartRFPrint";
            this.TestRFPrint.UseVisualStyleBackColor = true;
            this.TestRFPrint.Click += new System.EventHandler(this.StartRFPrint_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(602, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 30);
            this.button3.TabIndex = 5;
            this.button3.Text = "StopRFPrint";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(608, 112);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 31);
            this.button4.TabIndex = 6;
            this.button4.Text = "writelog";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(613, 168);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(103, 24);
            this.button5.TabIndex = 7;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(53, 276);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(172, 25);
            this.button6.TabIndex = 8;
            this.button6.Text = "Установка принтера";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // txtPrinterName
            // 
            this.txtPrinterName.Location = new System.Drawing.Point(53, 250);
            this.txtPrinterName.Name = "txtPrinterName";
            this.txtPrinterName.Size = new System.Drawing.Size(163, 20);
            this.txtPrinterName.TabIndex = 9;
            // 
            // txtPrinterPort
            // 
            this.txtPrinterPort.Location = new System.Drawing.Point(517, 250);
            this.txtPrinterPort.Name = "txtPrinterPort";
            this.txtPrinterPort.Size = new System.Drawing.Size(180, 20);
            this.txtPrinterPort.TabIndex = 10;
            this.txtPrinterPort.Text = "172.16.250.52";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Имя принтера";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(514, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Порт принтера";
            // 
            // txtPrinterDriver
            // 
            this.txtPrinterDriver.Location = new System.Drawing.Point(283, 250);
            this.txtPrinterDriver.Name = "txtPrinterDriver";
            this.txtPrinterDriver.Size = new System.Drawing.Size(163, 20);
            this.txtPrinterDriver.TabIndex = 9;
            this.txtPrinterDriver.Text = "HP LaserJet 400 M401 PCL 6";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Драйвер принтера";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 82);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(147, 33);
            this.button7.TabIndex = 13;
            this.button7.Text = "construct";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 141);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(147, 28);
            this.button8.TabIndex = 14;
            this.button8.Text = "VisibleParams";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(12, 328);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(147, 28);
            this.button9.TabIndex = 15;
            this.button9.Text = "manageReports";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 368);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPrinterDriver);
            this.Controls.Add(this.txtPrinterPort);
            this.Controls.Add(this.txtPrinterName);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.TestRFPrint);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button TestRFPrint;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtPrinterName;
        private System.Windows.Forms.TextBox txtPrinterPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrinterDriver;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;



    }
}

