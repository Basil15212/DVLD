using DVLDBussnessLayer;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DVLD_WithoutUC.Users
{
    public partial class frmAddUpdateUSer : Form
    {


        enum enMode { AddNew =0 , Update =1}
        enMode _Mode = enMode.AddNew;

        private clsUser _User;
        private int _UserID = -1;
        private int _PersonID = -1;


        public frmAddUpdateUSer()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            ctrlPersonCardWithFilter1.OnPersonSelected += PerosnSelected;
        }
        public frmAddUpdateUSer(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            ctrlPersonCardWithFilter1.OnPersonSelected += PerosnSelected;
            _Mode = enMode.Update;
        }

        
        public void resetForEdit()
        {
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            lblUserID.Text = _User.UserID.ToString();
            btnNext.Visible = false;
            ctrlPersonCardWithFilter1.FilterEnabled = false;
        }
        private void frmAddUpdateUSer_Load(object sender, EventArgs e)
        {
            
            if (_Mode == enMode.AddNew)
            {
                tabControl1.TabPages[1].Enabled = false;
                btnNext.Enabled = false;
                _User = new clsUser();
            }
            else
            {
                lblAddUserTitle.Text = "Edit User Info";
                _User = clsUser.FindByUserID(_UserID);
                ctrlPersonCardWithFilter1.LoudPersonInfo(_User.PersonID);
                resetForEdit();
            }
        }

        public void UserNameValidating()
        {
            if(string.IsNullOrEmpty(txtUserName.Text))
                errorProvider1.SetError(txtUserName, "Can't Be Empty");
            else
                errorProvider1.SetError(txtUserName, "");

            //Checck if  user name is already used or not 

            //if (clsUser.(txtUserName.Text ,_UserID))
            //    errorProvider1.SetError(txtUserName, "This User Name is already Used");
            //else
            //    errorProvider1.SetError(txtUserName, "");

        }

        public void MatchingPassword()
        {
            if(string.IsNullOrEmpty(txtPassword.Text))
                errorProvider1.SetError(txtPassword, "Can't Be Empty");
            else
                errorProvider1.SetError(txtPassword, "");
            if(string.IsNullOrEmpty(txtConfirmPassword.Text))
                errorProvider1.SetError(txtConfirmPassword, "Can't Be Empty");
            else
                errorProvider1.SetError(txtConfirmPassword, "");
            if(txtPassword.Text != txtConfirmPassword.Text)
                errorProvider1.SetError(txtConfirmPassword, "Pass word is not match");
            else
                errorProvider1.SetError(txtConfirmPassword, "");
        }

        

        private void btnNext_Click(object sender, EventArgs e)
        {
           
            if(clsUser.isUserExistForPersonID(_PersonID))
            {
                MessageBox.Show("This Person IS Already a User" ,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tabControl1.TabPages[1].Enabled = true;
                tabControl1.SelectedIndex = 1;

            }

        }
        private void PerosnSelected(int PersonID)
        {
            _PersonID = PersonID;
            btnNext.Enabled = (_PersonID != -1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds are not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _User.PersonInfo = clsPerson.Find(_PersonID);
           
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;
            _User.PersonID = _PersonID;
            if (_User.Save())
            {
                MessageBox.Show("User Saved Successfuly", "Saved", MessageBoxButtons.OK);
                _Mode = enMode.Update;
                lblUserID.Text = _User.UserID.ToString();
                lblAddUserTitle.Text = "Edit User Info";
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                btnNext.Visible = false;

            }
            else
            {
                MessageBox.Show("Somthing Went Wrong Didn't Save Successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void Password_Validating(object sender, CancelEventArgs e)
        {
            MatchingPassword();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            UserNameValidating();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
