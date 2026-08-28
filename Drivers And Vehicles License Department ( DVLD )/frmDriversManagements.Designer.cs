namespace DVLDPresentationLayer
{
    partial class frmDriversManagements
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitleFilterBy = new System.Windows.Forms.Label();
            this.pbDrivers = new System.Windows.Forms.PictureBox();
            this.tsmiPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddNewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPeopleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvDrivers = new System.Windows.Forms.DataGridView();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFormBigTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbDrivers)).BeginInit();
            this.cmsPeopleMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrivers)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblRecordsNumber.Location = new System.Drawing.Point(205, 716);
            this.lblRecordsNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(0, 31);
            this.lblRecordsNumber.TabIndex = 54;
            this.lblRecordsNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRecordsTitle
            // 
            this.lblRecordsTitle.AutoSize = true;
            this.lblRecordsTitle.Font = new System.Drawing.Font("Tahoma", 19F, System.Drawing.FontStyle.Bold);
            this.lblRecordsTitle.Location = new System.Drawing.Point(35, 716);
            this.lblRecordsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsTitle.Name = "lblRecordsTitle";
            this.lblRecordsTitle.Size = new System.Drawing.Size(164, 31);
            this.lblRecordsTitle.TabIndex = 53;
            this.lblRecordsTitle.Text = "# Records :";
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.Font = new System.Drawing.Font("Tahoma", 21F);
            this.txtFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtFilter.Location = new System.Drawing.Point(444, 355);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(344, 41);
            this.txtFilter.TabIndex = 46;
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
            this.cbFilterBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(199, 354);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(226, 42);
            this.cbFilterBy.TabIndex = 45;
            this.cbFilterBy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFilterBy_DrawItem);
            this.cbFilterBy.DropDown += new System.EventHandler(this.cbFilterBy_DropDown);
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.DropDownClosed += new System.EventHandler(this.cbFilterBy_DropDownClosed);
            // 
            // lblTitleFilterBy
            // 
            this.lblTitleFilterBy.AutoSize = true;
            this.lblTitleFilterBy.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitleFilterBy.Location = new System.Drawing.Point(35, 359);
            this.lblTitleFilterBy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitleFilterBy.Name = "lblTitleFilterBy";
            this.lblTitleFilterBy.Size = new System.Drawing.Size(147, 33);
            this.lblTitleFilterBy.TabIndex = 52;
            this.lblTitleFilterBy.Text = "Filter By :";
            // 
            // pbDrivers
            // 
            this.pbDrivers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbDrivers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbDrivers.Image = global::DVLDPresentationLayer.Properties.Resources.Driver_Main;
            this.pbDrivers.Location = new System.Drawing.Point(588, 60);
            this.pbDrivers.Margin = new System.Windows.Forms.Padding(2);
            this.pbDrivers.Name = "pbDrivers";
            this.pbDrivers.Size = new System.Drawing.Size(265, 156);
            this.pbDrivers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDrivers.TabIndex = 51;
            this.pbDrivers.TabStop = false;
            // 
            // tsmiPhoneCall
            // 
            this.tsmiPhoneCall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiPhoneCall.Image = global::DVLDPresentationLayer.Properties.Resources.call_32;
            this.tsmiPhoneCall.Name = "tsmiPhoneCall";
            this.tsmiPhoneCall.Size = new System.Drawing.Size(222, 38);
            this.tsmiPhoneCall.Text = "Phone Call";
            // 
            // tsmiSendEmail
            // 
            this.tsmiSendEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiSendEmail.Image = global::DVLDPresentationLayer.Properties.Resources.send_email_32;
            this.tsmiSendEmail.Name = "tsmiSendEmail";
            this.tsmiSendEmail.Size = new System.Drawing.Size(222, 38);
            this.tsmiSendEmail.Text = "Send Email";
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiDelete.Image = global::DVLDPresentationLayer.Properties.Resources.Delete_32;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(222, 38);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiEdit.Image = global::DVLDPresentationLayer.Properties.Resources.edit_32;
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(222, 38);
            this.tsmiEdit.Text = "Edit";
            this.tsmiEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsmiAddNewUser
            // 
            this.tsmiAddNewUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiAddNewUser.Image = global::DVLDPresentationLayer.Properties.Resources.Add_Person_40;
            this.tsmiAddNewUser.Name = "tsmiAddNewUser";
            this.tsmiAddNewUser.Size = new System.Drawing.Size(222, 38);
            this.tsmiAddNewUser.Text = "Add New User";
            this.tsmiAddNewUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // tsmiShowDetails
            // 
            this.tsmiShowDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiShowDetails.Image = global::DVLDPresentationLayer.Properties.Resources.PersonDetails_32;
            this.tsmiShowDetails.Name = "tsmiShowDetails";
            this.tsmiShowDetails.Size = new System.Drawing.Size(222, 38);
            this.tsmiShowDetails.Text = "Show Details";
            // 
            // cmsPeopleMenu
            // 
            this.cmsPeopleMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmsPeopleMenu.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmsPeopleMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsPeopleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowDetails,
            this.tsSeparator1,
            this.tsmiAddNewUser,
            this.tsmiEdit,
            this.tsmiDelete,
            this.tsSeparator2,
            this.tsmiSendEmail,
            this.tsmiPhoneCall});
            this.cmsPeopleMenu.Name = "cmsPeopleMenu";
            this.cmsPeopleMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsPeopleMenu.Size = new System.Drawing.Size(223, 244);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnClose.Image = global::DVLDPresentationLayer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1240, 709);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 47;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvDrivers
            // 
            this.dgvDrivers.AllowUserToAddRows = false;
            this.dgvDrivers.AllowUserToDeleteRows = false;
            this.dgvDrivers.AllowUserToOrderColumns = true;
            this.dgvDrivers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDrivers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDrivers.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 18F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDrivers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDrivers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrivers.ContextMenuStrip = this.cmsPeopleMenu;
            this.dgvDrivers.Location = new System.Drawing.Point(35, 415);
            this.dgvDrivers.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDrivers.Name = "dgvDrivers";
            this.dgvDrivers.ReadOnly = true;
            this.dgvDrivers.RowHeadersWidth = 72;
            this.dgvDrivers.RowTemplate.Height = 32;
            this.dgvDrivers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDrivers.Size = new System.Drawing.Size(1371, 266);
            this.dgvDrivers.StandardTab = true;
            this.dgvDrivers.TabIndex = 44;
            this.dgvDrivers.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvDrivers_Scroll);
            this.dgvDrivers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDrivers_KeyDown);
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.lblFormTitle.Location = new System.Drawing.Point(7, 7);
            this.lblFormTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(132, 29);
            this.lblFormTitle.TabIndex = 49;
            this.lblFormTitle.Text = "Drivers List";
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
            this.btnExit.Location = new System.Drawing.Point(1393, 8);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 48;
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
            this.lblFormBigTitle.Location = new System.Drawing.Point(539, 229);
            this.lblFormBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFormBigTitle.Name = "lblFormBigTitle";
            this.lblFormBigTitle.Size = new System.Drawing.Size(363, 52);
            this.lblFormBigTitle.TabIndex = 50;
            this.lblFormBigTitle.Text = "Manage Drivers";
            // 
            // frmDriversManagements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1445, 774);
            this.ControlBox = false;
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.lblRecordsTitle);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitleFilterBy);
            this.Controls.Add(this.pbDrivers);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvDrivers);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblFormBigTitle);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Name = "frmDriversManagements";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDriversManagements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDrivers)).EndInit();
            this.cmsPeopleMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrivers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.Label lblRecordsTitle;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitleFilterBy;
        private System.Windows.Forms.PictureBox pbDrivers;
        private System.Windows.Forms.ToolStripMenuItem tsmiPhoneCall;
        private System.Windows.Forms.ToolStripMenuItem tsmiSendEmail;
        private System.Windows.Forms.ToolStripSeparator tsSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNewUser;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowDetails;
        private System.Windows.Forms.ContextMenuStrip cmsPeopleMenu;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvDrivers;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblFormBigTitle;
    }
}