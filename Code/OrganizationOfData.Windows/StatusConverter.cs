namespace OrganizationOfData.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Represents a status for the Record
    /// </summary>
    public enum Status
    {
        active = 0,
        inactive = 1,
        empty = 2
    }

    /// <summary>
    /// A converter from enum <see cref="Status"/> to desired language
    /// </summary>
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status status = (Status)value;
            
            switch(status)
            {
                case Status.active:
                    return "Aktivan";
                case Status.inactive:
                    return "Neaktivan";
                case Status.empty:
                    return "Prazan";
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
