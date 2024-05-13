#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

#if ANDROID
using Android.Widget;
using EnergyControlMaui.Models;
using Android.Content;
using EnergyControlMaui.Services;
using Newtonsoft.Json;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EnergyControlMaui.Controllers
{
    public class LampController
    {
#if ANDROID
        public async Task ConnectToLampAsync()
        {
            var lamp = LampDetailsService.GetLampDetails();

            Ping ping = new Ping();
            PingReply reply = ping.Send(lamp.IPAddress);

            if (reply.Status == IPStatus.Success)
            {
                Toast.MakeText(Android.App.Application.Context, $"Lamp is at {lamp.IPAddress} responsive", ToastLength.Long).Show();
                await Task.Delay(1000);

                //await SendWifiDetailsToLampAsync(lamp.IPAddress); // Error 
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, $"Lamp at {lamp.IPAddress} is not responsive.", ToastLength.Long).Show();
            }
        }

        private async Task SendWifiDetailsToLampAsync(string lampIpAddress)
        {
            using (HttpClient client = new HttpClient())
            {
                var wifiDetails = WifiDetailsService.GetWifiDetails();
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
#endif
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.