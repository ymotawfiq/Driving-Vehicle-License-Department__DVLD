using DVLD_Business_Logic_Tear;
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
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
        }


        public enum enFilter
        {
            None,
            UserID,
            PersonID,
            Name,
            UserName,
            Active,
            NotActive
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("UserID");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("Name");
            cbFilter.Items.Add("UserName");
            cbFilter.Items.Add("Active");
            cbFilter.Items.Add("NotActive");
            cbFilter.SelectedIndex = 0;
            _FilterOnNone();
            _UpdateTotalRecordsNumber();
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void _UpdateTotalRecordsNumber()
        {
            lblTotalRecords.Text = dgvUsers.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "Active") _FilterByActiveUsers();
            else if (cbFilter.SelectedItem.ToString() == "NotActive") _FilterByNotActiveUsers();
            _UpdateTotalRecordsNumber();

            if (cbFilter.SelectedItem.ToString() == "ID") tbSearch.Text = "";
            
            
            if (cbFilter.SelectedItem.ToString() == "None" || cbFilter.SelectedItem.ToString() == "Active"
                || cbFilter.SelectedItem.ToString() == "NotActive")
            {
                tbSearch.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                tbSearch.Enabled = true;
                btnSearch.Enabled = true;
            }
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "UserID" || cbFilter.SelectedItem.ToString() == "PersonID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        #region Filters
        private void _FilterByPersonID()
        {
            dgvUsers.DataSource = clsUser.FindUsersByPersonID(Convert.ToInt32(tbSearch.Text));
        }
        private void _FilterByUserID()
        {
            dgvUsers.DataSource = clsUser.FindUserByID(Convert.ToInt32(tbSearch.Text));
        }
        private void _FilterByName()
        {
            dgvUsers.DataSource = clsUser.GetAllUsersByName(tbSearch.Text);
        }
        private void _FilterByUserName()
        {
            dgvUsers.DataSource = clsUser.GetAllUsersByUserName(tbSearch.Text);
        }
        private void _FilterByActiveUsers()
        {
            dgvUsers.DataSource = clsUser.GetAllActiveUsers();
        }
        private void _FilterByNotActiveUsers()
        {
            dgvUsers.DataSource = clsUser.GetAllNonActiveUsers();
        }
        private void _FilterOnNone()
        {
            dgvUsers.DataSource = clsUser.GetAllUsers();
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == "None") _FilterOnNone();
            else if (cbFilter.SelectedItem.ToString() == "UserID") _FilterByUserID();
            else if (cbFilter.SelectedItem.ToString() == "PersonID") _FilterByPersonID();
            else if (cbFilter.SelectedItem.ToString() == "Name") _FilterByName();
            else if (cbFilter.SelectedItem.ToString() == "UserName") _FilterByUserName();
            _UpdateTotalRecordsNumber();
        }

        private void _RefreshUsersList()
        {
            dgvUsers.DataSource = clsUser.GetAllUsers();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            new frmAddEditUsers().ShowDialog();
            _RefreshUsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value; 
            new frmAddEditUsers(userID).ShowDialog();
            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if(MessageBox.Show("Are you sure you want to delete?", "Deletion", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information) == DialogResult.OK)
            {
                try
                {
                    clsUser.DeleteByID(userID);
                    MessageBox.Show("User deleted successfully", "Deleted",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to delete because it's in use", "Failture",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAddEditUsers().ShowDialog();
            _RefreshUsersList();
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if (clsUser.ExistsByID(userID)) new frmUserInfo(userID).ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            new frmChangePassword(userID).ShowDialog();
        }
    }
}
