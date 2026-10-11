using DVLD_WithoutUC.Licenses;
using DVLD_WithoutUC.People;
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

namespace DVLD_WithoutUC.Drivers
{
    public partial class frmListDriver : Form
    {
        DataTable _dtDrivers;

        public frmListDriver()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmListDriver_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            _dtDrivers = clsDriver.ListDrivers();
            dgvDrivers.DataSource = _dtDrivers;
            lblRecords.Text = dgvDrivers.Rows.Count.ToString();
            if(dgvDrivers.Rows.Count >0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 170;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 150;
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");
            if (cbFilter.Text == "None")
                txtFilterValue.Enabled = false;
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string Filter = "";
            switch(cbFilter.Text)
            {
                case "Driver ID":
                    Filter = "DriverID";
                    break;
                case "Person ID":
                    Filter = "PersonID";
                    break;
                case "National No":
                    Filter = "NationalNo";
                    break;
                default:
                    Filter = "";
                    break;
            }

            if(txtFilterValue.Text== "" || Filter =="None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblRecords.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }
            if(Filter != "Full Name" || Filter != "Noational No")
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", Filter, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", Filter, txtFilterValue.Text.Trim());
            }

            lblRecords.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.Text == "Driver ID" || cbFilter.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmListDriver_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonLicensesHistory frm = new frmShowPersonLicensesHistory(PersonID);
            frm.ShowDialog();
            //MessageBox.Show("Not Ready Yet" ,"Soon" , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
