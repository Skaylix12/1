
using Microsoft.EntityFrameworkCore;
using ConcreteCrackManager.Data;
using System.IO;
using System.Text.Json;

namespace ConcreteCrackManager
{
    public class ConcreteDbContextFactory
    {
        public ConcreteDbContext CreateDbContext()
        {
            var cs = GetConnectionString();
            var options = new DbContextOptionsBuilder<ConcreteDbContext>()
                .UseNpgsql(cs)
                .Options;
            return new ConcreteDbContext(options);
        }

        private string GetConnectionString()
        {
            return GetConnectionStringStatic();
        }

        public static string GetConnectionStringStatic()
        {
            var file = "connection.json";
            if (File.Exists(file))
            {
                var j = File.ReadAllText(file);
                try
                {
                    using var doc = JsonDocument.Parse(j);
                    if (doc.RootElement.TryGetProperty("ConnectionString", out var cs))
                        return cs.GetString() ?? Default();
                }
                catch { return Default(); }
            }
            return Default();
        }

        private static string Default()
        {
            return "Host=localhost;Port=5432;Database=concrete_inspections;Username=concrete_admin;Password=AdminPass123;";
        }
    }
}
