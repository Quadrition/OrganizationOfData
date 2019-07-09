namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// ViewModel containing all functionalities for BucketControl View
    /// </summary>
    public class BucketControlViewModel : ViewModel
    {
        private ICollection<RecordControlViewModel> recordControlViewModels;

        /// <summary>
        /// Gets or sets a record controls which needs to be shown on bucket control
        /// </summary>
        public ICollection<RecordControlViewModel> RecordControlViewModels
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

        /// <summary>
        /// Gets or sets the address of the bucket
        /// </summary>
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

        private int overrunedRecords;

        /// <summary>
        /// Gets or sets the number of records that are in overrun zone
        /// </summary>
        public int OverrunedRecords
        {
            get
            {
                return overrunedRecords;
            }
            set
            {
                overrunedRecords = value;

                NotifyPropertyChanged(nameof(OverrunedRecords));
            }
        }

        private Visibility overrunedRecordsVisibility;

        public Visibility OverrunedRecordsVisibility
        {
            get
            {
                return overrunedRecordsVisibility;
            }
            set
            {
                overrunedRecordsVisibility = value;

                NotifyPropertyChanged(nameof(OverrunedRecordsVisibility));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BucketControlViewModel"/> class
        /// </summary>
        public BucketControlViewModel(int address)
        {
            Address = address;
            RecordControlViewModels = new ObservableCollection<RecordControlViewModel>();
            OverrunedRecords = 0;
            OverrunedRecordsVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BucketControlViewModel"/> class and copies data from existing Record
        /// </summary>
        /// <param name="bucket"></param>
        public BucketControlViewModel(Bucket bucket) : this(bucket.Address)
        {
            for (int i = 0; i < bucket.Records.Length; i++)
            {
                RecordControlViewModels.Add(new RecordControlViewModel()
                {
                    Record = bucket.Records[i]
                });
            }
        }

        public BucketControlViewModel(BucketControlViewModel bucketControlViewModel) : this(bucketControlViewModel.Address)
        {
            OverrunedRecordsVisibility = bucketControlViewModel.OverrunedRecordsVisibility;
            OverrunedRecords = bucketControlViewModel.OverrunedRecords;

            foreach (RecordControlViewModel recordControl in bucketControlViewModel.RecordControlViewModels)
            {
                RecordControlViewModels.Add(new RecordControlViewModel()
                {
                    Record = recordControl.Record
                });
            }
        }
    }
}
