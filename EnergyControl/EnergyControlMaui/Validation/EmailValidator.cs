using System.Text.RegularExpressions;
using EnergyControlMaui.Services;

namespace EnergyControlMaui.Validation
{
    public class EmailValidator
    {
        public static async Task<bool> ValidateLogInEmailAsync(string email, Label errorLabel)
        {
            bool IsValid = await ValidateEmailFormat(email, errorLabel);

            if (IsValid)
            {
                bool emailExists = await UserManager.GetInstance().UserExistsAsync(email); 

                if (!emailExists)
                {
                    await ErrorMessage.ShowErrorMessage(errorLabel, "This email is not registered!");
                    return false;
                }
            }
            else 
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidateSignUpEmailAsync(string email, Label errorLabel)
        {
            bool IsValid = await ValidateEmailFormat(email, errorLabel);

            if (IsValid) 
            {
                bool emailExists = await UserManager.GetInstance().UserExistsAsync(email); 

                if (emailExists)
                {
                    await ErrorMessage.ShowErrorMessage(errorLabel, "This email already exists!");
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidateEmailFormat(string email, Label errorLabel) 
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[\w\.-]+@([\w\.-]+\.)+[A-Za-z]{2,4}$"))
            {
                await ErrorMessage.ShowErrorMessage(errorLabel, "Invalid email format!");
                return false;
            }
            return true;
        }
    }
}

