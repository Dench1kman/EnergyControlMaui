
using EnergyControlMaui.Services;

namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private readonly UserManager userManager;
        public AccountPage(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
        }

        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(userManager));
        }
    }
}