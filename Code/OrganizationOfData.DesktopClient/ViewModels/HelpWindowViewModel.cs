namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System;
    using OrganizationOfData.Windows;

    /// <summary>
    /// ViewModel containing basic functionalities like closing the dialog for HelpWindow View
    /// </summary>
    public class HelpWindowViewModel : ViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Initializes a new instance of <see cref="HelpWindowViewModel"/> class
        /// </summary>
        public HelpWindowViewModel()
        {
            SelectedIndex = 0;
        }

        #region Transitioner

        private int selectedIndex;

        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;

                NotifyPropertyChanged(nameof(SelectedIndex));
            }
        }

        #endregion

        public void Close()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }
    }
}
