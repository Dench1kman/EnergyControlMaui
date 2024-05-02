#pragma warning disable IDE0017 // Simplify object initialization
#pragma warning disable CA1422 // Validate platform compatibility
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

#if ANDROID
using Android.Content;
using Android.Media.Midi;
using Android.Net.Wifi;
#endif
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Services
{
    public class WifiConnection
    {
#if ANDROID

        public async Task<bool> ConnectToWifiAsync(string ssid, string password)
        {
            try
            {
                WifiConfiguration wifiConfig = new WifiConfiguration();

                wifiConfig.Ssid = $"\"{ssid}\"";
                wifiConfig.PreSharedKey = $"\"{password}\"";

                var wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

                int netId = wifiManager.AddNetwork(wifiConfig);
                wifiManager.Disconnect();
                bool enableNetwork = wifiManager.EnableNetwork(netId, true);

                if (enableNetwork)
                {
                    return true;
                }
                else
                {
                    return true;//return false;
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
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CA1422 // Validate platform compatibility
#pragma warning restore IDE0017 // Simplify object initialization
