using System;
using System.ComponentModel;
using System.Windows.Input;
using DialARide.View;
using Xamarin.Forms;

namespace DialARide.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public ICommand ViewJourneyCommand { get; set; }
        public ICommand AddJourneyCommand { get; set; }

        public INavigation Navigation { get; set; }

        private JourneyRepository repos;
        public HomeViewModel()
        {
            ViewJourneyCommand = new Command(ViewJourney);
            AddJourneyCommand = new Command(AddJourney);
            repos = new JourneyRepository();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        async void ViewJourney()
        {
            await Navigation.PushAsync(new JourneyView());
        }

        async void AddJourney()
        {
            await Navigation.PushAsync(new AddJourneyView());
        }
    }
}
