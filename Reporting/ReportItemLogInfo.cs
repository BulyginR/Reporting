using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using ExtensionMethods;
namespace Reporting
{

    public class ReportItemLogInfo
    {

        public ReportApplication p_app;
        public int MaxCopies = 0;

        public int MaxOriginals = 0;
        public int MaxCopiesExportPdf = 0;
        public int MaxOriginalsExportPdf = 0;
        public int MaxCopiesExportXls = 0;

        public int MaxOriginalsExportXls = 0;

        public bool Autocopy = false;
        public int Copies;

        public int Originals;
        public int CopiesExportPdf;
        public int OriginalsExportPdf;
        public int CopiesExportXls;

        public int OriginalsExportXls;

        public int CopiesForPrint = 1;
        private object[] marrDocumentNums;
        private string mReportName;

        private string mWarehouse;
        public ReportItemLogInfo(ref ReportApplication _app, ref object[] arrDocumentNums, ref string ReportName, int _maxOriginals, int _maxCopies, int _maxOriginalsExportPdf, int _maxCopiesExportPdf, int _maxOriginalsExportXls, int _maxCopiesExportXls, bool _Autocopy,
        string warehouse = "")
        {
            p_app = _app;

            marrDocumentNums = arrDocumentNums;
            mReportName = ReportName;
            mWarehouse = warehouse;
            MaxOriginals = _maxOriginals;
            MaxCopies = _maxCopies;
            MaxOriginalsExportPdf = _maxOriginalsExportPdf;
            MaxCopiesExportPdf = _maxCopiesExportPdf;
            MaxOriginalsExportXls = _maxOriginalsExportXls;
            MaxCopiesExportXls = _maxCopiesExportXls;
            Autocopy = _Autocopy;
            //Refresh()
        }

        public bool CanPrintOriginal
        {
            get
            {
                if (MaxOriginals != -1)
                {
                    if (Originals + CopiesForPrint > MaxOriginals)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        public bool CanPrintCopies
        {
            get
            {
                if (MaxCopies != -1)
                {
                    if (Copies + CopiesForPrint > MaxCopies)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        public bool CanExportOriginal(ExportFormat ExportFormat)
        {
            int MaxOriginalsExport = 0;
            int OriginalsExport = 0;

            switch (ExportFormat)
            {
                case ExportFormat.Pdf:
                    MaxOriginalsExport = MaxOriginalsExportPdf;
                    OriginalsExport = OriginalsExportPdf;
                    break;
                case ExportFormat.Xls:
                    MaxOriginalsExport = MaxOriginalsExportXls;
                    OriginalsExport = OriginalsExportXls;
                    break;
            }

            if (MaxOriginalsExport != -1)
            {
                if (OriginalsExport + 1 > MaxOriginalsExport)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool CanExportCopies(ExportFormat ExportFormat)
        {
            int MaxCopiesExport = 0;
            int CopiesExport = 0;

            switch (ExportFormat)
            {
                case ExportFormat.Pdf:
                    MaxCopiesExport = MaxCopiesExportPdf;
                    CopiesExport = CopiesExportPdf;
                    break;
                case ExportFormat.Xls:
                    MaxCopiesExport = MaxCopiesExportXls;
                    CopiesExport = CopiesExportXls;
                    break;
            }

            if (MaxCopiesExport != -1)
            {
                if (CopiesExport + 1 > MaxCopiesExport)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }


        public void Refresh()
        {
            //string strTmp = "";
            //for (int i = 0; i < marrDocumentNums.Length; i += 1)
            //{
            //    strTmp = strTmp + "'" + marrDocumentNums[i] + "'" + ",";
            //}
            //strTmp = strTmp.Left(strTmp.Length - 1);

            //OleDbCommand Command = new OleDbCommand("SELECT  " + "ISNULL(SUM(CASE WHEN ORIGINAL = 1 AND PRINTFORMAT = 0 THEN PRINTAMOUNT ELSE 0 END),0) AS ORIGINAL, " + "ISNULL(SUM(CASE WHEN ORIGINAL = 0 AND PRINTFORMAT = 0 THEN PRINTAMOUNT ELSE 0 END),0) AS COPIES, " + "ISNULL(SUM(CASE WHEN ORIGINAL = 1 AND PRINTFORMAT = 1 THEN PRINTAMOUNT ELSE 0 END),0) AS ORIGINALEXPORTPDF, " + "ISNULL(SUM(CASE WHEN ORIGINAL = 0 AND PRINTFORMAT = 1 THEN PRINTAMOUNT ELSE 0 END),0) AS COPIESEXPORTPDF, " + "ISNULL(SUM(CASE WHEN ORIGINAL = 1 AND PRINTFORMAT = 2 THEN PRINTAMOUNT ELSE 0 END),0) AS ORIGINALEXPORTXLS, " + "ISNULL(SUM(CASE WHEN ORIGINAL = 0 AND PRINTFORMAT = 2 THEN PRINTAMOUNT ELSE 0 END),0) AS COPIESEXPORTXLS " + " FROM " + mWarehouse + (!string.IsNullOrEmpty(mWarehouse) ? "." : "") + "SrvPr_PRINTLOG WITH(NOLOCK) " + " WHERE REPORTNAME='" + mReportName + "' AND DOCNUMBER IN (" + strTmp + ")", p_app.AppLocation.Connection);

            //OleDbDataReader reader = Command.ExecuteReader();

            string strTmp = "";
            for (int i = 0; i < marrDocumentNums.Length; i += 1)
            {
                strTmp = strTmp + marrDocumentNums[i] + "#" + mReportName + "#1|";
            }
            strTmp = strTmp.TrimEnd('|');

            OleDbCommand command = new OleDbCommand("EXEC " + mWarehouse + ".[SrvPr_PrintedDocsGet] @DOCS = ?", p_app.AppLocation.Connection);
            OleDbParameter parDocs = new OleDbParameter("@parDocs", OleDbType.LongVarChar, -1);
            parDocs.Value = strTmp;
            command.Parameters.Add(parDocs);
            OleDbDataReader reader = command.ExecuteReader();


            Originals = 0;
            Copies = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Originals = reader.GetInt32(reader.GetOrdinal("ORIGINALS"));
                    Copies = reader.GetInt32(reader.GetOrdinal("COPIES"));
                    OriginalsExportPdf = reader.GetInt32(reader.GetOrdinal("ORIGINALSEXPORTPDF"));
                    CopiesExportPdf = reader.GetInt32(reader.GetOrdinal("COPIESEXPORTPDF"));
                    OriginalsExportXls = reader.GetInt32(reader.GetOrdinal("ORIGINALSEXPORTXLS"));
                    CopiesExportXls = reader.GetInt32(reader.GetOrdinal("COPIESEXPORTXLS"));
                }
            }


            reader.Close();




        }


    }
}
