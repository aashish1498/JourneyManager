using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace DialARide
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class JourneyView : ContentPage
    {
        JourneyViewModel vm;
        //JourneyRepository journeyRepository;
        public JourneyView()
        {
            InitializeComponent();
            vm = new JourneyViewModel();
            BindingContext = vm;
            myList.ItemsSource = JourneyRepository.JourneyCollection;
            //journeyRepository = new JourneyRepository();
            //myList.ItemsSource = journeyRepository.JourneyCollection;
        }

        void Handle_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var journey = button.BindingContext as Journey;
            RemoveJourney(journey);

        }

        void RemoveJourney(Journey journey)
        {
            JourneyRepository.JourneyCollection.Remove(journey);
            if (Application.Current.Properties.ContainsKey(journey.Id.ToString()))
            {
                Application.Current.Properties.Remove(journey.Id.ToString());
                Application.Current.SavePropertiesAsync();
            }
            CrossLocalNotifications.Current.Cancel(journey.Id);

        }
    }
}
