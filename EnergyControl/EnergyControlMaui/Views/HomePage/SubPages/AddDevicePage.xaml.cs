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

            _lamp = LampManager.GetInstance().GetDetails();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
			if (!string.IsNullOrEmpty(LampNameEntry.Text))
			{
                var userManager = UserManager.GetInstance();
                var lampManager = LampManager.GetInstance();
                var user = userManager.GetUser();

                _lamp.LampName = LampNameEntry.Text;
                _lamp.Brightness = 50;
                _lamp.Color = "White";
                _lamp.UserId = user.UserId;
                user.Lamps = _lamp;

                lampManager.SetDetails(_lamp);
                lampManager.AddLamp(_lamp);

                await userManager.UpdateUserAsync(user);
                userManager.SetUser(user);

                await DisplayAlert("Success", $"You have Successfully connected to your device! " +
                    $"Go to \"Control\" for the interaction. ", "OK");

                await Navigation.PopToRootAsync();
                await Navigation.PushAsync(new ControlPage());
            }
            else await ErrorMessage.ShowErrorMessage(LampNameErrorLabel, "Lamp Name cannot be empty!"); 
        }
    }
}