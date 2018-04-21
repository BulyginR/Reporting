using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reporting
{
    public class RFPrintDocument
    {
        public string WAREHOUSE;
        public int SERIALKEY;
        public string PTCID;
        public string USERID;
        public string WHSEID;
        public string SERVER;
        public string DOCTYPE;
        public string DOCKEY;
        public string PRNKEY;
        public string KEY1;
        public string KEY2;
        public string KEY3;
        public string KEY4;
        public string KEY5;
        public int STATUS;
        public string MESSAGE;
        public DateTime ADDDATE;
        public string ADDWHO;
        public string REPORTNAME;
        public string MAPING_DOCKEY;
        public string MAPING_DOCTYPE;
        public string MAPING_USERID;
        public string MAPING_KEY1;
        public string MAPING_KEY2;
        public string MAPING_KEY3;
        public string MAPING_KEY4;
        public string MAPING_KEY5;
        public string MAPING_DOCNUMBER;
        public bool PrinterExists;
        public List<string> reportParameters;
        public string[] reportDocs;
        public bool IsPrinted = false;

        public RFPrintDocument(List<string> printers, string _WAREHOUSE, int _SERIALKEY, string _PTCID, string _USERID, string _WHSEID, string _SERVER, string _DOCTYPE, string _DOCKEY, string _PRNKEY,
                            string _KEY1, string _KEY2, string _KEY3, string _KEY4, string _KEY5, int _STATUS, string _MESSAGE, DateTime _ADDDATE, string _ADDWHO, string _REPORTNAME,
            string _MAPING_DOCKEY, string _MAPING_DOCTYPE, string _MAPING_USERID, string _MAPING_KEY1, string _MAPING_KEY2, string _MAPING_KEY3, string _MAPING_KEY4, string _MAPING_KEY5, string _MAPING_DOCNUMBER)
        {
            WAREHOUSE = _WAREHOUSE;
            SERIALKEY = _SERIALKEY;
            PTCID = _PTCID;
            USERID = _USERID;
            WHSEID = _WHSEID;
            SERVER = _SERVER;
            DOCTYPE = _DOCTYPE;
            DOCKEY = _DOCKEY;
            PRNKEY = WAREHOUSE + "_" + _PRNKEY;
            KEY1 = _KEY1;
            KEY2 = _KEY2;
            KEY3 = _KEY3;
            KEY4 = _KEY4;
            KEY5 = _KEY5;
            STATUS = _STATUS;
            MESSAGE = _MESSAGE;
            ADDDATE = _ADDDATE;
            ADDWHO = _ADDWHO;
            REPORTNAME = _REPORTNAME;
            MAPING_DOCKEY = _MAPING_DOCKEY;
            MAPING_DOCTYPE = _MAPING_DOCTYPE;
            MAPING_USERID = _MAPING_USERID;
            MAPING_KEY1 = _MAPING_KEY1;
            MAPING_KEY2 = _MAPING_KEY2;
            MAPING_KEY3 = _MAPING_KEY3;
            MAPING_KEY4 = _MAPING_KEY4;
            MAPING_KEY5 = _MAPING_KEY5;
            MAPING_DOCNUMBER = _MAPING_DOCNUMBER;
            ConstructReportParameters();
            ConstructreportDocs();
            PrinterExists = printers.Count(s => s == PRNKEY) != 0;

        }
        private void ConstructreportDocs()
        {
             reportDocs = new string[] { GlobalObjects.strfmt(MAPING_DOCNUMBER, DOCKEY, KEY1, KEY2, KEY3, KEY4, KEY5) };
        }
        private void ConstructReportParameters()
        {
            reportParameters = new List<string>();
            reportParameters.Add(MAPING_DOCKEY);
            reportParameters.Add(DOCKEY);
            if (!String.IsNullOrEmpty(MAPING_DOCTYPE))
            {
                reportParameters.Add(MAPING_DOCTYPE);
                reportParameters.Add(DOCTYPE);
            }
            if (!String.IsNullOrEmpty(MAPING_USERID))
            {
                reportParameters.Add(MAPING_USERID);
                reportParameters.Add(USERID);
            }
            
            if (!String.IsNullOrEmpty(MAPING_KEY1))
            {
                reportParameters.Add(MAPING_KEY1);
                reportParameters.Add(KEY1);
            }
            if (!String.IsNullOrEmpty(MAPING_KEY2))
            {
                reportParameters.Add(MAPING_KEY2);
                reportParameters.Add(KEY2);
            }
            if (!String.IsNullOrEmpty(MAPING_KEY3))
            {
                reportParameters.Add(MAPING_KEY3);
                reportParameters.Add(KEY3);
            }
            if (!String.IsNullOrEmpty(MAPING_KEY4))
            {
                reportParameters.Add(MAPING_KEY4);
                reportParameters.Add(KEY4);
            }
            if (!String.IsNullOrEmpty(MAPING_KEY5))
            {
                reportParameters.Add(MAPING_KEY5);
                reportParameters.Add(KEY5);
            }
             //MessageBox.Show("Parameters: " + String.Join(",", reportParameters));

        }
    }
}
