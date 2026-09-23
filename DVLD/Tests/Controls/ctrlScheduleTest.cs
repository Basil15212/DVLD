using DVLDBussnessLayer;
using DVLDBussnessLayer.Applocations.LocalDrivingLicenseApp;
using DVLDBussnessLayer.Test_For_License.TestAppointments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {

       public enum enMode { AddNew =0 ,Update =1}
        enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule =0 , RetakeTestSchedule =1 }
        enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public clsTestType.enTestType TestTypeID
        {
            get {  return _TestTypeID; }
            set
            {
                _TestTypeID = value;
                switch(TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            lblTitle.Text = "Vision Test";
                            break;
                        }
                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            lblTitle.Text = "Written Test";
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            lblTitle.Text = "Street Test";
                            break;
                        }
                }
            }
        }

        public void  LoadInfo(int LocalDrivingLicenseApplicationID ,int AppointmentID =-1)
        {
            //the Mood ---- AddNew/Update
            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID=AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if(_LocalDrivingLicenseApplication==null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            // The creation Mode
            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode= enCreationMode.FirstTimeSchedule;

            if(_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRestakeAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled= false;
                lblTitle.Text = "Schedule Test";
                lblRestakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }

            lblAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrialTimes.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
                
            if(_Mode ==enMode.AddNew)
            {
                lblTestFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                dtpDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                //will be finished later  line 137
            }

            
        }


        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        

        
    }
}
