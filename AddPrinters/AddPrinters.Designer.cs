namespace InstallPrinters
{
    partial class AddPrinters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPrinters));
            this.lvPrinters = new System.Windows.Forms.ListView();
            this.PrinterName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrinterDriver = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrinterIp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLoadPrinters = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPrinters
            // 
            this.lvPrinters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PrinterName,
            this.PrinterDriver,
            this.PrinterIp});
            this.lvPrinters.ContextMenuStrip = this.contextMenuStrip1;
            this.lvPrinters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPrinters.FullRowSelect = true;
            this.lvPrinters.GridLines = true;
            this.lvPrinters.HideSelection = false;
            this.lvPrinters.Location = new System.Drawing.Point(0, 0);
            this.lvPrinters.MultiSelect = false;
            this.lvPrinters.Name = "lvPrinters";
            this.lvPrinters.Size = new System.Drawing.Size(706, 411);
            this.lvPrinters.TabIndex = 0;
            this.lvPrinters.UseCompatibleStateImageBehavior = false;
            this.lvPrinters.View = System.Windows.Forms.View.Details;
            // 
            // PrinterName
            // 
            this.PrinterName.Text = "Имя принтера";
            this.PrinterName.Width = 237;
            // 
            // PrinterDriver
            // 
            this.PrinterDriver.Text = "Название драйвера";
            this.PrinterDriver.Width = 243;
            // 
            // PrinterIp
            // 
            this.PrinterIp.Text = "IP адрес";
            this.PrinterIp.Width = 181;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 411);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(706, 50);
            this.panel1.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(3, 3);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(425, 47);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = "label1";
            this.lblInfo.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLoadPrinters);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(428, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(278, 50);
            this.panel3.TabIndex = 2;
            // 
            // btnLoadPrinters
            // 
            this.btnLoadPrinters.Location = new System.Drawing.Point(6, 13);
            this.btnLoadPrinters.Name = "btnLoadPrinters";
            this.btnLoadPrinters.Size = new System.Drawing.Size(130, 25);
            this.btnLoadPrinters.TabIndex = 3;
            this.btnLoadPrinters.Text = "Загрузить принтеры...";
            this.btnLoadPrinters.UseVisualStyleBackColor = true;
            this.btnLoadPrinters.Click += new System.EventHandler(this.btnLoadPrinters_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(142, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(130, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Создать принтеры";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvPrinters);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(706, 411);
            this.panel2.TabIndex = 2;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpen});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 48);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            this.contextMenuStrip1.Click += new System.EventHandler(this.contextMenuStrip1_Click);
            // 
            // toolStripOpen
            // 
            this.toolStripOpen.Name = "toolStripOpen";
            this.toolStripOpen.Size = new System.Drawing.Size(188, 22);
            this.toolStripOpen.Text = "Принтеры в системе";
            // 
            // AddPrinters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 461);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddPrinters";
            this.Text = "Пакетная установка принтеров";
            this.Load += new System.EventHandler(this.InstallPrinters_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPrinters;
        private System.Windows.Forms.ColumnHeader PrinterName;
        private System.Windows.Forms.ColumnHeader PrinterDriver;
        private System.Windows.Forms.ColumnHeader PrinterIp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLoadPrinters;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panel2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripOpen;
    }
}

