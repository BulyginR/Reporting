using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Reporting
{
    public partial class frmReportsPreview : Form
    {

        private TabControlSE mTabControl;

        private ReportApplication p_app;


        public frmReportsPreview(ref ReportApplication _app)
        {
            // This call is required by the designer.
            InitializeComponent();

            // Add any initialization after the InitializeComponent() call.

            p_app = _app;
            List<TabHeaderObject> tabHeaderObjectList = new List<TabHeaderObject>();
            foreach (ReportItem Item in p_app.Reports)
            {
                if (Item.IsChecked)
                {
                    Item.frmContainer = this;
                    tabHeaderObjectList.Add((TabHeaderObject)Item);
                }
            }
            mTabControl = new TabControlSE(tabHeaderObjectList, panReports);

            var _with1 = mTabControl;
            _with1.DrawMode = TabDrawMode.OwnerDrawFixed;
            _with1.ItemSize = new Size(0, 25);

            this.Text = this.Text + " [" + Constants.SERVER_NAME + "]";
            this.label1.Top = panReports.Height - this.label1.Height + 5;

        }

        private void frmReportsPreview_Resize(object sender, EventArgs e)
        {
            this.label1.Top = panReports.Height - this.label1.Height+5;
        }

    }


}
