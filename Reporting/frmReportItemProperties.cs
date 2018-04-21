using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;


namespace Reporting
{
    public partial class frmReportItemProperties : Form
    {

        private ReportItem mReportItem;
        private ReportApplication p_app;
        public frmReportItemProperties(ref ReportApplication _p_app, ref ReportItem _reportItem)
        {
            Activated += frmReportItemProperties_Activated;
            // This call is required by the designer.
            InitializeComponent();
            // Add any initialization after the InitializeComponent() call.
            mReportItem = _reportItem;
            p_app = _p_app;
        }

        private void frmReportItemProperties_Activated(object sender, System.EventArgs e)
        {
            lvItemProperties.Focus();
        }



        private void frmReportItemProperties_Load(object sender, System.EventArgs e)
        {
            this.Text = "Свойства отчета [" + mReportItem.ReportCaption + " [" + mReportItem.DocumentNums + "] ]";
            ListViewItem lvItem ;//= new ListViewItem();
            mReportItem.ReportItemLogInfo.Refresh();

            lvItem = this.lvItemProperties.Items.Add("Печать оригиналов");
            lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(mReportItem.ReportItemLogInfo.MaxOriginals, mReportItem.ReportItemLogInfo.Originals));
            lvItem.SubItems.Add((mReportItem.ReportItemLogInfo.MaxOriginals == -1 ? "не ограничено" : mReportItem.ReportItemLogInfo.MaxOriginals.ToString()));
            lvItem.SubItems.Add(mReportItem.ReportItemLogInfo.Originals.ToString());
            lvItem.SubItems.Add((mReportItem.ReportItemLogInfo.Autocopy ? "Да" : "Нет"));
            lvItem.Selected = true;
            lvItem = this.lvItemProperties.Items.Add("Печать копий");
            lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(mReportItem.ReportItemLogInfo.MaxCopies, mReportItem.ReportItemLogInfo.Copies));
            lvItem.SubItems.Add((mReportItem.ReportItemLogInfo.MaxCopies == -1 ? "не ограничено" : mReportItem.ReportItemLogInfo.MaxCopies.ToString()));
            lvItem.SubItems.Add(mReportItem.ReportItemLogInfo.Copies.ToString());

            lvItem = this.lvItemProperties.Items.Add("");
            lvItem = this.lvItemProperties.Items.Add("Экспорт в *.pdf оригиналов");
            lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(mReportItem.ReportItemLogInfo.MaxOriginalsExportPdf, mReportItem.ReportItemLogInfo.OriginalsExportPdf));
            lvItem.SubItems.Add((mReportItem.ReportItemLogInfo.MaxOriginalsExportPdf == -1 ? "не ограничено" : mReportItem.ReportItemLogInfo.MaxOriginalsExportPdf.ToString()));
            lvItem.SubItems.Add(mReportItem.ReportItemLogInfo.OriginalsExportPdf.ToString());

            lvItem = this.lvItemProperties.Items.Add("Экспорт в *.pdf копий");
            lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(mReportItem.ReportItemLogInfo.MaxCopiesExportPdf, mReportItem.ReportItemLogInfo.CopiesExportPdf));
            lvItem.SubItems.Add((mReportItem.ReportItemLogInfo.MaxCopiesExportPdf == -1 ? "не ограничено" : mReportItem.ReportItemLogInfo.MaxCopiesExportPdf.ToString()));
            lvItem.SubItems.Add(mReportItem.ReportItemLogInfo.CopiesExportPdf.ToString());

            lvItem = this.lvItemProperties.Items.Add("");
            lvItem = this.lvItemProperties.Items.Add("Экспорт в *.xls оригиналов");
            lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(mReportItem.ReportItemLogInfo.MaxOriginalsExportXls, mReportItem.ReportItemLogInfo.OriginalsExportXls));
            lvItem.SubItems.Add((mReportItem.ReportItemLogInfo.MaxOriginalsExportXls == -1 ? "не ограничено" : mReportItem.ReportItemLogInfo.MaxOriginalsExportXls.ToString()));
            lvItem.SubItems.Add(mReportItem.ReportItemLogInfo.OriginalsExportXls.ToString());

            lvItem = this.lvItemProperties.Items.Add("Экспорт в *.xls копий");
            lvItem.SubItems.Add(GlobalObjects.AllowForPrintExport(mReportItem.ReportItemLogInfo.MaxCopiesExportXls, mReportItem.ReportItemLogInfo.CopiesExportXls));
            lvItem.SubItems.Add((mReportItem.ReportItemLogInfo.MaxCopiesExportXls == -1 ? "не ограничено" : mReportItem.ReportItemLogInfo.MaxCopiesExportXls.ToString()));
            lvItem.SubItems.Add(mReportItem.ReportItemLogInfo.CopiesExportXls.ToString());


        }

        private void contextMenuViewHistory_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            object reports = new string[] { mReportItem.ReportName };
            p_app.ComCls.ShowPrintLog(mReportItem.DocumentNums + "#" + mReportItem.ReportName, ref reports, false, false, false);
        }


    }
}