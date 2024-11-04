using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Properties;
using System;

using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class frmPersonInfo : Form
    {
        private int _PersonID;
        private clsPerson _Person;
        public frmPersonInfo(int personID=-1)
        {
            InitializeComponent();
            _PersonID = personID;
            ctrlPersonInfo1.LoadPersonWithID(_PersonID);
        }

        private void frmPersonInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersons form = new frmAddEditPersons(_PersonID);
            form.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
