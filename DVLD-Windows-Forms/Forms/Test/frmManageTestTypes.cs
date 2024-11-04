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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTestTypes.DataSource = clsTestTypes.GetAllTestTypes();
            _UpdateTotalRecordsCount();
        }

        private void _UpdateTotalRecordsCount()
        {
            lblTotalRecords.Text = dgvTestTypes.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void _RefreshTestTypes()
        {
            dgvTestTypes.DataSource = clsTestTypes.GetAllTestTypes();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int testTypeID = (int)dgvTestTypes.CurrentRow.Cells[0].Value;
            new frmUpdateTestTypes(testTypeID).ShowDialog();
            _RefreshTestTypes();
        }
    }
}
