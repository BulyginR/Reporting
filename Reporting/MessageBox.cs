using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Reporting
{
    public static class MessageBox : Object
    {
        
        public static  void Show(string text)
        {
            if (!GlobalObjects.IsServerMode)
                System.Windows.Forms.MessageBox.Show(text);
            else
            {
                WriteLog(text);
            }
        }
        public static  void Show(string text, string caption)
        {
            if (!GlobalObjects.IsServerMode)
                System.Windows.Forms.MessageBox.Show(text, caption);
            else
            {
                WriteLog(text);
            }
        }
        public static void Show(string text, string caption, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon icon)
        {
            if (!GlobalObjects.IsServerMode)
                System.Windows.Forms.MessageBox.Show(text, caption, buttons, icon);
            else
            {
                WriteLog(text);
            }
        }

        private static void WriteLog(string text)
        {
            using (FileStream fs = new FileStream(Constants.REPORTINGDLL_PATH + @"\" + String.Format("{0:yyyy.MM}_", DateTime.Now) + "GhostReporting_LOG.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    fs.Seek(0, SeekOrigin.End);
                    sw.WriteLine(String.Format("[{0:yyyy.MM.dd HH:mm:ss.ffff}]: ", DateTime.Now) + text);
                    sw.Flush();
                    //Thread.Sleep(3000);
                }
            }
            /*
            using (StreamWriter sw = new StreamWriter(Constants.REPORTINGDLL_PATH + @"\" + String.Format("{0:yyyy.MM}_", DateTime.Now) + "GhostReporting_LOG.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(String.Format("[{0:yyyy.MM.dd HH:mm:ss.ffff}]: ", DateTime.Now) + text);
            }
             * */
        }
    }
}
