namespace DVLD_WithoutUC.Licenses.Internatioal_Licenses
{
    partial class frmShowInternationalLicenseInfo
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
            this.ctrlInterNationalLicenseInfo1 = new DVLD_WithoutUC.Licenses.Internatioal_Licenses.Controls.ctrlInterNationalLicenseInfo();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCLose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlInterNationalLicenseInfo1
            // 
            this.ctrlInterNationalLicenseInfo1.Location = new System.Drawing.Point(12, 58);
            this.ctrlInterNationalLicenseInfo1.Name = "ctrlInterNationalLicenseInfo1";
            this.ctrlInterNationalLicenseInfo1.Size = new System.Drawing.Size(917, 264);
            this.ctrlInterNationalLicenseInfo1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(945, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Internationl License Info";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCLose
            // 
            this.btnCLose.Image = global::DVLD_WithoutUC.Properties.Resources.cross_32;
            this.btnCLose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCLose.Location = new System.Drawing.Point(764, 328);
            this.btnCLose.Name = "btnCLose";
            this.btnCLose.Size = new System.Drawing.Size(131, 35);
            this.btnCLose.TabIndex = 2;
            this.btnCLose.Text = "Close";
            this.btnCLose.UseVisualStyleBackColor = true;
            this.btnCLose.Click += new System.EventHandler(this.btnCLose_Click);
            // 
            // frmShowInternationalLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 369);
            this.Controls.Add(this.btnCLose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlInterNationalLicenseInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowInternationalLicenseInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show International License Info";
            this.Load += new System.EventHandler(this.frmShowInternationalLicenseInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlInterNationalLicenseInfo ctrlInterNationalLicenseInfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCLose;
    }
}