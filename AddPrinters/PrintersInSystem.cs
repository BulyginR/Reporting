using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace InstallPrinters
{
    public partial class PrintersInSystem : Form
    {
        public PrintersInSystem()
        {
            InitializeComponent();
        }

        private void PrintersInSystem_Load(object sender, EventArgs e)
        {
            LoadPrinters();
        }
        private void LoadPrinters()
        {
            ManagementClass printerClass = new ManagementClass(new ManagementPath("Win32_Printer"), null);
            ManagementObjectCollection managementPrinters;
            managementPrinters = printerClass.GetInstances();


            ManagementClass portClass = new ManagementClass(new ManagementPath("Win32_TCPIPPrinterPort"), null);
            ManagementObjectCollection managementPorts;
            managementPorts = portClass.GetInstances();
            List<SystemPort> systemPorts = new List<SystemPort>();
            List<PrinterInfo> systemPrinters = new List<PrinterInfo>();

            foreach (ManagementObject item in managementPorts)
            {
                SystemPort systemPort = new SystemPort(item["Name"].ToString(), item["HostAddress"].ToString());
                systemPorts.Add(systemPort);
            }

            foreach (ManagementObject item in managementPrinters)
            {
                PrinterInfo printerInfo = new PrinterInfo(item["Name"].ToString(), item["DriverName"].ToString(), "", item["PortName"].ToString());
                systemPrinters.Add(printerInfo);
            }

            foreach (PrinterInfo item in systemPrinters)
            {
                item.IpAdress = (systemPorts.Count(x => x.Name == item.PortName) > 0 ? systemPorts.Where(x => x.Name == item.PortName).First().IpAdress : "");
            }


            lvPrinters.Items.Clear();
            foreach (PrinterInfo item in systemPrinters.OrderBy(x => x.PrinterName).ToList<PrinterInfo>())
            {
                ListViewItem lvItem = new ListViewItem(new string[] { item.PrinterName, item.DriverName, item.IpAdress });
                lvPrinters.Items.Add(lvItem);
            }
        }
        private void contextMenuForCopy_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "toolStripMenuSelectAll")
            {
                foreach (ListViewItem item in lvPrinters.Items)
                {
                    item.Selected = true;
                }
            }
            else if (e.ClickedItem.Name == "toolStripMenuCopy")
            {
                CopySelectedValuesToClipboard();
            }
            else if (e.ClickedItem.Name == "toolStripMenuDelete")
            {
                int deletedPrinters = 0;
                int notDeletedPrinters = 0;
                foreach (ListViewItem item in lvPrinters.SelectedItems)
                {
                    if (AddPrinters.DeletePrinter(item.Text))
                    {
                        deletedPrinters += 1;
                    }
                    else
                    {
                        notDeletedPrinters+=1;
                    }
                    
                    //item.Selected = true;
                    //LOAD + MESSAGEBOX
                }
                MessageBox.Show("Было успешно удалено " + deletedPrinters + " принтеров. С ошибками: " + notDeletedPrinters + ". Подробности см. в файле '" + AddPrinters.AssemblyDirectory + @"\" + String.Format("{0:yyyy.MM}_", DateTime.Now) + "InstallPrinters_LOG.txt'", "Удаление принтеров", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPrinters();
            }


        }


        private void CopySelectedValuesToClipboard()
        {
            var builder = new StringBuilder();
            foreach (ListViewItem item in lvPrinters.SelectedItems)
            {
                builder.AppendLine(item.SubItems[0].Text + Convert.ToChar(9) + item.SubItems[1].Text + Convert.ToChar(9) + item.SubItems[2].Text);
            }

            Clipboard.SetText(builder.ToString());
        }

        private void lvPrinters_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender != lvPrinters) return;

            if (e.Control && e.KeyCode == Keys.C)
                CopySelectedValuesToClipboard();
        }

    }
    /*
    public class SystemPrinter
    {
        public string Name;
        public string Driver;
        public string Port;
        public string IpAdress;
        public SystemPrinter(string name, string driver, string port)
        {
            Name = name;
            Driver = driver;
            Port = port;
        }

    }
     */
    public class SystemPort
    {
        public string Name;
        public string IpAdress;
        public SystemPort(string name, string ipAdress)
        {
            Name = name;
            IpAdress = ipAdress;
        }

    }
}
