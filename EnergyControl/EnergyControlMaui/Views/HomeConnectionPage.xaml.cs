#pragma warning disable CS8602 // Dereference of a possibly null reference.

#if ANDROID
using Android.Net.Wifi;
using Android.Widget;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeConnectionPage : ContentPage
	{
#if ANDROID
        private readonly WifiService _wifiService;
        private readonly WifiConnection _wifiConnection;
        private PopupView _popupView;

        public HomeConnectionPage()
		{
            _wifiService = new WifiService(Android.App.Application.Context);

            InitializeComponent();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
        }

        private async void LampButton_Clicked(object sender, EventArgs e)
        {
            var isWifiActive = await _wifiService.CheckAndSwitchToWifiAsync();
            if (isWifiActive)
            {   
                await Navigation.PushAsync(new WifiConnectionPage());
            }
        }
        
        private void ThermostatButton_Clicked(object sender, EventArgs e)
        {
            Toast.MakeText(Android.App.Application.Context, "This function is in development", ToastLength.Long).Show();
        }

        private void PrinterButton_Clicked(object sender, EventArgs e)
        {
            Toast.MakeText(Android.App.Application.Context, "This function is in development", ToastLength.Long).Show();
        }

        private void DeviceButton_Clicked(object sender, EventArgs e)
        {
            Toast.MakeText(Android.App.Application.Context, "This function is in development", ToastLength.Long).Show();
        }
#endif
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.