using DVLD_Buessness;
using DVLD_Buessness.Utilty;
using DVLD_Buessness.Validations;
using DVLD_WithoutUC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PErsonID);

        public event DataBackEventHandler DataBack;
        enum enMode { AddNew =0 ,Update =1}
        enum enGendor { Male=0 ,Female =1}

        private enMode _Mode ;
        private int _PersonID = -1;
        private clsPeople_Buessness _Person;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
             _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _ResetDefaultValues()
        {
            _FillCountriesInComboBox();

            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person =new clsPeople_Buessness();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            if (rbMale.Checked)
                pbPersonPic.Image = Resources.man;
            else
                pbPersonPic.Image = Resources.woman_avatar;

            // Link Lable Remove Visible true/false
            llRemoveImage.Visible = (pbPersonPic.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountries.SelectedIndex = cbCountries.FindString("Saudi Arabia");

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNO.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";


            rbMale.Checked =true;


        }
        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsBusCountries.GetAllCountries();
            foreach(DataRow dr in dtCountries.Rows)
            {
                cbCountries.Items.Add(dr["CountryName"]);
            }
        }
        private void _LoadData()
        {
            _Person = clsPeople_Buessness.Find(_PersonID);
            if(_Person == null)
            {
                MessageBox.Show("No Person With ID["+_PersonID+"]" ,"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            lblPersonID.Text    =_Person.PersonID.ToString();
            txtFirstName.Text   = _Person.FirstName;
            txtSecondName.Text  = _Person.SecName;
            txtThirdName.Text   = _Person.ThirdName;
            txtLastName.Text    = _Person.LastName;
            txtNationalNO.Text  = _Person.NationalNo;
            txtEmail.Text       = _Person.Email;
            txtPhone.Text       = _Person.Phone;
            txtAddress.Text     = _Person.Address;
            dtpDateOfBirth.Value =_Person.DateOfBirth;

            if(_Person.Gendor == 0)
                rbMale.Checked =true;
            else
                rbFemale.Checked =true;
            cbCountries.SelectedIndex = cbCountries.FindString(_Person.CountryInfo.CountryName);

            if (_Person.ImagePath != "")
                pbPersonPic.ImageLocation = _Person.ImagePath;

            llRemoveImage.Visible = (_Person.ImagePath != "");


        }



        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds are not valid", "Error" ,MessageBoxButtons.OK ,MessageBoxIcon.Error);
                return;
            }
            if (!_HundelPersonImage())
                return;

            int NationalCountryID = clsBusCountries.Find(cbCountries.Text).CountryID;
            _Person.FirstName= txtFirstName.Text.Trim();
            _Person.SecName =txtSecondName.Text.Trim();
            _Person.ThirdName =txtThirdName.Text.Trim();
            _Person.LastName =txtLastName.Text.Trim();
            _Person.Phone =txtPhone.Text.Trim();
            _Person.Email =txtEmail.Text.Trim();
            _Person.Address =txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalNo =txtNationalNO.Text.Trim();

                if (rbMale.Checked)
                    _Person.Gendor = (short)enGendor.Male;
                else
                    _Person.Gendor = (short)enGendor.Female;

            _Person.NationaltyCountryID = NationalCountryID;

            if(pbPersonPic.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonPic.ImageLocation;
            }
            else
            {
                _Person.ImagePath = "";
            }

            if(_Person.Save())
            {
                lblPersonID.Text =_Person.PersonID.ToString();

                lblTitle.Text = "Update Person"; 
                MessageBox.Show ("Data Saved Successfuly" ,"Save" , MessageBoxButtons.OK ,MessageBoxIcon.Information );
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                Exception ex = new Exception();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Error: Data Not Saved Successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool _HundelPersonImage()
        {
            if(_Person.ImagePath != pbPersonPic.ImageLocation)
            {
                if(_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch(IOException)
                    {

                    }
                }
                if(pbPersonPic.ImageLocation != null)
                {
                    string SourceImageFile =pbPersonPic.ImageLocation.ToString();
                    if(clsUtil.CopyImageToProjectImagesFile(ref SourceImageFile))
                    {
                        pbPersonPic.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Pictuer Faild" ,"Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }


        private void ValidateEmptyTextBox(object sender ,CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if(string.IsNullOrEmpty(Temp.Text.Trim()))
            {
               //e.Cancel =true;
                errorProvider1.SetError(Temp, "This Field is Required");
            }
            else
            {
                errorProvider1.SetError(Temp, "");
            }
        }
        private void txtEmail_Validating(object sender , CancelEventArgs e)
        {
            if(txtEmail.Text.Trim() == "")
            {
                return;
            }

            if(!clsValidations.ValidateEmail(txtEmail.Text))
            {
                //e.Cancel =true;
                errorProvider1.SetError(txtEmail, "InValid Email Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }

        }

        private void txtNatoinalNo_Validating(object sender , CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtNationalNO.Text.Trim()))
            {
                e.Cancel =true;
                errorProvider1.SetError(txtNationalNO, "This Filed is Required");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNO, null);
            }
            if(txtNationalNO.Text.Trim() != _Person.NationalNo && clsPeople_Buessness.IsExist(txtNationalNO.Text.Trim()))
            {
              //  e.Cancel =true;
                errorProvider1.SetError(txtNationalNO, "National Number  is Used for another Person");
            }
            else
            {
                errorProvider1.SetError(txtNationalNO, null);
            }
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (pbPersonPic.ImageLocation == null)
                pbPersonPic.Image = Resources.man;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {

            if (pbPersonPic.ImageLocation == null)
                pbPersonPic.Image = Resources.woman_avatar;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "\"Image Files(*.BMP;*.JPG;*.JPEG;*.PNG;*.GIF;*.WEBP)|*.BMP;*.JPG;*.JPEG;*.PNG;*.GIF;*.WEBP|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if(openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                string selectedFilePath =openFileDialog1.FileName;
                pbPersonPic.Load(selectedFilePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonPic.ImageLocation = null;

            if(rbMale.Checked)
            {
                pbPersonPic.Image = Resources.man;
            }
            else
            {
                pbPersonPic.Image = Resources.woman_avatar;
            }
            llRemoveImage.Visible = false;
        }
    }
}
