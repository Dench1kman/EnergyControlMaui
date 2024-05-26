using EnergyControlMaui.Services;

namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePage : ContentPage
	{
        private readonly UserManager _userManager;

        public WelcomePage (UserManager userManager)
		{
            InitializeComponent ();
            _userManager = userManager;
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            if (ConnectivityService.IsConnected())
            {
                await Navigation.PushAsync(new SignupPage());
            }
            else
                await ConnectivityService.ShowNoInternetConnectionError();
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            if (ConnectivityService.IsConnected())
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else
                await ConnectivityService.ShowNoInternetConnectionError();
        }
    }
}