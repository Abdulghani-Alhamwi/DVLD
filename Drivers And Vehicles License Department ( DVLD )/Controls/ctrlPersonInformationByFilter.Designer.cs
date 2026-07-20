namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class PersonInformationByFilter
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
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.lblTitleFindBy = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnFindUser = new System.Windows.Forms.Button();
            this.uctrlPersonDetails = new Driver_And_Vehicle_Licenses_Department___DVLD__.ctrlPersonDetails();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFindBy
            // 
            this.txtFindBy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtFindBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFindBy.Font = new System.Drawing.Font("Tahoma", 17F);
            this.txtFindBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.txtFindBy.Location = new System.Drawing.Point(394, 47);
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(373, 35);
            this.txtFindBy.TabIndex = 0;
            this.txtFindBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFindBy_KeyDown);
            // 
            // cbFindBy
            // 
            this.cbFindBy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.cbFindBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFindBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFindBy.Font = new System.Drawing.Font("Tahoma", 17F);
            this.cbFindBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.cbFindBy.FormattingEnabled = true;
            this.cbFindBy.Location = new System.Drawing.Point(148, 46);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(226, 36);
            this.cbFindBy.TabIndex = 35;
            this.cbFindBy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFindBy_DrawItem);
            this.cbFindBy.DropDown += new System.EventHandler(this.cbFindBy_DropDown);
            this.cbFindBy.SelectedIndexChanged += new System.EventHandler(this.cbFindBy_SelectedIndexChanged);
            this.cbFindBy.DropDownClosed += new System.EventHandler(this.cbFindBy_DropDownClosed);
            // 
            // lblTitleFindBy
            // 
            this.lblTitleFindBy.AutoSize = true;
            this.lblTitleFindBy.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold);
            this.lblTitleFindBy.Location = new System.Drawing.Point(23, 50);
            this.lblTitleFindBy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitleFindBy.Name = "lblTitleFindBy";
            this.lblTitleFindBy.Size = new System.Drawing.Size(112, 28);
            this.lblTitleFindBy.TabIndex = 37;
            this.lblTitleFindBy.Text = "Find By :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddNewPerson);
            this.groupBox1.Controls.Add(this.btnFindUser);
            this.groupBox1.Controls.Add(this.txtFindBy);
            this.groupBox1.Controls.Add(this.cbFindBy);
            this.groupBox1.Controls.Add(this.lblTitleFindBy);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 18F);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1073, 108);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewPerson.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnAddNewPerson.FlatAppearance.BorderSize = 2;
            this.btnAddNewPerson.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddNewPerson.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddNewPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewPerson.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.AddPerson_32;
            this.btnAddNewPerson.Location = new System.Drawing.Point(892, 39);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(74, 50);
            this.btnAddNewPerson.TabIndex = 42;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnFindUser
            // 
            this.btnFindUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFindUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.btnFindUser.FlatAppearance.BorderSize = 2;
            this.btnFindUser.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFindUser.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnFindUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindUser.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.SearchPerson;
            this.btnFindUser.Location = new System.Drawing.Point(794, 39);
            this.btnFindUser.Name = "btnFindUser";
            this.btnFindUser.Size = new System.Drawing.Size(74, 50);
            this.btnFindUser.TabIndex = 41;
            this.btnFindUser.UseVisualStyleBackColor = true;
            this.btnFindUser.Click += new System.EventHandler(this.btnFindUser_Click);
            // 
            // uctrlPersonDetails
            // 
            this.uctrlPersonDetails.AllowDrop = true;
            this.uctrlPersonDetails.AutoScroll = true;
            this.uctrlPersonDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.uctrlPersonDetails.Font = new System.Drawing.Font("Tahoma", 16F);
            this.uctrlPersonDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.uctrlPersonDetails.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.uctrlPersonDetails.Location = new System.Drawing.Point(-6, 122);
            this.uctrlPersonDetails.Margin = new System.Windows.Forms.Padding(0);
            this.uctrlPersonDetails.Name = "uctrlPersonDetails";
            this.uctrlPersonDetails.Size = new System.Drawing.Size(1084, 364);
            this.uctrlPersonDetails.TabIndex = 0;
            // 
            // PersonInformationByFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.uctrlPersonDetails);
            this.Name = "PersonInformationByFilter";
            this.Size = new System.Drawing.Size(1080, 487);
            this.Load += new System.EventHandler(this.PersonInformationByFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonDetails uctrlPersonDetails;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Label lblTitleFindBy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.Button btnFindUser;
    }
}
