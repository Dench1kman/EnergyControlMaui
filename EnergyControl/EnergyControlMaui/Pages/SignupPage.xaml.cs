﻿using EnergyControlMaui.Validation;
using EnergyControlMaui.Services;
using EnergyControlMaui.Models;
//using Microsoft.UI.Xaml;

namespace EnergyControlMaui
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        private readonly UserManager userManager;
        public SignupPage(UserManager userManager)
        {
            InitializeComponent();
            this.userManager = userManager;
        }

        private async void CreateAccountButton_Clicked(object sender, EventArgs e)
        {
            Task<bool> nameValidationTask = NameValidator.ValidateNameAsync(FirstNameEntry.Text, LastNameEntry.Text, FirstNameErrorLabel, LastNameErrorLabel);
            Task<bool> emailValidationTask = EmailValidator.ValidateEmailAsync(EmailEntrySignUp.Text, EmailEntryErrorLabelSignUp);
            Task<bool> passwordValidationTask = PasswordValidator.ValidatePasswordAsync(PasswordEntry.Text, ConfirmPasswordEntry.Text, PasswordEntryErrorLabelSignUp, ConfirmPasswordEntryErrorLabelSignUp);
            Task<bool> checkBoxValidationTask = CheckBoxValidator.ValidateCheckBoxAsync(CheckBox, CheckBoxErrorLabel);
            Task<bool> emailExistsValidationTask = Task.FromResult(false);
            if (emailValidationTask.Result)
            {
                emailExistsValidationTask = userManager.UserExistsAsync(EmailEntrySignUp.Text);
                if (emailExistsValidationTask.Result)
                {
                    await ErrorMessage.ShowErrorMessage(EmailExistsEntryErrorLabelSignUp, "This email already exists!");
                    return;
                }
            }

            await Task.WhenAll(nameValidationTask, emailValidationTask, passwordValidationTask, 
                checkBoxValidationTask, emailExistsValidationTask);

            if (nameValidationTask.Result && emailValidationTask.Result && 
                passwordValidationTask.Result && checkBoxValidationTask.Result &&
                !emailExistsValidationTask.Result)
            {
                User user = new User
                {
                    FirstName = FirstNameEntry.Text,
                    LastName = LastNameEntry.Text,
                    Email = EmailEntrySignUp.Text,
                    Password = PasswordEntry.Text
                };

                bool registrationResult = userManager.RegisterUser(user);

                if (registrationResult)
                {
                    await DisplayAlert("Success", "Registration successful!", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Error", "Registration failed. Please try again later.", "OK");
                }
            }
        }

        private async void LogInLabel_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(userManager));
        }
    }
}
