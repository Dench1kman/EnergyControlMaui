using EnergyControlMaui.Services;

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
            var isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;

            if (!isConnected)
            {
                await DisplayAlert("Error", "No internet connection. Please connect to the internet and try again.", "OK");
            }
            else
                await Navigation.PushAsync(new SignupPage(userManager));
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            var isConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;

            if (!isConnected)
            {
                await DisplayAlert("Error", "No internet connection. Please connect to the internet and try again.", "OK");
            }
            else
                await Navigation.PushAsync(new LoginPage(userManager));
        }
    }
}