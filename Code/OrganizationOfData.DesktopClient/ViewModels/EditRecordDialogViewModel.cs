namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;

    public class EditRecordDialogViewModel : ViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        #region NewValues Members

        private BulkFile bulkFile;
        private BulkFileType bulkFileType;

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

        public BulkFileType BulkFileType
        {
            get
            {
                return bulkFileType;
            }
            set
            {
                bulkFileType = value;
                NotifyPropertyChanged(nameof(BulkFileType));

                switch (value)
                {
                    case BulkFileType.withSerialOverrunZone:
                        BulkFile = new BulkFileWithSerialOverrunZone()
                        {
                            Factor = 3,
                            NumberOfBuckets = 3
                        };
                        break;
                    case BulkFileType.withSerialOverrunPrimaryZone:
                        throw new NotImplementedException();
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        #endregion

        public EditRecordDialogViewModel()
        {
            BulkFile = new BulkFileWithSerialOverrunZone()
            {
                Factor = 3,
                NumberOfBuckets = 3
            };
        }

        #region NewFileMembers

        public ICommand CreateNewFileCommand
        {
            get
            {
                return new ActionCommand(p => CreateNewFile(), p => IsNewFileValid);
            }
        }

        private void CreateNewFile()
        {
            BulkFile.FormEmptyBulkFile();
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        public bool IsNewFileValid
        {
            get
            {
                return BulkFile != null && BulkFile.IsValid();
            }
        }

        #endregion

        #region CancelCommandMembers

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
