using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        private enum _enFindBy { PersonID = 1 , NationalNo = 2}

        public delegate void PersonSelectedEventHandler(int PersonID);
        public event PersonSelectedEventHandler OnPersonSelected;

        public event ctrlPersonCard.PersonEditedInfo OnPersonEditedInfo;

        private string _PreviouslyFoundText = null;

        private bool _EnableEditPersonLabel = false;
        public bool EnableEditPersonLabel
        {
            get { return _EnableEditPersonLabel;}

            set 
            {
                _EnableEditPersonLabel = value;
                uctrlPersonDetails.lnlblEditPersonInfo.Enabled = _EnableEditPersonLabel;
            }
        }

        private bool _EnableFilter = false;
        public bool EnableFilter
        {
            get { return _EnableFilter; }

            set
            {
                _EnableFilter = value;
                gbFilter.Enabled = _EnableFilter;
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private _enFindBy GetSelectedItem()
        {
            switch(cbFindBy.SelectedItem)
            {
                case "Person ID":
                    return _enFindBy.PersonID;

                case "National No":
                    return _enFindBy.NationalNo;
            }

            return _enFindBy.NationalNo;
        }

        private void PersonInformationByFilter_Load(object sender, EventArgs e)
        {
            object[] Items = new object[] { "National No", "Person ID" };
            cbFindBy.Items.AddRange(Items);
            cbFindBy.SelectedIndex = 0;
            EnableFilter = true;
        }

        private void cbFindBy_DropDown(object sender, EventArgs e)
        {
            cbFindBy.BackColor = Color.FromArgb(245,245,245);
        }

        private void cbFindBy_DropDownClosed(object sender, EventArgs e)
        {
            cbFindBy.BackColor = Color.FromArgb(228, 228, 228);
        }

        private void cbFindBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsGeneralUtility.DrawComboBoxItems(sender, e);
        }

        private void _LoadNewPersonData(int PersonID)
        {
            txtFindBy.Text = PersonID.ToString();
            cbFindBy.SelectedItem = "Person ID";
            uctrlPersonDetails.LoadPersonDetails(PersonID);
            OnPersonSelected?.Invoke(PersonID);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersonInfo frm = new frmAddUpdatePersonInfo();
            frm.AfterAddingNewPerson += _LoadNewPersonData;
            frm.ShowDialog();
        }

        private void FindPerson()
        {
            if (txtFindBy.Text == "" || string.IsNullOrWhiteSpace(txtFindBy.Text))
            {
                MessageBox.Show("Please select a person first by their National Number or ID.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_PreviouslyFoundText == txtFindBy.Text)
                return;

            else if (GetSelectedItem() == _enFindBy.NationalNo)
            {
                clsPerson Person = uctrlPersonDetails.LoadPersonDetails(txtFindBy.Text);
                _PreviouslyFoundText = txtFindBy.Text;

                if (Person != null)
                    OnPersonSelected?.Invoke(Person.PersonID);
            }

            else
            {
                clsPerson Person = uctrlPersonDetails.LoadPersonDetails(Convert.ToInt32(txtFindBy.Text));
                _PreviouslyFoundText = txtFindBy.Text;

                if (Person != null)
                OnPersonSelected?.Invoke(Person.PersonID);
            }

        }

        public void SearchForPerson(int PersonID)
        {
            cbFindBy.SelectedItem = "PersonID";
            txtFindBy.Text = PersonID.ToString();
            uctrlPersonDetails.LoadPersonDetails(PersonID);
            gbFilter.Enabled = false;           
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindPerson();
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetSelectedItem() == _enFindBy.PersonID)
            {
                if (!txtFindBy.Text.All(char.IsDigit))
                    txtFindBy.Clear();
            }
            _PreviouslyFoundText = "";
        }

        public void LoadPersonDetails(int PersonID)
        {
          uctrlPersonDetails.LoadPersonDetails(PersonID);
        }

        private void uctrlPersonDetails_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnPersonEditedInfo?.Invoke(UpdatedPersonInfo);
        }

        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (GetSelectedItem() == _enFindBy.PersonID)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtFindBy_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnFind.PerformClick();
            }
        }

        public void FocusOnFilterTextBox()
        {
            txtFindBy.Focus();
        }
    }
}