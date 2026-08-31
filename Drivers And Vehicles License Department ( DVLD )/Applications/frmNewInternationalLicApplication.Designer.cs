namespace DVLDPresentationLayer.Applications
{
    partial class frmNewInternationalLicApplication
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
            this.btnIssueLicense = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFormBigTitle = new System.Windows.Forms.Label();
            this.lnlblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.lnlblShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.gbApplicationBasicInfo = new System.Windows.Forms.GroupBox();
            this.pbLocalLicenseID = new System.Windows.Forms.PictureBox();
            this.lblLocalLicenseID = new System.Windows.Forms.Label();
            this.lblLocalLicenseIDTitle = new System.Windows.Forms.Label();
            this.pbInternationalLicenseID = new System.Windows.Forms.PictureBox();
            this.lblInternationalLicenseID = new System.Windows.Forms.Label();
            this.lblInternationalLicenseIDTitle = new System.Windows.Forms.Label();
            this.pbIssueDate = new System.Windows.Forms.PictureBox();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.lblIssueDateTitle = new System.Windows.Forms.Label();
            this.pbUser = new System.Windows.Forms.PictureBox();
            this.pbExpirationDate = new System.Windows.Forms.PictureBox();
            this.pbApplicationDate = new System.Windows.Forms.PictureBox();
            this.pbFees = new System.Windows.Forms.PictureBox();
            this.pbInternationalLicenseAppID = new System.Windows.Forms.PictureBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblExpirationDate = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblCreatedBy_Title = new System.Windows.Forms.Label();
            this.lblExpirationDateTitle = new System.Windows.Forms.Label();
            this.lblAppDateTitle = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblInternationalLicenseAppID = new System.Windows.Forms.Label();
            this.lblApplicationFeesTitle = new System.Windows.Forms.Label();
            this.lblI_L_ApplicationIDTitle = new System.Windows.Forms.Label();
            this.uctrlLDLDetailsByFilter = new DVLDPresentationLayer.ctrlLDLicenseDetailsByFilter();
            this.gbApplicationBasicInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalLicenseID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicenseID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpirationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicenseAppID)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIssueLicense
            // 
            this.btnIssueLicense.FlatAppearance.BorderSize = 2;
            this.btnIssueLicense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnIssueLicense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnIssueLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssueLicense.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnIssueLicense.Image = global::DVLDPresentationLayer.Properties.Resources.License_Type_32;
            this.btnIssueLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueLicense.Location = new System.Drawing.Point(995, 946);
            this.btnIssueLicense.Name = "btnIssueLicense";
            this.btnIssueLicense.Size = new System.Drawing.Size(166, 45);
            this.btnIssueLicense.TabIndex = 182;
            this.btnIssueLicense.Text = "Issue";
            this.btnIssueLicense.UseVisualStyleBackColor = true;
            this.btnIssueLicense.Click += new System.EventHandler(this.btnIssueLicense_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnClose.Image = global::DVLDPresentationLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(797, 946);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 181;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFormTitle.Location = new System.Drawing.Point(10, 6);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(450, 31);
            this.lblFormTitle.TabIndex = 180;
            this.lblFormTitle.Text = "New International License Application";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkRed;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Firebrick;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnExit.Location = new System.Drawing.Point(1132, 6);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 179;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFormBigTitle
            // 
            this.lblFormBigTitle.AutoSize = true;
            this.lblFormBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblFormBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblFormBigTitle.Location = new System.Drawing.Point(186, 56);
            this.lblFormBigTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblFormBigTitle.Name = "lblFormBigTitle";
            this.lblFormBigTitle.Size = new System.Drawing.Size(746, 52);
            this.lblFormBigTitle.TabIndex = 183;
            this.lblFormBigTitle.Text = "International License Application";
            // 
            // lnlblShowLicenseHistory
            // 
            this.lnlblShowLicenseHistory.AutoSize = true;
            this.lnlblShowLicenseHistory.Enabled = false;
            this.lnlblShowLicenseHistory.Font = new System.Drawing.Font("Tahoma", 20F);
            this.lnlblShowLicenseHistory.Location = new System.Drawing.Point(17, 946);
            this.lnlblShowLicenseHistory.Margin = new System.Windows.Forms.Padding(0);
            this.lnlblShowLicenseHistory.Name = "lnlblShowLicenseHistory";
            this.lnlblShowLicenseHistory.Size = new System.Drawing.Size(265, 33);
            this.lnlblShowLicenseHistory.TabIndex = 184;
            this.lnlblShowLicenseHistory.TabStop = true;
            this.lnlblShowLicenseHistory.Text = "Show License History";
            this.lnlblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlblShowLicenseHistory_LinkClicked);
            // 
            // lnlblShowLicenseInfo
            // 
            this.lnlblShowLicenseInfo.AutoSize = true;
            this.lnlblShowLicenseInfo.Enabled = false;
            this.lnlblShowLicenseInfo.Font = new System.Drawing.Font("Tahoma", 20F);
            this.lnlblShowLicenseInfo.Location = new System.Drawing.Point(301, 946);
            this.lnlblShowLicenseInfo.Margin = new System.Windows.Forms.Padding(0);
            this.lnlblShowLicenseInfo.Name = "lnlblShowLicenseInfo";
            this.lnlblShowLicenseInfo.Size = new System.Drawing.Size(231, 33);
            this.lnlblShowLicenseInfo.TabIndex = 185;
            this.lnlblShowLicenseInfo.TabStop = true;
            this.lnlblShowLicenseInfo.Text = "Show License Info";
            this.lnlblShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlblShowLicenseInfo_LinkClicked);
            // 
            // gbApplicationBasicInfo
            // 
            this.gbApplicationBasicInfo.Controls.Add(this.pbLocalLicenseID);
            this.gbApplicationBasicInfo.Controls.Add(this.lblLocalLicenseID);
            this.gbApplicationBasicInfo.Controls.Add(this.lblLocalLicenseIDTitle);
            this.gbApplicationBasicInfo.Controls.Add(this.pbInternationalLicenseID);
            this.gbApplicationBasicInfo.Controls.Add(this.lblInternationalLicenseID);
            this.gbApplicationBasicInfo.Controls.Add(this.lblInternationalLicenseIDTitle);
            this.gbApplicationBasicInfo.Controls.Add(this.pbIssueDate);
            this.gbApplicationBasicInfo.Controls.Add(this.lblIssueDate);
            this.gbApplicationBasicInfo.Controls.Add(this.lblIssueDateTitle);
            this.gbApplicationBasicInfo.Controls.Add(this.pbUser);
            this.gbApplicationBasicInfo.Controls.Add(this.pbExpirationDate);
            this.gbApplicationBasicInfo.Controls.Add(this.pbApplicationDate);
            this.gbApplicationBasicInfo.Controls.Add(this.pbFees);
            this.gbApplicationBasicInfo.Controls.Add(this.pbInternationalLicenseAppID);
            this.gbApplicationBasicInfo.Controls.Add(this.lblUserName);
            this.gbApplicationBasicInfo.Controls.Add(this.lblExpirationDate);
            this.gbApplicationBasicInfo.Controls.Add(this.lblApplicationDate);
            this.gbApplicationBasicInfo.Controls.Add(this.lblCreatedBy_Title);
            this.gbApplicationBasicInfo.Controls.Add(this.lblExpirationDateTitle);
            this.gbApplicationBasicInfo.Controls.Add(this.lblAppDateTitle);
            this.gbApplicationBasicInfo.Controls.Add(this.lblApplicationFees);
            this.gbApplicationBasicInfo.Controls.Add(this.lblInternationalLicenseAppID);
            this.gbApplicationBasicInfo.Controls.Add(this.lblApplicationFeesTitle);
            this.gbApplicationBasicInfo.Controls.Add(this.lblI_L_ApplicationIDTitle);
            this.gbApplicationBasicInfo.Font = new System.Drawing.Font("Tahoma", 18F);
            this.gbApplicationBasicInfo.Location = new System.Drawing.Point(17, 670);
            this.gbApplicationBasicInfo.Name = "gbApplicationBasicInfo";
            this.gbApplicationBasicInfo.Size = new System.Drawing.Size(1144, 247);
            this.gbApplicationBasicInfo.TabIndex = 186;
            this.gbApplicationBasicInfo.TabStop = false;
            this.gbApplicationBasicInfo.Text = "Application Basic Info";
            // 
            // pbLocalLicenseID
            // 
            this.pbLocalLicenseID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbLocalLicenseID.Image = global::DVLDPresentationLayer.Properties.Resources.Driver_License_48;
            this.pbLocalLicenseID.Location = new System.Drawing.Point(904, 94);
            this.pbLocalLicenseID.Name = "pbLocalLicenseID";
            this.pbLocalLicenseID.Size = new System.Drawing.Size(32, 32);
            this.pbLocalLicenseID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLocalLicenseID.TabIndex = 123;
            this.pbLocalLicenseID.TabStop = false;
            // 
            // lblLocalLicenseID
            // 
            this.lblLocalLicenseID.AutoSize = true;
            this.lblLocalLicenseID.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblLocalLicenseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLocalLicenseID.Location = new System.Drawing.Point(945, 96);
            this.lblLocalLicenseID.Margin = new System.Windows.Forms.Padding(0);
            this.lblLocalLicenseID.Name = "lblLocalLicenseID";
            this.lblLocalLicenseID.Size = new System.Drawing.Size(71, 28);
            this.lblLocalLicenseID.TabIndex = 122;
            this.lblLocalLicenseID.Text = "[???]";
            // 
            // lblLocalLicenseIDTitle
            // 
            this.lblLocalLicenseIDTitle.AutoSize = true;
            this.lblLocalLicenseIDTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblLocalLicenseIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLocalLicenseIDTitle.Location = new System.Drawing.Point(683, 96);
            this.lblLocalLicenseIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblLocalLicenseIDTitle.Name = "lblLocalLicenseIDTitle";
            this.lblLocalLicenseIDTitle.Size = new System.Drawing.Size(216, 28);
            this.lblLocalLicenseIDTitle.TabIndex = 121;
            this.lblLocalLicenseIDTitle.Text = "Local License ID :";
            // 
            // pbInternationalLicenseID
            // 
            this.pbInternationalLicenseID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbInternationalLicenseID.Image = global::DVLDPresentationLayer.Properties.Resources.International_32;
            this.pbInternationalLicenseID.Location = new System.Drawing.Point(904, 51);
            this.pbInternationalLicenseID.Name = "pbInternationalLicenseID";
            this.pbInternationalLicenseID.Size = new System.Drawing.Size(32, 32);
            this.pbInternationalLicenseID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInternationalLicenseID.TabIndex = 120;
            this.pbInternationalLicenseID.TabStop = false;
            // 
            // lblInternationalLicenseID
            // 
            this.lblInternationalLicenseID.AutoSize = true;
            this.lblInternationalLicenseID.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblInternationalLicenseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblInternationalLicenseID.Location = new System.Drawing.Point(945, 53);
            this.lblInternationalLicenseID.Margin = new System.Windows.Forms.Padding(0);
            this.lblInternationalLicenseID.Name = "lblInternationalLicenseID";
            this.lblInternationalLicenseID.Size = new System.Drawing.Size(71, 28);
            this.lblInternationalLicenseID.TabIndex = 119;
            this.lblInternationalLicenseID.Text = "[???]";
            // 
            // lblInternationalLicenseIDTitle
            // 
            this.lblInternationalLicenseIDTitle.AutoSize = true;
            this.lblInternationalLicenseIDTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblInternationalLicenseIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblInternationalLicenseIDTitle.Location = new System.Drawing.Point(732, 53);
            this.lblInternationalLicenseIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblInternationalLicenseIDTitle.Name = "lblInternationalLicenseIDTitle";
            this.lblInternationalLicenseIDTitle.Size = new System.Drawing.Size(167, 28);
            this.lblInternationalLicenseIDTitle.TabIndex = 118;
            this.lblInternationalLicenseIDTitle.Text = "I.License ID :";
            // 
            // pbIssueDate
            // 
            this.pbIssueDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbIssueDate.Image = global::DVLDPresentationLayer.Properties.Resources.Calendar_32;
            this.pbIssueDate.Location = new System.Drawing.Point(254, 137);
            this.pbIssueDate.Name = "pbIssueDate";
            this.pbIssueDate.Size = new System.Drawing.Size(32, 32);
            this.pbIssueDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbIssueDate.TabIndex = 117;
            this.pbIssueDate.TabStop = false;
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblIssueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblIssueDate.Location = new System.Drawing.Point(295, 139);
            this.lblIssueDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(84, 28);
            this.lblIssueDate.TabIndex = 116;
            this.lblIssueDate.Text = "[????]";
            // 
            // lblIssueDateTitle
            // 
            this.lblIssueDateTitle.AutoSize = true;
            this.lblIssueDateTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblIssueDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblIssueDateTitle.Location = new System.Drawing.Point(94, 139);
            this.lblIssueDateTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblIssueDateTitle.Name = "lblIssueDateTitle";
            this.lblIssueDateTitle.Size = new System.Drawing.Size(153, 28);
            this.lblIssueDateTitle.TabIndex = 115;
            this.lblIssueDateTitle.Text = "Issue Date :";
            // 
            // pbUser
            // 
            this.pbUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbUser.Image = global::DVLDPresentationLayer.Properties.Resources.User_32__2;
            this.pbUser.Location = new System.Drawing.Point(904, 180);
            this.pbUser.Name = "pbUser";
            this.pbUser.Size = new System.Drawing.Size(32, 32);
            this.pbUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbUser.TabIndex = 113;
            this.pbUser.TabStop = false;
            // 
            // pbExpirationDate
            // 
            this.pbExpirationDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbExpirationDate.Image = global::DVLDPresentationLayer.Properties.Resources.Calendar_32;
            this.pbExpirationDate.Location = new System.Drawing.Point(904, 137);
            this.pbExpirationDate.Name = "pbExpirationDate";
            this.pbExpirationDate.Size = new System.Drawing.Size(32, 32);
            this.pbExpirationDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbExpirationDate.TabIndex = 112;
            this.pbExpirationDate.TabStop = false;
            // 
            // pbApplicationDate
            // 
            this.pbApplicationDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbApplicationDate.Image = global::DVLDPresentationLayer.Properties.Resources.Calendar_32;
            this.pbApplicationDate.Location = new System.Drawing.Point(254, 94);
            this.pbApplicationDate.Name = "pbApplicationDate";
            this.pbApplicationDate.Size = new System.Drawing.Size(32, 32);
            this.pbApplicationDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbApplicationDate.TabIndex = 111;
            this.pbApplicationDate.TabStop = false;
            // 
            // pbFees
            // 
            this.pbFees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbFees.Image = global::DVLDPresentationLayer.Properties.Resources.money_32;
            this.pbFees.Location = new System.Drawing.Point(254, 180);
            this.pbFees.Name = "pbFees";
            this.pbFees.Size = new System.Drawing.Size(32, 32);
            this.pbFees.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFees.TabIndex = 108;
            this.pbFees.TabStop = false;
            // 
            // pbInternationalLicenseAppID
            // 
            this.pbInternationalLicenseAppID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbInternationalLicenseAppID.Image = global::DVLDPresentationLayer.Properties.Resources.Number_32;
            this.pbInternationalLicenseAppID.Location = new System.Drawing.Point(254, 51);
            this.pbInternationalLicenseAppID.Name = "pbInternationalLicenseAppID";
            this.pbInternationalLicenseAppID.Size = new System.Drawing.Size(32, 32);
            this.pbInternationalLicenseAppID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInternationalLicenseAppID.TabIndex = 106;
            this.pbInternationalLicenseAppID.TabStop = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblUserName.Location = new System.Drawing.Point(945, 182);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(84, 28);
            this.lblUserName.TabIndex = 105;
            this.lblUserName.Text = "[????]";
            // 
            // lblExpirationDate
            // 
            this.lblExpirationDate.AutoSize = true;
            this.lblExpirationDate.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblExpirationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblExpirationDate.Location = new System.Drawing.Point(945, 139);
            this.lblExpirationDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblExpirationDate.Name = "lblExpirationDate";
            this.lblExpirationDate.Size = new System.Drawing.Size(84, 28);
            this.lblExpirationDate.TabIndex = 104;
            this.lblExpirationDate.Text = "[????]";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblApplicationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplicationDate.Location = new System.Drawing.Point(295, 96);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(71, 28);
            this.lblApplicationDate.TabIndex = 103;
            this.lblApplicationDate.Text = "[???]";
            // 
            // lblCreatedBy_Title
            // 
            this.lblCreatedBy_Title.AutoSize = true;
            this.lblCreatedBy_Title.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblCreatedBy_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblCreatedBy_Title.Location = new System.Drawing.Point(745, 182);
            this.lblCreatedBy_Title.Margin = new System.Windows.Forms.Padding(0);
            this.lblCreatedBy_Title.Name = "lblCreatedBy_Title";
            this.lblCreatedBy_Title.Size = new System.Drawing.Size(154, 28);
            this.lblCreatedBy_Title.TabIndex = 102;
            this.lblCreatedBy_Title.Text = "Created By :";
            // 
            // lblExpirationDateTitle
            // 
            this.lblExpirationDateTitle.AutoSize = true;
            this.lblExpirationDateTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblExpirationDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblExpirationDateTitle.Location = new System.Drawing.Point(691, 139);
            this.lblExpirationDateTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblExpirationDateTitle.Name = "lblExpirationDateTitle";
            this.lblExpirationDateTitle.Size = new System.Drawing.Size(208, 28);
            this.lblExpirationDateTitle.TabIndex = 101;
            this.lblExpirationDateTitle.Text = "Expiration Date :";
            // 
            // lblAppDateTitle
            // 
            this.lblAppDateTitle.AutoSize = true;
            this.lblAppDateTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblAppDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblAppDateTitle.Location = new System.Drawing.Point(28, 96);
            this.lblAppDateTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblAppDateTitle.Name = "lblAppDateTitle";
            this.lblAppDateTitle.Size = new System.Drawing.Size(219, 28);
            this.lblAppDateTitle.TabIndex = 100;
            this.lblAppDateTitle.Text = "Application Date :";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblApplicationFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplicationFees.Location = new System.Drawing.Point(295, 182);
            this.lblApplicationFees.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(84, 28);
            this.lblApplicationFees.TabIndex = 97;
            this.lblApplicationFees.Text = "[????]";
            // 
            // lblInternationalLicenseAppID
            // 
            this.lblInternationalLicenseAppID.AutoSize = true;
            this.lblInternationalLicenseAppID.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblInternationalLicenseAppID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblInternationalLicenseAppID.Location = new System.Drawing.Point(295, 53);
            this.lblInternationalLicenseAppID.Margin = new System.Windows.Forms.Padding(0);
            this.lblInternationalLicenseAppID.Name = "lblInternationalLicenseAppID";
            this.lblInternationalLicenseAppID.Size = new System.Drawing.Size(71, 28);
            this.lblInternationalLicenseAppID.TabIndex = 95;
            this.lblInternationalLicenseAppID.Text = "[???]";
            // 
            // lblApplicationFeesTitle
            // 
            this.lblApplicationFeesTitle.AutoSize = true;
            this.lblApplicationFeesTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblApplicationFeesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplicationFeesTitle.Location = new System.Drawing.Point(167, 182);
            this.lblApplicationFeesTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplicationFeesTitle.Name = "lblApplicationFeesTitle";
            this.lblApplicationFeesTitle.Size = new System.Drawing.Size(80, 28);
            this.lblApplicationFeesTitle.TabIndex = 92;
            this.lblApplicationFeesTitle.Text = "Fees :";
            // 
            // lblI_L_ApplicationIDTitle
            // 
            this.lblI_L_ApplicationIDTitle.AutoSize = true;
            this.lblI_L_ApplicationIDTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblI_L_ApplicationIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblI_L_ApplicationIDTitle.Location = new System.Drawing.Point(17, 53);
            this.lblI_L_ApplicationIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblI_L_ApplicationIDTitle.Name = "lblI_L_ApplicationIDTitle";
            this.lblI_L_ApplicationIDTitle.Size = new System.Drawing.Size(230, 28);
            this.lblI_L_ApplicationIDTitle.TabIndex = 90;
            this.lblI_L_ApplicationIDTitle.Text = "I.L.Application ID :";
            // 
            // uctrlLDLDetailsByFilter
            // 
            this.uctrlLDLDetailsByFilter.AutoScroll = true;
            this.uctrlLDLDetailsByFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlLDLDetailsByFilter.Font = new System.Drawing.Font("Tahoma", 18F);
            this.uctrlLDLDetailsByFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlLDLDetailsByFilter.Location = new System.Drawing.Point(8, 119);
            this.uctrlLDLDetailsByFilter.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.uctrlLDLDetailsByFilter.Name = "uctrlLDLDetailsByFilter";
            this.uctrlLDLDetailsByFilter.Size = new System.Drawing.Size(1157, 549);
            this.uctrlLDLDetailsByFilter.TabIndex = 124;
            this.uctrlLDLDetailsByFilter.OnSelectedLocalLicense += new DVLDPresentationLayer.ctrlLDLicenseDetailsByFilter.SelectedLocalLicense(this.uctrlLDLDetailsByFilter_OnSelectedLocalLicense);
            // 
            // frmNewInternationalLicApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1181, 1008);
            this.ControlBox = false;
            this.Controls.Add(this.gbApplicationBasicInfo);
            this.Controls.Add(this.lnlblShowLicenseInfo);
            this.Controls.Add(this.lnlblShowLicenseHistory);
            this.Controls.Add(this.lblFormBigTitle);
            this.Controls.Add(this.btnIssueLicense);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.uctrlLDLDetailsByFilter);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmNewInternationalLicApplication";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.gbApplicationBasicInfo.ResumeLayout(false);
            this.gbApplicationBasicInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLocalLicenseID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicenseID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpirationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicenseAppID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIssueLicense;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblFormBigTitle;
        private System.Windows.Forms.LinkLabel lnlblShowLicenseHistory;
        private System.Windows.Forms.LinkLabel lnlblShowLicenseInfo;
        private System.Windows.Forms.GroupBox gbApplicationBasicInfo;
        private System.Windows.Forms.PictureBox pbLocalLicenseID;
        private System.Windows.Forms.Label lblLocalLicenseID;
        private System.Windows.Forms.Label lblLocalLicenseIDTitle;
        private System.Windows.Forms.PictureBox pbInternationalLicenseID;
        private System.Windows.Forms.Label lblInternationalLicenseID;
        private System.Windows.Forms.Label lblInternationalLicenseIDTitle;
        private System.Windows.Forms.PictureBox pbIssueDate;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.Label lblIssueDateTitle;
        private System.Windows.Forms.PictureBox pbUser;
        private System.Windows.Forms.PictureBox pbExpirationDate;
        private System.Windows.Forms.PictureBox pbApplicationDate;
        private System.Windows.Forms.PictureBox pbFees;
        private System.Windows.Forms.PictureBox pbInternationalLicenseAppID;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblExpirationDate;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblCreatedBy_Title;
        private System.Windows.Forms.Label lblExpirationDateTitle;
        private System.Windows.Forms.Label lblAppDateTitle;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblInternationalLicenseAppID;
        private System.Windows.Forms.Label lblApplicationFeesTitle;
        private System.Windows.Forms.Label lblI_L_ApplicationIDTitle;
        private ctrlLDLicenseDetailsByFilter uctrlLDLDetailsByFilter;
    }
}