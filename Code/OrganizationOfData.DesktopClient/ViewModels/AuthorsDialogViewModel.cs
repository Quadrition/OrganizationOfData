namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System;
    using System.Windows.Input;
    using OrganizationOfData.Windows;

    /// <summary>
    /// ViewModel containing basic functionalities like closing the dialog for OverrunZoneControl View
    /// </summary>
    public class AuthorsDialogViewModel : ViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Initializes a new instance of <see cref="AuthorsDialogViewModel"/> class
        /// </summary>
        public AuthorsDialogViewModel()
        {

        }

        #region CloseCommand Members

        /// <summary>
        /// Gets an iccomand for closing a dialog
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                return new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
            }
        }

        #endregion  
    }
}
