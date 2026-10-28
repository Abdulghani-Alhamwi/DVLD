using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmPersonInfo : Form
    {
        public event Action<object[],int> OnEditedPersonInfo;
        public event Action<clsPerson> OnUpdatedPersonInfo;
        public event Action<clsPerson,int> AfterUpdatingPersonalInfo;

        private int _PeopleDGVRowIndex;

        public frmPersonInfo(int PersonID)
        {
            InitializeComponent();
            _LoadData(PersonID);
        }

        public frmPersonInfo(int PersonID,int PeopleDGVRowIndex)
        {
            InitializeComponent();
            _LoadData(PersonID);

            _PeopleDGVRowIndex = PeopleDGVRowIndex;
        }

        private void _LoadData(int PersonID)
        {
            if (UctrlPersonDetails.LoadPersonDetails(PersonID) == null)
                this.Close();

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

        private void UctrlPersonDetails_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            object[] NewPersonInfo = new object[] {UpdatedPersonInfo.PersonID,UpdatedPersonInfo.NationalNo, UpdatedPersonInfo.FirstName ,UpdatedPersonInfo.SecondName , UpdatedPersonInfo.ThirdName ,
            UpdatedPersonInfo.LastName,(UpdatedPersonInfo.Gendor == clsPerson.enGendor.Male) ? "Male":"Female",UpdatedPersonInfo.DateOfBirth.ToShortDateString(),
            UpdatedPersonInfo.CountryName,UpdatedPersonInfo.Phone,UpdatedPersonInfo.Email};

            OnEditedPersonInfo?.Invoke(NewPersonInfo,_PeopleDGVRowIndex);
            OnUpdatedPersonInfo?.Invoke(UpdatedPersonInfo);
            AfterUpdatingPersonalInfo?.Invoke(UpdatedPersonInfo, _PeopleDGVRowIndex);
        }
    }
}
