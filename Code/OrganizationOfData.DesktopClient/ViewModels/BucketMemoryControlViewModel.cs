namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Windows;

    public class BucketMemoryControlViewModel : ViewModel
    {
        private BucketControlViewModel bucketControlViewModel;

        public BucketControlViewModel BucketControlViewModel
        {
            get
            {
                return bucketControlViewModel;
            }
            set
            {
                bucketControlViewModel = value;
                NotifyPropertyChanged(nameof(BucketControlViewModel));
            }
        }

        public BucketMemoryControlViewModel()
        {

        }
    }
}
