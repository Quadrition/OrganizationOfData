namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A base ViewModel class for zone control
    /// </summary>
    public class ZoneControlViewModel : ViewModel
    {
        private ICollection<BucketControlViewModel> bucketControlViewModels;

        /// <summary>
        /// Gets or sets a bucket controls for the zone control
        /// </summary>
        public ICollection<BucketControlViewModel> BucketControlViewModels
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
            BucketControlViewModels = new ObservableCollection<BucketControlViewModel>();
        }
        //TODO mozda ne treba ovo
        public ZoneControlViewModel(Bucket[] buckets, bool overrunedZoneVisibility)
        {
            BucketControlViewModels = new ObservableCollection<BucketControlViewModel>();

            for (int i = 0; i < buckets.Length; i++)
            {
                BucketControlViewModels.Add(new BucketControlViewModel(buckets[i])
                {
                    Visibility = overrunedZoneVisibility ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed,
                    OverrunedRecords = buckets[i].OverrunedRecords
                });
            }
        }
    }
}
