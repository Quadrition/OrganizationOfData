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

        private IDialogService dialogService;

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
    }
}
