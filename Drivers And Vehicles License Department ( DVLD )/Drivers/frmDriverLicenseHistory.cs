using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmDriverLicenseHistory : Form
    {
        public event Action<clsPerson, int> OnEditedDriverPersonalInfo;
        public event Action<clsPerson> OnUpdatedDriverInfo;

        int _DGVRowIndex;

        public frmDriverLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _LoadData(PersonID);
        }

        public frmDriverLicenseHistory(int PersonID,int DGVRowIndex)
        {
            InitializeComponent();
            _LoadData(PersonID);
            _DGVRowIndex = DGVRowIndex;
        }

        private void _LoadData(int PersonID)
        {
            uctrlPersonDetailsByFilter.SearchForPerson(PersonID);
            uctrlDriverLicensesHistory.LoadDriverLicenseHistory(clsDriver.GetDriverID(PersonID));
            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctrlPersonDetailsByFilter_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnEditedDriverPersonalInfo?.Invoke(UpdatedPersonInfo, _DGVRowIndex);
            OnUpdatedDriverInfo?.Invoke(UpdatedPersonInfo);
        }
    }
}
