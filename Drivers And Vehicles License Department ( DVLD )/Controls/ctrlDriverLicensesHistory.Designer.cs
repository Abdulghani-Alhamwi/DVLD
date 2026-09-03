namespace DVLDPresentationLayer
{
    partial class ctrlDriverLicensesHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcLicensesHistory = new System.Windows.Forms.TabControl();
            this.tpLocalLicenses = new System.Windows.Forms.TabPage();
            this.lblLocalLicensesNum = new System.Windows.Forms.Label();
            this.lblLocalRecordsNumTitle = new System.Windows.Forms.Label();
            this.lblLLHistoryTitle = new System.Windows.Forms.Label();
            this.dgvLocalLicenses = new System.Windows.Forms.DataGridView();
            this.tpInternationalLicenses = new System.Windows.Forms.TabPage();
            this.lblInternationalLicensesNum = new System.Windows.Forms.Label();
            this.lblIntRecordsNumTitle = new System.Windows.Forms.Label();
            this.lblIntLicensesHistoryTitle = new System.Windows.Forms.Label();
            this.dgvInternationalLicenses = new System.Windows.Forms.DataGridView();
            this.gbDriverLicenses = new System.Windows.Forms.GroupBox();
            this.tcLicensesHistory.SuspendLayout();
            this.tpLocalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).BeginInit();
            this.tpInternationalLicenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenses)).BeginInit();
            this.gbDriverLicenses.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcLicensesHistory
            // 
            this.tcLicensesHistory.Controls.Add(this.tpLocalLicenses);
            this.tcLicensesHistory.Controls.Add(this.tpInternationalLicenses);
            this.tcLicensesHistory.Location = new System.Drawing.Point(18, 41);
            this.tcLicensesHistory.Name = "tcLicensesHistory";
            this.tcLicensesHistory.SelectedIndex = 0;
            this.tcLicensesHistory.Size = new System.Drawing.Size(1404, 352);
            this.tcLicensesHistory.TabIndex = 0;
            // 
            // tpLocalLicenses
            // 
            this.tpLocalLicenses.Controls.Add(this.lblLocalLicensesNum);
            this.tpLocalLicenses.Controls.Add(this.lblLocalRecordsNumTitle);
            this.tpLocalLicenses.Controls.Add(this.lblLLHistoryTitle);
            this.tpLocalLicenses.Controls.Add(this.dgvLocalLicenses);
            this.tpLocalLicenses.Location = new System.Drawing.Point(4, 38);
            this.tpLocalLicenses.Name = "tpLocalLicenses";
            this.tpLocalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocalLicenses.Size = new System.Drawing.Size(1396, 310);
            this.tpLocalLicenses.TabIndex = 0;
            this.tpLocalLicenses.Text = "Local";
            this.tpLocalLicenses.UseVisualStyleBackColor = true;
            // 
            // lblLocalLicensesNum
            // 
            this.lblLocalLicensesNum.AutoSize = true;
            this.lblLocalLicensesNum.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblLocalLicensesNum.Location = new System.Drawing.Point(188, 267);
            this.lblLocalLicensesNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocalLicensesNum.Name = "lblLocalLicensesNum";
            this.lblLocalLicensesNum.Size = new System.Drawing.Size(0, 29);
            this.lblLocalLicensesNum.TabIndex = 61;
            this.lblLocalLicensesNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLocalRecordsNumTitle
            // 
            this.lblLocalRecordsNumTitle.AutoSize = true;
            this.lblLocalRecordsNumTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblLocalRecordsNumTitle.Location = new System.Drawing.Point(18, 267);
            this.lblLocalRecordsNumTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLocalRecordsNumTitle.Name = "lblLocalRecordsNumTitle";
            this.lblLocalRecordsNumTitle.Size = new System.Drawing.Size(152, 29);
            this.lblLocalRecordsNumTitle.TabIndex = 60;
            this.lblLocalRecordsNumTitle.Text = "# Records :";
            // 
            // lblLLHistoryTitle
            // 
            this.lblLLHistoryTitle.AutoSize = true;
            this.lblLLHistoryTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblLLHistoryTitle.Location = new System.Drawing.Point(29, 16);
            this.lblLLHistoryTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLLHistoryTitle.Name = "lblLLHistoryTitle";
            this.lblLLHistoryTitle.Size = new System.Drawing.Size(293, 29);
            this.lblLLHistoryTitle.TabIndex = 59;
            this.lblLLHistoryTitle.Text = "Local Licenses History :";
            // 
            // dgvLocalLicenses
            // 
            this.dgvLocalLicenses.AllowUserToAddRows = false;
            this.dgvLocalLicenses.AllowUserToDeleteRows = false;
            this.dgvLocalLicenses.AllowUserToOrderColumns = true;
            this.dgvLocalLicenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLocalLicenses.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 18F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocalLicenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLocalLicenses.ColumnHeadersHeight = 40;
            this.dgvLocalLicenses.Location = new System.Drawing.Point(24, 54);
            this.dgvLocalLicenses.Margin = new System.Windows.Forms.Padding(2);
            this.dgvLocalLicenses.Name = "dgvLocalLicenses";
            this.dgvLocalLicenses.ReadOnly = true;
            this.dgvLocalLicenses.RowHeadersWidth = 72;
            this.dgvLocalLicenses.RowTemplate.Height = 32;
            this.dgvLocalLicenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalLicenses.Size = new System.Drawing.Size(1355, 205);
            this.dgvLocalLicenses.StandardTab = true;
            this.dgvLocalLicenses.TabIndex = 58;
            this.dgvLocalLicenses.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvLocalLicenses_Scroll);
            this.dgvLocalLicenses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvLocalLicenses_KeyDown);
            // 
            // tpInternationalLicenses
            // 
            this.tpInternationalLicenses.Controls.Add(this.lblInternationalLicensesNum);
            this.tpInternationalLicenses.Controls.Add(this.lblIntRecordsNumTitle);
            this.tpInternationalLicenses.Controls.Add(this.lblIntLicensesHistoryTitle);
            this.tpInternationalLicenses.Controls.Add(this.dgvInternationalLicenses);
            this.tpInternationalLicenses.Location = new System.Drawing.Point(4, 38);
            this.tpInternationalLicenses.Name = "tpInternationalLicenses";
            this.tpInternationalLicenses.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternationalLicenses.Size = new System.Drawing.Size(1396, 310);
            this.tpInternationalLicenses.TabIndex = 1;
            this.tpInternationalLicenses.Text = "International";
            this.tpInternationalLicenses.UseVisualStyleBackColor = true;
            // 
            // lblInternationalLicensesNum
            // 
            this.lblInternationalLicensesNum.AutoSize = true;
            this.lblInternationalLicensesNum.Font = new System.Drawing.Font("Tahoma", 18F);
            this.lblInternationalLicensesNum.Location = new System.Drawing.Point(188, 266);
            this.lblInternationalLicensesNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInternationalLicensesNum.Name = "lblInternationalLicensesNum";
            this.lblInternationalLicensesNum.Size = new System.Drawing.Size(0, 29);
            this.lblInternationalLicensesNum.TabIndex = 65;
            this.lblInternationalLicensesNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIntRecordsNumTitle
            // 
            this.lblIntRecordsNumTitle.AutoSize = true;
            this.lblIntRecordsNumTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblIntRecordsNumTitle.Location = new System.Drawing.Point(18, 266);
            this.lblIntRecordsNumTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIntRecordsNumTitle.Name = "lblIntRecordsNumTitle";
            this.lblIntRecordsNumTitle.Size = new System.Drawing.Size(152, 29);
            this.lblIntRecordsNumTitle.TabIndex = 64;
            this.lblIntRecordsNumTitle.Text = "# Records :";
            // 
            // lblIntLicensesHistoryTitle
            // 
            this.lblIntLicensesHistoryTitle.AutoSize = true;
            this.lblIntLicensesHistoryTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblIntLicensesHistoryTitle.Location = new System.Drawing.Point(29, 15);
            this.lblIntLicensesHistoryTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIntLicensesHistoryTitle.Name = "lblIntLicensesHistoryTitle";
            this.lblIntLicensesHistoryTitle.Size = new System.Drawing.Size(388, 29);
            this.lblIntLicensesHistoryTitle.TabIndex = 63;
            this.lblIntLicensesHistoryTitle.Text = "International Licenses History :";
            // 
            // dgvInternationalLicenses
            // 
            this.dgvInternationalLicenses.AllowUserToAddRows = false;
            this.dgvInternationalLicenses.AllowUserToDeleteRows = false;
            this.dgvInternationalLicenses.AllowUserToOrderColumns = true;
            this.dgvInternationalLicenses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvInternationalLicenses.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 18F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInternationalLicenses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInternationalLicenses.ColumnHeadersHeight = 40;
            this.dgvInternationalLicenses.Location = new System.Drawing.Point(24, 53);
            this.dgvInternationalLicenses.Margin = new System.Windows.Forms.Padding(2);
            this.dgvInternationalLicenses.Name = "dgvInternationalLicenses";
            this.dgvInternationalLicenses.ReadOnly = true;
            this.dgvInternationalLicenses.RowHeadersWidth = 72;
            this.dgvInternationalLicenses.RowTemplate.Height = 32;
            this.dgvInternationalLicenses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInternationalLicenses.Size = new System.Drawing.Size(1346, 205);
            this.dgvInternationalLicenses.StandardTab = true;
            this.dgvInternationalLicenses.TabIndex = 62;
            this.dgvInternationalLicenses.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvInternationalLicenses_Scroll);
            this.dgvInternationalLicenses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvInternationalLicenses_KeyDown);
            // 
            // gbDriverLicenses
            // 
            this.gbDriverLicenses.Controls.Add(this.tcLicensesHistory);
            this.gbDriverLicenses.Location = new System.Drawing.Point(7, -1);
            this.gbDriverLicenses.Name = "gbDriverLicenses";
            this.gbDriverLicenses.Size = new System.Drawing.Size(1439, 411);
            this.gbDriverLicenses.TabIndex = 1;
            this.gbDriverLicenses.TabStop = false;
            this.gbDriverLicenses.Text = "Driver Licenses";
            // 
            // ctrlDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.gbDriverLicenses);
            this.Font = new System.Drawing.Font("Tahoma", 18F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.Name = "ctrlDriverLicensesHistory";
            this.Size = new System.Drawing.Size(1458, 413);
            this.tcLicensesHistory.ResumeLayout(false);
            this.tpLocalLicenses.ResumeLayout(false);
            this.tpLocalLicenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenses)).EndInit();
            this.tpInternationalLicenses.ResumeLayout(false);
            this.tpInternationalLicenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenses)).EndInit();
            this.gbDriverLicenses.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcLicensesHistory;
        private System.Windows.Forms.TabPage tpLocalLicenses;
        private System.Windows.Forms.Label lblLocalLicensesNum;
        private System.Windows.Forms.Label lblLocalRecordsNumTitle;
        private System.Windows.Forms.Label lblLLHistoryTitle;
        private System.Windows.Forms.DataGridView dgvLocalLicenses;
        private System.Windows.Forms.TabPage tpInternationalLicenses;
        private System.Windows.Forms.GroupBox gbDriverLicenses;
        private System.Windows.Forms.Label lblInternationalLicensesNum;
        private System.Windows.Forms.Label lblIntRecordsNumTitle;
        private System.Windows.Forms.Label lblIntLicensesHistoryTitle;
        private System.Windows.Forms.DataGridView dgvInternationalLicenses;
    }
}
