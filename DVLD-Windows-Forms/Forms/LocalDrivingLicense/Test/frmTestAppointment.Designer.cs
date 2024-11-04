namespace DVLD_Windows_Forms.Forms.LocalDrivingLicense.Test
{
    partial class frmTestAppointment
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNewAppointment = new System.Windows.Forms.Button();
            this.dgvPersonAppointments = new System.Windows.Forms.DataGridView();
            this.cms1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTakeTest = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalRecords = new System.Windows.Forms.Label();
            this.ctrlApplicationBasicInfo1 = new DVLD_Windows_Forms.Controls.ctrlApplicationBasicInfo();
            this.ctrlDrivingLicenseApplicationInfo1 = new DVLD_Windows_Forms.Controls.ctrlDrivingLicenseApplicationInfo();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonAppointments)).BeginInit();
            this.cms1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(226, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 25);
            this.lblTitle.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Appoitment";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(606, 564);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 28);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNewAppointment
            // 
            this.btnNewAppointment.Image = global::DVLD_Windows_Forms.Properties.Resources.add_icon;
            this.btnNewAppointment.Location = new System.Drawing.Point(670, 363);
            this.btnNewAppointment.Name = "btnNewAppointment";
            this.btnNewAppointment.Size = new System.Drawing.Size(35, 31);
            this.btnNewAppointment.TabIndex = 6;
            this.btnNewAppointment.UseVisualStyleBackColor = true;
            this.btnNewAppointment.Click += new System.EventHandler(this.btnNewAppointment_Click);
            // 
            // dgvPersonAppointments
            // 
            this.dgvPersonAppointments.AllowUserToAddRows = false;
            this.dgvPersonAppointments.AllowUserToOrderColumns = true;
            this.dgvPersonAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvPersonAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPersonAppointments.ContextMenuStrip = this.cms1;
            this.dgvPersonAppointments.Location = new System.Drawing.Point(15, 397);
            this.dgvPersonAppointments.Name = "dgvPersonAppointments";
            this.dgvPersonAppointments.ReadOnly = true;
            this.dgvPersonAppointments.Size = new System.Drawing.Size(687, 161);
            this.dgvPersonAppointments.TabIndex = 7;
            // 
            // cms1
            // 
            this.cms1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsEdit,
            this.cmsTakeTest});
            this.cms1.Name = "contextMenuStrip1";
            this.cms1.Size = new System.Drawing.Size(138, 56);
            this.cms1.MouseHover += new System.EventHandler(this.MenuStrip_MouseHover);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(137, 26);
            this.cmsEdit.Text = "Edit";
            this.cmsEdit.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // cmsTakeTest
            // 
            this.cmsTakeTest.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmsTakeTest.Name = "cmsTakeTest";
            this.cmsTakeTest.Size = new System.Drawing.Size(137, 26);
            this.cmsTakeTest.Text = "Take Test";
            this.cmsTakeTest.Click += new System.EventHandler(this.cmsTakeTest_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 568);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Records:";
            // 
            // lblTotalRecords
            // 
            this.lblTotalRecords.AutoSize = true;
            this.lblTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRecords.ForeColor = System.Drawing.Color.Red;
            this.lblTotalRecords.Location = new System.Drawing.Point(95, 568);
            this.lblTotalRecords.Name = "lblTotalRecords";
            this.lblTotalRecords.Size = new System.Drawing.Size(0, 18);
            this.lblTotalRecords.TabIndex = 2;
            // 
            // ctrlApplicationBasicInfo1
            // 
            this.ctrlApplicationBasicInfo1.Location = new System.Drawing.Point(12, 169);
            this.ctrlApplicationBasicInfo1.Name = "ctrlApplicationBasicInfo1";
            this.ctrlApplicationBasicInfo1.Size = new System.Drawing.Size(695, 198);
            this.ctrlApplicationBasicInfo1.TabIndex = 1;
            this.ctrlApplicationBasicInfo1.Load += new System.EventHandler(this.ctrlApplicationBasicInfo1_Load);
            // 
            // ctrlDrivingLicenseApplicationInfo1
            // 
            this.ctrlDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(10, 37);
            this.ctrlDrivingLicenseApplicationInfo1.Name = "ctrlDrivingLicenseApplicationInfo1";
            this.ctrlDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(695, 121);
            this.ctrlDrivingLicenseApplicationInfo1.TabIndex = 0;
            this.ctrlDrivingLicenseApplicationInfo1.Load += new System.EventHandler(this.ctrlDrivingLicenseApplicationInfo1_Load);
            // 
            // frmVisionTestAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 597);
            this.Controls.Add(this.dgvPersonAppointments);
            this.Controls.Add(this.btnNewAppointment);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTotalRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ctrlApplicationBasicInfo1);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationInfo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVisionTestAppointment";
            this.Text = "Vision Test Appointment";
            this.Load += new System.EventHandler(this.frmVisionTestAppointment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersonAppointments)).EndInit();
            this.cms1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo1;
        private Controls.ctrlApplicationBasicInfo ctrlApplicationBasicInfo1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNewAppointment;
        private System.Windows.Forms.DataGridView dgvPersonAppointments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalRecords;
        private System.Windows.Forms.ContextMenuStrip cms1;
        private System.Windows.Forms.ToolStripMenuItem cmsEdit;
        private System.Windows.Forms.ToolStripMenuItem cmsTakeTest;
    }
}