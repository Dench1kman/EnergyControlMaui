using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
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
                var userManager = new UserManager(new SqliteDbContext());   
                bool emailExists = await userManager.UserExistsAsync(email); 

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
                var userManager = new UserManager(new SqliteDbContext());   
                bool emailExists = await userManager.UserExistsAsync(email); 

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

