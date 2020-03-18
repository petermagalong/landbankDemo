using System;
using System.Collections.Generic;
using landbankDemo.Models;
using Xamarin.Forms;

namespace landbankDemo.Views
{
    public partial class LoginPage : ContentPage
    {
        private Model model = new Model();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = model;
        }
    }
}
