using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reporting
{
    public class RFPrintExecPeriod
    {
        public int PeriodNumber;

        private bool mPeriodComplete = false;
        public bool PeriodComplete
        {
            get
            {
                return mPeriodComplete;
            }
            private set { }
        }

        public List<RFPrintExecWarehousePeriod> Warehouses = new List<RFPrintExecWarehousePeriod>();

        public RFPrintExecPeriod(int periodNumber)
        {
            PeriodNumber = periodNumber;
        }
        public void TryCompleteEmpty()
        {
            try
            {
                if (Warehouses.Count(x => x.WarehouseComplete == false) == 0)
                {
                    mPeriodComplete = true;
                    if (GlobalObjects.IsDebugMode)
                        MessageBox.Show("{" + PeriodNumber + "} full complete");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("{" + PeriodNumber + "}error in RFPrintExecPeriod.TryCompleteEmpty: " + e.Message);
            }

        }
        public void AddWarehouses_Printers(string warehouse, string[] printers)
        {
            try
            {
                foreach (string item in printers)
                {
                    Warehouses.Add(new RFPrintExecWarehousePeriod(warehouse, item));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("{" + PeriodNumber + "}error in RFPrintExecPeriod.AddWarehouses_Printers: " + e.Message);
            }

        }

        public void SetWarehousePeriodComplete(string warehouse, string printer)
        {
            try
            {
                Warehouses.Find(x => x.Warehouse == warehouse & (printer == "" | x.PrinterName == printer)).WarehouseComplete = true;
                if (GlobalObjects.IsDebugMode)
                    MessageBox.Show("{" + PeriodNumber + "} for WAREHOUSE '" + warehouse + "'" + (printer != "" ? " and PRINTER '" + printer + "'" : "") + " complete");
                if (Warehouses.Count(x => x.WarehouseComplete == false) == 0)
                {
                    mPeriodComplete = true;
                    if (GlobalObjects.IsDebugMode)
                        MessageBox.Show("{" + PeriodNumber + "} full complete");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("{" + PeriodNumber + "}error in RFPrintExecPeriod.SetWarehousePeriodComplete: " + e.Message);
            }

        }
    }
    public class RFPrintExecWarehousePeriod
    {
        public string PrinterName;
        public string Warehouse;
        public bool WarehouseComplete;
        public RFPrintExecWarehousePeriod(string warehouse, string printerName)
        {
            Warehouse = warehouse;
            PrinterName = printerName;
            WarehouseComplete = false;
        }
    }
}
