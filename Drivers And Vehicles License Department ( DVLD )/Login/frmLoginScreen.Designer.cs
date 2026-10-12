namespace DVLDPresentationLayer
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
            this.pbLoginImage = new System.Windows.Forms.PictureBox();
            this.spcLoginInfo = new System.Windows.Forms.SplitContainer();
            this.chbRememberMe = new System.Windows.Forms.CheckBox();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pbPerson = new System.Windows.Forms.PictureBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPasswordTitle = new System.Windows.Forms.Label();
            this.lblUserNameTitle = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoginImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spcLoginInfo)).BeginInit();
            this.spcLoginInfo.Panel1.SuspendLayout();
            this.spcLoginInfo.Panel2.SuspendLayout();
            this.spcLoginInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLoginImage
            // 
            this.pbLoginImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLoginImage.Image = global::DVLDPresentationLayer.Properties.Resources.LoginImage;
            this.pbLoginImage.Location = new System.Drawing.Point(0, 0);
            this.pbLoginImage.Name = "pbLoginImage";
            this.pbLoginImage.Size = new System.Drawing.Size(500, 641);
            this.pbLoginImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLoginImage.TabIndex = 0;
            this.pbLoginImage.TabStop = false;
            // 
            // spcLoginInfo
            // 
            this.spcLoginInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.spcLoginInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcLoginInfo.Location = new System.Drawing.Point(0, 0);
            this.spcLoginInfo.Name = "spcLoginInfo";
            // 
            // spcLoginInfo.Panel1
            // 
            this.spcLoginInfo.Panel1.AutoScroll = true;
            this.spcLoginInfo.Panel1.Controls.Add(this.pbLoginImage);
            // 
            // spcLoginInfo.Panel2
            // 
            this.spcLoginInfo.Panel2.AutoScroll = true;
            this.spcLoginInfo.Panel2.Controls.Add(this.chbRememberMe);
            this.spcLoginInfo.Panel2.Controls.Add(this.pbPassword);
            this.spcLoginInfo.Panel2.Controls.Add(this.txtPassword);
            this.spcLoginInfo.Panel2.Controls.Add(this.pbPerson);
            this.spcLoginInfo.Panel2.Controls.Add(this.txtUserName);
            this.spcLoginInfo.Panel2.Controls.Add(this.lblPasswordTitle);
            this.spcLoginInfo.Panel2.Controls.Add(this.lblUserNameTitle);
            this.spcLoginInfo.Panel2.Controls.Add(this.btnLogin);
            this.spcLoginInfo.Panel2.Controls.Add(this.lblLoginTitle);
            this.spcLoginInfo.Panel2.Controls.Add(this.btnClose);
            this.spcLoginInfo.Size = new System.Drawing.Size(1149, 641);
            this.spcLoginInfo.SplitterDistance = 500;
            this.spcLoginInfo.SplitterWidth = 3;
            this.spcLoginInfo.TabIndex = 6;
            // 
            // chbRememberMe
            // 
            this.chbRememberMe.AutoSize = true;
            this.chbRememberMe.Font = new System.Drawing.Font("Tahoma", 19F);
            this.chbRememberMe.Location = new System.Drawing.Point(281, 357);
            this.chbRememberMe.Name = "chbRememberMe";
            this.chbRememberMe.Size = new System.Drawing.Size(200, 35);
            this.chbRememberMe.TabIndex = 3;
            this.chbRememberMe.Text = "Remember Me";
            this.chbRememberMe.UseVisualStyleBackColor = true;
            // 
            // pbPassword
            // 
            this.pbPassword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPassword.Image = global::DVLDPresentationLayer.Properties.Resources.Number_32;
            this.pbPassword.Location = new System.Drawing.Point(231, 302);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(32, 32);
            this.pbPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPassword.TabIndex = 70;
            this.pbPassword.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Tahoma", 19F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtPassword.Location = new System.Drawing.Point(281, 299);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPassword.Size = new System.Drawing.Size(316, 38);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Tag = "First Name";
            // 
            // pbPerson
            // 
            this.pbPerson.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbPerson.Image = global::DVLDPresentationLayer.Properties.Resources.Person_32;
            this.pbPerson.Location = new System.Drawing.Point(231, 230);
            this.pbPerson.Name = "pbPerson";
            this.pbPerson.Size = new System.Drawing.Size(32, 32);
            this.pbPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPerson.TabIndex = 69;
            this.pbPerson.TabStop = false;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Tahoma", 19F);
            this.txtUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtUserName.Location = new System.Drawing.Point(281, 227);
            this.txtUserName.MaxLength = 100;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtUserName.Size = new System.Drawing.Size(316, 38);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Tag = "First Name";
            // 
            // lblPasswordTitle
            // 
            this.lblPasswordTitle.AutoSize = true;
            this.lblPasswordTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblPasswordTitle.Location = new System.Drawing.Point(53, 302);
            this.lblPasswordTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPasswordTitle.Name = "lblPasswordTitle";
            this.lblPasswordTitle.Size = new System.Drawing.Size(165, 33);
            this.lblPasswordTitle.TabIndex = 68;
            this.lblPasswordTitle.Text = "Password :";
            // 
            // lblUserNameTitle
            // 
            this.lblUserNameTitle.AutoSize = true;
            this.lblUserNameTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblUserNameTitle.Location = new System.Drawing.Point(44, 230);
            this.lblUserNameTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserNameTitle.Name = "lblUserNameTitle";
            this.lblUserNameTitle.Size = new System.Drawing.Size(174, 33);
            this.lblUserNameTitle.TabIndex = 67;
            this.lblUserNameTitle.Text = "UserName :";
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLogin.FlatAppearance.BorderSize = 2;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnLogin.Image = global::DVLDPresentationLayer.Properties.Resources.sign_in_32;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(254, 453);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(170, 47);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = true;
            this.lblLoginTitle.Font = new System.Drawing.Font("Tahoma", 27F, System.Drawing.FontStyle.Bold);
            this.lblLoginTitle.Location = new System.Drawing.Point(131, 99);
            this.lblLoginTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(414, 43);
            this.lblLoginTitle.TabIndex = 66;
            this.lblLoginTitle.Text = "Login to your account";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.IndianRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLDPresentationLayer.Properties.Resources.closeBlack32;
            this.btnClose.Location = new System.Drawing.Point(582, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(48, 47);
            this.btnClose.TabIndex = 5;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmLoginScreen
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1149, 641);
            this.ControlBox = false;
            this.Controls.Add(this.spcLoginInfo);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLoginScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbLoginImage)).EndInit();
            this.spcLoginInfo.Panel1.ResumeLayout(false);
            this.spcLoginInfo.Panel2.ResumeLayout(false);
            this.spcLoginInfo.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcLoginInfo)).EndInit();
            this.spcLoginInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPerson)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLoginImage;
        private System.Windows.Forms.SplitContainer spcLoginInfo;
        private System.Windows.Forms.CheckBox chbRememberMe;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pbPerson;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPasswordTitle;
        private System.Windows.Forms.Label lblUserNameTitle;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.Button btnClose;
    }
}