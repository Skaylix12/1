
using System.Windows;
using ConcreteCrackManager.Views;

namespace ConcreteCrackManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnConnectionClick(object sender, RoutedEventArgs e)
        {
            var dlg = new ConnectionDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        private void OnInspectionsClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TableViewerControl("Inspections");
        }

        private void OnImagesClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TableViewerControl("Images");
        }

        private void OnDefectsClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TableViewerControl("Defects");
        }

        private void OnUsersClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new UserAdminControl();
        }

        private void OnDbRolesClick(object sender, RoutedEventArgs e)
        {
            var dlg = new DbRolesDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
