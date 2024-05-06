#if ANDROID
using Android.Net.Wifi;
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
                //await Navigation.PushModalAsync(new WifiConnectionPage());
                
                await Navigation.PushAsync(new WifiConnectionPage());
            }
        }
        
        private void ThermostatButton_Clicked(object sender, EventArgs e)
        {

        }

        private void PrinterButton_Clicked(object sender, EventArgs e)
        {

        }

        private void DeviceButton_Clicked(object sender, EventArgs e)
        {

        }
#endif
    }
}