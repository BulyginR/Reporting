using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstallPrinters
{
    public class PrinterInfo
    {
        public string PrinterName = "";
        public string DriverName = "";
        public string IpAdress = "";
        public string PortName = "";
        public PrinterInfo(string printerName, string driverName, string ipAdress)
        {
            PrinterName = printerName;
            DriverName = driverName;
            IpAdress = ipAdress;
        }
        public PrinterInfo(string printerName, string driverName, string ipAdress, string portName)
        {
            PrinterName = printerName;
            DriverName = driverName;
            PortName = portName;
        }
    }
}
