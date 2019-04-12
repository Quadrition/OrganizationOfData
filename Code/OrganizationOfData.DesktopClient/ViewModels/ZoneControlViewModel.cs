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
        protected ObservableCollection<BucketControlViewModel> bucketControlViewModels;

        public ObservableCollection<BucketControlViewModel> BucketControlViewModels
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

        /// <summary>
        /// Creates a set of <see cref="BucketControlViewModel"/> based on passed buckets
        /// and adds them to ZoneControl
        /// </summary>
        /// <param name="buckets">Buckets to be added</param>
        public abstract void SetBuckets(ICollection<Bucket> buckets);
    }
}
