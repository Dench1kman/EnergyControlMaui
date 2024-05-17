#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionsPage : ContentPage
    {
#if ANDROID
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

        private void ConnectButton_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void ConfirmButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WifiLampConnectionPage());
        }
#endif
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete