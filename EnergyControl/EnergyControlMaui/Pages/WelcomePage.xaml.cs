using EnergyControlMaui.Services;

namespace EnergyControlMaui
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
            await Navigation.PushAsync(new SignupPage(userManager));
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(userManager));
        }
    }
}