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

namespace DVLD_DataAccess.Text_Types
{
    public partial class frmEditTestType : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TesType;
        public frmEditTestType(clsTestType.enTestType ID)
        {
            InitializeComponent();
            _TestTypeID = ID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TesType = clsTestType.Find(_TestTypeID);
            if(_TesType ==  null)
            {
                MessageBox.Show("InValid Test Type ID " ,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblID.Text =_TesType.ID.ToString();
            txtTestTypeTitle.Text = _TesType.Title;
            txtDescription.Text = _TesType.Description;
            txtFees.Text =_TesType.Fees.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            _TesType.Title = txtTestTypeTitle.Text.Trim();
            _TesType.Fees =Convert.ToSingle(txtFees.Text);
            _TesType.Description = txtDescription.Text.Trim();
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
