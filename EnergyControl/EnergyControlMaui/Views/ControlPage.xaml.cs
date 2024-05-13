#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

#if ANDROID
using Android.OS;
using Android.Widget;
#endif
using EnergyControlMaui.Models;
using EnergyControlMaui.Services;
using EnergyControlMaui.Utilities;


namespace EnergyControlMaui.Views
{
    public partial class ControlPage : ContentPage
    {
        private Lamp _lamp;
        private Services.UserManager _userManager;

        bool isImage1Displayed = true;

        public ControlPage()
        {
            InitializeComponent();
             _userManager = Services.UserManager.GetInstance();
            _lamp = LampDetailsService.GetLampDetails();
            if (_lamp != null)
            {
                Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
                GetLampControl();
            }
        }

        private async void GetLampControl()
        {
            User currentUser = await _userManager.GetUserDataAsync(AppConstants.Email);
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
            string lampIpAddress = LampDetailsService.GetLampDetails().IPAddress;
            string apiUrl = $"https://{lampIpAddress}/{action}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                _lamp = LampDetailsService.GetLampDetails();

                if (response.IsSuccessStatusCode)
                {
                    if (action == "turnOff")
                    {
                        _lamp.IsOn = false;
                    }
                    else
                        _lamp.IsOn = true;

                    LampDetailsService.SetLampDetails(_lamp);
                    LampManager lampManager = LampManager.GetInstance();
                    bool isUpdated = await lampManager.UpdateLampAsync(_lamp);
                }
                else
                {
#if ANDROID
                    Toast.MakeText(Android.App.Application.Context, "Error when sending request", ToastLength.Long).Show();
#endif
                }
            }

        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously