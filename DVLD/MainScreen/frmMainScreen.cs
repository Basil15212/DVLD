using DVLD_Buessness.People;
//using DVLD_Buessness.Users;
using DVLD_WithoutUC.Aplications;
using DVLD_WithoutUC.TestT_ypes;
using DVLD_WithoutUC.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC
{
    public partial class frmMainScreen : Form
    {
        frmLogin _FrmLogin;
        public frmMainScreen(frmLogin frmLogin)
        {
            InitializeComponent();
            _FrmLogin = frmLogin;
        }
        public frmMainScreen()
        {
            InitializeComponent();
           // _FrmLogin = frmLogin;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmListPeople frmListPeople = new frmListPeople();
                frmListPeople.ShowDialog();
            }catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CurrentUserID = clsGlobal.CurrentUser.UserID;
            frmUserInfo frm = new frmUserInfo(CurrentUserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm =new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void frmMainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMainScreen_Load(object sender, EventArgs e)
        {

        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm =new frmListTestTypes();
            frm.ShowDialog();
        }

        private void manageAplicatonTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageAplicationTypes frm =new frmManageAplicationTypes();
            frm.ShowDialog();
        }
    }
}
