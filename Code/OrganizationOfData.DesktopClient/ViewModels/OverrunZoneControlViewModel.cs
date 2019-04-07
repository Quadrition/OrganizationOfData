namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;

    public class OverrunZoneControlViewModel : ViewModel
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

        public OverrunZoneControlViewModel()
        {

        }

        public void SetBuckets(ICollection<Bucket> buckets)
        {
            BucketControlViewModels = new ObservableCollection<BucketControlViewModel>();

            foreach (Bucket bucket in buckets)
            {
                BucketControlViewModel bucketControlViewModel = new BucketControlViewModel()
                {
                    RecordControlViewModels = new ObservableCollection<RecordControlViewModel>()
                };

                foreach (Record record in bucket.Records)
                {
                    bucketControlViewModel.RecordControlViewModels.Add(new RecordControlViewModel()
                    {
                        Record = record
                    });
                }

                BucketControlViewModels.Add(bucketControlViewModel);
            }
        }
    }
}
