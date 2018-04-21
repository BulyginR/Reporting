using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Reporting
{
    [Guid("F1CC4EE4-A8AB-4A2A-A5EC-A574366916BA")]
    public interface ComClsReportingInterface
    {
        void SetLocation(string ServerName, string AppName, bool SSPI, string UserName, string Password, string Schema = "");
        void SetToDebugMode();
        void SetToServerMode();
        void ShowSettings();
        void DeleteAllReports();
        void AddReport(string ReportName, string Warehouse, ref object ReportParams, ref object DocumentNums, bool ShowOnlyInPreview = true, string UserName = "", bool ShowParameterPrompts = false, bool ForcedPrint = false, string Storerkey = "");
        void AddMultiParameter(string ParameterName, ref object MultiValue);
        void ShowReportsInPreview();
        void ShowDialog(bool SetToExport = false, int ExportFormat = 1, bool SetToCopy = false);
        void ShowPrintLog(string sDocs, ref object reports, bool showParameterPromts = false, bool showDocs = false, bool initDates = false);
        void ShowReportInBuilder(bool submitOnlyOnViewReport = false, string buttonOkText = "OK", bool additionalQuestion = false, string additionalQuestionText = "", int initPromptHeight = 0, bool changeDropDownHeight = false, int formHeight = 0, int formWidth = 0, bool tryReloadMultiParams = false, bool canExport = false);
        bool IsParametersValidated();
        string GetSingleParameterValue(string ParameterName);
        bool GetSingleParameterIsNull(string ParameterName);
        string[] GetMultiParameterValue(string ParameterName);
        void CloseReportBuilder();
    }

    [Guid("D472CF10-B63F-4B29-9288-8B11F5EA3E12"),
        InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface ComClsReportingEvents
    {
    }

    [Guid("58195FCD-1C32-4234-A1C5-E572C4DA9AB2"),
        ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(ComClsReportingEvents))]
    public class ComClsReporting : ComClsReportingInterface
    {
        public ReportApplication p_app ;//= new ReportApplication();
        private ReportItem mLastAddedReport;
        private frmReportBuilder mFrmReportBuilder;

        public ComClsReporting()
            : base()
        {
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Чтение ini файла...");
            Constants.ReadConstIni();
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Чтение ini файла...OK");

            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Создание экземпляра приложения...");
            p_app = new ReportApplication(this);
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Создание экземпляра приложения...OK");
        }
        public void SetLocation(string ServerName, string AppName, bool SSPI, string UserName, string Password, string Schema = "")
        {
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Создание экземпляра Location...");
            Location Location = new Location(ServerName, AppName, SSPI, UserName, Password, Schema);
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Создание экземпляра Location...OK");

            if (!Location.OpenConnection(true))
                Environment.Exit(1);//тестируем соединение
            Constants.ReadConst(ref Location);
            p_app.AppLocation = Location;
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Соединение присвоено экземпляру приложения");
        }
        public void SetToDebugMode()
        {
            GlobalObjects.IsDebugMode = true;
        }
        public void SetToServerMode()
        {
            GlobalObjects.IsServerMode = true;
        }

        public void ShowSettings()
        {
            frmReportsManager frmSettings = new frmReportsManager(p_app);
            frmSettings.Show();
        }

        public void DeleteAllReports()
        {
            p_app.Reports.RemoveRange(0, p_app.Reports.Count);
        }

        public void AddReport(string ReportName, string Warehouse, ref object ReportParams, ref object DocumentNums, bool ShowOnlyInPreview = true, string UserName = "", bool ShowParameterPrompts = false, bool ForcedPrint = false, string Storerkey = "")
        {
            if (!p_app.AppLocation.ConnectionSucceed)
                return;
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Создание отчета ''" + ReportName + "''");

            object[] resultReportParams = (object[])ReportParams;//Array.ConvertAll((object[])ReportParams, x => x == null ? "" : x.ToString());
            string[] resultDocumentNums = Array.ConvertAll((object[])DocumentNums, x => x.ToString());
            ReportItem Rep = new ReportItem(ref p_app, ReportName, Warehouse, (string.IsNullOrEmpty(UserName) ? p_app.AppLocation.UserName : UserName), resultReportParams, resultDocumentNums, ShowOnlyInPreview, ShowParameterPrompts, ForcedPrint, Storerkey);
            int AddedReport = 0;
            if (Rep.CreateSuccessfull)
            {
                p_app.Reports.Add(Rep);
                AddedReport = p_app.Reports.Count - 1;
            }
            mLastAddedReport = p_app.Reports[AddedReport];
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Отчет ''" + ReportName + " [" + Rep.DocumentNums + "]'' создан");
        }



        public void AddMultiParameter(string ParameterName, ref object MultiValue)
        {
            //метод добавления мультипараметра к последнему добавленному отчету
            string[] resultReportParam = Array.ConvertAll((object[])MultiValue, x => x.ToString());
            if (resultReportParam.Length == 0)
                return;

            if (GlobalObjects.IsDebugMode)
            {
                foreach (string item in resultReportParam)
                {
                    GlobalObjects.AddLogMessage("Значение мультипараметра: '" + item + "'");
                }
            }
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Добавляется мультипараметр ...");
            ReportParameter p = new ReportParameter(ParameterName, resultReportParam, false);
            mLastAddedReport.AddParameter(p);
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Мультипараметр добавлен");

        }

        public void ShowPrintLog(string sDocs, ref object reports, bool showParameterPromts = false, bool showDocs = false,bool initDates = false)
        {
            
            ComClsReporting objRep = new ComClsReporting();
            objRep.SetLocation(p_app.AppLocation.ServerName, p_app.AppLocation.AppName, p_app.AppLocation.SSPI, p_app.AppLocation.UserName, p_app.AppLocation.Password, p_app.AppLocation.Schema);
            object parameters = (showDocs ? new string[] { "DOCSREPORTS", sDocs } :new string[] { "DOCSREPORTS", sDocs, "DOCS", "" });
            object docnums = new string[] { String.Format("{0:yyyyMMdd HH:mm:ss}", DateTime.Now) };
            objRep.AddReport(Constants.REPORT_HISTRORY, p_app.AppLocation.Schema, ref parameters, ref docnums, false, "", showParameterPromts);
            objRep.AddMultiParameter("REPORTS", ref reports);
            if(initDates)
            {
                object initParam = new string[] {"1"};
                objRep.AddMultiParameter("InitDates", ref initParam);
            }
            objRep.ShowReportsInPreview();
        }

        public void ShowReportsInPreview()
        {
            if (p_app.Reports.Count == 0 | !p_app.AppLocation.ConnectionSucceed)
                return;
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Запуск предпросмотра");
            frmReportsPreview f = new frmReportsPreview(ref p_app);
            f.Show();

        }

        public void ShowDialog(bool SetToExport = false, int ExportFormat = 1, bool SetToCopy = false)
        {
            if (p_app.Reports.Count == 0 | !p_app.AppLocation.ConnectionSucceed)
                return;
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Запуск диалога печати");
            frmPrintoutDialog f = new frmPrintoutDialog(ref p_app, false, SetToExport, ExportFormat, SetToCopy);
            f.Show();
        }
        /*
        public void PrintRF(bool IsPrint, object printFormat, bool printOriginals = true, bool IsDuplex = false, string ExportPath = "")
        {
            if (p_app.Reports.Count == 0 | !p_app.AppLocation.ConnectionSucceed)
                return;
            RFPrintout rfPrint = new RFPrintout(ref p_app);
            rfPrint.PrintReportsRF(false, ExportFormat.Pdf, true, false, ExportPath);
        }
        */
        public void ShowReportInBuilder(bool submitOnlyOnViewReport = false, string buttonOkText = "OK", bool additionalQuestion = false, string additionalQuestionText = "", int initPromptHeight = 0, bool changeDropDownHeight = false, int formHeight = 0, int formWidth = 0, bool tryReloadMultiParams = false, bool canExport = false)
        {
            if (p_app.Reports.Count == 0 | !p_app.AppLocation.ConnectionSucceed)
                return;

            frmReportBuilder f = new frmReportBuilder(p_app.Reports[0], submitOnlyOnViewReport, buttonOkText, additionalQuestion, additionalQuestionText, initPromptHeight, changeDropDownHeight, formHeight, formWidth, tryReloadMultiParams, canExport);
            mFrmReportBuilder = f;
            f.ShowDialog();

        }
        public bool IsParametersValidated()
        {
            if (mFrmReportBuilder == null)
            {
                return false;
            }
            if (mFrmReportBuilder.CloseOk == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string GetSingleParameterValue(string ParameterName)
        {
            string functionReturnValue = "";
            if (mFrmReportBuilder == null)
                return functionReturnValue;
            if (mFrmReportBuilder.CloseOk == false)
                return functionReturnValue;
            return mFrmReportBuilder.objReportItem.GetSingleParameterValue(ParameterName);
        }
        public bool GetSingleParameterIsNull(string ParameterName)
        {
            bool functionReturnValue = true;
            if (mFrmReportBuilder == null)
                return functionReturnValue;
            if (mFrmReportBuilder.CloseOk == false)
                return functionReturnValue;
            return mFrmReportBuilder.objReportItem.GetSingleParameterIsNull(ParameterName);
        }

        public string[] GetMultiParameterValue(string ParameterName)
        {
            string[] functionReturnValue = null;
            if (mFrmReportBuilder == null)
                return functionReturnValue;
            if (mFrmReportBuilder.CloseOk == false)
                return functionReturnValue;
            return mFrmReportBuilder.objReportItem.GetMultiParameterValue(ParameterName);
        }
        public void CloseReportBuilder()
        {
            if ((mFrmReportBuilder != null))
                mFrmReportBuilder.Close();

        }
    }
}

