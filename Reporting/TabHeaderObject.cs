using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reporting
{
    public abstract class TabHeaderObject
    {
        public Control tabControl;
        public string tabCaption;
        public abstract void RefreshTab(bool IsLoaded, ref TabPage tabPage);
        public abstract void CloseTab();
    }
}
