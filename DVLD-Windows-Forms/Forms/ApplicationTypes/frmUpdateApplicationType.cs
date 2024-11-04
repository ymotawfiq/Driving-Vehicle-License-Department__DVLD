using DVLD_Business_Logic_Tear;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class frmUpdateApplicationType : Form
    {
        clsApplicationTypes _ApplicationType;
        public frmUpdateApplicationType(int applicatyionTypeID)
        {
            InitializeComponent();
            if (applicatyionTypeID != -1) _ApplicationType = clsApplicationTypes.FindByID(applicatyionTypeID);
        }

        private void _FillUpdateForm()
        {
            lblID.Text = _ApplicationType.ApplicationTypeID.ToString();
            tbTitle.Text = _ApplicationType.ApplicationTypeTitle;
            tbFees.Text = _ApplicationType.ApplicationFees.ToString();
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _FillUpdateForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(clsApplicationTypes.UpdateApplicationType(_ApplicationType.ApplicationTypeID, 
                _ApplicationType.ApplicationTypeTitle, _ApplicationType.ApplicationFees))
            {
                MessageBox.Show("Application Type Updated Successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
            MessageBox.Show("Failed to updated Application type", "failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            _ApplicationType.ApplicationTypeTitle = tbTitle.Text;
        }

        private void tbFees_TextChanged(object sender, EventArgs e)
        {
            _ApplicationType.ApplicationFees = Convert.ToDouble(tbFees.Text);
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
