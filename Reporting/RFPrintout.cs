using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Threading;
using System.Data.OleDb;

namespace Reporting
{
    public class RFPrintout
    {
        private ReportApplication p_app;
        private List<RFPrintDocument> RfPrintDocs;
        public int PeriodCount;
        public string PrinterName;

        public float dtInitTime;
        public float dtConstructTime = 0; public DateTime dtConstructTimeStart;
        public float dtGenerateTime = 0; public DateTime dtGenerateTimeStart;
        public RFPrintout(ref ReportApplication _app, ref List<RFPrintDocument> rfPrintDocs)
        {
            p_app = _app;
            RfPrintDocs = rfPrintDocs;
        }
        public static void StartBackground(object obj)
        {
            /*
            foreach (object item in obj)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
                { BackgroundPrint(item); }));
            }*/

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            { BackgroundPrint(obj); }));
        }

        private static void BackgroundPrint(object objThread) //Функция для потока, передаем объект, строго в соответствии с требуемыми полями
        {
            RFPrintExecPeriod execPeriod = null;
            Location location =null;

            string printerName = "";
            bool PRINTERTHREADS = true;
            try
            {
                List<object> args = (List<object>)objThread;
                location = (Location)args[0];
                //открываем соединение
                location.CreateConnectionString();
                if(!location.OpenConnection(true,true))
                    MessageBox.Show("{" + execPeriod.PeriodNumber + "}could not open connection (WAREHOUSE: " + location.Schema + ";PRINTER:" + printerName + ")");

                List<RFPrintDocument> rfPrintDocs = (List<RFPrintDocument>)args[1];
                execPeriod = (RFPrintExecPeriod)args[2];
                printerName = (string)args[3];
                PRINTERTHREADS = (bool)args[4];
                DateTime dtThreadStart = DateTime.Now;

                if (GlobalObjects.IsDebugMode)
                    MessageBox.Show("{" + execPeriod.PeriodNumber + "}start BackgroundPrint...(WAREHOUSE: " + location.Schema + ";PRINTER:" + printerName + ";DocsCount:" + rfPrintDocs.Count+ ")");

                Constants.ReadConst(ref location);
                ReportApplication RepApp = new ReportApplication();
                RepApp.AppLocation = location;

                int j;
                ReportItem Rep;
                string[] ReportParams;
                string[] ReportDocs;
                string ReportName;
                string Warehouse;
                string PrinterName = "";


                DateTime dtReportInitTimeStart = DateTime.Now;
                float dtReportInitTime = 0;


                foreach (RFPrintDocument item in rfPrintDocs)
                {
                    try
                    {
                        if (GlobalObjects.IsDebugMode)
                            MessageBox.Show("{" + execPeriod.PeriodNumber + "}Add report to app...(WAREHOUSE: " + location.Schema + ";PRINTER:" + printerName + ";REPORTNAME:" + item.REPORTNAME + ";DocNums: '" + string.Join(",", item.reportDocs) + "')");
                        Rep = new ReportItem(ref RepApp, item.REPORTNAME, item.WAREHOUSE, RepApp.AppLocation.UserName, item.reportParameters.ToArray(), item.reportDocs, true, false); //здесь дополнительное преобразование, лишние потери
                        Rep.PrinterName = item.PRNKEY;
                        Rep.rfPrintDocument = item;
                        if (Rep.CreateSuccessfull)
                            RepApp.Reports.Add(Rep);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("{" + execPeriod.PeriodNumber + "}error in new ReportItem: " + e.Message + "(WAREHOUSE: " + location.Schema + ";PRINTER:" + printerName + ";REPORTNAME:" + item.REPORTNAME + ";DocIsPrint:" + item.IsPrinted + ";DocNums: '" + string.Join(",", item.reportDocs) + "')");
                        OleDbCommand commandAddMessage = new OleDbCommand("EXEC [PRD1].DBO.RFPRINTUPDATEDOCS @MODE='MESSAGE', @SERIALKEYS='" + RepApp.AppLocation.Schema + ":" + item.SERIALKEY + "', @DATA='error on creating ReportItem: " + e.Message + "'", RepApp.AppLocation.Connection);
                        commandAddMessage.ExecuteNonQuery();
                    }

                }
                RFPrintout rfPrint = null;
                try
                {

                    dtReportInitTime = dtReportInitTime + GlobalObjects.DateDiff(dtReportInitTimeStart);
                    if (RepApp.Reports.Count == 0 | !RepApp.AppLocation.ConnectionSucceed)
                        return;

                    rfPrint = new RFPrintout(ref RepApp, ref rfPrintDocs);
                    rfPrint.PeriodCount = execPeriod.PeriodNumber;
                    rfPrint.PrinterName = printerName;
                }
                catch (Exception e)
                {
                    MessageBox.Show("{" + execPeriod.PeriodNumber + "}error in BackgroundPrint(new RFPrintout): " + e.Message + "(WAREHOUSE: " + location.Schema + ";PRINTER:" + printerName + ")");
                }

                rfPrint.PrintReportsRF(true, null, true, false, "");

                //обработка не напечатанных документов.
                try
                {
                    if (rfPrintDocs.Count(x => x.IsPrinted == false) > 0)
                    {
                        string sSerialkeysNotOK = String.Join(",", rfPrintDocs.Where(x => x.IsPrinted == false).Select(x => x.WAREHOUSE + ":" + x.SERIALKEY).Distinct());
                        if (!String.IsNullOrEmpty(sSerialkeysNotOK))
                        {
                            if (GlobalObjects.IsDebugMode)
                                MessageBox.Show("{" + execPeriod.PeriodNumber + "}exec UPDATEDOCS (MESSAGE_STATUS=5), SERIALKEYS:" + sSerialkeysNotOK + " (WAREHOUSE: " + location.Schema + ";PRINTER:" + printerName + ")");
                            OleDbCommand commandStatusNotOK = new OleDbCommand("EXEC [PRD1].DBO.RFPRINTUPDATEDOCS @MODE='MESSAGE_STATUS', @SERIALKEYS='" + sSerialkeysNotOK + "', @STATUS = 5", RepApp.AppLocation.Connection);
                            commandStatusNotOK.ExecuteNonQuery();
                        }
                    } 
                }
                catch (Exception e)
                {
                    MessageBox.Show("{" + execPeriod.PeriodNumber + "}error in BackgroundPrint, UPDATEDOCS (MESSAGE_STATUS_5): " + e.Message + "(WAREHOUSE:" + location.Schema + ";PRINTER:" + printerName + ")");
                }

                string sSerialkeysOK = String.Join(",", rfPrintDocs.Where(x => x.IsPrinted == true).Select(x => x.WAREHOUSE + ":" + x.SERIALKEY).Distinct());
                if (!String.IsNullOrEmpty(sSerialkeysOK))
                {
                    if (GlobalObjects.IsDebugMode)
                        MessageBox.Show("{" + execPeriod.PeriodNumber + "}exec UPDATEDOCS, SERIALKEYS:" + sSerialkeysOK + " (WAREHOUSE: " + location.Schema + ";PRINTER:" + printerName + ")");
                    OleDbCommand commandStatusOK = new OleDbCommand("EXEC [PRD1].DBO.RFPRINTUPDATEDOCS @MODE='STATUSOK', @SERIALKEYS='" + sSerialkeysOK + "'", RepApp.AppLocation.Connection);
                    commandStatusOK.ExecuteNonQuery();
                }
  


                float dtSoftwarePrintTime = 0;
                float dtInitServerRepotTime = 0;
                float dtAddParamsTime = 0;
                float dtGeneratePagesTime = 0;
                float dtInitSoftwarePrintTime = 0;
                float dtOtherInitOperations = 0;

                float dtGenerateOtherTime = 0;
                float dtGeneratePrintingTime = 0;


                /*
                foreach (ReportItem item in RepApp.Reports)
                {
                    dtSoftwarePrintTime += item.dtSoftwarePrintTime;
                    dtInitServerRepotTime += item.dtInitServerRepotTime;
                    dtAddParamsTime += item.dtAddParamsTime;
                    dtGeneratePagesTime += item.dtGeneratePagesTime;
                    dtInitSoftwarePrintTime += item.dtInitSoftwarePrintTime;
                    dtOtherInitOperations += item.dtOtherInitOperations;
                    dtGenerateOtherTime += item.dtGenerateOtherTime;
                    dtGeneratePrintingTime += item.dtGeneratePrintingTime;
                }
                */

                /*
                GlobalObjects.AddLogMessage("№" + "" + ": [ReportInit]:" + dtReportInitTime +
                    "; [ReportConstruct]:" + rfPrint.dtConstructTime +
                    "; [ReportGenerate]:" + rfPrint.dtGenerateTime +
                    "; [InitSoftwarePrint]:" + dtInitSoftwarePrintTime +
                    "; [AddParams]:" + dtAddParamsTime +
                    "; [OtherInitOperations]:" + dtOtherInitOperations +
                    "; [SoftwarePrint]:" + dtSoftwarePrintTime +
                    "; [GenerateOther]:" + dtGenerateOtherTime +
                    "; [GeneratePages]:" + dtGeneratePagesTime +
                    "; [GeneratePrinting]:" + dtGeneratePrintingTime +
                    ": [ДЛИТЕЛЬНОСТЬ]:" + GlobalObjects.DateDiff(dtThreadStart).ToString());*/


                RepApp.AppLocation.Connection.Close();
                RepApp.Reports.Clear();
                RepApp.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show("{" + execPeriod.PeriodNumber + "}error in BackgroundPrint: " + e.Message + "(WAREHOUSE:" + location.Schema + ";PRINTER:" + printerName + ")");
            }
            finally
            {
                if (execPeriod != null & /*location != null &*/ printerName != null)
                    execPeriod.SetWarehousePeriodComplete(location.Schema, (PRINTERTHREADS ? printerName : ""));
            }

            Thread.Sleep(1);
        }

        public void PrintReportsRF(bool IsPrint, object printFormat, bool printOriginals = true, bool IsDuplex = false, string ExportPath = "")
        {
            PrinterSettings currentPrinterSettings=null;
            ExportFormat PrintFormat = new ExportFormat();
            if (IsPrint)
            {

            }
            else
            {
                PrintFormat = (ExportFormat)printFormat;
                p_app.ExportDir = new System.IO.DirectoryInfo(ExportPath);
            }


            foreach (ReportItem objReportItem in p_app.Reports)
            {
                if (objReportItem.IsChecked)
                {
                    if (IsPrint)
                    {
                        try
                        {
                            if(GlobalObjects.IsDebugMode)
                                MessageBox.Show("{" + PeriodCount + "}Construct...(WAREHOUSE: " + objReportItem.rfPrintDocument.WAREHOUSE + ";PRINTER:" + objReportItem.rfPrintDocument.PRNKEY + ";SERIALKEY:" + objReportItem.rfPrintDocument.SERIALKEY + ";REPORTNAME:" + objReportItem.rfPrintDocument.REPORTNAME + ";RFDOCSTATUS:" + objReportItem.rfPrintDocument.IsPrinted + ")");
                            dtConstructTimeStart = DateTime.Now;
                            objReportItem.ConstructCtlReport(-1);
                            dtConstructTime = dtConstructTime + GlobalObjects.DateDiff(dtConstructTimeStart);
                            dtGenerateTimeStart = DateTime.Now;

                            if (currentPrinterSettings == null)
                            {
                                currentPrinterSettings = new PrinterSettings();
                                currentPrinterSettings.PrinterName = objReportItem.PrinterName;
                            }
                            else if (currentPrinterSettings.PrinterName != objReportItem.PrinterName)
                            {
                                currentPrinterSettings = new PrinterSettings();
                                currentPrinterSettings.PrinterName = objReportItem.PrinterName;
                            }
                            if (GlobalObjects.IsDebugMode)
                                MessageBox.Show("{" + PeriodCount + "}PrintMeSoftware...(WAREHOUSE: " + objReportItem.rfPrintDocument.WAREHOUSE + ";PRINTER:" + objReportItem.rfPrintDocument.PRNKEY + ";SERIALKEY:" + objReportItem.rfPrintDocument.SERIALKEY + ";REPORTNAME:" + objReportItem.rfPrintDocument.REPORTNAME + ";RFDOCSTATUS:" + objReportItem.rfPrintDocument.IsPrinted + ")");
                            objReportItem.PrintMeSoftware(currentPrinterSettings, printOriginals, IsDuplex);

                            objReportItem.rfPrintDocument.IsPrinted = true;
                            dtGenerateTime = dtGenerateTime + GlobalObjects.DateDiff(dtGenerateTimeStart);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("{" + PeriodCount + "}error in rendering ReportItem: " + e.Message + "(WAREHOUSE: " + objReportItem.rfPrintDocument.WAREHOUSE + ";PRINTER:" + objReportItem.rfPrintDocument.PRNKEY + ";SERIALKEY:" + objReportItem.rfPrintDocument.SERIALKEY + ";REPORTNAME:" + objReportItem.rfPrintDocument.REPORTNAME + ";RFDOCSTATUS:" + objReportItem.rfPrintDocument.IsPrinted + ")");
                            OleDbCommand commandAddMessage = new OleDbCommand("EXEC [PRD1].DBO.RFPRINTUPDATEDOCS @MODE='MESSAGE', @SERIALKEYS='" + p_app.AppLocation.Schema + ":" + objReportItem.rfPrintDocument.SERIALKEY + "', @DATA='error on rendering ReportItem: " + e.Message + "'", p_app.AppLocation.Connection);
                            commandAddMessage.ExecuteNonQuery();
                        }


                    }
                    else
                    {
                        //ветка не доработана.
                        objReportItem.ConstructCtlReport(-1);
                        objReportItem.ExportMeSoftware(printOriginals, PrintFormat, p_app.ExportDir);
                    }
                    //objReportItem = null; ??? возможно требуется обнуление!!!!
                }
            }
            currentPrinterSettings = null;
            //p_app.Reports.Clear();
            //p_app.Dispose();

        }
    }
}
