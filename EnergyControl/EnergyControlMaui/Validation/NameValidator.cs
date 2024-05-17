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
            else if (firstName.Length > 30)
            {
                await ErrorMessage.ShowErrorMessage(firstNameErrorLabel, "First name cannot be longer than 30 characters!");
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
            else if (lastName.Length > 30)
            {
                await ErrorMessage.ShowErrorMessage(lastNameErrorLabel, "Last name cannot be longer than 30 characters!");
                return false;
            }
            return true;
        }
    }
}
