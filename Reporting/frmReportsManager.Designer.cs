namespace Reporting
{
    partial class frmReportsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportsManager));
            this.dgwReports = new System.Windows.Forms.DataGridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblDebug = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ReportName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPORTCAPTION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REPORTPATH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORIGINAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COPIES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORIGINALEXPORTPDF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COPIESEXPORTPDF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORIGINALEXPORTXLS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COPIESEXPORTXLS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AUTOCOPY = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Serialkey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EXECUTABLE = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ADDWHO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EDITWHO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EDITDATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgwReports)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgwReports
            // 
            this.dgwReports.AllowUserToAddRows = false;
            this.dgwReports.AllowUserToDeleteRows = false;
            this.dgwReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwReports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReportName,
            this.REPORTCAPTION,
            this.REPORTPATH,
            this.ORIGINAL,
            this.COPIES,
            this.ORIGINALEXPORTPDF,
            this.COPIESEXPORTPDF,
            this.ORIGINALEXPORTXLS,
            this.COPIESEXPORTXLS,
            this.AUTOCOPY,
            this.Serialkey,
            this.EXECUTABLE,
            this.ADDWHO,
            this.EDITWHO,
            this.ADDDATE,
            this.EDITDATE});
            this.dgwReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgwReports.Location = new System.Drawing.Point(0, 0);
            this.dgwReports.Name = "dgwReports";
            this.dgwReports.Size = new System.Drawing.Size(1113, 386);
            this.dgwReports.TabIndex = 0;
            this.dgwReports.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwReports_CellValueChanged);
            this.dgwReports.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgwReports_KeyPress);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblDebug);
            this.pnlBottom.Controls.Add(this.label1);
            this.pnlBottom.Controls.Add(this.Panel1);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 386);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1113, 37);
            this.pnlBottom.TabIndex = 1;
            // 
            // lblDebug
            // 
            this.lblDebug.AutoSize = true;
            this.lblDebug.Location = new System.Drawing.Point(29, 20);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(108, 13);
            this.lblDebug.TabIndex = 4;
            this.lblDebug.Text = "IsDebugMode = True";
            this.lblDebug.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "c#";
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.btnCancel);
            this.Panel1.Controls.Add(this.btnOk);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel1.Location = new System.Drawing.Point(874, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(239, 37);
            this.Panel1.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(125, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(107, 28);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgwReports);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1113, 386);
            this.panel2.TabIndex = 2;
            // 
            // ReportName
            // 
            this.ReportName.DataPropertyName = "REPORTNAME";
            this.ReportName.HeaderText = "Отчет";
            this.ReportName.Name = "ReportName";
            this.ReportName.ReadOnly = true;
            this.ReportName.Width = 130;
            // 
            // REPORTCAPTION
            // 
            this.REPORTCAPTION.DataPropertyName = "REPORTCAPTION";
            this.REPORTCAPTION.HeaderText = "Заголовок";
            this.REPORTCAPTION.Name = "REPORTCAPTION";
            this.REPORTCAPTION.ReadOnly = true;
            this.REPORTCAPTION.Width = 130;
            // 
            // REPORTPATH
            // 
            this.REPORTPATH.DataPropertyName = "REPORTPATH";
            this.REPORTPATH.HeaderText = "Путь к отчету";
            this.REPORTPATH.Name = "REPORTPATH";
            this.REPORTPATH.ReadOnly = true;
            this.REPORTPATH.Width = 200;
            // 
            // ORIGINAL
            // 
            this.ORIGINAL.DataPropertyName = "ORIGINAL";
            this.ORIGINAL.HeaderText = "Оригиналы";
            this.ORIGINAL.Name = "ORIGINAL";
            this.ORIGINAL.Width = 70;
            // 
            // COPIES
            // 
            this.COPIES.DataPropertyName = "COPIES";
            this.COPIES.HeaderText = "Копии";
            this.COPIES.Name = "COPIES";
            this.COPIES.Width = 70;
            // 
            // ORIGINALEXPORTPDF
            // 
            this.ORIGINALEXPORTPDF.DataPropertyName = "ORIGINALEXPORTPDF";
            this.ORIGINALEXPORTPDF.HeaderText = "Ориг. *PDF";
            this.ORIGINALEXPORTPDF.Name = "ORIGINALEXPORTPDF";
            // 
            // COPIESEXPORTPDF
            // 
            this.COPIESEXPORTPDF.DataPropertyName = "COPIESEXPORTPDF";
            this.COPIESEXPORTPDF.HeaderText = "Копии *PDF";
            this.COPIESEXPORTPDF.Name = "COPIESEXPORTPDF";
            // 
            // ORIGINALEXPORTXLS
            // 
            this.ORIGINALEXPORTXLS.DataPropertyName = "ORIGINALEXPORTXLS";
            this.ORIGINALEXPORTXLS.HeaderText = "Ориг. *XLS";
            this.ORIGINALEXPORTXLS.Name = "ORIGINALEXPORTXLS";
            // 
            // COPIESEXPORTXLS
            // 
            this.COPIESEXPORTXLS.DataPropertyName = "COPIESEXPORTXLS";
            this.COPIESEXPORTXLS.HeaderText = "Копии *XLS";
            this.COPIESEXPORTXLS.Name = "COPIESEXPORTXLS";
            // 
            // AUTOCOPY
            // 
            this.AUTOCOPY.DataPropertyName = "AUTOCOPY";
            this.AUTOCOPY.HeaderText = "Копии авт.";
            this.AUTOCOPY.Name = "AUTOCOPY";
            this.AUTOCOPY.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AUTOCOPY.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Serialkey
            // 
            this.Serialkey.DataPropertyName = "Serialkey";
            this.Serialkey.HeaderText = "Serialkey";
            this.Serialkey.Name = "Serialkey";
            this.Serialkey.Visible = false;
            // 
            // EXECUTABLE
            // 
            this.EXECUTABLE.DataPropertyName = "EXECUTABLE";
            this.EXECUTABLE.HeaderText = "Для выгрузки из Ghost";
            this.EXECUTABLE.Name = "EXECUTABLE";
            this.EXECUTABLE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.EXECUTABLE.Visible = false;
            this.EXECUTABLE.Width = 200;
            // 
            // ADDWHO
            // 
            this.ADDWHO.DataPropertyName = "ADDWHO";
            this.ADDWHO.HeaderText = "Кто добавил";
            this.ADDWHO.Name = "ADDWHO";
            this.ADDWHO.ReadOnly = true;
            // 
            // EDITWHO
            // 
            this.EDITWHO.DataPropertyName = "EDITWHO";
            this.EDITWHO.HeaderText = "Кто редактировал";
            this.EDITWHO.Name = "EDITWHO";
            this.EDITWHO.ReadOnly = true;
            this.EDITWHO.Width = 130;
            // 
            // ADDDATE
            // 
            this.ADDDATE.DataPropertyName = "ADDDATE";
            this.ADDDATE.HeaderText = "Добавлен";
            this.ADDDATE.Name = "ADDDATE";
            this.ADDDATE.ReadOnly = true;
            // 
            // EDITDATE
            // 
            this.EDITDATE.DataPropertyName = "EDITDATE";
            this.EDITDATE.HeaderText = "Редактирован";
            this.EDITDATE.Name = "EDITDATE";
            this.EDITDATE.ReadOnly = true;
            this.EDITDATE.Width = 110;
            // 
            // frmReportsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 423);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportsManager";
            this.Text = "Управление отчетами";
            ((System.ComponentModel.ISupportInitialize)(this.dgwReports)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.DataGridView dgwReports;
        internal System.Windows.Forms.Panel pnlBottom;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.Button btnCancel;


        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportName;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPORTCAPTION;
        private System.Windows.Forms.DataGridViewTextBoxColumn REPORTPATH;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORIGINAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn COPIES;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORIGINALEXPORTPDF;
        private System.Windows.Forms.DataGridViewTextBoxColumn COPIESEXPORTPDF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORIGINALEXPORTXLS;
        private System.Windows.Forms.DataGridViewTextBoxColumn COPIESEXPORTXLS;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AUTOCOPY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serialkey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn EXECUTABLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDWHO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EDITWHO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDDATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn EDITDATE;
    }
}