namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System;
    using OrganizationOfData.Windows;

    public class AuthorsWindowViewModel : ViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public AuthorsWindowViewModel()
        {

        }
    }
}
