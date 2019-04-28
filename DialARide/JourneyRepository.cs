using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace DialARide
{
    public class JourneyRepository
    {
        private const double ReminderTimeMinutes = 30;
        int index;
        public static ObservableCollection<Journey> JourneyCollection;
        public static List<Color> Colors;

        public Color SingleColor
        {
            get
            {
                UpdateIndex();
                return Colors[index];
            }
        }

        private void UpdateIndex()
        {
            index = (index + 1) % Colors.Count;
        }

        public JourneyRepository()
        {
            Colors = new List<Color>
            {
                Color.Teal,
                Color.Fuchsia,
                Color.MediumAquamarine,
                Color.BlueViolet
            };

            JourneyCollection = new ObservableCollection<Journey>();
            LoadJourneysFromProperties();
            ReorderSavedJourneys();

            //FillJourneys();
        }

        //private void FillJourneys()
        //{
        //    JourneyCollection.Add(new Journey(DateTime.Today, "16:00", new Place("London"), new Place("Bristol"), SingleColor ));
        //    JourneyCollection.Add(new Journey(DateTime.Today, "06:00", new Place("Bath"), new Place("Cambridge"), SingleColor));
        //    JourneyCollection.Add(new Journey(DateTime.Today, "12:05", new Place("Canary Wharf"), new Place("Stratford"), SingleColor));
        //}
        private void LoadJourneysFromProperties()
        {
            try
            {
                int i = 0;
                foreach (var kvp in Application.Current.Properties)
                {
                    var jsonString = kvp.Value as string;
                    var testJourney = JsonConvert.DeserializeObject<Journey>(jsonString);
                    CrossLocalNotifications.Current.Cancel(testJourney.Id);
                    testJourney.Id = i;
                    JourneyCollection.Add(testJourney);
                    i++;
                }
            }
            catch
            {
            }
        }

        private void ReorderSavedJourneys()
        {
            Application.Current.Properties.Clear();
            JourneyCollection = new ObservableCollection<Journey>(JourneyCollection.OrderBy(j => j.Date));
            foreach (var journey in JourneyCollection)
            {
                var jsonString = JsonConvert.SerializeObject(journey);
                Application.Current.Properties.Add(journey.Id.ToString(), jsonString);
                if (journey.Date - TimeSpan.FromMinutes(ReminderTimeMinutes) > DateTime.Now)
                {
                    CrossLocalNotifications.Current.Show("Upcoming Journey!", $"You have a journey to {journey.Destination} at {journey.Time}", journey.Id, journey.Date - TimeSpan.FromMinutes(ReminderTimeMinutes));
                }
            }
            Application.Current.SavePropertiesAsync();
        }

        public static Color GetNextColor()
        {
            var index = JourneyCollection.Count;
            index = (index + 1) % Colors.Count;
            return Colors[index];
        }
    }
}
