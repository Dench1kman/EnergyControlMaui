using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using EnergyControlMaui.Services;

namespace EnergyControlMaui.Validation
{
    public class PasswordValidator
    {
        public static async Task<bool> ValidatePasswordsAsync(string password, string confirmPassword, Label errorLabel, Label errorLabelMatch)
        {
            Task<bool> validatePasswordTask = ValidatePasswordFormat(password, errorLabel);
            Task<bool> validateConfirmPasswordTask = ValidatePasswordFormat(confirmPassword, errorLabelMatch);

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
        public static async Task<bool> VerifyPasswordAsync(string email, string password, Label errorLabel, UserManager userManager)
        {
            Task<bool> validatePasswordTask = ValidatePasswordFormat(password, errorLabel);
            await validatePasswordTask;

            string hashedPassword = PasswordHasher.HashPassword(password);

            Task<bool> checkPassValidationTask = userManager.CheckPasswordAsync(email, hashedPassword);
            await checkPassValidationTask;

            if (!checkPassValidationTask.Result)
            {
                await ErrorMessage.ShowErrorMessage(errorLabel, "Incorrect password. Please try again!");
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidatePasswordFormat(string password, Label errorLabel)
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
