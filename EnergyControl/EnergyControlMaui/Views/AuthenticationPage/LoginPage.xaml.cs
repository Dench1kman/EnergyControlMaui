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

            Task<bool> emailValidationTask = EmailValidator.ValidateLogInEmailAsync(EmailEntry.Text, EmailErrorLabel);
            Task<bool> passwordValidationTask = PasswordValidator.VerifyPasswordAsync(EmailEntry.Text, PasswordEntry.Text, InvalidPasswordErrorLabel, UserManager.GetInstance()); // userManager

            await Task.WhenAll(emailValidationTask, passwordValidationTask);

            if (emailValidationTask.Result && passwordValidationTask.Result)
            {
                await Navigation.PushAsync(new AppShell());
            }
        }

        private async void SignUpLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}

