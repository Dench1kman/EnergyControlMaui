#pragma warning disable CS8602 // Dereference of a possibly null reference.

#if ANDROID
using Android.Content;
using Android.Media.Midi;
using Android.Net;
using Android.Net.Wifi;
using System.Collections;
#endif
using EnergyControlMaui.Services;
using EnergyControlMaui.Validation;
using EnergyControlMaui.Models;



namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WifiConnectionPage : ContentPage
    {
#if ANDROID
        private readonly WifiService _wifiService;
        private readonly WifiConnection _wifiConnection;

        public WifiConnectionPage()
        {
            _wifiService = new WifiService(Android.App.Application.Context);
            _wifiConnection = new WifiConnection();

            InitializeComponent();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
            GetAvailableNetworks();
            
        }
        private bool GetAvailableNetworks()
        {
            var getSsid = _wifiService.GetActiveWifiSsidAsync().Result;

            if (!string.IsNullOrEmpty(getSsid))
            {
                WifiSsidEntry.Text = getSsid;
                return true;
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Error when getting active Wi-Fi SSID", "OK");
                return false;
            }
        }
#endif
        private async void ConnectButton_Clicked(object sender, EventArgs e)
        {
            if (!ConnectivityService.IsConnected())
            {
                await ConnectivityService.ShowNoInternetConnectionError();
                return;
            }
#if ANDROID
            if (string.IsNullOrEmpty(WifiPasswordEntry.Text))
            {
                await ErrorMessage.ShowErrorMessage(WifiPasswordErrorLabel, "Password field cannot be empty!");
            }
            else
            {
                var isValid = await _wifiConnection.ConnectToWifiAsync(WifiSsidEntry.Text, WifiPasswordEntry.Text);

                if (isValid)
                {
                    await DisplayAlert("Success", $"Successfully connected to Wi-Fi network: {WifiSsidEntry.Text}", "OK");
                    //await Navigation.PushModalAsync(new InstructionsPage());
                    await Navigation.PushAsync(new InstructionsPage());
                }
                else
                {
                    await DisplayAlert("Error", $"Failed to connect to Wi-Fi network: {WifiSsidEntry.Text}. " +
                                        $"Please check your credentials and try again.", "OK");
                }
            }
#endif
        }
    }
}

#pragma warning restore CS8602 // Dereference of a possibly null reference.