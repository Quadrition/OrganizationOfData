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
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleMessageDialogViewModel"/>
        /// </summary>
        /// <param name="text">Message to be shown on dialog View</param>
        public SimpleMessageDialogViewModel(string text)
        {
            this.Message = text;
        }

        #region Dialog Members

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #endregion

        #region YesCommand Members

        public ICommand YesCommand
        {
            get
            {
                return new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            }
        }

        #endregion

        #region NoCommand Members

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
