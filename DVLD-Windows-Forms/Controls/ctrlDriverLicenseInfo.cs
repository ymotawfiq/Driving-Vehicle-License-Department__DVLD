using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Windows_Forms.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }


        public event Action<int> OnPersonIDChanged;
        protected virtual void PersonIDChanged(int personID)
        {
            Action<int> handler = OnPersonIDChanged;
            if (handler != null)
            {
                handler(personID);
            }
        }

        private void ctrlDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void _LoadPersonWithID(int PersonID)
        {
            clsPerson person = new clsPerson();
            if (PersonID != -1) person = clsPerson.FindByID(PersonID);
            lblAddress.Text = person.Address;
            lblCountry.Text = clsCountry.FindCountryByID(person.NationalityCountryID).CountryName;
            lblDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            lblEmail.Text = person.Email;
            lblName.Text = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}";
            lblNationalNo.Text = person.NationalNo;
            lblPersonID.Text = person.PersonID.ToString();
            lblPhone.Text = person.Phone;
            lblTitle.Text = "Driver License Info";
            if (person.Gendor == 0) lblGender.Text = "Male";
            else lblGender.Text = "Female";
            try
            {
                pbUserImage.Image = Image.FromFile(person.ImagePath);
                if (person.Gendor == 0) pbGender.Image = Resources.male;
                else pbGender.Image = Resources.female;
            }
            catch (Exception)
            {
                if (person.Gendor == 0) pbUserImage.Image = Resources.maleBigIcon;
                else pbUserImage.Image = Resources.femaleBigIcon;
            }
            if(OnPersonIDChanged != null)
            {
                OnPersonIDChanged(PersonID);
            }
        }

        public void LoadControlByLDLAppID(int ldlAppID)
        {
            int personID = clsPerson.GetPersonIDByLocalDrivingLicense(ldlAppID);
            if (clsDriver.IsDriverExistsByPersonID(personID))
            {
                _LoadPersonWithID(personID);

                clsDriver driver = clsDriver.FindByPersonID(personID);
                clsLocalDrivingLicence localDrivingLicence = clsLocalDrivingLicence.FindByID(ldlAppID);
                clsLicenceClasses licenceClass = clsLicenceClasses.FindLicenceClassByID(
                    localDrivingLicence.LicenseClassID);
                clsLicense license = clsLicense.FindByLDLAppID(ldlAppID);

                if (clsDetainedLicenses.ExistsByLicenseID(license.LicenseID))
                    lblIsDetained.Text = "True";
                else
                    lblIsDetained.Text = "False"; 

                lblDriverID.Text = driver.DriverID.ToString();
                lblClassName.Text = licenceClass.ClassName;
                lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
                lblIsseueDate.Text = license.IssueDate.ToShortDateString();
                lblLicenseID.Text = license.LicenseID.ToString();

                if (license.Notes == "")
                    lblNotes.Text = "No Notes";
                else
                    lblNotes.Text = license.Notes;

                if (license.IsActive)
                    lblIsActive.Text = "True";
                else
                    lblIsActive.Text = "False";


                if (license.IssueReason == 1) lblIsseueReason.Text = "First Time";
                else if (license.IssueReason == 2) lblIsseueReason.Text = "Renew License";
                else if (license.IssueReason == 3) lblIsseueReason.Text = "Replacement For Damaged";
                else if (license.IssueReason == 4) lblIsseueReason.Text = "Replacement For Lost";

            }
        }

        public void LoadControlByLicenseID(int licenseID, int iLicenseID=-1)
        {
            int personID = clsPerson.GetPersonIDByLicenseID(licenseID);
            if (clsDriver.IsDriverExistsByPersonID(personID))
            {
                if (clsLicense.ExistsByID(licenseID))
                {
                    _LoadPersonWithID(personID);
                    clsLicense license = clsLicense.FindByID(licenseID);
                    lblDriverID.Text = license.DriverID.ToString();
                    lblClassName.Text = clsLicenceClasses.FindLicenceClassByID(license.LicenseClass).ClassName;
                    lblExpirationDate.Text = license.ExpirationDate.ToShortDateString();
                    lblIsseueDate.Text = license.IssueDate.ToShortDateString();
                    lblLicenseID.Text = license.LicenseID.ToString();
                    lblInternationLicenseID.Text = iLicenseID.ToString();

                    if (clsDetainedLicenses.ExistsByLicenseID(license.LicenseID))
                        lblIsDetained.Text = "True";
                    else
                        lblIsDetained.Text = "False";

                    if (license.Notes == "")
                        lblNotes.Text = "No Notes";
                    else
                        lblNotes.Text = license.Notes;

                    if (license.IsActive)
                        lblIsActive.Text = "True";
                    else
                        lblIsActive.Text = "False";

                    if (iLicenseID != -1)
                    {
                        lblInternationLicenseID.Text = iLicenseID.ToString();
                        lblTitle.Text = "International Driver License Info";
                    }

                    else
                    {
                        lblInternationLicenseID.Enabled 
                            = lblNotValueInternationalLicenseID.Enabled = false;
                    }
                    if (license.IssueReason == 1) lblIsseueReason.Text = "First Time";
                    else if (license.IssueReason == 2) lblIsseueReason.Text = "Renew License";
                    else if (license.IssueReason == 3) lblIsseueReason.Text = "Replacement For Damaged";
                    else if (license.IssueReason == 4) lblIsseueReason.Text = "Replacement For Lost";
                }
            }
        }


        private void lblIsActive_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
