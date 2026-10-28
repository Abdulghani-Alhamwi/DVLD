using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Utility_Library
{
    public class clsValidation
    {
        public static void EnableErrorProvider(ErrorProvider erControl, Control control, string ErrorMessage, CancelEventArgs CancelArgs)
        {
            CancelArgs.Cancel = true;
            erControl.SetError(control, ErrorMessage);
        }

        public static void DisableErrorProvider(ErrorProvider erControl, Control control)
        {
            erControl.SetError(control, null);
        }

        /// <summary>
        /// Returns appropriate hour number to give to datetime object to show the time correctly if it entered hour in AM or PM.
        /// </summary
        public static byte GetAppropriatetHourNumForAmOrPm(byte EnteredHour, bool IsTime_PM)
        {
            if (IsTime_PM)
            {
                switch (EnteredHour)
                {
                    case 1:
                        EnteredHour = 13;
                        break;

                    case 2:
                        EnteredHour = 14;
                        break;

                    case 3:
                        EnteredHour = 15;
                        break;

                    case 4:
                        EnteredHour = 16;
                        break;

                    case 5:
                        EnteredHour = 17;
                        break;

                    case 6:
                        EnteredHour = 18;
                        break;

                    case 7:
                        EnteredHour = 19;
                        break;

                    case 8:
                        EnteredHour = 20;
                        break;

                    case 9:
                        EnteredHour = 21;
                        break;

                    case 10:
                        EnteredHour = 22;
                        break;

                    case 11:
                        EnteredHour = 23;
                        break;
                }
            }
            else
            {
                if (EnteredHour == 12)
                    EnteredHour = 0;
            }

            return EnteredHour;
        }

        public static byte GetHourFromInputFromTextBox(Control ctrl)
        {
            return Convert.ToByte(ctrl.Text.Substring(0, 2));
        }

        public static byte GetMinuteFromInputFromTextBox(Control ctrl)
        {
            return Convert.ToByte(ctrl.Text.Substring(3, 2));
        }

        public static string GetAmOrPmFromInputFromTextBox(Control ctrl)
        {
            return ctrl.Text.Substring(6, 2);
        }

        public static bool IsTimeInAmInputToTextBox(Control ctrl)
        {
            return (string.Compare(GetAmOrPmFromInputFromTextBox(ctrl), "AM", true) == 0);
        }

        public static bool IsTimeInPmInputToTextBox(Control ctrl)
        {
            return (string.Compare(GetAmOrPmFromInputFromTextBox(ctrl), "PM", true)) == 0;
        }

        public static bool IsValidEnteredTimeInTextBox(Control ctrl, DateTime ChoosedDate)
        {
            DateTime CurrentDateTime = DateTime.Now;

            if (!(CurrentDateTime.Year == ChoosedDate.Year && CurrentDateTime.Month == ChoosedDate.Month && CurrentDateTime.Day == ChoosedDate.Day))
                return true;

            byte EnteredHour = 0;
            byte EnteredMinute = 0;

            if (ctrl.Text != "")
            {
                EnteredHour = GetHourFromInputFromTextBox(ctrl);

                if (IsTimeInPmInputToTextBox(ctrl))
                    EnteredHour = GetAppropriatetHourNumForAmOrPm(EnteredHour, true);
                else
                    EnteredHour = GetAppropriatetHourNumForAmOrPm(EnteredHour, false);

                EnteredMinute = GetMinuteFromInputFromTextBox(ctrl);
            }
            else
                return false;

            if (EnteredHour > CurrentDateTime.Hour)
                return true;

            else if (CurrentDateTime.Hour == EnteredHour)
            {
                return (CurrentDateTime.Minute < EnteredMinute);
            }

            else
                return false;
        }

        public static bool ValidateEmail(string EmailAddress)
        {
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(EmailAddress);
        }

        public static bool ValidateInteger(string Number)
        {
            string pattern = @"^[0-9]*$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            string pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }

        /// <summary>
        /// Validate a part of the full name : first, second, third or last name.
        /// </summary
        public static bool ValidateNamePart(string Name)
        {
            string pattern = @"^[a-zA-Z]+$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(Name);
        }
    }
}
