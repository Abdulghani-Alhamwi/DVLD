namespace DVLDPresentationLayer.Controls
{
    partial class frmDetainLocalLicense
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
            this.pbLicenseFees = new System.Windows.Forms.PictureBox();
            this.lblFineFeesTitle = new System.Windows.Forms.Label();
            this.btnDetain = new System.Windows.Forms.Button();
            this.llShowLicenseHistory = new System.Windows.Forms.LinkLabel();
            this.lblFormBigTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.pbRenewedLicenseID = new System.Windows.Forms.PictureBox();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.lblLicenseIDTitle = new System.Windows.Forms.Label();
            this.pbUser = new System.Windows.Forms.PictureBox();
            this.gbDetainInfo = new System.Windows.Forms.GroupBox();
            this.txtFineFees = new System.Windows.Forms.TextBox();
            this.pbApplicationDate = new System.Windows.Forms.PictureBox();
            this.pbInternationalLicenseAppID = new System.Windows.Forms.PictureBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblDetainedDate = new System.Windows.Forms.Label();
            this.lblCreatedBy_Title = new System.Windows.Forms.Label();
            this.lblDetainDateTitle = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.lblDetainIDTitle = new System.Windows.Forms.Label();
            this.ertxtFineFees = new System.Windows.Forms.ErrorProvider(this.components);
            this.uctrlLocalLicenseDetailsWithFilter = new DVLDPresentationLayer.ctrlDriverLicenseInfoByFilter();
            ((System.ComponentModel.ISupportInitialize)(this.pbLicenseFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRenewedLicenseID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).BeginInit();
            this.gbDetainInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicenseAppID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtFineFees)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLicenseFees
            // 
            this.pbLicenseFees.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbLicenseFees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbLicenseFees.Image = global::DVLDPresentationLayer.Properties.Resources.money_32;
            this.pbLicenseFees.Location = new System.Drawing.Point(187, 131);
            this.pbLicenseFees.Name = "pbLicenseFees";
            this.pbLicenseFees.Size = new System.Drawing.Size(32, 32);
            this.pbLicenseFees.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLicenseFees.TabIndex = 126;
            this.pbLicenseFees.TabStop = false;
            // 
            // lblFineFeesTitle
            // 
            this.lblFineFeesTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFineFeesTitle.AutoSize = true;
            this.lblFineFeesTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblFineFeesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFineFeesTitle.Location = new System.Drawing.Point(23, 134);
            this.lblFineFeesTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblFineFeesTitle.Name = "lblFineFeesTitle";
            this.lblFineFeesTitle.Size = new System.Drawing.Size(129, 27);
            this.lblFineFeesTitle.TabIndex = 124;
            this.lblFineFeesTitle.Text = "Fine Fees :";
            // 
            // btnDetain
            // 
            this.btnDetain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDetain.Enabled = false;
            this.btnDetain.FlatAppearance.BorderSize = 2;
            this.btnDetain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnDetain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDetain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetain.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnDetain.Image = global::DVLDPresentationLayer.Properties.Resources.License_Type_32;
            this.btnDetain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetain.Location = new System.Drawing.Point(988, 841);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(166, 45);
            this.btnDetain.TabIndex = 2;
            this.btnDetain.Text = "Detain";
            this.btnDetain.UseVisualStyleBackColor = true;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // llShowLicenseHistory
            // 
            this.llShowLicenseHistory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.llShowLicenseHistory.AutoSize = true;
            this.llShowLicenseHistory.Enabled = false;
            this.llShowLicenseHistory.Font = new System.Drawing.Font("Tahoma", 19F);
            this.llShowLicenseHistory.Location = new System.Drawing.Point(28, 841);
            this.llShowLicenseHistory.Margin = new System.Windows.Forms.Padding(0);
            this.llShowLicenseHistory.Name = "llShowLicenseHistory";
            this.llShowLicenseHistory.Size = new System.Drawing.Size(259, 31);
            this.llShowLicenseHistory.TabIndex = 3;
            this.llShowLicenseHistory.TabStop = true;
            this.llShowLicenseHistory.Text = "Show License History";
            this.llShowLicenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlblShowLicenseHistory_LinkClicked);
            // 
            // lblFormBigTitle
            // 
            this.lblFormBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFormBigTitle.AutoSize = true;
            this.lblFormBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblFormBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblFormBigTitle.Location = new System.Drawing.Point(408, 50);
            this.lblFormBigTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblFormBigTitle.Name = "lblFormBigTitle";
            this.lblFormBigTitle.Size = new System.Drawing.Size(342, 52);
            this.lblFormBigTitle.TabIndex = 201;
            this.lblFormBigTitle.Text = "Detain License";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnClose.Image = global::DVLDPresentationLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(788, 841);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFormTitle.Location = new System.Drawing.Point(5, 7);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(184, 31);
            this.lblFormTitle.TabIndex = 198;
            this.lblFormTitle.Text = "Detain License";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnExit.BackColor = System.Drawing.Color.DarkRed;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Firebrick;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnExit.Location = new System.Drawing.Point(1120, 7);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Enabled = false;
            this.llShowLicenseInfo.Font = new System.Drawing.Font("Tahoma", 19F);
            this.llShowLicenseInfo.Location = new System.Drawing.Point(312, 841);
            this.llShowLicenseInfo.Margin = new System.Windows.Forms.Padding(0);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(225, 31);
            this.llShowLicenseInfo.TabIndex = 4;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show License Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlblShowLicenseInfo_LinkClicked);
            // 
            // pbRenewedLicenseID
            // 
            this.pbRenewedLicenseID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbRenewedLicenseID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbRenewedLicenseID.Image = global::DVLDPresentationLayer.Properties.Resources.Renew_Driving_License_32;
            this.pbRenewedLicenseID.Location = new System.Drawing.Point(752, 41);
            this.pbRenewedLicenseID.Name = "pbRenewedLicenseID";
            this.pbRenewedLicenseID.Size = new System.Drawing.Size(32, 32);
            this.pbRenewedLicenseID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbRenewedLicenseID.TabIndex = 120;
            this.pbRenewedLicenseID.TabStop = false;
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblLicenseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLicenseID.Location = new System.Drawing.Point(793, 44);
            this.lblLicenseID.Margin = new System.Windows.Forms.Padding(0);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(68, 27);
            this.lblLicenseID.TabIndex = 119;
            this.lblLicenseID.Text = "[???]";
            // 
            // lblLicenseIDTitle
            // 
            this.lblLicenseIDTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLicenseIDTitle.AutoSize = true;
            this.lblLicenseIDTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblLicenseIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLicenseIDTitle.Location = new System.Drawing.Point(597, 44);
            this.lblLicenseIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblLicenseIDTitle.Name = "lblLicenseIDTitle";
            this.lblLicenseIDTitle.Size = new System.Drawing.Size(143, 27);
            this.lblLicenseIDTitle.TabIndex = 118;
            this.lblLicenseIDTitle.Text = "License ID :";
            // 
            // pbUser
            // 
            this.pbUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbUser.Image = global::DVLDPresentationLayer.Properties.Resources.User_32__2;
            this.pbUser.Location = new System.Drawing.Point(752, 86);
            this.pbUser.Name = "pbUser";
            this.pbUser.Size = new System.Drawing.Size(32, 32);
            this.pbUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbUser.TabIndex = 113;
            this.pbUser.TabStop = false;
            // 
            // gbDetainInfo
            // 
            this.gbDetainInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gbDetainInfo.Controls.Add(this.txtFineFees);
            this.gbDetainInfo.Controls.Add(this.pbLicenseFees);
            this.gbDetainInfo.Controls.Add(this.lblFineFeesTitle);
            this.gbDetainInfo.Controls.Add(this.pbRenewedLicenseID);
            this.gbDetainInfo.Controls.Add(this.lblLicenseID);
            this.gbDetainInfo.Controls.Add(this.lblLicenseIDTitle);
            this.gbDetainInfo.Controls.Add(this.pbUser);
            this.gbDetainInfo.Controls.Add(this.pbApplicationDate);
            this.gbDetainInfo.Controls.Add(this.pbInternationalLicenseAppID);
            this.gbDetainInfo.Controls.Add(this.lblUserName);
            this.gbDetainInfo.Controls.Add(this.lblDetainedDate);
            this.gbDetainInfo.Controls.Add(this.lblCreatedBy_Title);
            this.gbDetainInfo.Controls.Add(this.lblDetainDateTitle);
            this.gbDetainInfo.Controls.Add(this.lblDetainID);
            this.gbDetainInfo.Controls.Add(this.lblDetainIDTitle);
            this.gbDetainInfo.Font = new System.Drawing.Font("Tahoma", 18F);
            this.gbDetainInfo.Location = new System.Drawing.Point(10, 626);
            this.gbDetainInfo.Name = "gbDetainInfo";
            this.gbDetainInfo.Size = new System.Drawing.Size(1144, 187);
            this.gbDetainInfo.TabIndex = 204;
            this.gbDetainInfo.TabStop = false;
            this.gbDetainInfo.Text = "Detain Info";
            // 
            // txtFineFees
            // 
            this.txtFineFees.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtFineFees.Font = new System.Drawing.Font("Tahoma", 16F);
            this.txtFineFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.txtFineFees.Location = new System.Drawing.Point(235, 131);
            this.txtFineFees.MaxLength = 10;
            this.txtFineFees.Name = "txtFineFees";
            this.txtFineFees.Size = new System.Drawing.Size(169, 33);
            this.txtFineFees.TabIndex = 1;
            this.txtFineFees.Validating += new System.ComponentModel.CancelEventHandler(this.txtFineFees_Validating);
            // 
            // pbApplicationDate
            // 
            this.pbApplicationDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbApplicationDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbApplicationDate.Image = global::DVLDPresentationLayer.Properties.Resources.Calendar_32;
            this.pbApplicationDate.Location = new System.Drawing.Point(187, 86);
            this.pbApplicationDate.Name = "pbApplicationDate";
            this.pbApplicationDate.Size = new System.Drawing.Size(32, 32);
            this.pbApplicationDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbApplicationDate.TabIndex = 111;
            this.pbApplicationDate.TabStop = false;
            // 
            // pbInternationalLicenseAppID
            // 
            this.pbInternationalLicenseAppID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbInternationalLicenseAppID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbInternationalLicenseAppID.Image = global::DVLDPresentationLayer.Properties.Resources.Number_32;
            this.pbInternationalLicenseAppID.Location = new System.Drawing.Point(187, 41);
            this.pbInternationalLicenseAppID.Name = "pbInternationalLicenseAppID";
            this.pbInternationalLicenseAppID.Size = new System.Drawing.Size(32, 32);
            this.pbInternationalLicenseAppID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbInternationalLicenseAppID.TabIndex = 106;
            this.pbInternationalLicenseAppID.TabStop = false;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblUserName.Location = new System.Drawing.Point(793, 89);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(80, 27);
            this.lblUserName.TabIndex = 105;
            this.lblUserName.Text = "[????]";
            // 
            // lblDetainedDate
            // 
            this.lblDetainedDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDetainedDate.AutoSize = true;
            this.lblDetainedDate.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblDetainedDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblDetainedDate.Location = new System.Drawing.Point(229, 89);
            this.lblDetainedDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblDetainedDate.Name = "lblDetainedDate";
            this.lblDetainedDate.Size = new System.Drawing.Size(68, 27);
            this.lblDetainedDate.TabIndex = 103;
            this.lblDetainedDate.Text = "[???]";
            // 
            // lblCreatedBy_Title
            // 
            this.lblCreatedBy_Title.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCreatedBy_Title.AutoSize = true;
            this.lblCreatedBy_Title.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblCreatedBy_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblCreatedBy_Title.Location = new System.Drawing.Point(597, 89);
            this.lblCreatedBy_Title.Margin = new System.Windows.Forms.Padding(0);
            this.lblCreatedBy_Title.Name = "lblCreatedBy_Title";
            this.lblCreatedBy_Title.Size = new System.Drawing.Size(147, 27);
            this.lblCreatedBy_Title.TabIndex = 102;
            this.lblCreatedBy_Title.Text = "Created By :";
            // 
            // lblDetainDateTitle
            // 
            this.lblDetainDateTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDetainDateTitle.AutoSize = true;
            this.lblDetainDateTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblDetainDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblDetainDateTitle.Location = new System.Drawing.Point(23, 89);
            this.lblDetainDateTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblDetainDateTitle.Name = "lblDetainDateTitle";
            this.lblDetainDateTitle.Size = new System.Drawing.Size(157, 27);
            this.lblDetainDateTitle.TabIndex = 100;
            this.lblDetainDateTitle.Text = "Detain Date :";
            // 
            // lblDetainID
            // 
            this.lblDetainID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblDetainID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblDetainID.Location = new System.Drawing.Point(229, 44);
            this.lblDetainID.Margin = new System.Windows.Forms.Padding(0);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(68, 27);
            this.lblDetainID.TabIndex = 95;
            this.lblDetainID.Text = "[???]";
            // 
            // lblDetainIDTitle
            // 
            this.lblDetainIDTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDetainIDTitle.AutoSize = true;
            this.lblDetainIDTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblDetainIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblDetainIDTitle.Location = new System.Drawing.Point(23, 44);
            this.lblDetainIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblDetainIDTitle.Name = "lblDetainIDTitle";
            this.lblDetainIDTitle.Size = new System.Drawing.Size(133, 27);
            this.lblDetainIDTitle.TabIndex = 90;
            this.lblDetainIDTitle.Text = "Detain ID :";
            // 
            // ertxtFineFees
            // 
            this.ertxtFineFees.ContainerControl = this;
            // 
            // uctrlLocalLicenseDetailsWithFilter
            // 
            this.uctrlLocalLicenseDetailsWithFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.uctrlLocalLicenseDetailsWithFilter.AutoScroll = true;
            this.uctrlLocalLicenseDetailsWithFilter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.uctrlLocalLicenseDetailsWithFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlLocalLicenseDetailsWithFilter.FilterEnabled = true;
            this.uctrlLocalLicenseDetailsWithFilter.Font = new System.Drawing.Font("Tahoma", 18F);
            this.uctrlLocalLicenseDetailsWithFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlLocalLicenseDetailsWithFilter.Location = new System.Drawing.Point(1, 105);
            this.uctrlLocalLicenseDetailsWithFilter.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.uctrlLocalLicenseDetailsWithFilter.Name = "uctrlLocalLicenseDetailsWithFilter";
            this.uctrlLocalLicenseDetailsWithFilter.Size = new System.Drawing.Size(1157, 514);
            this.uctrlLocalLicenseDetailsWithFilter.TabIndex = 0;
            this.uctrlLocalLicenseDetailsWithFilter.OnSelectedLicense += new System.Action<int>(this.uctrlLDLDetailsByFilter_OnSelectedLocalLicense);
            // 
            // frmDetainLocalLicense
            // 
            this.AcceptButton = this.btnDetain;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1167, 904);
            this.ControlBox = false;
            this.Controls.Add(this.uctrlLocalLicenseDetailsWithFilter);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.llShowLicenseHistory);
            this.Controls.Add(this.lblFormBigTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.gbDetainInfo);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Name = "frmDetainLocalLicense";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.frmDetainLocalLicense_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.pbLicenseFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRenewedLicenseID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).EndInit();
            this.gbDetainInfo.ResumeLayout(false);
            this.gbDetainInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicenseAppID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtFineFees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbLicenseFees;
        private System.Windows.Forms.Label lblFineFeesTitle;
        private ctrlDriverLicenseInfoByFilter uctrlLocalLicenseDetailsWithFilter;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.LinkLabel llShowLicenseHistory;
        private System.Windows.Forms.Label lblFormBigTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.PictureBox pbRenewedLicenseID;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.Label lblLicenseIDTitle;
        private System.Windows.Forms.PictureBox pbUser;
        private System.Windows.Forms.GroupBox gbDetainInfo;
        private System.Windows.Forms.PictureBox pbApplicationDate;
        private System.Windows.Forms.PictureBox pbInternationalLicenseAppID;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblDetainedDate;
        private System.Windows.Forms.Label lblCreatedBy_Title;
        private System.Windows.Forms.Label lblDetainDateTitle;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.Label lblDetainIDTitle;
        private System.Windows.Forms.TextBox txtFineFees;
        private System.Windows.Forms.ErrorProvider ertxtFineFees;
    }
}