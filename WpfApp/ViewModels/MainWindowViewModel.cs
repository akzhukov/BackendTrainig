using System.Windows;
using WpfApp.ModelViews;

namespace WpfApp.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindow MainWindow { get; set; }

        public DashboardView DashboardView { get; set; }
        public DashboardViewModel DashboardViewModel { get; set; }
        public LoginView LoginView { get; set; }
        public LoginViewModel LoginViewModel { get; set; }


        public void InitMainWindow(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MainWindow.Show();
            MainWindow.FrameBody.NavigationService.Navigate(LoginView);
        }


        public void NavigateToDashboardView()
        {
            MainWindow.FrameBody.NavigationService.Navigate(DashboardView);
        }

        public void NavigateToLoginView()
        {
            MainWindow.FrameBody.NavigationService.Navigate(LoginView);
        }

    }
}