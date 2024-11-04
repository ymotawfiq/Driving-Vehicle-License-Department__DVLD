namespace DVLD_Windows_Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnMenuApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.drivingLicenceServiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrivingLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.replacementForLostOrDamagedLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.releaseDetainedDrivingLicencToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retakeTesrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLicenceApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenceApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detainLicenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageDetainedLicensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detainLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseDetainedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTestTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMenuPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMenuDrivers = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMenuUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMenuAccountSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.currentUserInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMenuApplications,
            this.btnMenuPeople,
            this.btnMenuDrivers,
            this.btnMenuUsers,
            this.btnMenuAccountSettings,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(954, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // btnMenuApplications
            // 
            this.btnMenuApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drivingLicenceServiceToolStripMenuItem,
            this.manageApplicationsToolStripMenuItem,
            this.detainLicenceToolStripMenuItem,
            this.manageApplicationTypesToolStripMenuItem,
            this.manageTestTypesToolStripMenuItem});
            this.btnMenuApplications.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuApplications.Image = global::DVLD_Windows_Forms.Properties.Resources.application;
            this.btnMenuApplications.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenuApplications.Name = "btnMenuApplications";
            this.btnMenuApplications.Size = new System.Drawing.Size(150, 29);
            this.btnMenuApplications.Text = "Applicattions";
            // 
            // drivingLicenceServiceToolStripMenuItem
            // 
            this.drivingLicenceServiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrivingLicenceToolStripMenuItem,
            this.renewDrivingLicenceToolStripMenuItem,
            this.toolStripMenuItem3,
            this.replacementForLostOrDamagedLicenceToolStripMenuItem,
            this.toolStripMenuItem4,
            this.releaseDetainedDrivingLicencToolStripMenuItem,
            this.retakeTesrToolStripMenuItem});
            this.drivingLicenceServiceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drivingLicenceServiceToolStripMenuItem.Name = "drivingLicenceServiceToolStripMenuItem";
            this.drivingLicenceServiceToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.drivingLicenceServiceToolStripMenuItem.Text = "Driving Licence Service";
            // 
            // newDrivingLicenceToolStripMenuItem
            // 
            this.newDrivingLicenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localLicenceToolStripMenuItem,
            this.internationalLicenceToolStripMenuItem});
            this.newDrivingLicenceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newDrivingLicenceToolStripMenuItem.Name = "newDrivingLicenceToolStripMenuItem";
            this.newDrivingLicenceToolStripMenuItem.Size = new System.Drawing.Size(372, 24);
            this.newDrivingLicenceToolStripMenuItem.Text = "New Driving Licence";
            // 
            // localLicenceToolStripMenuItem
            // 
            this.localLicenceToolStripMenuItem.Name = "localLicenceToolStripMenuItem";
            this.localLicenceToolStripMenuItem.Size = new System.Drawing.Size(224, 24);
            this.localLicenceToolStripMenuItem.Text = "Local Licence";
            this.localLicenceToolStripMenuItem.Click += new System.EventHandler(this.localLicenceToolStripMenuItem_Click);
            // 
            // internationalLicenceToolStripMenuItem
            // 
            this.internationalLicenceToolStripMenuItem.Name = "internationalLicenceToolStripMenuItem";
            this.internationalLicenceToolStripMenuItem.Size = new System.Drawing.Size(224, 24);
            this.internationalLicenceToolStripMenuItem.Text = "International Licence";
            this.internationalLicenceToolStripMenuItem.Click += new System.EventHandler(this.internationalLicenceToolStripMenuItem_Click);
            // 
            // renewDrivingLicenceToolStripMenuItem
            // 
            this.renewDrivingLicenceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renewDrivingLicenceToolStripMenuItem.Name = "renewDrivingLicenceToolStripMenuItem";
            this.renewDrivingLicenceToolStripMenuItem.Size = new System.Drawing.Size(372, 24);
            this.renewDrivingLicenceToolStripMenuItem.Text = "Renew Driving Licence";
            this.renewDrivingLicenceToolStripMenuItem.Click += new System.EventHandler(this.renewDrivingLicenceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(369, 6);
            // 
            // replacementForLostOrDamagedLicenceToolStripMenuItem
            // 
            this.replacementForLostOrDamagedLicenceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replacementForLostOrDamagedLicenceToolStripMenuItem.Name = "replacementForLostOrDamagedLicenceToolStripMenuItem";
            this.replacementForLostOrDamagedLicenceToolStripMenuItem.Size = new System.Drawing.Size(372, 24);
            this.replacementForLostOrDamagedLicenceToolStripMenuItem.Text = "Replacement for lost or Damaged Licence";
            this.replacementForLostOrDamagedLicenceToolStripMenuItem.Click += new System.EventHandler(this.replacementForLostOrDamagedLicenceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(369, 6);
            // 
            // releaseDetainedDrivingLicencToolStripMenuItem
            // 
            this.releaseDetainedDrivingLicencToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.releaseDetainedDrivingLicencToolStripMenuItem.Name = "releaseDetainedDrivingLicencToolStripMenuItem";
            this.releaseDetainedDrivingLicencToolStripMenuItem.Size = new System.Drawing.Size(372, 24);
            this.releaseDetainedDrivingLicencToolStripMenuItem.Text = "Release Detained Driving Licence";
            this.releaseDetainedDrivingLicencToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedDrivingLicencToolStripMenuItem_Click);
            // 
            // retakeTesrToolStripMenuItem
            // 
            this.retakeTesrToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retakeTesrToolStripMenuItem.Name = "retakeTesrToolStripMenuItem";
            this.retakeTesrToolStripMenuItem.Size = new System.Drawing.Size(372, 24);
            this.retakeTesrToolStripMenuItem.Text = "Retake Test";
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLicenceApplicationToolStripMenuItem,
            this.internationalLicenceApplicationToolStripMenuItem});
            this.manageApplicationsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            this.manageApplicationsToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.manageApplicationsToolStripMenuItem.Text = "Manage Applications";
            // 
            // localDrivingLicenceApplicationToolStripMenuItem
            // 
            this.localDrivingLicenceApplicationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localDrivingLicenceApplicationToolStripMenuItem.Name = "localDrivingLicenceApplicationToolStripMenuItem";
            this.localDrivingLicenceApplicationToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.localDrivingLicenceApplicationToolStripMenuItem.Text = "Local Driving Licence Application";
            this.localDrivingLicenceApplicationToolStripMenuItem.Click += new System.EventHandler(this.localDrivingLicenceApplicationToolStripMenuItem_Click);
            // 
            // internationalLicenceApplicationToolStripMenuItem
            // 
            this.internationalLicenceApplicationToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.internationalLicenceApplicationToolStripMenuItem.Name = "internationalLicenceApplicationToolStripMenuItem";
            this.internationalLicenceApplicationToolStripMenuItem.Size = new System.Drawing.Size(311, 24);
            this.internationalLicenceApplicationToolStripMenuItem.Text = "International Licence Application";
            this.internationalLicenceApplicationToolStripMenuItem.Click += new System.EventHandler(this.internationalLicenceApplicationToolStripMenuItem_Click);
            // 
            // detainLicenceToolStripMenuItem
            // 
            this.detainLicenceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageDetainedLicensesToolStripMenuItem,
            this.detainLicenseToolStripMenuItem,
            this.releaseDetainedLicenseToolStripMenuItem});
            this.detainLicenceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detainLicenceToolStripMenuItem.Name = "detainLicenceToolStripMenuItem";
            this.detainLicenceToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.detainLicenceToolStripMenuItem.Text = "Detain Licence";
            // 
            // manageDetainedLicensesToolStripMenuItem
            // 
            this.manageDetainedLicensesToolStripMenuItem.Name = "manageDetainedLicensesToolStripMenuItem";
            this.manageDetainedLicensesToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.manageDetainedLicensesToolStripMenuItem.Text = "Manage Detained Licenses";
            this.manageDetainedLicensesToolStripMenuItem.Click += new System.EventHandler(this.manageDetainedLicensesToolStripMenuItem_Click);
            // 
            // detainLicenseToolStripMenuItem
            // 
            this.detainLicenseToolStripMenuItem.Name = "detainLicenseToolStripMenuItem";
            this.detainLicenseToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.detainLicenseToolStripMenuItem.Text = "Detain License";
            this.detainLicenseToolStripMenuItem.Click += new System.EventHandler(this.detainLicenseToolStripMenuItem_Click);
            // 
            // releaseDetainedLicenseToolStripMenuItem
            // 
            this.releaseDetainedLicenseToolStripMenuItem.Name = "releaseDetainedLicenseToolStripMenuItem";
            this.releaseDetainedLicenseToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.releaseDetainedLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedLicenseToolStripMenuItem_Click);
            // 
            // manageApplicationTypesToolStripMenuItem
            // 
            this.manageApplicationTypesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manageApplicationTypesToolStripMenuItem.Name = "manageApplicationTypesToolStripMenuItem";
            this.manageApplicationTypesToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.manageApplicationTypesToolStripMenuItem.Text = "Manage Application Types";
            this.manageApplicationTypesToolStripMenuItem.Click += new System.EventHandler(this.manageApplicationTypesToolStripMenuItem_Click);
            // 
            // manageTestTypesToolStripMenuItem
            // 
            this.manageTestTypesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manageTestTypesToolStripMenuItem.Name = "manageTestTypesToolStripMenuItem";
            this.manageTestTypesToolStripMenuItem.Size = new System.Drawing.Size(318, 30);
            this.manageTestTypesToolStripMenuItem.Text = "Manage Test Types";
            this.manageTestTypesToolStripMenuItem.Click += new System.EventHandler(this.manageTestTypesToolStripMenuItem_Click);
            // 
            // btnMenuPeople
            // 
            this.btnMenuPeople.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuPeople.Image = global::DVLD_Windows_Forms.Properties.Resources.people;
            this.btnMenuPeople.Name = "btnMenuPeople";
            this.btnMenuPeople.Size = new System.Drawing.Size(97, 29);
            this.btnMenuPeople.Text = "People";
            this.btnMenuPeople.Click += new System.EventHandler(this.btnMenuPeople_Click);
            // 
            // btnMenuDrivers
            // 
            this.btnMenuDrivers.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuDrivers.Image = global::DVLD_Windows_Forms.Properties.Resources.drivers;
            this.btnMenuDrivers.Name = "btnMenuDrivers";
            this.btnMenuDrivers.Size = new System.Drawing.Size(99, 29);
            this.btnMenuDrivers.Text = "Drivers";
            this.btnMenuDrivers.Click += new System.EventHandler(this.btnMenuDrivers_Click);
            // 
            // btnMenuUsers
            // 
            this.btnMenuUsers.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuUsers.Image = global::DVLD_Windows_Forms.Properties.Resources.people;
            this.btnMenuUsers.Name = "btnMenuUsers";
            this.btnMenuUsers.Size = new System.Drawing.Size(86, 29);
            this.btnMenuUsers.Text = "Users";
            this.btnMenuUsers.Click += new System.EventHandler(this.btnMenuUsers_Click);
            // 
            // btnMenuAccountSettings
            // 
            this.btnMenuAccountSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currentUserInfoToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.signOutToolStripMenuItem});
            this.btnMenuAccountSettings.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuAccountSettings.Image = global::DVLD_Windows_Forms.Properties.Resources.settings_icon64;
            this.btnMenuAccountSettings.Name = "btnMenuAccountSettings";
            this.btnMenuAccountSettings.Size = new System.Drawing.Size(183, 29);
            this.btnMenuAccountSettings.Text = "Account Serrings";
            this.btnMenuAccountSettings.Click += new System.EventHandler(this.btnMenuAccountSettings_Click);
            // 
            // currentUserInfoToolStripMenuItem
            // 
            this.currentUserInfoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentUserInfoToolStripMenuItem.Image = global::DVLD_Windows_Forms.Properties.Resources.person;
            this.currentUserInfoToolStripMenuItem.Name = "currentUserInfoToolStripMenuItem";
            this.currentUserInfoToolStripMenuItem.Size = new System.Drawing.Size(241, 30);
            this.currentUserInfoToolStripMenuItem.Text = "Current User Info";
            this.currentUserInfoToolStripMenuItem.Click += new System.EventHandler(this.currentUserInfoToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changePasswordToolStripMenuItem.Image = global::DVLD_Windows_Forms.Properties.Resources.settings_icon64;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(241, 30);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signOutToolStripMenuItem.Image = global::DVLD_Windows_Forms.Properties.Resources.signout;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(241, 30);
            this.signOutToolStripMenuItem.Text = "Sign Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 29);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(954, 642);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "DVLD";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem btnMenuApplications;
        private System.Windows.Forms.ToolStripMenuItem btnMenuPeople;
        private System.Windows.Forms.ToolStripMenuItem btnMenuDrivers;
        private System.Windows.Forms.ToolStripMenuItem btnMenuUsers;
        private System.Windows.Forms.ToolStripMenuItem btnMenuAccountSettings;
        private System.Windows.Forms.ToolStripMenuItem currentUserInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem drivingLicenceServiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detainLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageTestTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDrivingLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replacementForLostOrDamagedLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedDrivingLicencToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retakeTesrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLicenceApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenceApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem manageDetainedLicensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detainLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedLicenseToolStripMenuItem;
    }
}

