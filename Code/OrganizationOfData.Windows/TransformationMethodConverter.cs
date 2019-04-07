namespace OrganizationOfData.Windows
{
    using System.Windows.Data;
    using System;
    using System.Globalization;

    public enum TransformationMethod
    {
        residualSplitting = 0,
        centralKeyDigits = 1,
        overlap = 2
    }

    public class TransformationMethodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TransformationMethod method = (TransformationMethod)value;

            switch (method)
            {
                case TransformationMethod.residualSplitting:
                    return "Ostataka pri deljenju";
                case TransformationMethod.centralKeyDigits:
                    return "Centralnih cifara ključa";
                case TransformationMethod.overlap:
                    return "Preklapanja";
                default:
                    throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
