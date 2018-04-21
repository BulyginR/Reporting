using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Reporting
{
    public class testGrid : TabHeaderObject
    {
        public DataGridView dg=new DataGridView();

        public override void RefreshTab(bool IsLoaded, ref TabPage tabPage)
        {
            if (IsLoaded) { return; }
            dg.Dock = DockStyle.Fill;
            tabPage.Controls.Add(dg);
            LoadDG();
        }
        public override void CloseTab()
        {
        }
        private void LoadDG()
        {
            dg.Columns.Add("Test1", "Test1");
            DataGridViewRow dgr = new DataGridViewRow();
            dgr.CreateCells(dg);
            dgr.Cells[0].Value = tabCaption;
            dg.Rows.Add(dgr);

        }
        public testGrid(string Caption)
        {
            base.tabCaption = Caption;
            base.tabControl = dg;
            tabControl.Dock = DockStyle.Fill;
        }
    }
}
