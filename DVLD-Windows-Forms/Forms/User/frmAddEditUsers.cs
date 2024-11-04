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

namespace DVLD_Windows_Forms
{
    public partial class frmAddEditUsers : Form
    {
        private int _PersonID;
        private clsUser _User { get; set; }
        private enum enMode { AddNew, Update}
        private enMode _Mode;
        public frmAddEditUsers(int userID=-1)
        {
            InitializeComponent();
            if (userID == -1)
            {
                _PersonID = -1;
                _User = new clsUser();
                _Mode = enMode.AddNew;
            }
            else
            {
                _User = clsUser.FindByID(userID);
                _Mode = enMode.Update;
                _User.Mode = clsUser.enMode.Update;
                _FillUpdate();
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void frmAddEditUsers_Load(object sender, EventArgs e)
        {
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                btnSave.Enabled = false;
                btnLoginInfo.Enabled = false;
                panel1.BringToFront();
                panel2.SendToBack();
            }
            else
            {
                _FillUpdate();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (clsUser.ExistsByPersonID(_PersonID))
            {
                MessageBox.Show("This person is already user", "Error", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
            }
            else if(_PersonID != -1 && !clsUser.ExistsByPersonID(_PersonID))
            {
                btnLoginInfo.Enabled = true;
                btnLoginInfo_Click(sender, e);
            }
        }

        private void ctrlPersonInformationWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            _User.PersonID = obj;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPersonInfo_Click(object sender, EventArgs e)
        {
            panel2.SendToBack();
            panel1.BringToFront();
            btnPersonInfo.BringToFront();
            btnLoginInfo.SendToBack();
        }

        private void btnLoginInfo_Click(object sender, EventArgs e)
        {
            panel1.SendToBack();
            panel2.BringToFront();
            btnPersonInfo.SendToBack();
            btnLoginInfo.BringToFront();
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (tbUserName.Text.Trim() == "" || clsUser.ExistsByUserName(tbUserName.Text))
            {
                e.Cancel = true;
                tbUserName.Focus();
                errorProvider1.SetError(tbUserName, "Invalid User Name");
            }
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbPassword.Text.Trim() == "")
            {
                e.Cancel = true;
                tbPassword.Focus();
                errorProvider1.SetError(tbPassword, "Password should not be blank");
            }
        }
        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbPassword.Text != tbConfirmPassword.Text)
            {
                e.Cancel = true;
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "Password doesn't match");
            }
            
        }


        private void _FillUserWithData()
        {
            _User.UserName = tbUserName.Text;
            _User.Password = tbPassword.Text;
            _User.IsActive = cbIsActive.Checked;
        }

        private void _FillUpdate()
        {
            lblTitle.Text = $"Update User";
            lblUserID.Text = _User.UserID.ToString();
            tbUserName.Text = _User.UserName;
            tbPassword.Text = _User.Password;
            tbConfirmPassword.Text = _User.Password;
            ctrlPersonInformationWithFilter1.LoadPersonWithID(_User.PersonID);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _FillUserWithData();
            if (_User.Save())
            {
                if(_Mode == enMode.AddNew)
                {
                    MessageBox.Show("User Saved Successfully", "Added", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information);
                    _Mode = enMode.Update;
                }
                else
                {
                    MessageBox.Show("User Updated Successfully", "Updated", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information);
                }
                _FillUpdate();
            }
        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if(tbConfirmPassword.Text == tbPassword.Text)
            {
                btnSave.Enabled = true;
            }
        }

        private void lblEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmAddEditPersons(_User.PersonID).ShowDialog();
        }
    }
}
