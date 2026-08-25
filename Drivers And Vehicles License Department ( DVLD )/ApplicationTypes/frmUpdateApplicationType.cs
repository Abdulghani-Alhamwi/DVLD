using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace DVLDPresentationLayer
{
    public partial class frmUpdateApplicationType : Form
    {
        internal event Action<object[],byte> AfterUpdatingInfo;

        byte _ApplicationTypeID;
        string _ApplicationTitle;
        string _ApplicationFees;
        byte _AppTypesDGVRowIndex;
        public frmUpdateApplicationType(byte ApplicationTypeID,string ApplicationTitle,string ApplicationFees,byte AppTypesDGVRowIndex)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
            _ApplicationTitle = ApplicationTitle;
            _ApplicationFees = ApplicationFees;
            _AppTypesDGVRowIndex = AppTypesDGVRowIndex;

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
                if (clsApplicationType.UpdateApplicationType(_ApplicationTypeID, txtTitle.Text, Convert.ToDecimal(txtFees.Text)))
                {
                    object[] NewValues = new object[] { _ApplicationTypeID, txtTitle.Text, Convert.ToDecimal(txtFees.Text) };
                    AfterUpdatingInfo?.Invoke(NewValues, _AppTypesDGVRowIndex);
                    MessageBox.Show("Application Type Info Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
