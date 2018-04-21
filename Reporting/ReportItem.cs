
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.ReportRendering;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using ExtensionMethods;
using System.Reflection;
namespace Reporting
{
    public class ReportItem : TabHeaderObject
    {
        public override void RefreshTab(bool IsLoaded, ref TabPage tabPage)
        //реализация метода класса TabHeaderObject
        {
            if (IsLoaded) { return; }
            ConstructCtlReport(0, false, null, tabPage);
        }
        public override void CloseTab()
        //реализация метода класса TabHeaderObject
        {
            IsChecked = false;
        }
        private ReportApplication p_app;

        public DateTime dtSoftwarePrintTimeStart; public float dtSoftwarePrintTime = 0;
        public DateTime dtInitSoftwarePrintTimeStart; public float dtInitSoftwarePrintTime = 0;
        public DateTime dtInitServerRepotTimeStart; public float dtInitServerRepotTime = 0;

        public float dtAddParamsTime = 0;
        public float dtGeneratePagesTime = 0;
        public float dtOtherInitOperations = 0;
        public float dtGenerateOtherTime = 0;
        public float dtGeneratePrintingTime = 0;

        private object[] marrReportparameters;

        private frmReportBuilder mFrmReportBuilder = null;

        public bool IsChecked = true;

        private object[] marrDocumentNums;
        private ArrayList mReportparameters = new ArrayList();
        private ReportParameterCollection mReportParameterCollection = new ReportParameterCollection();

        private bool mShowParameterPrompts = false;
        private string mReportAddUser;
        private bool mIsOldChange = false;
        private bool mForcedPrint = false;

        private string mOldChangeUserName;
        private string mOldChangeUserFullName;

        private string mReportPath;

        private string mWarehouse;
        private string mReportName;
        private int mPageCount = 0;
        private string mStorerkey = "";

        private string mReportCaption;
        private ReportItemLogInfo mReportItemLogInfo;

        private bool mShowOnlyInPreview;

        public Form frmContainer;

        //только для РЧ печати
        public string PrinterName;
        public RFPrintDocument rfPrintDocument;

        Uri ServerUrl = new Uri(Constants.SERVER_URL);

        public bool IsLoadInterface;

        private bool mCreateSuccessfull;
        private ToolStripButton mPrintbutton;

        private ToolStripButton mPreviewbutton;

        private ReportParameterCollection mReportparametersSelected;
        private ReportViewer withEventsField_CtlReportViewer;
        public ReportViewer CtlReportViewer
        {
            get { return withEventsField_CtlReportViewer; }
            set
            {
                if (withEventsField_CtlReportViewer != null)
                {
                    withEventsField_CtlReportViewer.SubmittingParameterValues -= CtlReportViewer_SubmittingParameterValues;
                }
                withEventsField_CtlReportViewer = value;
                if (withEventsField_CtlReportViewer != null)
                {
                    withEventsField_CtlReportViewer.SubmittingParameterValues += CtlReportViewer_SubmittingParameterValues;
                }
            }

        }
        public ServerReport serverReport;

        private enum PrintingMsgStatus
        {
            PrintOriginal = 0,
            PrintCopy = 1,
            PrintCancel = 2
        }


        static ReportItem()
        {
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Статический конструктор класса ''ReportItem''...ОК");
            //Public WithEvents CtlReportViewer As ReportViewer

        }

        public ReportItemLogInfo ReportItemLogInfo
        {
            get { return mReportItemLogInfo; }
        }
        public ReportParameterCollection ReportparametersSelected
        {
            get { return mReportparametersSelected; }
        }


        public bool CreateSuccessfull
        {
            get { return mCreateSuccessfull; }
        }
        public string ReportPath
        {
            get { return mReportPath; }
        }
        public string ReportName
        {
            get { return mReportName; }
        }
        public string Warehouse
        {
            get { return mWarehouse; }
        }
        public string ReportCaption
        {
            get { return mReportCaption; }
        }
        public ArrayList Reportparameters
        {
            get { return mReportparameters; }
        }



        public object arrDocumentNums
        {
            get { return marrDocumentNums; }
        }
        public string DocumentNums
        {
            get
            {
                string strTmp = "";
                for (int i = 0; i < marrDocumentNums.Length; i += 1)
                {
                    strTmp = strTmp + marrDocumentNums[i] + ",";
                }
                strTmp = strTmp.Left(strTmp.Length - 1);
                return strTmp;
            }
        }
        public void AddParameter(ReportParameter p)
        {
            Reportparameters.Add(p);
        }


        public ReportItem(ref ReportApplication _app, string reportName, string Warehouse, string reportAddUser, object[] arrReportparameters, string[] arrDocumentNums, bool ShowOnlyInPreview = true, bool ShowParameterPrompts = false, bool ForcedPrint = false, string Storerkey = "")
        {
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Инициализация экземпляра класса ''ReportItem''...");
            p_app = _app;


            if (arrDocumentNums.Length == 0)
            {
                MessageBox.Show("Не указан номер документа");
                return;
            }
            if (string.IsNullOrEmpty(reportName))
            {
                MessageBox.Show("Не указан отчет");
                return;
            }
            else if (arrReportparameters.Length % 2 != 0)
            {
                MessageBox.Show("Данные для отчета сформированы неверно");
                return;
            }
            

            //Добавление массивов параметров, параметров для скрытия и номеров док-ов  в объект
            marrReportparameters = arrReportparameters;
            mShowParameterPrompts = ShowParameterPrompts;
            marrDocumentNums = arrDocumentNums;
            mWarehouse = Warehouse;


            mReportAddUser = reportAddUser;
            mReportName = reportName;
            mShowOnlyInPreview = ShowOnlyInPreview;
            mForcedPrint = ForcedPrint;
            mStorerkey = Storerkey;

            OleDbCommand command = new OleDbCommand("SELECT top 1 * FROM dbo.SrvPr_REPORTS  WITH(NOLOCK) where REPORTNAME='" + reportName + "';", p_app.AppLocation.Connection);

            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Запрос к серверу ''" + p_app.AppLocation.ServerName + "'' экземпляра класса ''ReportItem''...");
            OleDbDataReader reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    mReportPath = reader.GetString(reader.GetOrdinal("REPORTPATH"));
                    mReportCaption = reader.GetString(reader.GetOrdinal("REPORTCAPTION"));
                    int maxOriginals = reader.GetInt32(reader.GetOrdinal("ORIGINAL"));
                    int maxCopies = reader.GetInt32(reader.GetOrdinal("COPIES"));
                    int maxOriginalsExportPdf = reader.GetInt32(reader.GetOrdinal("ORIGINALEXPORTPDF"));
                    int maxCopiesExportPdf = reader.GetInt32(reader.GetOrdinal("COPIESEXPORTPDF"));
                    int maxOriginalsExportXls = reader.GetInt32(reader.GetOrdinal("ORIGINALEXPORTXLS"));
                    int maxCopiesExportXls = reader.GetInt32(reader.GetOrdinal("COPIESEXPORTXLS"));
                    bool Autocopy = reader.GetBoolean(reader.GetOrdinal("AUTOCOPY"));

                    mReportItemLogInfo = new ReportItemLogInfo(ref p_app, ref marrDocumentNums, ref  reportName, maxOriginals, maxCopies, maxOriginalsExportPdf, maxCopiesExportPdf, maxOriginalsExportXls, maxCopiesExportXls, Autocopy,
                    Warehouse);

                }
            }
            //привязка полей класса TabHeaderObject
            base.tabCaption = mReportCaption + " [ " + DocumentNums + " ]";
            reader.Close();

            mCreateSuccessfull = true;
            if (marrReportparameters.Length > 0)
            {
                for (int i = 0; i < marrReportparameters.Length; i += 2)
                {
                    //Create the report parameters
                    ReportParameter p;
                    p = new ReportParameter(marrReportparameters[i].ToString(), marrReportparameters[i + 1] == null ? null: marrReportparameters[i + 1].ToString(), false);
                    mReportparameters.Add(p);
                }
            }
        }


        public void ConstructCtlReport(int TabNum, bool AddParametersVisible = false, frmReportBuilder _FrmReportBuilder = null, TabPage tabPage = null) //ref убрал, зачем было?????????
        {
            // инициализация контрола ReportViewer
            try
            {

                //ServerReport ServerReport = CtlReportViewer.ServerReport;
                dtInitServerRepotTimeStart = DateTime.Now;

                serverReport = new ServerReport();
                serverReport.ReportServerUrl = ServerUrl;

                serverReport.ReportPath = mReportPath;
                dtInitServerRepotTime = GlobalObjects.DateDiff(dtInitServerRepotTimeStart);
                ReportParameterCollection collParams = new ReportParameterCollection();
                ReportParameter p1 = new ReportParameter("SqlServer", p_app.AppLocation.ServerName, false);
                //устанавливаем параметры сервер, пользователя и склад
                collParams.Add(p1);// serverReport.SetParameters(p1);
                if (!string.IsNullOrEmpty(mWarehouse))
                {
                    ReportParameter p2 = new ReportParameter("Warehouse", mWarehouse, false);
                    collParams.Add(p2); //serverReport.SetParameters(p2);
                }
                SetUserFullNameForReport("", collParams);

                //также меняется при авторизации старшего смены (AuthorizationForcedCopy)
                //Application.DoEvents();  //?????зачем нужно????

                if (mReportparameters.Count > 0)
                {
                    foreach (ReportParameter Param in mReportparameters)
                    {
                        if (AddParametersVisible)
                            Param.Visible = true;
                        collParams.Add(Param);//serverReport.SetParameters(Param);
                    }
                }

                mReportParameterCollection = collParams;
                //serverReport.SetParameters(collParams);
                if ((_FrmReportBuilder != null))
                {
                    mFrmReportBuilder = _FrmReportBuilder;
                    ConstructCtlReportViewerFromServerReport(); //инициализация ReportViewControl
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, p_app.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //это касается только интерфейса, поэтому при печати/экспорте не требуется
            if (TabNum != -1)
            {
                ConstructCtlReportViewerFromServerReport(); //инициализация ReportViewControl
                CtlReportViewer.Visible = false;
                CtlReportViewer.BorderStyle = BorderStyle.None;
                CtlReportViewer.Padding = new Padding(0);
                CtlReportViewer.Margin = new Padding(0);
                CtlReportViewer.ShowDocumentMapButton = false;
                CtlReportViewer.ShowPrintButton = false;
                CtlReportViewer.ShowContextMenu = false;
                CtlReportViewer.ShowFindControls = false;
                CtlReportViewer.ShowExportButton = false;
                CtlReportViewer.ShowRefreshButton = false;
                if (!mShowParameterPrompts)
                    CtlReportViewer.ShowParameterPrompts = false;
                CtlReportViewer.Dock = DockStyle.Fill;
                tabPage.Controls.Add(CtlReportViewer);
                //добавляем кнопки в тулбар:
                ToolStrip toolStrip = (ToolStrip)CtlReportViewer.Controls.Find("toolStrip1", true)[0];

                mPrintbutton = new ToolStripButton();
                mPrintbutton.Image = toolStrip.Items[toolStrip.Items.IndexOfKey("print")].Image;
                mPrintbutton.ToolTipText = "Печать";
                mPrintbutton.Height = toolStrip.Height;
                mPrintbutton.Width = toolStrip.Height;

                ToolStripButton Refreshbutton = new ToolStripButton();
                Refreshbutton.Image = toolStrip.Items[toolStrip.Items.IndexOfKey("refresh")].Image;
                Refreshbutton.ToolTipText = "Обновить";
                Refreshbutton.Height = toolStrip.Height;
                Refreshbutton.Width = toolStrip.Height;

                ToolStripButton PrintAllbutton = new ToolStripButton();
                PrintAllbutton.ToolTipText = "Печать всех отчетов";
                PrintAllbutton.Text = "Печатать все";
                PrintAllbutton.Height = toolStrip.Height;
                PrintAllbutton.Width = (int)(toolStrip.Height * 2.5);


                mPreviewbutton = new ToolStripButton();
                mPreviewbutton.Image = toolStrip.Items[toolStrip.Items.IndexOfKey("printPreview")].Image;
                mPreviewbutton.ToolTipText = "В режим просмотра печати";
                mPreviewbutton.CheckState = (mShowOnlyInPreview ? CheckState.Checked : CheckState.Unchecked);
                mPreviewbutton.Height = toolStrip.Height;
                mPreviewbutton.Width = toolStrip.Height;

                if (mShowOnlyInPreview)
                {
                    CtlReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                    mPreviewbutton.Visible = false;
                    CtlReportViewer.ZoomMode = ZoomMode.FullPage;
                }
                else
                {
                    CtlReportViewer.ZoomMode = ZoomMode.Percent;
                    CtlReportViewer.ZoomPercent = 75;
                }


                ToolStripSeparator Separator1 = new ToolStripSeparator();
                ToolStripSeparator Separator2 = new ToolStripSeparator();
                ToolStripSeparator Separator3 = new ToolStripSeparator();

                toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				Separator1,
				mPrintbutton,
				Separator2,
				PrintAllbutton,
				Separator3,
				mPreviewbutton,
				Refreshbutton
			});

                mPrintbutton.Click += CtlReportViewer_OnPrint;
                PrintAllbutton.Click += CtlReportViewer_OnPrintAll;
                Refreshbutton.Click += CtlReportViewer_OnRefresh;
                mPreviewbutton.Click += CtlReportViewer_OnPreview;

                this.ReportItemLogInfo.Refresh();//проверяем логирование еще раз
                CtlReportViewer.Visible = true;
                RefreshCtlReport(true);
            }

        }
        private void ConstructCtlReportViewerFromServerReport()
        {
            try
            {
                CtlReportViewer = null;
                CtlReportViewer = new ReportViewer();
                CtlReportViewer.Messages = new AdvancedReportViewer.Utils.RussianReportViewerMessages();
                CtlReportViewer.ProcessingMode = ProcessingMode.Remote;
                //дублируем инициализацию server report для визуальной части
                CtlReportViewer.ServerReport.ReportServerUrl = serverReport.ReportServerUrl;
                CtlReportViewer.ServerReport.ReportPath = serverReport.ReportPath;
                CtlReportViewer.ServerReport.SetParameters(mReportParameterCollection);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, p_app.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public void CtlReportViewer_OnPreview(object sender, System.EventArgs e)
        {
            if (CtlReportViewer.DisplayMode == DisplayMode.PrintLayout)
            {
                CtlReportViewer.SetDisplayMode(DisplayMode.Normal);
                mPreviewbutton.CheckState = CheckState.Unchecked;
                mPreviewbutton.ToolTipText = "В режим просмотра печати";
            }
            else
            {
                CtlReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                mPreviewbutton.CheckState = CheckState.Checked;
                mPreviewbutton.ToolTipText = "В обычный режим";
            }

        }

        public void CtlReportViewer_OnPrint(object sender, System.EventArgs e)
        {
            frmPrintoutDialog f = new frmPrintoutDialog(ref p_app, this);
            f.ShowDialog();

        }

        public void CtlReportViewer_OnPrintAll(object sender, System.EventArgs e)
        {
            frmPrintoutDialog f = new frmPrintoutDialog(ref p_app, true);
            
            f.Show();
            frmContainer.Close();
        }
        public void CtlReportViewer_OnRefresh(object sender, System.EventArgs e)
        {
            this.ReportItemLogInfo.Refresh();
            RefreshCtlReport(true);
        }


        public void RefreshCtlReport(bool RefreshInterface = false)
        {
            //если надо перерисовываем интерфейс контрола

            if (RefreshInterface)
            { 
                Microsoft.Reporting.WinForms.ReportParameter p = default(Microsoft.Reporting.WinForms.ReportParameter);

                if (this.ReportItemLogInfo.CanPrintOriginal == true)
                {
                    p = new ReportParameter("IsCopy", "false", false);
                    CtlReportViewer.ServerReport.SetParameters(p);
                }
                else
                {
                    p = new ReportParameter("IsCopy", "true", false);
                    CtlReportViewer.ServerReport.SetParameters(p);
                }

                CtlReportViewer.RefreshReport();
                IsLoadInterface = true;
            }


        }
        public void PrintMeSoftware(PrinterSettings printerSettings, bool PrintOriginal, bool IsDuplex = false)
        {
            //mIsOldChange = False 'сброс авторизации старшего смены перед каждой печатью/экспортом для отчета 
            //теперь сбрасываем после записи в лог
            ReportItemLogInfo.CopiesForPrint = printerSettings.Copies;

            ReportItemLogInfo.Refresh();
            bool IsOriginal = false;

            PrintingMsgStatus _printingMsgStatus = PrintingMsgStatus.PrintCancel;

            //по умолчанию ссылаемся на созданные ранее настройки. 
            //Но если нужно менять кол-во экземпляров - в переменную reportPrinterSettings размещаем новый объект PrinterSettings
            PrinterSettings reportPrinterSettings = printerSettings;

            if (PrintOriginal)
            {
                //пользователь выбрал печать оригиналов

                if (!(ReportItemLogInfo.CanPrintOriginal == true))
                {
                    reportPrinterSettings = (PrinterSettings)printerSettings.Clone(); //чтобы не трогать поле Copies глобального для всех отчетов экземпляра PrinterSettings
                    if (ReportItemLogInfo.MaxOriginals - ReportItemLogInfo.Originals > 0)
                    {
                        //печатаем если возможно часть оригиналов
                        PrinterSettings printerSettingsClone = (PrinterSettings)reportPrinterSettings.Clone();  //ПОЧЕМУ НУЖЕН CAST?
                        printerSettingsClone.Copies = (short)(ReportItemLogInfo.MaxOriginals - ReportItemLogInfo.Originals);
                        SoftwarePrintReportCaller(printerSettingsClone, true, IsDuplex);
                        reportPrinterSettings.Copies = (short)(reportPrinterSettings.Copies - printerSettingsClone.Copies);
                    }

                    _printingMsgStatus = AuthorizationForcedCopy(reportPrinterSettings.Copies, false, "ORIGINAL", ReportCaption + " [" + DocumentNums + "]");

                    if (_printingMsgStatus == PrintingMsgStatus.PrintCancel)
                    {
                        return;

                    }
                    else if (_printingMsgStatus == PrintingMsgStatus.PrintCopy)
                    {
                        ReportItemLogInfo.CopiesForPrint = reportPrinterSettings.Copies;
                        ReportItemLogInfo.Refresh();


                        if (!(ReportItemLogInfo.CanPrintCopies == true))
                        {
                            if (ReportItemLogInfo.MaxCopies - ReportItemLogInfo.Copies > 0)
                            {
                                //печатаем если возможно часть копий
                                PrinterSettings printerSettingsCloneForCopy = (PrinterSettings)reportPrinterSettings.Clone();
                                printerSettingsCloneForCopy.Copies = (short)(ReportItemLogInfo.MaxCopies - ReportItemLogInfo.Copies);
                                SoftwarePrintReportCaller(printerSettingsCloneForCopy, false, IsDuplex);
                                reportPrinterSettings.Copies = (short)(reportPrinterSettings.Copies - printerSettingsCloneForCopy.Copies);
                            }

                            _printingMsgStatus = AuthorizationForcedCopy(reportPrinterSettings.Copies, false, "", ReportCaption + " [" + DocumentNums + "]");
                            if (_printingMsgStatus == PrintingMsgStatus.PrintCopy)
                            {
                                IsOriginal = false;
                            }
                            else if (_printingMsgStatus == PrintingMsgStatus.PrintCancel)
                            {
                                return;
                            }
                        }

                    }
                    else if (_printingMsgStatus == PrintingMsgStatus.PrintOriginal)
                    {
                        IsOriginal = true;
                    }

                }
                else
                {
                    IsOriginal = true;
                }

            }
            else
            {
                //пользователь выбрал печать копий
                if (!(ReportItemLogInfo.CanPrintCopies == true))
                {
                    reportPrinterSettings = (PrinterSettings)printerSettings.Clone(); //чтобы не трогать поле Copies глобального для всех отчетов экземпляра PrinterSettings
                    if (ReportItemLogInfo.MaxCopies - ReportItemLogInfo.Copies > 0)
                    {
                        //печатаем если возможно часть копий
                        PrinterSettings printerSettingsCloneForCopy = (PrinterSettings)reportPrinterSettings.Clone();
                        printerSettingsCloneForCopy.Copies = (short)(ReportItemLogInfo.MaxCopies - ReportItemLogInfo.Copies);
                        SoftwarePrintReportCaller(printerSettingsCloneForCopy, false, IsDuplex);
                        reportPrinterSettings.Copies = (short)(reportPrinterSettings.Copies - printerSettingsCloneForCopy.Copies);
                    }

                    _printingMsgStatus = AuthorizationForcedCopy(reportPrinterSettings.Copies, false, "", ReportCaption + " [" + DocumentNums + "]");

                    if (_printingMsgStatus == PrintingMsgStatus.PrintCopy)
                    {
                        IsOriginal = false;
                    }
                    else if (_printingMsgStatus == PrintingMsgStatus.PrintCancel)
                    {
                        return;
                    }
                }
                else
                {
                    IsOriginal = false;
                }
            }


            SoftwarePrintReportCaller(reportPrinterSettings, IsOriginal, IsDuplex);

        }
        private void SoftwarePrintReportCaller(PrinterSettings printerSettings, bool IsOriginal, bool IsDuplex = false)
        {

            if (GlobalObjects.IsDebugMode) { GlobalObjects.AddLogMessage("Отправка на печать документа (" + printerSettings.Copies.ToString() + "экз) ''" + ReportCaption + " [" + DocumentNums + "]" + "''"); }
            dtInitSoftwarePrintTimeStart = DateTime.Now;
            ReportParameterCollection reportParameterCollection = CopyReportParameterCollection();
            SoftwarePrintReport prntRep = new SoftwarePrintReport(serverReport, printerSettings, reportParameterCollection, IsOriginal, ReportCaption + " [" + DocumentNums + "]", IsDuplex, mIsOldChange ? mOldChangeUserFullName : "",(mShowParameterPrompts?mReportparametersSelected:null));
            dtInitSoftwarePrintTime = GlobalObjects.DateDiff(dtInitSoftwarePrintTimeStart);
            dtAddParamsTime = prntRep.dtAddParamsTime;
            dtOtherInitOperations = prntRep.dtOtherInitOperations;
            dtSoftwarePrintTimeStart = DateTime.Now;
            mPageCount = prntRep.PrintReport();
            dtGeneratePagesTime = prntRep.dtGeneratePagesTime;
            dtGenerateOtherTime = prntRep.dtGenerateOtherTime;
            dtGeneratePrintingTime = prntRep.dtGeneratePrintingTime;


            dtSoftwarePrintTime = GlobalObjects.DateDiff(dtSoftwarePrintTimeStart);
            WriteLog(IsOriginal, 0, printerSettings.Copies);
            if (mIsOldChange)
            {
                mIsOldChange = false;
                //сбрасываем UserFullName если старший смены
                SetUserFullNameForReport();
            }

        }



        internal void ExportMeSoftware(bool ExportOriginal, ExportFormat ExportFormat, DirectoryInfo CurrExportDir)
        {
            //mIsOldChange = False 'сброс авторизации старшего смены перед каждой печатью/экспортом для отчета 
            //теперь сбрасываем после записи в лог
            ReportItemLogInfo.Refresh();

            bool CanExportOriginal = false;
            bool CanExportCopies = false;
            CanExportOriginal = ReportItemLogInfo.CanExportOriginal(ExportFormat);
            CanExportCopies = ReportItemLogInfo.CanExportCopies(ExportFormat);


            bool IsOriginal = false;

            PrintingMsgStatus _printingMsgStatus = PrintingMsgStatus.PrintCancel;

            if (ExportOriginal)
            {
                //пользователь выбрал экспорт оригинала

                if (CanExportOriginal == true)
                {
                    IsOriginal = true;
                }
                else
                {
                    _printingMsgStatus = AuthorizationForcedCopy(1, true, "ORIGINAL", ReportCaption + " [" + DocumentNums + "]");

                    if (_printingMsgStatus == PrintingMsgStatus.PrintCancel)
                    {
                        return;

                    }
                    else if (_printingMsgStatus == PrintingMsgStatus.PrintCopy)
                    {
                        if (CanExportCopies == true)
                        {
                            IsOriginal = false;
                        }
                        else
                        {
                            _printingMsgStatus = AuthorizationForcedCopy(1, true, "", ReportCaption + " [" + DocumentNums + "]");
                            if (_printingMsgStatus == PrintingMsgStatus.PrintCopy)
                            {
                                IsOriginal = false;
                            }
                            else if (_printingMsgStatus == PrintingMsgStatus.PrintCancel)
                            {
                                return;
                            }
                        }

                    }
                    else if (_printingMsgStatus == PrintingMsgStatus.PrintOriginal)
                    {
                        IsOriginal = true;
                    }
                }
            }
            else
            {
                //пользователь выбрал экспорт копий
                if (CanExportCopies == true)
                {
                    IsOriginal = false;
                }
                else
                {
                    _printingMsgStatus = AuthorizationForcedCopy(1, true, "", ReportCaption + " [" + DocumentNums + "]");

                    if (_printingMsgStatus == PrintingMsgStatus.PrintCopy)
                    {
                        IsOriginal = false;
                    }
                    else if (_printingMsgStatus == PrintingMsgStatus.PrintCancel)
                    {
                        return;
                    }
                }
            }

            SoftwareExportReportCaller(IsOriginal, ExportFormat, CurrExportDir);

        }

        private void SoftwareExportReportCaller(bool IsOriginal, ExportFormat ExportFormat, DirectoryInfo CurrExportDir)
        {
            ReportParameterCollection reportParameterCollection = CopyReportParameterCollection();
            SoftwareExport prntRep = new SoftwareExport(serverReport, ExportFormat, reportParameterCollection, CurrExportDir, IsOriginal, ReportCaption + " [" + DocumentNums + "]", mIsOldChange ? mOldChangeUserFullName : "", (mShowParameterPrompts ? mReportparametersSelected : null));
            prntRep.PrintReport();

            WriteLog(IsOriginal, ExportFormat, 1);
            if (mIsOldChange)
            {
                mIsOldChange = false;
                //сбрасываем UserFullName если старший смены
                SetUserFullNameForReport();
            }
        }
        private ReportParameterCollection CopyReportParameterCollection()
        {
            ReportParameterCollection col = new ReportParameterCollection();
            foreach (ReportParameter item in mReportParameterCollection)
            {
                col.Add(item);
            }
            return col;
        }


        private PrintingMsgStatus AuthorizationForcedCopy(int Amount, bool IsExport, string sMode = "", string RepInfo = "")
        {
            string strMsg = null;
            if (sMode == "ORIGINAL")
            {
                if (mReportItemLogInfo.Autocopy == true & p_app.IsOldChange == false)
                    return PrintingMsgStatus.PrintCopy;
                strMsg = "" + (IsExport ? "Дальнейший экспорт" : "Дальнейшая печать") + " оригиналов отчета ''" + RepInfo + "'' запрещен(а). " + Environment.NewLine + "Если Вы все же хотите " + (IsExport ? "экспортировать оригиналы" : "распечатать оригиналы (" + Amount + "шт.)") + "," + Environment.NewLine + "нажмите ''Да'' чтобы ввести логин/пароль старшего смены. " + Environment.NewLine + "Нажмите ''Нет'' чтобы распечатать/экспортировать копии." + Environment.NewLine + "Нажмите ''Отмена'' для отметы печати.";
                frmShowMessage MsgFrm = new frmShowMessage(frmShowMessage.ShowMessageMode.YesNoCancel, strMsg);
                MsgFrm.ShowDialog();
                switch (MsgFrm.Answer)
                {
                    //MsgBox(strMsg, MsgBoxStyle.YesNoCancel)
                    case frmShowMessage.ShowMessageAnswer.vbYes:

                        break;
                    case frmShowMessage.ShowMessageAnswer.vbNo:
                        return PrintingMsgStatus.PrintCopy;
                    case frmShowMessage.ShowMessageAnswer.vbCancel:
                        return PrintingMsgStatus.PrintCancel;
                }
                MsgFrm.Close();
            }
            else
            {
                strMsg = "" + (IsExport ? "Дальнейший экспорт" : "Дальнейшая печать") + " копий отчета ''" + RepInfo + "'' запрещен(а). " + Environment.NewLine + "Если Вы все же хотите " + (IsExport ? "экспортировать копии" : "распечатать копии (" + Amount + "шт.)") + "," + Environment.NewLine + "нажмите ''OK'' чтобы ввести логин/пароль старшего смены. " + Environment.NewLine + "Нажмите ''Отмена'' для отметы печати.";
                frmShowMessage MsgFrm = new frmShowMessage(frmShowMessage.ShowMessageMode.OkCancel, strMsg);
                MsgFrm.ShowDialog();
                switch (MsgFrm.Answer)
                {
                    //MsgBox(strMsg, MsgBoxStyle.OkCancel)
                    case frmShowMessage.ShowMessageAnswer.vbOK:

                        break;
                    case frmShowMessage.ShowMessageAnswer.vbCancel:
                        return PrintingMsgStatus.PrintCancel;
                }
                MsgFrm.Close();
            }
            frmForcedCopy f = new frmForcedCopy(ref p_app);
            if (p_app.IsOldChange)
            {
                f.closeOk = true;
                f.CurUserName = p_app.OldChangeCurUserName;
                f.CurUserFullName = p_app.OldChangeCurUserFullName;
            }
            else
            {
                f.ShowDialog();
            }
            if (f.closeOk)
            {
                mOldChangeUserName = f.CurUserName;
                mOldChangeUserFullName = f.CurUserFullName;
                f.Close();
                mIsOldChange = true;
                //если подтвердили авторизацию старшего смены для отчета. Тогда ADDWHO=старший смены.
                SetUserFullNameForReport(f.CurUserFullName);
                //устанавливаем полное имя старшего смены (после mIsOldChange = True!!!)
                if (sMode == "ORIGINAL")
                {
                    return PrintingMsgStatus.PrintOriginal;
                }
                else
                {
                    return PrintingMsgStatus.PrintCopy;
                }

            }
            else
            {
                f.Close();
                return PrintingMsgStatus.PrintCancel;
            }

        }

        private void SetUserFullNameForReport(string UserFullName = "", ReportParameterCollection collParams = null)
        {
            //ReportParameter p1 = default(ReportParameter);
            //p_app.AppLocation.UserFullName
            UserFullName = (mIsOldChange ? UserFullName :
                (p_app.AppLocation.UserName != this.mReportAddUser & this.mReportAddUser != "" ? 
                // если пользователь отчета не совпадает с пользователем приложения, то ищем полное имя пользователя отчета
                    p_app.AppLocation.UserFullNameReport(this.mReportAddUser) : p_app.AppLocation.UserFullName));

            ReportParameter p1 = new ReportParameter("UserName", UserFullName, false);
            if (!(collParams == null))
            {
                collParams.Add(p1);
            }
            else
            {
                serverReport.SetParameters(p1);
            }

        }



        private void WriteLog(bool IsOriginal, ExportFormat PrintFormat, int PrintAmount)
        {
            for (int i = 0; i < marrDocumentNums.Length; i += 1)
            {
                OleDbCommand Command = new OleDbCommand("INSERT INTO " + mWarehouse + (!string.IsNullOrEmpty(mWarehouse) ? "." : "") +
                    "SrvPr_PRINTLOG (REPORTNAME,DOCNUMBER,ORIGINAL,PRINTAMOUNT,PRINTFORMAT,ADDWHO,EDITWHO,FORCEDCOPY,WORKSTATION,FORCEDPRINT,PAGECOUNT,STORERKEY) VALUES ('" + 
                    mReportName + "','" + 
                    marrDocumentNums[i] + "', '" + 
                    IsOriginal.ToString().ToLower() + "', '" + 
                    PrintAmount + "', '" + 
                    (int)PrintFormat + "', '" + 
                    (mIsOldChange ? mOldChangeUserName : mReportAddUser) + "', '" + 
                    (mIsOldChange ? mOldChangeUserName : mReportAddUser) + "', '" +
                    (mIsOldChange ? "1" : "0") + "', '" + 
                    Environment.MachineName + "', '" +
                    (mForcedPrint ? "1" : "0") + "'," + 
                    mPageCount + ", '" +
                    mStorerkey + "')", 

                    p_app.AppLocation.Connection);
                try
                {
                    Command.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, p_app.AppLocation.AppName);
                }

            }
        }

        private void CtlReportViewer_SubmittingParameterValues(object sender, Microsoft.Reporting.WinForms.ReportParametersEventArgs e)
        {
            if ((mFrmReportBuilder != null))
            {
                if (mFrmReportBuilder.IsOkButtonClicked)
                {
                    mReportparametersSelected = e.Parameters;
                }
            }
            else
                mReportparametersSelected = e.Parameters;

        }

        public string GetSingleParameterValue(string ParameterName)
        {
            string functionReturnValue = null;
            if (mReportparametersSelected == null)
                return functionReturnValue;
            foreach (ReportParameter param in mReportparametersSelected)
            {
                if (param.Name == ParameterName)
                    return param.Values[0];
            }
            return functionReturnValue;

        }

        public bool GetSingleParameterIsNull(string ParameterName)
        {
            bool functionReturnValue = true;
            if (mReportparametersSelected == null)
                return functionReturnValue;
            foreach (ReportParameter param in mReportparametersSelected)
            {
                if (param.Name == ParameterName)
                    if (param.Values[0] == null)
                        return true;
                    else
                        return false;
            }
            return functionReturnValue;

        }

        public string[] GetMultiParameterValue(string ParameterName)
        {
            string[] functionReturnValue = null;
            if (mReportparametersSelected == null)
                return functionReturnValue;
            string[] arrStr = null;
            foreach (ReportParameter param in mReportparametersSelected)
            {
                if (param.Name == ParameterName)
                {
                    int i = 0;
                    foreach (string strVal in param.Values)
                    {
                        Array.Resize(ref arrStr, i + 1);
                        arrStr[i] = param.Values[i];
                        i += 1;
                    }
                    return arrStr;
                }

            }
            return arrStr;
            //return functionReturnValue;???????????????
        }


    }
}