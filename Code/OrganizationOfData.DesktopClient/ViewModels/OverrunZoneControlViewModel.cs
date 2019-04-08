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

        /// <summary>
        /// Overrides a base SetBuckets class from <see cref="ZoneControlViewModel"/> base class
        /// </summary>
        /// <param name="buckets">Buckets to be added</param>
        public override void SetBuckets(ICollection<Bucket> buckets)
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
