
using System.Windows;
using Npgsql;
using System.IO;
using System.Text.Json;

namespace ConcreteCrackManager.Views
{
    public partial class DbRolesDialog : Window
    {
        private string? _connectionString;

        public DbRolesDialog()
        {
            InitializeComponent();
            LoadConnection();
            SqlBox.Text = "-- Example: CREATE ROLE new_role WITH LOGIN PASSWORD 'pwd';\n-- Or: GRANT SELECT ON ALL TABLES IN SCHEMA app TO inspector_user;";
        }

        private void LoadConnection()
        {
            if (File.Exists("connection.json"))
            {
                var j = File.ReadAllText("connection.json");
                var obj = JsonDocument.Parse(j);
                if (obj.RootElement.TryGetProperty("ConnectionString", out var cs))
                    _connectionString = cs.GetString();
            }
            else
            {
                MessageBox.Show("connection.json not found. Open Connection... and save the connection string.");
            }
        }

        private void OnRun(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("No connection string");
                return;
            }

            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                conn.Open();
                using var cmd = new NpgsqlCommand(SqlBox.Text, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Command executed");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void OnClose(object sender, RoutedEventArgs e) => Close();
    }
}
