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

        public override void SetBuckets(Bucket[] buckets)
        {
            BucketControlViewModels = new ObservableCollection<BucketControlViewModel>();

            for (int i = 0; i < buckets.Length; i++)
            {
                BucketControlViewModel bucketControlViewModel = new BucketControlViewModel(i)
                {
                    RecordControlViewModels = new ObservableCollection<RecordControlViewModel>()
                };
                
                foreach(Record record in buckets[i].Records)
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
