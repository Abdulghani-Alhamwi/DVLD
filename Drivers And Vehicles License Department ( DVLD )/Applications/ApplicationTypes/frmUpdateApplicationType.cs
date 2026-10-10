using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmUpdateApplicationType : Form
    {
        public event Action<object[], byte> AfterUpdatingInfo;
        private static CancelEventArgs _CancelArgs = new CancelEventArgs();

        private clsApplicationType _ApplicationType;
        private byte _AppTypesDGVRowIndex;

        public frmUpdateApplicationType(byte ApplicationTypeID, string ApplicationTitle, string ApplicationFees, byte AppTypesDGVRowIndex)
        {
            InitializeComponent();
            _AppTypesDGVRowIndex = AppTypesDGVRowIndex;

            lblID.Text = ApplicationTypeID.ToString();
            txtTitle.Text = ApplicationTitle;
            txtFees.Text = ApplicationFees;

            _ApplicationType = new clsApplicationType(ApplicationTypeID, ApplicationTitle, Convert.ToDecimal(ApplicationFees));
        }

        public static void ValidateFeesTextBox_KeyDown(ErrorProvider erControl, TextBox txtBox, KeyEventArgs e)
        {
            if (char.IsDigit((char)e.KeyData) || e.KeyData == Keys.OemPeriod || char.IsControl((char)e.KeyData))
            {
                txtBox.ReadOnly = false;
                erControl.Dispose();
            }
            else
            {
                txtBox.ReadOnly = true;
                clsGeneralUtility.EnableErrorProvider(erControl, txtBox, "You can enter only digits!", _CancelArgs);
            }
        }

        private bool _ValidateData()
        {
            if (txtTitle.Text == "" || String.IsNullOrWhiteSpace(txtTitle.Text))
            {
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtTitle, "Title cannot be empty!", null);
                return false;
            }

            else if (txtFees.Text == "" || String.IsNullOrWhiteSpace(txtFees.Text))
            {
                clsGeneralUtility.EnableErrorProvider(ertxtBox, txtFees, "Application Fees cannot be empty!", null);
                return false;
            }
            else
                ertxtBox.Dispose();

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_ValidateData())
            {
                _ApplicationType.ApplicationTypeTitle = txtTitle.Text;
                _ApplicationType.ApplicationTypeFees = Convert.ToDecimal(txtFees.Text);

                if (_ApplicationType.AreAllFieldsOldValuesNotChanged())
                    MessageBox.Show("There are no changes on the application type info", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                {
                    if (_ApplicationType.Save())
                    {
                        object[] NewValues = new object[] { _ApplicationType.ApplicationTypeID, txtTitle.Text, clsGeneralUtility.GetCustomNumberFormat(Convert.ToSingle(txtFees.Text), clsGeneralUtility.enCustomNumberFormat.With4ZerosAfterFraction) };
                        AfterUpdatingInfo?.Invoke(NewValues, _AppTypesDGVRowIndex);

                        MessageBox.Show("Application Type Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Failed to update application type info!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtFees_KeyDown(object sender, KeyEventArgs e)
        {
            ValidateFeesTextBox_KeyDown(ertxtBox, txtFees, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
