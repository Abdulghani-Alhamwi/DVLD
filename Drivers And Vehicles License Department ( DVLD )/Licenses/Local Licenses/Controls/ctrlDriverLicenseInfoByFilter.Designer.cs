namespace DVLDPresentationLayer
{
    partial class ctrlDriverLicenseInfoByFilter
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
            this.components = new System.ComponentModel.Container();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.lblTitleLocalLicenseID = new System.Windows.Forms.Label();
            this.ertxtBoxInput = new System.Windows.Forms.ErrorProvider(this.components);
            this.uctrlLocalDrivingLicenseInfo = new DVLDPresentationLayer.ctrlDriverLocalLicenseInfo();
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBoxInput)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFind);
            this.gbFilter.Controls.Add(this.txtLicenseID);
            this.gbFilter.Controls.Add(this.lblTitleLocalLicenseID);
            this.gbFilter.Font = new System.Drawing.Font("Tahoma", 18F);
            this.gbFilter.Location = new System.Drawing.Point(10, 1);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(754, 92);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnFind
            // 
            this.btnFind.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnFind.FlatAppearance.BorderSize = 2;
            this.btnFind.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFind.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Image = global::DVLDPresentationLayer.Properties.Resources.License_View_32;
            this.btnFind.Location = new System.Drawing.Point(643, 28);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(74, 50);
            this.btnFind.TabIndex = 2;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtLicenseID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicenseID.Font = new System.Drawing.Font("Tahoma", 16F);
            this.txtLicenseID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtLicenseID.Location = new System.Drawing.Point(240, 37);
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(373, 33);
            this.txtLicenseID.TabIndex = 0;
            this.txtLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseID_KeyPress);
            this.txtLicenseID.Validating += new System.ComponentModel.CancelEventHandler(this.txtLicenseID_Validating);
            // 
            // lblTitleLocalLicenseID
            // 
            this.lblTitleLocalLicenseID.AutoSize = true;
            this.lblTitleLocalLicenseID.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitleLocalLicenseID.Location = new System.Drawing.Point(10, 40);
            this.lblTitleLocalLicenseID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitleLocalLicenseID.Name = "lblTitleLocalLicenseID";
            this.lblTitleLocalLicenseID.Size = new System.Drawing.Size(208, 27);
            this.lblTitleLocalLicenseID.TabIndex = 37;
            this.lblTitleLocalLicenseID.Text = "Local License ID :";
            // 
            // ertxtBoxInput
            // 
            this.ertxtBoxInput.ContainerControl = this;
            // 
            // uctrlLocalDrivingLicenseInfo
            // 
            this.uctrlLocalDrivingLicenseInfo.AutoScroll = true;
            this.uctrlLocalDrivingLicenseInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlLocalDrivingLicenseInfo.Font = new System.Drawing.Font("Tahoma", 18F);
            this.uctrlLocalDrivingLicenseInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlLocalDrivingLicenseInfo.Location = new System.Drawing.Point(3, 100);
            this.uctrlLocalDrivingLicenseInfo.Name = "uctrlLocalDrivingLicenseInfo";
            this.uctrlLocalDrivingLicenseInfo.Size = new System.Drawing.Size(1154, 410);
            this.uctrlLocalDrivingLicenseInfo.TabIndex = 44;
            // 
            // ctrlDriverLicenseInfoByFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.uctrlLocalDrivingLicenseInfo);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "ctrlDriverLicenseInfoByFilter";
            this.Size = new System.Drawing.Size(1157, 510);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ertxtBoxInput)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label lblTitleLocalLicenseID;
        public ctrlDriverLocalLicenseInfo uctrlLocalDrivingLicenseInfo;
        private System.Windows.Forms.ErrorProvider ertxtBoxInput;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox txtLicenseID;
    }
}
