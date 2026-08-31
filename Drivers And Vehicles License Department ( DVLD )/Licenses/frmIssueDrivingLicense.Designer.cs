namespace DVLDPresentationLayer.LocalDrivingLicenseApplications
{
    partial class frmIssueDrivingLicense
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
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotesTitle = new System.Windows.Forms.Label();
            this.lblLicenseFees = new System.Windows.Forms.Label();
            this.lblTestFeesTitle = new System.Windows.Forms.Label();
            this.pbFees = new System.Windows.Forms.PictureBox();
            this.btnIssueLicense = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pbNotes = new System.Windows.Forms.PictureBox();
            this.uctrlDLApplicationInfo = new DVLDPresentationLayer.ctrLDLApplicationDetails();
            ((System.ComponentModel.ISupportInitialize)(this.pbFees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFormTitle.Location = new System.Drawing.Point(7, 9);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(468, 31);
            this.lblFormTitle.TabIndex = 166;
            this.lblFormTitle.Text = "Issue Driver License For The First Time";
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
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.btnExit.Location = new System.Drawing.Point(1153, 9);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 165;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNotes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Location = new System.Drawing.Point(185, 615);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(994, 168);
            this.txtNotes.TabIndex = 176;
            // 
            // lblNotesTitle
            // 
            this.lblNotesTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNotesTitle.AutoSize = true;
            this.lblNotesTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblNotesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblNotesTitle.Location = new System.Drawing.Point(26, 617);
            this.lblNotesTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblNotesTitle.Name = "lblNotesTitle";
            this.lblNotesTitle.Size = new System.Drawing.Size(95, 28);
            this.lblNotesTitle.TabIndex = 174;
            this.lblNotesTitle.Text = "Notes :";
            // 
            // lblLicenseFees
            // 
            this.lblLicenseFees.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLicenseFees.AutoSize = true;
            this.lblLicenseFees.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblLicenseFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblLicenseFees.Location = new System.Drawing.Point(256, 562);
            this.lblLicenseFees.Margin = new System.Windows.Forms.Padding(0);
            this.lblLicenseFees.Name = "lblLicenseFees";
            this.lblLicenseFees.Size = new System.Drawing.Size(64, 28);
            this.lblLicenseFees.TabIndex = 180;
            this.lblLicenseFees.Text = "????";
            // 
            // lblTestFeesTitle
            // 
            this.lblTestFeesTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTestFeesTitle.AutoSize = true;
            this.lblTestFeesTitle.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblTestFeesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblTestFeesTitle.Location = new System.Drawing.Point(26, 562);
            this.lblTestFeesTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTestFeesTitle.Name = "lblTestFeesTitle";
            this.lblTestFeesTitle.Size = new System.Drawing.Size(174, 28);
            this.lblTestFeesTitle.TabIndex = 179;
            this.lblTestFeesTitle.Text = "License Fees :";
            // 
            // pbFees
            // 
            this.pbFees.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbFees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbFees.Image = global::DVLDPresentationLayer.Properties.Resources.money_32;
            this.pbFees.Location = new System.Drawing.Point(211, 560);
            this.pbFees.Name = "pbFees";
            this.pbFees.Size = new System.Drawing.Size(32, 32);
            this.pbFees.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbFees.TabIndex = 181;
            this.pbFees.TabStop = false;
            // 
            // btnIssueLicense
            // 
            this.btnIssueLicense.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnIssueLicense.FlatAppearance.BorderSize = 2;
            this.btnIssueLicense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnIssueLicense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnIssueLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssueLicense.Font = new System.Drawing.Font("Tahoma", 19F);
            this.btnIssueLicense.Image = global::DVLDPresentationLayer.Properties.Resources.License_Type_32;
            this.btnIssueLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueLicense.Location = new System.Drawing.Point(1013, 817);
            this.btnIssueLicense.Name = "btnIssueLicense";
            this.btnIssueLicense.Size = new System.Drawing.Size(166, 45);
            this.btnIssueLicense.TabIndex = 178;
            this.btnIssueLicense.Text = "Issue";
            this.btnIssueLicense.UseVisualStyleBackColor = true;
            this.btnIssueLicense.Click += new System.EventHandler(this.btnIssueLicense_Click);
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
            this.btnClose.Location = new System.Drawing.Point(810, 817);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 177;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbNotes
            // 
            this.pbNotes.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbNotes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbNotes.Image = global::DVLDPresentationLayer.Properties.Resources.Notes_32;
            this.pbNotes.Location = new System.Drawing.Point(133, 615);
            this.pbNotes.Name = "pbNotes";
            this.pbNotes.Size = new System.Drawing.Size(32, 32);
            this.pbNotes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbNotes.TabIndex = 175;
            this.pbNotes.TabStop = false;
            // 
            // uctrlDLApplicationInfo
            // 
            this.uctrlDLApplicationInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.uctrlDLApplicationInfo.AutoScroll = true;
            this.uctrlDLApplicationInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlDLApplicationInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlDLApplicationInfo.Location = new System.Drawing.Point(26, 88);
            this.uctrlDLApplicationInfo.Name = "uctrlDLApplicationInfo";
            this.uctrlDLApplicationInfo.Size = new System.Drawing.Size(1153, 452);
            this.uctrlDLApplicationInfo.TabIndex = 0;
            // 
            // frmIssueDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1206, 886);
            this.ControlBox = false;
            this.Controls.Add(this.pbFees);
            this.Controls.Add(this.lblLicenseFees);
            this.Controls.Add(this.lblTestFeesTitle);
            this.Controls.Add(this.btnIssueLicense);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.pbNotes);
            this.Controls.Add(this.lblNotesTitle);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.uctrlDLApplicationInfo);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmIssueDrivingLicense";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbFees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrLDLApplicationDetails uctrlDLApplicationInfo;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.PictureBox pbNotes;
        private System.Windows.Forms.Label lblNotesTitle;
        private System.Windows.Forms.Button btnIssueLicense;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbFees;
        private System.Windows.Forms.Label lblLicenseFees;
        private System.Windows.Forms.Label lblTestFeesTitle;
    }
}