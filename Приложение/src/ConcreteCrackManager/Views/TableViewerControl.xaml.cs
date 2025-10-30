
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using ConcreteCrackManager.Data;
using ConcreteCrackManager.Models;

namespace ConcreteCrackManager.Views
{
    public partial class TableViewerControl : UserControl
    {
        private readonly string _tableName;
        public string Title => $"Table: {_tableName}";

        public TableViewerControl(string tableName)
        {
            InitializeComponent();
            _tableName = tableName;
            DataContext = this;
            RefreshData();
        }

        private void RefreshData()
        {
            using var db = new ConcreteDbContextFactory().CreateDbContext();
            object? data = _tableName switch
            {
                "Inspections" => db.Inspections.Include(i => i.Inspector).ToList(),
                "Images" => db.Images.Include(i => i.Inspection).ToList(),
                "Defects" => db.Defects.Include(d => d.Image).ToList(),
                "AppUsers" => db.AppUsers.Include(u => u.Role).ToList(),
                "AppRoles" => db.AppRoles.ToList(),
                _ => null
            };

            Grid.ItemsSource = data as System.Collections.IEnumerable;
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            var dlg = new AddEditDialog(_tableName, null);
            dlg.Owner = Window.GetWindow(this);
            if (dlg.ShowDialog() == true) RefreshData();
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var selected = Grid.SelectedItem;
            if (selected == null) { MessageBox.Show("Select a row"); return; }
            var dlg = new AddEditDialog(_tableName, selected);
            dlg.Owner = Window.GetWindow(this);
            if (dlg.ShowDialog() == true) RefreshData();
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            var selected = Grid.SelectedItem;
            if (selected == null) { MessageBox.Show("Select a row"); return; }

            if (MessageBox.Show("Delete selected?", "Confirm", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;

            using var db = new ConcreteDbContextFactory().CreateDbContext();
            switch (_tableName)
            {
                case "Inspections": db.Remove((Inspection)selected); break;
                case "Images": db.Remove((ImageEntity)selected); break;
                case "Defects": db.Remove((Defect)selected); break;
                case "AppUsers": db.Remove((AppUser)selected); break;
                case "AppRoles": db.Remove((AppRole)selected); break;
            }
            db.SaveChanges();
            RefreshData();
        }

        private void OnRefresh(object sender, RoutedEventArgs e) => RefreshData();
    }
}
