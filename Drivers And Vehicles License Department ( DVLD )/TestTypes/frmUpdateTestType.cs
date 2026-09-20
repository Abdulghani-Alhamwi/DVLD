using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmUpdateTestType : Form
    {
        internal event Action<object[], byte> AfterUpdatingInfo;

        byte _TestsTypesDGVRowIndex;

        private clsTestType _TestType;

        public frmUpdateTestType(byte TestTypeID, string TestTypeTitle, string TestTypeDescription,decimal TestTypeFees,byte TestsTypesDGVRowIndex)
        {
            InitializeComponent();

            lblID.Text = TestTypeID.ToString();
            txtTitle.Text = TestTypeTitle;
            txtDescription.Text = TestTypeDescription;
            txtFees.Text = TestTypeFees.ToString();

            _TestType = new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription,TestTypeFees);
            _TestsTypesDGVRowIndex = TestsTypesDGVRowIndex;
        }

        private void txtFees_KeyDowm(object sender, KeyEventArgs e)
        {
            frmUpdateApplicationType.ValidateFeesTextBox_KeyDown(ertxtBox, txtFees, e);
        }

        private bool _ValidateData()
        {
            bool IsValidData = false;

            if (txtTitle.Text == "" || string.IsNullOrWhiteSpace(txtTitle.Text))
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtTitle, "Title cannot be empty!", null);

            else if (txtDescription.Text == "" || string.IsNullOrWhiteSpace(txtDescription.Text))
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtTitle, "Description cannot be empty!", null);

            else if (txtFees.Text == "")
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtTitle, "Fees cannot be empty!", null);

            else
            {
                ertxtBox.Dispose();
                IsValidData = true;
            }

            return IsValidData;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_ValidateData())
            {
                _TestType.TestTypeTitle = txtTitle.Text;
                _TestType.TestTypeDescription = txtDescription.Text;
                _TestType.TestTypeFees = Convert.ToDecimal(txtFees.Text);

                if (_TestType.AreAllFieldsOldValuesNotChanged())
                    MessageBox.Show("There are no changes on the test type info", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else 
                {
                    if (_TestType.Save())
                    {
                        object[] NewValues = new object[] { _TestType.TestTypeID, txtTitle.Text, txtDescription.Text, clsGeneralUtility.GetCustomNumberFormat(Convert.ToSingle(txtFees.Text), clsGeneralUtility.enCustomNumberFormat.With4ZerosAfterFraction) };
                        AfterUpdatingInfo?.Invoke(NewValues, _TestsTypesDGVRowIndex);

                        MessageBox.Show("Test Type Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Failed to update test type info!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

    }
}
