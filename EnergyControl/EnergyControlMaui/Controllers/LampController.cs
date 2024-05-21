#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

#if ANDROID
using Android.Widget;
using Newtonsoft.Json;
#endif
using System.Net.NetworkInformation;
using System.Text;
using EnergyControlMaui.Services;
using EnergyControlMaui.Models;


namespace EnergyControlMaui.Controllers
{
    public class LampController
    {
#if ANDROID
        private Lamp _lamp;

        public async Task ConnectToLampAsync()
        {
            var lamp = LampManager.GetInstance().GetDetails();

            Ping ping = new Ping();
            PingReply reply = ping.Send(lamp.IPAddress);

            if (reply.Status == IPStatus.Success)
            {
                Toast.MakeText(Android.App.Application.Context, $"Lamp is at {lamp.IPAddress} responsive", ToastLength.Long).Show();
                await Task.Delay(1000);

                try
                {
                    await SendWifiDetailsToLampAsync(lamp.IPAddress);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(Android.App.Application.Context, $"Error: {ex.Message}", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, $"Lamp at {lamp.IPAddress} is not responsive.", ToastLength.Long).Show();
            }
        }

        public async Task SendRequestToLampAsync(string action)
        {
            try
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
            catch (Exception)
            {
                Toast.MakeText(Android.App.Application.Context, "Connection failure", ToastLength.Long).Show();
            }
        }

        private async Task SendWifiDetailsToLampAsync(string lampIpAddress)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var wifiDetails = WifiService.GetInstance().GetDetails();
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(wifiDetails), Encoding.UTF8, "application/json");
                    var apiUrl = $"https://{lampIpAddress}/configure";
                    var response = await client.PostAsync(apiUrl, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        Toast.MakeText(Android.App.Application.Context, $"Lamp received Wi-Fi data", ToastLength.Long).Show();
                        await Task.Delay(1000);
                    }
                    else
                    {
                        Toast.MakeText(Android.App.Application.Context, $"Oops! Lamp didn't receive Wi-Fi data", ToastLength.Long).Show();
                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Android.App.Application.Context, $"Exception: {ex.Message}", ToastLength.Long).Show();

            }
        }

        public async Task SendBrightnessToLampAsync(int brightness)
        {
            var lamp = LampManager.GetInstance().GetDetails();
            string lampIpAddress = lamp.IPAddress;

            string apiUrl = $"https://{lampIpAddress}/setBrightness?value={brightness}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        Toast.MakeText(Android.App.Application.Context, "Error when sending brightness value to the lamp", ToastLength.Long).Show();
                    }
                }
                catch (HttpRequestException)
                {
                    Toast.MakeText(Android.App.Application.Context, "Connection failure", ToastLength.Long).Show();
                }
            }
        }
#endif
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.