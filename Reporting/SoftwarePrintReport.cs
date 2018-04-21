using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Reflection;

namespace Reporting
{
    public class SoftwarePrintReport
    {
        public float dtGeneratePagesTime = 0; public DateTime dtGeneratePagesTimeStart;
        public float dtAddParamsTime = 0; public DateTime dtAddParamsTimeStart;
        public float dtOtherInitOperations = 0; public DateTime dtOtherInitOperationsStart;
        public float dtGenerateOtherTime = 0; public DateTime dtGenerateOtherTimeStart;
        public float dtGeneratePrintingTime = 0; public DateTime dtGeneratePrintingTimeStart;

        private ServerReport mServerReport;
        private List<Metafile> pages = new List<Metafile>();
        private int pageIndex = 0;
        private PrintDocument doc = new PrintDocument();

        private ReportPageSettings pageSettings;
        public SoftwarePrintReport(ServerReport serverReport, PrinterSettings printerSettings, ReportParameterCollection reportParameterCollection, bool IsOriginal = true, string ReportCaption = "", bool IsDuplex = false, string OldChangeUserFullName = "", ReportParameterCollection reportSelectedParameterCollection = null)
        {
            dtOtherInitOperationsStart = DateTime.Now;
            mServerReport = serverReport;
            pageSettings = serverReport.GetDefaultPageSettings();
            doc.PrinterSettings = printerSettings;
            if (doc.PrinterSettings.CanDuplex & IsDuplex)
            {
                if(!pageSettings.IsLandscape)
                    doc.PrinterSettings.Duplex = Duplex.Vertical;
                else
                    doc.PrinterSettings.Duplex = Duplex.Horizontal;
                //IIf(pageSettings.IsLandscape, Duplex.Horizontal, Duplex.Vertical)
            }
            else
            {
                doc.PrinterSettings.Duplex = Duplex.Simplex;
            }

            PaperSize PaperSize = new PaperSize(pageSettings.PaperSize.PaperName, pageSettings.PaperSize.Width, pageSettings.PaperSize.Height);


            doc.DefaultPageSettings.PaperSize = PaperSize;

            if (!string.IsNullOrEmpty(ReportCaption))
            {
                doc.DocumentName = ReportCaption;
            }
            dtOtherInitOperations = GlobalObjects.DateDiff(dtOtherInitOperationsStart);
            dtAddParamsTimeStart = DateTime.Now;
            reportParameterCollection.Add(new ReportParameter("IsCopy", IsOriginal == false ? "true" : "false", false));
            mServerReport.SetParameters(reportParameterCollection);

            if (reportSelectedParameterCollection != null)
                mServerReport.SetParameters(reportSelectedParameterCollection);

            if (!(OldChangeUserFullName == ""))
            {
                ReportParameter p1 = new ReportParameter("UserName", OldChangeUserFullName, false);
                mServerReport.SetParameters(p1);
            }

            dtAddParamsTime = GlobalObjects.DateDiff(dtAddParamsTimeStart);
        }


        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {

            Metafile page = pages[pageIndex];
            pageIndex += 1;

            float pWidth = (pageSettings.IsLandscape ? pageSettings.PaperSize.Height : pageSettings.PaperSize.Width) * (float)Constants.PRINTSCALE;
            float pHeight = (pageSettings.IsLandscape ? pageSettings.PaperSize.Width : pageSettings.PaperSize.Height) * (float)Constants.PRINTSCALE;
            if (pHeight > pWidth)
            {
                pWidth = pWidth * (float)Constants.PRINTSCALE;
                pHeight = pHeight * (1 - ((1 - (float)Constants.PRINTSCALE) * (pWidth / pHeight)));
            }
            else
            {
                pHeight = pHeight * (float)Constants.PRINTSCALE;
                pWidth = pWidth * (1 - ((1 - (float)Constants.PRINTSCALE) * (pHeight / pWidth)));
            }

            e.Graphics.DrawImage(page, 0, 0, pWidth, pHeight);
            e.HasMorePages = pageIndex < pages.Count;

        }


        public int PrintReport()
        {
            int pageCount = 0;
            //ВРЕМЕННО!!
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("{}PrintReport...(REPORTNAME:" + doc.DocumentName + ")");
            dtGenerateOtherTimeStart = DateTime.Now;
            doc.PrintPage += PrintPageHandler;
            doc.DefaultPageSettings.Landscape = pageSettings.IsLandscape;
            string deviceInfo = "<DeviceInfo>" +
                "<OutputFormat>emf</OutputFormat>" +
                "  <PageWidth>" + Convert.ToSingle(pageSettings.IsLandscape ? pageSettings.PaperSize.Height : pageSettings.PaperSize.Width) / Convert.ToSingle(100) +
                "in</PageWidth>" +
                "  <PageHeight>" + Convert.ToSingle(pageSettings.IsLandscape ? pageSettings.PaperSize.Width : pageSettings.PaperSize.Height) / Convert.ToSingle(100) +
                "in</PageHeight>" +
                "  <MarginTop>" + Convert.ToSingle(pageSettings.Margins.Top) / Convert.ToSingle(100) + "in</MarginTop>" +
                "  <MarginLeft>" + Convert.ToSingle(pageSettings.Margins.Left) / Convert.ToSingle(100) + "in</MarginLeft>" +
                "  <MarginRight>" + Convert.ToSingle(pageSettings.Margins.Right) / Convert.ToSingle(100) + "in</MarginRight>" +
                "  <MarginBottom>" + Convert.ToSingle(pageSettings.Margins.Bottom) / Convert.ToSingle(100) + "in</MarginBottom>" +
                "</DeviceInfo>";

            string mimeType = "";
            string filenameExtension = "";

            NameValueCollection firstPageParameters = new NameValueCollection();
            firstPageParameters.Add("rs:PersistStreams", "True");

            NameValueCollection nonFirstPageParameters = new NameValueCollection();
            nonFirstPageParameters.Add("rs:GetNextStream", "True");
            dtGenerateOtherTime = GlobalObjects.DateDiff(dtGenerateOtherTimeStart);
            dtGenerateOtherTimeStart = DateTime.Now;
            Stream pageStream = mServerReport.Render("IMAGE", deviceInfo, firstPageParameters, out mimeType, out filenameExtension);

            while (pageStream.Length > 0)
            {
                pageCount += 1;
                pages.Add(new Metafile(pageStream));
                pageStream = mServerReport.Render("IMAGE", deviceInfo, nonFirstPageParameters, out mimeType, out filenameExtension);
            }
            dtGeneratePagesTime = GlobalObjects.DateDiff(dtGenerateOtherTimeStart);
            dtGeneratePrintingTimeStart = DateTime.Now;
            doc.PrintController = new System.Drawing.Printing.StandardPrintController();
            doc.Print();
            //ВРЕМЕННО!!
            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("{}PrintReport...OK(REPORTNAME:" + doc.DocumentName + ")");
            dtGeneratePrintingTime = dtGeneratePrintingTime + GlobalObjects.DateDiff(dtGeneratePrintingTimeStart);
            return pageCount;
        }


    }
}
