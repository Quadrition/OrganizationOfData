namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System;
    using System.Windows.Input;
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;

    /// <summary>
    /// ViewModel containing all functionalities for NewRecordDialog View
    /// </summary>
    public class NewRecordDialogViewModel : ViewModel, IDialogRequestClose
    {
        #region NewRecord Members

        private Person newPerson;

        /// <summary>
        /// Gets or sets a new record
        /// </summary>
        public Person NewPerson
        {
            get
            {
                return newPerson;
            }
            set
            {
                newPerson = value;
                NotifyPropertyChanged(nameof(NewPerson));
            }
        }

        #endregion

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Initializes a new instance of <see cref="NewRecordDialogViewModel"/> class
        /// </summary>
        public NewRecordDialogViewModel()
        {
            NewPerson = new Person();
        }

        #region CreateNewRecord Members

        /// <summary>
        /// Gets an icommand for creating a new record
        /// </summary>
        public ICommand CreateNewRecordCommand
        {
            get
            {
                return new ActionCommand(p => CreateNewRecord(), p => NewPerson.IsValid());
            }
        }

        /// <summary>
        /// Closes the dialog and returns a result of confirming creation of a new record
        /// </summary>
        public void CreateNewRecord()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        #endregion

        #region CancelCommand Members

        /// <summary>
        /// Gets an icommand for canceling input of a new record
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                return new ActionCommand(p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
            }
        }

        #endregion
    }
}
