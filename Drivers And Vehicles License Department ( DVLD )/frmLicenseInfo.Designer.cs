namespace DVLDPresentationLayer
{
    partial class frmLicenseInfo
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
            this.lblFormBigTitle = new System.Windows.Forms.Label();
            this.pbLicenseView = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.uctrlDriverLicenseInfo = new DVLDPresentationLayer.ctrlDriverLicenseInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pbLicenseView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFormTitle.Location = new System.Drawing.Point(11, 7);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(142, 29);
            this.lblFormTitle.TabIndex = 42;
            this.lblFormTitle.Text = "License Info";
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
            this.btnExit.Location = new System.Drawing.Point(1128, 7);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 41;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFormBigTitle
            // 
            this.lblFormBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFormBigTitle.AutoSize = true;
            this.lblFormBigTitle.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold);
            this.lblFormBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblFormBigTitle.Location = new System.Drawing.Point(387, 163);
            this.lblFormBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormBigTitle.Name = "lblFormBigTitle";
            this.lblFormBigTitle.Size = new System.Drawing.Size(403, 48);
            this.lblFormBigTitle.TabIndex = 43;
            this.lblFormBigTitle.Text = "Driver License Info";
            // 
            // pbLicenseView
            // 
            this.pbLicenseView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbLicenseView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbLicenseView.Image = global::DVLDPresentationLayer.Properties.Resources.LicenseView_400;
            this.pbLicenseView.Location = new System.Drawing.Point(484, 36);
            this.pbLicenseView.Margin = new System.Windows.Forms.Padding(2);
            this.pbLicenseView.Name = "pbLicenseView";
            this.pbLicenseView.Size = new System.Drawing.Size(208, 125);
            this.pbLicenseView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLicenseView.TabIndex = 44;
            this.pbLicenseView.TabStop = false;
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
            this.btnClose.Image = global::DVLDPresentationLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(998, 679);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 178;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // uctrlDriverLicenseInfo
            // 
            this.uctrlDriverLicenseInfo.AutoScroll = true;
            this.uctrlDriverLicenseInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlDriverLicenseInfo.Font = new System.Drawing.Font("Tahoma", 18F);
            this.uctrlDriverLicenseInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlDriverLicenseInfo.Location = new System.Drawing.Point(10, 230);
            this.uctrlDriverLicenseInfo.Name = "uctrlDriverLicenseInfo";
            this.uctrlDriverLicenseInfo.Size = new System.Drawing.Size(1157, 440);
            this.uctrlDriverLicenseInfo.TabIndex = 45;
            // 
            // frmLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1179, 736);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.uctrlDriverLicenseInfo);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pbLicenseView);
            this.Controls.Add(this.lblFormBigTitle);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmLicenseInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbLicenseView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pbLicenseView;
        private System.Windows.Forms.Label lblFormBigTitle;
        private ctrlDriverLicenseInfo uctrlDriverLicenseInfo;
        private System.Windows.Forms.Button btnClose;
    }
}