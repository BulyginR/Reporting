using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Reporting;
using System.Threading;
using System.Drawing.Printing;
using System.IO;
using System.Management;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int PotokNum = 1;
        Location location;
        //private TabControlSE mTabControl;
        public Form1()
        {
            InitializeComponent();
        }
        //ComClsReporting objComClsReporting = new ComClsReporting();
        private void Form1_Load(object sender, EventArgs e)
        {
            Constants.ReadConstIni();
            location = new Location("SSQL10", "PrintRF", false, "sa", "sa", "WH1");
            if (!location.OpenConnection(true))
                Environment.Exit(1);
            Constants.ReadConst(ref location);


            //List<TabHeaderObject> col = new List<TabHeaderObject>();
            //col.Add(new testGrid("111111"));
            //col.Add(new testGrid("222222"));
            //col.Add(new testGrid("333333"));
            //col.Add(new testGrid("444444"));
            //col.Add(new testGrid("555555"));
            //TabControlSE mTabControl = new TabControlSE(col, panel1);
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //GlobalObjects.IsDebugMode = true;
            string[] parameters1 = new string[] { "CaseId", "VRFK000568355", "Orderkey", "0002113210" };
            object objParameters1 = (object)parameters1;
            string[] parameters2 = new string[] { "CaseId", "VRFK000568356", "Orderkey", "0002113209" };
            object objParameters2 = (object)parameters1;
            string[] docs3 = new string[] { "0002113210_TEST_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "/VRFK000568355" };
            object objDocs3 = (object)docs3;
            string[] docs19 = new string[] { "0002113209_TEST_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "/VRFK000568356" };
            object objDocs19 = (object)docs19;
            object[] objThread1 = new object[] { (int)1, PotokNum.ToString(), DateTime.Now, "PackCase", "WH3", objParameters1, docs3 };
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            { this.BackgroundPrint(objThread1); }));
            //if (GlobalObjects.IsDebugMode)
            //GlobalObjects.AddLogMessage("№" + PotokNum.ToString() + "[Старт]");
            PotokNum++;

            object[] objThread2 = new object[] { (int)1, PotokNum.ToString(), DateTime.Now, "PackCaseList", "WH3", objParameters2, docs19 };
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            { this.BackgroundPrint(objThread2); }));
            //if (GlobalObjects.IsDebugMode)
            //GlobalObjects.AddLogMessage("№" + PotokNum.ToString() + "[Старт]");
            PotokNum++;

            /*
            object[] objThread2 = new object[] { (int)1, PotokNum.ToString(), "PackCaseList", "WH3", objParameters2, docs19 };
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {this.BackgroundPrint(objThread2);}));
            //if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("№" + PotokNum.ToString() + "[Старт]");
            PotokNum++;*/

            /*
            ThreadPool.QueueUserWorkItem(new WaitCallback((ThreadProc) =>
            {
                this.BackgroundPrint(objThread2);
            }), (object)PotokNum);
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Старт печати отчета №" + PotokNum.ToString());
            PotokNum++;*/
            /*
            Thread thread1 = new Thread(this.BackgroundPrint);
            thread1.Priority = ThreadPriority.Highest;
            thread1.Name = "thread [" + PotokNum.ToString() + "]";
            PotokNum++;
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Старт потока: " + thread1.Name);
            thread1.Start(objThread1);//передача параметра в поток

            Thread thread2 = new Thread(this.BackgroundPrint);
            thread2.Priority = ThreadPriority.Highest;
            thread2.Name = "thread [" + PotokNum.ToString() + "]";
            PotokNum++;
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Старт потока: " + thread2.Name);
            thread2.Start(objThread2);//передача параметра в поток
           */
            this.textBox1.Text = this.textBox1.Text + String.Format("{0:HH:mm:ss.ffff}", DateTime.Now) + Environment.NewLine;
            //this.timer1.Enabled = false;
        }
        void BackgroundPrint(object objThread)//Функция потока, передаем параметр
        {
            object[] args = (object[])objThread;
            int ReportCount = (int)args[0];
            string threadInfo = (string)((object)args[1]);
            DateTime dtThreadStart = DateTime.Now;
            ReportApplication RepApp = new ReportApplication();
            RepApp.AppLocation = location;
            int j;
            ReportItem Rep;
            DateTime dtReportInitTimeStart;
            float dtReportInitTime = 0;
            for (int i = 1; i <= ReportCount; i++)
            {
                dtReportInitTimeStart = DateTime.Now;
                j = 3 + (i - 1) * 4;
                object ReportParams = (object)args[j + 2];
                object ReportDocs = (object)args[j + 3];
                string[] info = (string[])ReportDocs;

                if (GlobalObjects.IsDebugMode)
                    GlobalObjects.AddLogMessage(threadInfo + ":" + info[0]);
                Rep = new ReportItem(ref RepApp, (string)args[j], (string)args[j + 1], RepApp.AppLocation.UserName, (string[])ReportParams, (string[])ReportDocs, true, false);
                if (Rep.CreateSuccessfull)
                {
                    RepApp.Reports.Add(Rep);
                }
                dtReportInitTime = dtReportInitTime + GlobalObjects.DateDiff(dtReportInitTimeStart);
            }

            if (RepApp.Reports.Count == 0 | !RepApp.AppLocation.ConnectionSucceed)
                return;
            /*
            RFPrintout rfPrint = new RFPrintout(ref RepApp);
            PrinterSettings prntSett = new PrinterSettings();
            rfPrint.PrintReportsRF(true, prntSett, false, false, @"D:\_TEST_PRINT");

            float dtSoftwarePrintTime = 0;
            float dtInitServerRepotTime = 0;
            float dtAddParamsTime = 0;
            float dtGeneratePagesTime = 0;
            float dtInitSoftwarePrintTime = 0;
            float dtOtherInitOperations = 0;

            float dtGenerateOtherTime = 0;
            float dtGeneratePrintingTime = 0;

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

            GlobalObjects.AddLogMessage("№" + threadInfo + ": [ReportInit]:" + dtReportInitTime + 
                "; [ReportConstruct]:" + rfPrint.dtConstructTime +
                //"; [InitServerRepot]:" + tInitServerRepotTime +

                "; [ReportGenerate]:" + rfPrint.dtGenerateTime +
                "; [InitSoftwarePrint]:" + dtInitSoftwarePrintTime +
                "; [AddParams]:" + dtAddParamsTime +
                "; [OtherInitOperations]:" + dtOtherInitOperations + 
                "; [SoftwarePrint]:" + dtSoftwarePrintTime +
                "; [GenerateOther]:" + dtGenerateOtherTime +
                "; [GeneratePages]:" + dtGeneratePagesTime +
                "; [GeneratePrinting]:" + dtGeneratePrintingTime + 
                ": [ДЛИТЕЛЬНОСТЬ]:" + GlobalObjects.DateDiff(dtThreadStart).ToString());
            RepApp.Reports.Clear();
            RepApp.Dispose();

            // + "; [СТАРТ]:" + String.Format("{0:HH:mm:ss.ffff}", threadStart) + "; [ФИНИШ]:" + String.Format("{0:HH:mm:ss.ffff}", DateTime.Now));
            //Thread.Sleep(0);
            //Thread.CurrentThread.Abort();
            */
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ComClsReporting objRep = new ComClsReporting();
            //objRep.SetToDebugMode();
            //objRep.SetToServerMode();
            
            objRep.SetLocation("SSQL10", "GHOST_TEST", true, "sa", "sa", "WH3");

            //string[] parameters2 = new string[] { "ACTSHIPMENTKEY", "K2200004025", "Stor", "RAB", "CarNumber", "", "DateAndTimeSettingForLoading", "02.06.2016 8:07:56" };
            object[] parameters2 = new object[] { "ACTSHIPMENTKEY", "K2200004025", "Stor", "RAB", "CarNumber", "", "DateAndTimeSettingForLoading", DateTime.Parse("02.06.2016 8:07:56") };
            object objParameters2 = (object)parameters2;

            string[] docsPack3 = new string[] { "K2200004061" };
            object objDocsPack3 = (object)docsPack3;
            objRep.AddReport("ActShipingV8", "WH3", ref objParameters2, ref objDocsPack3);
            objRep.ShowDialog();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            ComClsReporting objRep = new ComClsReporting();
            //objRep.SetToDebugMode();
            //objRep.SetToServerMode();
            objRep.SetLocation("SSQL10", "GHOST_TEST", true, "sa", "sa", "WH3");
            string[] parameters = new string[] { "UserName", "bulyginr" };
            object objParameters = (object)parameters;
            string[] docs3 = new string[] { "INV00000003" + "/3" };
            string[] docs19 = new string[] { "INV00000003" + "/19" };
            object objDocs3 = (object)docs3;
            object objDocs19 = (object)docs19;
            for (int i = 0; i < 1; i++)
            {
                docs3 = new string[] { "INV00000003_TEST_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "/3" };
                objDocs3 = (object)docs3;
                docs19 = new string[] { "INV00000003_TEST_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "/19" };
                objDocs19 = (object)docs19;
                objRep.AddReport("ActShipingV8", "WH2", ref objParameters, ref objDocs3);
                //objRep.AddReport("Inv19", "WH3", ref objParameters, ref objDocs19);
            }
            objRep.ShowReportInBuilder();
        }

        private void StartRFPrint_Click(object sender, EventArgs e)
        {
            GlobalObjects.AddLogMessage("Старт теста");
            timer1.Enabled = true;
            timer1_Tick(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
                return;
            timer1.Enabled = false;
            GlobalObjects.AddLogMessage("Финиш теста");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Constants.AssemblyDirectory + @"\RFPrint_LOG.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(String.Format("{0:HH:mm:ss.ffff}", DateTime.Now) + " Секундомер");
                }
            }
            catch (Exception e1)
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(String.IsNullOrEmpty("").ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string portName;
            if (AddPrinter(txtPrinterName.Text, txtPrinterDriver.Text, txtPrinterPort.Text, false, out portName))
                System.Windows.Forms.MessageBox.Show("Printer '" + txtPrinterName.Text + "' with driver '" + txtPrinterDriver.Text + "' install on port '" + portName + "'");
        }

        public static bool AddPrinter(string printerName, string printerDriver, string ipAddress, bool sharedPrinter, out string portName)
        {
            bool result = false;
            portName = "";
            try
            {


                //---------------------------------------------------
                //----- Printer ------------------------------------
                // Init Win32_Printer class 
                ManagementClass printerClass = new ManagementClass(new ManagementPath("Win32_Printer"), null);

                //---------------------------------------------------
                //----- Проверка существующих имен принтеров в системе. Если уже есть, не создаем принтер, просто выходим
                System.Management.ManagementObjectCollection managementPrinters;
                managementPrinters = printerClass.GetInstances();
                List<string> existPrinterNames = new List<string>();
                foreach (ManagementObject item in managementPrinters)
                {
                    existPrinterNames.Add(item["Name"].ToString());
                }
                if (existPrinterNames.Count(x => x == printerName) > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Printer '" + printerName + "' already exist in system");
                    return result;
                }

                //---------------------------------------------------
                //----- Port ------------------------------------


                ManagementClass portClass = new ManagementClass(new ManagementPath("Win32_TCPIPPrinterPort"), null);

                //---------------------------------------------------
                //----- Проверка существующих названий портов на указанном IP. Если уже есть, добавляем к имени порта номер: IP_172.16.250.52_i
                System.Management.ManagementObjectCollection managementPorts;
                managementPorts = portClass.GetInstances();
                List<string> existPortNames = new List<string>();
                string tmpPortName = "";
                foreach (ManagementObject item in managementPorts)
                {
                    if (item["HostAddress"].ToString() == ipAddress)
                        existPortNames.Add(item["Name"].ToString());

                }
                for (int i = 0; i <= 1000; i++)
                {
                    tmpPortName = "IP_" + ipAddress + (i == 0 ? "" : "_" + i.ToString());
                    if (existPortNames.Count(x => x == tmpPortName) == 0)
                        break;
                }
                if (existPortNames.Count(x => x == tmpPortName) > 0)
                {
                    System.Windows.Forms.MessageBox.Show("All available port names in range [IP_172.16.250.52_{1-1000}] are busy");
                    return result;
                }
                portName = tmpPortName;
                //----- Конец проверки существующих названий портов на указанном IP


                //-------создаем порт:
                ManagementObject portObject = portClass.CreateInstance();
                portObject["HostAddress"] = ipAddress;

                portObject["Name"] = portName;//"IP_" + ipAddress;

                portObject["PortNumber"] = 9100;

                portObject["Protocol"] = 1;

                portObject["SNMPEnabled"] = false;

                portObject.Put(new PutOptions() { Type = PutType.UpdateOrCreate });



                //-------создаем принтер:

                // Create new Win32_Printer object 
                ManagementObject printerObject = printerClass.CreateInstance();

                printerObject["Name"] = printerName;

                printerObject["PortName"] = portName;// "IP_" + ipAddress;

                // Set driver and device names 
                printerObject["DriverName"] = printerDriver;

                printerObject["DeviceID"] = printerName;

                printerObject["Network"] = true;

                // Set sharing 
                if (sharedPrinter)
                {
                    printerObject["Shared"] = sharedPrinter;
                    printerObject["ShareName"] = printerName;
                }

                //printerObject["comment"] = "Eugene Kirian";
                // specify put options: update or create 
                PutOptions options = new PutOptions();
                options.Type = PutType.UpdateOrCreate;

                // Put a newly created object to WMI objects set 
                printerObject.Put(options);

                result = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("WMI exception: {0}", ex.Message);
                //Console.WriteLine("WMI exception: {0}", ex.Message);
                //throw new Exception(string.Format("WMI exception: {0}", ex.Message));
            }
            return result;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ComClsReporting objRep = new ComClsReporting();
            objRep.SetLocation("SSQL10-tst2", "GHOST_TEST", false, "sa", "sa", "WH1");
            object[] parameters = new object[] { "DateFrom", "13/03/2016", "DateTo", null };
            object objParameters = (object)parameters;
            string[] docs3 = new string[] { "" };
            object objDocs3 = (object)docs3;
            for (int i = 0; i < 1; i++)
            {
                objRep.AddReport("SelectOrders", "WH1", ref objParameters, ref objDocs3, true, "bulygin.r");
            }
            string[] status = new string[] { "95"}; object objStatus = (object)status;
            objRep.AddMultiParameter("STATUS", ref objStatus);
            string[] storerkey = new string[] { "CART" }; object objStorerkey = (object)storerkey;
            objRep.AddMultiParameter("STORERKEY", ref objStorerkey);
            string[] orders = new string[] { "0001298872", "0001306245" }; object objOrders = (object)orders;
            objRep.AddMultiParameter("ORDERKEY", ref objOrders);

            objRep.ShowReportInBuilder(changeDropDownHeight:true, tryReloadMultiParams:true);
            //false, "OK1",false,"",0,true,400,700
            //False, "OK", False, "", 0, True, 200, 250
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ComClsReporting objRep = new ComClsReporting();

            objRep.SetLocation("SSQL10-tst2", "GHOST_TEST", true, "sa", "sa", "WH2");

            string[] parameters1 = new string[] {  };
            object objParameters1 = (object)parameters1;
            string[] docsPack = new string[] { "1111111111111111111" };

            object objDocsPack = (object)docsPack;
            objRep.AddReport("SrvPr_PrintLog", "WH2", ref objParameters1, ref objDocsPack,false,"",true);
            objRep.ShowReportsInPreview();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            ComClsReporting objRep = new ComClsReporting();

            objRep.SetLocation("SSQL10-tst2", "GHOST_TEST", true, "sa", "sa", "WH2");
            objRep.ShowSettings();
        }
    }
}
