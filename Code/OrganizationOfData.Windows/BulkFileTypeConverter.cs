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
            BulkFileType? status = (BulkFileType?)value;

            if (status == null)
            {
                return null;
            }

            switch (status)
            {
                case BulkFileType.withSerialOverrunZone:
                    return "Rasuta datoteka sa serijskom zonom prekoračenja";
                case BulkFileType.withSerialOverrunPrimaryZone:
                    return "Rasuta datoteka sa zonom prekoračenja u primarnoj zoni";
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
