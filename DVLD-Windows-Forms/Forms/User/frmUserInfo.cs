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
    public partial class frmUserInfo : Form
    {
        private clsUser _User;
        private clsPerson _Person;
        public frmUserInfo(int userID)
        {
            InitializeComponent();
            _User = clsUser.FindByID(userID);
            _Person = clsPerson.FindByID(_User.PersonID);
            ctrlPersonInfo1.LoadPersonWithID(_Person.PersonID);
            ctrlLoginInfo1.LoadUserLoginInfo(userID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            
        }

    }
}
