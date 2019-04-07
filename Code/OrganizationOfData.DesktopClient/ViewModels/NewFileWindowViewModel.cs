namespace OrganizationOfData.DesktopClient.ViewModels
{
    using MvvmDialogs;
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;

    public class NewFileWindowViewModel : ViewModel, IModalDialogViewModel
    {
        private bool? dialogResult;
        private BulkFileType bulkFileType;
        private BulkFile bulkFile;

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

                switch(value)
                {
                    case BulkFileType.withSerialOverrunZone:
                        BulkFile = new BulkFileWithSerialOverrunZone();
                        break;
                    case BulkFileType.withSerialOverrunPrimaryZone:
                        throw new NotImplementedException();
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
                NotifyPropertyChanged();
            }
        }

        public NewFileWindowViewModel()
        {
            BulkFile = new BulkFileWithSerialOverrunZone();
        }

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
            DialogResult = true;
        }

        public bool IsNewFileValid
        {
            get
            {
                return BulkFile != null && BulkFile.IsValid();
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new ActionCommand(p => Cancel());
            }
        }

        private void Cancel()
        {
            DialogResult = false;
        }
    }
}
