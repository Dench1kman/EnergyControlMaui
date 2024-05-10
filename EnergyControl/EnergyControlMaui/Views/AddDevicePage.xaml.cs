#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using EnergyControlMaui.Models;
using EnergyControlMaui.Services;
using EnergyControlMaui.Validation;


namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDevicePage : ContentPage
	{
		private Lamp _lamp;
        public AddDevicePage ()
        {
			_lamp = new Lamp ();
			InitializeComponent ();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
			if (!string.IsNullOrEmpty(LampNameEntry.Text))
			{
				_lamp.LampName = LampNameEntry.Text;
                LampDetailsService.SetLampDetails(_lamp);

                await DisplayAlert("Success", $"You have Successfully connected to your device! Go to \"Control\" for the interaction. ", "OK");

                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new ControlPage());

            }
            else await ErrorMessage.ShowErrorMessage(LampNameErrorLabel, "Lamp Name cannot be empty!");
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.