namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class frmChangePassword
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
            this.pbCurrentPassword = new System.Windows.Forms.PictureBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtPasswordConfirmation = new System.Windows.Forms.TextBox();
            this.pbConfirmPassword = new System.Windows.Forms.PictureBox();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPasswordTitle = new System.Windows.Forms.Label();
            this.lblNewPasswordTitle = new System.Windows.Forms.Label();
            this.lblCurrentPasswordTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbNewPassword = new System.Windows.Forms.PictureBox();
            this.lblManagePeopleTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.erTextBox = new System.Windows.Forms.ErrorProvider(this.components);
            this.uctrlUserDetails = new Driver_And_Vehicle_Licenses_Department___DVLD__.ctrlUserDetails();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConfirmPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCurrentPassword
            // 
            this.pbCurrentPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbCurrentPassword.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbCurrentPassword.Location = new System.Drawing.Point(311, 590);
            this.pbCurrentPassword.Name = "pbCurrentPassword";
            this.pbCurrentPassword.Size = new System.Drawing.Size(32, 32);
            this.pbCurrentPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbCurrentPassword.TabIndex = 63;
            this.pbCurrentPassword.TabStop = false;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtNewPassword.Location = new System.Drawing.Point(367, 661);
            this.txtNewPassword.MaxLength = 20;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtNewPassword.Size = new System.Drawing.Size(256, 36);
            this.txtNewPassword.TabIndex = 54;
            this.txtNewPassword.Tag = "First Name";
            this.txtNewPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtNewPassword_Validating);
            // 
            // txtPasswordConfirmation
            // 
            this.txtPasswordConfirmation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtPasswordConfirmation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPasswordConfirmation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPasswordConfirmation.Location = new System.Drawing.Point(367, 733);
            this.txtPasswordConfirmation.MaxLength = 20;
            this.txtPasswordConfirmation.Name = "txtPasswordConfirmation";
            this.txtPasswordConfirmation.PasswordChar = '*';
            this.txtPasswordConfirmation.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPasswordConfirmation.Size = new System.Drawing.Size(256, 36);
            this.txtPasswordConfirmation.TabIndex = 55;
            this.txtPasswordConfirmation.Tag = "First Name";
            this.txtPasswordConfirmation.Validating += new System.ComponentModel.CancelEventHandler(this.txtPasswordConfirmation_Validating);
            // 
            // pbConfirmPassword
            // 
            this.pbConfirmPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbConfirmPassword.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbConfirmPassword.Location = new System.Drawing.Point(311, 735);
            this.pbConfirmPassword.Name = "pbConfirmPassword";
            this.pbConfirmPassword.Size = new System.Drawing.Size(32, 32);
            this.pbConfirmPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbConfirmPassword.TabIndex = 59;
            this.pbConfirmPassword.TabStop = false;
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtCurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtCurrentPassword.Location = new System.Drawing.Point(367, 588);
            this.txtCurrentPassword.MaxLength = 20;
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtCurrentPassword.Size = new System.Drawing.Size(256, 36);
            this.txtCurrentPassword.TabIndex = 53;
            this.txtCurrentPassword.Tag = "First Name";
            this.txtCurrentPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtCurrentPassword_Validating);
            // 
            // lblConfirmPasswordTitle
            // 
            this.lblConfirmPasswordTitle.AutoSize = true;
            this.lblConfirmPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPasswordTitle.Location = new System.Drawing.Point(16, 735);
            this.lblConfirmPasswordTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfirmPasswordTitle.Name = "lblConfirmPasswordTitle";
            this.lblConfirmPasswordTitle.Size = new System.Drawing.Size(281, 33);
            this.lblConfirmPasswordTitle.TabIndex = 58;
            this.lblConfirmPasswordTitle.Text = "Confirm Password :";
            // 
            // lblNewPasswordTitle
            // 
            this.lblNewPasswordTitle.AutoSize = true;
            this.lblNewPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblNewPasswordTitle.Location = new System.Drawing.Point(63, 663);
            this.lblNewPasswordTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewPasswordTitle.Name = "lblNewPasswordTitle";
            this.lblNewPasswordTitle.Size = new System.Drawing.Size(234, 33);
            this.lblNewPasswordTitle.TabIndex = 57;
            this.lblNewPasswordTitle.Text = "New Password :";
            // 
            // lblCurrentPasswordTitle
            // 
            this.lblCurrentPasswordTitle.AutoSize = true;
            this.lblCurrentPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblCurrentPasswordTitle.Location = new System.Drawing.Point(21, 590);
            this.lblCurrentPasswordTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCurrentPasswordTitle.Name = "lblCurrentPasswordTitle";
            this.lblCurrentPasswordTitle.Size = new System.Drawing.Size(276, 33);
            this.lblCurrentPasswordTitle.TabIndex = 56;
            this.lblCurrentPasswordTitle.Text = "Current Password :";
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
            this.btnSave.Location = new System.Drawing.Point(952, 824);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(152, 42);
            this.btnSave.TabIndex = 62;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.btnClose.Location = new System.Drawing.Point(770, 824);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(152, 42);
            this.btnClose.TabIndex = 60;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbNewPassword
            // 
            this.pbNewPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbNewPassword.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbNewPassword.Location = new System.Drawing.Point(311, 663);
            this.pbNewPassword.Name = "pbNewPassword";
            this.pbNewPassword.Size = new System.Drawing.Size(32, 32);
            this.pbNewPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbNewPassword.TabIndex = 64;
            this.pbNewPassword.TabStop = false;
            // 
            // lblManagePeopleTitle
            // 
            this.lblManagePeopleTitle.AutoSize = true;
            this.lblManagePeopleTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblManagePeopleTitle.Location = new System.Drawing.Point(21, 11);
            this.lblManagePeopleTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManagePeopleTitle.Name = "lblManagePeopleTitle";
            this.lblManagePeopleTitle.Size = new System.Drawing.Size(175, 29);
            this.lblManagePeopleTitle.TabIndex = 65;
            this.lblManagePeopleTitle.Text = "Manage People";
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
            this.btnExit.Location = new System.Drawing.Point(1065, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 66;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // erTextBox
            // 
            this.erTextBox.ContainerControl = this;
            // 
            // uctrlUserDetails
            // 
            this.uctrlUserDetails.AutoScroll = true;
            this.uctrlUserDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlUserDetails.Font = new System.Drawing.Font("Tahoma", 18F);
            this.uctrlUserDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.uctrlUserDetails.Location = new System.Drawing.Point(21, 81);
            this.uctrlUserDetails.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.uctrlUserDetails.Name = "uctrlUserDetails";
            this.uctrlUserDetails.Size = new System.Drawing.Size(1083, 461);
            this.uctrlUserDetails.TabIndex = 0;
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1121, 890);
            this.ControlBox = false;
            this.Controls.Add(this.lblManagePeopleTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pbNewPassword);
            this.Controls.Add(this.pbCurrentPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtPasswordConfirmation);
            this.Controls.Add(this.pbConfirmPassword);
            this.Controls.Add(this.txtCurrentPassword);
            this.Controls.Add(this.lblConfirmPasswordTitle);
            this.Controls.Add(this.lblNewPasswordTitle);
            this.Controls.Add(this.lblCurrentPasswordTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.uctrlUserDetails);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Name = "frmChangePassword";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrentPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbConfirmPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNewPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlUserDetails uctrlUserDetails;
        private System.Windows.Forms.PictureBox pbCurrentPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtPasswordConfirmation;
        private System.Windows.Forms.PictureBox pbConfirmPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.Label lblConfirmPasswordTitle;
        private System.Windows.Forms.Label lblNewPasswordTitle;
        private System.Windows.Forms.Label lblCurrentPasswordTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbNewPassword;
        private System.Windows.Forms.Label lblManagePeopleTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ErrorProvider erTextBox;
    }
}