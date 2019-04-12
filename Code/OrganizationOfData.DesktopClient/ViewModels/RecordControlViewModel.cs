namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using MaterialDesignColors;

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

        private MaterialDesignThemes.Wpf.ColorZoneMode colorZoneMode;

        public MaterialDesignThemes.Wpf.ColorZoneMode ColorZoneMode
        {
            get
            {
                return colorZoneMode;
            }
            set
            {
                colorZoneMode = value;
                NotifyPropertyChanged(nameof(ColorZoneMode));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RecordControlViewModel"/> class
        /// </summary>
        public RecordControlViewModel()
        {
            ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryLight;
        }

        public void Select()
        {
            ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.Accent;
        }

        public void UnSelect()
        {
            ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryLight;
        }
    }
}
