using EnergyControlMaui.Services;
using EnergyControlMaui.Views;


namespace EnergyControlMaui
{
    public partial class App : Application
    {
        public App()  
        {
            InitializeComponent();

            MainPage = new NavigationPage(new WelcomePage(UserManager.GetInstance()));
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
