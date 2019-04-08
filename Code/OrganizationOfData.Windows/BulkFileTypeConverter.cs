namespace OrganizationOfData.Windows
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public enum BulkFileType
    {
        withSerialOverrunZone = 0,
        withSerialOverrunPrimaryZone = 1
    }

    /// <summary>
    /// A converter from enum <see cref="BulkFileType"/> to desired language
    /// </summary>
    public class BulkFileTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BulkFileType status = (BulkFileType)value;

            switch (status)
            {
                case BulkFileType.withSerialOverrunZone:
                    return "Zona prekoračenja";
                case BulkFileType.withSerialOverrunPrimaryZone:
                    return "Primarna zona";
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
