using System;
namespace Reporting
{
    public static class ExportFormatInfo
    {
        public static string FileExtension(ExportFormat Format)
        {

            switch (Format)
            {
                case ExportFormat.Pdf:
                    return "pdf";
                case ExportFormat.Xls:
                    return "xls";
                default:
                    return "unknown";
            }
        }
        public static string FileExtentionPromt(ExportFormat Format)
        {

            switch (Format)
            {
                case ExportFormat.Pdf:
                    return "Документ *.pdf";
                case ExportFormat.Xls:
                    return "Документ *.xls";
                default:
                    return "unknown";
            }
        }
        public static string FileFormatName(ExportFormat Format)
        {

            switch (Format)
            {
                case ExportFormat.Pdf:
                    return "PDF";
                case ExportFormat.Xls:
                    return "EXCEL";
                default:
                    return "UNKNOWN";
            }
        }

    }
}
