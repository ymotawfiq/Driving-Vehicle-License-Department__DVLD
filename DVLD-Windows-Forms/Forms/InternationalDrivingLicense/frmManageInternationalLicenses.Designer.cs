namespace DVLD_Windows_Forms.Forms.InternationalDrivingLicense
{
    partial class frmManageInternationalLicenses
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvInternationalDrivingLicenseApplications = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsShoPersonInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsShowLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsShowLicenseDetails = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalDrivingLicenseApplications)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(223, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(553, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "International Driving License Applications";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Windows_Forms.Properties.Resources.manageUsers;
            this.pictureBox1.Location = new System.Drawing.Point(386, -4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 147);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // cbFilter
            // 
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(89, 200);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(149, 21);
            this.cbFilter.TabIndex = 7;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filter By:";
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.Location = new System.Drawing.Point(139, 495);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(0, 20);
            this.lblTotalRecords.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 495);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "#Total Records:";
            // 
            // dgvInternationalDrivingLicenseApplications
            // 
            this.dgvInternationalDrivingLicenseApplications.AllowUserToAddRows = false;
            this.dgvInternationalDrivingLicenseApplications.AllowUserToOrderColumns = true;
            this.dgvInternationalDrivingLicenseApplications.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalDrivingLicenseApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalDrivingLicenseApplications.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvInternationalDrivingLicenseApplications.Location = new System.Drawing.Point(12, 237);
            this.dgvInternationalDrivingLicenseApplications.Name = "dgvInternationalDrivingLicenseApplications";
            this.dgvInternationalDrivingLicenseApplications.ReadOnly = true;
            this.dgvInternationalDrivingLicenseApplications.Size = new System.Drawing.Size(930, 237);
            this.dgvInternationalDrivingLicenseApplications.TabIndex = 10;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsShoPersonInfo,
            this.cmsShowLicenseHistory,
            this.cmsShowLicenseDetails});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(240, 82);
            // 
            // cmsShoPersonInfo
            // 
            this.cmsShoPersonInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsShoPersonInfo.Name = "cmsShoPersonInfo";
            this.cmsShoPersonInfo.Size = new System.Drawing.Size(239, 26);
            this.cmsShoPersonInfo.Text = "Show Person Info";
            this.cmsShoPersonInfo.Click += new System.EventHandler(this.cmsShoPersonInfo_Click);
            // 
            // cmsShowLicenseHistory
            // 
            this.cmsShowLicenseHistory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsShowLicenseHistory.Name = "cmsShowLicenseHistory";
            this.cmsShowLicenseHistory.Size = new System.Drawing.Size(239, 26);
            this.cmsShowLicenseHistory.Text = "Show License History";
            this.cmsShowLicenseHistory.Click += new System.EventHandler(this.cmsShowLicenseHistory_Click);
            // 
            // cmsShowLicenseDetails
            // 
            this.cmsShowLicenseDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsShowLicenseDetails.Name = "cmsShowLicenseDetails";
            this.cmsShowLicenseDetails.Size = new System.Drawing.Size(239, 26);
            this.cmsShowLicenseDetails.Text = "Show License Details";
            this.cmsShowLicenseDetails.Click += new System.EventHandler(this.cmsShowLicenseDetails_Click);
            // 
            // frmManageInternationalLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 524);
            this.Controls.Add(this.dgvInternationalDrivingLicenseApplications);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageInternationalLicenses";
            this.Text = "Manage International Licenses";
            this.Load += new System.EventHandler(this.frmManageInternationalLicenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalDrivingLicenseApplications)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvInternationalDrivingLicenseApplications;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsShoPersonInfo;
        private System.Windows.Forms.ToolStripMenuItem cmsShowLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem cmsShowLicenseDetails;
    }
}