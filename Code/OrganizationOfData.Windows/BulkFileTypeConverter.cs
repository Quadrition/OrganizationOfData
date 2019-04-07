namespace OrganizationOfData.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public enum BulkFileType
    {
        withSerialOverrunZone = 0,
        ovoDrugo = 1
    }

    public class BulkFileTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BulkFileType status = (BulkFileType)value;

            switch (status)
            {
                case BulkFileType.withSerialOverrunZone:
                    return "Sa serijskom zonom prekoračenja";
                case BulkFileType.ovoDrugo:
                    return "Ovo drugo";
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
