using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Data.OleDb;
using OfficeOpenXml;

namespace InstallPrinters
{
    public partial class AddPrinters : Form
    {
        private static string mCaption = "Пакетная установка принтеров";
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
        public AddPrinters()
        {
            InitializeComponent();
        }

        private void InstallPrinters_Load(object sender, EventArgs e)
        {
            this.Text = mCaption;
        }

        //Deletes the printer
        public static bool DeletePrinter(string sPrinterName)
        {
            try
            {
                ManagementScope oManagementScope = new ManagementScope(ManagementPath.DefaultPath);
                oManagementScope.Connect();

                SelectQuery oSelectQuery = new SelectQuery();
                oSelectQuery.QueryString = @"SELECT * FROM Win32_Printer WHERE Name = '" +
                   sPrinterName.Replace("\\", "\\\\") + "'";

                ManagementObjectSearcher oObjectSearcher = new ManagementObjectSearcher(oManagementScope, oSelectQuery);
                ManagementObjectCollection oObjectCollection = oObjectSearcher.Get();

                if (oObjectCollection.Count != 0)
                {
                    foreach (ManagementObject oItem in oObjectCollection)
                    {
                        oItem.Delete();
                        WriteLog("printer '" + sPrinterName + "' deleted");
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog("error when delete printer: " + ex.Message + "(Printer name:'" + sPrinterName + "'");
            }
            return false;

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
                    WriteLog("Printer '" + printerName + "' already exist in system");
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
                    WriteLog("All available port names in range [IP_172.16.250.52_{1-1000}] are busy");
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

                printerObject["Default"] = false;

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
                WriteLog("error: " + ex.Message + "(Printer:'" + printerName + "';driver:'" + printerDriver + "';ip:'" + ipAddress + "'");
            }
            return result;
        }
        public static void WriteLog(string text)
        {
            using (FileStream fs = new FileStream(AssemblyDirectory + @"\" + String.Format("{0:yyyy.MM}_", DateTime.Now) + "InstallPrinters_LOG.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
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

        private void btnLoadPrinters_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AssemblyDirectory;
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            openFileDialog.Filter = "excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            ReadExcel(openFileDialog.FileName);
        }
        private void ReadExcel(string fileName)
        {
            /*
            //string fileName = @"C:\ExcelFile.xlsx";
            //string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\"";
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";User ID=Admin";
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                var sheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                using (var cmd = conn.CreateCommand())
                {
                    string SpreadSheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                    cmd.CommandText = "select * from [" + SpreadSheetName + "] ";
                    //cmd.CommandText = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "] ";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    for (int i = 1; i < ds.Tables[0].Rows.Count;i++ )
                    {
                        DataRow row = ds.Tables[0].Rows[i];
                        ListViewItem lvItem = new ListViewItem(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                        lvPrinters.Items.Add(lvItem);
                    }

                    //foreach (DataRow row in ds.Tables[0].Rows)
                    //{

                    //    ListViewItem lvItem = new ListViewItem(new string[] { row[0].ToString(), row[1].ToString(), row[2].ToString() });
                    //    lvPrinters.Items.Add(lvItem);
                    //}
                }
            }

            */
            try
            {
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileName)))
                {

                    // get the first worksheet in the workbook
                    ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];
                    int i = 1; int totalRows;
                    while (worksheet.Cell(i, 1).Value != @"")
                    {
                        i++;
                    }
                    totalRows = i-1;
                    // output the data in column 2
                    if (totalRows < 2)
                    {
                        MessageBox.Show("В Excel нет ни одного принтера", "Загрузка принтеров", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    lvPrinters.Items.Clear();
                    for (int iRow = 2; iRow <= totalRows; iRow++)
                    {
                        ListViewItem lvItem = new ListViewItem(new string[] { worksheet.Cell(iRow, 1).Value.ToString(), worksheet.Cell(iRow, 2).Value.ToString(), worksheet.Cell(iRow, 3).Value.ToString() });
                        lvPrinters.Items.Add(lvItem);
                    }

                } // the using statement calls Dispose() which closes the package.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Загрузка принтеров", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lvPrinters.Items.Count > 0)
            {
                List<PrinterInfo> printers = new List<PrinterInfo>();
                foreach (ListViewItem lvItem in lvPrinters.Items)
                {
                    
                    PrinterInfo printerInfo = new PrinterInfo(lvItem.Text, lvItem.SubItems[1].Text, lvItem.SubItems[2].Text);
                    printers.Add(printerInfo);
                }

                this.backgroundWorker1.RunWorkerAsync(new object[] {printers});
                this.btnOk.Enabled = false;
                this.Text = mCaption + " [идет процесс установки...не закрывайте приложение до завершения]";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            List<PrinterInfo> printers = (List<PrinterInfo>)args[0];
            int i = 1;
            int installPrinters = 0;
            int notInstallPrinters = 0;
            foreach (PrinterInfo item in printers)
            {
                RefreshInfo(this, "Установка принтера '" + item.PrinterName + "', IP:" + item.IpAdress + " [" + i + "/" + printers.Count + "]...");
                string portName = "";
                if (AddPrinter(item.PrinterName, item.DriverName, item.IpAdress, false, out portName))
                {
                    installPrinters += 1;
                    WriteLog("Printer '" + item.PrinterName + "' with driver '" + item.DriverName + "', ip = '" + item.IpAdress + "' installed on port '" + portName + "'");
                }
                else
                    notInstallPrinters += 1;
                i++;
            }

            MessageBox.Show("Было успешно установлено " + installPrinters.ToString() + " принтеров. С ошибками: " + notInstallPrinters.ToString() + ". Подробности см. в файле '" + AssemblyDirectory + @"\" + String.Format("{0:yyyy.MM}_", DateTime.Now) + "InstallPrinters_LOG.txt'","Установка принтеров",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnOk.Enabled = true;
            this.lblInfo.Visible = false;
            this.Text = mCaption + " [установка завершена]";
        }
        public void RefreshInfo(string strInfo)
        {
            lblInfo.Visible = true;
            lblInfo.Text = strInfo;
        }
        public delegate void RefreshFormInfo(AddPrinters frm, string strInfo);
        public void RefreshInfo(AddPrinters frm, string strInfo)
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            PrintersInSystem frm = new PrintersInSystem();
            frm.ShowDialog();
        }
    }
}
