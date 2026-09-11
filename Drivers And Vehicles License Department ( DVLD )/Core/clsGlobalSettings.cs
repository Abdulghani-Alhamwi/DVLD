using System;
using System.Drawing;

namespace DVLDPresentationLayer
{
    internal class clsGlobalSettings
    {
        public static int CurrentUserID = -1;

        public static string CurrentUserName = "";

        public static bool LoginInfoChanged = false;

        public static Color ComboBoxBackColor = Color.FromArgb(228, 228, 228);

        public static Color ComboBoxItemsBackColor = Color.FromArgb(245, 245, 245);

        public static Color ComboBoxHighlightedBackColor = Color.FromArgb(221, 232, 240);
    }
}
