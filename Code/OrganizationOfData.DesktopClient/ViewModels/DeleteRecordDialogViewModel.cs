namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;

    public class DeleteRecordDialogViewModel : ViewModel, IDialogRequestClose
    {
        private int id;
        
        /// <summary>
        /// Gets or sets an id of the record which needs to be deleted
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        private bool logical;

        /// <summary>
        /// Gets or sets a boolean value which represents if the record should be deleted logical
        /// </summary>
        public bool Logical
        {
            get
            {
                return logical;
            }
            set
            {
                logical = value;
                NotifyPropertyChanged(nameof(Logical));
            }
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteRecordDialogViewModel"/> class
        /// </summary>
        public DeleteRecordDialogViewModel()
        {
            Logical = true;
        }

        #region DeleteRecordMembers

        /// <summary>
        /// Gets an icommand for deleting a record
        /// </summary>
        public ICommand DeleteRecordCommand
        {
            get
            {
                return new ActionCommand(p => DeleteRecord(), p => Id > 0);
            }
        }

        /// <summary>
        /// Closes the dialog with the result for deleting a record
        /// </summary>
        public void DeleteRecord()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        #endregion

        #region CancelCommandMembers

        /// <summary>
        /// Gets an icommand for canceling delete of the record
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
