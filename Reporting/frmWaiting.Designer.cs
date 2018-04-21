namespace Reporting
{
    partial class frmWaiting
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.pictWaiting = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictWaiting)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(66, 13);
            this.lblInfo.MaximumSize = new System.Drawing.Size(400, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(82, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Информация...";
            // 
            // pictWaiting
            // 
            this.pictWaiting.Image = global::Reporting.Properties.Resources._89;
            this.pictWaiting.Location = new System.Drawing.Point(5, 5);
            this.pictWaiting.Name = "pictWaiting";
            this.pictWaiting.Size = new System.Drawing.Size(50, 50);
            this.pictWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictWaiting.TabIndex = 0;
            this.pictWaiting.TabStop = false;
            // 
            // frmWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(476, 60);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.pictWaiting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWaiting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать/экспорт документов...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWaiting_FormClosing);
            this.Load += new System.EventHandler(this.frmWaiting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictWaiting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.PictureBox pictWaiting;
        internal System.Windows.Forms.Label lblInfo;

        #endregion
    }
}