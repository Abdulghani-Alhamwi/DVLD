namespace DVLDPresentationLayer.Licenses
{
    partial class frmIntLicenseApplications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntLicenseApplications));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbIsActive = new System.Windows.Forms.ComboBox();
            this.pbInternationalLicense = new System.Windows.Forms.PictureBox();
            this.btnAddInternationalLicenseApp = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvIntLicenseApplications = new System.Windows.Forms.DataGridView();
            this.cmsInternationalLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowPersonDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowLicenseDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFormBigTitle = new System.Windows.Forms.Label();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitleFilterBy = new System.Windows.Forms.Label();
            this.pbManageInternationalLicenses = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntLicenseApplications)).BeginInit();
            this.cmsInternationalLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbManageInternationalLicenses)).BeginInit();
            this.SuspendLayout();
            // 
            // cbIsActive
            // 
            this.cbIsActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.cbIsActive.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsActive.Font = new System.Drawing.Font("Tahoma", 21F);
            this.cbIsActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.cbIsActive.FormattingEnabled = true;
            this.cbIsActive.Location = new System.Drawing.Point(454, 377);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(182, 42);
            this.cbIsActive.TabIndex = 71;
            this.cbIsActive.Visible = false;
            this.cbIsActive.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawComboBoxItems);
            this.cbIsActive.DropDown += new System.EventHandler(this.ComboBoxes_DropDown);
            this.cbIsActive.SelectedIndexChanged += new System.EventHandler(this.cbIsActive_SelectedIndexChanged);
            this.cbIsActive.DropDownClosed += new System.EventHandler(this.cbIsActive_DropDownClosed);
            // 
            // pbInternationalLicense
            // 
            this.pbInternationalLicense.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbInternationalLicense.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbInternationalLicense.Image = ((System.Drawing.Image)(resources.GetObject("pbInternationalLicense.Image")));
            this.pbInternationalLicense.ImageLocation = "";
            this.pbInternationalLicense.Location = new System.Drawing.Point(768, 114);
            this.pbInternationalLicense.Margin = new System.Windows.Forms.Padding(2);
            this.pbInternationalLicense.Name = "pbInternationalLicense";
            this.pbInternationalLicense.Size = new System.Drawing.Size(58, 55);
            this.pbInternationalLicense.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbInternationalLicense.TabIndex = 70;
            this.pbInternationalLicense.TabStop = false;
            // 
            // btnAddInternationalLicenseApp
            // 
            this.btnAddInternationalLicenseApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddInternationalLicenseApp.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnAddInternationalLicenseApp.FlatAppearance.BorderSize = 2;
            this.btnAddInternationalLicenseApp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddInternationalLicenseApp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddInternationalLicenseApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddInternationalLicenseApp.Image = ((System.Drawing.Image)(resources.GetObject("btnAddInternationalLicenseApp.Image")));
            this.btnAddInternationalLicenseApp.Location = new System.Drawing.Point(1216, 349);
            this.btnAddInternationalLicenseApp.Name = "btnAddInternationalLicenseApp";
            this.btnAddInternationalLicenseApp.Size = new System.Drawing.Size(111, 70);
            this.btnAddInternationalLicenseApp.TabIndex = 59;
            this.btnAddInternationalLicenseApp.UseVisualStyleBackColor = true;
            this.btnAddInternationalLicenseApp.Click += new System.EventHandler(this.btnAddIntLicenseApplication_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1161, 763);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 62;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvIntLicenseApplications
            // 
            this.dgvIntLicenseApplications.AllowUserToAddRows = false;
            this.dgvIntLicenseApplications.AllowUserToDeleteRows = false;
            this.dgvIntLicenseApplications.AllowUserToOrderColumns = true;
            this.dgvIntLicenseApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIntLicenseApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvIntLicenseApplications.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 18F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIntLicenseApplications.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIntLicenseApplications.ColumnHeadersHeight = 40;
            this.dgvIntLicenseApplications.ContextMenuStrip = this.cmsInternationalLicense;
            this.dgvIntLicenseApplications.Location = new System.Drawing.Point(40, 441);
            this.dgvIntLicenseApplications.Margin = new System.Windows.Forms.Padding(2);
            this.dgvIntLicenseApplications.Name = "dgvIntLicenseApplications";
            this.dgvIntLicenseApplications.ReadOnly = true;
            this.dgvIntLicenseApplications.RowHeadersWidth = 72;
            this.dgvIntLicenseApplications.RowTemplate.Height = 32;
            this.dgvIntLicenseApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIntLicenseApplications.Size = new System.Drawing.Size(1287, 296);
            this.dgvIntLicenseApplications.StandardTab = true;
            this.dgvIntLicenseApplications.TabIndex = 58;
            this.dgvIntLicenseApplications.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvInternationalLicenses_Scroll);
            this.dgvIntLicenseApplications.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvInternationalLicenses_KeyDown);
            // 
            // cmsInternationalLicense
            // 
            this.cmsInternationalLicense.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmsInternationalLicense.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmsInternationalLicense.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsInternationalLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowPersonDetails,
            this.tsmiShowLicenseDetails,
            this.tsmiShowPersonLicenseHistory});
            this.cmsInternationalLicense.Name = "cmsPeopleMenu";
            this.cmsInternationalLicense.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsInternationalLicense.Size = new System.Drawing.Size(323, 112);
            this.cmsInternationalLicense.Paint += new System.Windows.Forms.PaintEventHandler(this.cmsInternationalLicense_Paint);
            // 
            // tsmiShowPersonDetails
            // 
            this.tsmiShowPersonDetails.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiShowPersonDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiShowPersonDetails.Image = global::DVLDPresentationLayer.Properties.Resources.PersonDetails_32;
            this.tsmiShowPersonDetails.Name = "tsmiShowPersonDetails";
            this.tsmiShowPersonDetails.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiShowPersonDetails.Size = new System.Drawing.Size(322, 36);
            this.tsmiShowPersonDetails.Text = "Show Person Details";
            this.tsmiShowPersonDetails.Click += new System.EventHandler(this.tsmiShowPersonDetails_Click);
            // 
            // tsmiShowLicenseDetails
            // 
            this.tsmiShowLicenseDetails.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiShowLicenseDetails.Image = global::DVLDPresentationLayer.Properties.Resources.License_View_32;
            this.tsmiShowLicenseDetails.Name = "tsmiShowLicenseDetails";
            this.tsmiShowLicenseDetails.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiShowLicenseDetails.Size = new System.Drawing.Size(322, 36);
            this.tsmiShowLicenseDetails.Text = "Show License Details";
            this.tsmiShowLicenseDetails.Click += new System.EventHandler(this.tsmiShowLicenseDetails_Click);
            // 
            // tsmiShowPersonLicenseHistory
            // 
            this.tsmiShowPersonLicenseHistory.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tsmiShowPersonLicenseHistory.Image = global::DVLDPresentationLayer.Properties.Resources.PersonLicenseHistory_32;
            this.tsmiShowPersonLicenseHistory.Name = "tsmiShowPersonLicenseHistory";
            this.tsmiShowPersonLicenseHistory.Padding = new System.Windows.Forms.Padding(0);
            this.tsmiShowPersonLicenseHistory.Size = new System.Drawing.Size(322, 36);
            this.tsmiShowPersonLicenseHistory.Text = "Show Person License History";
            this.tsmiShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmiShowPersonLicenseHistory_Click);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFormTitle.Location = new System.Drawing.Point(27, 15);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(366, 29);
            this.lblFormTitle.TabIndex = 64;
            this.lblFormTitle.Text = "International License Applications";
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
            this.btnExit.Location = new System.Drawing.Point(1313, 15);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 63;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFormBigTitle
            // 
            this.lblFormBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblFormBigTitle.AutoSize = true;
            this.lblFormBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblFormBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblFormBigTitle.Location = new System.Drawing.Point(312, 263);
            this.lblFormBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormBigTitle.Name = "lblFormBigTitle";
            this.lblFormBigTitle.Size = new System.Drawing.Size(768, 52);
            this.lblFormBigTitle.TabIndex = 65;
            this.lblFormBigTitle.Text = "International License Applications";
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblRecordsNumber.Location = new System.Drawing.Point(210, 770);
            this.lblRecordsNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(0, 31);
            this.lblRecordsNumber.TabIndex = 69;
            this.lblRecordsNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecordsTitle
            // 
            this.lblRecordsTitle.AutoSize = true;
            this.lblRecordsTitle.Font = new System.Drawing.Font("Tahoma", 19F, System.Drawing.FontStyle.Bold);
            this.lblRecordsTitle.Location = new System.Drawing.Point(40, 770);
            this.lblRecordsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsTitle.Name = "lblRecordsTitle";
            this.lblRecordsTitle.Size = new System.Drawing.Size(164, 31);
            this.lblRecordsTitle.TabIndex = 68;
            this.lblRecordsTitle.Text = "# Records :";
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.Font = new System.Drawing.Font("Tahoma", 21F);
            this.txtFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtFilter.Location = new System.Drawing.Point(454, 378);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(344, 41);
            this.txtFilter.TabIndex = 61;
            this.txtFilter.Visible = false;
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            this.txtFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyUp);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.cbFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Tahoma", 21F);
            this.cbFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(209, 377);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(226, 42);
            this.cbFilterBy.TabIndex = 60;
            this.cbFilterBy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawComboBoxItems);
            this.cbFilterBy.DropDown += new System.EventHandler(this.ComboBoxes_DropDown);
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.DropDownClosed += new System.EventHandler(this.cbFilterBy_DropDownClosed);
            // 
            // lblTitleFilterBy
            // 
            this.lblTitleFilterBy.AutoSize = true;
            this.lblTitleFilterBy.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitleFilterBy.Location = new System.Drawing.Point(40, 386);
            this.lblTitleFilterBy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitleFilterBy.Name = "lblTitleFilterBy";
            this.lblTitleFilterBy.Size = new System.Drawing.Size(147, 33);
            this.lblTitleFilterBy.TabIndex = 67;
            this.lblTitleFilterBy.Text = "Filter By :";
            // 
            // pbManageInternationalLicenses
            // 
            this.pbManageInternationalLicenses.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbManageInternationalLicenses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbManageInternationalLicenses.Image = ((System.Drawing.Image)(resources.GetObject("pbManageInternationalLicenses.Image")));
            this.pbManageInternationalLicenses.ImageLocation = "";
            this.pbManageInternationalLicenses.Location = new System.Drawing.Point(561, 74);
            this.pbManageInternationalLicenses.Margin = new System.Windows.Forms.Padding(2);
            this.pbManageInternationalLicenses.Name = "pbManageInternationalLicenses";
            this.pbManageInternationalLicenses.Size = new System.Drawing.Size(265, 169);
            this.pbManageInternationalLicenses.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbManageInternationalLicenses.TabIndex = 66;
            this.pbManageInternationalLicenses.TabStop = false;
            // 
            // frmIntLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1368, 822);
            this.ControlBox = false;
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.pbInternationalLicense);
            this.Controls.Add(this.btnAddInternationalLicenseApp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvIntLicenseApplications);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblFormBigTitle);
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.lblRecordsTitle);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitleFilterBy);
            this.Controls.Add(this.pbManageInternationalLicenses);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmIntLicenseApplications";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmInternationalLicensesManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbInternationalLicense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntLicenseApplications)).EndInit();
            this.cmsInternationalLicense.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbManageInternationalLicenses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbIsActive;
        private System.Windows.Forms.PictureBox pbInternationalLicense;
        private System.Windows.Forms.Button btnAddInternationalLicenseApp;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvIntLicenseApplications;
        private System.Windows.Forms.ContextMenuStrip cmsInternationalLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPersonDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowLicenseDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowPersonLicenseHistory;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblFormBigTitle;
        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.Label lblRecordsTitle;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitleFilterBy;
        private System.Windows.Forms.PictureBox pbManageInternationalLicenses;
    }
}