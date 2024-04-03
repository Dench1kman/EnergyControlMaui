

namespace EnergyControlMaui.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void AddBtn_Cliked(object sender, EventArgs e)
        {
            //Перенаправляет на страницу выбора какое у-во подключить
        }

        protected override bool OnBackButtonPressed()
        {
            return true; 
        }
    }
}
