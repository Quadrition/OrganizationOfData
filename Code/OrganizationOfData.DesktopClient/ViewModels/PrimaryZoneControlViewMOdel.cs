namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data; 

    /// <summary>
    /// ViewModel containing all functionalities for PrimaryZoneControl View
    /// </summary>
    public class PrimaryZoneControlViewModel : ZoneControlViewModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PrimaryZoneControlViewModel"/>
        /// </summary>
        public PrimaryZoneControlViewModel()
        {

        }

        public PrimaryZoneControlViewModel(Bucket[] buckets) : base(buckets)
        {

        }
    }
}
