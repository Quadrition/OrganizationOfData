namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Media;

    /// <summary>
    /// ViewModel containing all functionalities for RecordControl View
    /// </summary>
    public class RecordControlViewModel : ViewModel
    {
        private Record record;

        /// <summary>
        /// Gets or sets a record which needs to be displayed
        /// </summary>
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

                ResetColor();
            }
        }

        private MaterialDesignThemes.Wpf.ColorZoneMode colorZoneMode;

        /// <summary>
        /// Gets or sets a color zone of the record control
        /// </summary>
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

        private SolidColorBrush labelColor;

        /// <summary>
        /// Gets or sets a color of labels in the control
        /// </summary>
        public SolidColorBrush LabelColor
        {
            get
            {
                return labelColor;
            }
            set
            {
                labelColor = value;
                NotifyPropertyChanged(nameof(LabelColor));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RecordControlViewModel"/> class
        /// </summary>
        public RecordControlViewModel()
        {
            ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.Light;
            LabelColor = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Selects a label when it is compared
        /// </summary>
        public void Select()
        {
            ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.Accent;
            LabelColor = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Resets color which dependes on status of the record
        /// </summary>
        public void ResetColor()
        {
            switch (record.Status)
            {
                case Status.active:
                    ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark;
                    LabelColor = new SolidColorBrush(Colors.White);
                    break;
                case Status.inactive:
                    ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryMid;
                    LabelColor = new SolidColorBrush(Colors.White);
                    break;
                case Status.empty:
                    ColorZoneMode = MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryLight;
                    LabelColor = new SolidColorBrush(Colors.Black);
                    break;
            }
        }
    }
}
