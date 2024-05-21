#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

#if ANDROID
using Android.Hardware.Camera2.Params;
using Android.Widget;
#endif
using EnergyControlMaui.Models;
using EnergyControlMaui.Services;
using EnergyControlMaui.Controllers;


namespace EnergyControlMaui.Views
{
    public partial class ControlPage : ContentPage
    {
#if ANDROID
        private Lamp _lamp;
        private LampController _lampController;
        bool isImage1Displayed = true;

        public ControlPage()
        {
            InitializeComponent();
            GetLampControl();
        }

        private async void GetLampControl()
        {
            var userManager = UserManager.GetInstance();
            var currentUser = userManager.GetUser();

            var lampManager = LampManager.GetInstance();
            var lamp = await lampManager.GetLampByIdAsync(currentUser.UserId);

            if (currentUser?.Lamps != null)
            {
                _lamp = currentUser.Lamps;
                NoDevicesText.IsVisible = false;

                LampNameLabel.Text = _lamp.LampName;
                LampControl.IsVisible = true;

                BrightnessSlider.Value = _lamp.Brightness;
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
                isImage1Displayed = false;
                await _lampController.SendRequestToLampAsync("turnOn");
            }
            else
            {
                LampButton.Source = "bulb_on.svg";
                isImage1Displayed = true;
                await _lampController.SendRequestToLampAsync("turnOff");
            }
        }
        private async void BrightnessSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (_lamp != null)
            {
                int newBrightness = (int)e.NewValue;
                _lamp.Brightness = newBrightness;

                var lampManager = LampManager.GetInstance();
                lampManager.SetDetails(_lamp);
                await lampManager.UpdateLampAsync(_lamp);

                await _lampController.SendBrightnessToLampAsync(newBrightness);
            }
        }     

        private async void DisconnectButton_Clicked(object sender, EventArgs e)
        {
            var userManager = UserManager.GetInstance();
            var currentUser = userManager.GetUser();

            var lampManager = LampManager.GetInstance();

            if (currentUser == null)
            {
                await DisplayAlert("Error", "User not found", "OK");
                return;
            }

            bool isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to delete the lamp?", "Yes", "No");

            if (isConfirmed)
            {
                bool isLampRemovedFromLamps = await lampManager.RemoveLampAsync(currentUser.UserId);

                if (isLampRemovedFromLamps)
                {
                    currentUser.Lamps = null;
                    await userManager.UpdateUserAsync(currentUser);
                    userManager.SetUser(currentUser);

                    lampManager.ClearDetails();

                    Toast.MakeText(Android.App.Application.Context, "Lamp successfully removed", ToastLength.Long).Show();

                    NoDevicesText.IsVisible = true;
                    LampControl.IsVisible = false;
                    LampNameLabel.Text = string.Empty;
                }
                else
                {
                    Toast.MakeText(Android.App.Application.Context, "Failed to remove the lamp", ToastLength.Long).Show();
                }
            }
        }

#endif
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8602 // Dereference of a possibly null reference.