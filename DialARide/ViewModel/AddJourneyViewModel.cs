using System;
using System.Windows.Input;
using Newtonsoft.Json;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace DialARide.ViewModel
{
    public class AddJourneyViewModel
    {
        private double ReminderTimeMinutes = 30;
        public ICommand AddJourneyCommand { get; set; }

        public DateTime JourneyDate { get; set; }
        public TimeSpan JourneyTime { get; set; }
        public string FromPlaceString { get; set; }
        public string ToPlaceString { get; set; }

        public AddJourneyViewModel()
        {
            AddJourneyCommand = new Command(AddJourney);
            JourneyDate = DateTime.Today;
            JourneyTime = DateTime.Now.TimeOfDay;
        }

        void AddJourney()
        {
            JourneyDate += JourneyTime;
            var ID = Application.Current.Properties.Count;
            var newJourney = new Journey(JourneyDate, JourneyTime.ToString(@"hh\:mm"), FromPlaceString, ToPlaceString, ID);
            JourneyRepository.JourneyCollection.Add(newJourney);
            var json = JsonConvert.SerializeObject(newJourney);
            try
            {
                //Application.Current.Properties.Clear();
                Application.Current.Properties.Add(ID.ToString(), json);
            }
            catch
            {
            }

            CrossLocalNotifications.Current.Show("Upcoming Journey!", $"You have a journey to {ToPlaceString} at {JourneyTime.ToString(@"hh\:mm")}", ID, JourneyDate - TimeSpan.FromMinutes(ReminderTimeMinutes));
            Application.Current.SavePropertiesAsync();

            Application.Current.MainPage.Navigation.PopAsync();

            //JourneyRepository.JourneyCollection.Add(new Journey(DateTime.Today, "18:37", new Place("NewPlace"), new Place("EvenNewerPlace"), Color.BlanchedAlmond));
        }
    }
}
