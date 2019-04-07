namespace OrganizationOfData.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;

    public class IntegerValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string stringValue = Convert.ToString(value);

            if (string.IsNullOrWhiteSpace(stringValue))
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
