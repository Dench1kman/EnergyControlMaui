#pragma warning disable CA1422 // Validate platform compatibility
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS0618 // Type or member is obsolete

#if ANDROID
using Android.Content;
using Android.Net.Wifi;
using Android.Widget;
#endif
using System.Net;
using EnergyControlMaui.Validation;
using EnergyControlMaui.Models;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WifiLampConnectionPage : ContentPage
    {
#if ANDROID
        private Lamp _lamp;
        public WifiLampConnectionPage()
        {
            _lamp = new Lamp();

            InitializeComponent();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
        }

        private async void ConfirmConnectionButton_Clicked(object sender, EventArgs e)
        {
            Task<bool> checkBoxValidationTask = CheckBoxValidator.ValidateCheckBoxAsync(ChkBox, ChkBoxErrorLabel);
            await checkBoxValidationTask;

            if (checkBoxValidationTask.Result)
            {
                var lampIp = GetWifiHotspotIpAddress().ToString();
                if (lampIp == null)
                {
                    await DisplayAlert("Erorr", "Unable to locate the lamp!", "OK");
                }
                else
                    Toast.MakeText(Android.App.Application.Context, "lamp is detected", ToastLength.Short).Show();

                _lamp.IPAddress = lampIp;
                _lamp.Port = 443;
                _lamp.IsConnected = true;
                _lamp.IsOn = true; 
                LampDetailsService.SetLampDetails(_lamp);

                await Navigation.PushAsync(new LampConnectionPage());
            }
        }

        private string GetWifiHotspotIpAddress()
        {
            var wifiManager = (WifiManager)Android.App.Application.Context.GetSystemService(Context.WifiService);

            var wifiInfo = wifiManager.ConnectionInfo;

            if (wifiInfo != null && wifiInfo.SupplicantState == SupplicantState.Completed)
            {
                int ipAddress = wifiInfo.IpAddress;
                var ipAddr = IPAddress.Parse(Android.Text.Format.Formatter.FormatIpAddress(ipAddress));

                return ipAddr.ToString();
            }
            else
            {
                return null;
            }
        }
#endif
    }
}
#pragma warning restore CA1422 // Validate platform compatibility
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.