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

                switch(record.Status)
                {
                    case Status.active:
                        ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark;
                        break;
                    case Status.inactive:
                        ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryMid;
                        break;
                    case Status.empty:
                        ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryLight;
                        break;
                }
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
            ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark;
        }

        public void Select()
        {
            ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.Accent;
        }

        public void UnSelect()
        {
            switch (record.Status)
            {
                case Status.active:
                    ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark;
                    break;
                case Status.inactive:
                    ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryMid;
                    break;
                case Status.empty:
                    ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryLight;
                    break;
            }
        }
    }
}
