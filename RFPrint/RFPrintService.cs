using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Reporting;
using System.IO;
using System.Timers;
using System.Data.OleDb;
using System.Drawing.Printing;

namespace RFPrint
{
    public partial class RFPrintService : ServiceBase
    {
        public static int PeriodCount = 0;

        public static List<RFPrintExecPeriod> execPeriods = new List<RFPrintExecPeriod>();

        //private OleDbConnection conn;
        private List<Warehouse> warehouses;
        private Timer timer1 = null;
        private static string mAssemblyDirectory = "";
        public static string AssemblyDirectory
        {
            get
            {
                if (mAssemblyDirectory == "")
                    mAssemblyDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                return mAssemblyDirectory;
            }
        }
        private static string mAssemblyName = "";
        public static string AssemblyName
        {
            get
            {
                if (mAssemblyName == "")
                    mAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                return mAssemblyName;
            }
        }

        private static INI ini;
        private static string[] mWAREHOUSE_LIST;
        public static string[] WAREHOUSE_LIST
        {
            get { return mWAREHOUSE_LIST; }
        }


        private static string mDATASOURCE;
        public static string DATASOURCE
        {
            get { return mDATASOURCE; }
        }
        private static string mDATABASE;
        public static string DATABASE
        {
            get { return mDATABASE; }
        }
        private static string mUSERID;
        public static string USERID
        {
            get { return mUSERID; }
        }
        private static string mPASSWORD;
        public static string PASSWORD
        {
            get { return mPASSWORD; }
        }
        private static int mSCANPERIOD;
        public static int SCANPERIOD
        {
            get { return mSCANPERIOD; }
        }
        private static bool mDEBUGMODE;
        public static bool DEBUGMODE
        {
            get { return mDEBUGMODE; }
        }
        private static int mSTOPTIMEOUT;
        public static int STOPTIMEOUT
        {
            get { return mSTOPTIMEOUT; }
        }
        private static int mDEBUGSTARTDELAY;
        public static int DEBUGSTARTDELAY
        {
            get { return mDEBUGSTARTDELAY; }
        }
        private static bool mPRINTERTHREADS;
        public static bool PRINTERTHREADS
        {
            get { return mPRINTERTHREADS; }
        }


        private string sSqlRfPrintGetDocs;
        public RFPrintService()
        {
            InitializeComponent();
        }
        public static bool CreateConnection(out OleDbConnection conn)
        {
            try
            {
                string sConnStr = null;
                bool SSPI = false;
                sConnStr = GlobalObjects.strfmt("Provider=%1;%2;" + "User ID=%3;Password=%4;Initial Catalog=%5;" + "Data Source=%6;Application Name=%7;Workstation ID=%8", "SQLOLEDB.1", (SSPI ? "Integrated Security=SSPI" : "Persist Security Info=False"), USERID, PASSWORD, Constants.DB_LOG, DATASOURCE, AssemblyName, Environment.MachineName);
                conn = new OleDbConnection(sConnStr);
                conn.Open();
            }
            catch (Exception e)
            {
                WriteLog("error on create connection: ''" + e.Message + "''");
                conn = null;
                return false;
            }
            return true;
        }
        protected override void OnStart(string[] args)
        {
            ReadIni();
            if (DEBUGMODE)
            {
                WriteLog("DEBUGMODE: Sleeping " + DEBUGSTARTDELAY + " seconds...");
                System.Threading.Thread.Sleep(DEBUGSTARTDELAY*1000);
            }
            GlobalObjects.IsServerMode = true;
            Constants.ReadConstIni();
            if (DEBUGMODE)
                GlobalObjects.IsDebugMode = true;

            if (WAREHOUSE_LIST.Length == 0)
            {
                WriteLog("Service started, but warehouses not found");
                return;
            }
            //string sConnStr = null;
            //bool SSPI = false;
            //sConnStr = GlobalObjects.strfmt("Provider=%1;%2;" + "User ID=%3;Password=%4;Initial Catalog=%5;" + "Data Source=%6;Application Name=%7;Workstation ID=%8", "SQLOLEDB.1", (SSPI ? "Integrated Security=SSPI" : "Persist Security Info=False"), USERID, PASSWORD, Constants.DB_LOG, DATASOURCE, AssemblyName, Environment.MachineName);
            //conn = new OleDbConnection(sConnStr);
            //if (!TestConnection())
            //{
            //    WriteLog("Cannot connect to db on start");
            //}

            OleDbConnection conn=null;
            if(!CreateConnection(out conn))
                WriteLog("Cannot connect to db on start");

            warehouses = new List<Warehouse>();
            sSqlRfPrintGetDocs = "EXEC [" + DATABASE + "].DBO.RFPRINTGETDOCS ";
            foreach (string item in WAREHOUSE_LIST)
            {
                try
                {
                    int warehouseMaxDocuments = Convert.ToInt32(ini.IniReadValue(item, "MaxDocuments").Trim());
                    warehouses.Add(new Warehouse(item, warehouseMaxDocuments));
                    sSqlRfPrintGetDocs += "@MAXDOCS" + item + " = " + warehouseMaxDocuments.ToString() + ",";
                }
                catch (Exception e)
                {
                    WriteLog(e.Message);
                }

            }
            sSqlRfPrintGetDocs=sSqlRfPrintGetDocs.Trim(new char[] { ',' });
            if (DEBUGMODE)
                WriteLog("sql: ''" + sSqlRfPrintGetDocs + "''");
            WriteLog("Service started");
            conn.Close();
            timer1 = new Timer();
            this.timer1.Interval = Convert.ToDouble(SCANPERIOD * 1000); //timer intraval in milliseconds
            this.timer1.Elapsed += new ElapsedEventHandler(this.timer1_Tick);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OleDbConnection conn = null;
            if (CreateConnection(out conn))
            {
                try
                {
                    processWarehouses(conn);
                }
                catch (Exception ex)
                {
                    WriteLog("Unknown error in processWarehouses: " + ex.Message);
                }
                conn.Close();
            }
            else
            {
                WriteLog("Connection lost");
            }

            //if (TestConnection())
            //{
            //    processWarehouses();
            //}
            //else
            //{
            //    WriteLog("Connection lost");
            //}
        }

        private void processWarehouses(OleDbConnection conn)
        {
            
            int tmpPeriodCount = PeriodCount;
            execPeriods.RemoveAll(x => x.PeriodComplete == true);
            RFPrintExecPeriod tmpExecPeriod = new RFPrintExecPeriod(tmpPeriodCount);
            execPeriods.Add(tmpExecPeriod);
            PeriodCount += 1;

            if (DEBUGMODE)
                WriteLog("{" + tmpExecPeriod.PeriodNumber + "} start exec... Not finished threads: " + execPeriods.Count(x => x.PeriodComplete == false));

            try
            {
                OleDbCommand command = new OleDbCommand(sSqlRfPrintGetDocs, conn);
                OleDbDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    if (DEBUGMODE)
                        WriteLog("{" + tmpExecPeriod.PeriodNumber + "} reading docs...");
                    List<RFPrintDocument> rfPrintDocs = new List<RFPrintDocument>();
                    PrinterSettings.StringCollection printerStringCollection = PrinterSettings.InstalledPrinters;
                    List<string> printers = printerStringCollection.Cast<string>().ToList();
                    while (reader.Read())
                    {
                        try
                        {
                            rfPrintDocs.Add(new RFPrintDocument(printers, reader["WAREHOUSE"].ToString(), reader.GetInt32(reader.GetOrdinal("SERIALKEY")), reader["PTCID"].ToString(), reader["USERID"].ToString(),
                                                                reader["WHSEID"].ToString(), reader["SERVER"].ToString(), reader["DOCTYPE"].ToString(), reader["DOCKEY"].ToString(),
                                                                reader["PRNKEY"].ToString(), reader["KEY1"].ToString(), reader["KEY2"].ToString(), reader["KEY3"].ToString(),
                                                                reader["KEY4"].ToString(), reader["KEY5"].ToString(), reader.GetInt32(reader.GetOrdinal("STATUS")), reader["MESSAGE"].ToString(),
                                                                reader.GetDateTime(reader.GetOrdinal("ADDDATE")), reader["ADDWHO"].ToString(), reader["REPORTNAME"].ToString(),
                                                                reader["MAPING_DOCKEY"].ToString(), reader["MAPING_DOCTYPE"].ToString(), reader["MAPING_USERID"].ToString(),
                                                                reader["MAPING_KEY1"].ToString(), reader["MAPING_KEY2"].ToString(), reader["MAPING_KEY3"].ToString(), reader["MAPING_KEY4"].ToString(), reader["MAPING_KEY5"].ToString(), reader["MAPING_DOCNUMBER"].ToString()));
                        }
                        catch (Exception e)
                        {
                            OleDbCommand commandAddErrorMessage = new OleDbCommand("EXEC [" + DATABASE + "].DBO.RFPRINTUPDATEDOCS @MODE='MESSAGE', @SERIALKEYS='" + reader["WAREHOUSE"] + ":" + reader["SERIALKEY"] + "', @DATA='Error on create RFPrintDocument: " + e.Message + "'", conn);
                            commandAddErrorMessage.ExecuteNonQuery();
                            WriteLog("{" + tmpExecPeriod.PeriodNumber + "} Error on create RFPrintDocument: " + e.Message + "(WAREHOUSE: " + reader["WAREHOUSE"] + "; document: " + reader["SERIALKEY"] + ")");
                        }
                    }


                    if (rfPrintDocs.Count(x => x.PrinterExists == false) > 0)
                    {
                        string sSerialkeysWithoutPrinter = String.Join(",", rfPrintDocs.Where(x => x.PrinterExists == false).Select(x => x.WAREHOUSE + ":" + x.SERIALKEY).Distinct());
                        if (!String.IsNullOrEmpty(sSerialkeysWithoutPrinter))
                        {
                            if (DEBUGMODE)
                                WriteLog("{" + tmpExecPeriod.PeriodNumber + "} SerialkeysWithoutPrinter:" + sSerialkeysWithoutPrinter);
                            OleDbCommand commandAddMessage = new OleDbCommand("EXEC [" + DATABASE + "].DBO.RFPRINTUPDATEDOCS @MODE='MESSAGE_STATUS', @SERIALKEYS='" + sSerialkeysWithoutPrinter + "', @DATA='Printer not found in system', @STATUS = 4", conn);
                            commandAddMessage.ExecuteNonQuery();
                        }
                    }
                    rfPrintDocs.RemoveAll(x => x.PrinterExists == false);
                    if (DEBUGMODE) //ВРЕМЕННО
                    {
                        foreach (RFPrintDocument item in rfPrintDocs.Where(x => x.PrinterExists == true))
                        {
                            WriteLog("{" + tmpExecPeriod.PeriodNumber + "} WAREHOUSE:" + item.WAREHOUSE + ";REPORTNAME:" + item.REPORTNAME + ";DOCTYPE:" + item.DOCTYPE + ";SERIALKEY:" + item.SERIALKEY + ";PRNKEY:" + item.PRNKEY + ";DOCKEY:" + item.DOCKEY + ";KEY1:" + item.KEY1);
                        }
                    }
                    if (rfPrintDocs.Count == 0) //случай, когда не осталось ни одного документа
                    {
                        tmpExecPeriod.TryCompleteEmpty();
                        if (DEBUGMODE)
                            WriteLog("{" + tmpExecPeriod.PeriodNumber + "} all records removed: printer not found ");
                        reader.Close();
                        return;
                    }
                    //генерируем индикатор завершения фонового процесса
                    foreach (Warehouse item in warehouses)
                    {
                        if (RFPrintService.PRINTERTHREADS)
                            tmpExecPeriod.AddWarehouses_Printers(item.Name, rfPrintDocs.Where(x => x.WAREHOUSE == item.Name).Select(x => x.PRNKEY).Distinct().ToArray());
                        else
                            tmpExecPeriod.AddWarehouses_Printers(item.Name, new string[] { "NoPrinter" });
                    }
                    //ОСНОВНОЙ ЦИКЛ ДЛЯ ЗАПУСКА В РАБОТУ ЗАДАНИЙ НА ПЕЧАТЬ
                    foreach (Warehouse item in warehouses)
                    {
                        if (rfPrintDocs.Count(x => x.WAREHOUSE == item.Name) > 0)
                        {
                            item.Processing(rfPrintDocs.Where(x => x.WAREHOUSE == item.Name).ToList<RFPrintDocument>(), conn, tmpExecPeriod);
                        }
                        else if (!RFPrintService.PRINTERTHREADS)
                            tmpExecPeriod.SetWarehousePeriodComplete(item.Name, "");
                    }

                }
                else
                {
                    tmpExecPeriod.TryCompleteEmpty();
                    if (DEBUGMODE)
                        WriteLog("{" + tmpExecPeriod.PeriodNumber + "} records not found ");
                }
                reader.Close();

            }
            catch (Exception e1)
            {
                WriteLog("{" + tmpExecPeriod.PeriodNumber + "} error in RFPrintService.processWarehouses:" + e1.Message);
            }

        }

        protected override void OnStop()
        {
            WriteLog("Service stoping start...");
            timer1.Enabled = false;
            DateTime dtStopStarted = DateTime.Now;
            while (DateTime.Now.Subtract(dtStopStarted).TotalSeconds < STOPTIMEOUT & execPeriods.Count(x => x.PeriodComplete == false) > 0)
            {
                WriteLog("Waiting for stoping other threads: " + execPeriods.Count(x => x.PeriodComplete == false).ToString());
                System.Threading.Thread.Sleep(2000);
            }
            if (execPeriods.Count(x => x.PeriodComplete == false) > 0)
                WriteLog("Service stoped because stoping timeout " + STOPTIMEOUT.ToString() + " aboved;" + " not stoping threads: " + execPeriods.Count(x => x.PeriodComplete == false).ToString());
            WriteLog("Service stoped");
        }







        //private bool TestConnection()
        //{
        //    conn.ResetState();
        //    if (conn.State == ConnectionState.Open)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            conn.Open();
        //        }
        //        catch (Exception e1)
        //        {
        //            WriteLog("Connection lost:" + e1.Message);
        //        }
        //        if (conn.State == ConnectionState.Open)
        //            return true;
        //        else
        //            return false;
        //    }
        //}


        private static void ReadIni()
        {
            try
            {
                ini = new INI(AssemblyDirectory + @"\RFPrint.ini");
                mWAREHOUSE_LIST = ini.IniReadValue("WarehouseParameters", "WAREHOUSE_LIST").Trim().Split(';');
                for (int i = 0; i < mWAREHOUSE_LIST.Length; i++)
                {
                    mWAREHOUSE_LIST[i].Trim(' ');
                }
                mDATASOURCE = ini.IniReadValue("Connection", "DataSource").Trim();
                mDATABASE = ini.IniReadValue("Connection", "DataBase").Trim();
                mUSERID = ini.IniReadValue("Connection", "UserId").Trim();
                mPASSWORD = ini.IniReadValue("Connection", "Password").Trim();
                mSCANPERIOD = Convert.ToInt32(ini.IniReadValue("Settings", "ScanPeriod").Trim());
                mDEBUGMODE = Convert.ToBoolean(ini.IniReadValue("Settings", "DebugMode").Trim());
                mSTOPTIMEOUT = Convert.ToInt32(ini.IniReadValue("Settings", "StopTimeout").Trim());
                mPRINTERTHREADS = Convert.ToBoolean(ini.IniReadValue("Settings", "PrinterThreads").Trim());
                mDEBUGSTARTDELAY = Convert.ToInt32(ini.IniReadValue("Settings", "DebugStartDelay").Trim());
            }
            catch (Exception e)
            {
                WriteLog("Error when read service INI:" + e.Message);
            }
        }
        public static void WriteLog(string text)
        {
            /*
            using (StreamWriter sw = new StreamWriter(AssemblyDirectory + @"\" + String.Format("{0:yyyy.MM}_", DateTime.Now) + "RFPrint_LOG.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(String.Format("[{0:yyyy.MM.dd HH:mm:ss.ffff}]: ", DateTime.Now) + text);
                sw.Flush();
            }
            */
            using (FileStream fs = new FileStream(AssemblyDirectory + @"\" + String.Format("{0:yyyy.MM}_", DateTime.Now) + "RFPrint_LOG.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    fs.Seek(0, SeekOrigin.End);
                    sw.WriteLine(String.Format("[{0:yyyy.MM.dd HH:mm:ss.ffff}]: ", DateTime.Now) + text);
                    sw.Flush();
                    //Thread.Sleep(3000);
                }
            }

        }
    }
}
