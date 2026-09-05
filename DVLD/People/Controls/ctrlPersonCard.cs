using DVLD_Buessness;
using DVLD_WithoutUC.Properties;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_WithoutUC.People;

namespace DVLD_WithoutUC
{
    public partial class ctrlPersonCard : UserControl
    {

        private clsPeople_Buessness _Person;
        private int _PersonID =-1;
        public int PersonID
        {
            get { return _PersonID; }
        }
        public clsPeople_Buessness SelectedPerosnInfo
        {
            get { return _Person; }
        }


        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoudPersonInfo(int  personID)
        {
            _Person = clsPeople_Buessness.Find(personID);
            if(_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show ("No Person Found With ID = "+ personID.ToString(), "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }

        public void LoudPersonInfo(string NationalNO)
        {
            _Person = clsPeople_Buessness.Find(NationalNO);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person Found With NatonalNO = " +NationalNO , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillPersonInfo();
        }


        public void ResetPersonInfo()
        {
            llEditPersonInfo.Enabled = false;
            lblPersonID.Text = "??????";
            lblFullName.Text = "??????";
            lblNationalNO.Text = "??????";
            lblGendor.Text = "??????";
            lblEmail.Text = "??????";
            lblAddress.Text = "??????";
            lblPhone.Text = "??????";
            lblDateOfBirth.Text = "??????";
            lblCountry.Text = "??????";
            pbPersonImage.Image = Resources.man;
        }

        private void _LoudPersonImage()
        {
            if(_Person.Gendor== 0)
            {
                pbPersonImage.Image = Resources.man;
            }
            else
            {
                pbPersonImage.Image = Resources.woman_avatar;
            }

            string ImagePath = _Person.ImagePath;
            if(ImagePath!="")
                if(File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Couldnt find this image  = "+ImagePath ,"Error" , MessageBoxButtons.OK,MessageBoxIcon.Error);

        }
        private void _FillPersonInfo()
        {
           
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _PersonID.ToString();
            lblFullName.Text = _Person.FullName();
            lblNationalNO.Text =_Person.NationalNo;
            if (_Person.Gendor == 0)
                lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";
            
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();

            clsBusCountries Country = clsBusCountries.Find(_Person.NationaltyCountryID);
            if(Country != null)
            {
                lblCountry.Text = Country.CountryName;
            }
            else
            {
                lblCountry.Text = "UnKnown";
            }

               
            _LoudPersonImage();
                


        }
        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frmUpdate = new frmAddUpdatePerson(_PersonID);
            frmUpdate.ShowDialog();
            //reefresh
            LoudPersonInfo(_PersonID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCard_Load(object sender, EventArgs e)
        {

        }
    }
}
