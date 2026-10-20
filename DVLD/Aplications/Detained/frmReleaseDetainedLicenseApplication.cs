using DVLD_WithoutUC.Licenses;
using DVLD_WithoutUC.Licenses.LocalDrivingLicense;
using DVLD_WithoutUC.People;
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

namespace DVLD_WithoutUC.Aplications.Detained
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _LicenseID = -1;

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
            ctrlDrivingLicenseInfoWithFilter1.LoadLicensInfo(_LicenseID);
            ctrlDrivingLicenseInfoWithFilter1.FilterEnabled = false;

        }
        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            if (ctrlDrivingLicenseInfoWithFilter1.FilterEnabled)
                ctrlDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _LicenseID = obj;
            lblLicenseID .Text = _LicenseID.ToString();
            //lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            llShowLiceseHistory.Enabled = (_LicenseID != -1);
            if (_LicenseID == -1)
                return;
            if(!ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License  is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            lblCreatedBy.Text = ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.CreatedByUserInfo.UserName;

            lblDetainID.Text = ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.DetainDate);
            lblFineFees.Text = ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text.Trim()) + Convert.ToSingle(lblAppFees.Text)).ToString();
            btnRelease.Enabled = true;
        }

        

        private void llShowLiceseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicensesHistory frm =
                new frmShowPersonLicensesHistory(ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShoeDriverLicenseInfo frm = new frmShoeDriverLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;
            bool IsReleased = ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID,ref ApplicationID);
            lblAppID.Text = ApplicationID.ToString();
            if(!IsReleased)
            {
                MessageBox.Show("Faild  to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }
    }
}
