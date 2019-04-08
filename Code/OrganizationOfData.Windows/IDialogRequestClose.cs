namespace OrganizationOfData.Windows
{
    using System;

    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
