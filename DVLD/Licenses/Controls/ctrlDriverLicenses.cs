using DVLD_WithoutUC.Licenses.Internatioal_Licenses;
using DVLD_WithoutUC.Licenses.LocalDrivingLicense;
using DVLDBussnessLayer.Drivers;
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

namespace DVLD_WithoutUC.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID = -1;
        private clsDriver _Driver;

        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseInfo()
        {
            _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(_DriverID);

            if (_dtDriverLocalLicensesHistory == null)
            {
                MessageBox.Show("No Driver with Driver ID =" + _DriverID, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dgvLocal.DataSource = _dtDriverLocalLicensesHistory;
            lblLocalRecords.Text = dgvLocal.Rows.Count.ToString();
            if(dgvLocal.Rows.Count >0)
            {
                dgvLocal.Columns[0].HeaderText = "Lic.ID";
                dgvLocal.Columns[0].Width = 110;

                dgvLocal.Columns[1].HeaderText = "App.ID";
                dgvLocal.Columns[1].Width = 110;

                dgvLocal.Columns[2].HeaderText = "Class Name";
                dgvLocal.Columns[2].Width = 250;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 150;

                dgvLocal.Columns[4].HeaderText = "Expiration Date";
                dgvLocal.Columns[4].Width = 150;

                dgvLocal.Columns[5].HeaderText = "Is Active";
                dgvLocal.Columns[5].Width = 80;
            }

        }

        // international info loading will be here :)
        private void _LoadInternationalLicenseInfo()
        {
            _dtDriverInternationalLicensesHistory = clsDriver.GetInternationalLicenses(_DriverID);
            dgvInterNational.DataSource = _dtDriverInternationalLicensesHistory;
            lblInternationalRecords.Text = dgvInterNational.Rows.Count.ToString();
            if(dgvInterNational.Rows.Count>0)
            {
                dgvInterNational.Columns[0].HeaderText = "Int.License ID";
                dgvInterNational.Columns[0].Width = 110;
                dgvInterNational.Columns[1].HeaderText = "Application ID";
                dgvInterNational.Columns[1].Width = 110;
                dgvInterNational.Columns[2].HeaderText = "L.License ID";
                dgvInterNational.Columns[2].Width = 259;
                dgvInterNational.Columns[3].HeaderText = "Issue Date";
                dgvInterNational.Columns[3].Width = 150;
                dgvInterNational.Columns[4].HeaderText = "Expiration Date";
                dgvInterNational.Columns[4].Width = 150;
                dgvInterNational.Columns[5].HeaderText = "Is Active";
                dgvInterNational.Columns[5].Width = 80;
            }
        }


        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(_DriverID);

            if(_Driver == null)
            {
               MessageBox.Show("No Driver With ID =" +_DriverID  ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
            //Load international will be here:)

        }
        public void LoadDriverByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);
            if(_Driver == null)
            {
                MessageBox.Show("No Driver Linked with Person ID =" + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = _Driver.DriverID;
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
            //Load international will be here:)
        }
        private void gbLicensesHistory_Enter(object sender, EventArgs e)
        {

        }

        private void showLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocal.CurrentRow.Cells[0].Value;
            frmShoeDriverLicenseInfo frm = new frmShoeDriverLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        public void Clear()
        {
           _dtDriverLocalLicensesHistory.Clear();
            _dtDriverInternationalLicensesHistory.Clear();
        }

        private void showInternationalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID =(int)dgvInterNational.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        //Show internationa info Will be here :)
    }
}
