using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Reporting
{
    public partial class frmForcedCopy : Form
    {
        public string CurUserName = "";
        public string CurUserFullName = "";
        public bool closeOk;

        public ReportApplication p_app;

        public frmForcedCopy(ref ReportApplication _app)
        {

            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.

            p_app = _app;

        }


        private void OKButton_Click(System.Object sender, System.EventArgs e)
        {
            Location AuthLocation = new Location(p_app.AppLocation);

            AuthLocation.UserName = txtLogin.Text;
            AuthLocation.Password = txtPassword.Text;
            AuthLocation.SSPI = true;
            AuthLocation.DataBase = "PRD1";
            AuthLocation.CreateConnectionString();


            if (AuthLocation.OpenConnection(false, false))
            {
                if (AuthLocation.CheckGroupOldChange())
                {
                    closeOk = true;
                    p_app.IsOldChange = true;
                    // для каждого отчета
                    CurUserName = AuthLocation.UserName;
                    CurUserFullName = AuthLocation.UserFullName; // заменить на UserFullName!!!???   UserFullName_old()
                    //глобально чтобы не вводить каждый раз логин/пароль
                    p_app.OldChangeCurUserName = AuthLocation.UserName;
                    p_app.OldChangeCurUserFullName = AuthLocation.UserFullName;
                    p_app.OldChangeCurPassword = AuthLocation.Password;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Пользователь ''" + txtLogin.Text + "'' не является старшим смены");

                }
            }
            else
            {
                MessageBox.Show("Логин/пароль неверны");
            }

        }

        private void CancelButton_Click(System.Object sender, System.EventArgs e)
        {
            closeOk = false;
            this.Hide();
        }

        private void frmForcedCopy_Activated(object sender, System.EventArgs e)
        {
            if (!p_app.IsOldChange)
                txtLogin.Focus();
        }


        private void frmForcedCopy_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) //УТОЧНИТЬ;был номер 3, точно имелось в виду CloseReason.UserClosing?????
            {
                e.Cancel = true;
                closeOk = false;
                this.Hide();
            }
        }

        private void frmForcedCopy_Load(System.Object sender, System.EventArgs e)
        {
            if (p_app.IsOldChange)
            {
                txtLogin.Text = p_app.OldChangeCurUserName;
                txtPassword.Text = p_app.OldChangeCurPassword;
            }
        }


    }
}