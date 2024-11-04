namespace DVLD_Windows_Forms
{
    partial class frmManageLocalDrivingLicences
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLocalDrivingLicenseApplications = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsEditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsSceduleTest = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsIssueDrivingLicenseFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Windows_Forms.Properties.Resources.manageUsers;
            this.pictureBox1.Location = new System.Drawing.Point(371, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 147);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(259, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local Driving License Applications";
            // 
            // dgvLocalDrivingLicenseApplications
            // 
            this.dgvLocalDrivingLicenseApplications.AllowUserToAddRows = false;
            this.dgvLocalDrivingLicenseApplications.AllowUserToOrderColumns = true;
            this.dgvLocalDrivingLicenseApplications.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalDrivingLicenseApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalDrivingLicenseApplications.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvLocalDrivingLicenseApplications.Location = new System.Drawing.Point(12, 248);
            this.dgvLocalDrivingLicenseApplications.Name = "dgvLocalDrivingLicenseApplications";
            this.dgvLocalDrivingLicenseApplications.ReadOnly = true;
            this.dgvLocalDrivingLicenseApplications.Size = new System.Drawing.Size(930, 237);
            this.dgvLocalDrivingLicenseApplications.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetailsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.cmsEditApplication,
            this.cmsDeleteApplication,
            this.toolStripMenuItem3,
            this.cmsCancelApplication,
            this.toolStripMenuItem4,
            this.cmsSceduleTest,
            this.cmsIssueDrivingLicenseFirstTime,
            this.cmsShowLicense,
            this.cmsLicenseHistory});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(295, 230);
            this.contextMenuStrip1.MouseHover += new System.EventHandler(this.contextMenuStrip1_MouseHover);
            // 
            // showApplicationDetailsToolStripMenuItem
            // 
            this.showApplicationDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showApplicationDetailsToolStripMenuItem.Name = "showApplicationDetailsToolStripMenuItem";
            this.showApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(294, 26);
            this.showApplicationDetailsToolStripMenuItem.Text = "Show Application Details";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(291, 6);
            // 
            // cmsEditApplication
            // 
            this.cmsEditApplication.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsEditApplication.Name = "cmsEditApplication";
            this.cmsEditApplication.Size = new System.Drawing.Size(294, 26);
            this.cmsEditApplication.Text = "Edit Application";
            // 
            // cmsDeleteApplication
            // 
            this.cmsDeleteApplication.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsDeleteApplication.Name = "cmsDeleteApplication";
            this.cmsDeleteApplication.Size = new System.Drawing.Size(294, 26);
            this.cmsDeleteApplication.Text = "Delete  Application";
            this.cmsDeleteApplication.Click += new System.EventHandler(this.cmsDeleteApplication_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(291, 6);
            // 
            // cmsCancelApplication
            // 
            this.cmsCancelApplication.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsCancelApplication.Name = "cmsCancelApplication";
            this.cmsCancelApplication.Size = new System.Drawing.Size(294, 26);
            this.cmsCancelApplication.Text = "Cancel Application";
            this.cmsCancelApplication.Click += new System.EventHandler(this.cancelApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(291, 6);
            // 
            // cmsSceduleTest
            // 
            this.cmsSceduleTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsVisionTest,
            this.cmsWrittenTest,
            this.cmsStreetTest});
            this.cmsSceduleTest.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsSceduleTest.Name = "cmsSceduleTest";
            this.cmsSceduleTest.Size = new System.Drawing.Size(294, 26);
            this.cmsSceduleTest.Text = "Scedule Test";
            this.cmsSceduleTest.Click += new System.EventHandler(this.sceduleToolStripMenuItem_Click);
            this.cmsSceduleTest.MouseHover += new System.EventHandler(this.sceduleToolStripMenuItem_MouseHover);
            // 
            // cmsVisionTest
            // 
            this.cmsVisionTest.Name = "cmsVisionTest";
            this.cmsVisionTest.Size = new System.Drawing.Size(220, 26);
            this.cmsVisionTest.Text = "Scedule Vision Test";
            this.cmsVisionTest.Click += new System.EventHandler(this.sceduleVisionTestToolStripMenuItem_Click);
            // 
            // cmsWrittenTest
            // 
            this.cmsWrittenTest.Name = "cmsWrittenTest";
            this.cmsWrittenTest.Size = new System.Drawing.Size(220, 26);
            this.cmsWrittenTest.Text = "Scedule Written Test";
            this.cmsWrittenTest.Click += new System.EventHandler(this.cmsWrittenTest_Click);
            // 
            // cmsStreetTest
            // 
            this.cmsStreetTest.Name = "cmsStreetTest";
            this.cmsStreetTest.Size = new System.Drawing.Size(220, 26);
            this.cmsStreetTest.Text = "Sccedule Street Test";
            this.cmsStreetTest.Click += new System.EventHandler(this.cmsStreetTest_Click);
            // 
            // cmsIssueDrivingLicenseFirstTime
            // 
            this.cmsIssueDrivingLicenseFirstTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsIssueDrivingLicenseFirstTime.Name = "cmsIssueDrivingLicenseFirstTime";
            this.cmsIssueDrivingLicenseFirstTime.Size = new System.Drawing.Size(294, 26);
            this.cmsIssueDrivingLicenseFirstTime.Text = "Issue Driving License First Time";
            this.cmsIssueDrivingLicenseFirstTime.Click += new System.EventHandler(this.cmsIssueDrivingLicenseFirstTime_Click);
            // 
            // cmsShowLicense
            // 
            this.cmsShowLicense.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsShowLicense.Name = "cmsShowLicense";
            this.cmsShowLicense.Size = new System.Drawing.Size(294, 26);
            this.cmsShowLicense.Text = "Show License";
            this.cmsShowLicense.Click += new System.EventHandler(this.showLicenseToolStripMenuItem_Click);
            // 
            // cmsLicenseHistory
            // 
            this.cmsLicenseHistory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsLicenseHistory.Name = "cmsLicenseHistory";
            this.cmsLicenseHistory.Size = new System.Drawing.Size(294, 26);
            this.cmsLicenseHistory.Text = "Show History Licenses";
            this.cmsLicenseHistory.Click += new System.EventHandler(this.showHistoryLicensesToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 488);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "#Total Records:";
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(135, 488);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(0, 20);
            this.lblTotalRecords.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filter By:";
            // 
            // cbFilter
            // 
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(91, 206);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(149, 21);
            this.cbFilter.TabIndex = 5;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(265, 206);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(229, 20);
            this.tbSearch.TabIndex = 6;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            this.tbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSearch_KeyPress);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Image = global::DVLD_Windows_Forms.Properties.Resources.add_icon;
            this.btnAddNew.Location = new System.Drawing.Point(520, 202);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(31, 27);
            this.btnAddNew.TabIndex = 11;
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // frmManageLocalDrivingLicences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 535);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvLocalDrivingLicenseApplications);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageLocalDrivingLicences";
            this.Text = "frmManageLocalDrivingLicences";
            this.Load += new System.EventHandler(this.frmManageLocalDrivingLicences_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLocalDrivingLicenseApplications;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmsEditApplication;
        private System.Windows.Forms.ToolStripMenuItem cmsDeleteApplication;
        private System.Windows.Forms.ToolStripMenuItem cmsCancelApplication;
        private System.Windows.Forms.ToolStripMenuItem cmsSceduleTest;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem cmsVisionTest;
        private System.Windows.Forms.ToolStripMenuItem cmsWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem cmsStreetTest;
        private System.Windows.Forms.ToolStripMenuItem cmsIssueDrivingLicenseFirstTime;
        private System.Windows.Forms.ToolStripMenuItem cmsShowLicense;
        private System.Windows.Forms.ToolStripMenuItem cmsLicenseHistory;
    }
}