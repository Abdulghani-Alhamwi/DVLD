using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmMainScreen : Form
    {
        
        // Change Win32 style to remove the MDI client border -> to remove the mdi Client (sunken = 3d border) .
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr windowHandle, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr windowHandle, int index, int newStyle);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr windowHandle, IntPtr insertAfterHandle,
                              int x, int y, int width, int height, int flags);

        private const int ExtendedStyleIndex = -20;
        private const int ClientEdgeExtendedStyle = 0x00000200;
        private const int NoSizeFlag = 0x0001;
        private const int NoMoveFlag = 0x0002;
        private const int NoZOrderFlag = 0x0004;
        private const int FrameChangedFlag = 0x0020;

        private void _RemoveMdiClientBorder()
        {

            MdiClient mdiClient = this.Controls.OfType<MdiClient>().FirstOrDefault();
            if (mdiClient == null)
            {
                return;
            }

            int currentExtendedStyle = GetWindowLong(mdiClient.Handle, ExtendedStyleIndex);
            int updatedExtendedStyle = currentExtendedStyle & ~ClientEdgeExtendedStyle;

            SetWindowLong(mdiClient.Handle, ExtendedStyleIndex, updatedExtendedStyle);

            SetWindowPos(mdiClient.Handle, IntPtr.Zero, 0, 0, 0, 0, NoSizeFlag | NoMoveFlag | NoZOrderFlag | FrameChangedFlag);

        }

        private frmLoginScreen _frmLogin;
        private bool _SignOut = false;
        public frmMainScreen(frmLoginScreen frmLogin)
        {
            InitializeComponent();
            _RemoveMdiClientBorder();

            _frmLogin = frmLogin;
        }

        private Size SetFormsSize(int width = 200 , int height = 300)
        {
            return new Size(this.Width - width, this.Height - height);
        }
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeopleManagement frm = new frmPeopleManagement();
            frm.Size = SetFormsSize();
            frm.ShowDialog();            
        }
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsersManagement frm = new frmUsersManagement();
                
            frm.ShowDialog();
        }

        private void frmMainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_SignOut)
                _frmLogin.Close();
            else
                _frmLogin.Show();
        }

        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails(clsGlobalSettings.CurrentUserID);
            frm.ShowDialog();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobalSettings.CurrentUserID);
            frm.ShowDialog();
        }

        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
            _SignOut = true;
            this.Close();
        }
    }
}
