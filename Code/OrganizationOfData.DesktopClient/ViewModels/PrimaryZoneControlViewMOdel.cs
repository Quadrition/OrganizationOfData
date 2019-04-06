namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Collections.Generic;
    using OrganizationOfData.Windows;

    public class PrimaryZoneControlViewModel : ViewModel
    {
        private ICollection<BucketControlViewModel> bucketControlViewModels;

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

        public PrimaryZoneControlViewModel()
        {

        }
    }
}
