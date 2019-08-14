namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Windows.Input;
    using OrganizationOfData.Windows;
    using OrganizationOfData.Data;
    using System.Windows;
    using System;
    using MaterialDesignThemes.Wpf;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;

    /// <summary>
    /// ViewModel containing all functionalities for MainWindow View
    /// </summary>
    public class MainWindowViewModel : ViewModel
    {
        private IDialogService dialogService;
        private IFileDialogService fileDialogService;

        /// <summary>
        /// Gets or sets messages that needs to be displayed in the snackbar
        /// </summary>
        public SnackbarMessageQueue Messages { get; set; }

        #region Disk Members

        private Visibility diskVisibility;

        /// <summary>
        /// Gets or sets visibility for the disk group
        /// </summary>
        public Visibility DiskVisibility
        {
            get
            {
                return diskVisibility;
            }
            set
            {
                diskVisibility = value;

                NotifyPropertyChanged(nameof(DiskVisibility));
            }
        }

        private ZoneControlViewModel primaryZoneControlViewModel;

        /// <summary>
        /// Gets or sets the view model for the primary zone control
        /// </summary>
        public ZoneControlViewModel PrimaryZoneControlViewModel
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

        /// <summary>
        /// Gets or sets a list of records in overrun zone
        /// </summary>
        public ICollection<RecordControlViewModel> OverrunZoneRecordControls { get; set; }

        private Visibility overrunZoneVisibility;

        /// <summary>
        /// Gets or sets visibility for the disk group
        /// </summary>
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

        private Visibility memoryVisibility;
        
        /// <summary>
        /// Gets or sets visibility for memory controls
        /// </summary>
        public Visibility MemoryVisibility
        {
            get
            {
                return memoryVisibility;
            }
            set
            {
                memoryVisibility = value;
                NotifyPropertyChanged(nameof(MemoryVisibility));
            }
        }

        private BucketControlViewModel memoryBucketControlViewModel;

        /// <summary>
        /// Gets or sets the view model for the primary zone control
        /// </summary>
        public BucketControlViewModel MemoryBucketControlViewModel
        {
            get
            {
                return memoryBucketControlViewModel;
            }
            set
            {
                memoryBucketControlViewModel = value;

                NotifyPropertyChanged(nameof(MemoryBucketControlViewModel));
            }
        }

        private Visibility memoryBucketVisibility;

        public Visibility MemoryBucketVisibility
        {
            get
            {
                return memoryBucketVisibility;
            }
            set
            {
                memoryBucketVisibility = value;
                NotifyPropertyChanged(nameof(MemoryBucketVisibility));
            }
        }

        /// <summary>
        /// Gets or sets a list of records in memory
        /// </summary>
        public ICollection<RecordControlViewModel> MemoryRecordControls { get; set; }

        #endregion

        #region BulkFile Members

        private BulkFile bulkFile;

        /// <summary>
        /// Gets or sets a bulk file in main memory
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

                OverrunZoneRecordControls.Clear();
                if (value == null)
                {
                    DiskVisibility = Visibility.Collapsed;
                    OverrunZoneVisibility = Visibility.Collapsed;
                    PrimaryZoneControlViewModel = null;
                }
                else
                {
                    DiskVisibility = Visibility.Visible;

                    PrimaryZoneControlViewModel = new ZoneControlViewModel(bulkFile.PrimaryZone, true);
                    
                    foreach (Record record in bulkFile.OverrunZone)
                    {
                        OverrunZoneRecordControls.Add(new RecordControlViewModel()
                        {
                            Record = record,
                            Visibility = record.Status == Status.empty ? Visibility.Collapsed : Visibility.Visible
                        });
                    }

                    if (bulkFile.OverrunZone[0].Status == Status.empty)
                    {
                        OverrunZoneVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        OverrunZoneVisibility = Visibility.Visible;
                    }
                }
            }
        }

        private string fileName;

        /// <summary>
        /// Gets or sets a file path to the bulk file
        /// </summary>
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
          
            DiskVisibility = Visibility.Collapsed;
            OverrunZoneVisibility = Visibility.Collapsed;
            MemoryVisibility = Visibility.Collapsed;
            MemoryBucketVisibility = Visibility.Collapsed;

            Messages = new SnackbarMessageQueue();

            OverrunZoneRecordControls = new ObservableCollection<RecordControlViewModel>();
            MemoryRecordControls = new ObservableCollection<RecordControlViewModel>();
        }

        #region NewFile Members

        /// <summary>
        /// Gets or sets an icommand for opening a new file
        /// </summary>
        public ICommand OpenNewFileWindowCommand
        {
            get
            {
                return new ActionCommand(p => OpenNewFileWindow(), p => !IsManipulatingOverFile);
            }
        }

        /// <summary>
        /// Opens a window for inputing a new file
        /// </summary>
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

        /// <summary>
        /// Gets an icommand for opening the authors window
        /// </summary>
        public ICommand OpenAuthorsWindowsCommand
        {
            get
            {
                return new ActionCommand(p => OpenAuthorsWindow());
            }
        }

        private void OpenAuthorsWindow()
        {
            var viewModel = new AuthorsDialogViewModel();

            dialogService.ShowDialog(viewModel);
        }

        #endregion

        #region Documentation Members

        /// <summary>
        /// Gets an icommand for opening the authors window
        /// </summary>
        public ICommand OpenHelpWindowsCommand
        {
            get
            {
                return new ActionCommand(p => OpenHelpWindow());
            }
        }

        private void OpenHelpWindow()
        {
            var viewModel = new HelpWindowViewModel();

            dialogService.ShowDialog(viewModel);
        }

        #endregion

        #region OpenFile Members

        /// <summary>
        /// Gets or sets an icommand for opening file
        /// </summary>
        public ICommand OpenFileCommand
        {
            get
            {
                return new ActionCommand(p => OpenFile(), p => !IsManipulatingOverFile);
            }
        }

        /// <summary>
        /// Opens an existing bulk file from the pc
        /// </summary>
        private void OpenFile()
        {
            fileDialogService.Title = "Izaberite fajl";
            fileDialogService.Filter = "Rasuta datoteka (*.bf)|*.bf";
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

        /// <summary>
        /// Gets an icommand for saving bulk file
        /// </summary>
        public ICommand FileSaveCommand
        {
            get
            {
                return new ActionCommand(p => FileSave(), p => BulkFile != null && !IsManipulatingOverFile);
            }
        }

        /// <summary>
        /// Saves a bulk file in specified file path
        /// </summary>
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

        /// <summary>
        /// Gets an icommand for saving bulk file in a new path
        /// </summary>
        public ICommand FileSaveAsCommand
        {
            get
            {
                return new ActionCommand(p => FileSaveAs(), p => BulkFile != null && !IsManipulatingOverFile);
            }
        }

        /// <summary>
        /// Saves a bulk file in a new file path
        /// </summary>
        public void FileSaveAs()
        {
            fileDialogService.Title = "Sačuvajte datoteku";
            fileDialogService.Filter = "Rasuta datoteka (*.bf)|*.bf";
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
                return new ActionCommand(p => CloseFile(), p => IsFileOpened && !IsManipulatingOverFile);
            }
        }

        private void CloseFile()
        {
            BulkFile = null;
            DiskVisibility = Visibility.Collapsed;
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

        /// <summary>
        /// Gets or sets a simulation for inserting a new record
        /// </summary>
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

        /// <summary>
        /// Gets an icommand for showing a window for a new record
        /// </summary>
        public ICommand ShowNewRecordDialogCommand
        {
            get
            {
                return new ActionCommand(p => ShowNewRecordDialog(), p => !IsManipulatingOverFile && BulkFile != null);
            }
        }

        /// <summary>
        /// Opens a window for a new record
        /// </summary>
        private void ShowNewRecordDialog()
        {
            var viewModel = new NewRecordDialogViewModel();

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue && result.Value)
            {
                NewRecordSimulation = new NewRecordSimulation(BulkFile, viewModel.NewPerson);
                NextStepMessage = "Kliknite sledeći korak radi početka simulacije";

                Messages.Enqueue("Uspešno ste pokrenuli simulaciju unosa novog sloga");
            }
        }

        #endregion

        #region EditRecord Members

        private EditRecordSimulation editRecordSimulation;

        /// <summary>
        /// Gets or sets a simulation for editing a record
        /// </summary>
        public EditRecordSimulation EditRecordSimulation
        {
            get
            {
                return editRecordSimulation;
            }
            set
            {
                editRecordSimulation = value;
                NotifyPropertyChanged(nameof(EditRecordSimulation));
            }
        }

        /// <summary>
        /// Gets an icommand for showing a dialog for edition a record
        /// </summary>
        public ICommand ShowEditRecordRecordDialogCommand
        {
            get
            {
                return new ActionCommand(p => ShowEditRecordRecordDialog(), p => !IsManipulatingOverFile && BulkFile != null);
            }
        }

        /// <summary>
        /// Shows a dialog for editing a record
        /// </summary>
        private void ShowEditRecordRecordDialog()
        {
            var viewModel = new EditRecordDialogViewModel();

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue && result.Value)
            {
                EditRecordSimulation = new EditRecordSimulation(BulkFile, viewModel.Id, viewModel.NewFullName, viewModel.NewAdress, viewModel.NewAge);
                NextStepMessage = "Kliknite sledeći korak radi početka simulacije";

                Messages.Enqueue("Uspešno ste pokrenuli simulaciju izmene sloga");
            }
        }

        #endregion

        #region DeleteRecord Members

        private DeleteRecordSimulation deleteRecordSimulation;

        /// <summary>
        /// Gets or sets a simulation for deleting a record
        /// </summary>
        public DeleteRecordSimulation DeleteRecordSimulation
        {
            get
            {
                return deleteRecordSimulation;
            }
            set
            {
                deleteRecordSimulation = value;
                NotifyPropertyChanged(nameof(DeleteRecordSimulation));
            }
        }

        /// <summary>
        /// Gets an icommand for showing a dialog for deleting a record
        /// </summary>
        public ICommand ShowDeleteRecordDialogCommand
        {
            get
            {
                return new ActionCommand(p => ShowDeleteRecordDialog(), p => !IsManipulatingOverFile && BulkFile != null);
            }
        }

        /// <summary>
        /// Shows a dialog for deleting a record
        /// </summary>
        private void ShowDeleteRecordDialog()
        {
            var viewModel = new DeleteRecordDialogViewModel();

            bool? result = dialogService.ShowDialog(viewModel);

            if (result.HasValue && result.Value)
            {
                DeleteRecordSimulation = new DeleteRecordSimulation(BulkFile, viewModel.Id, viewModel.Logical);

                Messages.Enqueue("Uspešno ste pokrenuli simulaciju brisanja sloga");
            }
        }

        #endregion

        public bool IsManipulatingOverFile
        {
            get
            {
                return NewRecordSimulation != null || EditRecordSimulation != null || DeleteRecordSimulation != null;
            }
        }

        #region NextStep Members

        private string nextStepMessage;

        /// <summary>
        /// Gets or sets a message for next step that will be displayed
        /// </summary>
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

        /// <summary>
        /// Gets an icommand for the next step
        /// </summary>
        public ICommand NextStepCommand
        {
            get
            {
                return new ActionCommand(p => NextStep(), p => IsManipulatingOverFile);
            }
        }

        /// <summary>
        /// Performs a next step in active simulation
        /// </summary>
        private void NextStep()
        {
            if (NewRecordSimulation != null)
            {
                bool result = NewRecordSimulation.NextStep();
                NextStepMessage = NewRecordSimulation.Message;

                MemoryRecordControls.Clear();
                MemoryVisibility = Visibility.Visible;
                MemoryBucketControlViewModel = null;
                MemoryBucketVisibility = Visibility.Visible;
                if (NewRecordSimulation.KeyTransformation != -1 && !NewRecordSimulation.OverrunZone)
                {
                    MemoryBucketControlViewModel = new BucketControlViewModel(BulkFile.PrimaryZone[NewRecordSimulation.KeyTransformation]);

                    if (NewRecordSimulation.Column != -1)
                    {
                        MemoryBucketControlViewModel.RecordControlViewModels.ElementAt(NewRecordSimulation.Column).Select();
                    }
                }
                else if (NewRecordSimulation.Column != -1)
                {
                    MemoryRecordControls.Add(new RecordControlViewModel()
                    {
                        Record = BulkFile.OverrunZone[NewRecordSimulation.Column]
                    });

                    MemoryBucketVisibility = Visibility.Collapsed;
                }
                else
                {
                    MemoryVisibility = Visibility.Collapsed;
                }

                if (NewRecordSimulation.Changed)
                {
                    if (!NewRecordSimulation.OverrunZone)
                    {
                        PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(NewRecordSimulation.KeyTransformation).RecordControlViewModels.ElementAt(NewRecordSimulation.Column).ResetColor();
                    }
                    else
                    {
                        PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(NewRecordSimulation.KeyTransformation).OverrunedRecords++;
                        OverrunZoneRecordControls.ElementAt(NewRecordSimulation.Column).ResetColor();
                        OverrunZoneRecordControls.ElementAt(NewRecordSimulation.Column).Visibility = Visibility.Visible;
                        OverrunZoneVisibility = Visibility.Visible;
                    }
                }

                if (result == false)
                {
                    NewRecordSimulation = null;
                    MemoryRecordControls.Clear();
                    MemoryVisibility = Visibility.Collapsed;
                }
            }
            else if (EditRecordSimulation != null)
            {
                bool result = EditRecordSimulation.NextStep();
                NextStepMessage = EditRecordSimulation.Message;

                MemoryRecordControls.Clear();
                MemoryVisibility = Visibility.Visible;
                MemoryBucketControlViewModel = null;
                MemoryBucketVisibility = Visibility.Visible;
                if (EditRecordSimulation.Row != -1 && !EditRecordSimulation.OverrunZone)
                {
                    MemoryBucketControlViewModel = new BucketControlViewModel(BulkFile.PrimaryZone[EditRecordSimulation.Row]);

                    if (EditRecordSimulation.Column != -1)
                    {
                        MemoryBucketControlViewModel.RecordControlViewModels.ElementAt(EditRecordSimulation.Column).Select();
                    }
                }
                else if (EditRecordSimulation.Column != -1)
                {
                    MemoryRecordControls.Add(new RecordControlViewModel()
                    {
                        Record = BulkFile.OverrunZone[EditRecordSimulation.Column]
                    });

                    MemoryBucketVisibility = Visibility.Collapsed;
                }
                else
                {
                    MemoryVisibility = Visibility.Collapsed;
                }

                if (result == false)
                {
                    EditRecordSimulation = null;
                    MemoryRecordControls.Clear();
                    MemoryVisibility = Visibility.Collapsed;
                }
            }
            else if (DeleteRecordSimulation != null)
            {
                bool oldDeletedInPrimary = DeleteRecordSimulation.DeletedInPrimary;
                bool oldDeleted = DeleteRecordSimulation.Deleted;
                bool result = DeleteRecordSimulation.NextStep();
                NextStepMessage = DeleteRecordSimulation.Message;
                
                MemoryVisibility = Visibility.Visible;
                MemoryBucketVisibility = Visibility.Visible;
                MemoryRecordControls.Clear();

                if (DeleteRecordSimulation.KeyTransformation != -1)
                {
                    if (!DeleteRecordSimulation.OverrunZone)
                    {
                        MemoryBucketControlViewModel = new BucketControlViewModel(PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(DeleteRecordSimulation.KeyTransformation));
                    }
                    else
                    {
                        if (DeleteRecordSimulation.DeletedInPrimary || oldDeletedInPrimary != DeleteRecordSimulation.DeletedInPrimary)
                        {
                            MemoryBucketControlViewModel = new BucketControlViewModel(PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(DeleteRecordSimulation.KeyTransformation));
                            if (DeleteRecordSimulation.Column != -1)
                            {
                                MemoryRecordControls.Add(new RecordControlViewModel()
                                {
                                    Record = OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column).Record,
                                });
                            }
                        }
                        else if (DeleteRecordSimulation.DeletedInOverrun)
                        {
                            if (DeleteRecordSimulation.Column != -1 && DeleteRecordSimulation.Column != BulkFile.NumberOfRecordsInOverrunZone)
                            {
                                MemoryRecordControls.Add(new RecordControlViewModel()
                                {
                                    Record = OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column).Record,
                                });

                                if (!oldDeletedInPrimary && oldDeleted == DeleteRecordSimulation.Deleted)
                                {
                                    MemoryRecordControls.Add(new RecordControlViewModel()
                                    {
                                        Record = OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column - 1).Record,
                                    });
                                }

                                MemoryBucketVisibility = Visibility.Collapsed;
                                MemoryBucketControlViewModel = null;
                            }
                        }
                        else
                        {
                            if (DeleteRecordSimulation.Column != -1)
                            {
                                MemoryRecordControls.Add(new RecordControlViewModel()
                                {
                                    Record = OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column).Record,
                                });
                            }
                            MemoryBucketVisibility = Visibility.Collapsed;
                            MemoryBucketControlViewModel = null;
                        }
                    }

                    if (MemoryBucketControlViewModel != null)
                    {
                        if (!DeleteRecordSimulation.OverrunZone)
                        {
                            if (!DeleteRecordSimulation.Deleted || oldDeleted != DeleteRecordSimulation.Deleted)
                            {
                                if (DeleteRecordSimulation.Column != -1)
                                    MemoryBucketControlViewModel.RecordControlViewModels.ElementAt(DeleteRecordSimulation.Column).Select();
                            }
                            else if (oldDeleted == DeleteRecordSimulation.Deleted)
                            {
                                MemoryBucketControlViewModel.RecordControlViewModels.ElementAt(DeleteRecordSimulation.Column - 1).Select();
                                MemoryBucketControlViewModel.RecordControlViewModels.ElementAt(DeleteRecordSimulation.Column).Select();
                            }
                        }
                        else
                        {
                            MemoryBucketControlViewModel.RecordControlViewModels.ElementAt(BulkFile.Factor - 1).Select();
                        }
                    }
                    else if (DeleteRecordSimulation.DeletedInPrimary)
                    {
                        MemoryBucketControlViewModel.RecordControlViewModels.ElementAt(BulkFile.Factor - 1).Select();
                    }

                    foreach (RecordControlViewModel record in MemoryRecordControls)
                    {
                        record.Select();
                    }
                }
                
                if (DeleteRecordSimulation.Changed)
                {
                    //MemoryZoneControlViewModel.BucketControlViewModels.First().RecordControlViewModels.ElementAt(DeleteRecordSimulation.Column).ResetColor();
                    if (!DeleteRecordSimulation.OverrunZone)
                    {
                        if (DeleteRecordSimulation.Logical)
                        {
                            PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(DeleteRecordSimulation.KeyTransformation).RecordControlViewModels.ElementAt(DeleteRecordSimulation.Column).ResetColor();
                        }
                        else
                        {
                            if (DeleteRecordSimulation.Column != 0)
                                PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(DeleteRecordSimulation.KeyTransformation).RecordControlViewModels.ElementAt(DeleteRecordSimulation.Column - 1).ResetColor();

                            PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(DeleteRecordSimulation.KeyTransformation).RecordControlViewModels.ElementAt(DeleteRecordSimulation.Column).ResetColor();
                        }
                    }
                    else
                    {
                        if (DeleteRecordSimulation.Logical)
                        {
                            OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column).ResetColor();
                        }
                        else
                        {
                            if (MemoryBucketControlViewModel != null)
                            {
                                PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(DeleteRecordSimulation.KeyTransformation).RecordControlViewModels.ElementAt(BulkFile.Factor - 1).ResetColor();
                            }
                            else
                            {
                                if (DeleteRecordSimulation.Column == BulkFile.NumberOfRecordsInOverrunZone && DeleteRecordSimulation.Deleted)
                                {
                                    OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column - 1).ResetColor();
                                    OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column - 1).Visibility = Visibility.Hidden;

                                    if (OverrunZoneRecordControls.Count == 0)
                                    {
                                        OverrunZoneVisibility = Visibility.Hidden;
                                    }
                                }
                                else
                                {
                                    if (DeleteRecordSimulation.Column == BulkFile.NumberOfRecordsInOverrunZone)
                                    {
                                        OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column).ResetColor();
                                    }

                                    OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column - 1).ResetColor();
                                    MemoryBucketVisibility = Visibility.Collapsed;

                                    if (OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column - 1).Record.Status == Status.empty)
                                    {
                                        OverrunZoneRecordControls.ElementAt(DeleteRecordSimulation.Column - 1).Visibility = Visibility.Collapsed;
                                        MemoryVisibility = Visibility.Collapsed;
                                    }
                                }
                            }
                        }
                    }
                }

                if (MemoryBucketControlViewModel == null)
                    MemoryBucketVisibility = Visibility.Collapsed;

                if (result == false)
                {
                    if (!result)
                        DeleteRecordSimulation = null;

                    MemoryBucketControlViewModel = null;
                    MemoryBucketVisibility = Visibility.Collapsed;
                    MemoryVisibility = Visibility.Collapsed;
                    MemoryRecordControls.Clear();

                    for (int i = 0; i < BulkFile.NumberOfBuckets; i++)
                    {
                        PrimaryZoneControlViewModel.BucketControlViewModels.ElementAt(i).OverrunedRecords = BulkFile.PrimaryZone[i].OverrunedRecords;
                    }
                }
            }
            else
            {
                throw new Exception();
            }

            #endregion

            #endregion

        }
    }
}
