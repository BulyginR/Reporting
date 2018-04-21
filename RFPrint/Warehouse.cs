using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using Reporting;

namespace RFPrint
{
    class Warehouse
    {
        public string Name;
        public int MaxDocuments;
        public Location location;
        public Warehouse(string name, int maxDocuments)
        {
            Name = name;
            MaxDocuments = maxDocuments;
            location = new Location(RFPrintService.DATASOURCE, RFPrintService.AssemblyName, false, RFPrintService.USERID, RFPrintService.PASSWORD, name, false);
        }
        public bool Processing(List<RFPrintDocument> rfPrintDocs, OleDbConnection conn, RFPrintExecPeriod execPeriod)
        {
            bool processing = true;
            //List<string> serialkeysOK = new List<string>();
            try
            {
                List<string> printers = rfPrintDocs.Select(x => x.PRNKEY).Distinct().ToList<string>();
                //location.Connection = conn;
                //location.ConnectionSucceed = true;
                if (RFPrintService.PRINTERTHREADS)
                {
                    foreach (string printerName in printers)
                    {
                        if (RFPrintService.DEBUGMODE)
                            RFPrintService.WriteLog("{" + execPeriod.PeriodNumber + "} start printing (WAREHOUSE:" + this.Name + ";PRINTER:" + printerName + ")");
                        List<object> obj = new List<object>();
                        //ТУТ ВАЖЕН ПОРЯДОК ДОБАВЛЕНИЯ ЭЛЕМЕНТОВ!
                        //obj.Add(this.location);
                        obj.Add(new Location(RFPrintService.DATASOURCE, RFPrintService.AssemblyName, false, RFPrintService.USERID, RFPrintService.PASSWORD, Name, false));
                        obj.Add(rfPrintDocs.Where(x => x.PRNKEY == printerName).ToList<RFPrintDocument>());
                        obj.Add(execPeriod);
                        obj.Add(printerName);
                        obj.Add(RFPrintService.PRINTERTHREADS);
                        RFPrintout.StartBackground(obj);
                    }
                }
                else
                { //код по смылсу повторяет предудущую ветку. Только без деления на потоки в зависимости от принтера
                    if (RFPrintService.DEBUGMODE)
                        RFPrintService.WriteLog("{" + execPeriod.PeriodNumber + "} start printing (WAREHOUSE:" + this.Name + ";PRINTER:" + "NoPrinterMode" + ")");
                    List<object> obj = new List<object>();
                    //ТУТ ВАЖЕН ПОРЯДОК ДОБАВЛЕНИЯ ЭЛЕМЕНТОВ!
                    obj.Add(new Location(RFPrintService.DATASOURCE, RFPrintService.AssemblyName, false, RFPrintService.USERID, RFPrintService.PASSWORD, Name, false));
                    obj.Add(rfPrintDocs.OrderBy(x=> x.PRNKEY).ToList<RFPrintDocument>());
                    obj.Add(execPeriod);
                    obj.Add("NoPrinter");
                    obj.Add(RFPrintService.PRINTERTHREADS);
                    RFPrintout.StartBackground(obj);
                }
            }
            catch (Exception e)
            {
                RFPrintService.WriteLog("{" + execPeriod.PeriodNumber + "} error in Warehouse.Processing:" + e.Message + "(" + this.Name + ")");
                processing =  false;
            }

            /*finally
            {
                string sSerialkeysOK = String.Join(",", serialkeysOK.Distinct());
                if (!String.IsNullOrEmpty(sSerialkeysOK))
                {
                    if (RFPrintService.DEBUGMODE)
                        RFPrintService.WriteLog("SerialkeysOK:" + sSerialkeysOK);
                    OleDbCommand commandAddMessage = new OleDbCommand("EXEC [" + RFPrintService.DATABASE + "].DBO.RFPRINTUPDATEDOCS @MODE='STATUSOK', @SERIALKEYS='" + sSerialkeysOK + "'", conn);
                    commandAddMessage.ExecuteNonQuery();
                }
            }*/
            return processing;
        }
    }
}
