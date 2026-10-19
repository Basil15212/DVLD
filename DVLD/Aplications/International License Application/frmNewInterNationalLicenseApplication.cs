using DVLD_WithoutUC.Licenses;
using DVLD_WithoutUC.Licenses.Internatioal_Licenses;
using DVLDBussnessLayer;
using DVLDBussnessLayer.Applocations.International_Licesne;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Aplications.International_License_Application
{
    public partial class frmNewInterNationalLicenseApplication : Form
    {
        private int _InternationalLicenseID;

        public frmNewInterNationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewInterNationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblAppDate.Text;
            lblExpDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled =(SelectedLicenseID !=-1);
            if (SelectedLicenseID == -1)
                return;

            if(ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternatioalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            if(ActiveInternatioalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternatioalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = false;
                _InternationalLicenseID = ActiveInternatioalLicenseID;
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
                InternationalLicense.ApplicationDate =DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;
                InternationalLicense.CreatedByUserID =clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblAppID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            ctrlDrivingLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicensesHistory frm =
                new frmShowPersonLicensesHistory(ctrlDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void frmNewInterNationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }
    }
}
