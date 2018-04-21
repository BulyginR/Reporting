using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Reporting
{

    public partial class frmDebugLoger: Form
    {
        public frmDebugLoger()
        {
            // This call is required by the designer.
            InitializeComponent();
            // Add any initialization after the InitializeComponent() call.
            if (GlobalObjects.IsDebugMode)
            {
                txtLog.Text = "Приложение запущено в отладочном режиме" + " [" + String.Format("{0:HH:mm:ss.ffff}",DateTime.Now) + "]";
            }

        }

        public void AddMessage(string txt)
        {
            int selStart = txtLog.SelectionStart;
            int selLen = txtLog.SelectionLength;
            txtLog.Text = txtLog.Text + (txtLog.Text == ""?"":Environment.NewLine) + txt + " [" + String.Format("{0:HH:mm:ss.ffff}", DateTime.Now) + "]";
            txtLog.Select(selStart, selLen);
            txtLog.ScrollToCaret();
        }

        public void Clear()
        {
            txtLog.Text = "";
        }

        private void frmDebugLoger_Load(System.Object sender, System.EventArgs e)
        {
        }
    }

}
