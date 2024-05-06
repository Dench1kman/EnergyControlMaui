#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionsPage : ContentPage
    {
        private int imageIndex = 1;
        private string[] imageSources = { "bulb_off.svg", "bulb_on.svg" };

        public InstructionsPage()
        {
            InitializeComponent();
            Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                BulbImage.Source = imageSources[imageIndex % imageSources.Length];
                imageIndex++;

                return true; 
            });
        }

        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new AddDevicePage());
            await Navigation.PushAsync(new AddDevicePage());
        }

    }
}
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete