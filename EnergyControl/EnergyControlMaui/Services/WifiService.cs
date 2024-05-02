#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CA1422 // Validate platform compatibility

#if ANDROID
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.Widget;
#endif
using System.Threading.Tasks;
using EnergyControlMaui.Services;
using EnergyControlMaui.Views;
using Microsoft.EntityFrameworkCore;


namespace EnergyControlMaui.Services
{
    public class WifiService
    {
#if ANDROID
        private readonly Context _context;

        public WifiService(Context context)
        {
            _context = context;
        }

        public async Task<bool> CheckAndSwitchToWifiAsync()
        {
            try
            {
                IEnumerable<ConnectionProfile> profiles = Connectivity.Current.ConnectionProfiles;

                if (profiles.Contains(ConnectionProfile.Cellular))
                {
                    await Application.Current.MainPage.DisplayAlert("Wi-Fi", "Please connect to Wi-Fi for proper operation.", "OK");
                    return false;
                }

                if (profiles.Contains(ConnectionProfile.WiFi))
                {
                    var activeWifiSsid = await GetActiveWifiSsidAsync();

                    if (string.IsNullOrEmpty(activeWifiSsid))
                    {
                        await Application.Current.MainPage.DisplayAlert("Wi-Fi", "Please connect to an available Wi-Fi network.", "OK");
                        return false; 
                    }
                    return true;
                }

                if (!ConnectivityService.IsConnected())
                {
                    await ConnectivityService.ShowNoInternetConnectionError();
                }
                return false;
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error when checking and switching to Wi-Fi", "OK");
                return false;
            }
        }
    

        public async Task<string> GetActiveWifiSsidAsync()
        {
            try
            {
                var wifiManager = (WifiManager)_context.GetSystemService(Context.WifiService);

                var wifiInfo = wifiManager.ConnectionInfo;

                if (wifiInfo != null && !string.IsNullOrEmpty(wifiInfo.SSID))
                {
                    return wifiInfo.SSID.Replace("\"", "");
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error when getting active Wi-Fi SSID", "OK");
                return string.Empty;
            }
        }
#endif
    }
}
#pragma warning restore CA1422 // Validate platform compatibility
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.