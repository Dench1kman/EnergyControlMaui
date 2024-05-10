using CommunityToolkit.Maui.Views;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupView : Popup
    { 
#if ANDROID
        public PopupView(string title, string message, int timer)
        {
            InitializeComponent();

            Title.Text = title;
            Message.Text = message;

            ClosePopupAsync(timer);
        }

        public async Task ClosePopupAsync(int timer)
        {
            await Task.Delay(timer);
            await CloseAsync();
        }
#endif
    }
}