namespace DVLD_Windows_Forms.Forms.LocalDrivingLicense
{
    partial class frmDriverLicensesHistory
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInternationalDrivingLicenses = new System.Windows.Forms.Button();
            this.btnLocalDrivingLicenses = new System.Windows.Forms.Button();
            this.dgvDriverLicenses = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlPersonInfo1 = new DVLD_Windows_Forms.ctrlPersonInfo();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriverLicenses)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(284, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "License History";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInternationalDrivingLicenses);
            this.groupBox1.Controls.Add(this.btnLocalDrivingLicenses);
            this.groupBox1.Controls.Add(this.dgvDriverLicenses);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblRecords);
            this.groupBox1.Location = new System.Drawing.Point(12, 308);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(737, 237);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            // 
            // btnInternationalDrivingLicenses
            // 
            this.btnInternationalDrivingLicenses.Location = new System.Drawing.Point(78, 20);
            this.btnInternationalDrivingLicenses.Name = "btnInternationalDrivingLicenses";
            this.btnInternationalDrivingLicenses.Size = new System.Drawing.Size(75, 23);
            this.btnInternationalDrivingLicenses.TabIndex = 2;
            this.btnInternationalDrivingLicenses.Text = "International";
            this.btnInternationalDrivingLicenses.UseVisualStyleBackColor = true;
            this.btnInternationalDrivingLicenses.Click += new System.EventHandler(this.btnInternationalDrivingLicenses_Click);
            // 
            // btnLocalDrivingLicenses
            // 
            this.btnLocalDrivingLicenses.Location = new System.Drawing.Point(8, 20);
            this.btnLocalDrivingLicenses.Name = "btnLocalDrivingLicenses";
            this.btnLocalDrivingLicenses.Size = new System.Drawing.Size(75, 23);
            this.btnLocalDrivingLicenses.TabIndex = 2;
            this.btnLocalDrivingLicenses.Text = "Local";
            this.btnLocalDrivingLicenses.UseVisualStyleBackColor = true;
            this.btnLocalDrivingLicenses.Click += new System.EventHandler(this.btnLocalDrivingLicenses_Click);
            // 
            // dgvDriverLicenses
            // 
            this.dgvDriverLicenses.AllowUserToAddRows = false;
            this.dgvDriverLicenses.AllowUserToOrderColumns = true;
            this.dgvDriverLicenses.BackgroundColor = System.Drawing.Color.White;
            this.dgvDriverLicenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDriverLicenses.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvDriverLicenses.Location = new System.Drawing.Point(8, 42);
            this.dgvDriverLicenses.Name = "dgvDriverLicenses";
            this.dgvDriverLicenses.ReadOnly = true;
            this.dgvDriverLicenses.Size = new System.Drawing.Size(722, 149);
            this.dgvDriverLicenses.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(4, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "#Records";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.Color.Red;
            this.lblRecords.Location = new System.Drawing.Point(112, 203);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(0, 20);
            this.lblRecords.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(652, 551);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 37);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlPersonInfo1
            // 
            this.ctrlPersonInfo1.Location = new System.Drawing.Point(12, 58);
            this.ctrlPersonInfo1.Name = "ctrlPersonInfo1";
            this.ctrlPersonInfo1.Size = new System.Drawing.Size(737, 231);
            this.ctrlPersonInfo1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseInfoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 52);
            // 
            // showLicenseInfoToolStripMenuItem
            // 
            this.showLicenseInfoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showLicenseInfoToolStripMenuItem.Name = "showLicenseInfoToolStripMenuItem";
            this.showLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(214, 26);
            this.showLicenseInfoToolStripMenuItem.Text = "Show License Info";
            this.showLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.showLicenseInfoToolStripMenuItem_Click);
            // 
            // frmDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 600);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlPersonInfo1);
            this.Name = "frmDriverLicensesHistory";
            this.Text = "Driver Licenses History";
            this.Load += new System.EventHandler(this.frmDriverLicensesHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDriverLicenses)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlPersonInfo ctrlPersonInfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvDriverLicenses;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Button btnInternationalDrivingLicenses;
        private System.Windows.Forms.Button btnLocalDrivingLicenses;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInfoToolStripMenuItem;
    }
}