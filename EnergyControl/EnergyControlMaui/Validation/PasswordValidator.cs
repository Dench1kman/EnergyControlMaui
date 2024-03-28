using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace EnergyControlMaui.Validation
{
    public class PasswordValidator
    {
        public static async Task<bool> ValidatePasswordAsync(string password, string confirmPassword, Label errorLabel, Label errorLabelMatch)
        {
            Task<bool> validatePasswordTask = ValidatePassword(password, errorLabel);
            Task<bool> validateConfirmPasswordTask = ValidatePassword(confirmPassword, errorLabelMatch);

            await Task.WhenAll(validatePasswordTask, validateConfirmPasswordTask);

            bool isPasswordValid = await validatePasswordTask;
            bool isConfirmPasswordValid = await validateConfirmPasswordTask;

            if (isPasswordValid == true && isConfirmPasswordValid == true)
            {
                if (password != confirmPassword)
                {
                    await ErrorMessage.ShowErrorMessage(errorLabelMatch, "Passwords do not match!");
                    return false;
                }
                return true;
            }
            return false;
        }

        public static async Task<bool> ValidatePassword(string password, Label errorLabel)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                await ErrorMessage.ShowErrorMessage(errorLabel, "Password should be entered!");
                return false;
            }
            if (password.Length < 6 || password.Length > 12)
            {
                await ErrorMessage.ShowErrorMessage(errorLabel, "Password must be 6-12 characters!");
                return false;
            }
            if (!ContainsValidCharacters(password))
            {
                await ErrorMessage.ShowErrorMessage(errorLabel, "Password must contain at least one letter (Aa-Zz) and one number (0-9)!");
                return false;
            }
            return true;
        }

        private static bool ContainsValidCharacters(string password)
        {
            return password.Any(char.IsDigit) && password.Any(char.IsLower) && password.Any(char.IsUpper);
        }
    }
}
