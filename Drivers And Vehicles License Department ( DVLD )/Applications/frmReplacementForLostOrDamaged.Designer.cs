namespace DVLDPresentationLayer.Core
{
    partial class frmReplacementForLostOrDamaged
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
            this.btnIssueReplacement = new System.Windows.Forms.Button();
            this.lnlblShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.lblFormBigTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lnlblShowNewLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.pbOldLicenseID = new System.Windows.Forms.PictureBox();
            this.lblOldLocalLicenseID = new System.Windows.Forms.Label();
            this.lblOldLicenseIDTitle = new System.Windows.Forms.Label();
            this.pbReplacedLicenseID = new System.Windows.Forms.PictureBox();
            this.lblReplacedLicenseID = new System.Windows.Forms.Label();
            this.lblReplacedLicenseIDTitle = new System.Windows.Forms.Label();
            this.pbUser = new System.Windows.Forms.PictureBox();
            this.gbNewLicenseAppInfo = new System.Windows.Forms.GroupBox();
            this.pbApplicationDate = new System.Windows.Forms.PictureBox();
            this.pbAppFees = new System.Windows.Forms.PictureBox();
            this.pbReplacedLicenseAppID = new System.Windows.Forms.PictureBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblCreatedBy_Title = new System.Windows.Forms.Label();
            this.lblAppDateTitle = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblLicenseReplacementAppID = new System.Windows.Forms.Label();
            this.lblAppFeesTitle = new System.Windows.Forms.Label();
            this.lbl_LRApplicationIDTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.rbDamagedLicense = new System.Windows.Forms.RadioButton();
            this.rbLostLicense = new System.Windows.Forms.RadioButton();
            this.gbReplacementFor = new System.Windows.Forms.GroupBox();
            this.uctrlLDLDetailsByFilter = new DVLDPresentationLayer.ctrlLDLicenseDetailsByFilter();
            ((System.ComponentModel.ISupportInitialize)(this.pbOldLicenseID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbReplacedLicenseID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).BeginInit();
            this.gbNewLicenseAppInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbReplacedLicenseAppID)).BeginInit();
            this.gbReplacementFor.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIssueReplacement
            // 
            this.btnIssueReplacement.FlatAppearance.BorderSize = 2;
            this.btnIssueReplacement.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnIssueReplacement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnIssueReplacement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssueReplacement.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnIssueReplacement.Image = global::DVLDPresentationLayer.Properties.Resources.License_Type_32;
            this.btnIssueReplacement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueReplacement.Location = new System.Drawing.Point(848, 838);
            this.btnIssueReplacement.Name = "btnIssueReplacement";
            this.btnIssueReplacement.Size = new System.Drawing.Size(314, 45);
            this.btnIssueReplacement.TabIndex = 200;
            this.btnIssueReplacement.Text = "Issue Replacement";
            this.btnIssueReplacement.UseVisualStyleBackColor = true;
            this.btnIssueReplacement.Click += new System.EventHandler(this.btnIssueReplacement_Click);
            // 
            // lnlblShowLicenseHistory
            // 
            this.lnlblShowLicenseHistory.AutoSize = true;
            this.lnlblShowLicenseHistory.Enabled = false;
            this.lnlblShowLicenseHistory.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lnlblShowLicenseHistory.Location = new System.Drawing.Point(18, 838);
            this.lnlblShowLicenseHistory.Margin = new System.Windows.Forms.Padding(0);
            this.lnlblShowLicenseHistory.Name = "lnlblShowLicenseHistory";
            this.lnlblShowLicenseHistory.Size = new System.Drawing.Size(259, 31);
            this.lnlblShowLicenseHistory.TabIndex = 202;
            this.lnlblShowLicenseHistory.TabStop = true;
            this.lnlblShowLicenseHistory.Text = "Show License History";
            this.lnlblShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlblShowLicenseHistory_LinkClicked);
            // 
            // lblFormBigTitle
            // 
            this.lblFormBigTitle.AutoSize = true;
            this.lblFormBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblFormBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblFormBigTitle.Location = new System.Drawing.Point(410, 50);
            this.lblFormBigTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblFormBigTitle.Name = "lblFormBigTitle";
            this.lblFormBigTitle.Size = new System.Drawing.Size(290, 52);
            this.lblFormBigTitle.TabIndex = 201;
            this.lblFormBigTitle.Text = "frm Big Title";
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
            this.btnClose.Location = new System.Drawing.Point(653, 838);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 199;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFormTitle.Location = new System.Drawing.Point(13, 9);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(111, 31);
            this.lblFormTitle.TabIndex = 198;
            this.lblFormTitle.Text = "frm Title";
            // 
            // lnlblShowNewLicenseInfo
            // 
            this.lnlblShowNewLicenseInfo.AutoSize = true;
            this.lnlblShowNewLicenseInfo.Enabled = false;
            this.lnlblShowNewLicenseInfo.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lnlblShowNewLicenseInfo.Location = new System.Drawing.Point(302, 838);
            this.lnlblShowNewLicenseInfo.Margin = new System.Windows.Forms.Padding(0);
            this.lnlblShowNewLicenseInfo.Name = "lnlblShowNewLicenseInfo";
            this.lnlblShowNewLicenseInfo.Size = new System.Drawing.Size(283, 31);
            this.lnlblShowNewLicenseInfo.TabIndex = 203;
            this.lnlblShowNewLicenseInfo.TabStop = true;
            this.lnlblShowNewLicenseInfo.Text = "Show New License Info";
            this.lnlblShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlblShowNewLicenseInfo_LinkClicked);
            // 
            // pbOldLicenseID
            // 
            this.pbOldLicenseID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbOldLicenseID.Image = global::DVLDPresentationLayer.Properties.Resources.Driver_License_48;
            this.pbOldLicenseID.Location = new System.Drawing.Point(828, 82);
            this.pbOldLicenseID.Name = "pbOldLicenseID";
            this.pbOldLicenseID.Size = new System.Drawing.Size(32, 32);
            this.pbOldLicenseID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOldLicenseID.TabIndex = 123;
            this.pbOldLicenseID.TabStop = false;
            // 
            // lblOldLocalLicenseID
            // 
            this.lblOldLocalLicenseID.AutoSize = true;
            this.lblOldLocalLicenseID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblOldLocalLicenseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblOldLocalLicenseID.Location = new System.Drawing.Point(869, 85);
            this.lblOldLocalLicenseID.Margin = new System.Windows.Forms.Padding(0);
            this.lblOldLocalLicenseID.Name = "lblOldLocalLicenseID";
            this.lblOldLocalLicenseID.Size = new System.Drawing.Size(68, 27);
            this.lblOldLocalLicenseID.TabIndex = 122;
            this.lblOldLocalLicenseID.Text = "[???]";
            // 
            // lblOldLicenseIDTitle
            // 
            this.lblOldLicenseIDTitle.AutoSize = true;
            this.lblOldLicenseIDTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblOldLicenseIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblOldLicenseIDTitle.Location = new System.Drawing.Point(572, 85);
            this.lblOldLicenseIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblOldLicenseIDTitle.Name = "lblOldLicenseIDTitle";
            this.lblOldLicenseIDTitle.Size = new System.Drawing.Size(187, 27);
            this.lblOldLicenseIDTitle.TabIndex = 121;
            this.lblOldLicenseIDTitle.Text = "Old License ID :";
            // 
            // pbReplacedLicenseID
            // 
            this.pbReplacedLicenseID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbReplacedLicenseID.Image = global::DVLDPresentationLayer.Properties.Resources.Renew_Driving_License_32;
            this.pbReplacedLicenseID.Location = new System.Drawing.Point(828, 44);
            this.pbReplacedLicenseID.Name = "pbReplacedLicenseID";
            this.pbReplacedLicenseID.Size = new System.Drawing.Size(32, 32);
            this.pbReplacedLicenseID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbReplacedLicenseID.TabIndex = 120;
            this.pbReplacedLicenseID.TabStop = false;
            // 
            // lblReplacedLicenseID
            // 
            this.lblReplacedLicenseID.AutoSize = true;
            this.lblReplacedLicenseID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblReplacedLicenseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblReplacedLicenseID.Location = new System.Drawing.Point(869, 47);
            this.lblReplacedLicenseID.Margin = new System.Windows.Forms.Padding(0);
            this.lblReplacedLicenseID.Name = "lblReplacedLicenseID";
            this.lblReplacedLicenseID.Size = new System.Drawing.Size(68, 27);
            this.lblReplacedLicenseID.TabIndex = 119;
            this.lblReplacedLicenseID.Text = "[???]";
            // 
            // lblReplacedLicenseIDTitle
            // 
            this.lblReplacedLicenseIDTitle.AutoSize = true;
            this.lblReplacedLicenseIDTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblReplacedLicenseIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblReplacedLicenseIDTitle.Location = new System.Drawing.Point(572, 47);
            this.lblReplacedLicenseIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblReplacedLicenseIDTitle.Name = "lblReplacedLicenseIDTitle";
            this.lblReplacedLicenseIDTitle.Size = new System.Drawing.Size(251, 27);
            this.lblReplacedLicenseIDTitle.TabIndex = 118;
            this.lblReplacedLicenseIDTitle.Text = "Replaced License ID :";
            // 
            // pbUser
            // 
            this.pbUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbUser.Image = global::DVLDPresentationLayer.Properties.Resources.User_32__2;
            this.pbUser.Location = new System.Drawing.Point(828, 120);
            this.pbUser.Name = "pbUser";
            this.pbUser.Size = new System.Drawing.Size(32, 32);
            this.pbUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbUser.TabIndex = 113;
            this.pbUser.TabStop = false;
            // 
            // gbNewLicenseAppInfo
            // 
            this.gbNewLicenseAppInfo.Controls.Add(this.pbOldLicenseID);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblOldLocalLicenseID);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblOldLicenseIDTitle);
            this.gbNewLicenseAppInfo.Controls.Add(this.pbReplacedLicenseID);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblReplacedLicenseID);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblReplacedLicenseIDTitle);
            this.gbNewLicenseAppInfo.Controls.Add(this.pbUser);
            this.gbNewLicenseAppInfo.Controls.Add(this.pbApplicationDate);
            this.gbNewLicenseAppInfo.Controls.Add(this.pbAppFees);
            this.gbNewLicenseAppInfo.Controls.Add(this.pbReplacedLicenseAppID);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblUserName);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblApplicationDate);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblCreatedBy_Title);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblAppDateTitle);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblApplicationFees);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblLicenseReplacementAppID);
            this.gbNewLicenseAppInfo.Controls.Add(this.lblAppFeesTitle);
            this.gbNewLicenseAppInfo.Controls.Add(this.lbl_LRApplicationIDTitle);
            this.gbNewLicenseAppInfo.Font = new System.Drawing.Font("Tahoma", 18F);
            this.gbNewLicenseAppInfo.Location = new System.Drawing.Point(18, 637);
            this.gbNewLicenseAppInfo.Name = "gbNewLicenseAppInfo";
            this.gbNewLicenseAppInfo.Size = new System.Drawing.Size(1146, 172);
            this.gbNewLicenseAppInfo.TabIndex = 204;
            this.gbNewLicenseAppInfo.TabStop = false;
            this.gbNewLicenseAppInfo.Text = "New License Application Info";
            // 
            // pbApplicationDate
            // 
            this.pbApplicationDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbApplicationDate.Image = global::DVLDPresentationLayer.Properties.Resources.Calendar_32;
            this.pbApplicationDate.Location = new System.Drawing.Point(247, 82);
            this.pbApplicationDate.Name = "pbApplicationDate";
            this.pbApplicationDate.Size = new System.Drawing.Size(32, 32);
            this.pbApplicationDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbApplicationDate.TabIndex = 111;
            this.pbApplicationDate.TabStop = false;
            // 
            // pbAppFees
            // 
            this.pbAppFees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbAppFees.Image = global::DVLDPresentationLayer.Properties.Resources.money_32;
            this.pbAppFees.Location = new System.Drawing.Point(247, 120);
            this.pbAppFees.Name = "pbAppFees";
            this.pbAppFees.Size = new System.Drawing.Size(32, 32);
            this.pbAppFees.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbAppFees.TabIndex = 108;
            this.pbAppFees.TabStop = false;
            // 
            // pbReplacedLicenseAppID
            // 
            this.pbReplacedLicenseAppID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbReplacedLicenseAppID.Image = global::DVLDPresentationLayer.Properties.Resources.Number_32;
            this.pbReplacedLicenseAppID.Location = new System.Drawing.Point(247, 44);
            this.pbReplacedLicenseAppID.Name = "pbReplacedLicenseAppID";
            this.pbReplacedLicenseAppID.Size = new System.Drawing.Size(32, 32);
            this.pbReplacedLicenseAppID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbReplacedLicenseAppID.TabIndex = 106;
            this.pbReplacedLicenseAppID.TabStop = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblUserName.Location = new System.Drawing.Point(869, 123);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(80, 27);
            this.lblUserName.TabIndex = 105;
            this.lblUserName.Text = "[????]";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblApplicationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplicationDate.Location = new System.Drawing.Point(288, 85);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(68, 27);
            this.lblApplicationDate.TabIndex = 103;
            this.lblApplicationDate.Text = "[???]";
            // 
            // lblCreatedBy_Title
            // 
            this.lblCreatedBy_Title.AutoSize = true;
            this.lblCreatedBy_Title.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblCreatedBy_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblCreatedBy_Title.Location = new System.Drawing.Point(572, 123);
            this.lblCreatedBy_Title.Margin = new System.Windows.Forms.Padding(0);
            this.lblCreatedBy_Title.Name = "lblCreatedBy_Title";
            this.lblCreatedBy_Title.Size = new System.Drawing.Size(147, 27);
            this.lblCreatedBy_Title.TabIndex = 102;
            this.lblCreatedBy_Title.Text = "Created By :";
            // 
            // lblAppDateTitle
            // 
            this.lblAppDateTitle.AutoSize = true;
            this.lblAppDateTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblAppDateTitle.Location = new System.Drawing.Point(15, 85);
            this.lblAppDateTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblAppDateTitle.Name = "lblAppDateTitle";
            this.lblAppDateTitle.Size = new System.Drawing.Size(210, 27);
            this.lblAppDateTitle.TabIndex = 100;
            this.lblAppDateTitle.Text = "Application Date :";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblApplicationFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplicationFees.Location = new System.Drawing.Point(288, 123);
            this.lblApplicationFees.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(74, 27);
            this.lblApplicationFees.TabIndex = 97;
            this.lblApplicationFees.Text = "[$$$]";
            // 
            // lblLicenseReplacementAppID
            // 
            this.lblLicenseReplacementAppID.AutoSize = true;
            this.lblLicenseReplacementAppID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblLicenseReplacementAppID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLicenseReplacementAppID.Location = new System.Drawing.Point(288, 47);
            this.lblLicenseReplacementAppID.Margin = new System.Windows.Forms.Padding(0);
            this.lblLicenseReplacementAppID.Name = "lblLicenseReplacementAppID";
            this.lblLicenseReplacementAppID.Size = new System.Drawing.Size(68, 27);
            this.lblLicenseReplacementAppID.TabIndex = 95;
            this.lblLicenseReplacementAppID.Text = "[???]";
            // 
            // lblAppFeesTitle
            // 
            this.lblAppFeesTitle.AutoSize = true;
            this.lblAppFeesTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppFeesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblAppFeesTitle.Location = new System.Drawing.Point(15, 123);
            this.lblAppFeesTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblAppFeesTitle.Name = "lblAppFeesTitle";
            this.lblAppFeesTitle.Size = new System.Drawing.Size(208, 27);
            this.lblAppFeesTitle.TabIndex = 92;
            this.lblAppFeesTitle.Text = "Application Fees :";
            // 
            // lbl_LRApplicationIDTitle
            // 
            this.lbl_LRApplicationIDTitle.AutoSize = true;
            this.lbl_LRApplicationIDTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lbl_LRApplicationIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lbl_LRApplicationIDTitle.Location = new System.Drawing.Point(15, 47);
            this.lbl_LRApplicationIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_LRApplicationIDTitle.Name = "lbl_LRApplicationIDTitle";
            this.lbl_LRApplicationIDTitle.Size = new System.Drawing.Size(229, 27);
            this.lbl_LRApplicationIDTitle.TabIndex = 90;
            this.lbl_LRApplicationIDTitle.Text = "L.R.Application ID :";
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
            this.btnExit.Location = new System.Drawing.Point(1133, 9);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 197;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rbDamagedLicense
            // 
            this.rbDamagedLicense.AutoSize = true;
            this.rbDamagedLicense.Checked = true;
            this.rbDamagedLicense.Font = new System.Drawing.Font("Tahoma", 16F);
            this.rbDamagedLicense.Location = new System.Drawing.Point(11, 36);
            this.rbDamagedLicense.Name = "rbDamagedLicense";
            this.rbDamagedLicense.Size = new System.Drawing.Size(202, 31);
            this.rbDamagedLicense.TabIndex = 205;
            this.rbDamagedLicense.TabStop = true;
            this.rbDamagedLicense.Text = "Damaged License";
            this.rbDamagedLicense.UseVisualStyleBackColor = true;
            this.rbDamagedLicense.CheckedChanged += new System.EventHandler(this.rbDamagedLicense_CheckedChanged);
            // 
            // rbLostLicense
            // 
            this.rbLostLicense.AutoSize = true;
            this.rbLostLicense.Font = new System.Drawing.Font("Tahoma", 16F);
            this.rbLostLicense.Location = new System.Drawing.Point(11, 68);
            this.rbLostLicense.Name = "rbLostLicense";
            this.rbLostLicense.Size = new System.Drawing.Size(149, 31);
            this.rbLostLicense.TabIndex = 206;
            this.rbLostLicense.Text = "Lost License";
            this.rbLostLicense.UseVisualStyleBackColor = true;
            // 
            // gbReplacementFor
            // 
            this.gbReplacementFor.Controls.Add(this.rbLostLicense);
            this.gbReplacementFor.Controls.Add(this.rbDamagedLicense);
            this.gbReplacementFor.Location = new System.Drawing.Point(806, 115);
            this.gbReplacementFor.Name = "gbReplacementFor";
            this.gbReplacementFor.Size = new System.Drawing.Size(358, 103);
            this.gbReplacementFor.TabIndex = 207;
            this.gbReplacementFor.TabStop = false;
            this.gbReplacementFor.Text = "Replacement For";
            // 
            // uctrlLDLDetailsByFilter
            // 
            this.uctrlLDLDetailsByFilter.AutoScroll = true;
            this.uctrlLDLDetailsByFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlLDLDetailsByFilter.Font = new System.Drawing.Font("Tahoma", 18F);
            this.uctrlLDLDetailsByFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlLDLDetailsByFilter.Location = new System.Drawing.Point(9, 114);
            this.uctrlLDLDetailsByFilter.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.uctrlLDLDetailsByFilter.Name = "uctrlLDLDetailsByFilter";
            this.uctrlLDLDetailsByFilter.Size = new System.Drawing.Size(1157, 514);
            this.uctrlLDLDetailsByFilter.TabIndex = 196;
            this.uctrlLDLDetailsByFilter.OnSelectedLocalLicense += new DVLDPresentationLayer.ctrlLDLicenseDetailsByFilter.SelectedLocalLicense(this.uctrlLDLDetailsByFilter_OnSelectedLocalLicense);
            // 
            // frmReplacementForLostOrDamaged
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1184, 907);
            this.ControlBox = false;
            this.Controls.Add(this.gbReplacementFor);
            this.Controls.Add(this.btnIssueReplacement);
            this.Controls.Add(this.lnlblShowLicenseHistory);
            this.Controls.Add(this.lblFormBigTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.lnlblShowNewLicenseInfo);
            this.Controls.Add(this.uctrlLDLDetailsByFilter);
            this.Controls.Add(this.gbNewLicenseAppInfo);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmReplacementForLostOrDamaged";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbOldLicenseID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbReplacedLicenseID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).EndInit();
            this.gbNewLicenseAppInfo.ResumeLayout(false);
            this.gbNewLicenseAppInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAppFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbReplacedLicenseAppID)).EndInit();
            this.gbReplacementFor.ResumeLayout(false);
            this.gbReplacementFor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIssueReplacement;
        private System.Windows.Forms.LinkLabel lnlblShowLicenseHistory;
        private System.Windows.Forms.Label lblFormBigTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.LinkLabel lnlblShowNewLicenseInfo;
        private System.Windows.Forms.PictureBox pbOldLicenseID;
        private System.Windows.Forms.Label lblOldLocalLicenseID;
        private System.Windows.Forms.Label lblOldLicenseIDTitle;
        private System.Windows.Forms.PictureBox pbReplacedLicenseID;
        private System.Windows.Forms.Label lblReplacedLicenseID;
        private System.Windows.Forms.Label lblReplacedLicenseIDTitle;
        private System.Windows.Forms.PictureBox pbUser;
        private ctrlLDLicenseDetailsByFilter uctrlLDLDetailsByFilter;
        private System.Windows.Forms.GroupBox gbNewLicenseAppInfo;
        private System.Windows.Forms.PictureBox pbApplicationDate;
        private System.Windows.Forms.PictureBox pbAppFees;
        private System.Windows.Forms.PictureBox pbReplacedLicenseAppID;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblCreatedBy_Title;
        private System.Windows.Forms.Label lblAppDateTitle;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblLicenseReplacementAppID;
        private System.Windows.Forms.Label lblAppFeesTitle;
        private System.Windows.Forms.Label lbl_LRApplicationIDTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbDamagedLicense;
        private System.Windows.Forms.RadioButton rbLostLicense;
        private System.Windows.Forms.GroupBox gbReplacementFor;
    }
}