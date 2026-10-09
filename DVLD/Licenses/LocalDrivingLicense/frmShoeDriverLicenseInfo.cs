using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.Licenses.LocalDrivingLicense
{
    public partial class frmShoeDriverLicenseInfo : Form
    {
        private int _LicenseID = -1;
        public frmShoeDriverLicenseInfo( int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void frmShoeDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadInfo(_LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
