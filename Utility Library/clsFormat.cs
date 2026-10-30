using System;
using System.Windows.Forms;

namespace Utility_Library
{
    public class clsFormat
    {
        public enum enCustomDateFormat : byte { NumericFormat = 0, DateAppreviatedMonthName = 1, DateTimeCustomFormat = 2, TimeFormat = 3 }
        public enum enCustomNumberFormat : byte { With4ZerosAfterFraction = 0, NoJustZerosAfterFraction = 1 }

        private static string _GetNumberFormat(enCustomNumberFormat Format)
        {
            if (Format == enCustomNumberFormat.NoJustZerosAfterFraction)
                return "G29";

            else
                return "F4";
        }

        /// <summary>
        /// Takes decimal number and wanted format and returns a string contains the number and the format is :
        /// if enCustomNumberFormat.NoJustZerosAfterFraction then if there was only zeros after the fraction , it shows only the number with out the fraction and zeros after the fraction.
        /// if enCustomNumberFormat.With4ZerosAfterFraction then it will return string contain fees value with fraction and after it 4 zeros even if numbers after fraction was zeros and even if there was no fraction.
        /// </summary>
        public static string GetCustomNumberFormat(decimal Number, enCustomNumberFormat Format)
        {
            return Number.ToString(_GetNumberFormat(Format));
        }

        /// <summary>
        /// Takes float number and wanted format and returns a string contains the number and the format is :
        /// if enCustomNumberFormat.NoJustZerosAfterFraction then if there was only zeros after the fraction , it shows only the number with out the fraction and zeros after the fraction.
        /// if enCustomNumberFormat.With4ZerosAfterFraction then it will return string contain fees value with fraction and after it 4 zeros even if numbers after fraction was zeros and even if there was no fraction.
        /// </summary>
        public static string GetCustomNumberFormat(float Number, enCustomNumberFormat Format)
        {
            return Number.ToString(_GetNumberFormat(Format));
        }

        /// <summary>
        /// The enCustomDateFormat.NumericFormat returns format "dd/MM/yyyy",
        /// The enCustomDateFormat.DateAppreviatedMonthName returns format "d/MMM/yyyy";
        /// The enCustomDateFormat.DateTimeCustomFormat returns format "dd/MM/yyyy h:mm tt";
        /// The enCustomDateFormat.TimeFormat returns format "hh:mm tt";
        /// </summary>
        public static string GetCustomDateFormat(enCustomDateFormat CustomFormat)
        {
            switch (CustomFormat)
            {
                case enCustomDateFormat.NumericFormat:
                    return "dd/MM/yyyy";

                case enCustomDateFormat.DateAppreviatedMonthName:
                    return "dd/MMM/yyyy";

                case enCustomDateFormat.DateTimeCustomFormat:
                    return "dd/MM/yyyy h:mm tt";

                case enCustomDateFormat.TimeFormat:
                    return "hh:mm tt";
            }

            return null;
        }

        public static string DateToShort(DateTime Date)
        {
            return Date.ToString(GetCustomDateFormat(enCustomDateFormat.DateAppreviatedMonthName));
        }

        /// <summary>
        /// Set constraint on date by age, like entering the value 18 for MinAge parameter, then here the minimum age is 18 therefore the max date that the user will be allowed to choose in the DateTimePicker control is a date of a person its age at least 18.
        /// </summary
        public static void SetDateConstraintForAge(DateTimePicker dtpControl, byte MinAge, byte MaxAge)
        {
            DateTime MindateOfBirth = DateTime.Now.AddYears(-MinAge);

            dtpControl.Format = DateTimePickerFormat.Custom;
            dtpControl.CustomFormat = GetCustomDateFormat(enCustomDateFormat.NumericFormat);
            dtpControl.Value = MindateOfBirth;
            dtpControl.MaxDate = MindateOfBirth;

            dtpControl.MinDate = DateTime.Now.AddYears(-MaxAge);
        }
    }
}
