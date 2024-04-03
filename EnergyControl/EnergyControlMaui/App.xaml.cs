using EnergyControlMaui.Services;
using EnergyControlMaui.Views;

namespace EnergyControlMaui
{
    public partial class App : Application
    {
        private readonly UserManager userManager;
        public App(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
            MainPage = new NavigationPage(new WelcomePage(userManager)); 
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async Task HandleLoginSuccess()
        {
            // Создайте страницу с тап-баром
            var appShell = new AppShell();

            // Перейдите на страницу с тап-баром
            await MainPage.Navigation.PushAsync(appShell);
        }
    }
}
