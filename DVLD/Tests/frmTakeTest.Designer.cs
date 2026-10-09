namespace DVLD_WithoutUC.Tests
{
    partial class frmTakeTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUserMEssage = new System.Windows.Forms.Label();
            this.rbPass = new System.Windows.Forms.RadioButton();
            this.rbFail = new System.Windows.Forms.RadioButton();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.rbF = new System.Windows.Forms.RadioButton();
            this.ctrlSchedualedTest1 = new DVLD_WithoutUC.Tests.Controls.ctrlSchedualedTest();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 547);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Result:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 594);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Notes";
            // 
            // lblUserMEssage
            // 
            this.lblUserMEssage.AutoSize = true;
            this.lblUserMEssage.BackColor = System.Drawing.SystemColors.Control;
            this.lblUserMEssage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserMEssage.ForeColor = System.Drawing.Color.Red;
            this.lblUserMEssage.Location = new System.Drawing.Point(354, 551);
            this.lblUserMEssage.Name = "lblUserMEssage";
            this.lblUserMEssage.Size = new System.Drawing.Size(280, 20);
            this.lblUserMEssage.TabIndex = 3;
            this.lblUserMEssage.Text = "You Can Not Change The Result!!";
            this.lblUserMEssage.Visible = false;
            // 
            // rbPass
            // 
            this.rbPass.AutoSize = true;
            this.rbPass.Location = new System.Drawing.Point(120, 547);
            this.rbPass.Name = "rbPass";
            this.rbPass.Size = new System.Drawing.Size(69, 24);
            this.rbPass.TabIndex = 4;
            this.rbPass.TabStop = true;
            this.rbPass.Text = "Pass";
            this.rbPass.UseVisualStyleBackColor = true;
            // 
            // rbFail
            // 
            this.rbFail.AutoSize = true;
            this.rbFail.Location = new System.Drawing.Point(195, 548);
            this.rbFail.Name = "rbFail";
            this.rbFail.Size = new System.Drawing.Size(59, 24);
            this.rbFail.TabIndex = 5;
            this.rbFail.TabStop = true;
            this.rbFail.Text = "Fail";
            this.rbFail.UseVisualStyleBackColor = true;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(120, 594);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(514, 97);
            this.txtNotes.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(476, 697);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(376, 697);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 29);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // rbF
            // 
            this.rbF.AutoSize = true;
            this.rbF.Location = new System.Drawing.Point(195, 548);
            this.rbF.Name = "rbF";
            this.rbF.Size = new System.Drawing.Size(59, 24);
            this.rbF.TabIndex = 5;
            this.rbF.TabStop = true;
            this.rbF.Text = "Fail";
            this.rbF.UseVisualStyleBackColor = true;
            // 
            // ctrlSchedualedTest1
            // 
            this.ctrlSchedualedTest1.Location = new System.Drawing.Point(12, 12);
            this.ctrlSchedualedTest1.Name = "ctrlSchedualedTest1";
            this.ctrlSchedualedTest1.Size = new System.Drawing.Size(622, 523);
            this.ctrlSchedualedTest1.TabIndex = 0;
            // 
            // frmTakeTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(682, 748);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.rbFail);
            this.Controls.Add(this.rbPass);
            this.Controls.Add(this.lblUserMEssage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlSchedualedTest1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTakeTest";
            this.Text = "Take Test";
            this.Load += new System.EventHandler(this.frmTakeTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlSchedualedTest ctrlSchedualedTest1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUserMEssage;
        private System.Windows.Forms.RadioButton rbPass;
        private System.Windows.Forms.RadioButton rbFail;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rbF;
    }
}