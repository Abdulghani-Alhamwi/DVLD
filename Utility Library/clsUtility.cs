using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Utility_Library
{
    public class clsUtility
    {
        private const int _ExtendedStyleIndex = -20;
        private const int _ClientEdgeExtendedStyle = 0x00000200;
        private const int _NoSizeFlag = 0x0001;
        private const int _NoMoveFlag = 0x0002;
        private const int _NoZOrderFlag = 0x0004;
        private const int _FrameChangedFlag = 0x0020;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr windowHandle, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr windowHandle, int index, int newStyle);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr windowHandle, IntPtr insertAfterHandle,
                              int x, int y, int width, int height, int flags);

        /// <summary>
        /// Change Win32 style to remove the MDI client 3d border (sunken)
        /// </summary>
        public static void RemoveMdiClientBorder(Form frm)
        {
            MdiClient mdiClient = frm.Controls.OfType<MdiClient>().FirstOrDefault();
            if (mdiClient == null)
            {
                return;
            }

            int currentExtendedStyle = GetWindowLong(mdiClient.Handle, _ExtendedStyleIndex);
            int updatedExtendedStyle = currentExtendedStyle & ~_ClientEdgeExtendedStyle;

            SetWindowLong(mdiClient.Handle, _ExtendedStyleIndex, updatedExtendedStyle);

            SetWindowPos(mdiClient.Handle, IntPtr.Zero, 0, 0, 0, 0, _NoSizeFlag | _NoMoveFlag | _NoZOrderFlag | _FrameChangedFlag);
        }

        public static void DrawComboBoxItems(object sender, DrawItemEventArgs e, string ColumnName = null)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            string ItemText;

            if (ColumnName != null)
            {
                DataRowView RowView = (DataRowView)((ComboBox)sender).Items[e.Index];
                ItemText = RowView[ColumnName].ToString();
            }
            else
                ItemText = ((ComboBox)sender).Items[e.Index].ToString();

            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(ItemText, e.Font, brush, e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        public static void CenterControlHorizontally(Control ContainerControl, Control control)
        {
            control.Location = new Point(ContainerControl.Width / 2 - control.Width / 2, control.Location.Y);
        }

        public static void ChangeMdiFormBackColor(Form frm, Color BackColor)
        {
            MdiClient mdiClient = frm.Controls.OfType<MdiClient>().FirstOrDefault();
            mdiClient.BackColor = BackColor;
        }

        public static string GenerateGuid()
        {
            Guid guid = Guid.NewGuid();

            return guid.ToString();
        }

    }
}