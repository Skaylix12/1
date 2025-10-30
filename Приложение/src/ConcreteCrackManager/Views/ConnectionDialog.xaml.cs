
using System.Windows;
using Npgsql;
using System.IO;

namespace ConcreteCrackManager.Views
{
    public partial class ConnectionDialog : Window
    {
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        private void OnTest(object sender, RoutedEventArgs e)
        {
            var cs = BuildConnectionString();
            try
            {
                using var conn = new NpgsqlConnection(cs);
                conn.Open();
                MessageBox.Show("Connection OK");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Connection FAILED: " + ex.Message);
            }
        }

        private string BuildConnectionString()
        {
            return $"Host={HostBox.Text};Port={PortBox.Text};Database={DbBox.Text};Username={UserBox.Text};Password={PassBox.Password};";
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            var cs = BuildConnectionString();
            var cfg = new { ConnectionString = cs };
            var json = System.Text.Json.JsonSerializer.Serialize(cfg);
            File.WriteAllText("connection.json", json);
            MessageBox.Show("Saved to connection.json. Перезапустите приложение, чтобы строка была применена.");
        }

        private void OnClose(object sender, RoutedEventArgs e) => Close();
    }
}
