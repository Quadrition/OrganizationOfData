using MvvmDialogs;
using OrganizationOfData.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationOfData.DesktopClient.ViewModels
{
    public class AuthorsWindowViewModel : ViewModel, IModalDialogViewModel
    {
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
                NotifyPropertyChanged(nameof(DialogResult));
            }
        }

        public AuthorsWindowViewModel()
        {

        }
    }
}
