#pragma warning disable CS8604 // Possible null reference argument.

using EnergyControlMaui.Models;
using EnergyControlMaui.Services;
using EnergyControlMaui.Validation;
using EnergyControlMaui.Utilities;


namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDevicePage : ContentPage
	{
        private readonly UserManager _userManager;
        public AddDevicePage ()
        {
			InitializeComponent ();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
            _userManager = UserManager.GetInstance();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
			if (!string.IsNullOrEmpty(LampNameEntry.Text))
			{
                User currentUser = await _userManager.GetUserDataAsync(AppConstants.Email);

                var lamp = LampDetailsService.GetLampDetails();

				lamp.LampName = LampNameEntry.Text;
                lamp.LampId = currentUser.UserId;

                LampDetailsService.SetLampDetails(lamp);

                var lampManager = LampManager.GetInstance();
                lampManager.AddLamp(lamp);

                await DisplayAlert("Success", $"You have Successfully connected to your device! Go to \"Control\" for the interaction. ", "OK");

                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new ControlPage());

            }
            else await ErrorMessage.ShowErrorMessage(LampNameErrorLabel, "Lamp Name cannot be empty!"); 
        }
    }
}
#pragma warning restore CS8604 // Possible null reference argument.