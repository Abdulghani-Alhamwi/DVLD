using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace DVLDPresentationLayer
{
    public partial class ctrlPersonDetailsByFilter : UserControl
    {
        public delegate void PersonSelectedEventHandler(int PersonID);
        public event PersonSelectedEventHandler OnPersonSelected;

        public event Action AfterEditingPerson;

        private enum _enFindBy { PersonID = 1 , NationalNo = 2}

        public ctrlPersonDetailsByFilter()
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

        }
        private void cbFindBy_DropDown(object sender, EventArgs e)
        {
            cbFindBy.BackColor = Color.FromArgb(245,245,245);
        }

        private void cbFindBy_DropDownClosed(object sender, EventArgs e)
        {
            cbFindBy.BackColor = Color.FromArgb(228, 228, 228);
        }

        private void txtFindBy_KeyDown(object sender, KeyEventArgs e)
        {
            if(GetSelectedItem() == _enFindBy.PersonID)
            {
                if (Char.IsDigit((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFindBy.ReadOnly = false;
                else
                    txtFindBy.ReadOnly = true;
            }
            else
                txtFindBy.ReadOnly = false;
        }

        private void cbFindBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
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
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.AfterAddingNewPerson += _LoadNewPersonData;
            frm.ShowDialog();
        }

        string _PreviouslyFoundText = null;
        private void FindPerson()
        {
          if (txtFindBy.Text == "" || string.IsNullOrWhiteSpace(txtFindBy.Text))
            {
                MessageBox.Show("Please select a person first by their National Number or ID.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                
            if(_PreviouslyFoundText == txtFindBy.Text)
                return;
            
            else if (GetSelectedItem() == _enFindBy.NationalNo)//readability
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
                    OnPersonSelected?.Invoke(Person.PersonID);
            }

        }
        private void btnFindUser_Click(object sender, EventArgs e)
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
        }
        public void LoadPersonDetails(int PersonID)
        {
          uctrlPersonDetails.LoadPersonDetails(PersonID);
        }

        private void uctrlPersonDetails_AfterEditingPersonInfo()
        {
            AfterEditingPerson?.Invoke();
        }
    }
}
