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

namespace DVLD_WithoutUC.Users.Controls
{
    public partial class ctrlUserInfo : UserControl
    {

        private clsUser _User;
        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
        }
        
        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        

        private void _ResetUserInfo()
        {
            ctrlPersonCard1.LoudPersonInfo(-1);
            lblIsActive.Text = "????";
            lblUserID.Text = "????";
            lblUserName.Text = "????";
        }

        private void _FillUserCard()
        {
            ctrlPersonCard1.LoudPersonInfo(_User.PersonID);
            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else lblIsActive.Text = "No";

            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
        }
        public void LoudUserInfo(int UserId)
        {
            _UserID = UserId;

            _User = clsUser.FindByUserID(UserId);
            if(_User == null )
            {
                _ResetUserInfo();
                MessageBox.Show("No Users With User ID = " + UserId, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            else
            {
                _FillUserCard();
            }
            
        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
