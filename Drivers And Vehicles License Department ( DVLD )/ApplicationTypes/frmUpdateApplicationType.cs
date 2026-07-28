using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmUpdateApplicationType : Form
    {
        int _ApplicationTypeID;
        string _ApplicationTitle;
        string _ApplicationFees;
        public frmUpdateApplicationType(int ApplicationTypeID,string ApplicationTitle,string ApplicationFees)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
            _ApplicationTitle = ApplicationTitle;
            _ApplicationFees = ApplicationFees;

            lblID.Text = ApplicationTypeID.ToString();
            txtTitle.Text = ApplicationTitle;
            txtFees.Text = ApplicationFees;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void ValidateFeesTextBox_KeyDown(ErrorProvider erControl, TextBox txtBox, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || char.IsDigit((char)e.KeyData))
                txtBox.ReadOnly = false;
            else
            {
                txtBox.ReadOnly = true;
                clsUtility.EnableErrorProvider(erControl, txtBox, "You can enter only digits!", null);
            }
        }
        private bool _ValidateData()
        {
            if (txtTitle.Text == "" || String.IsNullOrWhiteSpace(txtTitle.Text))
            {
                clsUtility.EnableErrorProvider(ertxtBox, txtTitle, "Title cannot be empty!", null);
                return false;
            }

            else if (txtFees.Text == "" || String.IsNullOrWhiteSpace(txtFees.Text))
            {
                clsUtility.EnableErrorProvider(ertxtBox, txtFees, "Application Fees cannot be empty!", null);
                return false;
            }
            else
                ertxtBox.Dispose();

            return true;
        }

        private bool _IsInfoUnchanged()
        {
            return (txtTitle.Text == _ApplicationTitle
                 && txtFees.Text == _ApplicationFees);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_IsInfoUnchanged())
                MessageBox.Show("There are no changes on the application type info", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else if (_ValidateData())
            {
                if (clsApplicationTypes.UpdateApplicationType(_ApplicationTypeID, txtTitle.Text, Convert.ToDouble(txtFees.Text)))
                    MessageBox.Show("Application Type Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to update application type info!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFees_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFeesTextBox_KeyDown(ertxtBox, txtFees, e);
        }
    }
}
