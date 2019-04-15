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
        private ICollection<RecordControlViewModel> recordControlViewModels;

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
        public BucketControlViewModel(int address)
        {
            RecordControlViewModels = new ObservableCollection<RecordControlViewModel>();
            Address = address;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BucketControlViewModel"/> class and copies data from existing Record
        /// </summary>
        public BucketControlViewModel(BucketControlViewModel bucketControlViewModel)
        {
            RecordControlViewModels = new ObservableCollection<RecordControlViewModel>();
            Address = bucketControlViewModel.Address;

            foreach (RecordControlViewModel recordControlViewModel in bucketControlViewModel.RecordControlViewModels)
            {
                RecordControlViewModels.Add(new RecordControlViewModel
                {
                    Record = new Record(recordControlViewModel.Record)
                });
            }
        }
    }
}
