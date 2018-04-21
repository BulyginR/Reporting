using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Reporting
{
    public partial class frmReportsManager : Form
    {
        public ReportApplication p_app;
        private OleDbDataAdapter da;

        private DataTable DaTabl = new DataTable();



        private void FillDGW()
        {
            DaTabl = new DataTable();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM dbo.SrvPr_REPORTS", p_app.AppLocation.Connection);

            da = new OleDbDataAdapter(cmd);
            OleDbCommandBuilder b = new OleDbCommandBuilder(da);
            da.Fill(DaTabl);


            dgwReports.DataSource = DaTabl;
            foreach (DataGridViewColumn col in dgwReports.Columns)
            {
                if (col.ReadOnly)
                    col.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            }
        }


        public frmReportsManager(ReportApplication _p_app)
        {
            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.
            p_app = _p_app;
            FillDGW();
            if (GlobalObjects.IsDebugMode == true)
                lblDebug.Visible = true;

        }

        private void btnOk_Click(System.Object sender, System.EventArgs e)
        {
            try
            {
                da.Update(DaTabl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, p_app.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Изменения сохранены", p_app.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //MessageBox.Show("Изменения сохранены");
            //this.Close();
        }


        private void btnCancel_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }
        string serviceCode = "";
        private void dgwReports_KeyPress(object sender, KeyPressEventArgs e)
        {
            serviceCode += e.KeyChar;
            int findServiceWord = serviceCode.IndexOf("showdebug", 0);
            if (findServiceWord >= 0)
            {
                GlobalObjects.IsDebugMode = true;
                lblDebug.Visible = true;
            }
        }

        private void dgwReports_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (p_app == null)
                return;
            string user = p_app.AppLocation.UserName;
            string[] input = { "EDITDATE", 
                           "EDITWHO", 
                           "ADDDATE",
                             "ADDWHO"};
            List<string> arr = new List<string>(input);

            if (!arr.Contains(dgwReports.Columns[e.ColumnIndex].Name))
            {
                dgwReports.Rows[e.RowIndex].Cells["EDITDATE"].Value = DateTime.Now;
                dgwReports.Rows[e.RowIndex].Cells["EDITWHO"].Value = user;
            }
            
        }
    }

}
