using DVLD_Buessness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBussnessLayer;

namespace DVLD_WithoutUC.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;//Defind custom event handler delegate with parmeters

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if(handler != null )
            {
                handler(PersonID);
            }
        }

        private bool _ShowAddPerson;
        public bool showAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled =_FilterEnabled;
            }
        }


        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

       // private int _PersonID = -1;
        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }
        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPerosnInfo; }
        }

        public void LoudPersonInfo(int PerosnID)
        {
            cbFilter.SelectedIndex = cbFilter.FindString("PersonID");
            txtFilterValue.Text = PerosnID.ToString();
            FindNow();
        }
        private void FindNow()
        {
            switch(cbFilter.Text)
            {
                case "PersonID":
                    ctrlPersonCard1.LoudPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "Natonal No":
                    ctrlPersonCard1.LoudPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }


            // Raise the PersonSelected event only when filtering is enabled and there are subscribers.

            if (OnPersonSelected != null && FilterEnabled)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }
            


        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            btnAddNewPerson.Visible = true;
            txtFilterValue.Focus();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please enter valid information.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);return;
            }
            FindNow();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterValue.Text))
            {
               // e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "this field is required");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();

            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }
        private void DataBackEvent(object sender ,int PersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoudPersonInfo(PersonID);
        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }


            if(cbFilter.Text == "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
