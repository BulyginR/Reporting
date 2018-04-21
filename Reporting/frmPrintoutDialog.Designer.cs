namespace Reporting
{
    partial class frmPrintoutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintoutDialog));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.gbPrinters = new System.Windows.Forms.GroupBox();
            this.lblPrinterState = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.lblCurrentPirnter = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.lvPrinters = new System.Windows.Forms.ListView();
            this.PanelOkCansel = new System.Windows.Forms.Panel();
            this.lblReportServer = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.cboDuplex = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.updownNumCopies = new System.Windows.Forms.NumericUpDown();
            this.gbPrintSettings = new System.Windows.Forms.GroupBox();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.lblSetDirForExport = new System.Windows.Forms.Label();
            this.cmdSetDirForExport = new System.Windows.Forms.Button();
            this.cboExportFormat = new System.Windows.Forms.ComboBox();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.cmdOriginalSettings = new System.Windows.Forms.Button();
            this.lPrintExport = new System.Windows.Forms.Label();
            this.opbPrintCopies = new System.Windows.Forms.RadioButton();
            this.opbPrintOriginals = new System.Windows.Forms.RadioButton();
            this.chkCollate = new System.Windows.Forms.CheckBox();
            this.pbCollate = new System.Windows.Forms.PictureBox();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.opbExport = new System.Windows.Forms.RadioButton();
            this.opbPrint = new System.Windows.Forms.RadioButton();
            this.opbPreview = new System.Windows.Forms.RadioButton();
            this.gbDocs = new System.Windows.Forms.GroupBox();
            this.lvDocs = new System.Windows.Forms.ListView();
            this.Название = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OriginalsAlow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CopiesAlow = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDocsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuUnCheckAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuInvertAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuPrintLog = new System.Windows.Forms.ToolStripMenuItem();
            this.ilCollate = new System.Windows.Forms.ImageList(this.components);
            this.ToolTipSetDirForExport = new System.Windows.Forms.ToolTip(this.components);
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.gbPrinters.SuspendLayout();
            this.PanelOkCansel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownNumCopies)).BeginInit();
            this.gbPrintSettings.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCollate)).BeginInit();
            this.Panel2.SuspendLayout();
            this.gbDocs.SuspendLayout();
            this.lvDocsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "collated_s.png");
            this.ImageList.Images.SetKeyName(1, "collated_none_s.png");
            this.ImageList.Images.SetKeyName(2, "Selected.png");
            this.ImageList.Images.SetKeyName(3, "SelectedDefaultOK.png");
            this.ImageList.Images.SetKeyName(4, "NotSelected.png");
            this.ImageList.Images.SetKeyName(5, "NotSelectedDefault.png");
            // 
            // gbPrinters
            // 
            this.gbPrinters.Controls.Add(this.lblPrinterState);
            this.gbPrinters.Controls.Add(this.Label2);
            this.gbPrinters.Controls.Add(this.lblCurrentPirnter);
            this.gbPrinters.Controls.Add(this.Label1);
            this.gbPrinters.Controls.Add(this.lvPrinters);
            this.gbPrinters.Location = new System.Drawing.Point(0, 3);
            this.gbPrinters.Name = "gbPrinters";
            this.gbPrinters.Size = new System.Drawing.Size(554, 182);
            this.gbPrinters.TabIndex = 11;
            this.gbPrinters.TabStop = false;
            this.gbPrinters.Text = "Выбор принтера";
            // 
            // lblPrinterState
            // 
            this.lblPrinterState.AutoSize = true;
            this.lblPrinterState.Location = new System.Drawing.Point(112, 161);
            this.lblPrinterState.Name = "lblPrinterState";
            this.lblPrinterState.Size = new System.Drawing.Size(42, 13);
            this.lblPrinterState.TabIndex = 6;
            this.lblPrinterState.Text = "<state>";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(7, 161);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(64, 13);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "Состояние:";
            // 
            // lblCurrentPirnter
            // 
            this.lblCurrentPirnter.AutoSize = true;
            this.lblCurrentPirnter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCurrentPirnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCurrentPirnter.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblCurrentPirnter.Location = new System.Drawing.Point(112, 139);
            this.lblCurrentPirnter.Name = "lblCurrentPirnter";
            this.lblCurrentPirnter.Size = new System.Drawing.Size(48, 13);
            this.lblCurrentPirnter.TabIndex = 4;
            this.lblCurrentPirnter.Text = "<printer>";
            this.lblCurrentPirnter.Click += new System.EventHandler(this.lblCurrentPirnter_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 139);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(99, 13);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Текущий принтер:";
            // 
            // lvPrinters
            // 
            this.lvPrinters.LargeImageList = this.ImageList;
            this.lvPrinters.Location = new System.Drawing.Point(3, 19);
            this.lvPrinters.MultiSelect = false;
            this.lvPrinters.Name = "lvPrinters";
            this.lvPrinters.Size = new System.Drawing.Size(548, 117);
            this.lvPrinters.SmallImageList = this.ImageList;
            this.lvPrinters.TabIndex = 0;
            this.lvPrinters.UseCompatibleStateImageBehavior = false;
            this.lvPrinters.Click += new System.EventHandler(this.lvPrinters_Click);
            // 
            // PanelOkCansel
            // 
            this.PanelOkCansel.Controls.Add(this.lblReportServer);
            this.PanelOkCansel.Controls.Add(this.CancelButton);
            this.PanelOkCansel.Controls.Add(this.OKButton);
            this.PanelOkCansel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelOkCansel.Location = new System.Drawing.Point(0, 508);
            this.PanelOkCansel.Name = "PanelOkCansel";
            this.PanelOkCansel.Size = new System.Drawing.Size(554, 40);
            this.PanelOkCansel.TabIndex = 14;
            // 
            // lblReportServer
            // 
            this.lblReportServer.AutoEllipsis = true;
            this.lblReportServer.AutoSize = true;
            this.lblReportServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblReportServer.Location = new System.Drawing.Point(2, 24);
            this.lblReportServer.Name = "lblReportServer";
            this.lblReportServer.Size = new System.Drawing.Size(84, 13);
            this.lblReportServer.TabIndex = 12;
            this.lblReportServer.Text = "Сервер отчетов";
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(450, 6);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 31);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(332, 6);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 31);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cboDuplex
            // 
            this.cboDuplex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDuplex.Enabled = false;
            this.cboDuplex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cboDuplex.FormattingEnabled = true;
            this.cboDuplex.Location = new System.Drawing.Point(213, 97);
            this.cboDuplex.Name = "cboDuplex";
            this.cboDuplex.Size = new System.Drawing.Size(140, 24);
            this.cboDuplex.TabIndex = 4;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label3.Location = new System.Drawing.Point(24, 71);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(145, 16);
            this.Label3.TabIndex = 7;
            this.Label3.Text = "Кол-во экземпляров:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label4.Location = new System.Drawing.Point(24, 100);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(106, 16);
            this.Label4.TabIndex = 8;
            this.Label4.Text = "С двух сторон?";
            // 
            // updownNumCopies
            // 
            this.updownNumCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updownNumCopies.Location = new System.Drawing.Point(294, 66);
            this.updownNumCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updownNumCopies.Name = "updownNumCopies";
            this.updownNumCopies.Size = new System.Drawing.Size(59, 26);
            this.updownNumCopies.TabIndex = 3;
            this.updownNumCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gbPrintSettings
            // 
            this.gbPrintSettings.Controls.Add(this.Panel4);
            this.gbPrintSettings.Controls.Add(this.Panel1);
            this.gbPrintSettings.Controls.Add(this.lblSetDirForExport);
            this.gbPrintSettings.Controls.Add(this.cmdSetDirForExport);
            this.gbPrintSettings.Controls.Add(this.cboExportFormat);
            this.gbPrintSettings.Controls.Add(this.Label4);
            this.gbPrintSettings.Controls.Add(this.Label3);
            this.gbPrintSettings.Controls.Add(this.Panel3);
            this.gbPrintSettings.Controls.Add(this.chkCollate);
            this.gbPrintSettings.Controls.Add(this.pbCollate);
            this.gbPrintSettings.Controls.Add(this.updownNumCopies);
            this.gbPrintSettings.Controls.Add(this.cboDuplex);
            this.gbPrintSettings.Controls.Add(this.Panel2);
            this.gbPrintSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbPrintSettings.Location = new System.Drawing.Point(0, 191);
            this.gbPrintSettings.Name = "gbPrintSettings";
            this.gbPrintSettings.Size = new System.Drawing.Size(562, 202);
            this.gbPrintSettings.TabIndex = 12;
            this.gbPrintSettings.TabStop = false;
            this.gbPrintSettings.Text = "Режим вывода";
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.DimGray;
            this.Panel4.Location = new System.Drawing.Point(30, 170);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(520, 1);
            this.Panel4.TabIndex = 21;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.DimGray;
            this.Panel1.Location = new System.Drawing.Point(80, 55);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(310, 1);
            this.Panel1.TabIndex = 20;
            // 
            // lblSetDirForExport
            // 
            this.lblSetDirForExport.AutoEllipsis = true;
            this.lblSetDirForExport.Location = new System.Drawing.Point(400, 141);
            this.lblSetDirForExport.MaximumSize = new System.Drawing.Size(160, 17);
            this.lblSetDirForExport.Name = "lblSetDirForExport";
            this.lblSetDirForExport.Size = new System.Drawing.Size(154, 16);
            this.lblSetDirForExport.TabIndex = 18;
            this.lblSetDirForExport.Text = "Укажите путь для экспорта";
            this.lblSetDirForExport.Click += new System.EventHandler(this.cmdSetDirForExport_Click);
            // 
            // cmdSetDirForExport
            // 
            this.cmdSetDirForExport.Location = new System.Drawing.Point(367, 138);
            this.cmdSetDirForExport.Name = "cmdSetDirForExport";
            this.cmdSetDirForExport.Size = new System.Drawing.Size(27, 23);
            this.cmdSetDirForExport.TabIndex = 16;
            this.cmdSetDirForExport.Text = "...";
            this.cmdSetDirForExport.UseVisualStyleBackColor = true;
            this.cmdSetDirForExport.Click += new System.EventHandler(this.cmdSetDirForExport_Click);
            // 
            // cboExportFormat
            // 
            this.cboExportFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExportFormat.Enabled = false;
            this.cboExportFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cboExportFormat.FormattingEnabled = true;
            this.cboExportFormat.Location = new System.Drawing.Point(213, 138);
            this.cboExportFormat.Name = "cboExportFormat";
            this.cboExportFormat.Size = new System.Drawing.Size(140, 24);
            this.cboExportFormat.TabIndex = 14;
            // 
            // Panel3
            // 
            this.Panel3.Controls.Add(this.cmdOriginalSettings);
            this.Panel3.Controls.Add(this.lPrintExport);
            this.Panel3.Controls.Add(this.opbPrintCopies);
            this.Panel3.Controls.Add(this.opbPrintOriginals);
            this.Panel3.Location = new System.Drawing.Point(27, 171);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(521, 25);
            this.Panel3.TabIndex = 4;
            // 
            // cmdOriginalSettings
            // 
            this.cmdOriginalSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmdOriginalSettings.Image = ((System.Drawing.Image)(resources.GetObject("cmdOriginalSettings.Image")));
            this.cmdOriginalSettings.Location = new System.Drawing.Point(470, 1);
            this.cmdOriginalSettings.Name = "cmdOriginalSettings";
            this.cmdOriginalSettings.Size = new System.Drawing.Size(23, 23);
            this.cmdOriginalSettings.TabIndex = 9;
            this.cmdOriginalSettings.UseVisualStyleBackColor = true;
            this.cmdOriginalSettings.Click += new System.EventHandler(this.cmdOriginalSettings_Click);
            // 
            // lPrintExport
            // 
            this.lPrintExport.AutoSize = true;
            this.lPrintExport.Location = new System.Drawing.Point(59, 4);
            this.lPrintExport.Name = "lPrintExport";
            this.lPrintExport.Size = new System.Drawing.Size(74, 16);
            this.lPrintExport.TabIndex = 8;
            this.lPrintExport.Text = "Печатать:";
            // 
            // opbPrintCopies
            // 
            this.opbPrintCopies.AutoSize = true;
            this.opbPrintCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.opbPrintCopies.Location = new System.Drawing.Point(339, 2);
            this.opbPrintCopies.Name = "opbPrintCopies";
            this.opbPrintCopies.Size = new System.Drawing.Size(66, 20);
            this.opbPrintCopies.TabIndex = 7;
            this.opbPrintCopies.Text = "Копии";
            this.opbPrintCopies.UseVisualStyleBackColor = true;
            // 
            // opbPrintOriginals
            // 
            this.opbPrintOriginals.AutoSize = true;
            this.opbPrintOriginals.Checked = true;
            this.opbPrintOriginals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.opbPrintOriginals.Location = new System.Drawing.Point(153, 2);
            this.opbPrintOriginals.Name = "opbPrintOriginals";
            this.opbPrintOriginals.Size = new System.Drawing.Size(99, 20);
            this.opbPrintOriginals.TabIndex = 6;
            this.opbPrintOriginals.TabStop = true;
            this.opbPrintOriginals.Text = "Оригиналы";
            this.opbPrintOriginals.UseVisualStyleBackColor = true;
            // 
            // chkCollate
            // 
            this.chkCollate.AutoSize = true;
            this.chkCollate.Location = new System.Drawing.Point(369, 108);
            this.chkCollate.Name = "chkCollate";
            this.chkCollate.Size = new System.Drawing.Size(167, 20);
            this.chkCollate.TabIndex = 5;
            this.chkCollate.Text = "Разобрать по копиям";
            this.chkCollate.UseVisualStyleBackColor = true;
            this.chkCollate.CheckedChanged += new System.EventHandler(this.chkCollate_CheckedChanged);
            // 
            // pbCollate
            // 
            this.pbCollate.Image = ((System.Drawing.Image)(resources.GetObject("pbCollate.Image")));
            this.pbCollate.InitialImage = null;
            this.pbCollate.Location = new System.Drawing.Point(403, 34);
            this.pbCollate.Name = "pbCollate";
            this.pbCollate.Size = new System.Drawing.Size(105, 58);
            this.pbCollate.TabIndex = 13;
            this.pbCollate.TabStop = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.opbExport);
            this.Panel2.Controls.Add(this.opbPrint);
            this.Panel2.Controls.Add(this.opbPreview);
            this.Panel2.Location = new System.Drawing.Point(3, 18);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(157, 151);
            this.Panel2.TabIndex = 0;
            // 
            // opbExport
            // 
            this.opbExport.AutoSize = true;
            this.opbExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.opbExport.Location = new System.Drawing.Point(4, 121);
            this.opbExport.Name = "opbExport";
            this.opbExport.Size = new System.Drawing.Size(149, 20);
            this.opbExport.TabIndex = 18;
            this.opbExport.Text = "Экспорт в формат:";
            this.opbExport.UseVisualStyleBackColor = true;
            // 
            // opbPrint
            // 
            this.opbPrint.AutoSize = true;
            this.opbPrint.Checked = true;
            this.opbPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.opbPrint.Location = new System.Drawing.Point(2, 26);
            this.opbPrint.Name = "opbPrint";
            this.opbPrint.Size = new System.Drawing.Size(74, 20);
            this.opbPrint.TabIndex = 2;
            this.opbPrint.TabStop = true;
            this.opbPrint.Text = "Печать";
            this.opbPrint.UseVisualStyleBackColor = true;
            // 
            // opbPreview
            // 
            this.opbPreview.AutoSize = true;
            this.opbPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.opbPreview.Location = new System.Drawing.Point(3, 3);
            this.opbPreview.Name = "opbPreview";
            this.opbPreview.Size = new System.Drawing.Size(91, 20);
            this.opbPreview.TabIndex = 1;
            this.opbPreview.Text = "Просмотр";
            this.opbPreview.UseVisualStyleBackColor = true;
            // 
            // gbDocs
            // 
            this.gbDocs.Controls.Add(this.lvDocs);
            this.gbDocs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.gbDocs.Location = new System.Drawing.Point(0, 399);
            this.gbDocs.Name = "gbDocs";
            this.gbDocs.Size = new System.Drawing.Size(554, 106);
            this.gbDocs.TabIndex = 13;
            this.gbDocs.TabStop = false;
            this.gbDocs.Text = "Документы";
            // 
            // lvDocs
            // 
            this.lvDocs.CheckBoxes = true;
            this.lvDocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Название,
            this.OriginalsAlow,
            this.CopiesAlow});
            this.lvDocs.ContextMenuStrip = this.lvDocsContextMenu;
            this.lvDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDocs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lvDocs.FullRowSelect = true;
            this.lvDocs.GridLines = true;
            this.lvDocs.Location = new System.Drawing.Point(3, 18);
            this.lvDocs.MultiSelect = false;
            this.lvDocs.Name = "lvDocs";
            this.lvDocs.Size = new System.Drawing.Size(548, 85);
            this.lvDocs.TabIndex = 9;
            this.lvDocs.UseCompatibleStateImageBehavior = false;
            this.lvDocs.View = System.Windows.Forms.View.Details;
            // 
            // Название
            // 
            this.Название.Text = "Название";
            this.Название.Width = 277;
            // 
            // OriginalsAlow
            // 
            this.OriginalsAlow.Text = "Оригиналов доступно";
            this.OriginalsAlow.Width = 120;
            // 
            // CopiesAlow
            // 
            this.CopiesAlow.Text = "Копий доступно";
            this.CopiesAlow.Width = 120;
            // 
            // lvDocsContextMenu
            // 
            this.lvDocsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuCheckAll,
            this.ToolStripMenuUnCheckAll,
            this.ToolStripMenuInvertAll,
            this.toolStripSeparator1,
            this.ToolStripMenuProperties,
            this.ToolStripMenuPrintLog});
            this.lvDocsContextMenu.Name = "lvDocsContextMenu";
            this.lvDocsContextMenu.ShowImageMargin = false;
            this.lvDocsContextMenu.Size = new System.Drawing.Size(197, 142);
            this.lvDocsContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.lvDocsContextMenu_Opening);
            this.lvDocsContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.lvDocsContextMenu_ItemClicked);
            // 
            // ToolStripMenuCheckAll
            // 
            this.ToolStripMenuCheckAll.Name = "ToolStripMenuCheckAll";
            this.ToolStripMenuCheckAll.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuCheckAll.Text = "Выделить все";
            // 
            // ToolStripMenuUnCheckAll
            // 
            this.ToolStripMenuUnCheckAll.Name = "ToolStripMenuUnCheckAll";
            this.ToolStripMenuUnCheckAll.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuUnCheckAll.Text = "Снять выделение";
            // 
            // ToolStripMenuInvertAll
            // 
            this.ToolStripMenuInvertAll.Name = "ToolStripMenuInvertAll";
            this.ToolStripMenuInvertAll.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuInvertAll.Text = "Инвертировать выделение";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // ToolStripMenuProperties
            // 
            this.ToolStripMenuProperties.Name = "ToolStripMenuProperties";
            this.ToolStripMenuProperties.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuProperties.Text = "Свойства отчета";
            // 
            // ToolStripMenuPrintLog
            // 
            this.ToolStripMenuPrintLog.Name = "ToolStripMenuPrintLog";
            this.ToolStripMenuPrintLog.Size = new System.Drawing.Size(196, 22);
            this.ToolStripMenuPrintLog.Text = "История печати";
            // 
            // ilCollate
            // 
            this.ilCollate.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilCollate.ImageStream")));
            this.ilCollate.TransparentColor = System.Drawing.Color.Transparent;
            this.ilCollate.Images.SetKeyName(0, "Collate.ico");
            this.ilCollate.Images.SetKeyName(1, "CollateNot.ico");
            // 
            // ToolTipSetDirForExport
            // 
            this.ToolTipSetDirForExport.AutoPopDelay = 5000;
            this.ToolTipSetDirForExport.InitialDelay = 200;
            this.ToolTipSetDirForExport.ReshowDelay = 100;
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // frmPrintoutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 548);
            this.Controls.Add(this.gbDocs);
            this.Controls.Add(this.gbPrintSettings);
            this.Controls.Add(this.PanelOkCansel);
            this.Controls.Add(this.gbPrinters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(570, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(570, 575);
            this.Name = "frmPrintoutDialog";
            this.Text = "Печать";
            this.Activated += new System.EventHandler(this.frmPrintoutDialog_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrintoutDialog_FormClosing);
            this.Load += new System.EventHandler(this.frmPrintoutDialog_Load);
            this.Resize += new System.EventHandler(this.frmPrintoutDialog_Resize);
            this.gbPrinters.ResumeLayout(false);
            this.gbPrinters.PerformLayout();
            this.PanelOkCansel.ResumeLayout(false);
            this.PanelOkCansel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updownNumCopies)).EndInit();
            this.gbPrintSettings.ResumeLayout(false);
            this.gbPrintSettings.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCollate)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.gbDocs.ResumeLayout(false);
            this.lvDocsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.ImageList ImageList;
        internal System.Windows.Forms.GroupBox gbPrinters;
        internal System.Windows.Forms.ListView lvPrinters;
        internal System.Windows.Forms.Label lblPrinterState;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lblCurrentPirnter;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Panel PanelOkCansel;
        internal new System.Windows.Forms.Button CancelButton;
        internal System.Windows.Forms.Button OKButton;
        internal System.Windows.Forms.ComboBox cboDuplex;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.NumericUpDown updownNumCopies;
        internal System.Windows.Forms.GroupBox gbPrintSettings;
        internal System.Windows.Forms.CheckBox chkCollate;
        internal System.Windows.Forms.PictureBox pbCollate;
        //Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
        //Friend WithEvents LineShape2 As Microsoft.VisualBasic.PowerPacks.LineShape
        //Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.RadioButton opbPrintCopies;
        internal System.Windows.Forms.RadioButton opbPrintOriginals;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.RadioButton opbPrint;
        internal System.Windows.Forms.RadioButton opbPreview;
        internal System.Windows.Forms.GroupBox gbDocs;
        //Friend WithEvents LineShape3 As Microsoft.VisualBasic.PowerPacks.LineShape
        internal System.Windows.Forms.ComboBox cboExportFormat;
        internal System.Windows.Forms.RadioButton opbExport;
        internal System.Windows.Forms.Label lPrintExport;
        internal System.Windows.Forms.ListView lvDocs;
        internal System.Windows.Forms.ColumnHeader Название;
        internal System.Windows.Forms.ColumnHeader OriginalsAlow;
        internal System.Windows.Forms.ColumnHeader CopiesAlow;
        internal System.Windows.Forms.ContextMenuStrip lvDocsContextMenu;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuCheckAll;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuUnCheckAll;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuInvertAll;
        internal System.Windows.Forms.Button cmdSetDirForExport;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuProperties;
        internal System.Windows.Forms.ImageList ilCollate;
        internal System.Windows.Forms.Label lblSetDirForExport;
        internal System.Windows.Forms.ToolTip ToolTipSetDirForExport;
        internal System.ComponentModel.BackgroundWorker BackgroundWorker;
        internal System.Windows.Forms.Panel Panel4;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button cmdOriginalSettings;

        #endregion
        internal System.Windows.Forms.Label lblReportServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuPrintLog;
    }
}