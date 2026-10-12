using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmEditApplicationType : Form
    {
        public event Action<object[], byte> AfterUpdatingInfo;
 
        private clsApplicationType _ApplicationType;
        private byte _AppTypesDGVRowIndex;

        public frmEditApplicationType(byte ApplicationTypeID, string ApplicationTitle, string ApplicationFees, byte AppTypesDGVRowIndex)
        {
            InitializeComponent();
            _AppTypesDGVRowIndex = AppTypesDGVRowIndex;

            lblID.Text = ApplicationTypeID.ToString();
            txtTitle.Text = ApplicationTitle;
            txtFees.Text = ApplicationFees;

            _ApplicationType = new clsApplicationType(ApplicationTypeID, ApplicationTitle, Convert.ToDecimal(ApplicationFees));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationTypeFees = Convert.ToDecimal(txtFees.Text.Trim());

            if (_ApplicationType.AreAllFieldsOldValuesNotChanged())
                MessageBox.Show("There are no changes on the application type info", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                if (_ApplicationType.Save())
                {
                    object[] NewValues = new object[] { _ApplicationType.ApplicationTypeID, _ApplicationType.ApplicationTypeTitle
                            , clsGeneralUtility.GetCustomNumberFormat(_ApplicationType.ApplicationTypeFees, clsGeneralUtility.enCustomNumberFormat.With4ZerosAfterFraction) };

                    AfterUpdatingInfo?.Invoke(NewValues, _AppTypesDGVRowIndex);

                    MessageBox.Show("Application Type Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Failed to update application type info!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtTitle, "Title cannot be empty!", e);
            }

            else
                clsGeneralUtility.DisableErrorProvider(ertxtBox, txtTitle);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtFees, "Application Fees cannot be empty!", e);

            else if (!clsGeneralUtility.IsNumber(txtFees.Text.Trim()))
            {
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtFees, "You can enter only numbers!", e);
            }

            else
                clsGeneralUtility.DisableErrorProvider(ertxtBox, txtFees);
        }
    }
}
