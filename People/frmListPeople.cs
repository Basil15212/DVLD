using DVLD_WithoutUC.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Buessness.People
{
    public partial class frmListPeople : Form
    {
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            DataTable dtListPeople = clsPeople_Buessness.ListPeople();
            dgvListPeople.DataSource = dtListPeople;
            lblRecords.Text = dgvListPeople.RowCount.ToString();

            // 1. Anchor all four sides so it scales up and down dynamically with window sizing
            dgvListPeople.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 2. Automatically stretch columns to drop gray empty spaces on the right side
            dgvListPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 3. Keep vertical row spacing readable
            dgvListPeople.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddUpdat = new frmAddUpdatePerson();
            frmAddUpdat.ShowDialog();
        }
    }
}
