namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Windows.Input;
    using OrganizationOfData.Windows;
    using OrganizationOfData.Data;
    using System.Windows;
    using System;
    using MaterialDesignThemes.Wpf;
    using System.Collections.ObjectModel;

    /// <summary>
    /// ViewModel containing all functionalities for MainWindow View
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        private IDialogService dialogService;
        private IFileDialogService fileDialogService;

        public SnackbarMessageQueue Messages { get; set; }

        #region PrimaryZone Members

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

        private BucketControlViewModel memoryBucketControlViewModel;

        public BucketControlViewModel MemoryBucketControlViewModel
        {
            get
            {
                return memoryBucketControlViewModel;
            }
            set
            {
                memoryBucketControlViewModel = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region BulkFile Members

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

            MemoryBucketControlViewModel = null;

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

        #region ManipulateRecords Members

        #region NewRecord Members

        private NewRecordSimulation newRecordSimulation;

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
                newRecordSimulation = new NewRecordSimulation(bulkFile, viewModel.Record);

                Messages.Enqueue("Uspešno ste pokrenuli simulaciju unosa novog sloga");
            }
        }

        #endregion

        #region FindRecord Members

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
                return newRecordSimulation == null && bulkFile != null;
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
                return new ActionCommand(p => NextStep(), p => !CanManipulateOverRecord);
            }
        }

        private void NextStep()
        {
            if (newRecordSimulation.Row != -1 && newRecordSimulation.Column != -1)
            {
                (PrimaryZoneControlViewModel.BucketControlViewModels[newRecordSimulation.Row].RecordControlViewModels as ObservableCollection<RecordControlViewModel>)[newRecordSimulation.Column].UnSelect();
            }
            newRecordSimulation.NextStep();
            NextStepMessage = newRecordSimulation.Message;

            if (newRecordSimulation.Row != -1 && newRecordSimulation.Column != -1)
            {
                (PrimaryZoneControlViewModel.BucketControlViewModels[newRecordSimulation.Row].RecordControlViewModels as ObservableCollection<RecordControlViewModel>)[newRecordSimulation.Column].Select();
            }
        }

        #endregion

        #endregion
    }
}
