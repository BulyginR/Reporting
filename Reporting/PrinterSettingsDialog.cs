using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
namespace Reporting
{
    public static class PrinterSettingsDialog
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        // native functions
        private static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int GlobalUnlock(IntPtr handle);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr GlobalFree(IntPtr handle);
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern long DocumentPropertiesW(IntPtr hWnd, IntPtr hPrinter, string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, Int32 fMode);

        public static PrinterSettings OpenPrinterPropertiesDialog(IntPtr FormHandle, PrinterSettings Settings)
        {
            // PrinterSettings+PageSettings -> hDEVMODE
            IntPtr hDevMode = Settings.GetHdevmode(Settings.DefaultPageSettings);

            // Show Dialog ( [In,Out] pDEVMODE )
            IntPtr pDevMode = GlobalLock(hDevMode);
            DocumentPropertiesW(FormHandle, IntPtr.Zero, Settings.PrinterName, pDevMode, pDevMode, 14);
            GlobalUnlock(hDevMode);

            // hDEVMODE -> PrinterSettings+PageSettings
            Settings.SetHdevmode(hDevMode);
            Settings.DefaultPageSettings.SetHdevmode(hDevMode);

            // cleanup
            GlobalFree(hDevMode);

            return Settings;
        }



    }

}
