using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmEditTestType : Form
    {
        public event Action<object[], byte> AfterUpdatingInfo;

        private byte _TestsTypesDGVRowIndex;
        private clsTestType _TestType;

        public frmEditTestType(clsTestType.enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription,decimal TestTypeFees,byte TestsTypesDGVRowIndex)
        {
            InitializeComponent();

            lblID.Text = ((byte)TestTypeID).ToString();
            txtTitle.Text = TestTypeTitle;
            txtDescription.Text = TestTypeDescription;
            txtFees.Text = TestTypeFees.ToString();

            _TestType = new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription,TestTypeFees);
            _TestsTypesDGVRowIndex = TestsTypesDGVRowIndex;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestTypeFees = Convert.ToDecimal(txtFees.Text.Trim());

            if (_TestType.AreAllFieldsOldValuesNotChanged())
                MessageBox.Show("There are no changes on the test type info", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                if (_TestType.Save())
                {
                    object[] NewValues = new object[] { _TestType.TestTypeID, _TestType.TestTypeTitle, _TestType.TestTypeDescription, clsGeneralUtility.GetCustomNumberFormat(_TestType.TestTypeFees, clsGeneralUtility.enCustomNumberFormat.With4ZerosAfterFraction) };
                    AfterUpdatingInfo?.Invoke(NewValues, _TestsTypesDGVRowIndex);

                    MessageBox.Show("Test Type Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Failed to update test type info!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtTitle, "Title cannot be empty!", e);

            else
                clsGeneralUtility.DisableErrorProvider(ertxtBox, txtTitle);
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtDescription, "Description cannot be empty!", e);

            else
                clsGeneralUtility.DisableErrorProvider(ertxtBox, txtDescription);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtFees, "Fees cannot be empty!", e);

            else if (!clsGeneralUtility.IsNumber(txtFees.Text.Trim()))
            {
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtFees, "Invalid Number!", e);
            }

            else
                clsGeneralUtility.DisableErrorProvider(ertxtBox, txtFees);
        }

    }
}
