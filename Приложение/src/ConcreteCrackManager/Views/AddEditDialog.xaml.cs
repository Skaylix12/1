
using System;
using System.Windows;
using ConcreteCrackManager.Data;
using ConcreteCrackManager.Models;

namespace ConcreteCrackManager.Views
{
    public partial class AddEditDialog : Window
    {
        private readonly string _table;
        private readonly object? _entity;

        public string DialogTitle { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Location { get; set; }
        public DateTime? InspectedAt { get; set; }

        public AddEditDialog(string table, object? entity)
        {
            InitializeComponent();
            _table = table;
            _entity = entity;

            if (_table == "Inspections")
            {
                DialogTitle = entity == null ? "Add Inspection" : "Edit Inspection";
                if (entity is Inspection ins)
                {
                    Name = ins.Name;
                    Location = ins.Location;
                    InspectedAt = ins.InspectedAt;
                }
            }

            DataContext = this;
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            using var db = new ConcreteDbContextFactory().CreateDbContext();
            if (_table == "Inspections")
            {
                if (_entity == null)
                {
                    var newIns = new Inspection { Name = Name, Location = Location, InspectedAt = InspectedAt ?? DateTime.UtcNow };
                    db.Inspections.Add(newIns);
                }
                else
                {
                    var ins = db.Inspections.Find(((Inspection)_entity).Id);
                    if (ins != null)
                    {
                        ins.Name = Name;
                        ins.Location = Location;
                        ins.InspectedAt = InspectedAt ?? DateTime.UtcNow;
                        db.Update(ins);
                    }
                }
                db.SaveChanges();
                DialogResult = true;
            }
            else
            {
                // For simplicity, only Inspections implemented in this dialog.
                MessageBox.Show("This dialog currently supports Inspections only.");
                DialogResult = false;
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}
