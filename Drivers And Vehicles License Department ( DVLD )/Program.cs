using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLoginScreen());
        }
    }
}
