using EnergyControlMaui.Validation;
using EnergyControlMaui.Services;
using EnergyControlMaui.Models;


namespace EnergyControlMaui.Views
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void CreateAccountButton_Clicked(object sender, EventArgs e)
        {
            if (!ConnectivityService.IsConnected())
            {
                await ConnectivityService.ShowNoInternetConnectionError();
                return;
            }

            Task<bool> isNameValid = NameValidator.ValidateNameAsync(FirstNameEntry.Text, LastNameEntry.Text, FirstNameErrorLabel, LastNameErrorLabel);
            Task<bool> isEmailValid = EmailValidator.ValidateSignUpEmailAsync(EmailEntrySignUp.Text, EmailEntryErrorLabelSignUp);
            Task<bool> isPasswordValid = PasswordValidator.ValidatePasswordsAsync(PasswordEntry.Text, ConfirmPasswordEntry.Text, PasswordEntryErrorLabelSignUp, ConfirmPasswordEntryErrorLabelSignUp);
            Task<bool> isCheckBoxValid = CheckBoxValidator.ValidateCheckBoxAsync(CheckBox, CheckBoxErrorLabel);

            await Task.WhenAll(isNameValid, isEmailValid, isPasswordValid,
                isCheckBoxValid);

            if (isNameValid.Result && isEmailValid.Result &&
                isPasswordValid.Result && isCheckBoxValid.Result )
            {
                string hashedPassword = PasswordHasher.HashPassword(PasswordEntry.Text);

                User user = new User
                {
                    FirstName = FirstNameEntry.Text,
                    LastName = LastNameEntry.Text,
                    Email = EmailEntrySignUp.Text,
                    Password = hashedPassword
                };

                bool registrationResult = UserManager.GetInstance().RegisterUser(user);

                if (registrationResult)
                {
                    UserManager.GetInstance().SetUser(user);

                    await DisplayAlert("Success", "Registration successful!", "OK");
                    await Navigation.PushAsync(new AppShell());
                }
                else
                {
                    await DisplayAlert("Error", "Registration failed. Please try again later.", "OK");
                }
            }
        }

        private async void LogInLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
