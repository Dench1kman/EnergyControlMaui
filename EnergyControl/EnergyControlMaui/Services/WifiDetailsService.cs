#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using EnergyControlMaui.Models;


namespace EnergyControlMaui.Services
{
    public static class WifiDetailsService
    {
        private static WifiNetwork _wifiNetwork;

        public static WifiNetwork GetWifiDetails()
        {
            return _wifiNetwork;
        }

        public static void SetWifiDetails(WifiNetwork wifiNetwork)
        {
            _wifiNetwork = wifiNetwork;
        }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.