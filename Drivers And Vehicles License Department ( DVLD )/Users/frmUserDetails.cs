using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class frmUserDetails : Form
    {
        public delegate void UserPersonalEditedInfo(clsPerson UpdatedPersonInfo,int DGVRowIndex);
        public event UserPersonalEditedInfo OnEditedUserPersonalInfo;

        public int _DGVRowIndex;

        public frmUserDetails(int UserID)
        {
            InitializeComponent();
            _LoadData(UserID);
        }

        public frmUserDetails(int UserID,int DGVRowIndex)
        {
            InitializeComponent();
            _LoadData(UserID);
            _DGVRowIndex = DGVRowIndex;
        }

        private void _LoadData(int UserID)
        {
            if (!uctrlUserDetails.LoadUserInformation(UserID))
                this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctrlUserDetails_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnEditedUserPersonalInfo?.Invoke(UpdatedPersonInfo, _DGVRowIndex);
        }
    }
}
