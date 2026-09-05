using DVLD_Buessness.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Users
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            
        }

        private void _ResetLogin()
        {
            txtPassword.Text = "";
            txtUserName.Text = "";
            txtUserName.Focus();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds Are Required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsUser User = clsUser.Find(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (User != null)
            {

                if (ckbRememberMe.Checked)
                {
                    clsGlobal.RememberMeSave(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                    clsGlobal.RememberMeSave("", "");


                if (User.IsActive == true)
                {

                    clsGlobal.CurrentUser = User;
                    frmMainScreen frm = new frmMainScreen(this);
                    frm.FormClosed += frmMainScreen_FormClosed;
                    this.Hide();
                    frm.ShowDialog();


                }
                else
                {
                    MessageBox.Show("This User is not Active Please Contact Your Admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _ResetLogin();
                }
            }
            else
            { 
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                _ResetLogin();
            }


        }

        private void frmMainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsGlobal.CurrentUser = null;
           
            this.Show();
            _LoudLoginInfo();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            _LoudLoginInfo();
        }

        private void _LoudLoginInfo()
        {
            string UserName = "", Password = "";
            _ResetLogin();
            if (clsGlobal._RememberMeGetBack(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                ckbRememberMe.Checked = true;
            }
            else
                ckbRememberMe.Checked = false;
        }
        private void txtLogin_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
                errorProvider1.SetError(txtUserName, "Cant Be Empty");
            else
                errorProvider1.SetError(txtUserName, "");

            if (string.IsNullOrEmpty(txtPassword.Text))
                errorProvider1.SetError(txtPassword, "Cant Be Empty");
            else
            {
                errorProvider1.SetError(txtPassword, "");

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
