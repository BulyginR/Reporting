using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Reporting
{
    public class TabControlSE : TabControl
    {
        private const int WM_HSCROLL = 0x114;
        public List<TabHeaderItem> TabHeaderItems = new List<TabHeaderItem>();
        public event ScrollEventHandler HScroll;

        private int oldValue = 0;

        private bool bAfterScroll = false;
        private StringFormat sf = new StringFormat();
        private StringFormat sfX = new StringFormat();

        private Font f;
        public int CloseTabButtOffsetLeftSide = 19;
        public int CloseTabButtOffsetUpSide = 6;
        public int CloseTabButtWidth = 15;

        public int CloseTabButtHeight = 14;

        public TabControlSE(string[] captionArr, Control parentControl)
        {
            InitializeMainProperties(parentControl);
            int i = 0;
            foreach (string item in captionArr)
            {
                ConstructTabListFromString(i, item);
                i++;
            }
        }
        public TabControlSE(List<TabHeaderObject> tabs, Control parentControl)
        {
            InitializeMainProperties(parentControl);
            int i = 0;
            foreach (TabHeaderObject item in tabs)
            {
                ConstructTabList(i, item);
                i++;
            }
        }
        private void ConstructTabList(int ItemIndex, TabHeaderObject tabItem)
        {
            TabHeaderItems.Add(new TabHeaderItem(this, tabItem));
            TabPages.Add(TabHeaderItems[ItemIndex].tabPage);
            TabHeaderItems[ItemIndex].SetMyTabRect(ItemIndex);

            TabHeaderItems[ItemIndex].MoveNotOverClose += TabControlSE_DrawCloseTabButt;
            TabHeaderItems[ItemIndex].MoveOverClose += TabControlSE_DrawCloseTabButt;
            //прогружаем первую вкладку
            if (ItemIndex == 0 & TabHeaderItems[ItemIndex].tabHeaderObject != null) 
            { 
                TabHeaderItems[ItemIndex].Refresh(); 

            }
        }
        private void ConstructTabListFromString(int ItemIndex, string tabCaption)
        {
            //для тестов
            TabHeaderItems.Add(new TabHeaderItem(this, tabCaption));
            TabPages.Add(TabHeaderItems[ItemIndex].tabPage);
            TabHeaderItems[ItemIndex].SetMyTabRect(ItemIndex);

            TabHeaderItems[ItemIndex].MoveNotOverClose += TabControlSE_DrawCloseTabButt;
            TabHeaderItems[ItemIndex].MoveOverClose += TabControlSE_DrawCloseTabButt;
            //прогружаем первую вкладку
            if (ItemIndex == 0 & TabHeaderItems[ItemIndex].tabHeaderObject != null) { TabHeaderItems[ItemIndex].Refresh(); }
        }
        private void InitializeMainProperties(Control parentControl)
        {
            if (!(parentControl == null))
            {
                parentControl.Controls.Add(this);
                this.Dock = DockStyle.Fill;
            }

            f = new Font(this.Font.FontFamily, 5, FontStyle.Bold);
            this.ItemSize = new Size(0, 22);
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.NoWrap;

            sfX.Alignment = StringAlignment.Center;
            sfX.LineAlignment = StringAlignment.Center;
            sfX.FormatFlags = StringFormatFlags.NoWrap;

            DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            SizeMode = TabSizeMode.FillToRight;

            base.DrawItem += TabControlSE_DrawItem;
            base.MouseClick += TabControlSE_MouseClick;
            base.ControlRemoved += TabControlSE_ControlRemoved;
            base.Selecting += TabControlSE_Selecting;
            this.HScroll += TabControlSE_HScroll;
            base.KeyUp += TabControlSE_KeyUp;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }


        private void TabControlSE_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Rectangle TabRec = this.GetTabRect(e.Index);

            Color Clr = e.State == DrawItemState.Selected ? Color.White : Color.LightGray;

            e.Graphics.FillRectangle(new SolidBrush(Clr), TabRec);

            TabControlSE_DrawCloseTabButt(sender, TabRec, e);


            e.Graphics.DrawString(this.TabPages[e.Index].Text, this.Font, Brushes.Black, TabRec, sf);

            TabRec.Inflate(-3, -3);
            e.DrawFocusRectangle();
        }

        private void TabControlSE_DrawCloseTabButt(object sender, Rectangle TabRec, DrawItemEventArgs e = null, bool IsActive = false, bool IsSelected = false)
        {

            Rectangle BTNRec = new Rectangle(TabRec.X + TabRec.Width - CloseTabButtOffsetLeftSide, CloseTabButtOffsetUpSide, CloseTabButtWidth, CloseTabButtHeight);

            //при прорисовке (активной и неактивной вкладки соотв.
            if ((e != null))
            {
                if (e.State == DrawItemState.Selected)
                {
                    e.Graphics.FillRectangle(Brushes.DarkRed, BTNRec);
                    e.Graphics.DrawString("X", f, Brushes.AntiqueWhite, BTNRec, sfX);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.IndianRed, BTNRec);
                    e.Graphics.DrawString("X", f, Brushes.AntiqueWhite, BTNRec, sfX);
                }
                //при наведении мыши
            }
            else
            {
                Graphics g = this.CreateGraphics();
                //если мышь над кнопкой закрытия
                if (IsActive)
                {

                    g.FillRectangle(Brushes.Red, BTNRec);
                    g.DrawString("X", f, Brushes.AntiqueWhite, BTNRec, sfX);
                    //если мышь НЕ над кнопкой закрытия
                }
                else
                {
                    //если вкладка активна
                    if (IsSelected)
                    {
                        g.FillRectangle(Brushes.DarkRed, BTNRec);
                        g.DrawString("X", f, Brushes.AntiqueWhite, BTNRec, sfX);

                        //если вкладка не активна
                    }
                    else
                    {
                        g.FillRectangle(Brushes.IndianRed, BTNRec);
                        g.DrawString("X", f, Brushes.AntiqueWhite, BTNRec, sfX);
                    }
                }
            }
        }

        private void TabControlSE_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int OldSelectedIndex = this.SelectedIndex;
            //Rectangle Rec = this.GetTabRect(OldSelectedIndex);
            //Rectangle HitTest = new Rectangle(Rec.X + Rec.Width - CloseTabButtOffsetLeftSide, CloseTabButtOffsetUpSide, CloseTabButtWidth, CloseTabButtHeight);
            Rectangle HitTest = TabHeaderItems[OldSelectedIndex].TabRectClose;
            if (HitTest.Contains(new Point(e.X, e.Y)))
            {
                if (!(this.TabPages.Count == 1))
                {
                    //закрываем вкладку
                    TabHeaderItems[OldSelectedIndex].MoveNotOverClose -= TabControlSE_DrawCloseTabButt;
                    TabHeaderItems[OldSelectedIndex].MoveOverClose -= TabControlSE_DrawCloseTabButt;
                    TabHeaderItems[OldSelectedIndex].tabHeaderObject.CloseTab();
                    this.TabHeaderItems.Remove(TabHeaderItems[OldSelectedIndex]);

                    this.TabPages.Remove(this.SelectedTab);
                    this.SelectedIndex = (OldSelectedIndex - 1 >= 0 ? OldSelectedIndex - 1 : 0);
                }
            }
            else
            {
                TabHeaderItems[OldSelectedIndex].Refresh();
            }
        }
        private void TabControlSE_HScroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            // обновляет все зоны вкладок при горизонтальном скролле
            UpdateAfterPosChanged();

        }

        private void TabControlSE_ControlRemoved(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            UpdateAfterPosChanged();
        }

        private void TabControlSE_Selecting(object sender, System.Windows.Forms.TabControlCancelEventArgs e)
        {
            if (base.TabPages.Count == 0)
                return;
            UpdateAfterPosChanged();
        }


        private void TabControlSE_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {
                TabHeaderItems[this.SelectedIndex].Refresh();
            }
        }

        private void UpdateAfterPosChanged()
        {
            //после удаления вкладки или скролла необходимо обновить для каждого ReportTab обрабатываемый кусок
            int i = 0;
            foreach (TabHeaderItem Item in TabHeaderItems)
            {
                Item.SetMyTabRect(i);
                i += 1;
            }
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_HSCROLL)
            {
                if (bAfterScroll)
                {
                    bAfterScroll = false; return;
                }
                bAfterScroll = true;
                if (HScroll != null)
                {
                    HScroll(this, new ScrollEventArgs(LoWord(m.WParam), oldValue, HiWord(m.WParam), ScrollOrientation.HorizontalScroll));
                }
            }
        }



        private ScrollEventType LoWord(IntPtr dWord)
        {
            int i;
            i = dWord.ToInt32() | 0xffff;
            return (ScrollEventType)i;
        }

        private int HiWord(IntPtr dWord)
        {


            if ((dWord.ToInt32() |  0x80000000) == 0x80000000)
            {
                return (dWord.ToInt32() >> 16);
            }
            else
            {
                return (dWord.ToInt32() >> 16) | 0xffff;
            }
        }
    }
}