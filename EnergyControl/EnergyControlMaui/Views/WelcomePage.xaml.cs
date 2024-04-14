using EnergyControlMaui.Services;
using EnergyControlMaui.Validation;
using Microsoft.Maui.Networking;

namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePage : ContentPage
	{
        private readonly UserManager userManager;
        public WelcomePage (UserManager userManager)
		{
            InitializeComponent ();
            this.userManager = userManager;
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            if (ConnectivityService.IsConnected())
            {
                await Navigation.PushAsync(new SignupPage(userManager));
            }
            else
                await ConnectivityService.ShowNoInternetConnectionError();
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            if (ConnectivityService.IsConnected())
            {
                await Navigation.PushAsync(new LoginPage(userManager));
            }
            else
                await ConnectivityService.ShowNoInternetConnectionError();
        }
    }
}