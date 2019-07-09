namespace OrganizationOfData.DesktopClient.Views
{
    using OrganizationOfData.DesktopClient.ViewModels;
    using OrganizationOfData.Windows;
    using System.Windows;

    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window, IDialog
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 0;
        }

        private void TreeViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 1;
        }

        private void TreeViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 2;
        }

        private void TreeViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 3;
        }

        private void TreeViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 4;
        }

        private void TreeViewItem_Selected_5(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 5;
        }

        private void TreeViewItem_Selected_6(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 6;
        }

        private void TreeViewItem_Selected_7(object sender, RoutedEventArgs e)
        {
            (DataContext as HelpWindowViewModel).SelectedIndex = 7;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as HelpWindowViewModel).Close();
        }
    }
}
