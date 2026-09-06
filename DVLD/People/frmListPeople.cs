using DVLD_WithoutUC.People;
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

namespace DVLD_Buessness.People
{
    public partial class frmListPeople : Form
    {

        private  DataTable _dtAllPeople ;
        private DataTable _dtPeople ;

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            
            _RefreshPeopleList();
           
        }

        private void _RefreshPeopleList()
        {
             _dtAllPeople = clsPerson.GetAllPeople();
            if (_dtAllPeople == null)
                return;
             _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName", "LastName",
                                                                        "Gendor", "DateOfBirth", "Nationality", "Phone", "Email");

            dgvListPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dgvListPeople.Rows.Count.ToString();

            if (dgvListPeople.Rows.Count > 0)
            {
                dgvListPeople.Columns[0].HeaderText = "Person ID";
                //   dgvListPeople.Columns[0].Width = 110;

                dgvListPeople.Columns[1].HeaderText = "National NO";
                //   dgvListPeople.Columns[1].Width = 120;

                dgvListPeople.Columns[2].HeaderText = "First Name";
                //   dgvListPeople.Columns[2].Width = 120;

                dgvListPeople.Columns[3].HeaderText = "Second Name";
                // dgvListPeople.Columns[3].Width = 140;

                dgvListPeople.Columns[4].HeaderText = "Third Name";
                //  dgvListPeople.Columns[4].Width = 120;

                dgvListPeople.Columns[5].HeaderText = "Last Name";
                // dgvListPeople.Columns[5].Width = 120;

                dgvListPeople.Columns[6].HeaderText = "Gendor";
                // dgvListPeople.Columns[6].Width = 120;

                dgvListPeople.Columns[7].HeaderText = "Date Of Birth";
                //  dgvListPeople.Columns[7].Width = 140;

                dgvListPeople.Columns[8].HeaderText = "Nationality";
                //  dgvListPeople.Columns[8].Width = 120;


                dgvListPeople.Columns[9].HeaderText = "Phone";
                // dgvListPeople.Columns[9].Width = 120;

                dgvListPeople.Columns[10].HeaderText = "Email";
            }

                //// 1. Anchor all four sides so it scales up and down dynamically with window sizing
                dgvListPeople.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                // 2. Automatically stretch columns to drop gray empty spaces on the right side
                dgvListPeople.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 3. Keep vertical row spacing readable
                dgvListPeople.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }


        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddUpdat = new frmAddUpdatePerson();
            //frmAddUpdat.OnPersonAdded += RefreshPeopleList;
            frmAddUpdat.ShowDialog();
            frmListPeople_Load(null, null);
            
        }

        //private void RefreshPeopleList()
        //{
        //    _RefreshPeopleList();
        //}

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
             int ID = (int)dgvListPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmShowPersonInfo(ID);
            frm.ShowDialog();

        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson((int)dgvListPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListPeople_Load(null, null);
        }

       
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int.TryParse(dgvListPeople.CurrentRow.Cells[0].Value.ToString(), out int ID);
            if (MessageBox.Show("Are You Sure u Want to delete person with id =" + ID, 
                "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if(clsPerson.DeletePerson(ID))
                {
                    MessageBox.Show("Person Deleted Successfuly", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListPeople_Load(null, null);
                }
                else
                {
                    MessageBox.Show("You Can Not Delete This Person Cuz Have Data Linked To It" ,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National NO":
                    FilterColumn = "NationalNO";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Phone Number":
                    FilterColumn = "Phone";
                    break;
                case "Nationality":
                    FilterColumn = "Nationality";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                case "Gendor":
                    FilterColumn = "Gendor";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecords.Text = dgvListPeople.Rows.Count.ToString();
                return;
            }



            if (FilterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] ={1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            lblRecords.Text = dgvListPeople.Rows.Count.ToString();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None" &&
                                        cbFilterBy.Text!="Gendor");


            cbGender.Visible = (cbFilterBy.Text == "Gendor");

            if(cbFilterBy.Text == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
            }
            else if(cbFilterBy.Text =="Gendor")
            {
                cbGender.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            lblRecords.Text = dgvListPeople.Rows.Count.ToString();
            
        }
        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text != "Gendor")
                return;
            if (cbGender.Text == "All")
                _dtPeople.DefaultView.RowFilter = "";
            else if (cbGender.Text == "Male")
                _dtPeople.DefaultView.RowFilter = "[Gendor] ='Male'";
            else
                _dtPeople.DefaultView.RowFilter = "[Gendor] ='Female'";

            lblRecords.Text = dgvListPeople.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            //if (cbFilterBy.Text == "Person ID")
            //{
            //    e.Handled = !char.IsDigit(e.KeyChar) && char.IsControl(e.KeyChar);
            //}
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will Be Ready Soon" ,"Thanks" , MessageBoxButtons.OK);
        }

        private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will Be Ready Soon", "Thanks", MessageBoxButtons.OK);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will Be Ready Soon", "Thanks", MessageBoxButtons.OK);
        }

        private void AddNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddNew.PerformClick();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
