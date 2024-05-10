using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.Validation
{
    public class CheckBoxValidator
    {
        public static async Task<bool> ValidateCheckBoxAsync(CheckBox checkBox, Label errorLabel)
        {
            if (!checkBox.IsChecked)
            {
                await ErrorMessage.ShowErrorMessage(errorLabel, "Please confirm the chekbox");
                return false;
            }
            return true;
        }
    }
}
