using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer
{
    internal static class Program
    {
        private static void _AddEncryptionKey()
        {
            if (!clsCryptography.CredentialManager.GetStoredCredential(clsGlobalSettings.EncryptionKeyName, out byte[] SavedEncryptionKey))
            {
                byte[] EncryptionKey = clsCryptography.GenerateEncryptionKey(16);
                clsCryptography.CredentialManager.StoreCredential(clsGlobalSettings.EncryptionKeyName, EncryptionKey);

                clsGlobalSettings.EncryptionKey = EncryptionKey;
            }
            else
            {
                clsGlobalSettings.EncryptionKey = SavedEncryptionKey;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _AddEncryptionKey();

            Application.Run(new frmLoginScreen());  
        }
    }
}
