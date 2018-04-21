using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Data.OleDb;
using System.IO;
using System.Linq;
namespace Reporting
{
    public partial class frmPrintoutDialog : Form
    {
        private enum PrinterStatus
        {
            PrinterIdle = 3,
            PrinterPrinting = 4,
            PrinterWarmingUp = 5
        }


        private ReportApplication p_app;
        private List<ReportItem> mReports;
        private bool mLaunchedFromPreview;

        private PrinterSettings mPrinterSettings;
        private bool mIsSetToExport;
        private int mExportFormatDefault;

        private bool mIsSetToCopy;
        public frmPrintoutDialog(ref ReportApplication _app, bool FromPrintAll = false, bool SetToExport = false, int ExportFormat = 1, bool SetToCopy = false)
        {
            //печать пакета документов
            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            p_app = _app;
            mPrinterSettings = p_app.PrinterSettings;

            mReports = p_app.Reports;
            //Если форма вызвана из кнопки "печатать все" на ReportView то: 
            //необходимо заново создать ctlReportView и прописать что его интерфейс не загружался (IsLoadInterface = False)
            if (FromPrintAll)
            {
                foreach (ReportItem Item in mReports)
                {
                    Item.IsLoadInterface = false;

                }
            }
            mIsSetToExport = SetToExport;
            mExportFormatDefault = ExportFormat - 1;
            mIsSetToCopy = SetToCopy;


        }

        public frmPrintoutDialog(ref ReportApplication _app, ReportItem _report)
        {
            //печать из предпросмотра
            // This call is required by the designer.
            InitializeComponent();

            p_app = _app;
            mPrinterSettings = p_app.PrinterSettings;

            // Add any initialization after the InitializeComponent() call.
            lvDocs.CheckBoxes = false;
            lvDocsContextMenu.Items.Find("ToolStripMenuCheckAll", false)[0].Enabled = false;
            lvDocsContextMenu.Items.Find("ToolStripMenuUnCheckAll", false)[0].Enabled = false;
            lvDocsContextMenu.Items.Find("ToolStripMenuInvertAll", false)[0].Enabled = false;

            mLaunchedFromPreview = true;

            mReports = new List<ReportItem>();
            mReports.Add(_report);

            opbPreview.Enabled = false;
            opbPrint.Checked = true;

        }

        private string PrinterStatusToString(PrinterStatus ps)
        {
            switch (ps)
            {
                case PrinterStatus.PrinterIdle:
                    return "Не занят";
                case PrinterStatus.PrinterPrinting:
                    return "Занят";
                case PrinterStatus.PrinterWarmingUp:
                    return "Подготавливается";
                default:
                    return "Неизвестно";
            }
        }

        private void frmPrintoutDialog_Activated(object sender, System.EventArgs e)
        {
            OKButton.Focus();
        }


        private void frmPrintoutDialog_Load(System.Object sender = null, System.EventArgs e = null)
        {

            LoadPrinters();
            LoadcboDuplex();
            LoadcboExportFormat();
            chkCollate_CheckedChanged();
            opbPreview.CheckedChanged += opbPreview_CheckedChanged;
            opbPrint.CheckedChanged += opbPreview_CheckedChanged;
            opbExport.CheckedChanged += opbPreview_CheckedChanged;

            opbPreview_CheckedChanged();
            LoadDocs();
            if ((p_app.ExportDir != null))
                lblSetDirForExport.Text = p_app.ExportDir.FullName;

            lvDocs.ItemChecked += lvDocs_ItemChecked;


            if (mIsSetToExport == true)
            {
                this.opbExport.Checked = true;
                this.cboExportFormat.SelectedIndex = mExportFormatDefault;
            }
            if (mIsSetToCopy == true)
            {
                this.opbPrintCopies.Checked = true;
            }
            lblReportServer.Text = "Сервер отчетов: [" + Constants.SERVER_NAME + "];c#";

        }


        private void LoadPrinters()
        {
            ListViewItem lvItem = new ListViewItem();
            List<Printer> printers;
            printers = GlobalObjects.GetPrinters();

            if (printers.Count == 0)
            {
                //дописать обработку варианта когда нет принтеров
                return;
            }

            foreach (Printer item in printers)
            {

                if (item.Default)
                {
                    lvItem = lvPrinters.Items.Add(item.Name, item.Name, 5);

                }
                else
                {
                    lvItem = lvPrinters.Items.Add(item.Name, item.Name, 4);
                }


                if (mPrinterSettings.PrinterName == item.Name)
                {
                    lvItem.Selected = true;
                    lvItem.ImageIndex = (item.Default ? 3 : 2);
                }


                lvItem.Tag = (!item.WorkOffline ? "1" : "0") + (item.Default ? "1" : "0");
            }
            lvPrinters_Click();

        }
        private void lvPrinters_Click(object sender = null, System.EventArgs e = null)
        {
            //if (lvPrinters.SelectedItems.Count == 0)
            //{
            //    if (lvPrinters.Items.Count == 0)
            //        return;
            //    lvPrinters.Items[0].Selected = true;
            //}
            lblCurrentPirnter.Text = lvPrinters.SelectedItems[0].Text;
            lblPrinterState.Text = (lvPrinters.SelectedItems[0].Tag.ToString().Substring(0, 1) == "1" ? "Доступен" : "Не доступен");
            if (mPrinterSettings.PrinterName != lvPrinters.SelectedItems[0].Text)
            {
                mPrinterSettings = new PrinterSettings();
                mPrinterSettings.PrinterName = lvPrinters.SelectedItems[0].Text;
                p_app.PrinterSettings = mPrinterSettings;
            }

            foreach (ListViewItem Item in lvPrinters.Items)
            {
                if (Item.Selected)
                {
                    Item.ImageIndex = (Item.Tag.ToString().Substring(1, 1) == "1" ? 3 : 2);
                }
                else
                {
                    Item.ImageIndex = (Item.Tag.ToString().Substring(1, 1) == "1" ? 5 : 4);
                }
            }

        }

        private void lblCurrentPirnter_Click(System.Object sender, System.EventArgs e)
        {
            if ((lvPrinters.SelectedItems[0] != null))
            {
                mPrinterSettings.PrinterName = lvPrinters.SelectedItems[0].Text;
                mPrinterSettings = PrinterSettingsDialog.OpenPrinterPropertiesDialog(this.Handle, mPrinterSettings);
            }
        }

        private void LoadcboDuplex()
        {
            cboDuplex.Items.Add("нет");
            cboDuplex.Items.Add("да");
            //cboDuplex.Items.Add("да, вертикально")
            cboDuplex.SelectedIndex = 0;


        }
        private void LoadcboExportFormat()
        {
            //не забывать сверять с типом ExportFormat в модуле GlobalObjects.vb!!!
            //править в:
            //-ExportFormat в модуле GlobalObjects.vb
            //-запрос в процедуре LoadDocs (здесь)
            //-ReportItem New()
            //-ReportItemLogInfo New(),Refresh(),поля
            //-БД dbo.SrvPr_REPORTS
            foreach (ExportFormat format in Enum.GetValues(typeof(ExportFormat)))
            {
                cboExportFormat.Items.Add(ExportFormatInfo.FileExtentionPromt(format));
            }

            //cboExportFormat.Items.Add("Документ *.pdf");
            //cboExportFormat.Items.Add("Документ *.xls");
            cboExportFormat.SelectedIndex = 0;

        }

        private void chkCollate_CheckedChanged(System.Object sender = null, System.EventArgs e = null)
        {
            if (chkCollate.Checked)
            {
                pbCollate.Image = ilCollate.Images[0];
            }
            else
            {
                pbCollate.Image = ilCollate.Images[1];
            }

        }




        // Handles opbPreview.CheckedChanged
        private void opbPreview_CheckedChanged(System.Object sender = null, System.EventArgs e = null)
        {
            if ((sender != null))
            {
                RadioButton Ctl = default(RadioButton);
                Ctl = (RadioButton)sender;
                if (Ctl.Checked == false)
                    return;
            }

            if (opbPreview.Checked)
            {
                updownNumCopies.Enabled = false;
                cboDuplex.Enabled = false;
                chkCollate.Enabled = false;
                opbPrintOriginals.Enabled = false;
                opbPrintCopies.Enabled = false;

                cboExportFormat.Enabled = false;

                OKButton.Text = "Просмотр";
            }
            else if (opbPrint.Checked)
            {
                updownNumCopies.Enabled = true;
                cboDuplex.Enabled = true;
                chkCollate.Enabled = true;
                opbPrintOriginals.Enabled = true;
                opbPrintCopies.Enabled = true;

                cboExportFormat.Enabled = false;

                OKButton.Text = "Печать";

            }
            else if (opbExport.Checked)
            {
                updownNumCopies.Enabled = false;
                cboDuplex.Enabled = false;
                chkCollate.Enabled = false;
                opbPrintOriginals.Enabled = true;
                opbPrintCopies.Enabled = true;

                cboExportFormat.Enabled = true;

                OKButton.Text = "Экспорт";

            }

        }




        private void frmPrintoutDialog_Resize(object sender, System.EventArgs e)
        {
            gbDocs.Height = PanelOkCansel.Location.Y - gbDocs.Location.Y;
        }

        private void LoadDocs()
        {
            ListViewItem lvItem = default(ListViewItem);

            string sDocs = "";
            //sSQL = "DECLARE @DOCS TABLE(DOCNUM VARCHAR(50), REPNAME VARCHAR(100), IDENT INT, WAREHOUSE VARCHAR(15)) ";// +"INSERT INTO @DOCS ";
            int i = 0;

            foreach (ReportItem Item in mReports)
            {
                foreach (string DocItem in (string[])Item.arrDocumentNums)
                {
                    //sSQL = sSQL + "INSERT INTO @DOCS (DOCNUM,REPNAME,IDENT,WAREHOUSE) values('" + DocItem + "','" + Item.ReportName + "', " + i + ",'" + Item.Warehouse + "' ); ";
                    sDocs += DocItem + "#" + Item.ReportName + "#" + i.ToString() + "|";
                }
                i += 1;
            }
            sDocs = sDocs.TrimEnd('|');
            //sSQL = "EXEC " + p_app.AppLocation.Schema + ".[SrvPr_PrintedDocsGet] " + sSQL;

            //sSQL = sSQL + "; SELECT D.IDENT, " + "ISNULL(SUM(CASE WHEN ISNULL(PL1.REPORTNAME,'')<>'' AND PL1.ORIGINAL = 1 AND PL1.PRINTFORMAT = 0 THEN PL1.PRINTAMOUNT ELSE 0 END),0) AS ORIGINALS, " + "ISNULL(SUM(CASE WHEN ISNULL(PL1.REPORTNAME,'')<>'' AND PL1.ORIGINAL = 0 AND PL1.PRINTFORMAT = 0 THEN PL1.PRINTAMOUNT ELSE 0 END),0) AS COPIES, " + "ISNULL(SUM(CASE WHEN ISNULL(PL1.REPORTNAME,'')<>'' AND PL1.ORIGINAL = 1 AND PL1.PRINTFORMAT = 1 THEN PL1.PRINTAMOUNT ELSE 0 END),0) AS ORIGINALSEXPORTPDF, " + "ISNULL(SUM(CASE WHEN ISNULL(PL1.REPORTNAME,'')<>'' AND PL1.ORIGINAL = 0 AND PL1.PRINTFORMAT = 1 THEN PL1.PRINTAMOUNT ELSE 0 END),0) AS COPIESEXPORTPDF, " + "ISNULL(SUM(CASE WHEN ISNULL(PL1.REPORTNAME,'')<>'' AND PL1.ORIGINAL = 1 AND PL1.PRINTFORMAT = 2 THEN PL1.PRINTAMOUNT ELSE 0 END),0) AS ORIGINALSEXPORTXLS, " + "ISNULL(SUM(CASE WHEN ISNULL(PL1.REPORTNAME,'')<>'' AND PL1.ORIGINAL = 0 AND PL1.PRINTFORMAT = 2 THEN PL1.PRINTAMOUNT ELSE 0 END),0) AS COPIESEXPORTXLS " + "FROM  @DOCS D " + "LEFT JOIN " + p_app.AppLocation.Schema + ".SrvPr_PRINTLOG PL1 WITH(NOLOCK) ON PL1.DOCNUMBER=D.DOCNUM AND PL1.REPORTNAME=D.REPNAME " + "GROUP BY D.IDENT ORDER BY D.IDENT ";


            OleDbCommand command = new OleDbCommand("EXEC " + p_app.AppLocation.Schema + ".[SrvPr_PrintedDocsGet] @DOCS = ?", p_app.AppLocation.Connection);
            OleDbParameter parDocs = new OleDbParameter("@parDocs", OleDbType.LongVarChar, -1);
            parDocs.Value = sDocs;
            command.Parameters.Add(parDocs);
            OleDbDataReader reader = command.ExecuteReader();
            i = 0;
            if (reader.HasRows)
            {
                foreach (ReportItem Item in mReports)
                {
                    reader.Read();

                    lvItem = lvDocs.Items.Add(Item.ReportCaption + " [" + Item.DocumentNums + "]");
                    lvItem.Tag = i;
                    lvItem.Checked = Item.IsChecked;

                    lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(Item.ReportItemLogInfo.MaxOriginals, reader.GetInt32(reader.GetOrdinal("ORIGINALS"))));
                    lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(Item.ReportItemLogInfo.MaxCopies, reader.GetInt32(reader.GetOrdinal("COPIES"))));


                    Item.ReportItemLogInfo.Originals = reader.GetInt32(reader.GetOrdinal("ORIGINALS"));
                    Item.ReportItemLogInfo.Copies = reader.GetInt32(reader.GetOrdinal("COPIES"));
                    Item.ReportItemLogInfo.OriginalsExportPdf = reader.GetInt32(reader.GetOrdinal("ORIGINALSEXPORTPDF"));
                    Item.ReportItemLogInfo.CopiesExportPdf = reader.GetInt32(reader.GetOrdinal("COPIESEXPORTPDF"));
                    Item.ReportItemLogInfo.OriginalsExportPdf = reader.GetInt32(reader.GetOrdinal("ORIGINALSEXPORTXLS"));
                    Item.ReportItemLogInfo.CopiesExportPdf = reader.GetInt32(reader.GetOrdinal("COPIESEXPORTXLS"));

                    i += 1;
                }
            }
            reader.Close();


        }


        public void OKButton_Click(System.Object sender = null, System.EventArgs e = null)
        {
            if (opbPrint.Checked | opbExport.Checked)
            {
                //p_app.IsOldChange = False ' перед печатью/экспортом обнуляем глобальную авторизацию старшего смены -> теперь в конце всей печати
                //печать или экспорт

                if ((!HasCheckedItems()) & (!mLaunchedFromPreview))
                    return;


                if (opbPrint.Checked)
                {
                    mPrinterSettings.PrinterName = this.lvPrinters.SelectedItems[0].Text;
                    mPrinterSettings.Copies = (short)updownNumCopies.Value;
                    mPrinterSettings.Collate = (chkCollate.CheckState == CheckState.Checked ? true : false);  // 1==CheckState.Checked ?????

                    //If mPrinterSettings.CanDuplex = True Then
                    //    'двухсторонняя печать реализована иначе, чем в госте. Выбор горизонтально/вертикально - вручную, хотя можно было сделать и автоматом.
                    //    mPrinterSettings.Duplex = IIf(cboDuplex.SelectedIndex = 0, Duplex.Simplex, IIf(cboDuplex.SelectedIndex = 1, Duplex.Horizontal, Duplex.Vertical))
                    //End If
                }

                if (opbPrint.Checked)
                {
                    frmWaiting f = new frmWaiting("Старт процесса печати...");
                    f.Show();

                    this.BackgroundWorker.RunWorkerAsync(new object[] {
					true,
					mPrinterSettings,
					opbPrintOriginals.Checked,
					f,
					(cboDuplex.SelectedIndex == 1 ? true : false),
                    mLaunchedFromPreview
				});
                }
                else
                {
                    if ((p_app.ExportDir != null))
                    {
                        frmWaiting f = new frmWaiting("Старт процесса экспорта...");
                        f.Show();
                        this.BackgroundWorker.RunWorkerAsync(new object[] {
						false,
						cboExportFormat.SelectedIndex + 1,
						opbPrintOriginals.Checked,
						f,
                        null,
                        mLaunchedFromPreview
					});
                    }
                    else
                    {
                        MessageBox.Show("Укажите директорию для экспорта", p_app.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //доделать указание формата экспорта!!!
                }
                if (!mLaunchedFromPreview)
                    this.Close();
                else
                    this.Enabled = false;


            }
            else if (opbPreview.Checked)
            {
                //предпросмотр
                if (HasCheckedItems())
                {
                    this.Hide(); //добавил для более быстрой прогрузки формы просмотра
                    frmReportsPreview f = new frmReportsPreview(ref p_app);
                    f.Show();
                    this.Close();
                }

            }

        }
        private bool HasCheckedItems()
        {
            foreach (ListViewItem IvItem in lvDocs.Items)
            {
                if (IvItem.Checked == true)
                {
                    return true;
                }
            }
            MessageBox.Show("Необходимо отметить хотя бы один отчет");
            return false;
        }


        private void CancelButton_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }



        private void BackgroundWorker_DoWork(System.Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            bool IsPrint = (bool)args[0];
            PrinterSettings printerSettings = default(PrinterSettings);
            ExportFormat PrintFormat = default(ExportFormat);
            if (IsPrint)
                printerSettings = (PrinterSettings)args[1];
            else
                PrintFormat = (ExportFormat)args[1];
            //действия зависят от формата:печать или экспорт
            bool printOriginals = (bool)args[2];
            frmWaiting frmWaiting = (frmWaiting)args[3];
            bool IsDuplex = false;
            if (IsPrint)
                IsDuplex = (bool)args[4];
            bool launchedFromPreview = (bool)args[5];


            long CountmReports = 0;
            long Counter = 0;
            // счетчики для информации
            foreach (ReportItem objReportItem in mReports)
            {
                if (objReportItem.IsChecked)
                {
                    CountmReports = CountmReports + 1;
                }
            }

            foreach (ReportItem objReportItem in mReports)
            {
                if (objReportItem.IsChecked)
                {
                    Counter = Counter + 1;
                    //действия зависят от формата:печать или экспорт
                    try
                    {
                        if (IsPrint)
                        {
                            if (GlobalObjects.IsDebugMode) { GlobalObjects.AddLogMessage("Подготовка отчета"); }
                            RefreshInfo(frmWaiting, "Подготовка отчета ''" + objReportItem.ReportCaption + " [" + objReportItem.DocumentNums + "]'' (" + Counter + "/" + CountmReports + ")");
                            //так как запущено не из превью необходимо собрать ReportView control без интерфейса
                            objReportItem.ConstructCtlReport(-1);
                            if (GlobalObjects.IsDebugMode) { GlobalObjects.AddLogMessage("Печать отчета"); }
                            RefreshInfo(frmWaiting, "Печать отчета ''" + objReportItem.ReportCaption + " [" + objReportItem.DocumentNums + "]'' (" + Counter + "/" + CountmReports + ")");
                            objReportItem.PrintMeSoftware(printerSettings, printOriginals, IsDuplex);
                        }
                        else
                        {
                            RefreshInfo(frmWaiting, "Подготовка отчета ''" + objReportItem.ReportCaption + " [" + objReportItem.DocumentNums + "]'' (" + Counter + "/" + CountmReports + ")");
                            //так как запущено не из превью необходимо собрать ReportView control
                            objReportItem.ConstructCtlReport(-1);
                            RefreshInfo(frmWaiting, "Экспорт отчета ''" + objReportItem.ReportCaption + " [" + objReportItem.DocumentNums + "]'' (" + Counter + "/" + CountmReports + ")");
                            objReportItem.ExportMeSoftware(printOriginals, PrintFormat, p_app.ExportDir);
                        }
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show("Error in rendering ReportItem: " + e1.Message, p_app.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //objReportItem = null; ??? возможно требуется обнуление!!!!
                }
            }

            printerSettings = null;

            p_app.IsOldChange = false;

            //?????форму диалога закрывать до старта печати?????? 
            CloseMe(this);
            CloseMe(frmWaiting);

            BackgroundWorker.CancelAsync();
            mReports.Clear();
            if (launchedFromPreview)
                p_app.IsOldChange = false;
            else
                p_app.Dispose();

        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Dispose();
        }

        private void lvDocs_ItemChecked(object sender, System.Windows.Forms.ItemCheckedEventArgs e)
        {
            ReportItem objReportItem = (ReportItem)mReports[(int)e.Item.Tag];
            objReportItem.IsChecked = e.Item.Checked;
        }




        private void lvDocsContextMenu_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;
            switch (ToolStripMenuItem.Name)
            {
                case "ToolStripMenuCheckAll":
                    foreach (ListViewItem IvItem in lvDocs.Items)
                    {
                        IvItem.Checked = true;
                    }

                    break;
                case "ToolStripMenuUnCheckAll":
                    foreach (ListViewItem IvItem in lvDocs.Items)
                    {
                        IvItem.Checked = false;
                    }

                    break;
                case "ToolStripMenuInvertAll":
                    foreach (ListViewItem IvItem in lvDocs.Items)
                    {
                        IvItem.Checked = (IvItem.Checked ? false : true);
                    }

                    break;
                case "ToolStripMenuProperties":
                    if (lvDocs.SelectedItems.Count == 0)
                        break; // TODO: might not be correct. Was : Exit Select

                    ReportItem ReportItemSelected = (ReportItem)mReports[Convert.ToInt32(lvDocs.SelectedItems[0].Tag.ToString())];
                    frmReportItemProperties f = new frmReportItemProperties(ref p_app, ref ReportItemSelected);
                    f.ShowDialog();

                    lvDocs.SelectedItems[0].SubItems[1].Text = GlobalObjects.AllowForPrintExport(ReportItemSelected.ReportItemLogInfo.MaxOriginals, ReportItemSelected.ReportItemLogInfo.Originals);
                    lvDocs.SelectedItems[0].SubItems[2].Text = GlobalObjects.AllowForPrintExport(ReportItemSelected.ReportItemLogInfo.MaxCopies, ReportItemSelected.ReportItemLogInfo.Copies);
                    break;
                case "ToolStripMenuPrintLog":
                    List<string> docs = mReports.Where(x => x.IsChecked).Select(x => x.DocumentNums + "#" + x.ReportName).ToList();
                    if (docs.Count == 0)
                        return;
                    string sDocs = string.Join("|", docs);
                    object reports = mReports.Select(x => x.ReportName).Distinct().ToArray();
                    p_app.ComCls.ShowPrintLog(sDocs, ref reports, false, false, false);
                    /*
                    ComClsReporting objRep = new ComClsReporting();
                    objRep.SetLocation(p_app.AppLocation.ServerName, p_app.AppLocation.AppName, p_app.AppLocation.SSPI, p_app.AppLocation.UserName, p_app.AppLocation.Password, p_app.AppLocation.Schema);
                    object parameters = new string[] { "DOCSREPORTS", sDocs, "DOCS" , ""};
                    object docnums = new string[] { String.Format("{0:yyyyMMdd HH:mm:ss}", DateTime.Now) };
                    objRep.AddReport("SrvPr_PrintLog", p_app.AppLocation.Schema, ref parameters, ref docnums, false, "", false);

                    objRep.AddMultiParameter("REPORTS",ref  reports);
                    objRep.ShowReportsInPreview();*/
                    break;
            }

        }
        public delegate void CloseForm(Form frm);
        public void CloseMe(Form frm)
        {
            //реализуем закрытие формы из потока
            if (frm.InvokeRequired)
            {
                CloseForm tsDelegate = new CloseForm(CloseMe);
                frm.Invoke(tsDelegate, new object[] { frm });
            }
            else
            {
                frm.Close();
            }
        }
        public delegate void RefreshFormInfo(frmWaiting frm, string strInfo);
        public void RefreshInfo(frmWaiting frm, string strInfo)
        {
            if (frm.InvokeRequired)
            {
                RefreshFormInfo tsDelegate = new RefreshFormInfo(RefreshInfo);
                frm.Invoke(tsDelegate, new object[]{
				frm,
				strInfo
			});
            }
            else
            {
                frm.RefreshInfo(strInfo);
            }
        }


        private void cmdSetDirForExport_Click(System.Object sender, System.EventArgs e)
        {
            FolderBrowserDialog objFolderBrowserDialog = new FolderBrowserDialog();
            if ((p_app.ExportDir != null))
                objFolderBrowserDialog.SelectedPath = p_app.ExportDir.FullName;
            objFolderBrowserDialog.ShowDialog();
            if (!string.IsNullOrEmpty(objFolderBrowserDialog.SelectedPath))
            {
                p_app.ExportDir = new DirectoryInfo(objFolderBrowserDialog.SelectedPath);
                GlobalObjects.ExportDir = p_app.ExportDir;
                lblSetDirForExport.Text = objFolderBrowserDialog.SelectedPath;
            }


        }

        private void lblSetDirForExport_Click(System.Object sender, System.EventArgs e)
        {
            cmdSetDirForExport_Click(cmdSetDirForExport, e);
        }

        private void lblSetDirForExport_MouseHover(object sender, System.EventArgs e)
        {
            ToolTipSetDirForExport.Show(lblSetDirForExport.Text, lblSetDirForExport);
        }


        private void cmdOriginalSettings_Click(System.Object sender, System.EventArgs e)
        {
            frmForcedCopy f = new frmForcedCopy(ref p_app);
            f.ShowDialog();
        }

        private void lvDocsContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void frmPrintoutDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mLaunchedFromPreview)
            {
                foreach (ReportItem item in mReports)
                {
                    item.ReportItemLogInfo.CopiesForPrint = 1;
                    item.ReportItemLogInfo.Refresh();
                    item.RefreshCtlReport(true);
                }
            }
        }
    }

}
