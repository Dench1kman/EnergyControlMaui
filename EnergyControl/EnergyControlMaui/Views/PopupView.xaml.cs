using CommunityToolkit.Maui.Views;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupView : Popup
    { 
#if ANDROID
        public PopupView()
        {
            InitializeComponent();
            ClosePopupAsync();
        }

        public async Task ClosePopupAsync()
        {
            await Task.Delay(10000);
            await CloseAsync();
        }
#endif
    }
}