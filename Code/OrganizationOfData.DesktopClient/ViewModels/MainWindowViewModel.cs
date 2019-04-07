namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Windows.Input;
    using OrganizationOfData.Windows;
    using OrganizationOfData.Data;
    using MvvmDialogs;
    using System.Windows;
    using System;
    using System.Collections.Generic;

    public class MainWindowViewModel : ViewModel
    {
        private PrimaryZoneControlViewModel primaryZoneControlViewModel;
        private BucketControlViewModel bucketControlViewModel;
        private Visibility overrunZoneVisibility;
        private OverrunZoneControlViewModel overrunZoneControlViewModel;

        private string filePath;

        private IDialogService dialogService;

        private BulkFile bulkFile;

        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                NotifyPropertyChanged();
            }
        }

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

                PrimaryZoneControlViewModel.SetBuckets(BulkFile.PrimaryZone);
                switch(value)
                {
                    case BulkFileWithSerialOverrunZone bulkFileWithSerialOverrunZone:
                        OverrunZoneVisibility = Visibility.Visible;
                        OverrunZoneControlViewModel.SetBuckets(bulkFileWithSerialOverrunZone.OverrunZone);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                FilePath = null;
            }
        }

        public PrimaryZoneControlViewModel PrimaryZoneControlViewModel
        {
            get
            {
                return primaryZoneControlViewModel;
            }
            set
            {
                primaryZoneControlViewModel = value;
                NotifyPropertyChanged();
            }
        }

        public BucketControlViewModel BucketControlViewModel
        {
            get
            {
                return bucketControlViewModel;
            }
            set
            {
                bucketControlViewModel = value;
                NotifyPropertyChanged();
            }
        }
        
        public Visibility OverrunZoneVisibility
        {
            get
            {
                return overrunZoneVisibility;
            }
            set
            {
                overrunZoneVisibility = value;
                NotifyPropertyChanged(nameof(OverrunZoneVisibility));
            }
        }

        public OverrunZoneControlViewModel OverrunZoneControlViewModel
        {
            get
            {
                return overrunZoneControlViewModel;
            }
            set
            {
                overrunZoneControlViewModel = value;
                NotifyPropertyChanged(nameof(OverrunZoneControlViewModel));
            }
        }

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            PrimaryZoneControlViewModel = new PrimaryZoneControlViewModel();
            OverrunZoneControlViewModel = new OverrunZoneControlViewModel();
            BucketControlViewModel = new BucketControlViewModel();
            OverrunZoneVisibility = Visibility.Collapsed;
        }

        public ICommand OpenNewFileWindowCommand
        {
            get
            {
                return new ActionCommand(p => OpenNewFileWindow());
            }
        }

        private void OpenNewFileWindow()
        {
            var viewModel = new NewFileWindowViewModel();

            bool? result = dialogService.ShowDialog(this, viewModel);

            if (result.HasValue && result.Value)
            {
                BulkFile = viewModel.BulkFile;
            }
        }

        public ICommand OpenAuthorsWindowsCommand
        {
            get
            {
                return new ActionCommand(p => OpenAuthorsWindow());
            }
        }

        private void OpenAuthorsWindow()
        {
            var viewModel = new NewFileWindowViewModel();

            bool? result = dialogService.ShowDialog(this, viewModel);
        }
    }
}
