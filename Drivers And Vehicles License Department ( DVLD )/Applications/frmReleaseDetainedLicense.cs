using System;
using System.Threading;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public event Action<int,DateTime,int> AfterReleasingSentLicense;

        public event Action<int,DateTime,int> AfterReleasingALicense;

        private clsLocalLicense _LocalLicenseInfo;
        private clsDetainedLicenses _DetainInfo;

        private int _DetainedLicensesDgvRowIndex;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            _InitilizeFormData();
            _DetainedLicensesDgvRowIndex = -1;
        }
        public frmReleaseDetainedLicense(int LocalLicenseID , int DetainedLicensesDgvRowIndex)
        {
            InitializeComponent();
            _InitilizeFormData(LocalLicenseID);
            _DetainedLicensesDgvRowIndex = DetainedLicensesDgvRowIndex;
        }

        private bool _AddLocalLicenseInfo(int LocalLicenseID)
        {
            uctrlLDLDetailsByFilter.txtLicenseID.Text = LocalLicenseID.ToString();
            lblLicenseID.Text = LocalLicenseID.ToString();
            clsLocalLicense LocalLicense = clsLocalLicense.Find(LocalLicenseID);

            if (LocalLicense != null)
            {
                _LocalLicenseInfo = LocalLicense;
                uctrlLDLDetailsByFilter.uctrlLDLDetails.LoadDriverLicenseInfo(LocalLicenseID);
                return true;
            }

            else
                return false;
        }

        private bool _AddDetainInfo(int LocalLicenseID)
        {
            _DetainInfo = clsDetainedLicenses.Find(LocalLicenseID);
            _ShowDetainInfo();

            return (_DetainInfo != null);
        }

        private void _InitilizeFormData(int LocalLicenseID = -1)
          {
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            
            if (LocalLicenseID != -1)
               {
                if (_AddLocalLicenseInfo(LocalLicenseID))
               {
                    lnlblShowLicenseHistory.Enabled = true;
                    
                    if (_AddDetainInfo(LocalLicenseID))
                   {
                        btnRelease.Enabled = true;
                   }
                    else
                   {
                       MessageBox.Show("Detain info is not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       btnRelease.Enabled = false;
                   }
               }
               else
               {
                   MessageBox.Show("Local license info is not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   btnRelease.Enabled = false;
               }
                
               uctrlLDLDetailsByFilter.gbFilter.Enabled = false; 
           }

        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(_LocalLicenseInfo.DriverID));
            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_LocalLicenseInfo.LicenseID);
            frm.ShowDialog();
        }

        private void _ShowDetainInfo()
        {
            lblDetainID.Text = _DetainInfo.DetainID.ToString();
            lblDetainedDate.Text = _DetainInfo.DetainDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblUserName.Text = clsUtility.DecryptUserName(clsUser.GetUserName(_DetainInfo.CreatedByUserID));
            lblApplicationFees.Text = clsUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReleaseDetainedLicense),clsUtility.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblFineFees.Text = clsUtility.GetCustomNumberFormat(_DetainInfo.FineFees, clsUtility.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblFineFees.Text)).ToString();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(clsLocalLicense LicenseInfo)
        {
            if (!clsDetainedLicenses.IsDetainedLicense(LicenseInfo.LicenseID))
            {
                MessageBox.Show("Selected license is not detained , choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                _DetainInfo = clsDetainedLicenses.Find(LicenseInfo.LicenseID);

                if (_DetainInfo != null)
                {
                    _ShowDetainInfo();
                    _LocalLicenseInfo = LicenseInfo;
                    btnRelease.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Detained license is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lnlblShowLicenseHistory.Enabled = true;
                }
            }
            lblLicenseID.Text = LicenseInfo.LicenseID.ToString();

            lnlblShowLicenseHistory.Enabled = true;
        }

        private bool _AddNewApplication(out int NewApplicationID)
        {
            clsApplication ReleaseDetainedLicense = new clsApplication
             (
                ApplicantPersonID: clsDriver.GetDriverPersonID(_LocalLicenseInfo.DriverID),
                ApplicationDate: DateTime.Now,
                ApplicationTypeID: clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.ReleaseDetainedLicense),
                ApplicationStatus: clsApplication.enApplicationStatus.New,
                LastStatusDate: DateTime.Now,
                PaidApplicationFees:clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReleaseDetainedLicense),
                CreatedByUserID:clsGlobalSettings.CurrentUserID
             );

            if (ReleaseDetainedLicense.Save())
            {
                NewApplicationID = ReleaseDetainedLicense.ApplicationID;
                return true;
            }
            else
            {
                NewApplicationID = -1;
                return false;
            }
        }

        private void _ReleaseDetainedLicense()
        {
               DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to release this detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.Yes) 
                {
                    if (_AddNewApplication(out int NewApplicationID))
                    {
                    if (_DetainInfo.ReleaseDetainedLicense(DateTime.Now, clsGlobalSettings.CurrentUserID, NewApplicationID))
                    {
                        if (clsApplication.ChangeApplicationStatus(_DetainInfo.ReleaseApplicationID, clsApplication.enApplicationStatus.Completed))
                        {
                            lblReleaseApplicationID.Text = _DetainInfo.ReleaseApplicationID.ToString();
                            MessageBox.Show($"Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            AfterReleasingSentLicense?.Invoke(_DetainInfo.ReleaseApplicationID, _DetainInfo.ReleaseDate, _DetainedLicensesDgvRowIndex);

                            if(AfterReleasingALicense != null)
                            {
                                Thread NewThread = new Thread(() => AfterReleasingALicense.Invoke(_DetainInfo.DetainID, _DetainInfo.ReleaseDate,_DetainInfo.ReleaseApplicationID));
                                NewThread.Start();
                            }

                            uctrlLDLDetailsByFilter.gbFilter.Enabled = false;
                            btnRelease.Enabled = false;
                            lnlblShowLicenseInfo.Enabled = true;
                        }
                        else
                            MessageBox.Show("License released successfully from detain but failed to change application status to completed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Failed To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Failed to save application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }         
        
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (_LocalLicenseInfo == null && _DetainedLicensesDgvRowIndex == -1)
            {
                MessageBox.Show("Enter local license ID First in order to release detained license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _ReleaseDetainedLicense();
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