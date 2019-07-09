namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;


    public class EditRecordDialogViewModel : ViewModel, IDialogRequestClose
    {
        #region EditedValues Members

        private int id;

        /// <summary>
        /// Gets or sets an id of record that needs to be edited
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        private string newFullName;

        /// <summary>
        /// Gets or sets a new value of full name
        /// </summary>
        public string NewFullName
        {
            get
            {
                return newFullName;
            }
            set
            {
                newFullName = value;
                NotifyPropertyChanged(nameof(NewFullName));
            }
        }

        private string newAdress;

        /// <summary>
        /// Gets or sets a new value of adress
        /// </summary>
        public string NewAdress
        {
            get
            {
                return newAdress;
            }
            set
            {
                newAdress = value;
                NotifyPropertyChanged(nameof(NewAdress));
            }
        }

        private int? newAge;

        /// <summary>
        /// Gets or sets a new value of age
        /// </summary>
        public int? NewAge
        {
            get
            {
                return newAge;
            }
            set
            {
                newAge = value;
                NotifyPropertyChanged(nameof(NewAge));
            }
        }

        #endregion

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        /// <summary>
        /// Initializes a new instance of <see cref="EditRecordDialogViewModel"/> class
        /// </summary>
        public EditRecordDialogViewModel()
        {

        }

        #region EditPerson

        /// <summary>
        /// Gets an icommand for editiong person
        /// </summary>
        public ICommand EditPersonCommand
        {
            get
            {
                return new ActionCommand(p => EditPerson(), p => CanEditPerson);
            }
        }

        /// <summary>
        /// Gets a boolean value which represents if the person with new values is valid
        /// </summary>
        public bool CanEditPerson
        {
            get
            {
                if (Id < 1 || NewFullName.Length > 32 || NewAdress.Length > 32 || (NewAge.HasValue && (NewAge < 0 || NewAge > 100)))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Closes the dialog and returns a result which confirms a edition of the record
        /// </summary>
        public void EditPerson()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        #endregion

        #region CancelCommand Members

        /// <summary>
        /// Gets an icommand for canceling an edit of the record
        /// </summary>
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
