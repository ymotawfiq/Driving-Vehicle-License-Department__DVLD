using DVLD_Business_Logic_Tear;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        clsUser _User;
        clsPerson _Person;
        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _UserID = userID;
            _User = clsUser.FindByID(userID);
            _Person = clsPerson.FindByID(_User.PersonID);
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            if (_User.IsActive) lblIsActive.Text = "True";
            else lblIsActive.Text = "False";
            ctrlPersonInfo1.LoadPersonWithID(_Person.PersonID);
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmAddEditPersons(_Person.PersonID).ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(clsUser.UpdateUser(_User.UserName, _User.Password, _User.IsActive, _User.UserID))
            {
                MessageBox.Show("Password updated successfullt", "Success", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbCurrentPassword.Text.Trim() == ""
                || tbCurrentPassword.Text != _User.Password)
            {
                e.Cancel = true;
                tbCurrentPassword.Focus();
                errorProvider1.SetError(tbCurrentPassword, "Incorrect password");
            }
        }

        private void tbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbNewPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                tbNewPassword.Focus();
                errorProvider1.SetError(tbNewPassword, "Invalid password");
            }
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(tbNewPassword.Text != tbConfirmPassword.Text)
            {
                e.Cancel = true;
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "Not match password");
            }
        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbNewPassword.Text == tbConfirmPassword.Text)
            {
                btnSave.Enabled = true;
                _User.Password = tbNewPassword.Text;
            }
        }
    }
}
