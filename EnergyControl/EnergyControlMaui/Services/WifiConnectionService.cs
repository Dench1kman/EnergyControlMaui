using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.Services
{
    public class WifiConnectionService
    {
        private static WifiConnectionService _instance;
        private readonly WifiConnection _wifiConnection;

        private WifiConnectionService()
        {
            _wifiConnection = new WifiConnection();
        }

        public static WifiConnectionService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WifiConnectionService();
            }
            return _instance;
        }

        public WifiConnection GetWifiConnection()
        {
            return _wifiConnection;
        }
    }
}
