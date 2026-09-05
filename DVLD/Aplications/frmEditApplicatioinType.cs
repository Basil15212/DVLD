using DVLD_Buessness.Aplication_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Aplications
{
    public partial class frmEditApplicatioinType : Form
    {
        int _CurrentAppID;
        clsAplicationTypes _App;

        public frmEditApplicatioinType()
        {
            InitializeComponent();
            _ResetForm();
        }
        public frmEditApplicatioinType(int ApplicationID)
        {
            InitializeComponent();
            _CurrentAppID = ApplicationID;
        }

        private void _ResetForm()
        {
            lblTypeID.Text = "???";
            txtTitleType.Text = "";
            txtTypeFees.Text = "";
            txtTitleType.Focus();
        }
        private void frmEditApplicatioinType_Load(object sender, EventArgs e)
        {
            _App = clsAplicationTypes.Find(_CurrentAppID);
            if(_App != null )
            {
                lblTypeID.Text = _CurrentAppID.ToString();
                txtTitleType.Text = _App.AplicationTitle;
                txtTypeFees.Text =_App.AplicationFees.ToString();
            }
            
        }

        private void txtTypeFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are Required","Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _App.AplicationFees =Convert.ToDecimal(txtTypeFees.Text);
            _App.AplicationTitle = txtTitleType.Text.Trim();
            if(_App.Save())
            {
                MessageBox.Show("Saved Successfuly", "Saved" , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Saving Faild" ,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitleType_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitleType.Text))
            {
                errorProvider1.SetError(txtTitleType, "Cant Be Empty");
            }
            else
                errorProvider1.SetError(txtTitleType, "");
        }
    }
}
