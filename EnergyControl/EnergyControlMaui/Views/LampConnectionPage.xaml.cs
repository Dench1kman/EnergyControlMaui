#pragma warning disable CA1422 // Validate platform compatibility
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

#if ANDROID
using Android.Content;
using Android.Net.Wifi;
using Android.Widget;
#endif
using EnergyControlMaui.Controllers;
using EnergyControlMaui.Services;

namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LampConnectionPage : ContentPage
	{
#if ANDROID
		public LampConnectionPage()
		{
			InitializeComponent();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });

			LampConnection();
        }

		private async void LampConnection() 
		{
			LampController lampController = new LampController();
			await lampController.ConnectToLampAsync();


            var wifiInfo = WifiDetailsService.GetWifiDetails();

            WifiConfiguration wifiConfig = new WifiConfiguration
            {
                Ssid = $"\"{wifiInfo.SSID}\"",
                PreSharedKey = $"\"{wifiInfo.Password}\""
            };

            WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);
            wifiManager.Disconnect();

            int netId = wifiManager.AddNetwork(wifiConfig);
            wifiManager.EnableNetwork(netId, true);

            await Task.Delay(10000);

            Toast.MakeText(Android.App.Application.Context, "Reconnected to your Wi-Fi", ToastLength.Long).Show();

            await Task.Delay(1);
            await Navigation.PushAsync(new AddDevicePage());
		}
#endif
    }
}
#pragma warning restore CA1422 // Validate platform compatibility
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.