using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace EnergyControlMaui.Validation
{
    public class ErrorMessage
    {
        public static async Task ShowErrorMessage(Label errorLabel, string message, int milliseconds = 3000)
        {
            errorLabel.Text = message;
            errorLabel.IsVisible = true;

            await Task.Delay(milliseconds); 

            errorLabel.IsVisible = false;
        }
    }
}
