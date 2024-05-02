using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.Validation
{
    public class WifiValidator
    {
        public async Task<bool> ValidateWifiSsidAsync(string wifiSsid, Label wifiSsidErrorLabel)
        {
            if (string.IsNullOrWhiteSpace(wifiSsid))
            {
                await ErrorMessage.ShowErrorMessage(wifiSsidErrorLabel, "Wi-Fi name cannot be empty!");
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateWifiPasswordAsync(string wifiPassword, Label wifiPasswordErrorLabel)
        {
            if (string.IsNullOrWhiteSpace(wifiPassword))
            {
                await ErrorMessage.ShowErrorMessage(wifiPasswordErrorLabel, "Wi-Fi password cannot be empty!");
                return false;
            }
            return true;
        }
    }
}
