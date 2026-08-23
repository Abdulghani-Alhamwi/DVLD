using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MyLib;

namespace DVLDPresentationLayer
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
