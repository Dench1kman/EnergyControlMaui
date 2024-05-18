﻿#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

#if ANDROID
using Android.Widget;
#endif
using EnergyControlMaui.Models;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
#if ANDROID
        private UserManager _userManager;

        public AccountPage()
        {
            InitializeComponent();
            _userManager = UserManager.GetInstance();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var currentUser = UserManager.GetInstance().GetUser();

            if (currentUser != null)
            {
                SetUserData(currentUser);
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
            Toast.MakeText(Android.App.Application.Context, "This function is in development", ToastLength.Long).Show();
        }

        private async void AboutButton_Clicked(object sender, EventArgs e)
        {
            Toast.MakeText(Android.App.Application.Context, "This function is in development", ToastLength.Long).Show();
        }

        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            await Navigation.PushAsync(new LoginPage());
        }
#endif
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously