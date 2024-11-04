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
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople1_Load(object sender, EventArgs e)
        {
            lblTotalRecords.Text = $"#Total Records: {clsPerson.Count()}";
        }

        private void ctrlListUsersControl1_OnPeopleCountChange(int obj)
        {
            lblTotalRecords.Text = $"#Total Records: {obj}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
