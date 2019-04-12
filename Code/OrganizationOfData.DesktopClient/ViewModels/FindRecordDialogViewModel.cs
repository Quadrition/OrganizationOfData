namespace OrganizationOfData.DesktopClient.ViewModels
{
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;

    public class FindRecordDialogViewModel : ViewModel, IDialogRequestClose
    {
        private int id;

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

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public FindRecordDialogViewModel()
        {

        }

        #region DeleteRecordMembers

        public ICommand DeleteRecordCommand
        {
            get
            {
                return new ActionCommand(p => DeleteRecord());
            }
        }

        public void DeleteRecord()
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
