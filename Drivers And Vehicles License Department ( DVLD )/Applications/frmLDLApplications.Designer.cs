namespace DVLDPresentationLayer
{
    partial class frmLDLApplications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLDLApplications));
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitleFilterBy = new System.Windows.Forms.Label();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsLDLApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowApplicationDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiScheduleTests = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScheduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScheduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScheduleStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiIssueDLFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvLDLApplications = new System.Windows.Forms.DataGridView();
            this.lblLDLApplicationsTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblLDLApplicationsBigTitle = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.pbLocalLicense = new System.Windows.Forms.PictureBox();
            this.btnAddLDLApplication = new System.Windows.Forms.Button();
            this.pbManageLDLApplications = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmsLDLApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalLicense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbManageLDLApplications)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblRecordsNumber.Location = new System.Drawing.Point(209, 766);
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
            this.lblRecordsTitle.Location = new System.Drawing.Point(39, 766);
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
            this.txtFilter.Location = new System.Drawing.Point(453, 374);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(344, 41);
            this.txtFilter.TabIndex = 47;
            this.txtFilter.Visible = false;
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.cbFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Tahoma", 21F);
            this.cbFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(208, 373);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(226, 42);
            this.cbFilterBy.TabIndex = 46;
            this.cbFilterBy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawComboBoxItems);
            this.cbFilterBy.DropDown += new System.EventHandler(this.ComboBoxes_DropDown);
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.DropDownClosed += new System.EventHandler(this.cbFilterBy_DropDownClosed);
            // 
            // lblTitleFilterBy
            // 
            this.lblTitleFilterBy.AutoSize = true;
            this.lblTitleFilterBy.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitleFilterBy.Location = new System.Drawing.Point(39, 379);
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
            this.tsSeparator2.Size = new System.Drawing.Size(357, 6);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(357, 6);
            // 
            // cmsLDLApplications
            // 
            this.cmsLDLApplications.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmsLDLApplications.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmsLDLApplications.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsLDLApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowApplicationDetails,
            this.tsSeparator1,
            this.tsmiEditApplication,
            this.tsmiDelete,
            this.tsSeparator2,
            this.tsmiCancelApplication,
            this.tsSeparator3,
            this.tsmiScheduleTests,
            this.tsSeparator4,
            this.tsmiIssueDLFirstTime,
            this.tsSeparator5,
            this.tsmiShowLicense,
            this.tsSeparator6,
            this.tsmiShowPersonLicenseHistory});
            this.cmsLDLApplications.Name = "cmsPeopleMenu";
            this.cmsLDLApplications.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsLDLApplications.Size = new System.Drawing.Size(361, 350);
            this.cmsLDLApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLDLApplications_Opening);
            this.cmsLDLApplications.Paint += new System.Windows.Forms.PaintEventHandler(this.cmsLDLApplications_Paint);
            // 
            // tsmiShowApplicationDetails
            // 
            this.tsmiShowApplicationDetails.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiShowApplicationDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiShowApplicationDetails.Image = global::DVLDPresentationLayer.Properties.Resources.PersonDetails_32;
            this.tsmiShowApplicationDetails.Name = "tsmiShowApplicationDetails";
            this.tsmiShowApplicationDetails.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiShowApplicationDetails.Size = new System.Drawing.Size(360, 36);
            this.tsmiShowApplicationDetails.Text = "Show Application Details";
            this.tsmiShowApplicationDetails.Click += new System.EventHandler(this.tsmiShowApplicationDetails_Click);
            // 
            // tsmiEditApplication
            // 
            this.tsmiEditApplication.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiEditApplication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiEditApplication.Image = global::DVLDPresentationLayer.Properties.Resources.edit_32;
            this.tsmiEditApplication.Name = "tsmiEditApplication";
            this.tsmiEditApplication.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiEditApplication.Size = new System.Drawing.Size(360, 36);
            this.tsmiEditApplication.Text = "Edit Application";
            this.tsmiEditApplication.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsmiEditApplication.Click += new System.EventHandler(this.tsmiEditApplication_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiDelete.Image = global::DVLDPresentationLayer.Properties.Resources.Delete_32_2;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiDelete.Size = new System.Drawing.Size(360, 36);
            this.tsmiDelete.Text = "Delete Application";
            this.tsmiDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiCancelApplication
            // 
            this.tsmiCancelApplication.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiCancelApplication.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiCancelApplication.Image = global::DVLDPresentationLayer.Properties.Resources.Delete_32;
            this.tsmiCancelApplication.Name = "tsmiCancelApplication";
            this.tsmiCancelApplication.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiCancelApplication.Size = new System.Drawing.Size(360, 36);
            this.tsmiCancelApplication.Text = "Cancel Application";
            this.tsmiCancelApplication.Click += new System.EventHandler(this.tsmiCancelApplication_Click);
            // 
            // tsSeparator3
            // 
            this.tsSeparator3.Name = "tsSeparator3";
            this.tsSeparator3.Size = new System.Drawing.Size(357, 6);
            // 
            // tsmiScheduleTests
            // 
            this.tsmiScheduleTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiScheduleVisionTest,
            this.tsmiScheduleWrittenTest,
            this.tsmiScheduleStreetTest});
            this.tsmiScheduleTests.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiScheduleTests.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiScheduleTests.Image = global::DVLDPresentationLayer.Properties.Resources.Schedule_Test_32;
            this.tsmiScheduleTests.Name = "tsmiScheduleTests";
            this.tsmiScheduleTests.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiScheduleTests.Size = new System.Drawing.Size(360, 36);
            this.tsmiScheduleTests.Text = "Schedule Tests";
            // 
            // tsmiScheduleVisionTest
            // 
            this.tsmiScheduleVisionTest.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiScheduleVisionTest.Image = global::DVLDPresentationLayer.Properties.Resources.Vision_Test_32;
            this.tsmiScheduleVisionTest.Name = "tsmiScheduleVisionTest";
            this.tsmiScheduleVisionTest.Size = new System.Drawing.Size(253, 26);
            this.tsmiScheduleVisionTest.Text = "Schedule Vision Test";
            this.tsmiScheduleVisionTest.Click += new System.EventHandler(this.tsmiScheduleVisionTest_Click);
            // 
            // tsmiScheduleWrittenTest
            // 
            this.tsmiScheduleWrittenTest.Enabled = false;
            this.tsmiScheduleWrittenTest.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiScheduleWrittenTest.Image = global::DVLDPresentationLayer.Properties.Resources.Written_Test_32;
            this.tsmiScheduleWrittenTest.Name = "tsmiScheduleWrittenTest";
            this.tsmiScheduleWrittenTest.Size = new System.Drawing.Size(253, 26);
            this.tsmiScheduleWrittenTest.Text = "Schedule Written Test";
            this.tsmiScheduleWrittenTest.Click += new System.EventHandler(this.tsmiScheduleWrittenTest_Click);
            // 
            // tsmiScheduleStreetTest
            // 
            this.tsmiScheduleStreetTest.Enabled = false;
            this.tsmiScheduleStreetTest.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiScheduleStreetTest.Image = global::DVLDPresentationLayer.Properties.Resources.Street_Test_32;
            this.tsmiScheduleStreetTest.Name = "tsmiScheduleStreetTest";
            this.tsmiScheduleStreetTest.Size = new System.Drawing.Size(253, 26);
            this.tsmiScheduleStreetTest.Text = "Schedule Street Test";
            this.tsmiScheduleStreetTest.Click += new System.EventHandler(this.tsmiScheduleStreetTest_Click);
            // 
            // tsSeparator4
            // 
            this.tsSeparator4.Name = "tsSeparator4";
            this.tsSeparator4.Size = new System.Drawing.Size(357, 6);
            // 
            // tsmiIssueDLFirstTime
            // 
            this.tsmiIssueDLFirstTime.Enabled = false;
            this.tsmiIssueDLFirstTime.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiIssueDLFirstTime.Image = global::DVLDPresentationLayer.Properties.Resources.IssueDrivingLicense_32;
            this.tsmiIssueDLFirstTime.Name = "tsmiIssueDLFirstTime";
            this.tsmiIssueDLFirstTime.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiIssueDLFirstTime.Size = new System.Drawing.Size(360, 36);
            this.tsmiIssueDLFirstTime.Text = "Issue Driving License (First Time)";
            this.tsmiIssueDLFirstTime.Click += new System.EventHandler(this.tsmiIssueDLFirstTime_Click);
            // 
            // tsSeparator5
            // 
            this.tsSeparator5.Name = "tsSeparator5";
            this.tsSeparator5.Size = new System.Drawing.Size(357, 6);
            // 
            // tsmiShowLicense
            // 
            this.tsmiShowLicense.Enabled = false;
            this.tsmiShowLicense.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiShowLicense.Image = global::DVLDPresentationLayer.Properties.Resources.License_View_32;
            this.tsmiShowLicense.Name = "tsmiShowLicense";
            this.tsmiShowLicense.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiShowLicense.Size = new System.Drawing.Size(360, 36);
            this.tsmiShowLicense.Text = "Show License";
            this.tsmiShowLicense.Click += new System.EventHandler(this.tsmiShowLicense_Click);
            // 
            // tsSeparator6
            // 
            this.tsSeparator6.Name = "tsSeparator6";
            this.tsSeparator6.Size = new System.Drawing.Size(357, 6);
            // 
            // tsmiShowPersonLicenseHistory
            // 
            this.tsmiShowPersonLicenseHistory.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiShowPersonLicenseHistory.Image = global::DVLDPresentationLayer.Properties.Resources.PersonLicenseHistory_32;
            this.tsmiShowPersonLicenseHistory.Name = "tsmiShowPersonLicenseHistory";
            this.tsmiShowPersonLicenseHistory.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiShowPersonLicenseHistory.Size = new System.Drawing.Size(360, 36);
            this.tsmiShowPersonLicenseHistory.Text = "Show Person License History";
            this.tsmiShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmiShowPersonLicenseHistory_Click);
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
            this.dgvLDLApplications.ColumnHeadersHeight = 40;
            this.dgvLDLApplications.ContextMenuStrip = this.cmsLDLApplications;
            this.dgvLDLApplications.Location = new System.Drawing.Point(39, 437);
            this.dgvLDLApplications.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLDLApplications.Name = "dgvLDLApplications";
            this.dgvLDLApplications.ReadOnly = true;
            this.dgvLDLApplications.RowHeadersWidth = 72;
            this.dgvLDLApplications.RowTemplate.Height = 32;
            this.dgvLDLApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLDLApplications.Size = new System.Drawing.Size(1698, 296);
            this.dgvLDLApplications.StandardTab = true;
            this.dgvLDLApplications.TabIndex = 44;
            this.dgvLDLApplications.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvLDLApplications_Scroll);
            this.dgvLDLApplications.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvLDLApplications_KeyDown);
            // 
            // lblLDLApplicationsTitle
            // 
            this.lblLDLApplicationsTitle.AutoSize = true;
            this.lblLDLApplicationsTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblLDLApplicationsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLDLApplicationsTitle.Location = new System.Drawing.Point(26, 11);
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
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Firebrick;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnExit.Location = new System.Drawing.Point(1723, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 49;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblLDLApplicationsBigTitle
            // 
            this.lblLDLApplicationsBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLDLApplicationsBigTitle.AutoSize = true;
            this.lblLDLApplicationsBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblLDLApplicationsBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblLDLApplicationsBigTitle.Location = new System.Drawing.Point(516, 259);
            this.lblLDLApplicationsBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLDLApplicationsBigTitle.Name = "lblLDLApplicationsBigTitle";
            this.lblLDLApplicationsBigTitle.Size = new System.Drawing.Size(762, 52);
            this.lblLDLApplicationsBigTitle.TabIndex = 51;
            this.lblLDLApplicationsBigTitle.Text = "Local Driving License Applications";
            // 
            // cbStatus
            // 
            this.cbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Tahoma", 21F);
            this.cbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(453, 373);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(182, 42);
            this.cbStatus.TabIndex = 57;
            this.cbStatus.Visible = false;
            this.cbStatus.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawComboBoxItems);
            this.cbStatus.DropDown += new System.EventHandler(this.ComboBoxes_DropDown);
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            this.cbStatus.DropDownClosed += new System.EventHandler(this.cbStatus_DropDownClosed);
            // 
            // pbLocalLicense
            // 
            this.pbLocalLicense.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbLocalLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbLocalLicense.Image = ((System.Drawing.Image)(resources.GetObject("pbLocalLicense.Image")));
            this.pbLocalLicense.ImageLocation = "";
            this.pbLocalLicense.Location = new System.Drawing.Point(972, 110);
            this.pbLocalLicense.Margin = new System.Windows.Forms.Padding(2);
            this.pbLocalLicense.Name = "pbLocalLicense";
            this.pbLocalLicense.Size = new System.Drawing.Size(58, 55);
            this.pbLocalLicense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLocalLicense.TabIndex = 56;
            this.pbLocalLicense.TabStop = false;
            // 
            // btnAddLDLApplication
            // 
            this.btnAddLDLApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLDLApplication.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnAddLDLApplication.FlatAppearance.BorderSize = 2;
            this.btnAddLDLApplication.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddLDLApplication.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddLDLApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLDLApplication.Image = ((System.Drawing.Image)(resources.GetObject("btnAddLDLApplication.Image")));
            this.btnAddLDLApplication.Location = new System.Drawing.Point(1626, 345);
            this.btnAddLDLApplication.Name = "btnAddLDLApplication";
            this.btnAddLDLApplication.Size = new System.Drawing.Size(111, 70);
            this.btnAddLDLApplication.TabIndex = 45;
            this.btnAddLDLApplication.UseVisualStyleBackColor = true;
            this.btnAddLDLApplication.Click += new System.EventHandler(this.btnAddLDLApplication_Click);
            // 
            // pbManageLDLApplications
            // 
            this.pbManageLDLApplications.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbManageLDLApplications.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbManageLDLApplications.Image = ((System.Drawing.Image)(resources.GetObject("pbManageLDLApplications.Image")));
            this.pbManageLDLApplications.ImageLocation = "";
            this.pbManageLDLApplications.Location = new System.Drawing.Point(765, 70);
            this.pbManageLDLApplications.Margin = new System.Windows.Forms.Padding(2);
            this.pbManageLDLApplications.Name = "pbManageLDLApplications";
            this.pbManageLDLApplications.Size = new System.Drawing.Size(265, 169);
            this.pbManageLDLApplications.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbManageLDLApplications.TabIndex = 52;
            this.pbManageLDLApplications.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1590, 759);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmLDLApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1779, 822);
            this.ControlBox = false;
            this.Controls.Add(this.cbStatus);
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
            this.Name = "frmLDLApplications";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplications_Load);
            this.cmsLDLApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLDLApplications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalLicense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbManageLDLApplications)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem tsmiScheduleTests;
        private System.Windows.Forms.ToolStripMenuItem tsmiCancelApplication;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripSeparator tsSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditApplication;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowApplicationDetails;
        private System.Windows.Forms.ContextMenuStrip cmsLDLApplications;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.DataGridView dgvLDLApplications;
        private System.Windows.Forms.Label lblLDLApplicationsTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblLDLApplicationsBigTitle;
        private System.Windows.Forms.PictureBox pbLocalLicense;
        private System.Windows.Forms.ToolStripSeparator tsSeparator3;
        private System.Windows.Forms.ToolStripSeparator tsSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiIssueDLFirstTime;
        private System.Windows.Forms.ToolStripSeparator tsSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowLicense;
        private System.Windows.Forms.ToolStripSeparator tsSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPersonLicenseHistory;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiScheduleVisionTest;
        private System.Windows.Forms.ToolStripMenuItem tsmiScheduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem tsmiScheduleStreetTest;
    }
}