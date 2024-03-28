using System;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using EnergyControlMaui.Services;

namespace EnergyControlMaui
{
    public partial class App : Application
    {
        private readonly UserManager userManager;
        public App(UserManager userManager)
        {
            InitializeComponent();
            MainPage = new NavigationPage(new WelcomePage(userManager));
            this.userManager = userManager;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
