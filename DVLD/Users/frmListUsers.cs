using DVLDBussnessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Users
{
    public partial class frmListUsers : Form
    {

        private DataTable _dtAllUsers;
        
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void RefreshRecords()
        {
            lblRecords.Text =dgvUsers.RowCount.ToString();
        }
        private void _LoudUsers()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;

            dgvUsers.Columns["UserID"].HeaderText = "User ID";
            dgvUsers.Columns["PersonID"].HeaderText = "Person ID";
            dgvUsers.Columns["FullName"].HeaderText = "Full Name";
            
            dgvUsers.Columns["UserName"].HeaderText = "User Name";
            dgvUsers.Columns["IsActive"].HeaderText = "Is Active";
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            RefreshRecords();

        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(dgvUsers.SelectedRows.Count==0)
                e.Cancel =true;
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _LoudUsers();
            cbFilterBy.SelectedIndex = 0;
        }

        private void _DGVnotEmpty()
        {
            if (dgvUsers.SelectedRows.Count == 0)
                return;
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DGVnotEmpty();

            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUSer frm= new frmAddUpdateUSer();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DGVnotEmpty();
            frmAddUpdateUSer frm = new frmAddUpdateUSer((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddNewUser.PerformClick();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DGVnotEmpty();
            int ID = (int)dgvUsers.CurrentRow.Cells[0].Value;

            if(ID != clsGlobal.CurrentUser.UserID)
            {
                if (MessageBox.Show("Are You Sure u Want to delete person with id =" + ID,
                "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    clsUser.DeleteUser(ID);
                    frmListUsers_Load(null, null);
                }
                else
                {
                    MessageBox.Show("This User Is Linkd With another Data", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You Can't Delete this User Cuz He is the current User" ,"Error" ,
                    MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DGVnotEmpty();
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DGVnotEmpty();
            MessageBox.Show("Will Finish it Soon ", "Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sendWhatsappToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DGVnotEmpty();
            MessageBox.Show("Will Finish it Soon ", "Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DGVnotEmpty();
            MessageBox.Show("Will Finish it Soon ", "Soon",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvUsers_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && e.RowIndex>=0)
            {
                dgvUsers.ClearSelection();
                dgvUsers.Rows[e.RowIndex].Selected = true;
            }
        }


        //Filter and Hundlings
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFiterValue.Visible = (cbFilterBy.Text != "None" &&
                                    cbFilterBy.Text != "Is Active");

            cbIsActive.Visible = (cbFilterBy.Text == "Is Active");

            if (cbFilterBy.Text == "None")
                _dtAllUsers.DefaultView.RowFilter = "";
            else if (cbFilterBy.Text == "Is Active")
                cbIsActive.SelectedIndex = 0;
            else
            {
                txtFiterValue.Text = "";
                txtFiterValue.Focus();
            }
            lblRecords.Text =dgvUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text != "Is Active")
                return;
            if (cbIsActive.Text == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else if (cbIsActive.Text == "Yes")
                _dtAllUsers.DefaultView.RowFilter = "[IsActive] =1";
            else
                _dtAllUsers.DefaultView.RowFilter = "[IsActive] =0";
            lblRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void txtFiterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch(cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
          }
            if(txtFiterValue.Text.Trim() == "" || txtFiterValue.Text.Trim()=="None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "UserID" || FilterColumn == "PersonID")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFiterValue.Text.Trim());
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter =string.Format("[{0}] Like '{1}%'",FilterColumn, txtFiterValue.Text.Trim());
            }
            lblRecords.Text = dgvUsers.Rows.Count.ToString();

        }

        private void txtFiterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "User ID" || cbFilterBy.Text =="Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
