namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

        public override void SetBuckets(ICollection<Bucket> buckets)
        {
            BucketControlViewModels = new ObservableCollection<BucketControlViewModel>();

            foreach(Bucket bucket in buckets)
            {
                BucketControlViewModel bucketControlViewModel = new BucketControlViewModel()
                {
                    RecordControlViewModels = new ObservableCollection<RecordControlViewModel>()
                };
                
                foreach(Record record in bucket.Records)
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
