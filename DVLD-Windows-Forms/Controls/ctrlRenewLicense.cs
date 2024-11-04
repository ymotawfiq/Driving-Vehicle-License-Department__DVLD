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
    public partial class ctrlRenewLicense : UserControl
    {
        public ctrlRenewLicense()
        {
            InitializeComponent();
        }

        public event Action<string> OnNotesChanged;
        protected virtual void PersonIDChanged(string notes)
        {
            Action<string> handler = OnNotesChanged;
            if (handler != null)
            {
                handler(notes);
            }
        }

        clsApplication _Application;
        clsLocalDrivingLicence.stApplicationBasicInfoCtrl _stApplication;
        public void LoadControl(int ldlAppID)
        {
            _stApplication = clsLocalDrivingLicence.FindApplicationInfoForCtrlBasicInfo(ldlAppID);
            if (_stApplication.LocalDrivingLicenseApplicationID == -1)
                return;
            lblFees.Text = _stApplication.PaidFees.ToString();
            lblID.Text = _stApplication.LocalDrivingLicenseApplicationID.ToString();
            lblType.Text = _stApplication.ApplicationType.ToString();
            lblApplicant.Text = _stApplication.ApplicationPersonName.ToString();
            lblType.Text = _stApplication.ApplicationType;
            lblStatusDate.Text = _stApplication.LastStatusDate.ToShortDateString();
            lblDate.Text = _stApplication.ApplicationDate.ToShortDateString();
            lblCreatedBy.Text = _stApplication.CreatedByUserName;
            llPersonInfo.Visible = true;
            //lblNotValueApplicationID.Enabled = lblNotValueILID.Enabled = false;
            lblNotValueNotes.Visible = tbNotes.Visible = false;
            double licenseFees = clsLicenceClasses.FindLicenceClassByID(
                clsLocalDrivingLicence.FindByID(ldlAppID).LicenseClassID).ClassFees;
            lblLicenseFees.Text = licenseFees.ToString();
            lblTotalFees.Text = (_stApplication.PaidFees + licenseFees).ToString();
        }

        public void LoadControlByLicenseIDForRenewDrivingLicense(int licenseID, int appID=-1,
            int newLicenseID=-1)
        {
            if (clsLicense.ExistsByID(licenseID))
            {
                int ldlAppID = clsLocalDrivingLicence.GetLDLAppIDByLicenseID(licenseID);
                if (ldlAppID != -1)
                {
                    LoadControl(ldlAppID);
                    lblFees.Text = clsApplicationTypes.FindByID(2).ApplicationFees.ToString();
                    lblNotValueNotes.Visible = true;
                    tbNotes.Visible = true;
                }
                if (appID != -1) lblApplicationID1.Text = appID.ToString();
                if (newLicenseID != -1) lblNewLicenseID.Text = newLicenseID.ToString();
                if(tbNotes.Text != "" && tbNotes.Text != null)
                    if (OnNotesChanged != null) OnNotesChanged(tbNotes.Text);
            }
        }

        private void ctrlRenewLicense_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
