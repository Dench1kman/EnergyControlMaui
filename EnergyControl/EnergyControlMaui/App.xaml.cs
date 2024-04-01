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
            MainPage = new NavigationPage(new WelcomePage(userManager));
            this.userManager = userManager;
        }

        protected override void OnStart()
        {
            MainPage = new AppShell();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Переключиться на AppShell после отображения WelcomePage
            Application.Current.MainPage = new AppShell();
        }


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
