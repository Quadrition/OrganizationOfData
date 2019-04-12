namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System;
    using System.Windows.Input;
    using OrganizationOfData.Windows;

    public class AuthorsWindowViewModel : ViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public AuthorsWindowViewModel()
        {

        }

        #region CancelCommandMembers

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
