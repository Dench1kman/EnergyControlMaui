using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyControlMaui.Validation;
using EnergyControlMaui.Views;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WifiLampConnectionPage : ContentPage
    {
#if ANDROID
        public WifiLampConnectionPage()
        {
            InitializeComponent();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
        }

        private async void ConfirmConnectionButton_Clicked(object sender, EventArgs e)
        {
            Task<bool> checkBoxValidationTask = CheckBoxValidator.ValidateCheckBoxAsync(ChkBox, ChkBoxErrorLabel);
            await checkBoxValidationTask;

            if (checkBoxValidationTask.Result)
            {
                await Navigation.PushAsync(new LampConnectionPage());
            }
        }
#endif
    }
}