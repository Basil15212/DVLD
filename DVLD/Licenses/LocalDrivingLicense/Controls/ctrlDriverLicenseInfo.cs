using DVLD_WithoutUC.Properties;
using DVLDBussnessLayer.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Licenses.LocalDrivingLicense.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {

        private int _LicenseID = -1;
        private clsLicense _License;

        


        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicense
        {
            get { return _License; }
        }


        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gendor == 0)
                pbPerosnImage.Image = Resources.man;
            else
                pbPerosnImage.Image = Resources.woman_avatar;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if(ImagePath != "")
            {
                if(File.Exists(ImagePath))
                {
                    pbPerosnImage.Load(ImagePath);
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void LoadInfo(int LicenseID)
        {
            _LicenseID= LicenseID;
            _License = clsLicense.FindByLicenseID(_LicenseID);
            if(_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNO.Text = _License.DriverInfo.PersonInfo.NationalNo.ToString();
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblIssueReaseon.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            lblIsActive.Text = _License.IsActive  ? "No" : "Yes";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIsDetained.Text = "No";// Will Be Edit Later!!!

            _LoadPersonImage();
        }
    }
}
