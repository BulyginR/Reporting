using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;
using ExtensionMethods;
namespace Reporting
{
    public delegate void GlobalContolAddedEventHander(object sender, ControlEventArgs e);

    public partial class frmReportBuilder : Form
    {
        [Category("Action")]
        [Description("Fires when any control on the form is clicked.")]
        public event GlobalContolAddedEventHander GlobalContolAdded;

        private ReportViewer CtlReportViewer;
        public ReportItem objReportItem;
        public bool CloseOk = false;

        public bool IsOkButtonClicked = false;

        private bool mSubmitOnlyOnViewReport = false;
        private bool mChangeDropDownHeight = false;
        private Button btnViewReport;
        private bool mAditionalQuestion = false;
        private string mAdditionalQuestionText = "";
        private System.Drawing.Size formCustomSize = System.Drawing.Size.Empty;

        private bool mIsLoaded = false;
        private bool mTryReloadMultiParams = false;
        private List<string> mNotLoadedParameters =new List<string>();

        public frmReportBuilder(ReportItem _report, bool submitOnlyOnViewReport = false, string buttonOkText = "OK", bool additionalQuestion = false, string additionalQuestionText = "", int initPromptHeight = 0, bool changeDropDownHeight = false, int formHeight = 0, int formWidth = 0, bool tryReloadMultiParams = false, bool canExport = false)
        {
            // This call is required by the designer.
            InitializeComponent();
            mTryReloadMultiParams = tryReloadMultiParams;
            BindControlControlAdded(this);
            this.GlobalContolAdded +=new GlobalContolAddedEventHander(frmReportBuilder_GlobalContolAdded);

            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Init frmReportBuilder starts...");
            // Add any initialization after the InitializeComponent() call.
            _report.ConstructCtlReport(-1, true, this);
            mSubmitOnlyOnViewReport = submitOnlyOnViewReport;
            mChangeDropDownHeight = changeDropDownHeight;

            CtlReportViewer = _report.CtlReportViewer;
            btnViewReport = (Button)CtlReportViewer.Controls.Find("viewReport", true)[0];

            if (submitOnlyOnViewReport) // changeDropDownHeight - было по ошибке
            {
                btnViewReport.MouseDown += new MouseEventHandler(btnViewReport_MouseDown);
                btnViewReport.MouseUp += new MouseEventHandler(btnViewReport_MouseUp);
            }
            if (formHeight != 0 || formWidth != 0)
            {
                this.WindowState = FormWindowState.Normal;
                formCustomSize = new System.Drawing.Size(formWidth == 0 ? this.Width : formWidth, formHeight == 0 ? this.Height : formHeight);
            }
            this.btnOk.Text = buttonOkText;
            mAditionalQuestion = additionalQuestion;
            mAdditionalQuestionText = additionalQuestionText;

            if (initPromptHeight > 0)
            {
                object container = CtlReportViewer.Controls[0];
                PropertyInfo prop = container.GetType().GetProperty("Panel1MinSize");
                prop.SetValue(container, initPromptHeight, null);
            }

            objReportItem = _report;
            this.Text = "Ввод данных [" + _report.ReportCaption + "]";
            pnlBody.Controls.Add(CtlReportViewer);

            CtlReportViewer.Dock = DockStyle.Fill;

            CtlReportViewer.BorderStyle = BorderStyle.None;
            CtlReportViewer.Padding = new Padding(0);
            CtlReportViewer.Margin = new Padding(0);
            CtlReportViewer.ShowDocumentMapButton = false;
            CtlReportViewer.ShowPrintButton = false;
            CtlReportViewer.ShowContextMenu = false;
            CtlReportViewer.ShowFindControls = false;
            CtlReportViewer.ShowExportButton = canExport;
            CtlReportViewer.ShowRefreshButton = false;
            CtlReportViewer.RefreshReport();

            if (GlobalObjects.IsDebugMode)
                GlobalObjects.AddLogMessage("Init frmReportBuilder...ОК");
        }

        private void btnViewReport_MouseDown(System.Object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                IsOkButtonClicked = true;
        }
        private void btnViewReport_MouseUp(System.Object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                IsOkButtonClicked = false;
        }


        private void btnOk_Click(System.Object sender, System.EventArgs e)
        {

            if (mAditionalQuestion)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(mAdditionalQuestionText, objReportItem.ReportCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                    return;
            }
            if (!mSubmitOnlyOnViewReport)
            {
                IsOkButtonClicked = true;
                //необходимо для того чтобы параметры в objReportItem прописывались только 
                //когда пользователь нажал кнопку ОК
                btnViewReport.PerformClick();
                IsOkButtonClicked = false;
            }

            if ((objReportItem.ReportparametersSelected != null))
            {
                CloseOk = true;
                this.Hide();
            }
            else
            {
                if (mSubmitOnlyOnViewReport)
                    MessageBox.Show("Сначала нажмите кнопку ''Показать отчет''", objReportItem.ReportCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void btnCancel_Click(System.Object sender, System.EventArgs e)
        {
            this.Hide();
        }


        private void findcontrols(Control root)
        {

            foreach (Control cntrl in root.Controls)
            {

                findcontrols(cntrl);
                if (cntrl.Name.StartsWith(""))
                {
                    //TextBox1.Text = TextBox1.Text & cntrl.Name & "/"
                }
            }

        }


        private void frmReportBuilder_Shown(object sender, EventArgs e)
        {
            SetComboBoxDropDownHeight();
        }
        private void BindControlControlAdded(Control con)
        {
            con.ControlAdded += delegate(object sender, ControlEventArgs e)
            {
                TriggerMouseClicked(sender, e);
            };
            // bind to controls already added
            foreach (Control i in con.Controls)
            {
                BindControlControlAdded(i);
            }
            // bind to controls added in the future
            con.ControlAdded += delegate(object sender, ControlEventArgs e)
            {
                BindControlControlAdded(e.Control);
            };
        }

        private void TriggerMouseClicked(object sender, ControlEventArgs e)
        {
            if (GlobalContolAdded != null)
            {
                GlobalContolAdded(sender, e);
            }
        }
        private void frmReportBuilder_GlobalContolAdded(object sender, ControlEventArgs e)
        {
            try
            {
                string ctlType = e.Control.GetType().Name;
                if (mChangeDropDownHeight && ctlType == "ValidValuesControl")
                {
                    SetComboBoxDropDownHeight();
                }
                if (mTryReloadMultiParams) //Попытка загрузить указанные значения для каскадных параметров
                {


                    if (!mIsLoaded && ctlType == "MultiValueTextControl")
                    {
                        var reportInfo = e.Control.GetType().GetProperty("ParameterInfo").GetValue(e.Control, null);
                        var validValues = reportInfo.GetType().GetProperty("ValidValues").GetValue(reportInfo, null);
                        var objAreValidValuesQueryBased = reportInfo.GetType().GetProperty("AreValidValuesQueryBased").GetValue(reportInfo, null);
                        bool bAreValidValuesQueryBased = (bool)objAreValidValuesQueryBased;
                        string paramName = reportInfo.GetType().GetProperty("Name").GetValue(reportInfo, null).ToString();
                        if (bAreValidValuesQueryBased && validValues == null && !mNotLoadedParameters.Exists(x => x == paramName) && objReportItem.Reportparameters.ToList<ReportParameter>().Exists(x => x.Name == paramName))
                        {
                            mNotLoadedParameters.Add(paramName);
                        }

                    }
                    if (mNotLoadedParameters.Count > 0 && mIsLoaded && ctlType == "MultiValueValidValuesControl")
                    {
                        var reportInfo = e.Control.GetType().GetProperty("ParameterInfo").GetValue(e.Control, null);
                        var validValues = reportInfo.GetType().GetProperty("ValidValues").GetValue(reportInfo, null);
                        int count = Convert.ToInt32(validValues.GetType().GetProperty("Count").GetValue(validValues, null).ToString());
                        string paramName = reportInfo.GetType().GetProperty("Name").GetValue(reportInfo, null).ToString();
                        if (count > 0 && mNotLoadedParameters.Exists(x => x == paramName) && objReportItem.Reportparameters.ToList<ReportParameter>().Exists(x => x.Name == paramName))
                        {
                            ReportParameter par = objReportItem.Reportparameters.ToList<ReportParameter>().Find(x => x.Name == paramName);
                            this.CtlReportViewer.ServerReport.SetParameters(par);
                            mNotLoadedParameters.Remove(paramName);
                            this.CtlReportViewer.RefreshReport();
                        }
                    }
                }
            }
            catch (Exception e1)
            {
            }
        }

        private void SetComboBoxDropDownHeight()
        {
            Control promtPanel = (Control)CtlReportViewer.Controls.Find("promptPanel", true)[0];
            foreach (Control reportParam in promtPanel.Controls)
            {
                if (reportParam.GetType().Name == "ValidValuesControl")
                {
                    foreach (Control reportParamCombo in reportParam.Controls)
                    {
                        if (reportParamCombo.GetType().Name == "AutoWidthComboBox")
                        {
                            ComboBox obj = (ComboBox)reportParamCombo;
                            obj.DropDownHeight = 500;
                            obj.IntegralHeight = true;
                        }
                    }
                }
            }
        }

        private void frmReportBuilder_ControlAdded(object sender, ControlEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void frmReportBuilder_Activated(object sender, EventArgs e)
        {

            if (formCustomSize != System.Drawing.Size.Empty)
                this.Size = formCustomSize;
            mIsLoaded = true;
        }




    }
}