using EnergyControlMaui.Services;
using EnergyControlMaui.Views;


namespace EnergyControlMaui
{
    public partial class App : Application
    {
        private readonly UserManager userManager;

        public App(UserManager userManager)  
        {
            this.userManager = userManager;
            InitializeComponent();

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
    }
}
