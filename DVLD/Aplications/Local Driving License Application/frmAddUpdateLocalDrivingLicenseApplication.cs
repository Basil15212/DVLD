using DVLDBussnessLayer;
using DVLDBussnessLayer.Applocations.LocalDrivingLicenseApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Aplications.Local_Driving_License_Application
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
       public enum enMode { AddNew =0 , Update = 1 }
        public enMode Mode = enMode.AddNew;

        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;


        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }
        public frmAddUpdateLocalDrivingLicenseApplication( int  LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
        }

        //will be done later after Making the LicenseClass Part


        //private void _FillLicenseClassesInComoboBox()
        //{
        //    DataTable dtLicenseClasses = clsLicenseClass.GetAllLicenseClasses();

        //    foreach (DataRow row in dtLicenseClasses.Rows)
        //    {
        //        cbLicenseClass.Items.Add(row["ClassName"]);
        //    }
        //}


        private void _ResetValues()
        {
            //_FillLicenseClassesInComoboBox();

            if(Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Appliction";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication =new clsLocalDrivingLicenseApplication();
                ctrlPersonCardWithFilter1.FilterFocus();
                btnNext.Enabled = (tbAppInfo.Enabled);
                btnSave.Enabled = (btnNext.Enabled);
                tbAppInfo.Enabled = false;

                lblFees.Text = clsApplicationType.Find(
                    (int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblDate.Text = DateTime.Now.ToShortDateString();
                lblUserName.Text = clsGlobal.CurrentUser.UserName;
            }else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                tbAppInfo.Enabled = true;
                btnSave .Enabled = (btnNext.Enabled);
                btnNext.Enabled = tbAppInfo.Enabled;
            }
        }


        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
                (_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoudPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            //cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblFees.Text =_LocalDrivingLicenseApplication.PaidFees.ToString();
            lblUserName.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).UserName;
        }

        private void DataBackEvent(object sender,int PersonID)
        {
            _SelectedPersonID = PersonID;
            ctrlPersonCardWithFilter1.LoudPersonInfo(PersonID);
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetValues();
            if(Mode == enMode.Update)
            {
                _LoadData();
            }


        }
    }
}
