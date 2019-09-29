namespace OrganizationOfData.Windows
{
    using System.Windows.Data;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a transformation methods in bulk file
    /// </summary>
    public enum TransformationMethod
    {
        residualSplitting = 0,
        centralKeyDigits = 1,
        overlap = 2
    }

    /// <summary>
    /// A converter from enum <see cref="TransformationMethod"/> to desired language
    /// </summary>
    public class TransformationMethodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransformationMethod? method = (TransformationMethod?)value;

            if (method == null)
            {
                return null;
            }

            switch (method)
            {
                case TransformationMethod.residualSplitting:
                    return "Ostataka pri deljenju";
                case TransformationMethod.centralKeyDigits:
                    return "Centralnih cifara ključa";
                case TransformationMethod.overlap:
                    return "Preklapanja";
                default:
                    throw new ArgumentException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
