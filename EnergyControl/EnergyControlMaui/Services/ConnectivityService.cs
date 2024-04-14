using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.Services
{
    public class ConnectivityService
    {
        public static bool IsConnected()
        {
            var networkAccess = Connectivity.NetworkAccess;
            return networkAccess == NetworkAccess.Internet;
        }

        public static async Task ShowNoInternetConnectionError()
        {
            if (Application.Current != null)
            {
                await Application.Current.MainPage.DisplayAlert("No Internet Connection",
                "Please check your internet connection and try again.", "OK");
            }
        }
    }
}
