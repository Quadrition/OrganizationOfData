namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;

    /// <summary>
    /// ViewModel containing all functionalities for NewFileWindow View
    /// </summary>
    public class NewFileWindowViewModel : ViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #region BulkFileMembers

        private BulkFile bulkFile;

        /// <summary>
        /// Gets or sets a new bulk file
        /// </summary>
        public BulkFile BulkFile
        {
            get
            {
                return bulkFile;
            }
            set
            {
                bulkFile = value;
                NotifyPropertyChanged(nameof(BulkFile));
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref="NewFileWindowViewModel"/> class
        /// </summary>
        public NewFileWindowViewModel()
        {
            BulkFile = new BulkFile()
            {
                Factor = 3,
                NumberOfBuckets = 3
            };
        }

        #region NewFileMembers

        /// <summary>
        /// Gets and icommand for creating a new file
        /// </summary>
        public ICommand CreateNewFileCommand
        {
            get
            {
                return new ActionCommand(p => CreateNewFile(), p => BulkFile.IsValid);
            }
        }

        /// <summary>
        /// Forms a new empty bulk file and closes the dialog
        /// </summary>
        private void CreateNewFile()
        {
            BulkFile.FormEmptyBulkFile();
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        #endregion

        #region CancelCommandMembers

        /// <summary>
        /// Gets an icommand for canceling input of new file
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
