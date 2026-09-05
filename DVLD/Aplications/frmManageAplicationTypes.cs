using DVLD_Buessness.Aplication_Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Aplications
{
    public partial class frmManageAplicationTypes : Form
    {
        DataTable _dtAplicationTypes;
        public frmManageAplicationTypes()
        {
            InitializeComponent();
        }

        private void frmManageAplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAplicationTypes = clsAplicationTypes.ListAplicationTypes();
            dgvAplicationTypes.DataSource = _dtAplicationTypes;
            dgvAplicationTypes.Columns[0].Width = 60;
            dgvAplicationTypes.Columns[1].Width = 320;
            dgvAplicationTypes.Columns[2].Width = 80;
            //dgvAplicationTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lblRecords.Text = dgvAplicationTypes.Rows.Count.ToString();
        }

        private void editAplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvAplicationTypes.CurrentRow.Cells[0].Value;
            frmEditApplicatioinType frm = new frmEditApplicatioinType(ID);
            frm.ShowDialog();
            frmManageAplicationTypes_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
