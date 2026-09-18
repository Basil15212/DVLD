using DVLDBussnessLayer.Applocations.LocalDrivingLicenseApp;
using DVLDBussnessLayer.License_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Aplications.Local_Driving_License_Application.Controls
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicensApplicationID = -1;
        private int _LicenseID;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicensApplicationID; }
        }
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingAppID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingAppID);
            if(_LocalDrivingLicenseApplication ==null)
            {
                _ResetLocalDrivingApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + LocalDrivingAppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }


        public void LoadAppInfoByAppID(int AppID)
        {
            _LocalDrivingLicenseApplication =clsLocalDrivingLicenseApplication.FindByApplicationID(AppID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingApplicationInfo();

                MessageBox.Show("No Application with ApplicationID = " + AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = 1000; //will be edited Later

            llShowLicenseInfo.Enabled = false; //will be Edited Later

            lblDLAppID.Text =_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedForLicense.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = "3"; //Will Be Edited Later
            clsApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

        }

        private void _ResetLocalDrivingApplicationInfo()
        {
            _LocalDrivingLicensApplicationID =- 1;
            clsApplicationBasicInfo1.LoadApplicationInfo(-1);
            lblDLAppID.Text = "[???]";
            lblAppliedForLicense.Text = "[???]";
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("will Be Finished soon " ,"information" , MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
    }
}
