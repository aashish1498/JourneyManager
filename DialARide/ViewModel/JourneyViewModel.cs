using System;
using Xamarin.Forms;

namespace DialARide
{
    public class JourneyViewModel
    {
        int index;
        public JourneyViewModel()
        {
        }

        public Color SingleColor
        {
            get
            {
                UpdateIndex();
                return JourneyRepository.Colors[index];
            }
        }

        private void UpdateIndex()
        {
            index = (index + 1) % JourneyRepository.Colors.Count;
        }
    }
}
