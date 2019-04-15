namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// ViewModel containing all functionalities for BucketControl View
    /// </summary>
    public class BucketControlViewModel : ViewModel
    {
        private RecordControlViewModel[] recordControlViewModels;

        public RecordControlViewModel[] RecordControlViewModels
        {
            get
            {
                return recordControlViewModels;
            }
            set
            {
                recordControlViewModels = value;
                NotifyPropertyChanged();
            }
        }

        private int address;

        public int Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                NotifyPropertyChanged(nameof(Address));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BucketControlViewModel"/> class
        /// </summary>
        public BucketControlViewModel(int address, int factor)
        {
            Address = address;
            RecordControlViewModels = new RecordControlViewModel[factor];
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BucketControlViewModel"/> class and copies data from existing Record
        /// </summary>
        public BucketControlViewModel(BucketControlViewModel bucketControlViewModel)
        {
            RecordControlViewModels = new RecordControlViewModel[bucketControlViewModel.RecordControlViewModels.Length];
            Address = bucketControlViewModel.Address;

            for (int i = 0; i < bucketControlViewModel.RecordControlViewModels.Length; i++)
            {
                RecordControlViewModels[i] = new RecordControlViewModel()
                {
                    Record = new Record(bucketControlViewModel.RecordControlViewModels[i].Record)
                };
            }
        }

        public BucketControlViewModel(Bucket bucket) : this(bucket.Address, bucket.Records.Length)
        {
            for (int i = 0; i < bucket.Records.Length; i++)
            {
                RecordControlViewModels[i] = new RecordControlViewModel()
                {
                    Record = bucket.Records[i]
                };
            }
        }
    }
}
