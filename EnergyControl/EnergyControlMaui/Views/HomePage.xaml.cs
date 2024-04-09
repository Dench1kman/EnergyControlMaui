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
            await Navigation.PushModalAsync(new HomeConnectionPage());
        }
    }
}
