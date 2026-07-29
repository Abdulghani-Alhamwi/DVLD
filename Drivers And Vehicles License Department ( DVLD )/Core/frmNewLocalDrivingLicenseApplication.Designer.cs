namespace Driver_And_Vehicle_Licenses_Department___DVLD__.Core
{
    partial class frmNewLocalDrivingLicenseApplication
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
            this.btnExit = new System.Windows.Forms.Button();
            this.lblNewLocalDrivingLicenseApplicationBigTitle = new System.Windows.Forms.Label();
            this.lblNewLocalDrivingLicenseApplicationTitle = new System.Windows.Forms.Label();
            this.tcNewLocalDrivingLicenseApplication = new System.Windows.Forms.TabControl();
            this.tpPersonalInfo = new System.Windows.Forms.TabPage();
            this.uctrlPersonDetailsByFilter = new Driver_And_Vehicle_Licenses_Department___DVLD__.ctrlPersonDetailsByFilter();
            this.btnNext = new System.Windows.Forms.Button();
            this.tpApplicationInfo = new System.Windows.Forms.TabPage();
            this.cbLicenseClass = new System.Windows.Forms.ComboBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblApplciationFees = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblDLApplicationID = new System.Windows.Forms.Label();
            this.pbDLApplicationID = new System.Windows.Forms.PictureBox();
            this.pbApplicationDate = new System.Windows.Forms.PictureBox();
            this.pbLicenseClass = new System.Windows.Forms.PictureBox();
            this.pbApplciationFees = new System.Windows.Forms.PictureBox();
            this.pbUser = new System.Windows.Forms.PictureBox();
            this.lblCreatedByTitle = new System.Windows.Forms.Label();
            this.lblApplciationFeesTitle = new System.Windows.Forms.Label();
            this.lblLicenseClassTitle = new System.Windows.Forms.Label();
            this.lblApplicationDateTitle = new System.Windows.Forms.Label();
            this.lblDLApplicationIDTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tcNewLocalDrivingLicenseApplication.SuspendLayout();
            this.tpPersonalInfo.SuspendLayout();
            this.tpApplicationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDLApplicationID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLicenseClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplciationFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).BeginInit();
            this.SuspendLayout();
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
            this.btnExit.Location = new System.Drawing.Point(1094, 15);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 43;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblNewLocalDrivingLicenseApplicationBigTitle
            // 
            this.lblNewLocalDrivingLicenseApplicationBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNewLocalDrivingLicenseApplicationBigTitle.AutoSize = true;
            this.lblNewLocalDrivingLicenseApplicationBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblNewLocalDrivingLicenseApplicationBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblNewLocalDrivingLicenseApplicationBigTitle.Location = new System.Drawing.Point(139, 88);
            this.lblNewLocalDrivingLicenseApplicationBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewLocalDrivingLicenseApplicationBigTitle.Name = "lblNewLocalDrivingLicenseApplicationBigTitle";
            this.lblNewLocalDrivingLicenseApplicationBigTitle.Size = new System.Drawing.Size(850, 52);
            this.lblNewLocalDrivingLicenseApplicationBigTitle.TabIndex = 44;
            this.lblNewLocalDrivingLicenseApplicationBigTitle.Text = "New Local Driving License Application";
            // 
            // lblNewLocalDrivingLicenseApplicationTitle
            // 
            this.lblNewLocalDrivingLicenseApplicationTitle.AutoSize = true;
            this.lblNewLocalDrivingLicenseApplicationTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblNewLocalDrivingLicenseApplicationTitle.Location = new System.Drawing.Point(7, 15);
            this.lblNewLocalDrivingLicenseApplicationTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewLocalDrivingLicenseApplicationTitle.Name = "lblNewLocalDrivingLicenseApplicationTitle";
            this.lblNewLocalDrivingLicenseApplicationTitle.Size = new System.Drawing.Size(410, 29);
            this.lblNewLocalDrivingLicenseApplicationTitle.TabIndex = 45;
            this.lblNewLocalDrivingLicenseApplicationTitle.Text = "New Local Driving License Application";
            // 
            // tcNewLocalDrivingLicenseApplication
            // 
            this.tcNewLocalDrivingLicenseApplication.Controls.Add(this.tpPersonalInfo);
            this.tcNewLocalDrivingLicenseApplication.Controls.Add(this.tpApplicationInfo);
            this.tcNewLocalDrivingLicenseApplication.Location = new System.Drawing.Point(20, 173);
            this.tcNewLocalDrivingLicenseApplication.Name = "tcNewLocalDrivingLicenseApplication";
            this.tcNewLocalDrivingLicenseApplication.Padding = new System.Drawing.Point(15, 3);
            this.tcNewLocalDrivingLicenseApplication.SelectedIndex = 0;
            this.tcNewLocalDrivingLicenseApplication.Size = new System.Drawing.Size(1103, 606);
            this.tcNewLocalDrivingLicenseApplication.TabIndex = 46;
            this.tcNewLocalDrivingLicenseApplication.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcNewLocalDrivingLicenseApplication_Selecting);
            // 
            // tpPersonalInfo
            // 
            this.tpPersonalInfo.Controls.Add(this.uctrlPersonDetailsByFilter);
            this.tpPersonalInfo.Controls.Add(this.btnNext);
            this.tpPersonalInfo.Location = new System.Drawing.Point(4, 38);
            this.tpPersonalInfo.Name = "tpPersonalInfo";
            this.tpPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonalInfo.Size = new System.Drawing.Size(1095, 564);
            this.tpPersonalInfo.TabIndex = 0;
            this.tpPersonalInfo.Text = "Personal Info";
            this.tpPersonalInfo.UseVisualStyleBackColor = true;
            // 
            // uctrlPersonDetailsByFilter
            // 
            this.uctrlPersonDetailsByFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlPersonDetailsByFilter.Location = new System.Drawing.Point(2, 6);
            this.uctrlPersonDetailsByFilter.Name = "uctrlPersonDetailsByFilter";
            this.uctrlPersonDetailsByFilter.Size = new System.Drawing.Size(1080, 486);
            this.uctrlPersonDetailsByFilter.TabIndex = 0;
            this.uctrlPersonDetailsByFilter.OnPersonSelected += new Driver_And_Vehicle_Licenses_Department___DVLD__.ctrlPersonDetailsByFilter.PersonSelectedEventHandler(this.uctrlPersonDetailsByFilter_OnPersonSelected);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNext.FlatAppearance.BorderSize = 2;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnNext.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(930, 505);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(152, 42);
            this.btnNext.TabIndex = 47;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tpApplicationInfo
            // 
            this.tpApplicationInfo.Controls.Add(this.cbLicenseClass);
            this.tpApplicationInfo.Controls.Add(this.lblUserName);
            this.tpApplicationInfo.Controls.Add(this.lblApplciationFees);
            this.tpApplicationInfo.Controls.Add(this.lblApplicationDate);
            this.tpApplicationInfo.Controls.Add(this.lblDLApplicationID);
            this.tpApplicationInfo.Controls.Add(this.pbDLApplicationID);
            this.tpApplicationInfo.Controls.Add(this.pbApplicationDate);
            this.tpApplicationInfo.Controls.Add(this.pbLicenseClass);
            this.tpApplicationInfo.Controls.Add(this.pbApplciationFees);
            this.tpApplicationInfo.Controls.Add(this.pbUser);
            this.tpApplicationInfo.Controls.Add(this.lblCreatedByTitle);
            this.tpApplicationInfo.Controls.Add(this.lblApplciationFeesTitle);
            this.tpApplicationInfo.Controls.Add(this.lblLicenseClassTitle);
            this.tpApplicationInfo.Controls.Add(this.lblApplicationDateTitle);
            this.tpApplicationInfo.Controls.Add(this.lblDLApplicationIDTitle);
            this.tpApplicationInfo.Location = new System.Drawing.Point(4, 38);
            this.tpApplicationInfo.Name = "tpApplicationInfo";
            this.tpApplicationInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpApplicationInfo.Size = new System.Drawing.Size(1095, 571);
            this.tpApplicationInfo.TabIndex = 1;
            this.tpApplicationInfo.Text = "Application Info";
            this.tpApplicationInfo.UseVisualStyleBackColor = true;
            // 
            // cbLicenseClass
            // 
            this.cbLicenseClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.cbLicenseClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLicenseClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClass.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.cbLicenseClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(5)))), ((int)(((byte)(5)))));
            this.cbLicenseClass.FormattingEnabled = true;
            this.cbLicenseClass.Location = new System.Drawing.Point(371, 155);
            this.cbLicenseClass.Name = "cbLicenseClass";
            this.cbLicenseClass.Size = new System.Drawing.Size(457, 37);
            this.cbLicenseClass.TabIndex = 94;
            this.cbLicenseClass.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbLicenseClass_DrawItem);
            this.cbLicenseClass.DropDown += new System.EventHandler(this.cbLicenseClass_DropDown);
            this.cbLicenseClass.DropDownClosed += new System.EventHandler(this.cbLicenseClass_DropDownClosed);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblUserName.Location = new System.Drawing.Point(365, 263);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(69, 29);
            this.lblUserName.TabIndex = 93;
            this.lblUserName.Text = "????";
            // 
            // lblApplciationFees
            // 
            this.lblApplciationFees.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblApplciationFees.AutoSize = true;
            this.lblApplciationFees.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblApplciationFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplciationFees.Location = new System.Drawing.Point(365, 211);
            this.lblApplciationFees.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplciationFees.Name = "lblApplciationFees";
            this.lblApplciationFees.Size = new System.Drawing.Size(69, 29);
            this.lblApplciationFees.TabIndex = 92;
            this.lblApplciationFees.Text = "????";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblApplicationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplicationDate.Location = new System.Drawing.Point(365, 107);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(69, 29);
            this.lblApplicationDate.TabIndex = 91;
            this.lblApplicationDate.Text = "????";
            // 
            // lblDLApplicationID
            // 
            this.lblDLApplicationID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDLApplicationID.AutoSize = true;
            this.lblDLApplicationID.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblDLApplicationID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblDLApplicationID.Location = new System.Drawing.Point(365, 55);
            this.lblDLApplicationID.Margin = new System.Windows.Forms.Padding(0);
            this.lblDLApplicationID.Name = "lblDLApplicationID";
            this.lblDLApplicationID.Size = new System.Drawing.Size(77, 29);
            this.lblDLApplicationID.TabIndex = 90;
            this.lblDLApplicationID.Text = "[???]";
            // 
            // pbDLApplicationID
            // 
            this.pbDLApplicationID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbDLApplicationID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbDLApplicationID.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbDLApplicationID.Location = new System.Drawing.Point(320, 53);
            this.pbDLApplicationID.Name = "pbDLApplicationID";
            this.pbDLApplicationID.Size = new System.Drawing.Size(32, 32);
            this.pbDLApplicationID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbDLApplicationID.TabIndex = 89;
            this.pbDLApplicationID.TabStop = false;
            // 
            // pbApplicationDate
            // 
            this.pbApplicationDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbApplicationDate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbApplicationDate.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Calendar_32;
            this.pbApplicationDate.Location = new System.Drawing.Point(320, 105);
            this.pbApplicationDate.Name = "pbApplicationDate";
            this.pbApplicationDate.Size = new System.Drawing.Size(32, 32);
            this.pbApplicationDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbApplicationDate.TabIndex = 88;
            this.pbApplicationDate.TabStop = false;
            // 
            // pbLicenseClass
            // 
            this.pbLicenseClass.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbLicenseClass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbLicenseClass.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.License_Type_32;
            this.pbLicenseClass.Location = new System.Drawing.Point(320, 157);
            this.pbLicenseClass.Name = "pbLicenseClass";
            this.pbLicenseClass.Size = new System.Drawing.Size(32, 32);
            this.pbLicenseClass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLicenseClass.TabIndex = 87;
            this.pbLicenseClass.TabStop = false;
            // 
            // pbApplciationFees
            // 
            this.pbApplciationFees.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbApplciationFees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbApplciationFees.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.money_32;
            this.pbApplciationFees.Location = new System.Drawing.Point(320, 209);
            this.pbApplciationFees.Name = "pbApplciationFees";
            this.pbApplciationFees.Size = new System.Drawing.Size(32, 32);
            this.pbApplciationFees.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbApplciationFees.TabIndex = 86;
            this.pbApplciationFees.TabStop = false;
            // 
            // pbUser
            // 
            this.pbUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbUser.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.User_32__2;
            this.pbUser.Location = new System.Drawing.Point(320, 261);
            this.pbUser.Name = "pbUser";
            this.pbUser.Size = new System.Drawing.Size(32, 32);
            this.pbUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbUser.TabIndex = 85;
            this.pbUser.TabStop = false;
            // 
            // lblCreatedByTitle
            // 
            this.lblCreatedByTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCreatedByTitle.AutoSize = true;
            this.lblCreatedByTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblCreatedByTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblCreatedByTitle.Location = new System.Drawing.Point(67, 263);
            this.lblCreatedByTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblCreatedByTitle.Name = "lblCreatedByTitle";
            this.lblCreatedByTitle.Size = new System.Drawing.Size(159, 29);
            this.lblCreatedByTitle.TabIndex = 84;
            this.lblCreatedByTitle.Text = "Created By :";
            // 
            // lblApplciationFeesTitle
            // 
            this.lblApplciationFeesTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblApplciationFeesTitle.AutoSize = true;
            this.lblApplciationFeesTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblApplciationFeesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplciationFeesTitle.Location = new System.Drawing.Point(67, 211);
            this.lblApplciationFeesTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplciationFeesTitle.Name = "lblApplciationFeesTitle";
            this.lblApplciationFeesTitle.Size = new System.Drawing.Size(224, 29);
            this.lblApplciationFeesTitle.TabIndex = 83;
            this.lblApplciationFeesTitle.Text = "Application Fees :";
            // 
            // lblLicenseClassTitle
            // 
            this.lblLicenseClassTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLicenseClassTitle.AutoSize = true;
            this.lblLicenseClassTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblLicenseClassTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLicenseClassTitle.Location = new System.Drawing.Point(67, 159);
            this.lblLicenseClassTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblLicenseClassTitle.Name = "lblLicenseClassTitle";
            this.lblLicenseClassTitle.Size = new System.Drawing.Size(186, 29);
            this.lblLicenseClassTitle.TabIndex = 82;
            this.lblLicenseClassTitle.Text = "License Class :";
            // 
            // lblApplicationDateTitle
            // 
            this.lblApplicationDateTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblApplicationDateTitle.AutoSize = true;
            this.lblApplicationDateTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblApplicationDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblApplicationDateTitle.Location = new System.Drawing.Point(67, 107);
            this.lblApplicationDateTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblApplicationDateTitle.Name = "lblApplicationDateTitle";
            this.lblApplicationDateTitle.Size = new System.Drawing.Size(226, 29);
            this.lblApplicationDateTitle.TabIndex = 81;
            this.lblApplicationDateTitle.Text = "Application Date :";
            // 
            // lblDLApplicationIDTitle
            // 
            this.lblDLApplicationIDTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblDLApplicationIDTitle.AutoSize = true;
            this.lblDLApplicationIDTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblDLApplicationIDTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblDLApplicationIDTitle.Location = new System.Drawing.Point(67, 55);
            this.lblDLApplicationIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblDLApplicationIDTitle.Name = "lblDLApplicationIDTitle";
            this.lblDLApplicationIDTitle.Size = new System.Drawing.Size(248, 29);
            this.lblDLApplicationIDTitle.TabIndex = 80;
            this.lblDLApplicationIDTitle.Text = "D.L.Application ID :";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.FlatAppearance.BorderSize = 2;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnSave.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(967, 802);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(152, 42);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnClose.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(790, 802);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(152, 42);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmNewLocalDrivingLicenseApplication
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1148, 861);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tcNewLocalDrivingLicenseApplication);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblNewLocalDrivingLicenseApplicationBigTitle);
            this.Controls.Add(this.lblNewLocalDrivingLicenseApplicationTitle);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmNewLocalDrivingLicenseApplication";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmNewLocalDrivingLicenseApplication_Load);
            this.tcNewLocalDrivingLicenseApplication.ResumeLayout(false);
            this.tpPersonalInfo.ResumeLayout(false);
            this.tpApplicationInfo.ResumeLayout(false);
            this.tpApplicationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDLApplicationID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLicenseClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplciationFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblNewLocalDrivingLicenseApplicationBigTitle;
        private System.Windows.Forms.Label lblNewLocalDrivingLicenseApplicationTitle;
        private System.Windows.Forms.TabControl tcNewLocalDrivingLicenseApplication;
        private System.Windows.Forms.TabPage tpPersonalInfo;
        private ctrlPersonDetailsByFilter uctrlPersonDetailsByFilter;
        private System.Windows.Forms.TabPage tpApplicationInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblApplciationFees;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblDLApplicationID;
        private System.Windows.Forms.PictureBox pbDLApplicationID;
        private System.Windows.Forms.PictureBox pbApplicationDate;
        private System.Windows.Forms.PictureBox pbLicenseClass;
        private System.Windows.Forms.PictureBox pbApplciationFees;
        private System.Windows.Forms.PictureBox pbUser;
        private System.Windows.Forms.Label lblCreatedByTitle;
        private System.Windows.Forms.Label lblApplciationFeesTitle;
        private System.Windows.Forms.Label lblLicenseClassTitle;
        private System.Windows.Forms.Label lblApplicationDateTitle;
        private System.Windows.Forms.Label lblDLApplicationIDTitle;
        private System.Windows.Forms.ComboBox cbLicenseClass;
    }
}