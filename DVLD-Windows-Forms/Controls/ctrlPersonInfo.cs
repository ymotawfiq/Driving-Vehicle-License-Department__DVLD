using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class ctrlPersonInfo : UserControl
    {
        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        private int _PersonID { get; set; } = -1;
        private clsPerson _Person;

        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {

        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmAddEditPersons(_PersonID).ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void LoadPersonWithID(int PersonID)
        {
            _PersonID = PersonID;
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
        }

    }
}
