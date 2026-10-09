using DVLDBussnessLayer.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Licenses.LocalDrivingLicense.Controls
{
    public partial class ctrlDrivingLicenseInfoWithFilter : UserControl
    {

        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int licenseId)
        {
            Action<int> Handler = OnLicenseSelected;
            if (Handler != null)
            {
                Handler(LicenseID);
            }
        }
        public ctrlDrivingLicenseInfoWithFilter()
        {
            InitializeComponent();
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;
        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicense; }
        }

        public void LoadLicensInfo(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ctrlDriverLicenseInfo1.LicenseID;
            if(OnLicenseSelected != null && FilterEnabled)
            {
                LicenseSelected(LicenseID);
            }
        }

        private void ctrlDrivingLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }
        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicensInfo(_LicenseID);
        }
    }
}
