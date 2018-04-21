using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Win32;

namespace Reporting
{
    public static class Constants
    {
        private static string CurrencyDecimalSeparator = Convert.ToString(0.5).Substring(1, 1);
        private static INI ini;
        //public const string SERVER_URL = "http://ssrs01/reportserver";
        //public const string SERVER_NAME = "ssrs01";
        //public const string DB_LOG = "GHOST";
        //public const string DOMAIN_PREFIX = "UNITED-EUROPE\\";
        //public const string DOMAIN = "UNITED-EUROPE.RU";
        //public const double PRINTSCALE = 0.98;
        private static string mTITLE="";
        public static string TITLE
        {
            get
            {
                if (mTITLE == "")
                {

                    System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
                    if (asm != null)
                    {
                        System.Reflection.AssemblyName asmName = asm.GetName();
                        mTITLE = asmName.Name;
                    }
                }
                return mTITLE;
            }
            private set { }
        }

        public static bool mIsSettingsReaded = false;
        private static string mSERVER_URL;
        public static string SERVER_URL
        {
            get { return mSERVER_URL; }
        }
        private static string mSERVER_NAME;
        public static string SERVER_NAME
        {
            get { return mSERVER_NAME; }
        }
        private static string mDB_LOG;
        public static string DB_LOG
        {
            get { return mDB_LOG; }
        }
        private static string mDOMAIN_PREFIX;
        public static string DOMAIN_PREFIX
        {
            get { return mDOMAIN_PREFIX; }
        }
        private static string mDOMAIN;
        public static string DOMAIN
        {
            get { return mDOMAIN; }
        }
        private static string mREPORT_HISTRORY;
        public static string REPORT_HISTRORY
        {
            get { return mREPORT_HISTRORY; }
        }
        private static float mPRINTSCALE;
        public static float PRINTSCALE
        {
            get { return mPRINTSCALE; }
        }
        private static string mREPORTINGDLL_PATH;
        public static string REPORTINGDLL_PATH
        {
            get { return mREPORTINGDLL_PATH; }
        }
        public static string AssemblyDirectory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }
        public static void ReadConstIni()
        {
            if (mIsSettingsReaded)
                return;
            try 
            {
                //инициализация
                mREPORTINGDLL_PATH = AssemblyDirectory;
                if (!File.Exists(mREPORTINGDLL_PATH + @"\GhostReporting.ini"))
                {
                    MessageBox.Show("В директории ''" + mREPORTINGDLL_PATH + "'' не найден файл ''GhostReporting.ini''");
                    Environment.Exit(1);
                }
                ini = new INI(mREPORTINGDLL_PATH + @"\GhostReporting.ini");
                mDB_LOG = ini.IniReadValue("Parameters", "DB_LOG").Trim();// ReedIni("Parameters", "DB_LOG");
                mDOMAIN_PREFIX = ini.IniReadValue("Parameters", "DOMAIN").Trim() + @"\";//ReedIni("Parameters", "DOMAIN") + "\\";
                mDOMAIN = ini.IniReadValue("Parameters", "DOMAIN").Trim() + ".RU";//ReedIni("Parameters", "DOMAIN") + ".RU";
                mREPORT_HISTRORY = ini.IniReadValue("Parameters", "REPORT_HISTRORY").Trim();

                bool Disable_proxy = true;
                Disable_proxy = Convert.ToBoolean(ini.IniReadValue("Parameters", "DISABLE_PROXY").Trim());
                //очень важно! Если не поставить этот параметр то первый запуск отчета будет очень медленный, т.к. Reporting будет пытаться получить WebResponse через прокси
                if (Disable_proxy)
                    System.Net.HttpWebRequest.DefaultWebProxy = null;
                
                bool Disable_CheckCertificateRevocation = true;
                Disable_CheckCertificateRevocation = Convert.ToBoolean(ini.IniReadValue("Parameters", "DISABLE_CHECKCERTIFICATEREVOCATION").Trim());
                if (Disable_CheckCertificateRevocation)
                {
                    string keyName = @"Software\Microsoft\Windows\CurrentVersion\WinTrust\Trust Providers\Software Publishing";
                    RegistryKey rk = Registry.CurrentUser.OpenSubKey(keyName, true);
                    rk.SetValue("State", 146944);
                    rk.Close();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public static void ReadConst(ref Location location)
        {
            if (mIsSettingsReaded)
                return;
            mSERVER_NAME = ReadParamFromBD("SERVER_NAME", ref location);
            //mSERVER_NAME = "172.16.218.7";//временно!!!!
            mSERVER_URL = (!string.IsNullOrEmpty(SERVER_NAME) ? "http://" + SERVER_NAME + "/reportserver" : "");
            string tmpPRINTSCALE = null;
            tmpPRINTSCALE = ReadParamFromBD("PRINTSCALE", ref location).Replace(",", CurrencyDecimalSeparator).Replace(".", CurrencyDecimalSeparator);
            tmpPRINTSCALE = (string.IsNullOrEmpty(tmpPRINTSCALE) ? "0" : tmpPRINTSCALE);
            mPRINTSCALE = Convert.ToSingle(tmpPRINTSCALE);
            if (string.IsNullOrEmpty(SERVER_URL))
                mSERVER_URL = ini.IniReadValue("Parameters", "SERVER_URL").Trim();//ReedIni("Parameters", "SERVER_URL");
            if (PRINTSCALE == 0)
                mPRINTSCALE = Convert.ToSingle(ini.IniReadValue("DB_LOG", "PRINTSCALE").Trim().Replace(",", CurrencyDecimalSeparator).Replace(".", CurrencyDecimalSeparator));
            mIsSettingsReaded = true;
        }
        private static string ReadParamFromBD(string Param, ref Location location)
        {
            string sSQL = null;
            string ParamVaue = "";
            sSQL = "SELECT [Value] FROM [%1].[dbo].[SrvPr_PARAMETERS] where Name='%2'";
            sSQL = GlobalObjects.strfmt(sSQL, DB_LOG, Param);
            OleDbCommand command = new OleDbCommand(sSQL, location.Connection);
            OleDbDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ParamVaue = reader.GetString(reader.GetOrdinal("Value"));
                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Параметр ''" + Param + "'' не найден в таблице конфигурации");
                return "";
            }
            return ParamVaue;

        }



    }
}
