using EnergyControlMaui.Validation;
using EnergyControlMaui.Services;

namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly UserManager userManager;
        public LoginPage(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
        }

        private async void AuthorizeButton_Clicked(object sender, EventArgs e)
        {
            Task<bool> emailValidationTask = EmailValidator.ValidateEmailAsync(EmailEntry.Text, EmailErrorLabel);
            Task<bool> passwordValidationTask = PasswordValidator.ValidatePassword(PasswordEntry.Text, InvalidPasswordErrorLabel);
            await Task.WhenAll(emailValidationTask, passwordValidationTask);

            if (emailValidationTask.Result && passwordValidationTask.Result)
            {
                Task<bool> emailExistsValidationTask = userManager.UserExistsAsync(EmailEntry.Text);
                await emailExistsValidationTask;

                if (!emailExistsValidationTask.Result)
                {
                    await ErrorMessage.ShowErrorMessage(EmailNotRegisteredErrorLabel, "This email is not registered!");
                    return;
                }

                string hashedPassword = PasswordHasher.HashPassword(PasswordEntry.Text);

                Task<bool> checkPassValidationTask = userManager.CheckPasswordAsync(EmailEntry.Text, hashedPassword);
                await checkPassValidationTask;


                if (!checkPassValidationTask.Result)
                {
                    await ErrorMessage.ShowErrorMessage(InvalidPasswordErrorLabel, "Incorrect password. Please try again!");
                    return;
                }

                await Task.WhenAll(emailExistsValidationTask, checkPassValidationTask);
            }
            else
                return;

            await Navigation.PushAsync(new AppShell());
        }

        private async void SignUpLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage(userManager));
        }
    }
}

