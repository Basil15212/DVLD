using DVLDBussnessLayer;
using DVLD_DataAccess.Text_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.TestT_ypes
{
    public partial class frmListTestTypes : Form
    {
        public DataTable _dtTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            
            dgvTestTypes.DataSource = _dtTestTypes;

            if (dgvTestTypes.Rows.Count == 0)
                return;
            dgvTestTypes.RowHeadersWidth = 15;
            dgvTestTypes.Columns["TestTypeID"].Width = 50;
            dgvTestTypes.Columns["TestTypeTitle"].Width = 200;
            dgvTestTypes.Columns["TestTypeDescription"].Width = 320;
            dgvTestTypes.Columns["TestTypeFees"].Width = 100;

            lblRecords.Text =dgvTestTypes.Rows.Count.ToString();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);

        }
    }
}
