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

namespace DVLD_WithoutUC.Aplications.Controls
{
    public partial class clsApplicationBasicInfo : UserControl
    {
        private clsApplication _Application;
        private int _ApplicationID = -1;
        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public clsApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.FindBaseApplication(ApplicationID);
            if (_Application == null)
            {
                _ResetApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _FillApplicationInfo();
        }

        private void _FillApplicationInfo()
        {
            _ApplicationID      = _Application.ApplicationID;
            lblID.Text          = _Application.ApplicationID.ToString();
            lblStatus.Text      = _Application.ApplicationStatus.ToString();
            lblType.Text        = _Application.ApplicationTypeInfo.Title;
            lblFees.Text        =_Application.PaidFees.ToString();
            lblPerson.Text      =_Application.ApplicantFullName;
            lblDate.Text        = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text  = clsFormat.DateToShort(_Application.LastStatusDate);
            lblCreatedBy.Text   = _Application.CreatedByUserInfo.UserName;
        }
        private void _ResetApplicationInfo()
        {
            _ApplicationID = -1;
            lblID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblType.Text = "[???]";
            lblFees.Text = "[???]";
            lblPerson.Text = "[???]";
            lblDate.Text ="[???]";
            lblStatusDate.Text ="[???]";
            lblCreatedBy.Text ="[???]";
        }



        

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();
            LoadApplicationInfo(_ApplicationID);
        }
    }
}
