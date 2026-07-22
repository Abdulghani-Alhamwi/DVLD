namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    partial class frmPersonDetails
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblPersonDetailsBigTitle = new System.Windows.Forms.Label();
            this.lblPersonDetailsTitle = new System.Windows.Forms.Label();
            this.UctrlPersonDetails = new Driver_And_Vehicle_Licenses_Department___DVLD__.ctrlPersonDetails();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 2;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::Driver_And_Vehicle_Licenses_Department___DVLD__.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(962, 581);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(166, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.btnExit.Location = new System.Drawing.Point(1089, 19);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(39, 36);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblPersonDetailsBigTitle
            // 
            this.lblPersonDetailsBigTitle.AutoSize = true;
            this.lblPersonDetailsBigTitle.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblPersonDetailsBigTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.lblPersonDetailsBigTitle.Location = new System.Drawing.Point(416, 75);
            this.lblPersonDetailsBigTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblPersonDetailsBigTitle.Name = "lblPersonDetailsBigTitle";
            this.lblPersonDetailsBigTitle.Size = new System.Drawing.Size(336, 52);
            this.lblPersonDetailsBigTitle.TabIndex = 87;
            this.lblPersonDetailsBigTitle.Text = "Person Details";
            // 
            // lblPersonDetailsTitle
            // 
            this.lblPersonDetailsTitle.AutoSize = true;
            this.lblPersonDetailsTitle.Font = new System.Drawing.Font("Tahoma", 22F);
            this.lblPersonDetailsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.lblPersonDetailsTitle.Location = new System.Drawing.Point(26, 19);
            this.lblPersonDetailsTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblPersonDetailsTitle.Name = "lblPersonDetailsTitle";
            this.lblPersonDetailsTitle.Size = new System.Drawing.Size(203, 36);
            this.lblPersonDetailsTitle.TabIndex = 86;
            this.lblPersonDetailsTitle.Text = "Person Details";
            // 
            // UctrlPersonDetails
            // 
            this.UctrlPersonDetails.AllowDrop = true;
            this.UctrlPersonDetails.AutoScroll = true;
            this.UctrlPersonDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UctrlPersonDetails.Font = new System.Drawing.Font("Tahoma", 16F);
            this.UctrlPersonDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.UctrlPersonDetails.Location = new System.Drawing.Point(41, 182);
            this.UctrlPersonDetails.Margin = new System.Windows.Forms.Padding(0);
            this.UctrlPersonDetails.Name = "UctrlPersonDetails";
            this.UctrlPersonDetails.Size = new System.Drawing.Size(1087, 367);
            this.UctrlPersonDetails.TabIndex = 0;
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1167, 657);
            this.ControlBox = false;
            this.Controls.Add(this.UctrlPersonDetails);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblPersonDetailsBigTitle);
            this.Controls.Add(this.lblPersonDetailsTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.Name = "frmPersonDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblPersonDetailsBigTitle;
        private System.Windows.Forms.Label lblPersonDetailsTitle;
        private ctrlPersonDetails UctrlPersonDetails;
    }
}