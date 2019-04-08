namespace OrganizationOfData.Windows
{
    using Microsoft.Win32;
    using System.Windows;

    public class FileDialogService : IFileDialogService
    {
        private readonly Window owner;

        public string FileName { get; set; }

        public string Title { get; set; }

        public string Filter { get; set; }

        public string InitialDirectory { get; set; }

        public FileDialogService(Window owner)
        {
            this.owner = owner;
        }

        public bool? ShowOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = this.Title,
                Filter = this.Filter,
                InitialDirectory = this.InitialDirectory
            };

            bool? result = openFileDialog.ShowDialog(owner);

            FileName = openFileDialog.FileName;

            return result;
        }

        public bool? ShowSaveFileDialog()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Title = this.Title,
                Filter = this.Filter,
                InitialDirectory = this.InitialDirectory
            };

            bool? result = saveFileDialog.ShowDialog(owner);

            FileName = saveFileDialog.FileName;

            return result;
        }
    }
}
