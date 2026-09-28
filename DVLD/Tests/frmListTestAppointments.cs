using DVLDBussnessLayer;
using DVLDBussnessLayer.Applocations.LocalDrivingLicenseApp;
using DVLDBussnessLayer.Test_For_License.TestAppointments;
using DVLDBussnessLayer.Test_For_License.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Tests
{
    public partial class frmListTestAppointments : Form
    {

        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;


        public frmListTestAppointments(int LocalDrivingLicenseApplicationID ,clsTestType.enTestType TestType)

        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            _TestType = TestType;
        }
        private void _LoadTestTypeImageAndTitle()
        {
            switch(_TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointments";
                        this.Text = lblTitle.Text;
                        break;
                    }
                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointments";
                        this.Text = lblTitle.Text;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test Appointments";
                        this.Text = lblTitle.Text;
                        break;
                    }
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);


            _dtLicenseTestAppointments = 
                clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID ,_TestType);

            if( _dtLicenseTestAppointments == null || _dtLicenseTestAppointments.Rows.Count == 0)
            {
                MessageBox.Show("DataTable is Empty");
                return;
            }
            else
            {
                dgvTestAppointments.DataSource = _dtLicenseTestAppointments;
                
            }


            lblRecords.Text =dgvTestAppointments.Rows.Count.ToString();
            if(dgvTestAppointments.Rows.Count >0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns[0].Width = 90;
                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[1].Width = 120;
                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 90;
                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 60;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if(LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //clsTest
            //Line 113
            //ShouldMakeTheTestClass ....

            clsTest LastTest = LocalDrivingLicenseApplication.GetLastTestPerTestType(_TestType);
            if(LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;

            }
            if(LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if no last Test
            frmScheduleTest frm = new frmScheduleTest(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID,_TestType );
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
            return;

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID ,_TestType ,TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
            return;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            frmListTestAppointments_Load(null, null);
        }
    }
}
