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
            this.tpLoginInfo = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.uctrlpersonInfoByFilter = new Driver_And_Vehicle_Licenses_Department___DVLD__.PersonInformationByFilter();
            this.ertxtBox = new System.Windows.Forms.ErrorProvider(this.components);
            this.pbPerson = new System.Windows.Forms.PictureBox();
            this.pbConfirmPassword = new System.Windows.Forms.PictureBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblConfirmPasswordTitle = new System.Windows.Forms.Label();
            this.lblPasswordTitle = new System.Windows.Forms.Label();
            this.lblUserNameTitle = new System.Windows.Forms.Label();
            this.lblUserIDTitle = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.pbUserID = new System.Windows.Forms.PictureBox();
            this.chbIsActive = new System.Windows.Forms.CheckBox();
            this.tcAddNewUser.SuspendLayout();
            this.tpPersonalInfo.SuspendLayout();
            this.tpLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConfirmPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserID)).BeginInit();
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
            this.btnExit.Location = new System.Drawing.Point(1347, 19);
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
            this.lblAddNewUserBigTitle.Location = new System.Drawing.Point(542, 80);
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
            this.lblAddNewUserTitle.Location = new System.Drawing.Point(25, 19);
            this.lblAddNewUserTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddNewUserTitle.Name = "lblAddNewUserTitle";
            this.lblAddNewUserTitle.Size = new System.Drawing.Size(165, 29);
            this.lblAddNewUserTitle.TabIndex = 42;
            this.lblAddNewUserTitle.Text = "Add New User";
            // 
            // tcAddNewUser
            // 
            this.tcAddNewUser.Controls.Add(this.tpPersonalInfo);
            this.tcAddNewUser.Controls.Add(this.tpLoginInfo);
            this.tcAddNewUser.Location = new System.Drawing.Point(25, 165);
            this.tcAddNewUser.Name = "tcAddNewUser";
            this.tcAddNewUser.Padding = new System.Drawing.Point(20, 3);
            this.tcAddNewUser.SelectedIndex = 0;
            this.tcAddNewUser.Size = new System.Drawing.Size(1361, 791);
            this.tcAddNewUser.TabIndex = 43;
            // 
            // tpPersonalInfo
            // 
            this.tpPersonalInfo.AutoScroll = true;
            this.tpPersonalInfo.Controls.Add(this.btnNext);
            this.tpPersonalInfo.Controls.Add(this.uctrlpersonInfoByFilter);
            this.tpPersonalInfo.Location = new System.Drawing.Point(4, 42);
            this.tpPersonalInfo.Name = "tpPersonalInfo";
            this.tpPersonalInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonalInfo.Size = new System.Drawing.Size(1353, 745);
            this.tpPersonalInfo.TabIndex = 0;
            this.tpPersonalInfo.Text = "Personal Info";
            this.tpPersonalInfo.UseVisualStyleBackColor = true;
            // 
            // tpLoginInfo
            // 
            this.tpLoginInfo.AutoScroll = true;
            this.tpLoginInfo.Controls.Add(this.chbIsActive);
            this.tpLoginInfo.Controls.Add(this.pbUserID);
            this.tpLoginInfo.Controls.Add(this.pbPassword);
            this.tpLoginInfo.Controls.Add(this.textBox2);
            this.tpLoginInfo.Controls.Add(this.textBox1);
            this.tpLoginInfo.Controls.Add(this.lblUserID);
            this.tpLoginInfo.Controls.Add(this.pbPerson);
            this.tpLoginInfo.Controls.Add(this.pbConfirmPassword);
            this.tpLoginInfo.Controls.Add(this.txtFirstName);
            this.tpLoginInfo.Controls.Add(this.lblConfirmPasswordTitle);
            this.tpLoginInfo.Controls.Add(this.lblPasswordTitle);
            this.tpLoginInfo.Controls.Add(this.lblUserNameTitle);
            this.tpLoginInfo.Controls.Add(this.lblUserIDTitle);
            this.tpLoginInfo.Location = new System.Drawing.Point(4, 42);
            this.tpLoginInfo.Name = "tpLoginInfo";
            this.tpLoginInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoginInfo.Size = new System.Drawing.Size(1353, 745);
            this.tpLoginInfo.TabIndex = 1;
            this.tpLoginInfo.Text = "Login Info";
            this.tpLoginInfo.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(916, 981);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(206, 59);
            this.btnClose.TabIndex = 45;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderSize = 2;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1157, 981);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(206, 59);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatAppearance.BorderSize = 2;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Next_64;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(1128, 651);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(206, 59);
            this.btnNext.TabIndex = 46;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // uctrlpersonInfoByFilter
            // 
            this.uctrlpersonInfoByFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlpersonInfoByFilter.Location = new System.Drawing.Point(16, 16);
            this.uctrlpersonInfoByFilter.Name = "uctrlpersonInfoByFilter";
            this.uctrlpersonInfoByFilter.Size = new System.Drawing.Size(1318, 620);
            this.uctrlpersonInfoByFilter.TabIndex = 45;
            // 
            // ertxtBox
            // 
            this.ertxtBox.ContainerControl = this;
            // 
            // pbPerson
            // 
            this.pbPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPerson.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Person_32;
            this.pbPerson.Location = new System.Drawing.Point(337, 149);
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
            this.pbConfirmPassword.Location = new System.Drawing.Point(337, 293);
            this.pbConfirmPassword.Name = "pbConfirmPassword";
            this.pbConfirmPassword.Size = new System.Drawing.Size(32, 32);
            this.pbConfirmPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbConfirmPassword.TabIndex = 47;
            this.pbConfirmPassword.TabStop = false;
            // 
            // txtFirstName
            // 
            this.txtFirstName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtFirstName.Location = new System.Drawing.Point(390, 145);
            this.txtFirstName.MaxLength = 20;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtFirstName.Size = new System.Drawing.Size(256, 40);
            this.txtFirstName.TabIndex = 42;
            this.txtFirstName.Tag = "First Name";
            // 
            // lblConfirmPasswordTitle
            // 
            this.lblConfirmPasswordTitle.AutoSize = true;
            this.lblConfirmPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPasswordTitle.Location = new System.Drawing.Point(38, 293);
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
            this.lblPasswordTitle.Location = new System.Drawing.Point(154, 221);
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
            this.lblUserNameTitle.Location = new System.Drawing.Point(145, 149);
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
            this.lblUserIDTitle.Location = new System.Drawing.Point(191, 77);
            this.lblUserIDTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserIDTitle.Name = "lblUserIDTitle";
            this.lblUserIDTitle.Size = new System.Drawing.Size(128, 33);
            this.lblUserIDTitle.TabIndex = 43;
            this.lblUserIDTitle.Text = "UserID :";
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblUserID.Location = new System.Drawing.Point(390, 77);
            this.lblUserID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(60, 33);
            this.lblUserID.TabIndex = 49;
            this.lblUserID.Text = "???";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.textBox1.Location = new System.Drawing.Point(390, 289);
            this.textBox1.MaxLength = 20;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox1.Size = new System.Drawing.Size(256, 40);
            this.textBox1.TabIndex = 50;
            this.textBox1.Tag = "First Name";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.textBox2.Location = new System.Drawing.Point(390, 217);
            this.textBox2.MaxLength = 20;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox2.Size = new System.Drawing.Size(256, 40);
            this.textBox2.TabIndex = 51;
            this.textBox2.Tag = "First Name";
            // 
            // pbPassword
            // 
            this.pbPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPassword.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbPassword.Location = new System.Drawing.Point(337, 221);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(32, 32);
            this.pbPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPassword.TabIndex = 52;
            this.pbPassword.TabStop = false;
            // 
            // pbUserID
            // 
            this.pbUserID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbUserID.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbUserID.Location = new System.Drawing.Point(337, 77);
            this.pbUserID.Name = "pbUserID";
            this.pbUserID.Size = new System.Drawing.Size(32, 32);
            this.pbUserID.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbUserID.TabIndex = 53;
            this.pbUserID.TabStop = false;
            // 
            // chbIsActive
            // 
            this.chbIsActive.AutoSize = true;
            this.chbIsActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbIsActive.Font = new System.Drawing.Font("Tahoma", 20F);
            this.chbIsActive.Location = new System.Drawing.Point(390, 354);
            this.chbIsActive.Name = "chbIsActive";
            this.chbIsActive.Size = new System.Drawing.Size(131, 37);
            this.chbIsActive.TabIndex = 54;
            this.chbIsActive.Text = "Is Active";
            this.chbIsActive.UseVisualStyleBackColor = true;
            // 
            // frmAddNewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1411, 1084);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
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
            this.tcAddNewUser.ResumeLayout(false);
            this.tpPersonalInfo.ResumeLayout(false);
            this.tpLoginInfo.ResumeLayout(false);
            this.tpLoginInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConfirmPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserID)).EndInit();
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
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider ertxtBox;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.PictureBox pbPerson;
        private System.Windows.Forms.PictureBox pbConfirmPassword;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblConfirmPasswordTitle;
        private System.Windows.Forms.Label lblPasswordTitle;
        private System.Windows.Forms.Label lblUserNameTitle;
        private System.Windows.Forms.Label lblUserIDTitle;
        private System.Windows.Forms.PictureBox pbUserID;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chbIsActive;
    }
}