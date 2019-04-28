using System;
using System.Collections.Generic;
using DialARide.ViewModel;
using Xamarin.Forms;

namespace DialARide
{
    public partial class HomeView : ContentPage
    {
        HomeViewModel vm;
        public HomeView()
        {
            InitializeComponent();
            vm = new HomeViewModel
            {
                Navigation = Navigation
            };
            BindingContext = vm;
        }
    }
}
