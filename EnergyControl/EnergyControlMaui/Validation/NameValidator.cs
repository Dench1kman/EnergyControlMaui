﻿using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace EnergyControlMaui.Validation
{
    public class NameValidator
    {
        public static async Task<bool> ValidateNameAsync(string firstName, string lastName, Label firstNameErrorLabel, Label lastNameErrorLabel)
        {
            Task<bool> validateFirstNameTask = ValidateFirstNameAsync(firstName, firstNameErrorLabel);
            Task<bool> validateLastNameTask = ValidateLastNameAsync(lastName, lastNameErrorLabel);

            await Task.WhenAll(validateFirstNameTask, validateLastNameTask);

            bool isFirstNameValid = await validateFirstNameTask;
            bool isLastNameValid = await validateLastNameTask;

            return isFirstNameValid && isLastNameValid;
        }

        public static async Task<bool> ValidateFirstNameAsync(string firstName, Label firstNameErrorLabel)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                await ErrorMessage.ShowErrorMessage(firstNameErrorLabel, "First name cannot be empty!");
                return false;
            }
            return true;
        }

        public static async Task<bool> ValidateLastNameAsync(string lastName, Label lastNameErrorLabel)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                await ErrorMessage.ShowErrorMessage(lastNameErrorLabel, "Last name cannot be empty!");
                return false;
            }
            return true;
        }
    }

}
