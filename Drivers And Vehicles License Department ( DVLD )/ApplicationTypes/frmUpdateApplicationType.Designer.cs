namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class frmUpdateApplicationType
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
            this.lblUpdateApplicationTitle = new System.Windows.Forms.Label();
            this.lblUpdateApplicationBigTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtFees = new System.Windows.Forms.TextBox();
            this.lblFees = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblIDTitle = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbApplicationTitle = new System.Windows.Forms.PictureBox();
            this.pbApplicationFees = new System.Windows.Forms.PictureBox();
            this.ertxtBox = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBox)).BeginInit();
            this.SuspendLayout();
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
            this.btnExit.Location = new System.Drawing.Point(588, 11);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblUpdateApplicationTitle
            // 
            this.lblUpdateApplicationTitle.AutoSize = true;
            this.lblUpdateApplicationTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblUpdateApplicationTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblUpdateApplicationTitle.Location = new System.Drawing.Point(8, 9);
            this.lblUpdateApplicationTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateApplicationTitle.Name = "lblUpdateApplicationTitle";
            this.lblUpdateApplicationTitle.Size = new System.Drawing.Size(270, 29);
            this.lblUpdateApplicationTitle.TabIndex = 46;
            this.lblUpdateApplicationTitle.Text = "Update Application Type";
            // 
            // lblUpdateApplicationBigTitle
            // 
            this.lblUpdateApplicationBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUpdateApplicationBigTitle.AutoSize = true;
            this.lblUpdateApplicationBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblUpdateApplicationBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblUpdateApplicationBigTitle.Location = new System.Drawing.Point(28, 77);
            this.lblUpdateApplicationBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateApplicationBigTitle.Name = "lblUpdateApplicationBigTitle";
            this.lblUpdateApplicationBigTitle.Size = new System.Drawing.Size(552, 52);
            this.lblUpdateApplicationBigTitle.TabIndex = 47;
            this.lblUpdateApplicationBigTitle.Text = "Update Application Type";
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtTitle.Location = new System.Drawing.Point(179, 244);
            this.txtTitle.MaxLength = 200;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtTitle.Size = new System.Drawing.Size(423, 36);
            this.txtTitle.TabIndex = 0;
            this.txtTitle.Tag = "First Name";
            // 
            // txtFees
            // 
            this.txtFees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtFees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtFees.Location = new System.Drawing.Point(179, 309);
            this.txtFees.MaxLength = 200;
            this.txtFees.Name = "txtFees";
            this.txtFees.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtFees.Size = new System.Drawing.Size(423, 36);
            this.txtFees.TabIndex = 1;
            this.txtFees.Tag = "First Name";
            this.txtFees.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFees_KeyDown);
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblFees.Location = new System.Drawing.Point(26, 311);
            this.lblFees.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(95, 33);
            this.lblFees.TabIndex = 57;
            this.lblFees.Text = "Fees :";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(28, 246);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(93, 33);
            this.lblTitle.TabIndex = 56;
            this.lblTitle.Text = "Title :";
            // 
            // lblIDTitle
            // 
            this.lblIDTitle.AutoSize = true;
            this.lblIDTitle.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblIDTitle.Location = new System.Drawing.Point(55, 181);
            this.lblIDTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIDTitle.Name = "lblIDTitle";
            this.lblIDTitle.Size = new System.Drawing.Size(66, 33);
            this.lblIDTitle.TabIndex = 55;
            this.lblIDTitle.Text = "ID :";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblID.Location = new System.Drawing.Point(179, 181);
            this.lblID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(75, 33);
            this.lblID.TabIndex = 62;
            this.lblID.Text = "????";
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
            this.btnSave.Location = new System.Drawing.Point(436, 386);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(166, 45);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.btnClose.Location = new System.Drawing.Point(236, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbApplicationTitle
            // 
            this.pbApplicationTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbApplicationTitle.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.ApplicationTitle;
            this.pbApplicationTitle.Location = new System.Drawing.Point(131, 246);
            this.pbApplicationTitle.Name = "pbApplicationTitle";
            this.pbApplicationTitle.Size = new System.Drawing.Size(32, 32);
            this.pbApplicationTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbApplicationTitle.TabIndex = 59;
            this.pbApplicationTitle.TabStop = false;
            // 
            // pbApplicationFees
            // 
            this.pbApplicationFees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbApplicationFees.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.money_32;
            this.pbApplicationFees.Location = new System.Drawing.Point(131, 311);
            this.pbApplicationFees.Name = "pbApplicationFees";
            this.pbApplicationFees.Size = new System.Drawing.Size(32, 32);
            this.pbApplicationFees.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbApplicationFees.TabIndex = 58;
            this.pbApplicationFees.TabStop = false;
            // 
            // ertxtBox
            // 
            this.ertxtBox.ContainerControl = this;
            // 
            // frmUpdateApplicationType
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(633, 448);
            this.ControlBox = false;
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pbApplicationTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtFees);
            this.Controls.Add(this.pbApplicationFees);
            this.Controls.Add(this.lblFees);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblIDTitle);
            this.Controls.Add(this.lblUpdateApplicationBigTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblUpdateApplicationTitle);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Name = "frmUpdateApplicationType";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbApplicationFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblUpdateApplicationTitle;
        private System.Windows.Forms.Label lblUpdateApplicationBigTitle;
        private System.Windows.Forms.PictureBox pbApplicationTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtFees;
        private System.Windows.Forms.PictureBox pbApplicationFees;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblIDTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ErrorProvider ertxtBox;
    }
}