using DVLD_Buessness.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Users
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _User = clsUser.Find(UserID);
        }
        private void changePasswordValidation()
        {
            if(txtCurrentPassword.Text != _User.Password)
                errorProvider1.SetError(txtCurrentPassword, "Wrong Password");
            else if(string.IsNullOrEmpty(txtCurrentPassword.Text))
                errorProvider1.SetError(txtCurrentPassword, "Can't Be Blank");
            else
                errorProvider1.SetError(txtCurrentPassword, "");

            

            if (string.IsNullOrEmpty(txtNewPassword.Text))
                errorProvider1.SetError(txtNewPassword, "Can't Be Blank");
            else
                errorProvider1.SetError(txtNewPassword, "");

            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
                errorProvider1.SetError(txtConfirmPassword, "Can't Be Blank");
            else if (txtConfirmPassword.Text != txtNewPassword.Text)
                errorProvider1.SetError(txtConfirmPassword, "Not Match!!!");
            else
                errorProvider1.SetError(txtConfirmPassword, "");
            

        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.LoudUserInfo(_UserID);
            txtCurrentPassword.Focus();

        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            changePasswordValidation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not Valid" ,"Error" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _User.Password =txtConfirmPassword.Text;
            if(_User.Save())
            {
                MessageBox.Show("Password Changed Successfuly" ,"Success" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                gbChangePassword.Enabled = false;
            }
            else
            {
                MessageBox.Show("Somthing Went Wrong ","Error" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCLose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
