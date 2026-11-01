using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        public event Action<clsPerson, int> OnEditedDriverPersonalInfo;
        public event Action<clsPerson> OnUpdatedDriverInfo;

        int _PersonID;
        int _DGVRowIndex;

        public  frmShowPersonLicenseHistory()
        {
            InitializeComponent();
            _PersonID = -1;
        }

        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        public frmShowPersonLicenseHistory(int PersonID,int DGVRowIndex)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _DGVRowIndex = DGVRowIndex;
        }

        private void _LoadData(int PersonID)
        {
            if (PersonID != -1)
            {
                uctrlPersonDetailsByFilter.LoadPersonInfo(PersonID);
                uctrlPersonDetailsByFilter.FilterEnabled = false;  
                uctrlDriverLicensesHistory.LoadInfoByPersonID(PersonID);
            }

            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
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

        private void frmDriverLicenseHistory_Load(object sender, EventArgs e)
        {
            _LoadData(_PersonID);
        }

        private void uctrlPersonDetailsByFilter_OnPersonSelected(int PersonID)
        {
            _PersonID = PersonID;

            if (_PersonID == -1)
            {
                uctrlDriverLicensesHistory.Clear();
            }

            else
                uctrlDriverLicensesHistory.LoadInfoByPersonID(_PersonID);
        }
    }
}
