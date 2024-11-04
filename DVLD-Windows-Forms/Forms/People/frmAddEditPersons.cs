using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Properties;
using DVLD_Windows_Forms.Validation;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class frmAddEditPersons : Form
    {
        private string PATH = @"D:\My Code And Projects\Programming Advices Roadmap\course 19\DVLD-Solution\Images\PersonImages\";
        private struct stImagePath
        {
            public string oldImagePath;
            public string newImagePath;
        }
        stImagePath _stImagePath = new stImagePath();
        
        private enum enMode { AddNew, Update}
        enMode _Mode;

        private clsPerson _Person;
        public frmAddEditPersons(int personID=-1)
        {
            InitializeComponent();
            if (personID != -1)
            {
                _Person = clsPerson.FindByID(personID);
                _Mode = enMode.Update;
            }
            else
            {
                _Person = new clsPerson();
                _stImagePath.oldImagePath = _stImagePath.newImagePath = "";
                _Person.Mode = clsPerson.enMode.AddNew;
                _Mode = enMode.AddNew;
            }
        }

        private void _FillCountriesInComboBox()
        {
            foreach (DataRow row in clsCountry.GetAllCountries().Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
            if(_Person.PersonID != -1)
                cbCountry.SelectedIndex = _Person.NationalityCountryID - 1;
            else
                cbCountry.SelectedItem = "Egypt";
        }

        private void _OnControlLoading()
        {
            if (_Person.Mode == clsPerson.enMode.AddNew)
            {
                _FillFormInitialy();
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
                llRemoveImage.Visible = false;
            }
            else _FillUpdateMode();
        }

        private void _FillFormInitialy()
        {
            _FillCountriesInComboBox();
            llRemoveImage.Visible = false;
            dtpDateOfBirth.MaxDate = DateTime.Now.AddDays(-18 * 365 - 5);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
        }

        private void frmAddEditPersons_Load(object sender, EventArgs e)
        {
            _OnControlLoading();
        }

        private short _GetGender()
        {
            if (rbFemale.Checked)
            {
                return 1;
            }
            return 0;
        }


        #region OnSaveButtonClickValidation
        private bool _HandleFirstNameInput()
        {
            if (!clsAddEditUserControlValidation.ValidateName(tbFirstName.Text))
            {
                tbFirstName.Focus();
                MessageBox.Show("First Name shoud be atleast 3 chars and not contain spechial chars(@#$%^&)",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandleSecondNameInput()
        {
            if (!clsAddEditUserControlValidation.ValidateName(tbSecondName.Text))
            {
                tbSecondName.Focus();
                MessageBox.Show("Second Name shoud be atleast 3 chars and not contain spechial chars(@#$%^&)",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandleLastNameInput()
        {
            if (!clsAddEditUserControlValidation.ValidateName(tbLastName.Text))
            {
                tbLastName.Focus();
                MessageBox.Show("Last Name shoud be atleast 3 chars and not contain spechial chars(@#$%^&)",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandleNationalNoInput()
        {
            if (!clsAddEditUserControlValidation.ValidateNationalNo(tbNationalNo.Text, _Person.PersonID))
            {
                tbNationalNo.Focus();
                if (!clsPerson.ExistsByNationalNoForPerson(tbNationalNo.Text, _Person.PersonID)
                    && clsPerson.ExistsByNationalNo(tbNationalNo.Text))
                {
                    MessageBox.Show("National number already exists",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                if (tbNationalNo.Text.Trim() == "")
                {
                    MessageBox.Show("National number should not be empty",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                MessageBox.Show("National Number should be less than 20 chars and not contain spechial chars(@#$%^&)",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandlePhoneInput()
        {
            if (!clsAddEditUserControlValidation.ValidatePhone(tbPhone.Text, _Person.PersonID))
            {
                tbPhone.Focus();
                if (!clsPerson.ExistsByPhoneForPerson(tbPhone.Text, _Person.PersonID)
                    && clsPerson.ExistsByPhone(tbPhone.Text))
                {
                    MessageBox.Show("Phone already exists",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                if (tbPhone.Text.Trim() == "")
                {
                    MessageBox.Show("Phone should not be empty",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                MessageBox.Show("Phone should be less than 20 chars and contain only numbers(0123456789)",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandleEmailInput()
        {
            if (!clsAddEditUserControlValidation.ValidateEmail(tbEmail.Text, _Person.PersonID))
            {
                tbEmail.Focus();
                if (!clsPerson.ExistsByEmailForPerson(tbEmail.Text, _Person.PersonID)
                    && clsPerson.ExistsByEmail(tbEmail.Text))
                {
                    MessageBox.Show("Email already exists",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return false;
                }
                MessageBox.Show("Invalid Email",
                    "Validation Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandleGenderInput()
        {
            if (rbFemale.Checked == false && rbMale.Checked == false)
            {
                MessageBox.Show("Choose gender", "Validation Error", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _HandleAddressInput()
        {
            if (tbAddress.Text.Trim().Length > 500)
            {
                MessageBox.Show("Too big address, it should be less than 500 chars", "Validation Error",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            if (tbAddress.Text.Trim() == "")
            {
                MessageBox.Show("Address should not be empty", "Validation Error",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _ValidateControlInput()
        {
            if (!_HandleFirstNameInput() || !_HandleSecondNameInput() || !_HandleLastNameInput()
                || !_HandleNationalNoInput() || !_HandlePhoneInput()
                || !_HandleGenderInput() || !_HandleEmailInput()
                || !_HandleAddressInput()) return false;
            return true;
        }

        #endregion

        #region Handle Images
        private string _GetNewImagePathToSave()
        {
            string newPath = $"{PATH}";
            string[] arr = _stImagePath.newImagePath.Split('.');
            newPath += $"{Guid.NewGuid()}.{arr[arr.Length - 1]}";
            return newPath;
        }

        private void _SaveImageToFolder(string newPath)
        {
            if (!Directory.Exists(PATH)) Directory.CreateDirectory(PATH);
            if (File.Exists(_stImagePath.newImagePath)) 
                File.Copy(_stImagePath.newImagePath, newPath);
        }

        private bool _DeleteImageFromFolder(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                Image.FromFile(imagePath).Dispose();
                File.Delete(imagePath);
                return true;
            }
            return false;
        }

        #endregion
        private bool _FillPersonWithDataToAddNewPerson()
        {
            if (!_ValidateControlInput())
            {
                return false;
            }
            _Person.FirstName = tbFirstName.Text;
            _Person.SecondName = tbSecondName.Text;
            _Person.ThirdName = tbThirdName.Text;
            _Person.LastName = tbLastName.Text;
            _Person.Email = tbEmail.Text;
            _Person.Address = tbAddress.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gendor = _GetGender();
            _Person.Phone = tbPhone.Text;
            _Person.NationalNo = tbNationalNo.Text;
            _Person.NationalityCountryID = cbCountry.SelectedIndex;
            return true;
        }

        private bool _FillPersonWithDataToUpdatePerson()
        {
            if (!_ValidateControlInput())
            {
                return false;
            }
            _Person.FirstName = tbFirstName.Text;
            _Person.SecondName = tbSecondName.Text;
            _Person.ThirdName = tbThirdName.Text;
            _Person.LastName = tbLastName.Text;
            _Person.Email = tbEmail.Text;
            _Person.Address = tbAddress.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gendor = _GetGender();
            _Person.NationalityCountryID = cbCountry.SelectedIndex + 1;
            _Person.Phone = tbPhone.Text;
            _Person.NationalNo = tbNationalNo.Text;

            if (_Person.Gendor == 1) rbFemale.Checked = true;
            else rbMale.Checked = true;


            return true;
        }

        private void _FillUpdateMode()
        {
            _FillFormInitialy();
            _Person.Mode = clsPerson.enMode.Update;
            _stImagePath.oldImagePath = _Person.ImagePath;
            _stImagePath.newImagePath = _Person.ImagePath;
            _stImagePath.newImagePath = "";
            _Mode = enMode.Update;
            lblTitle.Text = $"Edit Person with id = {_Person.PersonID}";
            lblPersonID.Text = _Person.PersonID.ToString();
            tbFirstName.Text = _Person.FirstName;
            tbSecondName.Text = _Person.SecondName;
            tbThirdName.Text = _Person.ThirdName;
            tbLastName.Text = _Person.LastName;
            tbEmail.Text = _Person.Email;
            tbAddress.Text = _Person.Address;
            tbNationalNo.Text = _Person.NationalNo;
            tbPhone.Text = _Person.Phone;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            _stImagePath.oldImagePath = _Person.ImagePath;

            if (_Person.Gendor == 1) rbFemale.Checked = true;
            else rbMale.Checked = true;

            try
            {
                pbImage.Image = Image.FromFile($"{_Person.ImagePath}");
                llRemoveImage.Visible = true;
            }
            catch (Exception)
            {
                llRemoveImage.Visible = false;
                _stImagePath.newImagePath = _stImagePath.oldImagePath = "";
                if (_Person.Gendor == 1) pbImage.Image = Resources.femaleBigIcon;
                else if (_Person.Gendor == 0) pbImage.Image = Resources.maleBigIcon;
                else pbImage.Image = Resources.person;
            }
        }

        private bool UpdatePerson()
        {
            bool fillUpdate = false;
            bool success = false;
            fillUpdate = _FillPersonWithDataToUpdatePerson();
            try
            {
                if (_stImagePath.oldImagePath != _stImagePath.newImagePath)
                {
                    if(_stImagePath.newImagePath=="") _DeleteImageFromFolder(_stImagePath.oldImagePath);
                    if (_stImagePath.oldImagePath != "") _DeleteImageFromFolder(_stImagePath.oldImagePath);
                    if (_stImagePath.newImagePath != "") _SaveImageToFolder(_Person.ImagePath);
                }
                success = true;
            }
            catch(Exception)
            {
                MessageBox.Show("Failed to save", "Failed", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                success = false;
            }
            if (success)
            {
                _Person.Save();
                MessageBox.Show("Person updated successfully", "Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                this.Close();
            }
            return false;
        }

        private bool AddNewPerson()
        {
            bool fillAdd = false;
            fillAdd = _FillPersonWithDataToAddNewPerson();
            if (fillAdd && _Person.Save())
            {
                MessageBox.Show("Person saved successfully", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                if (_stImagePath.newImagePath != "") _SaveImageToFolder(_Person.ImagePath);
                _FillUpdateMode();
                _Mode = enMode.Update;
                return true;
            }
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew) AddNewPerson();
            else UpdatePerson();   
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdImagePath.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            ofdImagePath.FilterIndex = 1;
            ofdImagePath.RestoreDirectory = true;
            if (ofdImagePath.ShowDialog() == DialogResult.OK)
            {
                _stImagePath.newImagePath = ofdImagePath.FileName;
                _Person.ImagePath = _GetNewImagePathToSave();
                pbImage.Image = Image.FromFile(_stImagePath.newImagePath);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _stImagePath.newImagePath = "";
            if (pbImage.Image != null)
            {
                pbImage.Image.Dispose();
                pbImage.Image = null;
            }
            _Person.ImagePath = "";
            if (_Person.Gendor == 1) pbImage.Image = Resources.femaleBigIcon;
            else pbImage.Image = Resources.maleBigIcon;
            llRemoveImage.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        #region Validation

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (!clsAddEditUserControlValidation.ValidateName(tbFirstName.Text))
            {
                e.Cancel = true;
                tbFirstName.Focus();
                errorProvider.SetError(tbFirstName, "Invalid Name");
            }
        }

        private void tbSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (!clsAddEditUserControlValidation.ValidateName(tbSecondName.Text))
            {
                e.Cancel = true;
                tbSecondName.Focus();
                errorProvider.SetError(tbSecondName, "Invalid Name");
            }
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (!clsAddEditUserControlValidation.ValidateName(tbLastName.Text))
            {
                e.Cancel = true;
                tbLastName.Focus();
                errorProvider.SetError(tbLastName, "Invalid Name");
            }
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(!clsAddEditUserControlValidation.ValidateNationalNo(tbNationalNo.Text, _Person.PersonID))
            {
                e.Cancel = true;
                tbNationalNo.Focus();
                errorProvider.SetError(tbNationalNo, "Invalid National Number");
            }
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (!clsAddEditUserControlValidation.ValidatePhone(tbPhone.Text, _Person.PersonID))
            {
                e.Cancel = true;
                tbPhone.Focus();
                errorProvider.SetError(tbPhone, "Invalid Phone Number");
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pbImage.Image = Resources.maleBigIcon;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pbImage.Image = Resources.femaleBigIcon;
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (tbEmail.Text.Trim() != "")
            {
                if (!tbEmail.Text.Contains("@"))
                {
                    e.Cancel = true;
                    tbEmail.Focus();
                    errorProvider.SetError(tbPhone, "Invalid Email");
                }
                else
                {
                    try
                    {
                        MailAddress address = new MailAddress(tbEmail.Text);
                        if (address != null && address.Address != tbEmail.Text
                            || (!clsPerson.ExistsByEmailForPerson(tbEmail.Text, _Person.PersonID)
                        && clsPerson.ExistsByEmail(tbEmail.Text)))
                        {
                            e.Cancel = true;
                            tbEmail.Focus();
                            errorProvider.SetError(tbEmail, "Invalid Email");
                        }
                    }
                    catch (Exception)
                    {
                        errorProvider.SetError(tbEmail, "Invalid Email");
                    }
                }
                  
            }
            
        }

        private void tbAddress_Validating(object sender, CancelEventArgs e)
        {
            if (tbAddress.Text.Trim().Length > 500 || tbAddress.Text.Trim() == "")
            {
                e.Cancel = true;
                tbEmail.Focus();
                errorProvider.SetError(tbEmail, "Invalid Address");
            }
        }
        #endregion
    }
}
