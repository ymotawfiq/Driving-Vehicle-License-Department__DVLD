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
    public partial class frmUpdateTestTypes : Form
    {
        clsTestTypes _TestType;
        public frmUpdateTestTypes(int testTypeID)
        {
            InitializeComponent();
            _TestType = clsTestTypes.FindTestTypeByID(testTypeID);
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            lblID.Text = _TestType.TestTypeID.ToString();
            tbDescription.Text = _TestType.TestTypeDescription;
            tbFees.Text = _TestType.TestTypeFees.ToString();
            tbTitle.Text = _TestType.TestTypeTitle;
        }

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            _TestType.TestTypeTitle = tbTitle.Text;
        }

        private void tbFees_TextChanged(object sender, EventArgs e)
        {
            _TestType.TestTypeFees = Convert.ToDouble(tbFees.Text);
        }

        private void tbDescription_TextChanged(object sender, EventArgs e)
        {
            _TestType.TestTypeDescription = tbDescription.Text;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(clsTestTypes.UpdateTestType(_TestType.TestTypeID, _TestType.TestTypeTitle,
                _TestType.TestTypeDescription, _TestType.TestTypeFees))
            {
                MessageBox.Show("Test type updated successfully", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.Close();
                return;
            }
            MessageBox.Show("Test type Failed to update", "Failed", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
