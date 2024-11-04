using DVLD_Business_Logic_Tear;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dgvApplicationsTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvApplicationsTypes.DataSource = clsApplicationTypes.GetAllApplicationTypes();
            _UpdateTotalRecordsCount();
        }
    
        private void _UpdateTotalRecordsCount()
        {
            lblTotalRecords.Text = dgvApplicationsTypes.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void _RefreshApplicationTypes()
        {
            dgvApplicationsTypes.DataSource = clsTestTypes.GetAllTestTypes();
        }


        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int applicationTypeID = (int)dgvApplicationsTypes.CurrentRow.Cells[0].Value;
            new frmUpdateApplicationType(applicationTypeID).ShowDialog();
            _RefreshApplicationTypes();
        }
    }
}
