using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;

namespace Reporting
{

    public class Location
    {


        //public ReportApplication p_app;
        public string ServerName;
        public string AppName;
        public bool SSPI;
        public string UserName;
        public string Password;
        public bool ConnectionSucceed = false;
        public string Schema;

        public string DataBase = Constants.DB_LOG;

        public OleDbConnection Connection;

        internal const int LOGON32_LOGON_INTERACTIVE = unchecked((int)2);
        internal const int LOGON32_PROVIDER_DEFAULT = unchecked((int)0);

        [DllImport("advapi32.dll")]//, CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true
        private static extern bool LogonUser(string un, string domain, string pw, int LogonType, int LogonProvider, ref IntPtr Token);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)] //, ExactSpelling = true
        public static extern bool CloseHandle(IntPtr handle);

        private string mUserFullName = "";
        public string UserFullName
        {
            get
            {
                if (mUserFullName == "")
                {
                    string sSQL = null;
                    sSQL = " SELECT isnull(%1.%2.UDF_SCRT_USER_FULLNAME('%3'),' ') as FULLNAME ";
                    sSQL = GlobalObjects.strfmt(sSQL, "PRD1", Schema, UserName);

                    OleDbCommand command = new OleDbCommand(sSQL, this.Connection);
                    OleDbDataReader reader = command.ExecuteReader();
                    string tmp = " ";
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tmp = reader["FULLNAME"].ToString();
                        }
                    }
                    reader.Close();
                    mUserFullName = tmp;
                }

                return mUserFullName;

            }

            private set { }
        }

        public string UserFullNameReport(string ReportUserName)
        //если имя юзера отчета не совпадает с именем юзера приложения
        {

            string sSQL = null;
            sSQL = " SELECT isnull(%1.%2.UDF_SCRT_USER_FULLNAME('%3'),'') as FULLNAME ";
            sSQL = GlobalObjects.strfmt(sSQL, "PRD1", Schema, ReportUserName);

            OleDbCommand command = new OleDbCommand(sSQL, this.Connection);
            OleDbDataReader reader = command.ExecuteReader();
            string tmp = "";
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tmp = reader["FULLNAME"].ToString();
                }
            }
            reader.Close();
            return tmp;
        }

        public Location(Location objLocation)
        {
            //p_app = _app;

            ServerName = objLocation.ServerName;
            AppName = objLocation.AppName;
            SSPI = objLocation.SSPI;
            UserName = objLocation.UserName;
            Password = objLocation.Password;
            ConnectionSucceed = objLocation.ConnectionSucceed;
            Schema = objLocation.Schema;
            Connection = new OleDbConnection(objLocation.Connection.ConnectionString);

        }
        public Location(string _ServerName, string _AppName, bool _SSPI, string _UserName, string _Password, string _Schema, bool CreateConnection = true)
        {
            SSPI = _SSPI;
            UserName = _UserName.Replace(Constants.DOMAIN_PREFIX, "");
            Password = _Password;

            ServerName = _ServerName;
            AppName = _AppName;
            Schema = _Schema;
            if (CreateConnection)
                CreateConnectionString();
            return;

        }

        public Location(ref OleDbConnection _Connection, string _ServerName, string _AppName, bool _SSPI, string _UserName, string _Password, string _Schema)
        {

            SSPI = _SSPI;
            UserName = _UserName.Replace(Constants.DOMAIN_PREFIX, "");
            Password = _Password;

            ServerName = _ServerName;
            AppName = _AppName;
            Schema = _Schema;

            Connection = _Connection;
            return;

        }


        public void CreateConnectionString()
        {
            string sConnStr = null;
            sConnStr = GlobalObjects.strfmt("Provider=%1;%2;" + "User ID=%3;Password=%4;Initial Catalog=%5;" + "Data Source=%6;Application Name=%7;Workstation ID=%8", "SQLOLEDB.1", (SSPI ? "Integrated Security=SSPI" : "Persist Security Info=False"), UserName, Password, DataBase, ServerName, AppName, Environment.MachineName);
            Connection = new OleDbConnection(sConnStr);
        }

        public bool OpenConnection(bool IsStartFromOtherApp, bool PrintMessage = true)
        {
            if (SSPI)
            {
                if (IsStartFromOtherApp)
                {
                    //старт из-под другого приложения. При SSPI нельзя узнать пользователя/пароль. Имперсонация невозможна.
                    try
                    {
                        if (GlobalObjects.IsDebugMode)
                            GlobalObjects.AddLogMessage("Открытие соединения... ");
                        Connection.Open();
                        if (GlobalObjects.IsDebugMode)
                            GlobalObjects.AddLogMessage("Соединение открыто");
                    }
                    catch (Exception ex)
                    {
                        if (PrintMessage)
                            MessageBox.Show(ex.Message);
                        return false;
                    }

                }
                else
                {
                    //пользователь и пароль указываются прямо в отчетности. имперсонация
                    IntPtr tokenHandle = IntPtr.Zero;
                    try
                    {
                        LogonUser(UserName, Constants.DOMAIN, Password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);
                        if (tokenHandle.ToInt32() == 0)
                        {
                            if (PrintMessage)
                                MessageBox.Show("tokenHandle = 0", Constants.TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        //If LogonUser(UserName, "DOMAIN", Password, 2, 0, tokenHandle) Then
                        //Else 'logon failed
                        //    If PrintMessage Then MsgBox("Пользователь " & UserName & " не прошел авторизацию", vbExclamation, AppName)
                        //    Return False
                        //End If
                        WindowsIdentity newId = new WindowsIdentity(tokenHandle);
                        using (WindowsImpersonationContext impersonatedUser = newId.Impersonate())
                        {
                            //perform impersonated commands
                            try
                            {
                                Connection.Open();
                            }
                            catch (Exception ex)
                            {
                                if (PrintMessage)
                                    MessageBox.Show(ex.Message);
                                return false;
                            }
                        }
                        CloseHandle(tokenHandle);

                    }
                    catch (Exception ex)
                    {
                        if (PrintMessage)
                            MessageBox.Show(ex.Message);
                        return false;
                    }
                }

            }
            else
            {
                try
                {
                    if (GlobalObjects.IsDebugMode)
                        GlobalObjects.AddLogMessage("Открытие соединения... ");
                    Connection.Open();
                    if (GlobalObjects.IsDebugMode)
                        GlobalObjects.AddLogMessage("Соединение открыто");
                }
                catch (Exception ex)
                {
                    ConnectionSucceed = false;
                    if (PrintMessage)
                        MessageBox.Show(ex.Message);
                    return false;

                }

            }
            //тест пользователя прошел
            ConnectionSucceed = true;
            return true;

        }

        public bool CheckGroupOldChange()
        {

            string sSQL = null;
            sSQL = " SELECT DISTINCT 1 AS UserInGroup " + " FROM %1.%3.NSQLCONFIG a, %2.dbo.T_GH_SCRT_USER_GROUP b " + "  WHERE     CONFIGKEY = 'Ghost.RoleMasterSmena' " + "      AND upper (b.GROUPID) = upper (a.NSQLVALUE) " + "      AND b.DBID = '%1' " + "      AND upper (b.USERID) = upper ('%4')";
            sSQL = GlobalObjects.strfmt(sSQL, "PRD1", "db_report", Schema, UserName);

            OleDbCommand command = new OleDbCommand(sSQL, this.Connection);
            OleDbDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



    }
}
