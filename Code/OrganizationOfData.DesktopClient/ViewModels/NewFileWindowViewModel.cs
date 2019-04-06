namespace OrganizationOfData.DesktopClient.ViewModels
{
    using MvvmDialogs;
    using OrganizationOfData.Data;
    using OrganizationOfData.Windows;
    using System;
    using System.Windows.Input;

    public class NewFileWindowViewModel : ViewModel, IModalDialogViewModel
    {
        public BulkFile bulkFile;

        private bool? dialogResult;

        public bool? DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                dialogResult = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand Zavrsi
        {
            get
            {
                return new ActionCommand(p => Zavrsio());
            }
        }

        private void Zavrsio()
        {
            DialogResult = true;
        }

        public ICommand ZavrsiBez
        {
            get
            {
                return new ActionCommand(p => ZavrsioBez());
            }
        }

        private void ZavrsioBez()
        {
            DialogResult = false;
        }
    }
}
