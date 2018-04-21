using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.IO;
using System.Management;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using ExtensionMethods;

namespace Reporting
{
    public enum ExportFormat
    {
        Pdf = 1,
        Xls = 2
    }
    public class Printer
    {
        public string Name = "";
        public bool Default = false;
        public bool WorkOffline = false;
        public string DriverName = "";

        public Printer(string _Name, bool _Default,bool _WorkOffline, string _DriverName)
        {
            Name = _Name;
            Default = _Default;
            DriverName = _DriverName;
        }
    }
    public static class GlobalObjects
    {
        //для записи в форму лога из других потоков
        private delegate void AddFormLogMessage(string strInfo);
        public static void AddLogMessage(string strInfo)
        {
            if (GlobalObjects.IsServerMode)
                return;
            if (DebugLoger.InvokeRequired)
            {
                AddFormLogMessage tsDelegate = new AddFormLogMessage(AddLogMessage);
                DebugLoger.Invoke(tsDelegate, new object[]{
				strInfo
			});
            }
            else
            {
                DebugLoger.AddMessage(strInfo);
            }
        }

        public static DirectoryInfo ExportDir;
        public static bool IsDebugMode = false;
        public static bool IsServerMode = false;
        private static frmDebugLoger mDebugLoger;
        public static frmDebugLoger DebugLoger 
        { 
            get
            {
                if (mDebugLoger == null )
                {
                    mDebugLoger = new frmDebugLoger();
                }
                if ( mDebugLoger.IsDisposed)
                {
                    mDebugLoger = new frmDebugLoger();
                }
                Form fc = Application.OpenForms["frmDebugLoger"];
                if (fc == null)
                {
                    mDebugLoger.Show();
                }
                else 
                {
                    fc=null;
                }

                return mDebugLoger;
            }
            set
            {
                mDebugLoger = value;
            }
        }


        public static float DateDiff(DateTime startDate)
        {
            return (float)Math.Round((double)(DateTime.Now - startDate).TotalMilliseconds / (double)1000, 3);
        }
        public static string AllowForPrintExport(int MaxCopies, int FromLogCopies)
        {
            if (MaxCopies == -1)
            {
                return "не ограничено";
            }
            else
            {
                int iTmp = 0;
                iTmp = MaxCopies - FromLogCopies;
                if (iTmp <= 0)
                {
                    return "0";
                }
                else
                {
                    return iTmp.ToString();
                }
            }
        }

        public static string strfmt(string str, params object[] s)
        {
            int i = 0;

            for (i = 0; i < s.Length; i++)
            {
                str = Regex.Replace(str,"%" + (i + 1).ToString(), (s[i]==null?"": s[i].ToString()) /*s.GetValue(i).ToString()*/, RegexOptions.IgnoreCase);
            }

            return str;
        }
        

        public static bool IsZebraPrinter(string PrinterName)
        {
            try
            {
                string ComputerName = "localhost";
                ManagementScope Scope;                
                Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);
                Scope.Connect();
                ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_Printer");
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    if (WmiObject["Name"].ToString().ToUpper() == PrinterName.ToUpper())
                    {
                        if (WmiObject["DriverName"].ToString().Left(9).ToLower() == "zdesigner")
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return false;
        }


        public static List<Printer> GetPrinters()
        {
            List<Printer> printers = new List<Printer>();
            try
            {
                string ComputerName = "localhost";
                ManagementScope Scope;
                Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);
                Scope.Connect();
                ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_Printer");
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    printers.Add(new Printer(WmiObject["Name"].ToString(), (bool)WmiObject["Default"], (bool)WmiObject["WorkOffline"], WmiObject["DriverName"].ToString()));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return printers;
            }
            return printers;
        }

    }
}
