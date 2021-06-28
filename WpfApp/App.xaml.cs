using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.ModelViews;
using WpfApp.Services;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<AutorizeService>();
                services.AddSingleton<EventsService>();

            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var mainViewModel = host.Services.GetRequiredService<MainWindowViewModel>();
            var autorizeService = host.Services.GetRequiredService<AutorizeService>();
            var getDataService = host.Services.GetRequiredService<EventsService>();
            var loginViewModel = new LoginViewModel(autorizeService);
            // Add data context for MainWindow
            var mainWindow = new MainWindow { DataContext = mainViewModel };

            mainViewModel.LoginViewModel = loginViewModel;
            mainViewModel.LoginView = new LoginView { DataContext = loginViewModel };

            var dashboardViewModel = new DashboardViewModel(autorizeService, getDataService);
            mainViewModel.DashboardViewModel = dashboardViewModel;
            mainViewModel.DashboardView = new DashboardView(autorizeService, getDataService) { DataContext = dashboardViewModel };


            mainViewModel.InitMainWindow(mainWindow);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
