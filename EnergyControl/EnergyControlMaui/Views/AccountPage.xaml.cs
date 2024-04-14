using EnergyControlMaui.Models;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        public AccountPage(User user)
        {
            InitializeComponent();

            SetUserData(user);
        }

        private void SetUserData(User user) 
        {
            FirstNameLabel.Text = user.FirstName; 
            LastNameLabel.Text = user.LastName;  
            EmailLabel.Text = user.Email;         
        }

        private async void LogOutButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}