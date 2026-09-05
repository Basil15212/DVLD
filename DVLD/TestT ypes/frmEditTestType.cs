using DVLD_Buessness.Test_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_DataAccess.Text_Types
{
    public partial class frmEditTestType : Form
    {
        private int _TestTypeID;
        private clsTestTypes _TesType;
        public frmEditTestType(int ID)
        {
            InitializeComponent();
            _TestTypeID = ID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TesType = clsTestTypes.Find(_TestTypeID);
            if(_TesType ==  null)
            {
                MessageBox.Show("InValid Test Type ID " ,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblID.Text =_TesType.TestTypeID.ToString();
            txtTestTypeTitle.Text = _TesType.TestTypeTitle;
            txtDescription.Text = _TesType.TestTypeDescription;
            txtFees.Text =_TesType.TestTypeFees.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            _TesType.TestTypeTitle = txtTestTypeTitle.Text.Trim();
            _TesType.TestTypeFees =Convert.ToDecimal(txtFees.Text);
            _TesType.TestTypeDescription = txtDescription.Text.Trim();
            if(_TesType.Save())
            {
                MessageBox.Show("Saved Successfuly","Saved" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Saving Faild", "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void txtBox_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtDescription.Text))
                errorProvider1.SetError(txtDescription, "Cant Be Empty");
            else
                errorProvider1.SetError(txtDescription, "");

            if(string.IsNullOrEmpty(txtTestTypeTitle.Text))
                errorProvider1.SetError(txtTestTypeTitle, "Cant Be Empty");
            else
                errorProvider1.SetError(txtTestTypeTitle, "");

            if(string.IsNullOrEmpty(txtFees.Text))
                errorProvider1.SetError(txtFees, "Cant Be Empty");
            else
                errorProvider1.SetError(txtFees, "");
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
