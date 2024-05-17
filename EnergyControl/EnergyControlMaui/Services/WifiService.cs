#pragma warning disable CA1422 // Validate platform compatibility
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.

#if ANDROID
using Android.Content;
using Android.Net.Wifi;
#endif
using Microsoft.EntityFrameworkCore;
using EnergyControlMaui.Models;


namespace EnergyControlMaui.Services
{
    public class WifiService
    {
#if ANDROID
        public WifiNetwork? WifiNetwork { get; private set; }
        private static WifiService? _instance;

        public WifiService() { }

        public static WifiService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WifiService();
            }
            return _instance;
        }

        public void SetDetails(WifiNetwork wifiNetwork)
        {
            WifiNetwork = wifiNetwork;
        }

        public WifiNetwork GetDetails()
        {
            return WifiNetwork;
        }

        public async Task<string> GetActiveWifiSsidAsync()
        {
            try
            {
                var wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

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


        public bool ConnectToWifi(string ssid, string password)
        {
            try
            {
                WifiConfiguration wifiConfig = new WifiConfiguration();

                wifiConfig.Ssid = $"\"{ssid}\"";
                wifiConfig.PreSharedKey = $"\"{password}\"";

                WifiManager wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

                int netId = wifiManager.AddNetwork(wifiConfig);
                wifiManager.Disconnect();
                bool isConnected = wifiManager.EnableNetwork(netId, true);
                wifiManager.Reconnect();

                if (isConnected)
                {
                    return true;
                }
                else
                {
                    return true;//false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when connecting to Wi-Fi: {ex.Message}");
                return false;
            }
        }
#endif
    }
}
#pragma warning restore CA1422 // Validate platform compatibility
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8603 // Possible null reference return.