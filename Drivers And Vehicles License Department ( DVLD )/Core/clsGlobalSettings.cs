using System;
using System.Drawing;

namespace DVLDPresentationLayer
{
    internal class clsGlobalSettings
    {
        public static string EncryptionKeyName = "DVLD_EncryptionKey";

        public static int CurrentUserID = -1;
        public static string CurrentUsername = "";
        public static byte[] EncryptionKey;
        public static sbyte CurrentUserPermissions;

        public static bool LoginInfoChanged = false;

        public static Color ComboBoxBackColor = Color.FromArgb(228, 228, 228);
        public static Color ComboBoxItemsBackColor = Color.FromArgb(245, 245, 245);
        public static Color ComboBoxHighlightedBackColor = Color.FromArgb(221, 232, 240);
    }
}