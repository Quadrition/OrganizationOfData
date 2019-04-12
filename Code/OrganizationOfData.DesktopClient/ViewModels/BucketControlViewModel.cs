namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Windows;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// ViewModel containing all functionalities for BucketControl View
    /// </summary>
    public class BucketControlViewModel : ViewModel
    {
        private ObservableCollection<RecordControlViewModel> recordControlViewModels;

        public ObservableCollection<RecordControlViewModel> RecordControlViewModels
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

        /// <summary>
        /// Initializes a new instance of <see cref="BucketControlViewModel"/> class
        /// </summary>
        public BucketControlViewModel()
        {
            RecordControlViewModels = new ObservableCollection<RecordControlViewModel>();
        }
    }
}
