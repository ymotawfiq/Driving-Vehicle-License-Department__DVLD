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
    public partial class ctrlLoginInfo : UserControl
    {
        public int UserID { get; set; } = -1;
        private clsUser _User;
        public ctrlLoginInfo()
        {
            InitializeComponent();
        }

        private void ctrlLoginInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void LoadUserLoginInfo(int userID)
        {
            if (userID == -1) _User = new clsUser();
            else _User = clsUser.FindByID(userID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            if (_User.IsActive) lblIsActive.Text = "True";
            else lblIsActive.Text = "False";
        }

    }
}
