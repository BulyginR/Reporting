namespace Reporting
{
    partial class frmReportsPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportsPreview));
            this.panReports = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // panReports
            // 
            this.panReports.Controls.Add(this.label1);
            this.panReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panReports.Location = new System.Drawing.Point(0, 0);
            this.panReports.Name = "panReports";
            this.panReports.Size = new System.Drawing.Size(778, 435);
            this.panReports.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(-2, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 7);
            this.label1.TabIndex = 1;
            this.label1.Text = "[c#]";
            // 
            // frmReportsPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 435);
            this.Controls.Add(this.panReports);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReportsPreview";
            this.Text = "Предпросмотр отчетов";
            this.Resize += new System.EventHandler(this.frmReportsPreview_Resize);
            this.panReports.ResumeLayout(false);
            this.panReports.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panReports;
        private System.Windows.Forms.Label label1;
    }
}