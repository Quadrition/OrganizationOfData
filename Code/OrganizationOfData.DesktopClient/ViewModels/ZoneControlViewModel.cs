namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;

    /// <summary>
    /// A base ViewModel class for zone control
    /// </summary>
    public abstract class ZoneControlViewModel : ViewModel
    {
        protected BucketControlViewModel[] bucketControlViewModels;

        public BucketControlViewModel[] BucketControlViewModels
        {
            get
            {
                return bucketControlViewModels;
            }
            set
            {
                bucketControlViewModels = value;
                NotifyPropertyChanged(nameof(BucketControlViewModels));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ZoneControlViewModel"/>
        /// </summary>
        public ZoneControlViewModel()
        {

        }

        public ZoneControlViewModel(Bucket[] buckets)
        {
            BucketControlViewModels = new BucketControlViewModel[buckets.Length];

            for (int i = 0; i < buckets.Length; i++)
            {
                BucketControlViewModels[i] = new BucketControlViewModel(buckets[i]);
            }
        }
    }
}
