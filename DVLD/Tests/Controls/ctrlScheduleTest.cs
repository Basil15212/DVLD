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
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = ((Convert.ToString(lblRestakeAppFees.Text)) + (Convert.ToString(lblTestFees.Text))).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;
            if (!_HandleAppointmentLockedConstraint())
                return;
            if (!_HandlePrviousTestConstraint())
                return;

            
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode ==enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID,_TestTypeID))
            {
                lblMessageForUser.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if(_TestAppointment == null )
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblTestFees.Text = _TestAppointment.PaidFees.ToString();

            //Compare Date
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) > 0)
                dtpDate.MinDate = DateTime.Now;
            else dtpDate.MinDate = _TestAppointment.AppointmentDate;

            dtpDate.Value = _TestAppointment.AppointmentDate;

            if(_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRestakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRestakeAppFees.Text = _TestAppointment.RetakeTestInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();

            }
            return true;

                
        }

        private bool _HandleAppointmentLockedConstraint()
        {

            if(_TestAppointment.IsLocked)
            {
                lblMessageForUser.Visible = true;
                lblMessageForUser.Text = "Person already sat for the test, appointment loacked.";
                dtpDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                lblMessageForUser.Visible = false;
            }
            return true;
           
        }
        private bool _HandlePrviousTestConstraint()
        {
            switch(TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblMessageForUser.Visible = false;
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    if(!clsLocalDrivingLicenseApplication.DoesPassTestType(_LocalDrivingLicenseApplicationID ,clsTestType.enTestType.VisionTest))
                    {
                        lblMessageForUser.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblMessageForUser.Visible = true;
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessageForUser.Visible = false;
                        btnSave.Enabled = true;
                        dtpDate.Enabled = true;
                    }
                    return true;

                case clsTestType.enTestType.StreetTest:
                    if(!clsLocalDrivingLicenseApplication.DoesPassTestType(_LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest))
                    {

                        lblMessageForUser.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblMessageForUser.Visible = true;
                        btnSave.Enabled = false;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblMessageForUser.Visible = false;
                        btnSave.Enabled = true;
                        dtpDate.Enabled = true;
                    }
                    return true;
            }
            return true;
        }

        private bool _HandleRetakeApplication()
        {

            if(_Mode ==enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;


                if(!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }
            return true;
        }
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpDate.Value;
            _TestAppointment.PaidFees =Convert.ToSingle(lblTestFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
