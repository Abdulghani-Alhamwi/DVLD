namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class frmAddNewUser
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
            this.btnExit = new System.Windows.Forms.Button();
            this.lblAddNewUserBigTitle = new System.Windows.Forms.Label();
            this.lblAddNewUserTitle = new System.Windows.Forms.Label();
            this.tcAddNewUser = new System.Windows.Forms.TabControl();
            this.tpPersonalInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.uctrlpersonInfoByFilter = new Driver_And_Vehicle_Licenses_Department___DVLD__.PersonInformationByFilter();
            this.tpLoginInfo = new System.Windows.Forms.TabPage();
            this.chbIsActive = new System.Windows.Forms.CheckBox();
            this.pbUserID = new System.Windows.Forms.PictureBox();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPasswordConfirmation = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.pbPerson = new System.Windows.Forms.PictureBox();
            this.pbConfirmPassword = new System.Windows.Forms.PictureBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblConfirmPasswordTitle = new System.Windows.Forms.Label();
            this.lblPasswordTitle = new System.Windows.Forms.Label();
            this.lblUserNameTitle = new System.Windows.Forms.Label();
            this.lblUserIDTitle = new System.Windows.Forms.Label();
            this.ertxtBox = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tcAddNewUser.SuspendLayout();
            this.tpPersonalInfo.SuspendLayout();
            this.tpLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConfirmPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBox)).BeginInit();
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
            this.btnExit.Location = new System.Drawing.Point(1120, 8);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 40;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblAddNewUserBigTitle
            // 
            this.lblAddNewUserBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblAddNewUserBigTitle.AutoSize = true;
            this.lblAddNewUserBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblAddNewUserBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblAddNewUserBigTitle.Location = new System.Drawing.Point(429, 42);
            this.lblAddNewUserBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddNewUserBigTitle.Name = "lblAddNewUserBigTitle";
            this.lblAddNewUserBigTitle.Size = new System.Drawing.Size(327, 52);
            this.lblAddNewUserBigTitle.TabIndex = 41;
            this.lblAddNewUserBigTitle.Text = "Add New User";
            // 
            // lblAddNewUserTitle
            // 
            this.lblAddNewUserTitle.AutoSize = true;
            this.lblAddNewUserTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblAddNewUserTitle.Location = new System.Drawing.Point(26, 8);
            this.lblAddNewUserTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddNewUserTitle.Name = "lblAddNewUserTitle";
            this.lblAddNewUserTitle.Size = new System.Drawing.Size(165, 29);
            this.lblAddNewUserTitle.TabIndex = 42;
            this.lblAddNewUserTitle.Text = "Add New User";
            // 
            // tcAddNewUser
            // 
            this.tcAddNewUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tcAddNewUser.Controls.Add(this.tpPersonalInfo);
            this.tcAddNewUser.Controls.Add(this.tpLoginInfo);
            this.tcAddNewUser.Location = new System.Drawing.Point(26, 109);
            this.tcAddNewUser.Name = "tcAddNewUser";
            this.tcAddNewUser.Padding = new System.Drawing.Point(20, 3);
            this.tcAddNewUser.SelectedIndex = 0;
            this.tcAddNewUser.Size = new System.Drawing.Size(1133, 617);
            this.tcAddNewUser.TabIndex = 43;
            this.tcAddNewUser.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcAddNewUser_Selecting);
            // 
            // tpPersonalInfo
            // 
            this.tpPersonalInfo.AutoScroll = true;
            this.tpPersonalInfo.Controls.Add(this.btnNext);
            this.tpPersonalInfo.Controls.Add(this.uctrlpersonInfoByFilter);
            this.tpPersonalInfo.Location = new System.Drawing.Point(4, 42);
            this.tpPersonalInfo.Name = "tpPersonalInfo";
            this.tpPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonalInfo.Size = new System.Drawing.Size(1125, 571);
            this.tpPersonalInfo.TabIndex = 0;
            this.tpPersonalInfo.Text = "Personal Info";
            this.tpPersonalInfo.UseVisualStyleBackColor = true;
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
            this.btnNext.Location = new System.Drawing.Point(943, 512);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(152, 42);
            this.btnNext.TabIndex = 46;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // uctrlpersonInfoByFilter
            // 
            this.uctrlpersonInfoByFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.uctrlpersonInfoByFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlpersonInfoByFilter.Location = new System.Drawing.Point(18, 15);
            this.uctrlpersonInfoByFilter.Name = "uctrlpersonInfoByFilter";
            this.uctrlpersonInfoByFilter.Size = new System.Drawing.Size(1089, 487);
            this.uctrlpersonInfoByFilter.TabIndex = 45;
            // 
            // tpLoginInfo
            // 
            this.tpLoginInfo.AutoScroll = true;
            this.tpLoginInfo.Controls.Add(this.chbIsActive);
            this.tpLoginInfo.Controls.Add(this.pbUserID);
            this.tpLoginInfo.Controls.Add(this.pbPassword);
            this.tpLoginInfo.Controls.Add(this.txtPassword);
            this.tpLoginInfo.Controls.Add(this.txtPasswordConfirmation);
            this.tpLoginInfo.Controls.Add(this.lblUserID);
            this.tpLoginInfo.Controls.Add(this.pbPerson);
            this.tpLoginInfo.Controls.Add(this.pbConfirmPassword);
            this.tpLoginInfo.Controls.Add(this.txtUserName);
            this.tpLoginInfo.Controls.Add(this.lblConfirmPasswordTitle);
            this.tpLoginInfo.Controls.Add(this.lblPasswordTitle);
            this.tpLoginInfo.Controls.Add(this.lblUserNameTitle);
            this.tpLoginInfo.Controls.Add(this.lblUserIDTitle);
            this.tpLoginInfo.Location = new System.Drawing.Point(4, 42);
            this.tpLoginInfo.Name = "tpLoginInfo";
            this.tpLoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoginInfo.Size = new System.Drawing.Size(1125, 571);
            this.tpLoginInfo.TabIndex = 1;
            this.tpLoginInfo.Text = "Login Info";
            this.tpLoginInfo.UseVisualStyleBackColor = true;
            // 
            // chbIsActive
            // 
            this.chbIsActive.AutoSize = true;
            this.chbIsActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbIsActive.Font = new System.Drawing.Font("Tahoma", 20F);
            this.chbIsActive.Location = new System.Drawing.Point(380, 354);
            this.chbIsActive.Name = "chbIsActive";
            this.chbIsActive.Size = new System.Drawing.Size(131, 37);
            this.chbIsActive.TabIndex = 3;
            this.chbIsActive.Text = "Is Active";
            this.chbIsActive.UseVisualStyleBackColor = true;
            // 
            // pbUserID
            // 
            this.pbUserID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbUserID.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbUserID.Location = new System.Drawing.Point(327, 77);
            this.pbUserID.Name = "pbUserID";
            this.pbUserID.Size = new System.Drawing.Size(32, 32);
            this.pbUserID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbUserID.TabIndex = 53;
            this.pbUserID.TabStop = false;
            // 
            // pbPassword
            // 
            this.pbPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPassword.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbPassword.Location = new System.Drawing.Point(327, 221);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(32, 32);
            this.pbPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPassword.TabIndex = 52;
            this.pbPassword.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPassword.Location = new System.Drawing.Point(380, 217);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPassword.Size = new System.Drawing.Size(256, 40);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Tag = "First Name";
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            // 
            // txtPasswordConfirmation
            // 
            this.txtPasswordConfirmation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtPasswordConfirmation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPasswordConfirmation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPasswordConfirmation.Location = new System.Drawing.Point(380, 289);
            this.txtPasswordConfirmation.MaxLength = 20;
            this.txtPasswordConfirmation.Name = "txtPasswordConfirmation";
            this.txtPasswordConfirmation.PasswordChar = '*';
            this.txtPasswordConfirmation.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPasswordConfirmation.Size = new System.Drawing.Size(256, 40);
            this.txtPasswordConfirmation.TabIndex = 2;
            this.txtPasswordConfirmation.Tag = "First Name";
            this.txtPasswordConfirmation.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasswordConfirmation_Validating);
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblUserID.Location = new System.Drawing.Point(380, 77);
            this.lblUserID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(60, 33);
            this.lblUserID.TabIndex = 49;
            this.lblUserID.Text = "???";
            // 
            // pbPerson
            // 
            this.pbPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPerson.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Person_32;
            this.pbPerson.Location = new System.Drawing.Point(327, 149);
            this.pbPerson.Name = "pbPerson";
            this.pbPerson.Size = new System.Drawing.Size(32, 32);
            this.pbPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPerson.TabIndex = 48;
            this.pbPerson.TabStop = false;
            // 
            // pbConfirmPassword
            // 
            this.pbConfirmPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbConfirmPassword.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbConfirmPassword.Location = new System.Drawing.Point(327, 293);
            this.pbConfirmPassword.Name = "pbConfirmPassword";
            this.pbConfirmPassword.Size = new System.Drawing.Size(32, 32);
            this.pbConfirmPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbConfirmPassword.TabIndex = 47;
            this.pbConfirmPassword.TabStop = false;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtUserName.Location = new System.Drawing.Point(380, 145);
            this.txtUserName.MaxLength = 20;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtUserName.Size = new System.Drawing.Size(256, 40);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Tag = "First Name";
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            // 
            // lblConfirmPasswordTitle
            // 
            this.lblConfirmPasswordTitle.AutoSize = true;
            this.lblConfirmPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPasswordTitle.Location = new System.Drawing.Point(28, 293);
            this.lblConfirmPasswordTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfirmPasswordTitle.Name = "lblConfirmPasswordTitle";
            this.lblConfirmPasswordTitle.Size = new System.Drawing.Size(281, 33);
            this.lblConfirmPasswordTitle.TabIndex = 46;
            this.lblConfirmPasswordTitle.Text = "Confirm Password :";
            // 
            // lblPasswordTitle
            // 
            this.lblPasswordTitle.AutoSize = true;
            this.lblPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblPasswordTitle.Location = new System.Drawing.Point(144, 221);
            this.lblPasswordTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPasswordTitle.Name = "lblPasswordTitle";
            this.lblPasswordTitle.Size = new System.Drawing.Size(165, 33);
            this.lblPasswordTitle.TabIndex = 45;
            this.lblPasswordTitle.Text = "Password :";
            // 
            // lblUserNameTitle
            // 
            this.lblUserNameTitle.AutoSize = true;
            this.lblUserNameTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblUserNameTitle.Location = new System.Drawing.Point(135, 149);
            this.lblUserNameTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserNameTitle.Name = "lblUserNameTitle";
            this.lblUserNameTitle.Size = new System.Drawing.Size(174, 33);
            this.lblUserNameTitle.TabIndex = 44;
            this.lblUserNameTitle.Text = "UserName :";
            // 
            // lblUserIDTitle
            // 
            this.lblUserIDTitle.AutoSize = true;
            this.lblUserIDTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblUserIDTitle.Location = new System.Drawing.Point(181, 77);
            this.lblUserIDTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserIDTitle.Name = "lblUserIDTitle";
            this.lblUserIDTitle.Size = new System.Drawing.Size(128, 33);
            this.lblUserIDTitle.TabIndex = 43;
            this.lblUserIDTitle.Text = "UserID :";
            // 
            // ertxtBox
            // 
            this.ertxtBox.ContainerControl = this;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnClose.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(790, 750);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(152, 42);
            this.btnClose.TabIndex = 47;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderSize = 2;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnSave.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(973, 750);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(152, 42);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // frmAddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1184, 817);
            this.ControlBox = false;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tcAddNewUser);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblAddNewUserBigTitle);
            this.Controls.Add(this.lblAddNewUserTitle);
            this.Font = new System.Drawing.Font("Tahoma", 20F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "frmAddNewUser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAddNewUser_Load);
            this.tcAddNewUser.ResumeLayout(false);
            this.tpPersonalInfo.ResumeLayout(false);
            this.tpLoginInfo.ResumeLayout(false);
            this.tpLoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConfirmPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblAddNewUserBigTitle;
        private System.Windows.Forms.Label lblAddNewUserTitle;
        private System.Windows.Forms.TabControl tcAddNewUser;
        private System.Windows.Forms.TabPage tpPersonalInfo;
        private System.Windows.Forms.TabPage tpLoginInfo;
        private PersonInformationByFilter uctrlpersonInfoByFilter;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ErrorProvider ertxtBox;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.PictureBox pbPerson;
        private System.Windows.Forms.PictureBox pbConfirmPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblConfirmPasswordTitle;
        private System.Windows.Forms.Label lblPasswordTitle;
        private System.Windows.Forms.Label lblUserNameTitle;
        private System.Windows.Forms.Label lblUserIDTitle;
        private System.Windows.Forms.PictureBox pbUserID;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPasswordConfirmation;
        private System.Windows.Forms.CheckBox chbIsActive;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}