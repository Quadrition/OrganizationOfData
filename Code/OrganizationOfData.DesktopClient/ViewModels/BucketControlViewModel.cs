namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Windows;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

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

        public BucketControlViewModel()
        {
            RecordControlViewModels = new ObservableCollection<RecordControlViewModel>();
        }
    }
}
