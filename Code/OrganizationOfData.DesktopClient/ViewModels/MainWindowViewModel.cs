namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Windows.Input;
    using OrganizationOfData.Windows;
    using OrganizationOfData.Data;
    using System.Windows;
    using System;
    using MaterialDesignThemes.Wpf;

    /// <summary>
    /// ViewModel containing all functionalities for MainWindow View
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        private IDialogService dialogService;
        private IFileDialogService fileDialogService;

        public SnackbarMessageQueue Messages { get; set; }

        #region PrimaryZoneMembers

        private PrimaryZoneControlViewModel primaryZoneControlViewModel;

        public PrimaryZoneControlViewModel PrimaryZoneControlViewModel
        {
            get
            {
                return primaryZoneControlViewModel;
            }
            set
            {
                primaryZoneControlViewModel = value;
                NotifyPropertyChanged(nameof(PrimaryZoneControlViewModel));
            }
        }

        #endregion

        #region OverrunZoneMembers

        private OverrunZoneControlViewModel overrunZoneControlViewModel;
        private Visibility overrunZoneVisibility;

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

        #endregion

        #region MemoryZoneMembers

        private BucketControlViewModel bucketControlViewModel;

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

        #endregion

        #region BulkFileMembers

        private BulkFile bulkFile;
        private string fileName;

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
                switch (value)
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

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
        
        /// <summary>
        /// Initializes a new instance of <see cref="MainWindowViewModel"/> class
        /// </summary>
        /// <param name="dialogService">Dialog service for MainWindow</param>
        public MainWindowViewModel(IDialogService dialogService, IFileDialogService fileDialogService)
        {
            this.dialogService = dialogService;
            this.fileDialogService = fileDialogService;

            PrimaryZoneControlViewModel = new PrimaryZoneControlViewModel();

            OverrunZoneControlViewModel = new OverrunZoneControlViewModel();
            OverrunZoneVisibility = Visibility.Collapsed;

            BucketControlViewModel = new BucketControlViewModel();

            Messages = new SnackbarMessageQueue();
        }

        #region NewFileMembers

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

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue && result.Value)
            {
                BulkFile = viewModel.BulkFile;
                Messages.Enqueue("Uspešno ste kreirali datoteku");
                FileName = null;
            }
        }

        #endregion

        #region AuthorMembers

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

            dialogService.ShowDialog(viewModel);
        }

        #endregion

        #region FileSaveMembers

        public ICommand FileSaveCommand
        {
            get
            {
                return new ActionCommand(p => FileSave(), p => BulkFile != null);
            }
        }

        public void FileSave()
        {
            if (FileName == null)
            {
                FileSaveAs();
            }
            else
            {
                BusinessContext.WriteToBinaryFile(FileName, BulkFile as BulkFileWithSerialOverrunZone);
                Messages.Enqueue("Uspešno ste sačuvali datoteku");
            }
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
            fileDialogService.Title = "Sačuvajte datoteku";
            fileDialogService.Filter = "Rasuta datoteka sa serijskom zonom prekoračenja (*.bfo)|*.bfo";
            fileDialogService.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fileDialogService.ShowSaveFileDialog() == true)
            {
                FileName = fileDialogService.FileName;
                BusinessContext.WriteToBinaryFile(FileName, BulkFile as BulkFileWithSerialOverrunZone);
                Messages.Enqueue("Uspešno ste sačuvali datoteku");
            }
        }

        #endregion

        #region OpenFileMembers

        public ICommand OpenFileCommand
        {
            get
            {
                return new ActionCommand(p => OpenFile());
            }
        }

        private void OpenFile()
        {
            fileDialogService.Title = "Izaberite fajl";
            fileDialogService.Filter = "Rasuta datoteka sa serijskom zonom prekoračenja (*.bfo)|*.bfo";
            fileDialogService.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fileDialogService.ShowOpenFileDialog() == true)
            {
                FileName = fileDialogService.FileName;
                BulkFile = BusinessContext.ReadFromBinaryFile(FileName);
                Messages.Enqueue("Uspešno ste učitali datoteku");
            }
        }

        #endregion
    }
}
