namespace DVLDPresentationLayer
{
    partial class ctrlLDLicenseDetailsByFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnSearchForLicense = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.lblTitleLocalLicenseID = new System.Windows.Forms.Label();
            this.uctrlLDLDetails = new DVLDPresentationLayer.ctrlLDLicenseDetails();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnSearchForLicense);
            this.gbFilter.Controls.Add(this.txtLicenseID);
            this.gbFilter.Controls.Add(this.lblTitleLocalLicenseID);
            this.gbFilter.Font = new System.Drawing.Font("Tahoma", 18F);
            this.gbFilter.Location = new System.Drawing.Point(10, 1);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(754, 108);
            this.gbFilter.TabIndex = 43;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnSearchForLicense
            // 
            this.btnSearchForLicense.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnSearchForLicense.FlatAppearance.BorderSize = 2;
            this.btnSearchForLicense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearchForLicense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearchForLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchForLicense.Image = global::DVLDPresentationLayer.Properties.Resources.License_View_32;
            this.btnSearchForLicense.Location = new System.Drawing.Point(643, 39);
            this.btnSearchForLicense.Name = "btnSearchForLicense";
            this.btnSearchForLicense.Size = new System.Drawing.Size(74, 50);
            this.btnSearchForLicense.TabIndex = 2;
            this.btnSearchForLicense.UseVisualStyleBackColor = true;
            this.btnSearchForLicense.Click += new System.EventHandler(this.btnSearchForLicense_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtLicenseID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicenseID.Font = new System.Drawing.Font("Tahoma", 17F);
            this.txtLicenseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtLicenseID.Location = new System.Drawing.Point(240, 47);
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(373, 35);
            this.txtLicenseID.TabIndex = 1;
            // 
            // lblTitleLocalLicenseID
            // 
            this.lblTitleLocalLicenseID.AutoSize = true;
            this.lblTitleLocalLicenseID.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitleLocalLicenseID.Location = new System.Drawing.Point(10, 50);
            this.lblTitleLocalLicenseID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitleLocalLicenseID.Name = "lblTitleLocalLicenseID";
            this.lblTitleLocalLicenseID.Size = new System.Drawing.Size(216, 28);
            this.lblTitleLocalLicenseID.TabIndex = 37;
            this.lblTitleLocalLicenseID.Text = "Local License ID :";
            // 
            // uctrlLDLDetails
            // 
            this.uctrlLDLDetails.AutoScroll = true;
            this.uctrlLDLDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlLDLDetails.Font = new System.Drawing.Font("Tahoma", 18F);
            this.uctrlLDLDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlLDLDetails.Location = new System.Drawing.Point(3, 111);
            this.uctrlLDLDetails.Name = "uctrlLDLDetails";
            this.uctrlLDLDetails.Size = new System.Drawing.Size(1154, 435);
            this.uctrlLDLDetails.TabIndex = 44;
            // 
            // ctrlLDLDetailsByFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.uctrlLDLDetails);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ctrlLDLDetailsByFilter";
            this.Size = new System.Drawing.Size(1157, 549);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.Button btnSearchForLicense;
        private System.Windows.Forms.TextBox txtLicenseID;
        private System.Windows.Forms.Label lblTitleLocalLicenseID;
        private ctrlLDLicenseDetails uctrlLDLDetails;
    }
}
