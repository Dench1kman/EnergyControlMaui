﻿#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using EnergyControlMaui.Models;
using EnergyControlMaui.Services;
using EnergyControlMaui.Utilities;
using EnergyControlMaui.Views;

namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private UserManager _userManager;
        public AccountPage()
        {
            InitializeComponent();

            _userManager = UserManager.GetInstance();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            User _user = await _userManager.GetUserDataAsync(AppConstants.Email);

            if (_user != null)
            {
                SetUserData(_user);
            }
            else
            {
                await DisplayAlert("Error", "User data not found", "OK");

                await Navigation.PushAsync(new LoginPage());
            }
        }

        private void SetUserData(User _user)
        {
            FirstNameLabel.Text = _user.FirstName;
            LastNameLabel.Text = _user.LastName;
            EmailLabel.Text = _user.Email;
        }

        private async void SecurityButton_Clicked(object sender, EventArgs e)
        {
        }

        private async void AboutButton_Clicked(object sender, EventArgs e)
        {
        }

        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
