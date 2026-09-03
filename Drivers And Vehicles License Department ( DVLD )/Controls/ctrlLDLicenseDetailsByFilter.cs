using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class ctrlLDLicenseDetailsByFilter : UserControl
    {
        public delegate void SelectedLocalLicense(int LocalLicenseID,int DriverID);
        public event SelectedLocalLicense OnSelectedLocalLicense;
        public ctrlLDLicenseDetailsByFilter()
        {
            InitializeComponent();
        }

        private bool _IsOrdinaryLocalDrivingLicense(int LicenseClassID)
        {
            if (clsLicenseClass.GetLicenseClassEnumMember(LicenseClassID) == clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass)
                return true;
            else
                return false;
        }

        private bool _CanDriverApply(clsLocalLicense LocalLicense)
        {
            if (clsInternationalLicense.HasDriverActiveInternationalLicense(LocalLicense.LicenseID, out int InternationalLicenseID)) 
            {
                MessageBox.Show($"Person already has an active international license with ID : {InternationalLicenseID}"
             , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else if (!_IsOrdinaryLocalDrivingLicense(LocalLicense.LicenseClassID))
            {
                MessageBox.Show("The entered ID is for a local license but it is not from the ordinary driving license class!\nYou can issue international license only if you have local license from the ordinary driving license class."
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else if (LocalLicense.IsExpired())
            {
                MessageBox.Show("The entered ID is for a local license but it has been expired!, re-new it first then re-apply for international license."
                , "Expired Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else if (!LocalLicense.IsActive)
            {
                if (clsDetainedLicenses.IsDetainedLicense(LocalLicense.LicenseID))
                {
                    MessageBox.Show("The entered ID is for a local license but it is not active because it has been detained,\nrelease it from detain first then re-apply for international license."
                        , "Detained Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (clsDriver.HasActiveLicenseFromClass(LocalLicense.DriverID, clsLicenseClass.GetLicenseClassID(clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass), out int ActiveLicenseID))
                {
                    MessageBox.Show($"The entered ID is for a local license that is no longer active,\nThe driver has already active local licenseID from ordinary driving class which has been issued as {clsLocalLicense.GetLicenseNotes(ActiveLicenseID)} and its ID is : {ActiveLicenseID}"
                        , "This License Is No Longer Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }

            return true;
        }
        private void btnSearchForLicense_Click(object sender, EventArgs e)
        {
            if (txtLicenseID.Text != "")
            {
                clsLocalLicense LocalLicense = clsLocalLicense.Find(Convert.ToInt32(txtLicenseID.Text));

                if (LocalLicense != null)
                {
                    if (_CanDriverApply(LocalLicense))
                    {
                        uctrlLDLDetails.LoadDriverLicenseInfo(LocalLicense.LicenseID);
                        OnSelectedLocalLicense?.Invoke(LocalLicense.LicenseID, LocalLicense.DriverID);
                    }
                }
                else
                    MessageBox.Show("There is no local license for the entered ID!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("No entered ID!", "Enter License ID First", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
