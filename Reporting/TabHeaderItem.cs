using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reporting
{
    public class TabHeaderItem
    {
        private bool IsLoaded = false;
        public TabHeaderObject tabHeaderObject;
        public TabPage tabPage = new TabPage();
        private TabControlSE withEventsField_mTabControl;
        private TabControlSE mTabControl
        {
            get { return withEventsField_mTabControl; }
            set
            {
                if (withEventsField_mTabControl != null)
                {
                    withEventsField_mTabControl.MouseLeave -= mTabControl_MouseLeave;
                    withEventsField_mTabControl.MouseMove -= mTabControl_MouseMove;
                }
                withEventsField_mTabControl = value;
                if (withEventsField_mTabControl != null)
                {
                    withEventsField_mTabControl.MouseLeave += mTabControl_MouseLeave;
                    withEventsField_mTabControl.MouseMove += mTabControl_MouseMove;
                }
            }

        }
        private bool mIsMouseOverClose;
        public event MoveOverCloseEventHandler MoveOverClose;
        public delegate void MoveOverCloseEventHandler(TabControlSE mTabControl, Rectangle TabRec, DrawItemEventArgs e, bool IsActive, bool IsSelectedTab);
        public event MoveNotOverCloseEventHandler MoveNotOverClose;
        public delegate void MoveNotOverCloseEventHandler(TabControlSE mTabControl, Rectangle TabRec, DrawItemEventArgs e, bool IsActive, bool IsSelectedTab);
        private int TabNum;
        public Rectangle TabRect;
        public Rectangle TabRectClose;

        public TabHeaderItem(TabControlSE TabControl, string Caption, Color? tabColor = null)
        {
            //к удалению
            mTabControl = TabControl;
            tabPage.Text = "          " + Caption + "          ";
            tabPage.BackColor = tabColor ?? Color.White;
            tabPage.Controls.Add(tabHeaderObject.tabControl);
        }
        public TabHeaderItem(TabControlSE TabControl, TabHeaderObject item, Color? tabColor = null)
        {
            mTabControl = TabControl;
            tabHeaderObject = item;
            tabPage.Text = " " + item.tabCaption + "       ";
            tabPage.BackColor = tabColor ?? Color.White; ;
            //tabPage.Controls.Add(item.tabControl);
        }
        public void Refresh()
        {
            tabHeaderObject.RefreshTab(IsLoaded, ref tabPage);
            IsLoaded = true;
        }
        public void SetMyTabRect(int ItemNum)
        {
            TabNum = ItemNum;
            TabRect = mTabControl.GetTabRect(ItemNum);
            TabRectClose = new Rectangle(TabRect.X + TabRect.Width - mTabControl.CloseTabButtOffsetLeftSide, mTabControl.CloseTabButtOffsetUpSide, mTabControl.CloseTabButtWidth, mTabControl.CloseTabButtHeight);
        }

        private void mTabControl_MouseLeave(object sender, System.EventArgs e)
        {
            if (mIsMouseOverClose)
            {
                if (MoveNotOverClose != null)
                {
                    MoveNotOverClose(mTabControl, TabRect, null, false, mTabControl.SelectedIndex == TabNum ? true : false);
                }
                mIsMouseOverClose = false;
            }
        }


        private void mTabControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (TabRect.Contains(p))
            {
                if (TabRectClose.Contains(p))
                {
                    if (!mIsMouseOverClose)
                    {
                        if (MoveOverClose != null)
                        {
                            MoveOverClose(mTabControl, TabRect, null, true, mTabControl.SelectedIndex == TabNum ? true : false);
                        }
                        mIsMouseOverClose = true;
                    }

                }
                else
                {
                    if (mIsMouseOverClose)
                    {
                        if (MoveNotOverClose != null)
                        {
                            MoveNotOverClose(mTabControl, TabRect, null, false, mTabControl.SelectedIndex == TabNum ? true : false);
                        }
                        mIsMouseOverClose = false;
                    }
                }
            }
            else
            {
                if (mIsMouseOverClose)
                {
                    if (MoveNotOverClose != null)
                    {
                        MoveNotOverClose(mTabControl, TabRect, null, false, mTabControl.SelectedIndex == TabNum ? true : false);
                    }
                    mIsMouseOverClose = false;
                }

            }
        }
    }
}