using System;
using System.Collections.Generic;
using DialARide.ViewModel;
using Xamarin.Forms;

namespace DialARide.View
{
    public partial class AddJourneyView : ContentPage
    {
        AddJourneyViewModel vm;
        public AddJourneyView()
        {
            InitializeComponent();
            vm = new AddJourneyViewModel();
            BindingContext = vm;
        }
    }
}
