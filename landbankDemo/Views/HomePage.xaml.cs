using System;
using System.Collections.Generic;
using System.Threading;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace landbankDemo.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }


        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("", "Would you like to exit from application?", "Yes", "No");
                if (result)
                {
                        Thread.CurrentThread.Abort();
                }
            });
            return true;
        }

    }
}
