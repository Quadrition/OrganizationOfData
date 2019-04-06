namespace OrganizationOfData.DesktopClient
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using MvvmDialogs;
    using OrganizationOfData.Data;
    using OrganizationOfData.DesktopClient.ViewModels;
    using OrganizationOfData.DesktopClient.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindow()
            {
                DataContext = new MainWindowViewModel()
            };

            window.ShowDialog();
        }
    }
}
