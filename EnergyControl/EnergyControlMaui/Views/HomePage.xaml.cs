using EnergyControlMaui.Services;

namespace EnergyControlMaui.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (ConnectivityService.IsConnected())
            {
                await Navigation.PushModalAsync(new HomeConnectionPage());
            }
            else 
                await ConnectivityService.ShowNoInternetConnectionError();
        }
    }
}
