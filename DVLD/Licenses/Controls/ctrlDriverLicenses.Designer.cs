namespace DVLD_WithoutUC.Licenses.Controls
{
    partial class ctrlDriverLicenses
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
            this.components = new System.ComponentModel.Container();
            this.gbLicensesHistory = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbLocal = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLocalRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocal = new System.Windows.Forms.DataGridView();
            this.cmsLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLocalLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbInterNational = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInternationalRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvInterNational = new System.Windows.Forms.DataGridView();
            this.cmsInternaltional = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInternationalLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbLicensesHistory.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).BeginInit();
            this.cmsLocal.SuspendLayout();
            this.tbInterNational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterNational)).BeginInit();
            this.cmsInternaltional.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLicensesHistory
            // 
            this.gbLicensesHistory.Controls.Add(this.tabControl1);
            this.gbLicensesHistory.Location = new System.Drawing.Point(3, 3);
            this.gbLicensesHistory.Name = "gbLicensesHistory";
            this.gbLicensesHistory.Size = new System.Drawing.Size(1435, 326);
            this.gbLicensesHistory.TabIndex = 0;
            this.gbLicensesHistory.TabStop = false;
            this.gbLicensesHistory.Text = "Driver Licenses";
            this.gbLicensesHistory.Enter += new System.EventHandler(this.gbLicensesHistory_Enter);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbLocal);
            this.tabControl1.Controls.Add(this.tbInterNational);
            this.tabControl1.Location = new System.Drawing.Point(6, 36);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1438, 299);
            this.tabControl1.TabIndex = 0;
            // 
            // tbLocal
            // 
            this.tbLocal.Controls.Add(this.label1);
            this.tbLocal.Controls.Add(this.lblLocalRecords);
            this.tbLocal.Controls.Add(this.label2);
            this.tbLocal.Controls.Add(this.dgvLocal);
            this.tbLocal.Location = new System.Drawing.Point(4, 29);
            this.tbLocal.Name = "tbLocal";
            this.tbLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tbLocal.Size = new System.Drawing.Size(1430, 266);
            this.tbLocal.TabIndex = 0;
            this.tbLocal.Text = "Local";
            this.tbLocal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "# Record(S)";
            // 
            // lblLocalRecords
            // 
            this.lblLocalRecords.AutoSize = true;
            this.lblLocalRecords.Location = new System.Drawing.Point(134, 227);
            this.lblLocalRecords.Name = "lblLocalRecords";
            this.lblLocalRecords.Size = new System.Drawing.Size(45, 20);
            this.lblLocalRecords.TabIndex = 3;
            this.lblLocalRecords.Text = "????";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Local Licenses History";
            // 
            // dgvLocal
            // 
            this.dgvLocal.AllowUserToAddRows = false;
            this.dgvLocal.AllowUserToDeleteRows = false;
            this.dgvLocal.AllowUserToOrderColumns = true;
            this.dgvLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocal.ContextMenuStrip = this.cmsLocal;
            this.dgvLocal.Location = new System.Drawing.Point(6, 59);
            this.dgvLocal.Name = "dgvLocal";
            this.dgvLocal.ReadOnly = true;
            this.dgvLocal.RowHeadersWidth = 62;
            this.dgvLocal.RowTemplate.Height = 28;
            this.dgvLocal.Size = new System.Drawing.Size(1413, 153);
            this.dgvLocal.TabIndex = 0;
            // 
            // cmsLocal
            // 
            this.cmsLocal.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLocalLicenseInfoToolStripMenuItem});
            this.cmsLocal.Name = "cmsLocal";
            this.cmsLocal.Size = new System.Drawing.Size(272, 36);
            // 
            // showLocalLicenseInfoToolStripMenuItem
            // 
            this.showLocalLicenseInfoToolStripMenuItem.Name = "showLocalLicenseInfoToolStripMenuItem";
            this.showLocalLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(271, 32);
            this.showLocalLicenseInfoToolStripMenuItem.Text = "Show Local License Info";
            this.showLocalLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.showLocalLicenseInfoToolStripMenuItem_Click);
            // 
            // tbInterNational
            // 
            this.tbInterNational.Controls.Add(this.label4);
            this.tbInterNational.Controls.Add(this.lblInternationalRecords);
            this.tbInterNational.Controls.Add(this.label3);
            this.tbInterNational.Controls.Add(this.dgvInterNational);
            this.tbInterNational.Location = new System.Drawing.Point(4, 29);
            this.tbInterNational.Name = "tbInterNational";
            this.tbInterNational.Padding = new System.Windows.Forms.Padding(3);
            this.tbInterNational.Size = new System.Drawing.Size(1430, 266);
            this.tbInterNational.TabIndex = 1;
            this.tbInterNational.Text = "International";
            this.tbInterNational.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "# Record(S)";
            // 
            // lblInternationalRecords
            // 
            this.lblInternationalRecords.AutoSize = true;
            this.lblInternationalRecords.Location = new System.Drawing.Point(142, 226);
            this.lblInternationalRecords.Name = "lblInternationalRecords";
            this.lblInternationalRecords.Size = new System.Drawing.Size(45, 20);
            this.lblInternationalRecords.TabIndex = 5;
            this.lblInternationalRecords.Text = "????";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "International Licenses History";
            // 
            // dgvInterNational
            // 
            this.dgvInterNational.AllowUserToAddRows = false;
            this.dgvInterNational.AllowUserToDeleteRows = false;
            this.dgvInterNational.AllowUserToOrderColumns = true;
            this.dgvInterNational.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInterNational.ContextMenuStrip = this.cmsInternaltional;
            this.dgvInterNational.Location = new System.Drawing.Point(6, 60);
            this.dgvInterNational.Name = "dgvInterNational";
            this.dgvInterNational.ReadOnly = true;
            this.dgvInterNational.RowHeadersWidth = 62;
            this.dgvInterNational.RowTemplate.Height = 28;
            this.dgvInterNational.Size = new System.Drawing.Size(1413, 152);
            this.dgvInterNational.TabIndex = 2;
            // 
            // cmsInternaltional
            // 
            this.cmsInternaltional.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsInternaltional.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInternationalLicenseInfoToolStripMenuItem});
            this.cmsInternaltional.Name = "cmsInternaltional";
            this.cmsInternaltional.Size = new System.Drawing.Size(331, 36);
            // 
            // showInternationalLicenseInfoToolStripMenuItem
            // 
            this.showInternationalLicenseInfoToolStripMenuItem.Name = "showInternationalLicenseInfoToolStripMenuItem";
            this.showInternationalLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(330, 32);
            this.showInternationalLicenseInfoToolStripMenuItem.Text = "Show International License Info";
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbLicensesHistory);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(1450, 341);
            this.gbLicensesHistory.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbLocal.ResumeLayout(false);
            this.tbLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).EndInit();
            this.cmsLocal.ResumeLayout(false);
            this.tbInterNational.ResumeLayout(false);
            this.tbInterNational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterNational)).EndInit();
            this.cmsInternaltional.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLicensesHistory;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbLocal;
        private System.Windows.Forms.TabPage tbInterNational;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvInterNational;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLocalRecords;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInternationalRecords;
        private System.Windows.Forms.ContextMenuStrip cmsLocal;
        private System.Windows.Forms.ToolStripMenuItem showLocalLicenseInfoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsInternaltional;
        private System.Windows.Forms.ToolStripMenuItem showInternationalLicenseInfoToolStripMenuItem;
    }
}
