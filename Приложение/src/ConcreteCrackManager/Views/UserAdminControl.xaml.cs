
using System.Windows;
using System.Windows.Controls;

namespace ConcreteCrackManager.Views
{
    public partial class UserAdminControl : UserControl
    {
        public UserAdminControl()
        {
            InitializeComponent();
            UsersContent.Content = new TableViewerControl("AppUsers");
            RolesContent.Content = new TableViewerControl("AppRoles");
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            UsersContent.Content = new TableViewerControl("AppUsers");
            RolesContent.Content = new TableViewerControl("AppRoles");
        }
    }
}
