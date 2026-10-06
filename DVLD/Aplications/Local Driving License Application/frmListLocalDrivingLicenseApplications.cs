using DVLD_WithoutUC.Licenses.LocalDrivingLicense;
using DVLD_WithoutUC.Tests;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_WithoutUC.Aplications.Local_Driving_License_Application
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        private DataTable dtAllApps;


        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            //issueDrivingLicenseFirstTimeToolStripMenuItem.Tag = "";
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            dtAllApps =clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalLicenseApps.DataSource = dtAllApps;
            if(dgvLocalLicenseApps.Rows.Count > 0 )
            {
                dgvLocalLicenseApps.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalLicenseApps.Columns[0].Width = 80;
                dgvLocalLicenseApps.Columns[1].HeaderText = "Drving Class";
                dgvLocalLicenseApps.Columns[1].Width = 230;
                dgvLocalLicenseApps.Columns[2].HeaderText = "National NO.";
                dgvLocalLicenseApps.Columns[2].Width = 100;
                dgvLocalLicenseApps.Columns[3].HeaderText = "Full Name";
                dgvLocalLicenseApps.Columns[3].Width = 280;
                dgvLocalLicenseApps.Columns[4].HeaderText = "Application Date";
                dgvLocalLicenseApps.Columns[4].Width = 130;
                dgvLocalLicenseApps.Columns[5].HeaderText = "Passed Tests";
                dgvLocalLicenseApps.Columns[5].Width = 100;
            }
            cbFilterBy.SelectedIndex = 0;   
            lblRecords.Text =dgvLocalLicenseApps.Rows.Count.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm =new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible =(cbFilterBy.SelectedIndex != 0);
            if(txtFilterValue.Visible )
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            dtAllApps.DefaultView.RowFilter = "";
            lblRecords.Text = dgvLocalLicenseApps.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "D.L.AppID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterValue = "";
            switch(cbFilterBy.Text)
            {
                case "D.L.AppID":
                    FilterValue = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    FilterValue = "NationalNo";
                    break;
                case "Full Name":
                    FilterValue = "FullName";
                    break;
                case "Status.":
                    FilterValue = "Status";
                    break;
                default:
                    FilterValue = "None";
                    break;
            }
            if(txtFilterValue.Text.Trim() == "" || FilterValue == "None")
            {
                dtAllApps.DefaultView.RowFilter = "";
                lblRecords.Text = dgvLocalLicenseApps.Rows.Count.ToString();
                return;
            }
            if(FilterValue == "LocalDrivingLicenseApplicationID")
            {
                dtAllApps.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterValue, txtFilterValue.Text.Trim());
            }
            else
            {
                dtAllApps.DefaultView.RowFilter= string.Format("[{0}] LIKE '{1}%'" ,FilterValue , txtFilterValue.Text.Trim());
            }
            lblRecords.Text = dgvLocalLicenseApps.Rows.Count.ToString();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm =
                new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm =
                new frmAddUpdateLocalDrivingLicenseApplication((int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID= (int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivngLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if(LocalDrivngLicenseApplication != null )
            {
                if(LocalDrivngLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frmListLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivngLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (LocalDrivngLicenseApplication != null)
            {
                if(LocalDrivngLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListLocalDrivingLicenseApplications_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sechduToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This Featuer Is Not Ready Yet.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);

        }

        private void sechduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("This Featuer Is Not Ready Yet.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //   MessageBox.Show("This Featuer Is Not Ready Yet.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value;
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
            //MessageBox.Show("This Featuer Is Not Ready Yet. Dont forget to change the tag later", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Featuer Is Not Ready Yet.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Featuer Is Not Ready Yet.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApps.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalLicenseApps.CurrentRow.Cells[5].Value;
            bool LicenseExist = LocalDrivingLicenseApplication.IsLicenseIssued();

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExist; //thise false must be (LicenseExist)

            showLicenseToolStripMenuItem.Enabled = LicenseExist;  //thise false must be (LicenseExist)

            editApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.New);


            sechduleTestsToolStripMenuItem.Enabled = !LicenseExist;

            cancelApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.New);

            deleteApplicationToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.New);



            //LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest); //the sol
            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PAssedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            sechduleTestsToolStripMenuItem.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PAssedStreetTest) &&
                (LocalDrivingLicenseApplication.ApplicationStatus == clsLocalDrivingLicenseApplication.enApplicationStatus.New);

            if (sechduleTestsToolStripMenuItem.Enabled)
            {
                sechduVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                sechduleWrittenTestToolStripMenuItem.Enabled = !PassedWrittenTest;

                sechduleStreetTestToolStripMenuItem.Enabled = !PAssedStreetTest;
            }
        }

            

        // sechdule Tests Click Openning 
        private void sechduToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {

            int PassedTests = (int)dgvLocalLicenseApps.CurrentRow.Cells[5].Value;
            sechduleStreetTestToolStripMenuItem.Enabled = false;
            sechduleWrittenTestToolStripMenuItem.Enabled = false;
            sechduVisionTestToolStripMenuItem.Enabled = false;

            switch(PassedTests)
            {
                case 2:
                    sechduleStreetTestToolStripMenuItem.Enabled = true; break;
                case 1:
                    sechduleWrittenTestToolStripMenuItem.Enabled= true; break;
                case 0:
                    sechduVisionTestToolStripMenuItem.Enabled=true; break;
                

            }
        }
    }
}
