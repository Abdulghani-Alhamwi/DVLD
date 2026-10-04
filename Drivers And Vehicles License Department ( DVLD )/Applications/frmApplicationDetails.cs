using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer.Controls
{
    public partial class frmApplicationDetails : Form
    {
        public event Action<clsPerson,int> OnApplicantEditedIfo;

        private int _DGVRowIndex;
        public frmApplicationDetails(int LDLApplicationID,int DGVRowIndex)
        {
            InitializeComponent();

            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
            uctrlDLApplicationInfo.LoadLDLAppInfo(LDLApplicationID);
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
    }
}
