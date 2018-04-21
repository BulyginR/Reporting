namespace Reporting
{
    partial class frmShowMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowMessage));
            this.PanelOkCancel = new System.Windows.Forms.Panel();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.PanelOkCancel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelOkCancel
            // 
            this.PanelOkCancel.Controls.Add(this.btnNo);
            this.PanelOkCancel.Controls.Add(this.btnYes);
            this.PanelOkCancel.Controls.Add(this.btnCancel);
            this.PanelOkCancel.Controls.Add(this.btnOk);
            this.PanelOkCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelOkCancel.Location = new System.Drawing.Point(0, 96);
            this.PanelOkCancel.Name = "PanelOkCancel";
            this.PanelOkCancel.Size = new System.Drawing.Size(394, 36);
            this.PanelOkCancel.TabIndex = 5;
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(166, 5);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(107, 25);
            this.btnNo.TabIndex = 8;
            this.btnNo.Text = "Нет";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(53, 5);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(107, 25);
            this.btnYes.TabIndex = 7;
            this.btnYes.Text = "Да";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(279, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(166, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(107, 25);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(3, 3);
            this.lblMessage.MaximumSize = new System.Drawing.Size(394, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(39, 13);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = "Label1";
            // 
            // frmShowMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 132);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.PanelOkCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "frmShowMessage";
            this.Text = "Выбор действия";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShowMessage_FormClosing);
            this.Load += new System.EventHandler(this.frmShowMessage_Load);
            this.PanelOkCancel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        internal System.Windows.Forms.Panel PanelOkCancel;
        internal System.Windows.Forms.Button btnNo;
        internal System.Windows.Forms.Button btnYes;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.Label lblMessage;
    }
}