using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace EnergyControlMaui.Validation
{
    public class EmailValidator
    {
        public static async Task<bool> ValidateEmailAsync(string email, Label errorLabel)
        {
            var input = email;

            if (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, @"^[\w\.-]+@([\w\.-]+\.)+[A-Za-z]{2,4}$"))
            {
                await ErrorMessage.ShowErrorMessage(errorLabel, "Invalid email format!");
                return false;
            }

            return true;
        }


    }
}

