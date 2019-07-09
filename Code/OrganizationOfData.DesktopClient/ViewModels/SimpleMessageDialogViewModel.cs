namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;

    /// <summary>
    /// ViewModel containing all functionalities for SimpleMessageDialog View
    /// </summary>
    public class SimpleMessageDialogViewModel : ViewModel, IDialogRequestClose
    {
        /// <summary>
        /// Gets or sets a message that needs to be shown on the dialog
        /// </summary>
        public string Message { get; set; }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleMessageDialogViewModel"/>
        /// </summary>
        /// <param name="text">Message to be shown on the dialog view</param>
        public SimpleMessageDialogViewModel(string text)
        {
            Message = text;
        }

        #region YesNoCommand Members

        /// <summary>
        /// Gets an icommand for confirming the question message
        /// </summary>
        public ICommand YesCommand
        {
            get
            {
                return new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            }
        }

        /// <summary>
        /// Gets an icommand for denying the question message
        /// </summary>
        public ICommand NoCommand
        {
            get
            {
                return new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
            }
        }

        #endregion
    }
}
