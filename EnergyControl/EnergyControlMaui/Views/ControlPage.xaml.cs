using EnergyControlMaui.Models;
using EnergyControlMaui.Services;


namespace EnergyControlMaui.Views
{
    public partial class ControlPage : ContentPage
    {
        private Lamp _lamp;

        bool isImage1Displayed = true;

        public ControlPage()
        {
            InitializeComponent();
            _lamp = LampDetailsService.GetLampDetails();
            if (_lamp != null) 
            {
                Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
                GetLampControl();

            }
        }
        //public ControlPage(Lamp lamp)
        //{
        //    InitializeComponent();
        //    Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false });
        //    _lamp = lamp;
        //    GetLampControl();
        //}

        private async void GetLampControl() 
        {
            if (!string.IsNullOrEmpty(_lamp.LampName))
            {
                NoDevicesText.IsVisible = false;

                LampNameLabel.Text = _lamp.LampName;
                LampControl.IsVisible = true;
            }
            else 
            {
                NoDevicesText.IsVisible = true;
            }

        }

        private void LampButton_Clicked(object sender, EventArgs e)
        {
            if (isImage1Displayed)
            {
                LampButton.Source = "bulb_on.svg";
                isImage1Displayed = false;
            }
            else
            {
                LampButton.Source = "bulb_off.svg";
                isImage1Displayed = true;
            }
        }
    }
}