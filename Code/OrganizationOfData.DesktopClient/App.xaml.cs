namespace OrganizationOfData.DesktopClient
{
    using System.Windows;
    using OrganizationOfData.DesktopClient.ViewModels;
    using OrganizationOfData.DesktopClient.Views;
    using OrganizationOfData.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var view = new MainWindow();
            MainWindow = view;

            IDialogService dialogService = new DialogService(MainWindow);

            dialogService.Register<NewFileWindowViewModel, NewFileWindow>();
            dialogService.Register<AuthorsDialogViewModel, AuthorsDialog>();
            dialogService.Register<NewRecordDialogViewModel, NewRecordDialog>();
            dialogService.Register<DeleteRecordDialogViewModel, DeleteRecordDialog>();
            dialogService.Register<HelpWindowViewModel, HelpWindow>();
            dialogService.Register<EditRecordDialogViewModel, EditRecordDialog>();

            IFileDialogService fileDialogService = new FileDialogService(MainWindow);

            var viewModel = new MainWindowViewModel(dialogService, fileDialogService);
            view.DataContext = viewModel;

            view.ShowDialog();
        }
    }
}
