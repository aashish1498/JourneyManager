using System;
using Xamarin.Forms;

namespace DialARide
{
    public class Journey
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public int Id;

        public string DateString
        {
            get { return Date.ToShortDateString(); }
        }

        public Color JourneyColor
        {
            get
            {
                return JourneyRepository.Colors[Id % JourneyRepository.Colors.Count];
            }
        }

        public Journey(DateTime date, string time, string from, string destination, int id)
        {
            Date = date;
            Time = time;
            From = from;
            Destination = destination;
            Id = id;
        }
    }
}
