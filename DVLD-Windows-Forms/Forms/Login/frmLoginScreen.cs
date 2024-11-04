using DVLD_Business_Logic_Tear;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_Windows_Forms
{
    public partial class frmLoginScreen : Form
    {
        private clsUser _User;
        public bool IsLoggedIn = false;
        public frmLoginScreen(string userName="", string password="")
        {
            _User = new clsUser();
            InitializeComponent();
            tbUserName.Text = userName;
            tbPassword.Text = password;
            if (userName != null && userName.Trim() != "") cbRememberMe.Checked = true;
        }

        private void frmLoginScreen1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _User = clsUser.CheckLogin(tbUserName.Text, tbPassword.Text);
            if (_User != null)
            {
                if (_User.IsActive) 
                {
                    IsLoggedIn = true;
                    clsCurrentUser.CurrentUser = _User;
                    cbRememberMe_CheckedChanged(sender, e);
                    this.Close();
                } 

                else MessageBox.Show("Your account is deactivated please contact admin",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Invalid User Name or Password", "Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void btnCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            string filePath = @"D:\My Code And Projects\Programming Advices Roadmap\course 19\DVLD-Solution\DVLD-Windows-Forms\RememberMe.txt";
            if (cbRememberMe.Checked)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    if (_User.PersonID != -1)
                    {
                        writer.WriteLine($"{_User.UserID},{_User.PersonID},{_User.UserName},{_User.Password},{_User.IsActive}");
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"");
                }
            }
        }
    }
}
