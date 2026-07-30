namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class frmLocalDrivingLicenseApplications
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitleFilterBy = new System.Windows.Forms.Label();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsPeopleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddNewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvLDLApplications = new System.Windows.Forms.DataGridView();
            this.lblLDLApplicationsTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblLDLApplicationsBigTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddLDLApplication = new System.Windows.Forms.Button();
            this.pbManageLDLApplications = new System.Windows.Forms.PictureBox();
            this.pbLocalLicense = new System.Windows.Forms.PictureBox();
            this.cmsPeopleMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbManageLDLApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalLicense)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblRecordsNumber.Location = new System.Drawing.Point(204, 766);
            this.lblRecordsNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(0, 31);
            this.lblRecordsNumber.TabIndex = 55;
            this.lblRecordsNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecordsTitle
            // 
            this.lblRecordsTitle.AutoSize = true;
            this.lblRecordsTitle.Font = new System.Drawing.Font("Tahoma", 19F, System.Drawing.FontStyle.Bold);
            this.lblRecordsTitle.Location = new System.Drawing.Point(30, 766);
            this.lblRecordsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsTitle.Name = "lblRecordsTitle";
            this.lblRecordsTitle.Size = new System.Drawing.Size(164, 31);
            this.lblRecordsTitle.TabIndex = 54;
            this.lblRecordsTitle.Text = "# Records :";
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.Font = new System.Drawing.Font("Tahoma", 21F);
            this.txtFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtFilter.Location = new System.Drawing.Point(444, 374);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(344, 41);
            this.txtFilter.TabIndex = 47;
            this.txtFilter.Visible = false;
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.cbFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Tahoma", 21F);
            this.cbFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(199, 373);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(226, 42);
            this.cbFilterBy.TabIndex = 46;
            // 
            // lblTitleFilterBy
            // 
            this.lblTitleFilterBy.AutoSize = true;
            this.lblTitleFilterBy.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitleFilterBy.Location = new System.Drawing.Point(30, 379);
            this.lblTitleFilterBy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitleFilterBy.Name = "lblTitleFilterBy";
            this.lblTitleFilterBy.Size = new System.Drawing.Size(160, 36);
            this.lblTitleFilterBy.TabIndex = 53;
            this.lblTitleFilterBy.Text = "Filter By :";
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(203, 6);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // cmsPeopleMenu
            // 
            this.cmsPeopleMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmsPeopleMenu.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmsPeopleMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsPeopleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowDetails,
            this.tsSeparator1,
            this.tsmiAddNewUser,
            this.tsmiEdit,
            this.tsmiDelete,
            this.tsSeparator2,
            this.tsmiSendEmail,
            this.tsmiPhoneCall});
            this.cmsPeopleMenu.Name = "cmsPeopleMenu";
            this.cmsPeopleMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsPeopleMenu.Size = new System.Drawing.Size(207, 172);
            // 
            // tsmiShowDetails
            // 
            this.tsmiShowDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiShowDetails.Name = "tsmiShowDetails";
            this.tsmiShowDetails.Size = new System.Drawing.Size(206, 26);
            this.tsmiShowDetails.Text = "Show Details";
            // 
            // tsmiAddNewUser
            // 
            this.tsmiAddNewUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiAddNewUser.Name = "tsmiAddNewUser";
            this.tsmiAddNewUser.Size = new System.Drawing.Size(206, 26);
            this.tsmiAddNewUser.Text = "Add New User";
            this.tsmiAddNewUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(206, 26);
            this.tsmiEdit.Text = "Edit";
            this.tsmiEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(206, 26);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsmiSendEmail
            // 
            this.tsmiSendEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiSendEmail.Name = "tsmiSendEmail";
            this.tsmiSendEmail.Size = new System.Drawing.Size(206, 26);
            this.tsmiSendEmail.Text = "Send Email";
            // 
            // tsmiPhoneCall
            // 
            this.tsmiPhoneCall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiPhoneCall.Name = "tsmiPhoneCall";
            this.tsmiPhoneCall.Size = new System.Drawing.Size(206, 26);
            this.tsmiPhoneCall.Text = "Phone Call";
            // 
            // dgvLDLApplications
            // 
            this.dgvLDLApplications.AllowUserToAddRows = false;
            this.dgvLDLApplications.AllowUserToDeleteRows = false;
            this.dgvLDLApplications.AllowUserToOrderColumns = true;
            this.dgvLDLApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLDLApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLDLApplications.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 18F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLDLApplications.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLDLApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLDLApplications.ContextMenuStrip = this.cmsPeopleMenu;
            this.dgvLDLApplications.Location = new System.Drawing.Point(30, 437);
            this.dgvLDLApplications.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLDLApplications.Name = "dgvLDLApplications";
            this.dgvLDLApplications.ReadOnly = true;
            this.dgvLDLApplications.RowHeadersWidth = 72;
            this.dgvLDLApplications.RowTemplate.Height = 32;
            this.dgvLDLApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLDLApplications.Size = new System.Drawing.Size(1234, 296);
            this.dgvLDLApplications.StandardTab = true;
            this.dgvLDLApplications.TabIndex = 44;
            // 
            // lblLDLApplicationsTitle
            // 
            this.lblLDLApplicationsTitle.AutoSize = true;
            this.lblLDLApplicationsTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblLDLApplicationsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLDLApplicationsTitle.Location = new System.Drawing.Point(25, 11);
            this.lblLDLApplicationsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLDLApplicationsTitle.Name = "lblLDLApplicationsTitle";
            this.lblLDLApplicationsTitle.Size = new System.Drawing.Size(366, 29);
            this.lblLDLApplicationsTitle.TabIndex = 50;
            this.lblLDLApplicationsTitle.Text = "Local Driving License Applications";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.DarkRed;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Firebrick;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnExit.Location = new System.Drawing.Point(1245, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 49;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // lblLDLApplicationsBigTitle
            // 
            this.lblLDLApplicationsBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLDLApplicationsBigTitle.AutoSize = true;
            this.lblLDLApplicationsBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblLDLApplicationsBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblLDLApplicationsBigTitle.Location = new System.Drawing.Point(266, 256);
            this.lblLDLApplicationsBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLDLApplicationsBigTitle.Name = "lblLDLApplicationsBigTitle";
            this.lblLDLApplicationsBigTitle.Size = new System.Drawing.Size(762, 52);
            this.lblLDLApplicationsBigTitle.TabIndex = 51;
            this.lblLDLApplicationsBigTitle.Text = "Local Driving License Applications";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1098, 759);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnAddLDLApplication
            // 
            this.btnAddLDLApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLDLApplication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnAddLDLApplication.FlatAppearance.BorderSize = 2;
            this.btnAddLDLApplication.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddLDLApplication.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddLDLApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLDLApplication.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.New_Application_64;
            this.btnAddLDLApplication.Location = new System.Drawing.Point(1153, 345);
            this.btnAddLDLApplication.Name = "btnAddLDLApplication";
            this.btnAddLDLApplication.Size = new System.Drawing.Size(111, 70);
            this.btnAddLDLApplication.TabIndex = 45;
            this.btnAddLDLApplication.UseVisualStyleBackColor = true;
            // 
            // pbManageLDLApplications
            // 
            this.pbManageLDLApplications.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbManageLDLApplications.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbManageLDLApplications.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Manage_Applications;
            this.pbManageLDLApplications.ImageLocation = "";
            this.pbManageLDLApplications.Location = new System.Drawing.Point(515, 70);
            this.pbManageLDLApplications.Margin = new System.Windows.Forms.Padding(2);
            this.pbManageLDLApplications.Name = "pbManageLDLApplications";
            this.pbManageLDLApplications.Size = new System.Drawing.Size(265, 169);
            this.pbManageLDLApplications.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbManageLDLApplications.TabIndex = 52;
            this.pbManageLDLApplications.TabStop = false;
            // 
            // pbLocalLicense
            // 
            this.pbLocalLicense.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbLocalLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbLocalLicense.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Local_32;
            this.pbLocalLicense.ImageLocation = "";
            this.pbLocalLicense.Location = new System.Drawing.Point(722, 110);
            this.pbLocalLicense.Margin = new System.Windows.Forms.Padding(2);
            this.pbLocalLicense.Name = "pbLocalLicense";
            this.pbLocalLicense.Size = new System.Drawing.Size(42, 50);
            this.pbLocalLicense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLocalLicense.TabIndex = 56;
            this.pbLocalLicense.TabStop = false;
            // 
            // frmLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1296, 822);
            this.ControlBox = false;
            this.Controls.Add(this.pbLocalLicense);
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.lblRecordsTitle);
            this.Controls.Add(this.btnAddLDLApplication);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitleFilterBy);
            this.Controls.Add(this.pbManageLDLApplications);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvLDLApplications);
            this.Controls.Add(this.lblLDLApplicationsTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblLDLApplicationsBigTitle);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmLocalDrivingLicenseApplications";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplications_Load);
            this.cmsPeopleMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbManageLDLApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalLicense)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.Label lblRecordsTitle;
        private System.Windows.Forms.Button btnAddLDLApplication;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitleFilterBy;
        private System.Windows.Forms.PictureBox pbManageLDLApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmiPhoneCall;
        private System.Windows.Forms.ToolStripMenuItem tsmiSendEmail;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripSeparator tsSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNewUser;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowDetails;
        private System.Windows.Forms.ContextMenuStrip cmsPeopleMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.DataGridView dgvLDLApplications;
        private System.Windows.Forms.Label lblLDLApplicationsTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblLDLApplicationsBigTitle;
        private System.Windows.Forms.PictureBox pbLocalLicense;
    }
}