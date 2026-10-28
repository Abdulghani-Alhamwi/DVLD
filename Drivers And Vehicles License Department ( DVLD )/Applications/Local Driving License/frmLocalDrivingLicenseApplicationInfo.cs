using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer.Controls
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        public event Action<clsPerson,int> OnApplicantEditedIfo;

        private int _DGVRowIndex;
        private int _LocalDrivingLicenseApplicationID;
        public frmLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID,int DGVRowIndex)
        {
            InitializeComponent();

            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _DGVRowIndex = DGVRowIndex;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctrlDLApplicationInfo_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnApplicantEditedIfo?.Invoke(UpdatedPersonInfo, _DGVRowIndex);
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            uctrlDLApplicationInfo.LoadInfoByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);
        }
    }
}
