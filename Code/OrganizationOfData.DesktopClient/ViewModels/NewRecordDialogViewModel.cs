namespace OrganizationOfData.DesktopClient.ViewModels
{
    using System;
    using System.Windows.Input;
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;

    public class NewRecordDialogViewModel : ViewModel, IDialogRequestClose
    {
        private Record record;

        public Record Record
        {
            get
            {
                return record;
            }
            set
            {
                record = value;
                NotifyPropertyChanged(nameof(Record));
            }
        }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public NewRecordDialogViewModel()
        {
            Record = new Record()
            {
                Person = new Person(),
                Status = Status.active
            };
        }

        #region CreateNewRecord

        public ICommand CreateNewRecordCommand
        {
            get
            {
                return new ActionCommand(p => CreateNewRecord(), p => Record.IsValid());
            }
        }

        public void CreateNewRecord()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        #endregion

        #region CancelCommandMembers

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
