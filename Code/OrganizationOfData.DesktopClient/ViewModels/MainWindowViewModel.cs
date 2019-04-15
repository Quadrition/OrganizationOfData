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

        #region PrimaryZone Members

        #region PrimaryOverrunZone Members

        private PrimaryZoneControlViewModel primaryZoneControlViewModel;
        private Visibility primaryZoneVisibility;

        public Visibility PrimaryZoneVisibility
        {
            get
            {
                return primaryZoneVisibility;
            }
            set
            {
                primaryZoneVisibility = value;
                NotifyPropertyChanged(nameof(PrimaryZoneVisibility));
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
                NotifyPropertyChanged(nameof(PrimaryZoneControlViewModel));

                if (primaryZoneControlViewModel == null)
                {
                    PrimaryZoneVisibility = Visibility.Collapsed;
                }
                else
                {
                    PrimaryZoneVisibility = Visibility.Visible;
                }
            }
        }

        #endregion

        #region PrimaryPrimaryZone Members

        #endregion

        #endregion

        #region OverrunZone Members

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

                if (overrunZoneControlViewModel == null)
                {
                    OverrunZoneVisibility = Visibility.Collapsed;
                }
                else
                {
                    OverrunZoneVisibility = Visibility.Visible;
                }
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

        #region MemoryZone Members

        private BucketMemoryControlViewModel bucketMemoryControlViewModel;
        private Visibility bucketMemoryControlVisibility;

        public Visibility BucketMemoryControlVisibility
        {
            get
            {
                return bucketMemoryControlVisibility;
            }
            set
            {
                bucketMemoryControlVisibility = value;
                NotifyPropertyChanged(nameof(BucketMemoryControlVisibility));
            }
        }

        public BucketMemoryControlViewModel BucketMemoryControlViewModel
        {
            get
            {
                return bucketMemoryControlViewModel;
            }
            set
            {
                bucketMemoryControlViewModel = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region BulkFile Members

        private BulkFile bulkFile;
        private BulkFileType? bulkFileType;
        private string fileName;

        public BulkFileType? BulkFileType
        {
            get
            {
                return bulkFileType;
            }
            set
            {
                bulkFileType = value;
                NotifyPropertyChanged(nameof(BulkFileType));
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

                if (value == null)
                {
                    BulkFileType = null;
                    PrimaryZoneControlViewModel = null;
                    OverrunZoneControlViewModel = null;
                }

                switch (value)
                {
                    case BulkFileWithSerialOverrunZone bulkFileWithSerialOverrunZone:
                        PrimaryZoneControlViewModel = new PrimaryZoneControlViewModel(bulkFileWithSerialOverrunZone.PrimaryZone);
                        OverrunZoneControlViewModel = new OverrunZoneControlViewModel(bulkFileWithSerialOverrunZone.OverrunZone);
                        BulkFileType = Windows.BulkFileType.withSerialOverrunZone;
                        break;
                    case BulkFilePrimaryOverrunZone bulkFilePrimaryOverrunZone:
                        BulkFileType = Windows.BulkFileType.withSerialOverrunPrimaryZone;
                        break;
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

            PrimaryZoneControlViewModel = null;
            OverrunZoneControlViewModel = null;

            BucketMemoryControlViewModel = null;

            Messages = new SnackbarMessageQueue();
        }

        #region NewFile Members

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

        #region Author Members

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

        #region OpenFile Members

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
            fileDialogService.Filter = "Rasuta datoteka (*.bfo;*.bfp)|*.bfo;*.bfp|Rasuta datoteka sa serijskom zonom prekoračenja (*.bfo)|*.bfo|Rasuta datoteka sa zonom prekoračenja u primarnoj zoni (*.bfp)|*.bfp";
            fileDialogService.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fileDialogService.ShowOpenFileDialog() == true)
            {
                FileName = fileDialogService.FileName;
                BulkFile = BusinessContext.ReadFromBinaryFile(FileName);
                Messages.Enqueue("Uspešno ste učitali datoteku");
            }
        }

        #endregion

        #region FileSave Members

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
                BusinessContext.WriteToBinaryFile(FileName, BulkFile);
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

            switch(BulkFile)
            {
                case BulkFilePrimaryOverrunZone bulkFilePrimaryOverrunZone:
                    fileDialogService.Filter = "Rasuta datoteka sa zonom prekoračenja u primarnoj zoni (*.bfp)|*.bfp";
                    break;
                case BulkFileWithSerialOverrunZone bulkFileWithSerialOverrunZone:
                    fileDialogService.Filter = "Rasuta datoteka sa serijskom zonom prekoračenja (*.bfo)|*.bfo";
                    break;
            }
            fileDialogService.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fileDialogService.ShowSaveFileDialog() == true)
            {
                FileName = fileDialogService.FileName;
                BusinessContext.WriteToBinaryFile(FileName, BulkFile);
                Messages.Enqueue("Uspešno ste sačuvali datoteku");
            }
        }

        #endregion

        #region CloseFile Members

        public ICommand CloseFileCommand
        {
            get
            {
                return new ActionCommand(p => CloseFile(), p => IsFileOpened);
            }
        }

        private void CloseFile()
        {
            BulkFile = null;
        }

        private bool IsFileOpened
        {
            get
            {
                return BulkFile != null;
            }
        }

        #endregion

        #region ManipulateRecords Members

        #region NewRecord Members

        private NewRecordSimulation newRecordSimulation;

        public NewRecordSimulation NewRecordSimulation
        {
            get
            {
                return newRecordSimulation;
            }
            set
            {
                newRecordSimulation = value;
                NotifyPropertyChanged(nameof(NewRecordSimulation));
            }
        }

        public ICommand ShowNewRecordDialogCommand
        {
            get
            {
                return new ActionCommand(p => ShowNewRecordDialog(), p => CanManipulateOverRecord);
            }
        }

        private void ShowNewRecordDialog()
        {
            var viewModel = new NewRecordDialogViewModel();

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue && result.Value)
            {
                NewRecordSimulation = new NewRecordSimulation(bulkFile as BulkFileWithSerialOverrunZone, viewModel.Record);

                Messages.Enqueue("Uspešno ste pokrenuli simulaciju unosa novog sloga");
            }
        }

        #endregion

        #region FindRecord Members

        private FindRecordSimulation findRecordSimulation;

        public FindRecordSimulation FindRecordSimulation
        {
            get
            {
                return findRecordSimulation;
            }
            set
            {
                findRecordSimulation = value;
                NotifyPropertyChanged(nameof(FindRecordSimulation));
            }
        }

        public ICommand ShowFindRecordRecordDialogCommand
        {
            get
            {
                return new ActionCommand(p => ShowFindRecordRecordDialog(), p => CanManipulateOverRecord);
            }
        }

        private void ShowFindRecordRecordDialog()
        {
            var viewModel = new FindRecordDialogViewModel();

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue && result.Value)
            {
                Messages.Enqueue("Uspešno ste pokrenuli simulaciju traženja sloga");
            }
        }

        #endregion

        #region DeleteRecord Members

        public ICommand ShowDeleteRecordRecordDialogCommand
        {
            get
            {
                return new ActionCommand(p => ShowDeleteRecordRecordDialog(), p => CanManipulateOverRecord);
            }
        }

        private void ShowDeleteRecordRecordDialog()
        {
            var viewModel = new DeleteRecordDialogViewModel();

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue && result.Value)
            {
                Messages.Enqueue("Uspešno ste pokrenuli simulaciju brisanja sloga");
            }
        }

        #endregion

        public bool CanManipulateOverRecord
        {
            get
            {
                return BulkFile != null && NewRecordSimulation == null;
            }
        }

        #region NextStep Members

        private string nextStepMessage;

        public string NextStepMessage
        {
            get
            {
                return nextStepMessage;
            }
            set
            {
                nextStepMessage = value;
                NotifyPropertyChanged(nameof(NextStepMessage));
            }
        }

        public ICommand NextStepCommand
        {
            get
            {
                return new ActionCommand(p => NextStep(), p => CanManipulateOverRecord);
            }
        }

        private void NextStep()
        {
            if (NewRecordSimulation != null)
            {

            }
            else if (FindRecordSimulation != null)
            {

            }
            bool result = NewRecordSimulation.NextStep();
            NextStepMessage = NewRecordSimulation.Message;

            if (NewRecordSimulation.Row != -1)
            {
                if (NewRecordSimulation.OverrunZone == false)
                {
                    BucketMemoryControlViewModel = new BucketMemoryControlViewModel
                    {
                        BucketControlViewModel = new BucketControlViewModel(PrimaryZoneControlViewModel.BucketControlViewModels[NewRecordSimulation.Row])
                    };
                }
                else
                {
                    BucketMemoryControlViewModel = new BucketMemoryControlViewModel
                    {
                        BucketControlViewModel = new BucketControlViewModel(OverrunZoneControlViewModel.BucketControlViewModels[NewRecordSimulation.Row])
                    };
                }
            }
            else
            {
                BucketMemoryControlViewModel = null;
            }

            if (NewRecordSimulation.Column != -1)
            {
                BucketMemoryControlViewModel.BucketControlViewModel.RecordControlViewModels[NewRecordSimulation.Column].Select();
            }

            if (result == false)
            {
                if (NewRecordSimulation.Row != -1 && NewRecordSimulation.Column != -1)
                {
                    BucketMemoryControlViewModel.BucketControlViewModel.RecordControlViewModels[NewRecordSimulation.Column].Record = NewRecordSimulation.NewRecord;
                    BucketMemoryControlViewModel.BucketControlViewModel.RecordControlViewModels[NewRecordSimulation.Column].Select();
                }
                if (NewRecordSimulation.OverrunZone == false)
                {
                    PrimaryZoneControlViewModel.BucketControlViewModels[NewRecordSimulation.Row].RecordControlViewModels[NewRecordSimulation.Column].Record.Person = NewRecordSimulation.NewRecord.Person;
                    PrimaryZoneControlViewModel.BucketControlViewModels[NewRecordSimulation.Row].RecordControlViewModels[NewRecordSimulation.Column].Record.Status = Status.active;
                }
                else
                {
                    OverrunZoneControlViewModel.BucketControlViewModels[NewRecordSimulation.Row].RecordControlViewModels[NewRecordSimulation.Column].Record.Person = NewRecordSimulation.NewRecord.Person;
                    OverrunZoneControlViewModel.BucketControlViewModels[NewRecordSimulation.Row].RecordControlViewModels[NewRecordSimulation.Column].Record.Status = Status.active;
                }

                NewRecordSimulation = null;
            }
        }

        #endregion

        #endregion
    }
}
