namespace InstallPrinters
{
    partial class PrintersInSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintersInSystem));
            this.lvPrinters = new System.Windows.Forms.ListView();
            this.PrinterName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrinterDriver = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrinterIp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuForCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuForCopy.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvPrinters
            // 
            this.lvPrinters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PrinterName,
            this.PrinterDriver,
            this.PrinterIp});
            this.lvPrinters.ContextMenuStrip = this.contextMenuForCopy;
            this.lvPrinters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPrinters.FullRowSelect = true;
            this.lvPrinters.GridLines = true;
            this.lvPrinters.HideSelection = false;
            this.lvPrinters.Location = new System.Drawing.Point(0, 0);
            this.lvPrinters.Name = "lvPrinters";
            this.lvPrinters.Size = new System.Drawing.Size(698, 476);
            this.lvPrinters.TabIndex = 1;
            this.lvPrinters.UseCompatibleStateImageBehavior = false;
            this.lvPrinters.View = System.Windows.Forms.View.Details;
            this.lvPrinters.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvPrinters_KeyUp);
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
            // contextMenuForCopy
            // 
            this.contextMenuForCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuSelectAll,
            this.toolStripMenuCopy,
            this.toolStripMenuDelete});
            this.contextMenuForCopy.Name = "contextMenuForCopy";
            this.contextMenuForCopy.Size = new System.Drawing.Size(310, 92);
            this.contextMenuForCopy.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuForCopy_ItemClicked);
            // 
            // toolStripMenuSelectAll
            // 
            this.toolStripMenuSelectAll.Name = "toolStripMenuSelectAll";
            this.toolStripMenuSelectAll.Size = new System.Drawing.Size(309, 22);
            this.toolStripMenuSelectAll.Text = "Выбрать все";
            // 
            // toolStripMenuCopy
            // 
            this.toolStripMenuCopy.Name = "toolStripMenuCopy";
            this.toolStripMenuCopy.Size = new System.Drawing.Size(309, 22);
            this.toolStripMenuCopy.Text = "Копировать выделенное";
            // 
            // toolStripMenuDelete
            // 
            this.toolStripMenuDelete.Name = "toolStripMenuDelete";
            this.toolStripMenuDelete.Size = new System.Drawing.Size(309, 22);
            this.toolStripMenuDelete.Text = "Удалить выбранные принтеры из системы";
            // 
            // PrintersInSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 476);
            this.Controls.Add(this.lvPrinters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrintersInSystem";
            this.Text = "Установленные принтеры";
            this.Load += new System.EventHandler(this.PrintersInSystem_Load);
            this.contextMenuForCopy.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvPrinters;
        private System.Windows.Forms.ColumnHeader PrinterName;
        private System.Windows.Forms.ColumnHeader PrinterDriver;
        private System.Windows.Forms.ColumnHeader PrinterIp;
        private System.Windows.Forms.ContextMenuStrip contextMenuForCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSelectAll;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuDelete;
    }
}