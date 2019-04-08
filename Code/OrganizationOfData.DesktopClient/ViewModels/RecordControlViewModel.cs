namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;

    /// <summary>
    /// ViewModel containing all functionalities for RecordControl View
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of <see cref="RecordControlViewModel"/> class
        /// </summary>
        public RecordControlViewModel()
        {

        }
    }
}
