namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using OrganizationOfData.Data;

    /// <summary>
    /// ViewModel containing all functionalities for OverrunZoneControl View
    /// </summary>
    public class OverrunZoneControlViewModel : ZoneControlViewModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OverrunZoneControlViewModel"/> class
        /// </summary>
        public OverrunZoneControlViewModel()
        {

        }

        public OverrunZoneControlViewModel(Bucket[] buckets) : base(buckets)
        {

        }
    }
}
