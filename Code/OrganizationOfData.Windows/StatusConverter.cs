namespace OrganizationOfData.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public enum Status
    {
        active = 0,
        inactive = 1,
        empty = 2
    }

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
                    return "Obrisan";
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
