using EnergyControlMaui.Models;
using EnergyControlMaui.Services;
using System;
using System.Collections.Generic;

namespace EnergyControlMaui.Views
{
    public partial class HomePage : ContentPage
    {
        //private readonly UserManager _userManager;
        //private List<User> _users;

        public HomePage()
        {
            InitializeComponent();
            //_userManager = UserManager.GetInstance();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (ConnectivityService.IsConnected())
            {
                //await Navigation.PushModalAsync(new HomeConnectionPage());
                await Navigation.PushAsync(new HomeConnectionPage());
            }
            else 
                await ConnectivityService.ShowNoInternetConnectionError();
        }

        // Show All Users List

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    _users = await _userManager.GetAllUsersAsync();
        //    listView.ItemsSource = _users;
        //}
    }
}
