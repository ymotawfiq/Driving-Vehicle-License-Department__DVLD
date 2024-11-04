using DVLD_Business_Logic_Tear;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    public partial class frmNewLocalDrivingLicenceApplication : Form
    {


        enum enApplicationType
        {
            New=1,Canceled=2,Completed=3
        }

        public frmNewLocalDrivingLicenceApplication()
        {
            InitializeComponent();
        }

        int _PersonID = -1;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillLicenceClasses()
        {
            foreach(DataRow row in clsLicenceClasses.GetAllLicenceClasses().Rows)
            {
                cbLicenceClasses.Items.Add(row["ClassName"]);
            }
            cbLicenceClasses.SelectedIndex = 2;
        }

        private void frmNewLocalDrivingLicenceApplication_Load(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            btnApplicationInfo.SendToBack();
            btnPersonalInfo.BringToFront();
            groupBox2.SendToBack();
            groupBox1.BringToFront();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            btnNext.Enabled = false;
            btnApplicationInfo.Enabled = false;
            _FillLicenceClasses();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            btnPersonalInfo.SendToBack();
            btnApplicationInfo.BringToFront();
            groupBox1.SendToBack();
            groupBox2.BringToFront();
        }

        private void ctrlPersonInformationWithFilter1_OnPersonSelected(int obj)
        {
            if (obj != -1)
            {
                btnNext.Enabled = true;
                btnApplicationInfo.Enabled = true;
                clsUser user = clsUser.FindByID(obj);
                lblCreatedBy.Text = clsCurrentUser.CurrentUser.UserName.ToString();
                btnSave.Enabled = true;
                lblApplicationFees.Text = clsApplicationTypes.FindByID(1).ApplicationFees.ToString();
                _PersonID = obj;
            }
            
        }

        private void cbLicenceClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationTypes.FindByID(1).ApplicationFees.ToString();
        }

        private clsApplication _AddNewApplication(int appTypeID)
        {
            clsApplication application = new clsApplication();
            application.Mode = clsApplication.enMode.AddNew;
            application.ApplicantPersonID = _PersonID;
            application.ApplicationStatus = (int)enApplicationType.New;
            application.ApplicationTypeID = appTypeID;
            application.PaidFees = clsApplicationTypes.FindByID(1).ApplicationFees;
            application.CreatedByUserID = clsCurrentUser.CurrentUser.UserID;
            if (application.Save())
            {
                return clsApplication.FindByID(application.ApplicationID);
            }
            return null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsApplication application = _AddNewApplication(1);
            if (application != null)
            {
                if (clsApplication.IsPersonHasCompletetOrActiveApplicationFromType(
                    _PersonID, 1, cbLicenceClasses.SelectedIndex+1))
                {
                    MessageBox.Show("You already applied for this licence and have active " +
                        "Application Or already has license", "Error",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    clsLocalDrivingLicence localDrivingLicence1 = clsLocalDrivingLicence
                    .AddNewLocalDrivingLicence(application.ApplicationID, 
                    cbLicenceClasses.SelectedIndex + 1);
                    MessageBox.Show("Local Driver Licence Added Successfully", "Success",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed To Add Local Driving Licence", "Error",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {
                clsApplication.DeleteApplication(application.ApplicationID);
                MessageBox.Show("Failed To Add Local Driving Licence", "Error",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }
    }
}
