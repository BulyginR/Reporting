
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.IO;

namespace Reporting
{

    public class ReportApplication : IDisposable
    {
        private DirectoryInfo mExportDir;

        private Location mAppLocation;
        private List<ReportItem> mReports = new List<ReportItem>();//был ArrayList();

        private PrinterSettings mPrinterSettings = new PrinterSettings();
        private string mOldChangeCurPassword = "";
        private string mOldChangeCurUserName = "";
        private string mOldChangeCurUserFullName = "";

        private bool mIsOldChange = false;
        public List<ReportItem> Reports
        {
            get { return mReports; }
            set { mReports = value; }
        }
        private string mTitle="";
        public string Title
        {
            get 
            {
                if (mTitle == "")
                {
                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetAssembly(this.GetType());
                    if (asm != null)
                    {
                        System.Reflection.AssemblyName asmName = asm.GetName();
                        mTitle = asmName.Name;
                    }
                }
                return mTitle; 
            }
            private set { } 
        }
        public PrinterSettings PrinterSettings
        {
            get { return mPrinterSettings; }
            set { mPrinterSettings = value; }
        }

        public string OldChangeCurPassword
        {
            get { return mOldChangeCurPassword; }
            set { mOldChangeCurPassword = value; }
        }

        public string OldChangeCurUserName
        {
            get { return mOldChangeCurUserName; }
            set { mOldChangeCurUserName = value; }
        }
        public string OldChangeCurUserFullName
        {
            get { return mOldChangeCurUserFullName; }
            set { mOldChangeCurUserFullName = value; }
        }
        public ComClsReporting ComCls=null;

        public bool IsOldChange
        {
            get { return mIsOldChange; }
            set { mIsOldChange = value; }
        }

        public DirectoryInfo ExportDir
        {
            get { return mExportDir; }
            set { mExportDir = value; }
        }

        public Location AppLocation
        {
            get { return mAppLocation; }
            set { mAppLocation = value; }
        }


        public ReportApplication()
        {
            if ((GlobalObjects.ExportDir != null))
            {
                this.ExportDir = GlobalObjects.ExportDir;
            }

        }
        public ReportApplication(ComClsReporting comCls)
        {
            ComCls = comCls;
            if ((GlobalObjects.ExportDir != null))
            {
                this.ExportDir = GlobalObjects.ExportDir;
            }

        }
        public ReportApplication(string _ServerName, string _AppName, bool _SSPI, string _UserName, string _Password, string _Schema)
        {
            Constants.ReadConstIni();
            Location Location = new Location(_ServerName, _AppName, _SSPI, _UserName, _Password, _Schema);

            if (!Location.OpenConnection(true))
                Environment.Exit(1);//тестируем соединение
            AppLocation = Location;
            Constants.ReadConst(ref Location);

            if ((GlobalObjects.ExportDir != null))
            {
                this.ExportDir = GlobalObjects.ExportDir;
            }

        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }

}
