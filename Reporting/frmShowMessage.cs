using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
namespace Reporting
{
    public partial class frmShowMessage : System.Windows.Forms.Form
    {
        public ShowMessageAnswer Answer = ShowMessageAnswer.vbCancel;
        public enum ShowMessageMode
        {
            OkOnly = 0,
            YesNoCancel = 1,
            OkCancel = 2
        }
        public enum ShowMessageAnswer
        {
            vbOK = 0,
            vbYes = 1,
            vbNo = 2,
            vbCancel = 3
        }



        public frmShowMessage(ShowMessageMode Mode, string msgText)
        {
            // This call is required by the designer.
            InitializeComponent();
            // Add any initialization after the InitializeComponent() call.
            switch (Mode)
            {
                case ShowMessageMode.YesNoCancel:
                    btnOk.Visible = false;
                    break;
                case ShowMessageMode.OkCancel:
                    btnYes.Visible = false;
                    btnNo.Visible = false;
                    break;
            }
            lblMessage.Text = msgText;

        }

        private void btnYes_Click(System.Object sender, System.EventArgs e)
        {
            Answer = ShowMessageAnswer.vbYes;
            this.Hide();
        }

        private void btnNo_Click(System.Object sender, System.EventArgs e)
        {
            Answer = ShowMessageAnswer.vbNo;
            this.Hide();
        }

        private void btnCancel_Click(System.Object sender, System.EventArgs e)
        {
            Answer = ShowMessageAnswer.vbCancel;
            this.Hide();
        }

        private void btnOk_Click(System.Object sender, System.EventArgs e)
        {
            Answer = ShowMessageAnswer.vbOK;
            this.Hide();
        }


        private void frmShowMessage_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void frmShowMessage_Load(object sender, System.EventArgs e)
        {
            this.ClientSize = new Size(this.ClientSize.Width, lblMessage.Top + lblMessage.Height + PanelOkCancel.Height);
        }
    }
}
