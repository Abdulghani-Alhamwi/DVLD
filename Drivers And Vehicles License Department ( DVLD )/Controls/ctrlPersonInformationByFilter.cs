using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class PersonInformationByFilter : UserControl
    {
        public PersonInformationByFilter()
        {
            InitializeComponent();
        }
        string _NationalNo = "";
        int _PersonID = -1;
        public string NationalNo { get { return _NationalNo; } }
        public int PersonID { get { return _PersonID; } }
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
            if(cbFindBy.SelectedItem.ToString()=="Person ID")
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

        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.AddedNewPerson += _LoadNewPersonData;
            frm.ShowDialog();
        }

        string PreviouslyFoundText = null;
        public bool FindPerson()
        {
          if (string.IsNullOrEmpty(txtFindBy.Text))
            {
                MessageBox.Show("Please select a person first by their National Number or ID.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
                
            if(PreviouslyFoundText == txtFindBy.Text)
                return true;
            
            else if (cbFindBy.SelectedItem.ToString() == "National No")
            {
                if (!clsUser.IsUserExists(txtFindBy.Text))
                {
                    _NationalNo = txtFindBy.Text;
                    uctrlPersonDetails.LoadPersonDetails(_NationalNo);
                    PreviouslyFoundText = txtFindBy.Text;
                    return true;
                }
            }

            else
            {
                if (!clsUser.IsUserExists(Convert.ToInt32(txtFindBy.Text)))
                {
                    _PersonID = Convert.ToInt32(txtFindBy.Text);
                    uctrlPersonDetails.LoadPersonDetails(_PersonID);
                    PreviouslyFoundText = txtFindBy.Text;
                    return true;
                }
            }

            MessageBox.Show("The selected person already has a user. Choose another one.", "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }
        private void btnFindUser_Click(object sender, EventArgs e)
        {
            FindPerson();
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFindBy.SelectedItem.ToString() == "Person ID")
            {
                if (!txtFindBy.Text.All(char.IsDigit))
                    txtFindBy.Clear();
            }
        }
    }
}
