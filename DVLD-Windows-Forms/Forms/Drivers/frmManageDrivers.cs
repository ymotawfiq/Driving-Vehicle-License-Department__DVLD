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

namespace DVLD_Windows_Forms.Forms.Drivers
{
    public partial class frmManageDrivers : Form
    {
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private enum enFilter
        {
            None, DriverID, PersonID, NationalNo, FullName
        }

        private void _RefreshDrivers()
        {
            dgvDrivers.DataSource = clsDriver.GetAllDrivers();
            _UpdateDriversCount();
        }

        private void _UpdateDriversCount()
        {
            lblRecords.Text = dgvDrivers.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }


        private void _FillCmomboBoxFilter()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("DriverID");
            cbFilter.Items.Add("PersonID");
            cbFilter.Items.Add("NationalNo");
            cbFilter.Items.Add("FullName");
            cbFilter.SelectedIndex = 0;
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            dgvDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _RefreshDrivers();
            _FillCmomboBoxFilter();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Filters
        private void _FilterByPersonID()
        {
            dgvDrivers.DataSource = clsDriver.GetAllDriversByPersonID(Convert.ToInt32(tbSearch.Text));
            _UpdateDriversCount();
        }
        private void _FilterByDriverID()
        {
            dgvDrivers.DataSource = clsDriver.GetAllDriversByID(Convert.ToInt32(tbSearch.Text));
            _UpdateDriversCount();
        }
        private void _FilterByFullName()
        {
            dgvDrivers.DataSource = clsDriver.GetAllDriversByFullName(tbSearch.Text);
            _UpdateDriversCount();
        }
        private void _FilterByNationalNo()
        {
            dgvDrivers.DataSource = clsDriver.GetAllDriversByNationalNo(tbSearch.Text);
            _UpdateDriversCount();
        }
        private void _FilterOnNone()
        {
            _RefreshDrivers();
        }
        #endregion

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedItem.ToString() == enFilter.None.ToString())
            {
                tbSearch.Enabled = false;
            }
            else
            {
                tbSearch.Enabled = true;
            }
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == enFilter.DriverID.ToString()
                || cbFilter.SelectedItem.ToString() == enFilter.PersonID.ToString())
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedItem.ToString() == enFilter.DriverID.ToString()) _FilterByDriverID();
            else if (cbFilter.SelectedItem.ToString() == enFilter.PersonID.ToString()) _FilterByPersonID();
            else if (cbFilter.SelectedItem.ToString() == enFilter.FullName.ToString()) _FilterByFullName();
            else if (cbFilter.SelectedItem.ToString() == enFilter.NationalNo.ToString()) _FilterByNationalNo();
            else _FilterOnNone();
        }
    }
}
