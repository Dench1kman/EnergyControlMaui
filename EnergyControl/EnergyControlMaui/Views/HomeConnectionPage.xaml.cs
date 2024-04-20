using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeConnectionPage : ContentPage
	{
		public HomeConnectionPage()
		{
			InitializeComponent();
		}

        private async void LampButton_Clicked(object sender, EventArgs e)
        {
            if (ConnectivityService.IsConnected())
            {
                await Navigation.PushModalAsync(new WifiConnectionPage());
            }
            else
                await ConnectivityService.ShowNoInternetConnectionError();
        }

        private void ThermostatButton_Clicked(object sender, EventArgs e)
        {

        }

        private void PrinterButton_Clicked(object sender, EventArgs e)
        {

        }

        private void DeviceButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}