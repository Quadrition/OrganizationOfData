namespace OrganizationOfData.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    /// Custom rule which checks if user input is integer value
    /// </summary>
    public class IntegerValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!int.TryParse(Convert.ToString(value), out int result))
            {
                return new ValidationResult(false, "Broj je neispravan");
            }

            return new ValidationResult(true, null);
        }
    }

    /// <summary>
    /// Custom rule which checks if user input is integer value or empty
    /// </summary>
    public class EmptyIntegerValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(stringValue))
            {
                return new ValidationResult(true, null);
            }

            if (!int.TryParse(stringValue, out int result))
            {
                return new ValidationResult(false, "Broj je neispravan");
            }

            return new ValidationResult(true, null);
        }
    }
}
