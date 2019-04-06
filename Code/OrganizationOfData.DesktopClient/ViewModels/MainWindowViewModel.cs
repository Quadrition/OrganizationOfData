namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using OrganizationOfData.Windows;
    using OrganizationOfData.Data;
    using MvvmDialogs;

    public class MainWindowViewModel : ViewModel
    {
        private PrimaryZoneControlViewModel primaryZoneControlViewModel;
        private BucketControlViewModel bucketControlViewModel;

        private IDialogService dialogService;

        private BulkFile bulkFile;

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

        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            ICollection<BucketControlViewModel> bucketControlViewModels = new ObservableCollection<BucketControlViewModel>();

            ICollection<RecordControlViewModel> collection = new ObservableCollection<RecordControlViewModel>();
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );

            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            bucketControlViewModels.Add(new BucketControlViewModel
            {
                RecordControlViewModels = collection
            });

            collection = new ObservableCollection<RecordControlViewModel>();
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            collection.Add(new RecordControlViewModel
            {
                Record = new Record
                {
                    Person = new Person()
                    {
                        Id = 2,
                        FullName = "Marko Mackic",
                        Adress = "Milosa Crjn",
                        Age = 32
                    },
                    Status = Status.active
                }

            }
            );
            bucketControlViewModels.Add(new BucketControlViewModel
            {
                RecordControlViewModels = collection
            });

            PrimaryZoneControlViewModel = new PrimaryZoneControlViewModel()
            {
                BucketControlViewModels = bucketControlViewModels
            };
            BucketControlViewModel = new BucketControlViewModel
            {
                RecordControlViewModels = collection
            };
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

            if (result.HasValue)
            {
                if (result.Value)
                {
                    this.bulkFile = viewModel.bulkFile;
                }
                else
                {
                    this.bulkFile = null;
                }
            }
            else
            {
                this.bulkFile = null;
            }
        }
    }
}
