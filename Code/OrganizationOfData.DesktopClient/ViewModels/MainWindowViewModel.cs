namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Windows.Input;
    using OrganizationOfData.Windows;
    using OrganizationOfData.Data;
    using MvvmDialogs;
    using System.Windows;
    using System;
    using Microsoft.Win32;
    using MvvmDialogs.FrameworkDialogs.OpenFile;
    using MaterialDesignThemes.Wpf;
    using MvvmDialogs.FrameworkDialogs.SaveFile;

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
        
        public SnackbarMessageQueue Messages { get; set; }

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
            Messages = new SnackbarMessageQueue();
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
                Messages.Enqueue("Uspešno ste kreirali datoteku");
                FilePath = null;
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
            var viewModel = new AuthorsWindowViewModel();

            dialogService.ShowDialog(this, viewModel);
        }

        public ICommand FileSaveAsCommand
        {
            get
            {
                return new ActionCommand(p => FileSaveAs(), p => BulkFile != null);
            }
        }

        public void FileSaveAs()
        {
            var settings = new SaveFileDialogSettings()
            {
                Title = "Sačuvajte datoteku",
                Filter = "Rasuta datoteka sa serijskom zonom prekoračenja (*.bfo)|*.bfo",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                CheckFileExists = false
            };
            if (dialogService.ShowSaveFileDialog(this, settings) == true)
            {
                FilePath = settings.FileName;
                BusinessContext.WriteToBinaryFile(FilePath, BulkFile as BulkFileWithSerialOverrunZone);
                Messages.Enqueue("Uspešno ste sačuvali datoteku");
            }
        }

        public ICommand FileSaveCommand
        {
            get
            {
                return new ActionCommand(p => FileSave(), p => BulkFile != null);
            }
        }

        public void FileSave()
        {
            if (FilePath == null)
            {
                FileSaveAs();
            }
            else
            {
                BusinessContext.WriteToBinaryFile(FilePath, BulkFile as BulkFileWithSerialOverrunZone);
                Messages.Enqueue("Uspešno ste sačuvali datoteku");
            }
        }

        public ICommand OpenFileCommand
        {
            get
            {
                return new ActionCommand(p => OpenFile());
            }
        }

        private void OpenFile()
        {
            var settings = new OpenFileDialogSettings()
            {
                Title = "Izaberite fajl",
                Filter = "Rasuta datoteka sa serijskom zonom prekoračenja (*.bfo)|*.bfo",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (dialogService.ShowOpenFileDialog(this, settings) == true)
            {
                FilePath = settings.FileName;
                BulkFile = BusinessContext.ReadFromBinaryFile(FilePath);
                Messages.Enqueue("Uspešno ste učitali datoteku");
            }
        }
    }
}
