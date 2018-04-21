namespace Reporting
{
    partial class frmReportItemProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportItemProperties));
            this.lvItemProperties = new System.Windows.Forms.ListView();
            this.NameOfProperty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Allow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Max = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Printed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Autocopy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuViewHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuViewHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuViewHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvItemProperties
            // 
            this.lvItemProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameOfProperty,
            this.Allow,
            this.Max,
            this.Printed,
            this.Autocopy});
            this.lvItemProperties.ContextMenuStrip = this.contextMenuViewHistory;
            this.lvItemProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvItemProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvItemProperties.FullRowSelect = true;
            this.lvItemProperties.GridLines = true;
            this.lvItemProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvItemProperties.Location = new System.Drawing.Point(0, 0);
            this.lvItemProperties.MultiSelect = false;
            this.lvItemProperties.Name = "lvItemProperties";
            this.lvItemProperties.Size = new System.Drawing.Size(673, 199);
            this.lvItemProperties.TabIndex = 10;
            this.lvItemProperties.UseCompatibleStateImageBehavior = false;
            this.lvItemProperties.View = System.Windows.Forms.View.Details;
            // 
            // NameOfProperty
            // 
            this.NameOfProperty.Text = "Тип документа";
            this.NameOfProperty.Width = 200;
            // 
            // Allow
            // 
            this.Allow.Text = "Доступно";
            this.Allow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Allow.Width = 110;
            // 
            // Max
            // 
            this.Max.Text = "Разрешено";
            this.Max.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Max.Width = 110;
            // 
            // Printed
            // 
            this.Printed.Text = "Использовано";
            this.Printed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Printed.Width = 110;
            // 
            // Autocopy
            // 
            this.Autocopy.Text = "Автомат. копия";
            this.Autocopy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Autocopy.Width = 112;
            // 
            // contextMenuViewHistory
            // 
            this.contextMenuViewHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuViewHistory});
            this.contextMenuViewHistory.Name = "contextMenuViewHistory";
            this.contextMenuViewHistory.Size = new System.Drawing.Size(163, 26);
            this.contextMenuViewHistory.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuViewHistory_ItemClicked);
            // 
            // toolStripMenuViewHistory
            // 
            this.toolStripMenuViewHistory.Name = "toolStripMenuViewHistory";
            this.toolStripMenuViewHistory.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuViewHistory.Text = "История печати";
            // 
            // frmReportItemProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 199);
            this.Controls.Add(this.lvItemProperties);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportItemProperties";
            this.Text = "Свойства отчета";
            this.Activated += new System.EventHandler(this.frmReportItemProperties_Activated);
            this.Load += new System.EventHandler(this.frmReportItemProperties_Load);
            this.contextMenuViewHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.ListView lvItemProperties;
        internal System.Windows.Forms.ColumnHeader NameOfProperty;
        internal System.Windows.Forms.ColumnHeader Allow;
        internal System.Windows.Forms.ColumnHeader Max;
        internal System.Windows.Forms.ColumnHeader Printed;
        internal System.Windows.Forms.ColumnHeader Autocopy;


        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuViewHistory;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuViewHistory;
    }
}