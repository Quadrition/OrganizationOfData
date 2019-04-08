namespace OrganizationOfData.Windows
{
    public interface IFileDialogService
    {
        string FileName { get; set; }

        string Title { get; set; }

        string Filter { get; set; }

        string InitialDirectory { get; set; }

        bool? ShowOpenFileDialog();

        bool? ShowSaveFileDialog();
    }
}
