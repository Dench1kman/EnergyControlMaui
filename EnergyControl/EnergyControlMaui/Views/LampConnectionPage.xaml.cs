

namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LampConnectionPage : ContentPage
	{
		public LampConnectionPage()
		{
			InitializeComponent();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });

			Temporarily();
        }

		private async void Temporarily() 
		{
            await Task.Delay(10);
            await DisplayAlert("Success", $"Successfully connected to Lamp", "OK");
			await Task.Delay(1);
            await Navigation.PushAsync(new AddDevicePage());
        }
    }
}