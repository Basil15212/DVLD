namespace DVLD_WithoutUC.Aplications.Local_Driving_License_Application.Controls
{
    partial class ctrlDrivingLicenseApplicationInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.lblDLAppID = new System.Windows.Forms.Label();
            this.lblAppliedForLicense = new System.Windows.Forms.Label();
            this.lblPassedTests = new System.Windows.Forms.Label();
            this.clsApplicationBasicInfo1 = new DVLD_WithoutUC.Aplications.Controls.clsApplicationBasicInfo();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAppliedForLicense);
            this.groupBox1.Controls.Add(this.lblPassedTests);
            this.groupBox1.Controls.Add(this.lblDLAppID);
            this.groupBox1.Controls.Add(this.llShowLicenseInfo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(734, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driving License Application Info";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "D.L.App ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Applied For License";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(384, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Passed Tests";
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Location = new System.Drawing.Point(144, 66);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(140, 20);
            this.llShowLicenseInfo.TabIndex = 4;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show License Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseInfo_LinkClicked);
            // 
            // lblDLAppID
            // 
            this.lblDLAppID.AutoSize = true;
            this.lblDLAppID.Location = new System.Drawing.Point(144, 35);
            this.lblDLAppID.Name = "lblDLAppID";
            this.lblDLAppID.Size = new System.Drawing.Size(45, 20);
            this.lblDLAppID.TabIndex = 5;
            this.lblDLAppID.Text = "????";
            // 
            // lblAppliedForLicense
            // 
            this.lblAppliedForLicense.AutoSize = true;
            this.lblAppliedForLicense.Location = new System.Drawing.Point(504, 35);
            this.lblAppliedForLicense.Name = "lblAppliedForLicense";
            this.lblAppliedForLicense.Size = new System.Drawing.Size(36, 20);
            this.lblAppliedForLicense.TabIndex = 7;
            this.lblAppliedForLicense.Text = "???";
            // 
            // lblPassedTests
            // 
            this.lblPassedTests.AutoSize = true;
            this.lblPassedTests.Location = new System.Drawing.Point(513, 66);
            this.lblPassedTests.Name = "lblPassedTests";
            this.lblPassedTests.Size = new System.Drawing.Size(36, 20);
            this.lblPassedTests.TabIndex = 6;
            this.lblPassedTests.Text = "???";
            // 
            // clsApplicationBasicInfo1
            // 
            this.clsApplicationBasicInfo1.Location = new System.Drawing.Point(3, 132);
            this.clsApplicationBasicInfo1.Name = "clsApplicationBasicInfo1";
            this.clsApplicationBasicInfo1.Size = new System.Drawing.Size(742, 228);
            this.clsApplicationBasicInfo1.TabIndex = 1;
            // 
            // ctrlDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clsApplicationBasicInfo1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlDrivingLicenseApplicationInfo";
            this.Size = new System.Drawing.Size(746, 364);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAppliedForLicense;
        private System.Windows.Forms.Label lblPassedTests;
        private System.Windows.Forms.Label lblDLAppID;
        private Aplications.Controls.clsApplicationBasicInfo clsApplicationBasicInfo1;
    }
}
