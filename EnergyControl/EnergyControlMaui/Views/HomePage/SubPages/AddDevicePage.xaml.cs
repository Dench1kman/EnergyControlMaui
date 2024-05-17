#pragma warning disable CS8602 // Dereference of a possibly null reference.

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
            InitializeComponent ();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });

            var lampManager = LampManager.GetInstance();
            _lamp = lampManager.GetDetails();

        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
			if (!string.IsNullOrEmpty(LampNameEntry.Text))
			{
                var currentUser = UserManager.GetInstance().GetUser();
                var lamp = LampManager.GetInstance();

                _lamp.LampName = LampNameEntry.Text;
                _lamp.LampId = currentUser.UserId;

                lamp.SetDetails(_lamp);
                lamp.AddLamp(_lamp);

                await DisplayAlert("Success", $"You have Successfully connected to your device! Go to \"Control\" for the interaction. ", "OK");

                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new ControlPage());
            }
            else await ErrorMessage.ShowErrorMessage(LampNameErrorLabel, "Lamp Name cannot be empty!"); 
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.