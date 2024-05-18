using EnergyControlMaui.Validation;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage() 
        {
            InitializeComponent();
        }

        private async void AuthorizeButton_Clicked(object sender, EventArgs e)
        {
            if (!ConnectivityService.IsConnected())
            {
                await ConnectivityService.ShowNoInternetConnectionError();
                return;
            }

            Task<bool> isEmailValid = EmailValidator.ValidateLogInEmailAsync(EmailEntry.Text, EmailErrorLabel);
            Task<bool> isPasswordValid = PasswordValidator.VerifyPasswordAsync(EmailEntry.Text, PasswordEntry.Text, InvalidPasswordErrorLabel, UserManager.GetInstance()); // userManager

            await Task.WhenAll(isEmailValid, isPasswordValid);

            if (isEmailValid.Result && isPasswordValid.Result)
            {
                var userManager = UserManager.GetInstance();
                var user = await userManager.GetUserDataAsync(EmailEntry.Text);
                userManager.SetUser(user);

                    await Navigation.PushAsync(new AppShell());
            }
        }

        private async void SignUpLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}

