namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class frmLoginScreen
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
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.chbRememberMe = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPasswordTitle = new System.Windows.Forms.Label();
            this.lblUserNameTitle = new System.Windows.Forms.Label();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.pbPerson = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbLoginImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoginImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = true;
            this.lblLoginTitle.Font = new System.Drawing.Font("Tahoma", 27F, System.Drawing.FontStyle.Bold);
            this.lblLoginTitle.Location = new System.Drawing.Point(618, 100);
            this.lblLoginTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(414, 43);
            this.lblLoginTitle.TabIndex = 35;
            this.lblLoginTitle.Text = "Login to your account";
            // 
            // chbRememberMe
            // 
            this.chbRememberMe.AutoSize = true;
            this.chbRememberMe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbRememberMe.Font = new System.Drawing.Font("Tahoma", 19F);
            this.chbRememberMe.Location = new System.Drawing.Point(771, 358);
            this.chbRememberMe.Name = "chbRememberMe";
            this.chbRememberMe.Size = new System.Drawing.Size(197, 35);
            this.chbRememberMe.TabIndex = 55;
            this.chbRememberMe.Text = "Remember Me";
            this.chbRememberMe.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPassword.Location = new System.Drawing.Point(771, 301);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPassword.Size = new System.Drawing.Size(316, 36);
            this.txtPassword.TabIndex = 54;
            this.txtPassword.Tag = "First Name";
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtUserName.Location = new System.Drawing.Point(771, 229);
            this.txtUserName.MaxLength = 20;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtUserName.Size = new System.Drawing.Size(316, 36);
            this.txtUserName.TabIndex = 53;
            this.txtUserName.Tag = "First Name";
            // 
            // lblPasswordTitle
            // 
            this.lblPasswordTitle.AutoSize = true;
            this.lblPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblPasswordTitle.Location = new System.Drawing.Point(531, 303);
            this.lblPasswordTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPasswordTitle.Name = "lblPasswordTitle";
            this.lblPasswordTitle.Size = new System.Drawing.Size(165, 33);
            this.lblPasswordTitle.TabIndex = 57;
            this.lblPasswordTitle.Text = "Password :";
            // 
            // lblUserNameTitle
            // 
            this.lblUserNameTitle.AutoSize = true;
            this.lblUserNameTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblUserNameTitle.Location = new System.Drawing.Point(531, 231);
            this.lblUserNameTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserNameTitle.Name = "lblUserNameTitle";
            this.lblUserNameTitle.Size = new System.Drawing.Size(174, 33);
            this.lblUserNameTitle.TabIndex = 56;
            this.lblUserNameTitle.Text = "UserName :";
            // 
            // pbPassword
            // 
            this.pbPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPassword.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Number_32;
            this.pbPassword.Location = new System.Drawing.Point(718, 303);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(32, 32);
            this.pbPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPassword.TabIndex = 60;
            this.pbPassword.TabStop = false;
            // 
            // pbPerson
            // 
            this.pbPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPerson.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Person_32;
            this.pbPerson.Location = new System.Drawing.Point(718, 231);
            this.pbPerson.Name = "pbPerson";
            this.pbPerson.Size = new System.Drawing.Size(32, 32);
            this.pbPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPerson.TabIndex = 58;
            this.pbPerson.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLogin.FlatAppearance.BorderSize = 2;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnLogin.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.sign_in_32;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(740, 454);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(170, 47);
            this.btnLogin.TabIndex = 59;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.closeBlack32;
            this.btnClose.Location = new System.Drawing.Point(1088, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(49, 44);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbLoginImage
            // 
            this.pbLoginImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbLoginImage.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.LoginImage;
            this.pbLoginImage.Location = new System.Drawing.Point(0, 0);
            this.pbLoginImage.Name = "pbLoginImage";
            this.pbLoginImage.Size = new System.Drawing.Size(477, 641);
            this.pbLoginImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLoginImage.TabIndex = 0;
            this.pbLoginImage.TabStop = false;
            // 
            // frmLoginScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1149, 641);
            this.ControlBox = false;
            this.Controls.Add(this.chbRememberMe);
            this.Controls.Add(this.pbPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.pbPerson);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblPasswordTitle);
            this.Controls.Add(this.lblUserNameTitle);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblLoginTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbLoginImage);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLoginScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoginImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLoginImage;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.CheckBox chbRememberMe;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pbPerson;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPasswordTitle;
        private System.Windows.Forms.Label lblUserNameTitle;
        private System.Windows.Forms.Button btnLogin;
    }
}