using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmUpdateTestType : Form
    {
        int _ApplicationTypeID;
        string _ApplicationTypeTitle, _ApplicationTypeDescription;
        double _ApplicationTypeFees;
        public frmUpdateTestType(int ApplicationTypeID, string ApplicationTypeTitle, string ApplicationTypeDescription,double ApplicationTypeFees)
        {
            InitializeComponent();

            lblID.Text = ApplicationTypeID.ToString();
            txtTitle.Text = ApplicationTypeTitle;
            txtDescription.Text = ApplicationTypeDescription;
            txtFees.Text = ApplicationTypeFees.ToString();

            _ApplicationTypeID = ApplicationTypeID;
            _ApplicationTypeTitle = ApplicationTypeTitle;
            _ApplicationTypeDescription = ApplicationTypeDescription;
            _ApplicationTypeFees = ApplicationTypeFees;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtFees_KeyDowm(object sender, KeyEventArgs e)
        {
            frmUpdateApplicationType.ValidateFeesTextBox_KeyDown(ertxtBox, txtFees, e);
        }

        private bool _ValidateData()
        {
            bool IsValidData = false;

            if (txtTitle.Text == "" || string.IsNullOrWhiteSpace(txtTitle.Text))
                clsUtility.EnableErrorProvider(ertxtBox, txtTitle, "Title cannot be empty!", null);

            else if (txtDescription.Text == "" || string.IsNullOrWhiteSpace(txtDescription.Text))
                clsUtility.EnableErrorProvider(ertxtBox, txtTitle, "Description cannot be empty!", null);

            else if (txtFees.Text == "")
                clsUtility.EnableErrorProvider(ertxtBox, txtTitle, "Fees cannot be empty!", null);

            else
            {
                ertxtBox.Dispose();
                IsValidData = true;
            }

            return IsValidData;

        }

        private bool _IsInfoUnchanged()
        {
            return (txtTitle.Text == _ApplicationTypeTitle
                  & txtDescription.Text == _ApplicationTypeDescription
                  & txtFees.Text == _ApplicationTypeFees.ToString());
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_IsInfoUnchanged())
                MessageBox.Show("There are no changes on the test type info", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else if(_ValidateData())
            {
                 if (clsTestTypes.UpdateTestType(_ApplicationTypeID,txtTitle.Text,txtDescription.Text,Convert.ToDecimal(txtFees.Text)))
                    MessageBox.Show("Test Type Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to update test type info!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
