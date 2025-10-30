
using System;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ConcreteCrackManager.Data;

namespace ConcreteCrackManager
{
    public partial class App : Application
    {
        private IHost? _host;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Try to read connection from connection.json, fallback to default sample
                    var cs = ConcreteCrackManager.ConcreteDbContextFactory.GetConnectionStringStatic();

                    services.AddDbContext<ConcreteDbContext>(options =>
                        options.UseNpgsql(cs));

                    services.AddSingleton<MainWindow>();
                })
                .Build();

            await _host.StartAsync();

            var wnd = _host.Services.GetRequiredService<MainWindow>();
            wnd.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_host is not null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
            base.OnExit(e);
        }
    }
}
