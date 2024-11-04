using DVLD_Business_Logic_Tear;
using DVLD_Windows_Forms.Forms.DetainLicenses;
using DVLD_Windows_Forms.Forms.Drivers;
using DVLD_Windows_Forms.Forms.InternationalDrivingLicense;
using DVLD_Windows_Forms.Forms.LicenseReplacement;
using DVLD_Windows_Forms.Forms.RenewLocalDrivingLicense;
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

namespace DVLD_Windows_Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnMenuPeople_Click(object sender, EventArgs e)
        {
            frmManagePeople form = new frmManagePeople();
            form.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnMenuAccountSettings_Click(object sender, EventArgs e)
        {

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmUserInfo(clsCurrentUser.CurrentUser.UserID).ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnMenuUsers_Click(object sender, EventArgs e)
        {
            new frmManageUsers().ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmChangePassword(clsCurrentUser.CurrentUser.UserID).ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmManageApplicationTypes().ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmManageTestTypes().ShowDialog();
        }

        private void localDrivingLicenceApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmManageLocalDrivingLicences().ShowDialog();
        }

        private void localLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmNewLocalDrivingLicenceApplication().ShowDialog();
        }

        private void btnMenuDrivers_Click(object sender, EventArgs e)
        {
            new frmManageDrivers().ShowDialog();
        }

        private void internationalLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmInternationalDrivingLicense().ShowDialog();
        }

        private void internationalLicenceApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmManageInternationalLicenses().ShowDialog();
        }

        private void renewDrivingLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmRenewLocalDrivingLicense().ShowDialog();
        }

        private void replacementForLostOrDamagedLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmReplaceMentForDamagedOrLost().ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmManageDetainedLicenses().ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmDetainLicense().ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmReleaseDetainedLicense().ShowDialog();
        }

        private void releaseDetainedDrivingLicencToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmReleaseDetainedLicense().ShowDialog();
        }
    }
}
