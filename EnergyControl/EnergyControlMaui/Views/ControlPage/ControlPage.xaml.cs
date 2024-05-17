#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

#if ANDROID
using Android.Widget;
#endif
using EnergyControlMaui.Models;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
    public partial class ControlPage : ContentPage
    {
#if ANDROID
        private Lamp _lamp;

        bool isImage1Displayed = true;

        public ControlPage()
        {
            InitializeComponent();

            var lampManager = LampManager.GetInstance();
            _lamp = lampManager.GetDetails();

            if (_lamp != null)
            {
                Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
                GetLampControl();
            }
        }

        private async void GetLampControl()
        {
            var currentUser = Services.UserManager.GetInstance().GetUser();
            var lampManager = LampManager.GetInstance();

            var isExists = await lampManager.GetLampByUserIdAsync(currentUser.UserId);
            
            if (isExists.LampId != 0)
            {
                NoDevicesText.IsVisible = false;

                LampNameLabel.Text = _lamp.LampName;
                LampControl.IsVisible = true;
            }
            else
            {
                NoDevicesText.IsVisible = true;
            }
        }

        private async void LampButton_Clicked(object sender, EventArgs e)
        {
            if (isImage1Displayed)
            {
                LampButton.Source = "bulb_off.svg";
                isImage1Displayed = true;
                await SendRequestToLampAsync("turnOn");
            }
            else
            {
                LampButton.Source = "bulb_on.svg";
                isImage1Displayed = false;
                await SendRequestToLampAsync("turnOff");
            }
        }

        private async Task SendRequestToLampAsync(string action)
        {
            var lamp = LampManager.GetInstance();
            string lampIpAddress = lamp.GetDetails().IPAddress;

            string apiUrl = $"https://{lampIpAddress}/{action}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    if (action == "turnOff")
                    {
                        _lamp.IsOn = false;
                    }
                    else
                        _lamp.IsOn = true;

                    lamp.SetDetails(_lamp);
                    await lamp.UpdateLampAsync(_lamp);
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context, "Error when sending request", ToastLength.Long).Show();
                }
            }

        }
#endif
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.