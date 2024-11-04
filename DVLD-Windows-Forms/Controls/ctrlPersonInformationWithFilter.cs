using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class ctrlPersonInformationWithFilter : UserControl
    {
        enum enFilter { NationalNo, ID, Name, Phone, Email}
        enFilter _filter;
        clsPerson _Person;

        public int PersonID { get; set; } = -1;

        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int personID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(personID);
            }
        }

        public ctrlPersonInformationWithFilter()
        {
            InitializeComponent();
            if (PersonID == -1) _Person = new clsPerson();
            else
            {
                _Person = clsPerson.FindByID(PersonID);
                gbFilter.Enabled = false;
                _FillControlWithPersonData();
            } 
                
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlPersonInformationWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.Items.Add(enFilter.NationalNo);
            cbFilter.Items.Add(enFilter.ID);
            cbFilter.Items.Add(enFilter.Name);
            cbFilter.Items.Add(enFilter.Phone);
            llEditPersonInfo.Visible = false;
            cbFilter.SelectedIndex = 1;
        }

        private void _FillControlWithPersonData()
        {
            if (_Person.PersonID != -1)
            {
                llEditPersonInfo.Visible = true;
                lblAddress.Text = _Person.Address;
                lblCountry.Text = clsCountry.FindCountryByID(_Person.NationalityCountryID).CountryName;
                lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
                lblEmail.Text = _Person.Email;
                if (_Person.Gendor == 0)
                {
                    lblGender.Text = "Male";
                    pbGender.Image = Resources.male;
                }
                else
                {
                    lblGender.Text = "FeMale";
                    pbGender.Image = Resources.female;
                }
                lblName.Text = $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";
                lblNationalNo.Text = _Person.NationalNo;
                lblPersonID.Text = _Person.PersonID.ToString();
                lblPhone.Text = _Person.Phone;
                try
                {
                    pbUserImage.Image = Image.FromFile(_Person.ImagePath);
                }
                catch (Exception)
                {
                    pbUserImage.Image = null;
                    if (_Person.Gendor == 0) pbUserImage.Image = Resources.maleBigIcon;
                    else pbUserImage.Image = Resources.femaleBigIcon;
                }
                return;
            }
            MessageBox.Show("Person Not Found", "Not Found", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Error);
        }


        bool _FilterByID()
        {
            if (clsPerson.ExistsByID(Convert.ToInt32(tbSearch.Text)))
            {
                _Person = clsPerson.FindByID(Convert.ToInt32(tbSearch.Text));
                if (OnPersonSelected != null) OnPersonSelected(_Person.PersonID);
                return true;
            }
            return false;
        }

        bool _FilterByNationalNo()
        {
            if (clsPerson.ExistsByNationalNo(tbSearch.Text))
            {
                _Person = clsPerson.FindByNationalNo(tbSearch.Text);
                if (OnPersonSelected != null) OnPersonSelected(_Person.PersonID);
                return true;
            }
            return false;
        }

        bool _FilterByPhone()
        {
            if (clsPerson.ExistsByPhone(tbSearch.Text))
            {
                _Person = clsPerson.FindByPhone(tbSearch.Text);
                if (OnPersonSelected != null) OnPersonSelected(_Person.PersonID);
                return true;
            }
            return false;
        }

        bool _FilterByEmail()
        {
            if (clsPerson.ExistsByEmail(tbSearch.Text))
            {
                _Person = clsPerson.FindByEmail(tbSearch.Text);
                if (OnPersonSelected != null) OnPersonSelected(_Person.PersonID);
                return true;
            }
            return false;
        }

        bool _FilterByName()
        {
            if (clsPerson.FindFirstPersonByName(tbSearch.Text) != null)
            {
                _Person = clsPerson.FindFirstPersonByName(tbSearch.Text);
                if (OnPersonSelected != null) OnPersonSelected(_Person.PersonID);
                return true;
            }
            return false;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text.Trim() != "")
            {
                if (enFilter.Email.ToString() == cbFilter.SelectedItem.ToString()) _FilterByEmail();

                else if (enFilter.ID.ToString() == cbFilter.SelectedItem.ToString()) _FilterByID();

                else if (enFilter.Name.ToString() == cbFilter.Text.ToString()) _FilterByName();

                else if (enFilter.NationalNo.ToString() == cbFilter.Text.ToString()) _FilterByNationalNo();

                else if (enFilter.Phone.ToString() == cbFilter.Text.ToString()) _FilterByPhone();
                
            }
            _FillControlWithPersonData();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditPersons form = new frmAddEditPersons(-1);
            form.ShowDialog();
            _FillControlWithPersonData();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(_Person!=null && _Person.PersonID != -1)
            {
                frmAddEditPersons form = new frmAddEditPersons(_Person.PersonID);
                form.ShowDialog();
                //_FillControlWithPersonData();
            }
        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(enFilter.ID.ToString() == cbFilter.SelectedItem.ToString())
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void LoadPersonWithID(int PersonID)
        {
            if (PersonID != -1) _Person = clsPerson.FindByID(PersonID);
            else _Person = new clsPerson();
            lblAddress.Text = _Person.Address;
            lblCountry.Text = clsCountry.FindCountryByID(_Person.NationalityCountryID).CountryName;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblEmail.Text = _Person.Email;
            lblName.Text = $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";
            lblNationalNo.Text = _Person.NationalNo;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblPhone.Text = _Person.Phone;
            if (_Person.Gendor == 0) lblGender.Text = "Male";
            else lblGender.Text = "Female";
            try
            {
                pbUserImage.Image = Image.FromFile(_Person.ImagePath);
                if (_Person.Gendor == 0) pbGender.Image = Resources.male;
                else pbGender.Image = Resources.female;
            }
            catch (Exception)
            {
                if (_Person.Gendor == 0) pbUserImage.Image = Resources.maleBigIcon;
                else pbUserImage.Image = Resources.femaleBigIcon;
            }
            gbFilter.Enabled = false;
        }

    }
}
