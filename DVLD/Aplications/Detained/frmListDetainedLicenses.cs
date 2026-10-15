using DVLD_WithoutUC.Licenses;
using DVLD_WithoutUC.Licenses.LocalDrivingLicense;
using DVLD_WithoutUC.People;
using DVLDBussnessLayer.Detained;
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

namespace DVLD_WithoutUC.Aplications.Detained
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dtAllDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllDetainedLicenses = clsDetaindLicense.GetAllDetainedLicenses();
            dgvDetainLicenses.DataSource = _dtAllDetainedLicenses;
            lblRecords.Text = dgvDetainLicenses.Rows.Count.ToString();
            if(dgvDetainLicenses.Rows.Count > 0)
            {
                dgvDetainLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainLicenses.Columns[0].Width = 70;
                dgvDetainLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainLicenses.Columns[1].Width = 70;
                dgvDetainLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainLicenses.Columns[2].Width = 110;
                dgvDetainLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainLicenses.Columns[3].Width = 110;
                dgvDetainLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainLicenses.Columns[4].Width = 80;
                dgvDetainLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainLicenses.Columns[5].Width = 110;
                dgvDetainLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainLicenses.Columns[6].Width = 90;
                dgvDetainLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainLicenses.Columns[7].Width = 225;
                dgvDetainLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainLicenses.Columns[8].Width = 100;


            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filter = "";
            switch(cbFilterBy.Text)
            {
                case "Detain ID":
                    filter = "DetainID";
                    break;
                case "Is Released":
                    filter = "IsReleased";
                    break;
                case "National No":
                    filter = "NationalNo";
                    break;
                case "Full Name":
                    filter = "FullName";
                    break;
                case "Release Application ID":
                    filter = "ReleaseApplicationID";
                    break;
                default:
                    filter = "None";
                    break;
            }
            if(txtFilterValue.Text.Trim() == "" || filter == "None")
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
                lblRecords.Text = dgvDetainLicenses.Rows.Count.ToString();
                return;
            }
            if(filter == "DetainID" || filter == "ReleaseApplicationID")
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", filter, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filter, txtFilterValue.Text.Trim());
            }
            lblRecords.Text = dgvDetainLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsDetained.Visible = true;
                cbIsDetained.SelectedIndex = 0;
                cbIsDetained.Focus();
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsDetained.Visible = false;

                if(cbFilterBy.Text =="None" )
                {
                    txtFilterValue.Enabled = false;
                }
                else
                {
                    txtFilterValue.Enabled = true;
                }
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

        private void cbIsDetained_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsDetained.Text;
            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue= "0";
                    break;
            }
            if(FilterValue=="All")
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";

            }
            else
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            }
            lblRecords.Text = dgvDetainLicenses.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "ReleaseApplicationID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            releaseLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainLicenses.CurrentRow.Cells[3].Value;
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainLicenses.CurrentRow.Cells[1].Value;
            int PerosnID= clsLicense.FindByLicenseID(LicenseID).DriverInfo.PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PerosnID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainLicenses.CurrentRow.Cells[1].Value;
            frmShoeDriverLicenseInfo frm = new frmShoeDriverLicenseInfo(LicenseID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void showLicenseHistoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainLicenses.CurrentRow.Cells[1].Value;
            int PerosnID = clsLicense.FindByLicenseID(LicenseID).DriverInfo.PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PerosnID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainLicenses.CurrentRow.Cells[1].Value;
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            frmListDetainedLicenses_Load(null, null);
        }
    }
}
