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
    public partial class ctrlListUsersControl : UserControl
    {
        enum enFilter 
        { 
            NationalNo,
            ID,
            Name,
            TopNameWith,
            Phone,
            Email,
            FirstName,
            SecondName,
            ThirdName,
            LastName,
            None,
            Nationality,
            Gender
        }

        public event Action<int> OnPeopleCountChange;

        protected virtual void PeopleCountChange(int count)
        {
            Action<int> handler = OnPeopleCountChange;
            if (handler != null)
            {
                handler(count);
            }
        }

        enFilter _filter;
        public ctrlListUsersControl()
        {
            InitializeComponent();
        }

        private void _RefreshPersonsList()
        {
            dgvListUsers.DataSource = clsPerson.GetAllPersons();
        }

        private void clsListUsersControl_Load(object sender, EventArgs e)
        {
            cbFilter.Items.Add(enFilter.None);
            cbFilter.Items.Add(enFilter.ID);
            cbFilter.Items.Add(enFilter.NationalNo);
            cbFilter.Items.Add(enFilter.Nationality);
            cbFilter.Items.Add(enFilter.Gender);
            cbFilter.Items.Add(enFilter.Phone);
            cbFilter.Items.Add(enFilter.Email);
            cbFilter.Items.Add(enFilter.TopNameWith);
            cbFilter.Items.Add(enFilter.Name);
            cbFilter.Items.Add(enFilter.FirstName);
            cbFilter.Items.Add(enFilter.SecondName);
            cbFilter.Items.Add(enFilter.ThirdName);
            cbFilter.Items.Add(enFilter.LastName);
            
            cbFilter.SelectedIndex = 0;
            _RefreshPersonsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            if (clsPerson.ExistsByID(PersonID))
            {
                frmAddEditPersons form = new frmAddEditPersons(PersonID);
                form.ShowDialog();
                _RefreshPersonsList();
            }
            else
            {
                return;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            if (clsPerson.ExistsByID(PersonID))
            {
                if(MessageBox.Show("Are you sure you want to delete?", "Delation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    clsPerson.DeletePerson(PersonID);
                    if (OnPeopleCountChange != null)
                    {
                        OnPeopleCountChange(dgvListUsers.Rows.GetRowCount(DataGridViewElementStates.Visible));
                    }
                    MessageBox.Show("Person Deleted Successfully", "Delation", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.None);
                }
            }
            
            _RefreshPersonsList();
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            
            new frmPersonInfo(PersonID).ShowDialog();
        }

        #region Filter
        void _FilterByID()
        {
            if (clsPerson.ExistsByID(Convert.ToInt32(tbSearch.Text)))
            {
                dgvListUsers.DataSource = clsPerson.FindTopUserByID(Convert.ToInt32(tbSearch.Text));
            }
        }
        void _FilterByNationalNo()
        {
            if (clsPerson.ExistsByNationalNo(tbSearch.Text))
            {
                dgvListUsers.DataSource = clsPerson.FindTopUserByNationalNo(tbSearch.Text);
            }
        }
        void _FilterByPhone()
        {
            if (clsPerson.ExistsByPhone(tbSearch.Text))
            {
                dgvListUsers.DataSource = clsPerson.FindTopUserByPhone(tbSearch.Text);
            }
        }
        void _FilterByEmail()
        {
            if (clsPerson.ExistsByEmailForFilter(tbSearch.Text))
            {
                dgvListUsers.DataSource = clsPerson.FindAllPersonsByEmail(tbSearch.Text);
            }
        }
        void _FilterByTopNameWith()
        {
            if (clsPerson.FindFirstPersonByName(tbSearch.Text) != null)
            {
                dgvListUsers.DataSource = clsPerson.FindTopUserByName(tbSearch.Text);
            }
        }
        void _FilterByName()
        {
            if (clsPerson.FindAllPersonsByName(tbSearch.Text) != null)
            {
                dgvListUsers.DataSource = clsPerson.FindAllPersonsByName(tbSearch.Text);
            }
        }
        void _FilterByFirstName()
        {
            if (clsPerson.FindAllPersonsByFirstName(tbSearch.Text) != null)
            {
                dgvListUsers.DataSource = clsPerson.FindAllPersonsByFirstName(tbSearch.Text);
            }
        }
        void _FilterBySecondName()
        {
            if (clsPerson.FindAllPersonsBySecondName(tbSearch.Text) != null)
            {
                dgvListUsers.DataSource = clsPerson.FindAllPersonsBySecondName(tbSearch.Text);
            }
        }
        void _FilterByThirdName()
        {
            if (clsPerson.FindAllPersonsByThirdName(tbSearch.Text) != null)
            {
                dgvListUsers.DataSource = clsPerson.FindAllPersonsByThirdName(tbSearch.Text);
            }
        }
        void _FilterByLastName()
        {
            if (clsPerson.FindAllPersonsByLastName(tbSearch.Text) != null)
            {
                dgvListUsers.DataSource = clsPerson.FindAllPersonsByLastName(tbSearch.Text);
            }
        }
        void _FilterByNationality()
        {
            if (clsCountry.ExistsByCountryNameForFilter(tbSearch.Text))
            {
                dgvListUsers.DataSource = clsPerson.FindAllPersonsByNationality(tbSearch.Text);
            }
        }
        void _FilterByGender()
        {
            dgvListUsers.DataSource = clsPerson.FindAllPersonsByGender(Convert.ToInt32(cbGender.SelectedIndex));
        }
        #endregion
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text.Trim() != "")
            {
                if (enFilter.Email.ToString() == cbFilter.SelectedItem.ToString()) _FilterByEmail();

                else if (enFilter.ID.ToString() == cbFilter.SelectedItem.ToString()) _FilterByID();

                else if (enFilter.Name.ToString() == cbFilter.Text.ToString()) _FilterByName();

                else if (enFilter.NationalNo.ToString() == cbFilter.Text.ToString()) _FilterByNationalNo();

                else if (enFilter.Phone.ToString() == cbFilter.Text.ToString()) _FilterByPhone();

                else if (enFilter.TopNameWith.ToString() == cbFilter.Text.ToString()) _FilterByTopNameWith();

                else if (enFilter.FirstName.ToString() == cbFilter.Text.ToString()) _FilterByFirstName();

                else if (enFilter.SecondName.ToString() == cbFilter.Text.ToString()) _FilterBySecondName();

                else if (enFilter.ThirdName.ToString() == cbFilter.Text.ToString()) _FilterByThirdName();

                else if (enFilter.LastName.ToString() == cbFilter.Text.ToString()) _FilterByLastName();

                else if (enFilter.Nationality.ToString() == cbFilter.Text.ToString()) _FilterByNationality();

                else if (enFilter.Gender.ToString() == cbFilter.Text.ToString()) _FilterByGender();

                else
                    MessageBox.Show("Person not found", "Error", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Error);
                if (OnPeopleCountChange != null)
                {
                    OnPeopleCountChange(dgvListUsers.Rows.GetRowCount(DataGridViewElementStates.Visible));
                }
            }
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditPersons form = new frmAddEditPersons(-1);
            form.ShowDialog();
            _RefreshPersonsList();
            if (OnPeopleCountChange != null)
            {
                OnPeopleCountChange(dgvListUsers.Rows.GetRowCount(DataGridViewElementStates.Visible));
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
            if (OnPeopleCountChange != null)
            {
                OnPeopleCountChange(dgvListUsers.Rows.GetRowCount(DataGridViewElementStates.Visible));
            }
        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }

        private void addPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPersons form = new frmAddEditPersons(-1);
            form.ShowDialog();
            _RefreshPersonsList();
            if (OnPeopleCountChange != null)
            {
                OnPeopleCountChange(dgvListUsers.Rows.GetRowCount(DataGridViewElementStates.Visible));
            }
        }


        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnPeopleCountChange != null)
            {
                OnPeopleCountChange(dgvListUsers.Rows.GetRowCount(DataGridViewElementStates.Visible));
            }

            if (cbFilter.SelectedItem.ToString() == "ID") tbSearch.Text = "";
            
            if (cbFilter.SelectedItem.ToString() == "Gender")
            {
                tbSearch.Visible = false;
                cbGender.Items.Add("Male");
                cbGender.Items.Add("Female");
                cbGender.SelectedIndex = 0;
                cbGender.Visible = true;
                btnSearch.Enabled = true;
            }
            else if (cbFilter.SelectedItem.ToString() == "None")
            {
                tbSearch.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                tbSearch.Enabled = true;
                btnSearch.Enabled = true;
                cbGender.Visible = false;
            }
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.SelectedItem.ToString() == "ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgvListUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterByGender();
        }
    }
}
