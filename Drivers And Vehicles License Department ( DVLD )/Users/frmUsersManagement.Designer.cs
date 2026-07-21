namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class frmUsersManagement
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
            this.btnExit = new System.Windows.Forms.Button();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblTitleFilterBy = new System.Windows.Forms.Label();
            this.lblManageUsersBigTitle = new System.Windows.Forms.Label();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsUsersMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddNewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.lblManageUsersTitle = new System.Windows.Forms.Label();
            this.pbUser = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbFilterOnIsActive = new System.Windows.Forms.ComboBox();
            this.btnAddNewUser = new System.Windows.Forms.Button();
            this.cmsUsersMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).BeginInit();
            this.SuspendLayout();
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
            this.btnExit.Location = new System.Drawing.Point(1045, 12);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 31;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Tahoma", 19F);
            this.lblRecordsNumber.Location = new System.Drawing.Point(218, 706);
            this.lblRecordsNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(0, 31);
            this.lblRecordsNumber.TabIndex = 38;
            this.lblRecordsNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFilter
            // 
            this.txtFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilter.Font = new System.Drawing.Font("Tahoma", 21F);
            this.txtFilter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtFilter.Location = new System.Drawing.Point(460, 344);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(262, 41);
            this.txtFilter.TabIndex = 30;
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
            this.cbFilterBy.Location = new System.Drawing.Point(211, 343);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(226, 42);
            this.cbFilterBy.TabIndex = 29;
            this.cbFilterBy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFilterBy_DrawItem);
            this.cbFilterBy.DropDown += new System.EventHandler(this.cbFilterBy_DropDown);
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            this.cbFilterBy.DropDownClosed += new System.EventHandler(this.cbFilterBy_DropDownClosed);
            // 
            // lblTitleFilterBy
            // 
            this.lblTitleFilterBy.AutoSize = true;
            this.lblTitleFilterBy.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitleFilterBy.Location = new System.Drawing.Point(48, 346);
            this.lblTitleFilterBy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitleFilterBy.Name = "lblTitleFilterBy";
            this.lblTitleFilterBy.Size = new System.Drawing.Size(160, 36);
            this.lblTitleFilterBy.TabIndex = 34;
            this.lblTitleFilterBy.Text = "Filter By :";
            // 
            // lblManageUsersBigTitle
            // 
            this.lblManageUsersBigTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblManageUsersBigTitle.AutoSize = true;
            this.lblManageUsersBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblManageUsersBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblManageUsersBigTitle.Location = new System.Drawing.Point(395, 216);
            this.lblManageUsersBigTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManageUsersBigTitle.Name = "lblManageUsersBigTitle";
            this.lblManageUsersBigTitle.Size = new System.Drawing.Size(327, 52);
            this.lblManageUsersBigTitle.TabIndex = 32;
            this.lblManageUsersBigTitle.Text = "Manage Users";
            // 
            // lblRecordsTitle
            // 
            this.lblRecordsTitle.AutoSize = true;
            this.lblRecordsTitle.Font = new System.Drawing.Font("Tahoma", 19F, System.Drawing.FontStyle.Bold);
            this.lblRecordsTitle.Location = new System.Drawing.Point(48, 706);
            this.lblRecordsTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsTitle.Name = "lblRecordsTitle";
            this.lblRecordsTitle.Size = new System.Drawing.Size(164, 31);
            this.lblRecordsTitle.TabIndex = 37;
            this.lblRecordsTitle.Text = "# Records :";
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // cmsUsersMenu
            // 
            this.cmsUsersMenu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmsUsersMenu.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.cmsUsersMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsUsersMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowDetails,
            this.tsSeparator1,
            this.tsmiAddNewUser,
            this.tsmiEdit,
            this.tsmiDelete,
            this.tsSeparator2,
            this.tsmiSendEmail,
            this.tsmiPhoneCall});
            this.cmsUsersMenu.Name = "cmsPeopleMenu";
            this.cmsUsersMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.cmsUsersMenu.Size = new System.Drawing.Size(223, 266);
            // 
            // tsmiShowDetails
            // 
            this.tsmiShowDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiShowDetails.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.PersonDetails_32;
            this.tsmiShowDetails.Name = "tsmiShowDetails";
            this.tsmiShowDetails.Size = new System.Drawing.Size(222, 38);
            this.tsmiShowDetails.Text = "Show Details";
            // 
            // tsmiAddNewUser
            // 
            this.tsmiAddNewUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiAddNewUser.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Add_Person_40;
            this.tsmiAddNewUser.Name = "tsmiAddNewUser";
            this.tsmiAddNewUser.Size = new System.Drawing.Size(222, 38);
            this.tsmiAddNewUser.Text = "Add New User";
            this.tsmiAddNewUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiAddNewUser.Click += new System.EventHandler(this.tsmiAddNewUser_Click);
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiEdit.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.edit_32;
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(222, 38);
            this.tsmiEdit.Text = "Edit";
            this.tsmiEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiEdit.Click += new System.EventHandler(this.tsmiEdit_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiDelete.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Delete_32;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(222, 38);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiSendEmail
            // 
            this.tsmiSendEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiSendEmail.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.send_email_32;
            this.tsmiSendEmail.Name = "tsmiSendEmail";
            this.tsmiSendEmail.Size = new System.Drawing.Size(222, 38);
            this.tsmiSendEmail.Text = "Send Email";
            // 
            // tsmiPhoneCall
            // 
            this.tsmiPhoneCall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.tsmiPhoneCall.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.call_32;
            this.tsmiPhoneCall.Name = "tsmiPhoneCall";
            this.tsmiPhoneCall.Size = new System.Drawing.Size(222, 38);
            this.tsmiPhoneCall.Text = "Phone Call";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToOrderColumns = true;
            this.dgvUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 16F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.ContextMenuStrip = this.cmsUsersMenu;
            this.dgvUsers.Location = new System.Drawing.Point(54, 410);
            this.dgvUsers.Margin = new System.Windows.Forms.Padding(2);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 72;
            this.dgvUsers.RowTemplate.Height = 32;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.Size = new System.Drawing.Size(1008, 266);
            this.dgvUsers.StandardTab = true;
            this.dgvUsers.TabIndex = 27;
            // 
            // lblManageUsersTitle
            // 
            this.lblManageUsersTitle.AutoSize = true;
            this.lblManageUsersTitle.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblManageUsersTitle.Location = new System.Drawing.Point(21, 12);
            this.lblManageUsersTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManageUsersTitle.Name = "lblManageUsersTitle";
            this.lblManageUsersTitle.Size = new System.Drawing.Size(165, 29);
            this.lblManageUsersTitle.TabIndex = 35;
            this.lblManageUsersTitle.Text = "Manage Users";
            // 
            // pbUser
            // 
            this.pbUser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbUser.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Users_2_400;
            this.pbUser.Location = new System.Drawing.Point(426, 56);
            this.pbUser.Margin = new System.Windows.Forms.Padding(2);
            this.pbUser.Name = "pbUser";
            this.pbUser.Size = new System.Drawing.Size(265, 137);
            this.pbUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUser.TabIndex = 33;
            this.pbUser.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 18F);
            this.btnClose.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(896, 706);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 36;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbFilterOnIsActive
            // 
            this.cbFilterOnIsActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.cbFilterOnIsActive.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterOnIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterOnIsActive.Font = new System.Drawing.Font("Tahoma", 21F);
            this.cbFilterOnIsActive.FormattingEnabled = true;
            this.cbFilterOnIsActive.Location = new System.Drawing.Point(460, 343);
            this.cbFilterOnIsActive.Name = "cbFilterOnIsActive";
            this.cbFilterOnIsActive.Size = new System.Drawing.Size(182, 42);
            this.cbFilterOnIsActive.TabIndex = 39;
            this.cbFilterOnIsActive.Visible = false;
            this.cbFilterOnIsActive.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFilterOnIsActive_DrawItem);
            this.cbFilterOnIsActive.DropDown += new System.EventHandler(this.cbFilterOnIsActive_DropDown);
            this.cbFilterOnIsActive.SelectedIndexChanged += new System.EventHandler(this.cbFilterOnIsActive_SelectedIndexChanged);
            this.cbFilterOnIsActive.DropDownClosed += new System.EventHandler(this.cbFilterOnIsActive_DropDownClosed);
            // 
            // btnAddNewUser
            // 
            this.btnAddNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnAddNewUser.FlatAppearance.BorderSize = 2;
            this.btnAddNewUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddNewUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddNewUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewUser.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Add_New_User_72;
            this.btnAddNewUser.Location = new System.Drawing.Point(951, 315);
            this.btnAddNewUser.Name = "btnAddNewUser";
            this.btnAddNewUser.Size = new System.Drawing.Size(111, 70);
            this.btnAddNewUser.TabIndex = 40;
            this.btnAddNewUser.UseVisualStyleBackColor = true;
            this.btnAddNewUser.Click += new System.EventHandler(this.btnAddNewUser_Click);
            // 
            // frmUsersManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1115, 780);
            this.ControlBox = false;
            this.Controls.Add(this.btnAddNewUser);
            this.Controls.Add(this.cbFilterOnIsActive);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblTitleFilterBy);
            this.Controls.Add(this.pbUser);
            this.Controls.Add(this.lblManageUsersBigTitle);
            this.Controls.Add(this.lblRecordsTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.lblManageUsersTitle);
            this.Font = new System.Drawing.Font("Tahoma", 16F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmUsersManagement";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmUsersManagement_Load);
            this.cmsUsersMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label lblTitleFilterBy;
        private System.Windows.Forms.PictureBox pbUser;
        private System.Windows.Forms.Label lblManageUsersBigTitle;
        private System.Windows.Forms.Label lblRecordsTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiPhoneCall;
        private System.Windows.Forms.ToolStripSeparator tsSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNewUser;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowDetails;
        private System.Windows.Forms.ContextMenuStrip cmsUsersMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSendEmail;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Label lblManageUsersTitle;
        private System.Windows.Forms.ComboBox cbFilterOnIsActive;
        private System.Windows.Forms.Button btnAddNewUser;
    }
}