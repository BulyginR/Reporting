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

namespace Reporting
{

    public class SoftwareExport
    {
        private ServerReport mServerReport;
        private ExportFormat mExportFormat;
        private string mReportCaption;

        private DirectoryInfo CurrExportDir;
        private List<Metafile> pages = new List<Metafile>();
        private PrintDocument doc = new PrintDocument();

        //private Microsoft.Reporting.WinForms.ReportPageSettings pageSettings;



        internal SoftwareExport(ServerReport serverReport, ExportFormat ExportFormat, ReportParameterCollection reportParameterCollection, DirectoryInfo _CurrExportDir, bool IsOriginal = true, string ReportCaption = "", string OldChangeUserFullName = "", ReportParameterCollection reportSelectedParameterCollection= null)
        {
            mServerReport = serverReport;
            mExportFormat = ExportFormat;
            //pageSettings = serverReport.GetDefaultPageSettings();
            CurrExportDir = _CurrExportDir;
            mReportCaption = ReportCaption;
            reportParameterCollection.Add(new ReportParameter("IsCopy", IsOriginal == false ? "true" : "false", false));
            mServerReport.SetParameters(reportParameterCollection);
            if(reportSelectedParameterCollection!=null)
                mServerReport.SetParameters(reportSelectedParameterCollection);
            if (!(OldChangeUserFullName == ""))
            {
                ReportParameter p1 = new ReportParameter("UserName", OldChangeUserFullName, false);
                mServerReport.SetParameters(p1);
            }

        }



        public void PrintReport()
        {

            try
            {

                string mimeType = "";
                string encoding = "";
                string filenameExtension = "";


                string sFileName = string.Format(mReportCaption + "_{0:yyyyMMdd_hhmmss}." + ExportFormatInfo.FileExtension(mExportFormat), DateTime.Now);

                char[] invalidFileChars = Path.GetInvalidFileNameChars();
                sFileName =  RelaceInvalidChars(invalidFileChars, sFileName);
                //MsgBox(sFileName)

                FileInfo file = new FileInfo(System.IO.Path.Combine(CurrExportDir.FullName, sFileName));
                //MsgBox(System.IO.Path.Combine( _
                //CurrExportDir.FullName, sFileName))
                if (!CurrExportDir.Exists)
                {
                    CurrExportDir.Create();
                }

                Warning[] warnings = null;
                string[] streamIDs = null;

                byte[] bytes = mServerReport.Render(ExportFormatInfo.FileFormatName(mExportFormat), string.Empty, PageCountMode.Actual, out mimeType, out  encoding, out filenameExtension, out streamIDs, out warnings);


                using (System.IO.FileStream fs = new System.IO.FileStream(file.FullName, System.IO.FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //doc.Print()
        }
        private byte[] GetStreamAsByteArray(System.IO.Stream stream)
        {

            int streamLength = Convert.ToInt32(stream.Length);

            byte[] fileData = new byte[streamLength + 1];

            // Read the file into a byte array
            stream.Read(fileData, 0, streamLength);
            stream.Close();

            return fileData;

        }

        private string RelaceInvalidChars(char[] charArray, string strFullFileName)
        {
            foreach (char someChar in charArray)
            {
                strFullFileName = strFullFileName.Replace(someChar,'_');
            }
            return strFullFileName;
        }
    }

}
