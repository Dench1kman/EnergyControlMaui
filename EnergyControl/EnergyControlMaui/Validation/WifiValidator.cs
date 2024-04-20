using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.Validation
{
    public class WifiValidator
    {
        public async Task<bool> ValidateWifiNameAsync(string wifiName, string wifiPassword, Label wifiNameErrorLabel)
        {
            if (string.IsNullOrWhiteSpace(wifiName))
            {
                await ErrorMessage.ShowErrorMessage(wifiNameErrorLabel, "Wi-Fi name cannot be empty!");
                return false;
            }

            bool wifiAvailable = await IsWifiAvailableAsync(wifiName);
            if (!wifiAvailable)
            {
                await ErrorMessage.ShowErrorMessage(wifiNameErrorLabel, "Wi-Fi network with this name not found!");
                return false;
            }
            return true;
        }

        public async Task<bool> ValidateWifiPasswordAsync(string wifiName, string wifiPassword, Label wifiPasswordErrorLabel)
        {
            if (string.IsNullOrWhiteSpace(wifiPassword))
            {
                await ErrorMessage.ShowErrorMessage(wifiPasswordErrorLabel, "Wi-Fi password cannot be empty!");
                return false;
            }
            
            bool passwordCorrect = await IsPasswordCorrectAsync(wifiName, wifiPassword);
            if (!passwordCorrect)
            {
                await ErrorMessage.ShowErrorMessage(wifiPasswordErrorLabel, "Incorrect Wi-Fi password!");
                return false;
            }
            return true;
        }

        private async Task<bool> IsWifiAvailableAsync(string wifiName)
        {
            // Here

            return true; 
        }

        private async Task<bool> IsPasswordCorrectAsync(string wifiName, string wifiPassword)
        {
            // Here

            return true; 
        }
    }
}
