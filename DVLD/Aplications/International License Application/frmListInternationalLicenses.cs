using DVLD_WithoutUC.Licenses;
using DVLD_WithoutUC.Licenses.Internatioal_Licenses;
using DVLD_WithoutUC.People;
using DVLDBussnessLayer.Applocations.International_Licesne;
using DVLDBussnessLayer.Drivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_WithoutUC.Aplications.International_License_Application
{
    public partial class frmListInternationalLicenses : Form
    {
        private DataTable _dtInternationalLicenseApplications;

        public frmListInternationalLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListInternationalLicenses_Load(object sender, EventArgs e)
        {
            _dtInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            cbFilterby.SelectedIndex = 0;
            dgvInternationalLicenses.DataSource = _dtInternationalLicenseApplications;
            lblRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
            if(dgvInternationalLicenses.Rows.Count >0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 110;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 110;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 80;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 80;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 110;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 110;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 60;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmNewInterNationalLicenseApplication frm = new frmNewInterNationalLicenseApplication();
            frm.ShowDialog();
            frmListInternationalLicenses_Load(null, null);
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmListInternationalLicenses_Load(null, null);
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int internationalLicenseID = (int)dgvInternationalLicenses.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo (internationalLicenseID);
            frm.ShowDialog();
            frmListInternationalLicenses_Load(null, null);
        }

        private void showPersonHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenses.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
            frmListInternationalLicenses_Load(null, null);

        }

        private void cbFilterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterby.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive .Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;

            }
            else
            {
                txtFilterValue.Visible = (cbFilterby.Text != "None");
                cbIsActive.Visible = false;
                if (cbFilterby.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                    txtFilterValue.Enabled = true;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColmun = "IsActive";
            string filterValue = cbIsActive.Text;
           
            switch(filterValue)
            {
                case "All":
                    break;
                case "Yes":
                    filterValue = "1";
                    break;
                case "No":
                    filterValue = "0";
                    break;
            }

            if (filterValue == "All")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";

            }
            else
                _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColmun, filterValue);
            lblRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch(cbFilterby.Text)
            {
                case "International License ID":
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if(txtFilterValue.Text == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();
                return;

            }

            _dtInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            lblRecords.Text = _dtInternationalLicenseApplications.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


    }
}
