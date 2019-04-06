namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;

    public class RecordControlViewModel : ViewModel
    {
        private Record record;

        public Record Record
        {
            get
            {
                return record;
            }
            set
            {
                record = value;
                NotifyPropertyChanged(nameof(Record));
            }
        }

        public RecordControlViewModel()
        {

        }
    }
}
