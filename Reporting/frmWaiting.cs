using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Reporting
{
    public partial class frmWaiting : Form
    {
        private void frmWaiting_Load(System.Object sender, System.EventArgs e)
        {
            FormSizeCalculate();
            this.TopMost = true;
        }
        private void FormSizeCalculate()
        {
            //Me.ClientSize = New Drawing.Size(lblInfo.Width + lblInfo.Left + 3, Me.ClientSize.Height)
        }

        public frmWaiting(string strInfo = "Please, wait...")
        {
            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.

            lblInfo.Text = strInfo;

        }
        public void RefreshInfo(string strInfo)
        {
            lblInfo.Text = strInfo;
            FormSizeCalculate();
        }

        private void frmWaiting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void frmWaiting_FormClosing(object sender, FormClosedEventArgs e)
        {

        }
    }
}
