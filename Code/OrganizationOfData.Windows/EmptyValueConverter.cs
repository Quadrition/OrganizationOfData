namespace OrganizationOfData.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class EmptyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(System.Convert.ToString(value)))
            {
                return null;
            }

            return Binding.DoNothing;
        }
    }
}
